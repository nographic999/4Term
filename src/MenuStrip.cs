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
        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(Process.GetCurrentProcess().ProcessName);

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ConnectDisconnectToolStripMenuItem_Click
         * 
         * Toggles the connection state by invoking the same logic 
         * as the Connect button.
         *---------------------------------------------------------*/
        private void ConnectDisconnectToolStripMenuItem_Click(object sender, EventArgs e) => ConnectButton_Click(sender, e);

        /*----------------------------------------------------------
         * ConnectDisconnectToolStripMenuItem_Click
         * 
         * Toggles the Auto Connect feature by invoking the same logic 
         * as the Auto Connect button.
         *---------------------------------------------------------*/
        private void AutoConnectToolStripMenuItem_Click(object sender, EventArgs e) => AutoConnectButton_Click(sender, e);

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
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

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
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.Undo();

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * CutToolStripMenuItem_Click
         * 
         * Cuts the selected text from the RichTextBox
         * and places it on the clipboard
         *---------------------------------------------------------*/
        private void CutToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.Cut();

        /*----------------------------------------------------------
         * CopyToolStripMenuItem_Click
         * 
         * Copies the selected text from the RichTextBox
         * to the clipboard
         *---------------------------------------------------------*/
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.Copy();

        /*----------------------------------------------------------
         * PasteToolStripMenuItem_Click
         * 
         * Pastes the clipboard content into the RichTextBox
         * at the current cursor position
         *---------------------------------------------------------*/
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.Paste();
            
        /*----------------------------------------------------------
         * DeleteToolStripMenuItem_Click
         * 
         * Deletes the selected text from the RichTextBox
         *---------------------------------------------------------*/
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.SelectedText = "";

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * SelectAllToolStripMenuItem_Click
         * 
         * Selects all the text in the RichTextBox
         *---------------------------------------------------------*/
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.SelectAll();

        /*----------------------------------------------------------
         * Separator
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ClearToolStripMenuItem_Click
         * 
         * Clears all text from the RichTextBox
         *---------------------------------------------------------*/
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e) => RichTextBox.Clear();

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
            

            /* Set Form size*/
            Size = new Size(Settings.Form.Width, Settings.Form.Height);

            /* Set Advanced Mode */
            SetAdvancedMode(Settings.Form.AdvancedMode);

            /* Set RichTextBox font */
            FontStyle style = Enum.TryParse(Settings.RichTextBox.FontStyle, out FontStyle parsedStyle)
                ? parsedStyle
                : FontStyle.Regular;

            if (Settings.RichTextBox.Underline)
                style |= FontStyle.Underline;

            if (Settings.RichTextBox.Strikeout)
                style |= FontStyle.Strikeout;

            RichTextBox.Font = new Font(
                new FontFamily(Settings.RichTextBox.Font),
                Settings.RichTextBox.Size,
                style
            );

            /* Set Word wrap */
            RichTextBox.WordWrap = WordWrapToolStripMenuItem.Checked = Settings.RichTextBox.WordWrap;

            /* Set Toggle scrolling */
            ToggleScrollingToolStripMenuItem.Checked = Settings.RichTextBox.Scroll;

            /* Set RichTextBox background color */
            RichTextBox.BackColor = Color.FromArgb(Settings.Color.Back.R, Settings.Color.Back.G, Settings.Color.Back.B);
        }

        /*----------------------------------------------------------
         * MenuStrip - View
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] ToggleScrollingToolStripMenuItem_Click
         * [✓] MacroModeToolStripMenuItem_Click
         * [✓] AdvancedModeToolStripMenuItem_Click
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

        /*----------------------------------------------------------
         * AdvancedModeToolStripMenuItem_Click
         * 
         * Toggles the Advanced Mode option when clicked,
         * updates the UI accordingly, and saves the setting.
         *---------------------------------------------------------*/
        private void AdvancedModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdvancedModeToolStripMenuItem.Checked = !AdvancedModeToolStripMenuItem.Checked;
            SetAdvancedMode(AdvancedModeToolStripMenuItem.Checked);
            Settings.WriteXml();
        }

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

        /*----------------------------------------------------------
         * MenuStrip - DTR
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] DtrToolStripMenuItem_MouseDown
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * DtrToolStripMenuItem_MouseDown
         * 
         * Toggles the DTR (Data Terminal Ready) signal setting on right-click.
         * Updates the menu item color to indicate status and applies the setting
         * to the serial port if open. Saves the updated settings.
         *---------------------------------------------------------*/
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

        /*----------------------------------------------------------
         * MenuStrip - RTS
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓]  RtsToolStripMenuItem_MouseDown
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * RtsToolStripMenuItem_MouseDown
         * 
         * Toggles the RTS (Request To Send) signal setting on right-click.
         * Updates the menu item color to indicate status and applies the setting
         * to the serial port if open. Saves the updated settings.
         *---------------------------------------------------------*/
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

        /*----------------------------------------------------------
         * MenuStrip - M1
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] M1ToolStripMenuItem_MouseDown
         * [✓] M1SetToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * M1ToolStripMenuItem_MouseDown
         * 
         * Handles right-click events on the M1 macro menu item,
         * toggling the macro timer based on user input interval.
         *---------------------------------------------------------*/
        private void M1ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e) => HandleMacroMouseDown(0, M1ToolStripMenuItem, M1ToolStripTextBox, e);

        /*----------------------------------------------------------
         * M1SetToolStripMenuItem_Click
         * 
         * Sets the M1 macro configuration when the set menu item
         * is clicked by delegating to the SetMMacro helper method.
         *---------------------------------------------------------*/
        private void M1SetToolStripMenuItem_Click(object sender, EventArgs e) => SetMMacro(0, M1ToolStripMenuItem);

        /*----------------------------------------------------------
         * MenuStrip - M2
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] M2ToolStripMenuItem_MouseDown
         * [✓] M2SetToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * M2ToolStripMenuItem_MouseDown
         * 
         * Handles right-click events on the M2 macro menu item,
         * toggling the macro timer based on user input interval.
         *---------------------------------------------------------*/
        private void M2ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e) => HandleMacroMouseDown(1, M2ToolStripMenuItem, M2ToolStripTextBox, e);

        /*----------------------------------------------------------
         * M2SetToolStripMenuItem_Click
         * 
         * Sets the M2 macro configuration when the set menu item
         * is clicked by delegating to the SetMMacro helper method.
         *---------------------------------------------------------*/
        private void M2SetToolStripMenuItem_Click(object sender, EventArgs e) => SetMMacro(1, M2ToolStripMenuItem);

        /*----------------------------------------------------------
         * MenuStrip - M3
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] M3ToolStripMenuItem_MouseDown
         * [✓] M3SetToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * M3ToolStripMenuItem_MouseDown
         * 
         * Handles right-click events on the M3 macro menu item,
         * toggling the macro timer based on user input interval.
         *---------------------------------------------------------*/
        private void M3ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e) => HandleMacroMouseDown(2, M3ToolStripMenuItem, M3ToolStripTextBox, e);

        /*----------------------------------------------------------
         * M3SetToolStripMenuItem_Click
         * 
         * Sets the M3 macro configuration when the set menu item
         * is clicked by delegating to the SetMMacro helper method.
         *---------------------------------------------------------*/
        private void M3SetToolStripMenuItem_Click(object sender, EventArgs e) => SetMMacro(2, M3ToolStripMenuItem);


        /*----------------------------------------------------------
         * MenuStrip - M4
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] M4ToolStripMenuItem_MouseDown
         * [✓] M4SetToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * M4ToolStripMenuItem_MouseDown
         * 
         * Handles right-click events on the M4 macro menu item,
         * toggling the macro timer based on user input interval.
         *---------------------------------------------------------*/
        private void M4ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e) => HandleMacroMouseDown(3, M4ToolStripMenuItem, M4ToolStripTextBox, e);

        /*----------------------------------------------------------
         * M4SetToolStripMenuItem_Click
         * 
         * Sets the M4 macro configuration when the set menu item
         * is clicked by delegating to the SetMMacro helper method.
         *---------------------------------------------------------*/
        private void M4SetToolStripMenuItem_Click(object sender, EventArgs e) => SetMMacro(3, M4ToolStripMenuItem);


        /*----------------------------------------------------------
         * MenuStrip - M5
         *---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] M5ToolStripMenuItem_MouseDown
         * [✓] M5SetToolStripMenuItem_Click
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * M5ToolStripMenuItem_MouseDown
         * 
         * Handles right-click events on the M5 macro menu item,
         * toggling the macro timer based on user input interval.
         *---------------------------------------------------------*/
        private void M5ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e) => HandleMacroMouseDown(4, M5ToolStripMenuItem, M5ToolStripTextBox, e);

        /*----------------------------------------------------------
         * M5SetToolStripMenuItem_Click
         * 
         * Sets the M5 macro configuration when the set menu item
         * is clicked by delegating to the SetMMacro helper method.
         *---------------------------------------------------------*/
        private void M5SetToolStripMenuItem_Click(object sender, EventArgs e) => SetMMacro(4, M5ToolStripMenuItem);

        /*----------------------------------------------------------
         * Helper Functions
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         * 
         * [✓] SetMMacro
         * [✓] HandleMacroMouseDown
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * SetMMacro
         * 
         * Activates the macro selection mode for the specified
         * macro index if no other macros are currently flagged.
         * Changes the menu item's background color to yellow
         * and displays an informational message.
         *---------------------------------------------------------*/
        private void SetMMacro(int index, ToolStripMenuItem menuItem)
        {
            if (MFlag.Where((flag, i) => i != index).All(flag => !flag))
            {
                menuItem.BackColor = Color.Yellow;
                MFlag[index] = true;
                InfoMessage("Please Select macro\n");
            }
        }

        /*----------------------------------------------------------
         * HandleMacroMouseDown
         * 
         * Handles right-click events on macro menu items.
         * - Checks if the COM port is open and macro is valid.
         * - Parses interval input and toggles the corresponding
         *   timer's enabled state and interval.
         * - Updates the menu item's background color accordingly.
         * - Displays error messages for invalid inputs or port state.
         *---------------------------------------------------------*/
        private void HandleMacroMouseDown(int index, ToolStripMenuItem menuItem, ToolStripTextBox textBox, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (!Port.IsOpen || CurrentMRepeat[index] == -1)
            {
                RichTextBox.AppendText("COM port is not open.\n");
                return;
            }

            if (!MTimer[index].Enabled)
            {
                if (int.TryParse(textBox.Text, out int interval) && interval > 0)
                {
                    MTimer[index].Interval = interval;
                    MTimer[index].Enabled = true;
                    menuItem.BackColor = Color.Green;
                }
                else
                {
                    RichTextBox.SelectionColor = Color.IndianRed;
                    RichTextBox.AppendText("Please enter a valid positive number.\n");
                }
            }
            else
            {
                MTimer[index].Enabled = false;
                menuItem.BackColor = SystemColors.Control;
            }
        }
    }
}
