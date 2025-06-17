using System;
using System.IO.Ports;
using System.Management;
using System.Windows.Forms;
using System.Collections.Generic;

namespace _4Term
{
    public partial class SerialPortOptionsForm : Form
    {
        /*----------------------------------------------------------
         * Class Functions
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] SerialPortOptionsForm (Constructor)
         * [✓] COMPortComboBox_Click
         * [✓] BaudRateRadioButton_Click
         * [✓] BaudRateCustomRadioButton_Click
         * [✓] SaveSettings_Click
         * [✓] WndProc
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * SerialPortOptionsForm (Constructor)
         * 
         * Initializes serial port configuration form components.
         *---------------------------------------------------------*/
        public SerialPortOptionsForm()
        {
            InitializeComponent();

            /* Refresh list of available COM ports */
            COMPortComboBox_Click(this, EventArgs.Empty);

            /* Load baudrate */
            switch (Settings.Port.BaudRate)
            {
                case 600: BaudRate600RadioButton.Checked = true; break;
                case 1200: BaudRate1200RadioButton.Checked = true; break;
                case 2400: BaudRate2400RadioButton.Checked = true; break;
                case 4800: BaudRate4800RadioButton.Checked = true; break;
                case 9600: BaudRate9600RadioButton.Checked = true; break;
                case 14400: BaudRate14400RadioButton.Checked = true; break;
                case 19200: BaudRate19200RadioButton.Checked = true; break;
                case 28800: BaudRate28800RadioButton.Checked = true; break;
                case 38400: BaudRate38400RadioButton.Checked = true; break;
                case 56000: BaudRate56000RadioButton.Checked = true; break;
                case 57600: BaudRate57600RadioButton.Checked = true; break;
                case 115200: BaudRate115200RadioButton.Checked = true; break;
                case 128000: BaudRate128000RadioButton.Checked = true; break;
                case 256000: BaudRate256000RadioButton.Checked = true; break;
                default:
                    /* Fallback to custom baud rate */
                    BaudRateCustomRadioButton.Checked = true;
                    BaudRateCustomRadioButton_Click(this, EventArgs.Empty);
                    CustomBRTextBox.Text = Settings.Port.BaudRate.ToString();
                    break;
            }

            /* Load append */
            switch (Settings.Port.Append)
            {
                case Settings.Port.AppendType.AppendNothing: AppendNoneRadioButton.Checked = true; break;
                case Settings.Port.AppendType.AppendCR: AppendCRRadioButton.Checked = true; break;
                case Settings.Port.AppendType.AppendLF: AppendLFRadioButton.Checked = true; break;
                case Settings.Port.AppendType.AppendCRLF: AppendCRLFRadioButton.Checked = true; break;
            }

            /* Load data bits */
            switch (Settings.Port.DataBits)
            {
                case 5: DataBits5RadioButton.Checked = true; break;
                case 6: DataBits6RadioButton.Checked = true; break;
                case 7: DataBits7RadioButton.Checked = true; break;
                case 8: DataBits8RadioButton.Checked = true; break;
            }
            
            /* Load parity */
            switch (Settings.Port.Parity)
            {
                case System.IO.Ports.Parity.None: ParityNoneRadioButton.Checked = true; break;
                case System.IO.Ports.Parity.Odd: ParityOddRadioButton.Checked = true; break;
                case System.IO.Ports.Parity.Even: ParityEvenRadioButton.Checked = true; break;
                case System.IO.Ports.Parity.Mark: ParityMarkRadioButton.Checked = true; break;
                case System.IO.Ports.Parity.Space: ParitySpaceRadioButton.Checked = true; break;
            }

            /* Load stop bits */
            switch (Settings.Port.StopBits)
            {
                case System.IO.Ports.StopBits.One: StopBits1RadioButton.Checked = true; break;
                case System.IO.Ports.StopBits.OnePointFive: StopBits15RadioButton.Checked = true; break;
                case System.IO.Ports.StopBits.Two: StopBits2RadioButton.Checked = true; break;
            }

            /* Load handshake */
            switch (Settings.Port.Handshake)
            {
                case System.IO.Ports.Handshake.None: HandshakingNoneRadioButton.Checked = true; break;
                case System.IO.Ports.Handshake.XOnXOff: HandshakingRTSCTSRadioButton.Checked = true; break;
                case System.IO.Ports.Handshake.RequestToSend: HandshakingXONXOFRadioButton.Checked = true; break;
                case System.IO.Ports.Handshake.RequestToSendXOnXOff: HandshakingRTSCTSXONXOFRadioButton.Checked = true; break;
            }

            /* Load dtr */
            TransmitDTRCheckBox.Checked = Settings.Port.Dtr;

            /* Load rts */
            TransmitRTSCheckBox.Checked = Settings.Port.Rts;
        }

        

        /*----------------------------------------------------------
         * COMPortComboBox_Click
         * 
         * Refreshes COM port list with device descriptions.
         *---------------------------------------------------------*/
        private void COMPortComboBox_Click(object sender, EventArgs e)
        {
            /* Clear existing port listings */
            COMPortComboBox.Items.Clear();

            int found = 0;

            /* Retrieve raw COM port names */
            string[] portList = System.IO.Ports.SerialPort.GetPortNames();

            /* Dictionary to store port descriptions */
            Dictionary<string, string> portDescriptions = new Dictionary<string, string>();

            /* Attempt to get friendly port names from Windows Management */
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    "SELECT Caption, DeviceID FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'"))
                {
                    foreach (ManagementBaseObject baseObj in searcher.Get())
                    {
                        ManagementObject obj = (ManagementObject)baseObj;
                        string caption = obj["Caption"]?.ToString();
                        string deviceId = obj["DeviceID"]?.ToString();

                        if (caption != null && deviceId != null)
                        {
                            var match = System.Text.RegularExpressions.Regex.Match(caption, @"\((COM\d+)\)");
                            if (match.Success)
                            {
                                string comPort = match.Groups[1].Value;
                                string description = caption.Replace($"({comPort})", "").Trim();
                                portDescriptions[comPort] = description;
                            }
                        }
                    }
                }
            }

            /* Silent fail - use basic port names if WMI unavailable */
            catch (Exception ){}

            /* Build display items combining port names and descriptions */
            for (int i = 0; i < portList.Length; ++i)
            {
                string name = portList[i];
                string displayText = name;

                /* Append description if available */
                if (portDescriptions.TryGetValue(name, out string desc))
                {
                    displayText += $" - {desc}";
                }

                COMPortComboBox.Items.Add(displayText);

                /* Track currently configured port */
                if (name == Settings.Port.PortName)
                    found = i;
            }

            /* Select configured port if available */
            if (portList.Length > 0)
                COMPortComboBox.SelectedIndex = found;
        }

        /*----------------------------------------------------------
         * BaudRateRadioButton_Click
         * 
         * Hides custom baud rate input and resizes group box.
         *---------------------------------------------------------*/
        private void BaudRateRadioButton_Click(object sender, EventArgs e)
        {
            CustomBRTextBox.Visible = CustomBRTextBox.Enabled = CustomBRGroupBox.Visible = CustomBRGroupBox.Enabled = false;
            ComPortGroupBox.Size = new System.Drawing.Size(99, 95);
        }

        /*----------------------------------------------------------
         * BaudRateCustomRadioButton_Click
         * 
         * Shows custom baud rate input and resizes group box.
         *---------------------------------------------------------*/
        private void BaudRateCustomRadioButton_Click(object sender, EventArgs e)
        {
            CustomBRTextBox.Visible = CustomBRTextBox.Enabled = CustomBRGroupBox.Visible = CustomBRGroupBox.Enabled = true;
            ComPortGroupBox.Size = new System.Drawing.Size(99, 46);
        }

        /*----------------------------------------------------------
         * SaveSettings_Click
         * 
         * Saves current UI selections to application settings.
         *---------------------------------------------------------*/
        private void SaveOptions_Click(object sender, EventArgs e)
        {
            /* Set COM Port */
            Settings.Port.PortName = COMPortComboBox.Text.Split('-')[0].Trim();

            /* Set baudrate */
            if (BaudRateCustomRadioButton.Checked && int.TryParse(CustomBRTextBox.Text, out int customBaud))
            {
                Settings.Port.BaudRate = customBaud;
            }
            else
            {
                Settings.Port.BaudRate = BaudRate600RadioButton.Checked ? 600 :
                                        BaudRate1200RadioButton.Checked ? 1200 :
                                        BaudRate2400RadioButton.Checked ? 2400 :
                                        BaudRate4800RadioButton.Checked ? 4800 :
                                        BaudRate9600RadioButton.Checked ? 9600 :
                                        BaudRate14400RadioButton.Checked ? 14400 :
                                        BaudRate19200RadioButton.Checked ? 19200 :
                                        BaudRate28800RadioButton.Checked ? 28800 :
                                        BaudRate38400RadioButton.Checked ? 38400 :
                                        BaudRate56000RadioButton.Checked ? 56000 :
                                        BaudRate57600RadioButton.Checked ? 57600 :
                                        BaudRate115200RadioButton.Checked ? 115200 :
                                        BaudRate128000RadioButton.Checked ? 128000 :
                                        BaudRate256000RadioButton.Checked ? 256000 : 9600;
            }

            /* Set data bits */
            Settings.Port.DataBits = DataBits5RadioButton.Checked ? 5 :
                                    DataBits6RadioButton.Checked ? 6 :
                                    DataBits7RadioButton.Checked ? 7 : 8;

            /* Set append */
            Settings.Port.Append = AppendNoneRadioButton.Checked ? Settings.Port.AppendType.AppendNothing :
                                  AppendCRRadioButton.Checked ? Settings.Port.AppendType.AppendCR :
                                  AppendLFRadioButton.Checked ? Settings.Port.AppendType.AppendLF :
                                  Settings.Port.AppendType.AppendCRLF;

            /* Set parity */
            Settings.Port.Parity = ParityNoneRadioButton.Checked ? Parity.None :
                                  ParityOddRadioButton.Checked ? Parity.Odd :
                                  ParityEvenRadioButton.Checked ? Parity.Even :
                                  ParityMarkRadioButton.Checked ? Parity.Mark : Parity.Space;

            /* Set stop bits */
            Settings.Port.StopBits = StopBits1RadioButton.Checked ? StopBits.One :
                                    StopBits15RadioButton.Checked ? StopBits.OnePointFive : StopBits.Two;

            /* Set handshake */
            Settings.Port.Handshake = HandshakingNoneRadioButton.Checked ? Handshake.None :
                                     HandshakingXONXOFRadioButton.Checked ? Handshake.XOnXOff :
                                     HandshakingRTSCTSRadioButton.Checked ? Handshake.RequestToSend :
                                     Handshake.RequestToSendXOnXOff;

            /* Set dtr */
            Settings.Port.Dtr = TransmitDTRCheckBox.Checked;

            /* Set rts */
            Settings.Port.Rts = TransmitRTSCheckBox.Checked;

            /* Save all settings to XML */
            Settings.WriteXml();

            /* Close settings form */
            Close();
        }

        /*----------------------------------------------------------
         * WndProc
         * 
         * Blocks right-clicks on title bar/borders.
        *---------------------------------------------------------*/
        protected override void WndProc(ref Message buffer)
        {
            if (buffer.Msg == 0xA3)
            {
                return;
            }
            base.WndProc(ref buffer);
        }

        
    }
}
