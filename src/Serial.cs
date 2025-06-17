using System;
using System.IO.Ports;
using System.Threading;

namespace _4Term
{
    public sealed class Serial
    {
        /*----------------------------------------------------------
         * Constants
         * -------------------------------------------------------*/

        /* Maximum wait time for read/write operations*/
        private const int ReadWriteTimeout = 50;

        /* Sleep duration when port is unavailable */
        private const int LoopPreventDelay = 100;

        /* Pause after read timeouts */
        private const int TimeoutDelay = 10;

        /*----------------------------------------------------------
         * Class variables
         * -------------------------------------------------------*/

        /* Core serial port communication handler. */
        private readonly System.IO.Ports.SerialPort SerialPortInstance;

        /* Provides global access to the serial port manager. */
        public static Serial Instance => instance;

        /* The singleton instance of Serial class. */
        static readonly Serial instance = new Serial();
        
        /* Gets the current open/closed state of the serial port. */
        public bool IsOpen => SerialPortInstance.IsOpen;

        /* Control flag for serial port reading operations. */
        private volatile bool ReadThreadFlag = false;      

        /*----------------------------------------------------------
         * Event Handlers
         * -------------------------------------------------------*/

        /* Defines the signature for serial port notification events. */
        public delegate void EventHandler(string param);

        /* Event triggered when serial port status changes. */
        public event EventHandler StatusChanged;

        /* Event triggered when new data arrives from serial port.*/
        public event EventHandler DataReceived;

        /*----------------------------------------------------------
         * Threads
         * -------------------------------------------------------*/

        /* Background thread for continuous data reading. */
        private Thread ReadThreadInstance = null;         

        /*----------------------------------------------------------
         * Form
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] Serial (Constructor)
         * [✓] Open
         * [✓] ReadThread
         * [✓] Send
         * [✓] Close
         * [✓] SetDtr
         * [✓] SetRts
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * Serial (Constructor)
         * 
         * Private constructor for singleton Serial class instance.
         *---------------------------------------------------------*/
        private Serial()
        {
            SerialPortInstance = new System.IO.Ports.SerialPort();
        }

        /*----------------------------------------------------------
         * Open
         * 
         * Opens and configures the serial port connection with 
         * settings from application configuration.
         *---------------------------------------------------------*/
        public void Open()
        {
            /* Check if serial port is open, if not close serial port */
            if (SerialPortInstance.IsOpen)
                SerialPortInstance.Close();
            
            try
            {
                /* Configure the serial port instance with settings from the application configuration */
                SerialPortInstance.PortName = Settings.Port.PortName;
                SerialPortInstance.BaudRate = Settings.Port.BaudRate;
                SerialPortInstance.Parity = Settings.Port.Parity; 
                SerialPortInstance.DataBits = Settings.Port.DataBits;
                SerialPortInstance.StopBits = Settings.Port.StopBits;
                SerialPortInstance.Handshake = Settings.Port.Handshake;
                SerialPortInstance.ReadTimeout = ReadWriteTimeout;
                SerialPortInstance.WriteTimeout = ReadWriteTimeout;
                SerialPortInstance.DtrEnable = Settings.Port.Dtr;
                SerialPortInstance.RtsEnable = Settings.Port.Rts;

                /* Open serial port */
                SerialPortInstance.Open();

                /* Start read thread */
                try
                {
                    ReadThreadFlag = true;
                    ReadThreadInstance = new Thread(ReadThread);
                    ReadThreadInstance.Start();
                }

                /*This is a generic catch block that catches any other exceptions that may occur.*/
                catch (Exception ex)
                {
                    ReadThreadFlag = false;
                    ReadThreadInstance = null;
                    SerialPortInstance.Close();
                    StatusChanged?.Invoke($"Error: {ex.Message}");                 
                    return;
                }

                /* Set program status when connected to serial port */
                string Parity = SerialPortInstance.Parity.ToString().Substring(0, 1);
                string Handshake = SerialPortInstance.Handshake.ToString();
                if (SerialPortInstance.Handshake == System.IO.Ports.Handshake.None)
                    Handshake = "no handshake";

                StatusChanged?.Invoke($"Connected to {SerialPortInstance.PortName}: " +
                    $"{SerialPortInstance.BaudRate}, " +
                    $"{SerialPortInstance.DataBits}{Parity}{(int)SerialPortInstance.StopBits}, " +
                    $"{Handshake}");
            }

            /*This is a generic catch block that catches any other exceptions that may occur.*/
            catch (Exception ex)            
            { 
                StatusChanged?.Invoke($"Error: {ex.Message}");
            }       
        }

        /*----------------------------------------------------------
         * ReadThread
         * 
         * Background thread for continuous serial port data reading.
         *---------------------------------------------------------*/
        private void ReadThread()
        {
            /* Read while enabled and port is available */
            while (ReadThreadFlag && SerialPortInstance != null)         
            {
                /* Only proceed if port is open and operational */
                if (SerialPortInstance.IsOpen)            
                {
                    try
                    {
                        /* Read all immediately available data */
                        string serialIn = SerialPortInstance.ReadExisting();

                        /* Process non-empty data */
                        if (!string.IsNullOrEmpty(serialIn))
                        {
                            /* Notify subscribers of new data */
                            DataReceived?.Invoke(serialIn);                            
                        }
                    }

                    /* Handles serial port timeout exceptions during read operations. */
                    catch (TimeoutException ex)
                    {
                        StatusChanged?.Invoke($"Timeout: {ex.Message}");
                        Thread.Sleep(TimeoutDelay);
                    }

                    /* This is a generic catch block that catches any other exceptions that may occur. */
                    catch (Exception ex)
                    {                        
                        StatusChanged?.Invoke($"Error: {ex.Message}");
                        ReadThreadFlag = false;
                    }
                    
                }

                else
                {
                    /* Prevent tight loop when no port available */
                    Thread.Sleep(LoopPreventDelay);
                }
            }
        }

        /*----------------------------------------------------------
         * Send
         * 
         * Sends data through the serial port with configurable line 
         * ending.
         *---------------------------------------------------------*/
        public string Send(string data)
        {
            string LineEnding = "";

            /* Determine which line ending to append based on user settings */
            switch (Settings.Port.Append)
            {
                /* Carriage Return */
                case Settings.Port.AppendType.AppendCR:
                    LineEnding = "\r";
                    break;

                /* Line Feed */
                case Settings.Port.AppendType.AppendLF:
                    LineEnding = "\n";
                    break;

                /* Carriage Return + Line Feed */
                case Settings.Port.AppendType.AppendCRLF:
                    LineEnding = "\r\n";
                    break;
            }

            /* Send the data with the selected line ending via the serial port */
            SerialPortInstance.Write(data + LineEnding);

            /* Return the appended line ending so the caller can use/display it */
            return LineEnding;
        }

        /*----------------------------------------------------------
         * Close
         * 
         * Gracefully shuts down serial port connection and reading thread.
         *---------------------------------------------------------*/
        public void Close()
        {
            /* Signal the reading thread to stop */
            ReadThreadFlag = false;

            try
            {
                /* Check if thread instance exists and is alive */
                if (ReadThreadInstance?.IsAlive == true)
                {
                    /* Graceful shutdown */
                    if (!ReadThreadInstance.Join(2000))
                    {
                        /* Cooperative interruption */
                        ReadThreadInstance.Interrupt();

                        /* Forced termination */
                        if (!ReadThreadInstance.Join(500))
                            ReadThreadInstance.Abort(); 
                    }
                }

                /* Clean up thread reference */
                ReadThreadInstance = null;

                /* Clear serial port buffers if port is open */
                if (SerialPortInstance.IsOpen)
                {
                    /* Discard all buffered data */
                    SerialPortInstance.DiscardInBuffer();
                    SerialPortInstance.DiscardOutBuffer();

                    /* Close the port */
                    SerialPortInstance.Close();
                }

                StatusChanged?.Invoke("Disconnected");
            }

            /* Occurs when the reading thread is forcibly terminated via Thread.Interrupt().*/
            catch (ThreadInterruptedException)
            {
                StatusChanged?.Invoke("Warning: Reading thread was forcefully terminated");
            }

            /* This is a generic catch block that catches any other exceptions that may occur. */
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Error: {ex.Message}");
            } 
        }

        /*----------------------------------------------------------
         * SetDtr
         * 
         * Controls the Data Terminal Ready (DTR) signal state.
         * 
         * Safety:
         * - Only operates when port exists and is open
         * - No effect if port is not available
         * 
         * Typical Usage:
         * - Flow control handshaking
         * - Device reset/wakeup signals
         *---------------------------------------------------------*/
        public void SetDtr(bool enabled)
        {
            if (SerialPortInstance != null && SerialPortInstance.IsOpen)
                SerialPortInstance.DtrEnable = enabled;
        }

        /*----------------------------------------------------------
         * SetRts
         * 
         * Controls the Request to Send (RTS) signal state.
         * 
         * Safety:
         * - Only operates when port exists and is open
         * - No effect if port is not available
         * 
         * Typical Usage:
         * - Hardware flow control
         * - Half-duplex communication control
         *---------------------------------------------------------*/
        public void SetRts(bool enabled)
        {
            if (SerialPortInstance != null && SerialPortInstance.IsOpen)
                SerialPortInstance.RtsEnable = enabled;
        }
    }
}