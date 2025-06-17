using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace _4Term
{
    public partial class Main_Form : Form
    {
        /*----------------------------------------------------------
         * MenuStrip - File
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] NewPortStripMenuItem_Click
         * [✓] PortOptionsToolStripMenuItem_Click
         * [✓] ScanCOMsToolStripMenuItem_Click
         * [✓] NewWindowToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] ConnectDisconnectToolStripMenuItem_Click
         * [✓] AutoConnectToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] OpenToolStripMenuItem_Click
         * [✓] SaveToolStripMenuItem_Click
         * [✓] SaveAsToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] OpenConfigXmlToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] ExitToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * NewPortStripMenuItem_Click
         * 
         * Closes the current serial port if open, clears the RichTextBox,
         * opens the SerialPortOptionsForm for new configuration,
         * then opens the serial port with updated settings.
         *---------------------------------------------------------*/
        private void NewPortStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Check if serial port is open */
            if (Port.IsOpen)
            {
                /* Ask user if they want to close the serial port */
                DialogResult result = MessageBox.Show("A serial port connection is currently active. Close it to configure a new one?",
                            "Active Serial Connection",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                    Port.Close();
                if (result == DialogResult.No)
                    return;
            }

            /* Clear RichTextBox */
            RichTextBox.Clear();

            /* Open the SerialPortOptionsForm dialog to configure settings */
            SerialPortOptionsForm OptionsForm = new SerialPortOptionsForm();
            OptionsForm.ShowDialog();

            /* Open the serial port */
            Port.Open();
        }

        /*----------------------------------------------------------
         * PortOptionsToolStripMenuItem_Click
         * 
         * Opens the SerialPortOptionsForm dialog to configure serial port settings.
         *---------------------------------------------------------*/
        private void PortOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Open the SerialPortOptionsForm dialog to configure settings */
            SerialPortOptionsForm OptionsForm = new SerialPortOptionsForm();
            OptionsForm.ShowDialog();
        }

        /*----------------------------------------------------------
         * ScanCOMsToolStripMenuItem_Click
         * 
         * Retrieves and displays the list of available COM ports 
         * in the RichTextBox.
         *---------------------------------------------------------*/
        private void ScanCOMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.AppendText("-------------------------------------------------------------------------------------------------\n");

            /* Step 1: Get all available COM port names using the basic .NET API
             * This returns just the raw port names (e.g., "COM1", "COM3") */
            string[] allPorts = SerialPort.GetPortNames();

            /* Check if any ports were found */
            if (allPorts.Length == 0)
            {
                RichTextBox.SelectionColor = Color.Red;
                RichTextBox.AppendText("No COM ports found\n");
                RichTextBox.AppendText("-------------------------------------------------------------------------------------------------\n");
                return;
            }

            RichTextBox.AppendText("Detected COM Ports:\n");

            /* Step 2: Attempt to get detailed port descriptions using WMI
             * This provides more human-readable information about each port */
            Dictionary<string, string> portDescriptions = new Dictionary<string, string>();
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    "SELECT Caption, DeviceID FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'"))
                {
                    /* Execute query and process results */
                    foreach (ManagementBaseObject baseObj in searcher.Get())
                    {
                        ManagementObject obj = (ManagementObject)baseObj;
                        /* Get the device caption (description) and ID */
                        string caption = obj["Caption"]?.ToString();
                        string deviceId = obj["DeviceID"]?.ToString();

                        /* Only process if we have valid data */
                        if (caption != null && deviceId != null)
                        {
                            /* Extract COM port number from caption */
                            var match = System.Text.RegularExpressions.Regex.Match(caption, @"\((COM\d+)\)");
                            if (match.Success)
                            {
                                string comPort = match.Groups[1].Value;
                                /* Clean up the description by removing the COM port reference */
                                string description = caption.Replace($"({comPort})", "").Trim();
                                /* Store in dictionary for later lookup */
                                portDescriptions[comPort] = description;
                            }
                        }
                    }
                }
            }
            /* WMI query might fail due to permissions or other issues */
            catch (Exception ex)
            {
                RichTextBox.AppendText($"Note: Could not retrieve detailed port info ({ex.Message})\n");
            }

            /* Step 3: Display all detected ports with their descriptions
             * Ports are displayed in alphabetical order (COM1, COM2, etc.) */
            foreach (string port in allPorts.OrderBy(p => p))
            {
                /* Try to get the description from our WMI results */
                if (portDescriptions.TryGetValue(port, out string description))
                {
                    /* Display port with its detailed description in green */
                    RichTextBox.SelectionColor = Color.Tomato;
                    RichTextBox.AppendText($"{port}: ");
                    RichTextBox.SelectionColor = Color.MediumTurquoise;
                    RichTextBox.AppendText($"{description}\n");
                }
                else
                {
                    /* Fallback display for ports without WMI information in orange */
                    RichTextBox.SelectionColor = Color.Tomato;
                    RichTextBox.AppendText($"{port}: [Driver/Device description not available]\n");
                    RichTextBox.SelectionColor = Color.MediumTurquoise; // Reset to default color
                }
            }

            /* Display summary information */
            RichTextBox.AppendText($"-------------------------------------------------------------------------------------------------\n");
            RichTextBox.AppendText($"Total ports detected: ");
            RichTextBox.SelectionColor = Color.Gold;
            RichTextBox.AppendText($"{allPorts.Length}\n");
        }

        /*----------------------------------------------------------
         * NewWindowToolStripMenuItem_Click
         * 
         * Launches a new instance of the current application.
         *---------------------------------------------------------*/
        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Start new 4Term window */
            Process.Start(Process.GetCurrentProcess().ProcessName);
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ConnectDisconnectToolStripMenuItem_Click
         * 
         * Toggles the connection state by invoking the same logic 
         * as the Connect button.
         *---------------------------------------------------------*/
        private void ConnectDisconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Call the same logic used by the Connect button */
            ConnectButton_Click(sender, e);
        }

        /*----------------------------------------------------------
         * ConnectDisconnectToolStripMenuItem_Click
         * 
         * Toggles the Auto Connect feature by invoking the same logic 
         * as the Auto Connect button.
         *---------------------------------------------------------*/
        private void AutoConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Call the same logic used by the Auto Connect button */
            AutoConnectButton_Click(sender, e);
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * OpenToolStripMenuItem_Click
         * 
         * Opens a file dialog to select and load a file into the application.
         *---------------------------------------------------------*/
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { 
                /* Check if serial port is open */
                if (Port.IsOpen)
                {
                    /* Ask user if they want to close the serial port */
                    DialogResult result = MessageBox.Show("A serial port connection is currently active. Close it to proceed with file operations?",
                                "Active Serial Connection",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                        Port.Close();
                    if (result == DialogResult.No)
                        return;
                }
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    /* Set dialog properties */
                    openFileDialog.Title = "Select File to Open";
                    openFileDialog.Filter = "Log Files (*.log)|*.log|Config Files (*.cfg)|*.cfg|All Files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.CheckFileExists = true;
                    openFileDialog.CheckPathExists = true;

                    /* Process file selection */
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                    
                            /* Step 3: Handle the selected file */
                            FilePath = openFileDialog.FileName;

                            /* Verify file size before loading */
                            FileInfo fileInfo = new FileInfo(FilePath);
                            /* 10MB limit */
                            if (fileInfo.Length > 10 * 1024 * 1024)
                            {
                                MessageBox.Show(
                                    "The selected file is too large (maximum 10MB allowed).",
                                    "File Size Limit Exceeded",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                return;
                            }

                            /* Clear existing content */
                            RichTextBox.Clear();

                            /* Use ReadAllText with explicit encoding handling */
                            RichTextBox.Text = File.ReadAllText(FilePath, Encoding.UTF8);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        /*----------------------------------------------------------
         * SaveToolStripMenuItem_Click
         * 
         * Saves the current document.
         *---------------------------------------------------------*/
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) // added messageboxes, must be changed
        {
            /* Check if serial port is open */
            if (Port.IsOpen)
            {
                /* Ask user if they want to close the serial port */
                DialogResult result = MessageBox.Show("A serial port connection is currently active. Close it to proceed with file operations?",
                            "Active Serial Connection",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                    Port.Close();
                if (result == DialogResult.No)
                    return;
            }

            /* Validate content */
            if (string.IsNullOrWhiteSpace(RichTextBox.Text))
            {
                MessageBox.Show("There is nothing to save.",
                              "Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            /* Save to existing path or prompt for new one */
            try
            {
                if (!string.IsNullOrWhiteSpace(FilePath))
                    File.WriteAllText(FilePath, RichTextBox.Text, Encoding.UTF8);
                else
                    SaveAsToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        /*----------------------------------------------------------
         * SaveAsToolStripMenuItem_Click
         * 
         * Saves the current document to a user-specified location.
         *---------------------------------------------------------*/
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Check if serial port is open */
            if (Port.IsOpen)
            {
                /* Ask user if they want to close the serial port */
                DialogResult result = MessageBox.Show("A serial port connection is currently active. Close it to proceed with file operations?",
                            "Active Serial Connection",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                    Port.Close();
                if (result == DialogResult.No)
                    return;
            }

            /* Validate content */
            if (string.IsNullOrWhiteSpace(RichTextBox.Text))
            {
                MessageBox.Show("There is nothing to save.",
                              "Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            /* Show save dialog and save file */
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Log Files (*.log)|*.log|Config Files (*.cfg)|*.cfg|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.RestoreDirectory = true;

                /* Set initial directory if available */
                if (!string.IsNullOrWhiteSpace(FilePath))
                {
                    saveFileDialog.InitialDirectory = Path.GetDirectoryName(FilePath);
                    saveFileDialog.FileName = Path.GetFileNameWithoutExtension(FilePath);
                }

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, RichTextBox.Text, Encoding.UTF8);
                        FilePath = saveFileDialog.FileName;
                        MessageBox.Show("File saved successfully.",
                                      "Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}",
                                      "Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                    }
                }
            }
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * OpenConfigXmlToolStripMenuItem_Click
         * 
         * Opens and loads the configuration from the config.xml file.
         *---------------------------------------------------------*/
        private void OpenConfigXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                /* Check if serial port is open */
                if (Port.IsOpen)
                {
                    /* Ask user if they want to close the serial port */
                    DialogResult result = MessageBox.Show("A serial port connection is currently active. Close it to proceed with file operations?",
                                "Active Serial Connection",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                        Port.Close();
                    if (result == DialogResult.No)
                        return;
                }

                /* Validate config file existence */
                if (!File.Exists(Xml.FileNameq))
                {
                    MessageBox.Show($"Configuration file not found: {Xml.FileNameq}",
                                  "File Missing",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                /* Check file size(limit to 1MB for XML) */
                FileInfo configFile = new FileInfo(Xml.FileNameq);
                if (configFile.Length > 1024 * 1024)  // 1MB limit
                {
                    MessageBox.Show("Configuration file exceeds maximum size (1MB)",
                                  "File Too Large",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return;
                }

                /* Clear and load content */
                RichTextBox.Clear();

                /* Read with explicit UTF-8 encoding and error handling */
                RichTextBox.Text = File.ReadAllText(Xml.FileNameq, Encoding.UTF8);
                FilePath = Xml.FileNameq;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ExitToolStripMenuItem_Click
         * 
         * Cleans buffers, closes the COM port properly, 
         * and exits the application.
         *---------------------------------------------------------*/
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*----------------------------------------------------------
         * MenuStrip - Edit
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] UndoToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] CutToolStripMenuItem_Click
         * [✓] CopyToolStripMenuItem_Click
         * [✓] PasteToolStripMenuItem_Click
         * [✓] DeleteToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] SelectAllToolStripMenuItem_Click
         * ---------------------------------------------------------
         * [✓] ClearToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * UndoToolStripMenuItem_Click
         * 
         * Reverts the last change made in the RichTextBox
         *---------------------------------------------------------*/
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Undo();
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * CutToolStripMenuItem_Click
         * 
         * Cuts the selected text from the RichTextBox
         * and places it on the clipboard
         *---------------------------------------------------------*/
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Cut();
        }

        /*----------------------------------------------------------
         * CopyToolStripMenuItem_Click
         * 
         * Copies the selected text from the RichTextBox
         * to the clipboard
         *---------------------------------------------------------*/
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Copy();
        }

        /*----------------------------------------------------------
         * PasteToolStripMenuItem_Click
         * 
         * Pastes the clipboard content into the RichTextBox
         * at the current cursor position
         *---------------------------------------------------------*/
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Paste();
        }

        /*----------------------------------------------------------
         * DeleteToolStripMenuItem_Click
         * 
         * Deletes the selected text from the RichTextBox
         *---------------------------------------------------------*/
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectedText = "";
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * SelectAllToolStripMenuItem_Click
         * 
         * Selects all the text in the RichTextBox
         *---------------------------------------------------------*/
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectAll();
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ClearToolStripMenuItem_Click
         * 
         * Clears all text from the RichTextBox
         *---------------------------------------------------------*/
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.Clear();
        }

        /*----------------------------------------------------------
         * MenuStrip - Format
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] WordWrapToolStripMenuItem_Click
         * [✓] SettingsToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * WordWrapToolStripMenuItem_Click
         * 
         * Toggles word wrap in the RichTextBox. Updates the
         * settings and saves them to XML.
         *---------------------------------------------------------*/
        private void WordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.WordWrap = !RichTextBox.WordWrap;
            Settings.RichTextBox.WordWrap = WordWrapToolStripMenuItem.Checked = RichTextBox.WordWrap;
            Settings.WriteXml();
        }

        /*----------------------------------------------------------
         * SettingsToolStripMenuItem_Click
         * 
         * Opens the Settings form as a modal dialog
         *---------------------------------------------------------*/
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings_Form SettingsForm = new Settings_Form();
            SettingsForm.ShowDialog();
            SetAdvancedMode(Settings.RichTextBox.AdvancedMode);
        }

        /*----------------------------------------------------------
         * MenuStrip - View
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] ToggleScrollingToolStripMenuItem_Click
         * [✓] MacroModeToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ToggleScrollingToolStripMenuItem_Click
         * 
         * Toggles the custom scrolling feature. Updates the setting
         * and saves it to XML.
         *---------------------------------------------------------*/
        private void ToggleScrollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleScrollingToolStripMenuItem.Checked = !ToggleScrollingToolStripMenuItem.Checked;
            Settings.RichTextBox.Scroll = ToggleScrollingToolStripMenuItem.Checked;
            Settings.WriteXml();
        }

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * MacroModeToolStripMenuItem_Click
         * 
         * Toggles Macro Mode. When enabled, it activates all macro 
         * editing UI elements and disables the main interface. 
         * Updates visibility and enable states accordingly.
         *---------------------------------------------------------*/
        private void MacroModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MacroEditingFlag = !MacroEditingFlag;
            SetMacroSetMode(MacroEditingFlag);
        }
/*
        private void MacroRepeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MacroRepeatTimer.Enabled)
            {
                MacroRepeatToolStripMenuItem.Checked = false;
                MacroRepeatTimer.Enabled = false;
            }
            else
            {
                MacroRepeatFlag = true;
                MacroRepeatToolStripMenuItem.Checked = true;
                RichTextBox.SelectionColor = Color.IndianRed;
                RichTextBox.AppendText("Press any macro button to repeat\n");
            }
        }*/

        /*----------------------------------------------------------
         * MenuStrip - Help
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] AboutToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * AboutToolStripMenuItem_Click
         * 
         * Opens the About form as a modal dialog to display
         * application information.
         *---------------------------------------------------------*/
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Form AboutForm = new About_Form();
            AboutForm.ShowDialog();
        }

        private void M1SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!M2Flag && !M3Flag && !M4Flag && !M5Flag)
            {
                M1ToolStripMenuItem.BackColor = Color.Yellow;
                M1Flag = true;
                InfoMessage("Please Select macro\n");
            }
        }

        private void M2SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!M1Flag && !M3Flag && !M4Flag && !M5Flag)
            {
                M2ToolStripMenuItem.BackColor = Color.Yellow;
                M2Flag = true;
                InfoMessage("Please Select macro\n");
            }
        }

        private void M3SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!M1Flag && !M2Flag && !M4Flag && !M5Flag)
            {
                M3ToolStripMenuItem.BackColor = Color.Yellow;
                M3Flag = true;
                InfoMessage("Please Select macro\n");
            }
        }

        private void M4SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!M1Flag && !M2Flag && !M3Flag && !M5Flag)
            {
                M4ToolStripMenuItem.BackColor = Color.Yellow;
                M4Flag = true;
                InfoMessage("Please Select macro\n");
            
      
            }
        }

        private void M5SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!M1Flag && !M2Flag && !M3Flag && !M4Flag)
            {
                M5ToolStripMenuItem.BackColor = Color.Yellow;
                M5Flag = true;
                InfoMessage("Please Select macro\n");
            }
        }

        private void AdvancedModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdvancedModeToolStripMenuItem.Checked = !AdvancedModeToolStripMenuItem.Checked;
            SetAdvancedMode(AdvancedModeToolStripMenuItem.Checked);
            Settings.WriteXml();
        }

        private void M1ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Port.IsOpen && (CurrentM1Repeat != -1))
                {
                    if (!M1Timer.Enabled)
                    {

                        if (int.TryParse(M1ToolStripTextBox.Text, out int interval) && interval > 0)
                        {
                            
                            M1Timer.Interval = interval;
                            M1Timer.Enabled = true;
                        }
                        else
                        {
                            RichTextBox.SelectionColor = Color.IndianRed;
                            RichTextBox.AppendText("Please enter a valid positive number.\n");
                        }
                    }
                    else
                    {
                        M1Timer.Enabled = false;
                        M1ToolStripMenuItem.BackColor = SystemColors.Control;
                    }
                }
                else
                    RichTextBox.AppendText("COM port is not open.\n");
            }
        }

        private void M2ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Port.IsOpen && (CurrentM2Repeat != -1))
                {
                    if (!M2Timer.Enabled)
                    {

                        if (int.TryParse(M2ToolStripTextBox.Text, out int interval) && interval > 0)
                        {
                            M2ToolStripMenuItem.BackColor = Color.Green;
                            M2Timer.Interval = interval;
                            M2Timer.Enabled = true;
                        }
                        else
                        {
                            RichTextBox.SelectionColor = Color.IndianRed;
                            RichTextBox.AppendText("Please enter a valid positive number.\n");
                        }
                    }
                    else
                    {
                        M2Timer.Enabled = false;
                        M2ToolStripMenuItem.BackColor = SystemColors.Control;
                    }
                }
                else
                    RichTextBox.AppendText("COM port is not open.\n");
            }
        }

        private void M3ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Port.IsOpen && (CurrentM3Repeat != -1))
                {
                    if (!M3Timer.Enabled)
                    {

                        if (int.TryParse(M3ToolStripTextBox.Text, out int interval) && interval > 0)
                        {
                            M3ToolStripMenuItem.BackColor = Color.Green;
                            M3Timer.Interval = interval;
                            M3Timer.Enabled = true;
                        }
                        else
                        {
                            RichTextBox.SelectionColor = Color.IndianRed;
                            RichTextBox.AppendText("Please enter a valid positive number.\n");
                        }
                    }
                    else
                    {
                        M3Timer.Enabled = false;
                        M3ToolStripMenuItem.BackColor = SystemColors.Control;
                    }
                }
                else
                    RichTextBox.AppendText("COM port is not open.\n");
            }
        }

        private void M4ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Port.IsOpen && (CurrentM4Repeat != -1))
                {
                    if (!M4Timer.Enabled)
                    {
                        if (int.TryParse(M4ToolStripTextBox.Text, out int interval) && interval > 0)
                        {
                            M4ToolStripMenuItem.BackColor = Color.Green;
                            M4Timer.Interval = interval;
                            M4Timer.Enabled = true;
                        }
                        else
                        {
                            RichTextBox.SelectionColor = Color.IndianRed;
                            RichTextBox.AppendText("Please enter a valid positive number.\n");
                        }
                    }
                    else
                    {
                        M4Timer.Enabled = false;
                        M4ToolStripMenuItem.BackColor = SystemColors.Control;
                    }
                }
                else
                    RichTextBox.AppendText("COM port is not open.\n");
            }
        }

        private void M5ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Port.IsOpen && (CurrentM5Repeat != -1))
                {
                    if (!M5Timer.Enabled)
                    {
                        if (int.TryParse(M5ToolStripTextBox.Text, out int interval) && interval > 0)
                        {
                            M5ToolStripMenuItem.BackColor = Color.Green;
                            M5Timer.Interval = interval;
                            M5Timer.Enabled = true;
                        }
                        else
                        {
                            RichTextBox.SelectionColor = Color.IndianRed;
                            RichTextBox.AppendText("Please enter a valid positive number.\n");
                        }
                    }
                    else
                    {
                        M5Timer.Enabled = false;
                        M5ToolStripMenuItem.BackColor = SystemColors.Control;
                    }
                }
                else
                    RichTextBox.AppendText("COM port is not open.\n");
            }
        }

        private void DtrToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Settings.Port.Dtr = !Settings.Port.Dtr;
                DtrToolStripMenuItem.BackColor = Settings.Port.Dtr ? Color.Green : SystemColors.Control;
                if (Serial.Instance.IsOpen)
                    Serial.Instance.SetDtr(Settings.Port.Dtr);
                Settings.WriteXml();
            }
        }

        private void RtsToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Settings.Port.Rts = !Settings.Port.Rts;
                RtsToolStripMenuItem.BackColor = Settings.Port.Rts ? Color.Green : SystemColors.Control;
                if (Serial.Instance.IsOpen)
                    Serial.Instance.SetRts(Settings.Port.Rts);
                Settings.WriteXml();
            }
        }

    }
}
