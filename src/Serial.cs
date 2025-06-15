using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace _4Term
{
    public sealed class Serial
    {   
        private readonly System.IO.Ports.SerialPort SerialPortInstance; // Handles communication with external devices via the serial port
        public static Serial Instance => instance; // Provides global access to the single instance of the Serial class (Singleton pattern)
        static readonly Serial instance = new Serial();// Lazily initialized, thread-safe singleton instance of the Serial class 
        private Thread ThreadInstance; // Thread instance responsible for reading from the serial port
        public bool IsOpen => SerialPortInstance.IsOpen; // Serial port flag
        private volatile bool ReadFlag; // Flag to control whether reading from the serial port is enabled  
        public delegate void EventHandler(string param); // Delegate for handling status and data received events   
        public event EventHandler StatusChanged; // Event for notifying changes in the status
        public event EventHandler DataReceived; // Event for notifying data received
        public string[] GetAvailablePorts() => System.IO.Ports.SerialPort.GetPortNames(); // Returns an array of available serial port names
                                                                                         
        private Serial()
        {
            SerialPortInstance = new System.IO.Ports.SerialPort();
            ThreadInstance = null;
            ReadFlag = false;
        }

        public void Open()
        {
            /* Check if serial port is open, if not close serial port */
            if (IsOpen)
                Close();
            
            try
            {
                /* Configure the serial port instance with settings from the application configuration */
                SerialPortInstance.PortName = Settings.Port.PortName;
                SerialPortInstance.BaudRate = Settings.Port.BaudRate;
                SerialPortInstance.Parity = Settings.Port.Parity; 
                SerialPortInstance.DataBits = Settings.Port.DataBits;
                SerialPortInstance.StopBits = Settings.Port.StopBits;
                SerialPortInstance.Handshake = Settings.Port.Handshake;
                SerialPortInstance.ReadTimeout = 50;
                SerialPortInstance.WriteTimeout = 50;
                SerialPortInstance.DtrEnable = Settings.Port.Dtr;
                SerialPortInstance.RtsEnable = Settings.Port.Rts;

                /* Open serial port */
                SerialPortInstance.Open();

                /* Start read thread */
                try
                {
                    ReadFlag = true;
                    ThreadInstance = new Thread(ReadData);
                    ThreadInstance.Start();
                }

                /*This is a generic catch block that catches any other exceptions that may occur.*/
                catch (Exception ex)
                { StatusChanged?.Invoke($"Error: Unexpected exception: {ex.Message}"); return; }

                /* Set program status when connected to serial port */
                string HandshakeDescription = SerialPortInstance.Handshake.ToString();
                if (SerialPortInstance.Handshake == Handshake.None)
                    HandshakeDescription = "no handshake";
                StatusChanged?.Invoke($"Connected to: {SerialPortInstance.PortName}: {SerialPortInstance.BaudRate}, " +
                    $"{SerialPortInstance.DataBits}{SerialPortInstance.Parity.ToString().Substring(0, 1)}{(int)SerialPortInstance.StopBits}, " +
                    $"{HandshakeDescription}");
            }

            /*This is a generic catch block that catches any other exceptions that may occur.*/
            catch (Exception ex)            
            { 
                StatusChanged?.Invoke($"Error: {ex.Message}"); 
            }       
        }

        private void ReadData()
        {
            /* Read while enabled and port is available */
            while (ReadFlag && SerialPortInstance != null)         
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
                        else
                        {
                            /* Prevent tight loop when no data available */
                            Thread.Sleep(1);
                        }
                    }

                    catch (TimeoutException tex)
                    {
                        StatusChanged?.Invoke($"Timeout: {tex.Message}");
                        Thread.Sleep(10);
                    }

                    /* This is a generic catch block that catches any other exceptions that may occur. */
                    catch (Exception ex)
                    {                        
                        StatusChanged?.Invoke($"Error: {ex.Message}");
                        ReadFlag = false;
                    }
                    
                }
                else
                {
                    /* Prevent tight loop when no port available */
                    Thread.Sleep(50);
                }
            }
        }

        public string Send(string data)
        {
            string lineEnding = "";

            /* Determine which line ending to append based on user settings */
            switch (Settings.Port.AppendToSend)
            {
                /* Carriage Return */
                case Settings.Port.AppendType.AppendCR:
                    lineEnding = "\r";
                    break;

                /* Line Feed */
                case Settings.Port.AppendType.AppendLF:
                    lineEnding = "\n";
                    break;

                /* Carriage Return + Line Feed */
                case Settings.Port.AppendType.AppendCRLF:
                    lineEnding = "\r\n";
                    break;
            }

            /* Send the data with the selected line ending via the serial port */
            SerialPortInstance.Write(data + lineEnding);

            /* Return the appended line ending so the caller can use/display it */
            return lineEnding;
        }

        public void Close()
        {
            /* Signal the reading thread to stop */
            ReadFlag = false;
            try
            {
                /* Check if thread instance exists and is alive */
                if (ThreadInstance?.IsAlive == true)
                {
                    /* Graceful shutdown */
                    if (!ThreadInstance.Join(2000))
                    {
                        /* Cooperative interruption */
                        ThreadInstance.Interrupt();
                        if (!ThreadInstance.Join(500))
                            /* Forced termination */
                            ThreadInstance.Abort(); 
                    }
                }

                /* Clean up thread reference */
                ThreadInstance = null;

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

            /* Occurs when the reading thread is forcibly terminated via Thread.Interrupt().
             * This is a controlled interruption when the thread fails to stop gracefully.
             * We log it as a warning rather than an error since it's an expected fallback behavior. */
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
    }
}