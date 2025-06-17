using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace _4Term
{
    public partial class Main_Form : Form
    {
        /*----------------------------------------------------------
         * Class variables
         * -------------------------------------------------------*/

        /* Delegate for thread-safe operations that involve passing a string */
        internal delegate void StringDelegate(string data);
        /* Singleton instance of the Serial communication handler */
        private readonly Serial Port = Serial.Instance;
        /* Timer used for periodically attempting automatic connection */
        private readonly Timer AutoConnectTimer = new Timer();
        /* Timer used for periodically send macros */
        //private readonly Timer MacroRepeatTimer = new Timer();
        private readonly Timer M1Timer = new Timer();
        private readonly Timer M2Timer = new Timer();
        private readonly Timer M3Timer = new Timer();
        private readonly Timer M4Timer = new Timer();
        private readonly Timer M5Timer = new Timer();
        /* Tracks the current page group index (0-31) where each group contains 8 pages */
        private int PageIndex = 0;
        /* Tracks the current macro bank offset (0-511) where each bank contains 128 macros */
        private int MacroBank = 0;
        /* Stores the index of the currently selected radio button */
        private int CurrentRadioValue = 0;
        /* Stores the index of the currently selected macro button */
        private int CurrentButtonValue = 0;

        //private int CurrentMacroRepeat = 0;


        private int CurrentM1Repeat = -1;
        private int CurrentM2Repeat = -1;
        private int CurrentM3Repeat = -1;
        private int CurrentM4Repeat = -1;
        private int CurrentM5Repeat = -1;
        private bool M1Flag = false;
        private bool M2Flag = false;
        private bool M3Flag = false;
        private bool M4Flag = false;
        private bool M5Flag = false;
        private static bool M1Flicker = false;
        private static bool M2Flicker = false;
        private static bool M3Flicker = false;
        private static bool M4Flicker = false;
        private static bool M5Flicker = false;

        /* Flag indicating whether the application is currently in macro editing mode */
        private bool MacroEditingFlag = false;
        /* Path of the currently opened file */
        private string FilePath = null;
        /* Radio Button to Macro Offset Mapping */
        private readonly Dictionary<string, int> radioButtonMapping = new Dictionary<string, int>
        {
            { "RadioButton0", 0 },
            { "RadioButton1", 16 },
            { "RadioButton2", 32 },
            { "RadioButton3", 48 },
            { "RadioButton4", 64 },
            { "RadioButton5", 80 },
            { "RadioButton6", 96 },
            { "RadioButton7", 112 }

        };

        /*----------------------------------------------------------
         * Form
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] Main_Form(constructor)
         * [✓] Main_Form_Load
         * [✓] Main_Form_Shown
         * [✓] Main_Form_FormClosing
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * Main_Form
         * 
         * Main application form constructor.
         * Initializes UI components and sets up core event handlers.
         *---------------------------------------------------------*/
        public Main_Form()
        {
            /* Initialize all UI components defined in the designer */
            InitializeComponent();

            /* Set up event subscriptions (lightweight) */
            Port.StatusChanged += ChangeStatus;
            Port.DataReceived += DataReceive;
        }

        /*----------------------------------------------------------
         * Main_Form_Load
         * 
         * Runs when form is initialized but not yet visible
         * Use for: Settings, data loading
         *---------------------------------------------------------*/
        private void Main_Form_Load(object sender, EventArgs e)
        {
            /* Load settings and configure UI */
            Settings.ReadXml();

            /* Set Form size*/
            Size = new Size(Settings.Form.Width, Settings.Form.Height);

            /* Initialize font settings */
            string buffer = "";
            RichTextBox.Font = cFont.ParseFontString(
                Settings.RichTextBox.Font + "-" +
                Settings.RichTextBox.Size + "-" +
                Settings.RichTextBox.FontStyle,
                Settings.RichTextBox.Underline,
                Settings.RichTextBox.Strikeout,
                ref buffer);

            /* Set UI control states */
            WordWrapToolStripMenuItem.Checked = Settings.RichTextBox.WordWrap;
            ToggleScrollingToolStripMenuItem.Checked = Settings.RichTextBox.Scroll;
            DtrToolStripMenuItem.BackColor = Settings.Port.Dtr ? Color.Green : SystemColors.Control;
            RtsToolStripMenuItem.BackColor = Settings.Port.Rts ? Color.Green : SystemColors.Control;
            SetAdvancedMode(Settings.RichTextBox.AdvancedMode);

            /* Initialize UI components */
            AssignButtonClickHandlers();
            LoadGroupContent();
        }

        /*----------------------------------------------------------
         * Main_Form_Shown
         * 
         * Runs when form is completely visible to user
         * Use for: focus, animations, background tasks
         *---------------------------------------------------------*/
        private void Main_Form_Shown(object sender, EventArgs e)
        {
            /* Start background operations */
            AutoConnectTimer.Interval = 100;
            AutoConnectTimer.Tick += ReconnectTick;

            M1Timer.Interval = 500;
            M1Timer.Tick += M1Tick;
            M2Timer.Interval = 500;
            M2Timer.Tick += M2Tick;
            M3Timer.Interval = 500;
            M3Timer.Tick += M3Tick;
            M4Timer.Interval = 500;
            M4Timer.Tick += M4Tick;
            M5Timer.Interval = 500;
            M5Timer.Tick += M5Tick;

            /* UI-dependent initialization */
            WelcomeMsg();            
        }

        /*----------------------------------------------------------
         * Main_Form_FormClosing
         * 
         * Handles cleanup tasks when the form is closing.
         * Ensures the serial port is closed and performs base cleanup.
         *---------------------------------------------------------*/
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Port.IsOpen)
                Port.Close();
            base.OnClosed(e);
        }

        /*----------------------------------------------------------
         * Buttons
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] CloseButton_Click
         * [✓] AlienButton_Click
         * [~] SendButton_Click - needs optimization
         * [✓] ClearButton_Click
         * [✓] OptionsButton_Click
         * [✓] AutoConnectButton_Click
         * [✓] ConnectButton_Click
         * [✓] SwapButton_MouseDown
         * [✓] RadioButton_Click
         * [~] MacroButton_Click - needs optimization
         *---------------------------------------------------------*/

        private void CloseButton_Click(object sender, EventArgs e)
        {
            MacroEditingFlag = !MacroEditingFlag;
            SetMacroSetMode(MacroEditingFlag);
        }

        /*----------------------------------------------------------
         * ProgButton_Click
         * 
         * Handles macro button editing confirmation
         * Saves macro settings when text fields are valid
         *---------------------------------------------------------*/
        private void AlienButton_Click(object sender, EventArgs e)
        {
            /* Validate that the tab text input box */
            if (string.IsNullOrWhiteSpace(PageBox.Text))
            {
                ErrorMessage("Tab cannot be empty\n");
                return;
            }            

            /* Validate that the button text input box */
            if (string.IsNullOrWhiteSpace(MacroTextBox.Text))
            {
                ErrorMessage("Text cannot be empty\n");
                return;
            }

            /* Validate that the command input box */
            if (string.IsNullOrWhiteSpace(MacroBox.Text))
            {
                ErrorMessage("Macro cannot be empty\n");
                return;
            }

            /* Store trimmed tab/page title in settings array */
            Settings.Page.Text[CurrentRadioValue / 16 + PageIndex] = PageBox.Text.Trim();
            InfoMessage($"Page{CurrentRadioValue / 16 + PageIndex} set to: " + PageBox.Text.Trim() + "\n");

            /* Store trimmed button display text in settings array */
            Settings.MacroText.Text[CurrentButtonValue + CurrentRadioValue + MacroBank] = MacroTextBox.Text.Trim();
            InfoMessage($"Text{CurrentButtonValue + CurrentRadioValue + MacroBank} set to: " + MacroTextBox.Text.Trim() + "\n");

            /* Store trimmed command text in settings array */
            Settings.MacroCommand.Text[CurrentButtonValue + CurrentRadioValue + MacroBank] = MacroBox.Text.Trim();
            InfoMessage($"Macro{CurrentButtonValue + CurrentRadioValue + MacroBank} set to: " + MacroBox.Text.Trim() + "\n");

            /* Save changes to configuration file */
            Settings.WriteXml();

            /* Refresh the UI with new button set */
            LoadGroupContent();
        }

        /*----------------------------------------------------------
         * SendButton_Click
         * 
         * Handles data sending via serial port
         * Shows data in RichTextBox
         *---------------------------------------------------------*/
        private void SendButton_Click(object sender, EventArgs e)
        {
            /* Check if port is open */
            if (Port.IsOpen)
            {
                /* Get the command from the ComboBox */
                string buffer = ComboBox.Text;

                /* Proceed only if the command is not empty */
                if (!string.IsNullOrEmpty(buffer))
                {
                    /* Send the command and get the appended line ending */
                    string lineEnding = Port.Send(buffer);

                    /* Add the command to ComboBox history if not already there */
                    if(!ComboBox.Items.Contains(buffer))
                    {
                        ComboBox.Items.Add(buffer);
                    }

                    /* If local echo is enabled, display the sent command */
                    if (Settings.RichTextBox.LocalEcho)
                    {
                        AddLine(buffer + lineEnding, 0);
                    }

                    /* Refocus the ComboBox for quick user input */
                    ComboBox.Focus();
                }
            }

            else
                /* Inform the user if the serial port is not open */
                InfoMessage("The serial port is not open. Please open a port before sending the macro.\n");       
        }

        /*----------------------------------------------------------
         * ClearButton_Click
         * 
         * Clears the RichTextBox content
         *---------------------------------------------------------*/
        private void ClearButton_Click(object sender, EventArgs e)
        {
            RichTextBox.Clear();
        }

        /*----------------------------------------------------------
         * OptionsButton_Click
         * 
         * Opens the Serial Port Options configuration dialog
         *---------------------------------------------------------*/
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            SerialPortOptionsForm OptionsForm = new SerialPortOptionsForm();
            OptionsForm.ShowDialog();
            DtrToolStripMenuItem.BackColor = Settings.Port.Dtr ? Color.Green : SystemColors.Control;
            RtsToolStripMenuItem.BackColor = Settings.Port.Rts ? Color.Green : SystemColors.Control;
        }

        /*----------------------------------------------------------
         * AutoConnectButton_Click
         * 
         * Toggles automatic connection timer on/off
        *---------------------------------------------------------*/
        private void AutoConnectButton_Click(object sender, EventArgs e)
        {
            AutoConnectTimer.Enabled = !AutoConnectTimer.Enabled;
            AutoConnectButton.ForeColor = AutoConnectTimer.Enabled ? Color.Red : Color.Black;
        }

        /*----------------------------------------------------------
         * ConnectButton_Click
         * 
         * Manages serial port connection state
        *---------------------------------------------------------*/
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (Port.IsOpen) Port.Close(); else Port.Open();
            ConnectButton.Text = Port.IsOpen ? "Disconnect" : "Connect";
            ConnectButton.ForeColor = Port.IsOpen ? Color.Red : Color.Green;
        }

        /*----------------------------------------------------------
         * SwapButton_MouseDown
         * 
         * Handles circular navigation through button/page groups
         *---------------------------------------------------------*/

        private void SwapButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                /* Left click: navigate forward */
                PageIndex = (PageIndex + 8) % 32;
                MacroBank = (MacroBank + 128) % 512;
            }
            else if (e.Button == MouseButtons.Left)
            {
                /* Right click: navigate backward */
                PageIndex = (PageIndex - 8 + 32) % 32;
                MacroBank = (MacroBank - 128 + 512) % 512;
            }

            LoadGroupContent();
        }

        /*----------------------------------------------------------
         * RadioButton_Click
         * 
         * Handles radio button selection for macro button navigation
         *---------------------------------------------------------*/

        private void RadioButton_Click(object sender, EventArgs e)
        {
            /* Safely cast sender to RadioButton and get mapped value */
            if (sender is RadioButton selectedRadioButton && radioButtonMapping.TryGetValue(selectedRadioButton.Name, out int radioValue))
            {
                CurrentRadioValue = radioValue;

                LoadGroupContent();
            }
        }

        /*----------------------------------------------------------
         * MacroButton_Click
         * 
         * Sends the selected macro command if not in edit mode.
         * If in edit mode, loads the macro into the editor UI.
         *---------------------------------------------------------*/
        public async void MacroButton_Click(int macroIndex)
        {
            /* If not in macro editing mode, proceed with sending the macro */
            if (!M1Flag && !M2Flag && !M3Flag && !M4Flag && !M5Flag && !MacroEditingFlag)
            {
                /* Check if port is open */
                if(Port.IsOpen)
                {
                    /* Retrieve the macro command based on current index and bank */
                    string command = Settings.MacroCommand.Text[macroIndex + CurrentRadioValue + MacroBank];

                    /* Send the command only if it's not empty */
                    if (!string.IsNullOrEmpty(command))
                    {
                        string lineEnding = Port.Send(command);

                        if (Settings.RichTextBox.LocalEcho)
                        {
                            AddLine(command + lineEnding, 0);
                        }

                        await Task.Delay(50);
                    }

                    else
                    {
                        InfoMessage("The selected macro is empty. Please configure a command before using this button.\n");
                    }
                } 
                
                else
                {
                    /* Inform the user if the serial port is not open */
                    InfoMessage("The serial port is not open. Please open a port before sending the macro.\n");
                }
            }
            if (M1Flag)
                SetMacro(ref M1Flag, M1SetToolStripMenuItem, M1ToolStripMenuItem, ref CurrentM1Repeat, macroIndex);
            else if (M2Flag)
                SetMacro(ref M2Flag, M2SetToolStripMenuItem, M2ToolStripMenuItem, ref CurrentM2Repeat, macroIndex);
            else if (M3Flag)
                SetMacro(ref M3Flag, M3SetToolStripMenuItem, M3ToolStripMenuItem, ref CurrentM3Repeat, macroIndex);
            else if (M4Flag)
                SetMacro(ref M4Flag, M4SetToolStripMenuItem, M4ToolStripMenuItem, ref CurrentM4Repeat, macroIndex);
            else if (M5Flag)
                SetMacro(ref M5Flag, M5SetToolStripMenuItem, M5ToolStripMenuItem, ref CurrentM5Repeat, macroIndex);
            else if (MacroEditingFlag)
            {
                /* In macro editing mode: load macro details into the UI for editing */
                MacroBox.Text = Settings.MacroCommand.Text[macroIndex + CurrentRadioValue + MacroBank];
                MacroTextBox.Text = Settings.MacroText.Text[macroIndex + CurrentRadioValue + MacroBank];
                PageBox.Text = Settings.Page.Text[CurrentRadioValue / 16 + PageIndex];
                CurrentButtonValue = macroIndex;
            }
        }

        private void SetMacro(ref bool flag, ToolStripMenuItem setItem, ToolStripMenuItem mainItem, ref int currentRepeat, int macroIndex)
        {
            int index = macroIndex + CurrentRadioValue + MacroBank;
            setItem.Text = Settings.MacroText.Text[index];
            currentRepeat = index;
            flag = false;
            mainItem.BackColor = SystemColors.Control;
            InfoMessage($"Macro{setItem.Name[1]} set to: {Settings.MacroText.Text[index]}\n");
        }

        /*----------------------------------------------------------
         * ComboBox
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] ComboBox_Enter - OK
         * [✓] ComboBox_KeyDown - OK
         * [✓] ComboBox_Leave - OK
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ComboBox_Enter
         * 
         * Disables clipboard context menu items when the ComboBox
         * gains focus, preventing operations like copy/paste while
         * entering commands.
         *---------------------------------------------------------*/
        private void ComboBox_Enter(object sender, EventArgs e)
        {
            UndoToolStripMenuItem.Enabled =
                PasteToolStripMenuItem.Enabled = 
                CopyToolStripMenuItem.Enabled = 
                CutToolStripMenuItem.Enabled = 
                DeleteToolStripMenuItem.Enabled =
                SelectAllToolStripMenuItem.Enabled = false;
        }


        /*----------------------------------------------------------
         * ComboBox_KeyDown
         * 
         * Handles Enter key within the ComboBox. Sends the current
         * command by calling SendButton_Click and prevents the beep
         * or focus shift by suppressing the key press.
         *---------------------------------------------------------*/
        private void ComboBox_KeyDown(object sender, KeyEventArgs keyboard)
        {
            if (keyboard.KeyCode == Keys.Enter)
            {
                SendButton_Click(sender, keyboard);
                keyboard.Handled = true;
                keyboard.SuppressKeyPress = true;
            }
        }

        /*----------------------------------------------------------
         * ComboBox_Leave
         * 
         * Re-enables clipboard context menu items when the ComboBox
         * loses focus, restoring standard edit options.
         *---------------------------------------------------------*/
        private void ComboBox_Leave(object sender, EventArgs e)
        {
            UndoToolStripMenuItem.Enabled =
                PasteToolStripMenuItem.Enabled =
                CopyToolStripMenuItem.Enabled =
                CutToolStripMenuItem.Enabled =
                DeleteToolStripMenuItem.Enabled =
                SelectAllToolStripMenuItem.Enabled = true;
        }

        /*----------------------------------------------------------
         * Events
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] ChangeStatus - OK
         * [✓] DataReceive - OK
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ChangeStatus
         * 
         * Thread-safe UI updater for connection status changes.
         *---------------------------------------------------------*/
        public void ChangeStatus(string buffer)
        {
            if (InvokeRequired)
            {
                Invoke(new StringDelegate(ChangeStatus), new object[] { buffer });
                return;
            }
            ConnectButton.Text = Port.IsOpen ? "Disconnect" : "Connect";
            ConnectButton.ForeColor = Port.IsOpen ? Color.Red : Color.Green;
            Text = buffer;
        }

        /*----------------------------------------------------------
         * DataReceive
         * 
         * Thread-safe method for processing incoming serial data.
         *---------------------------------------------------------*/
        public void DataReceive(string dataIn)
        {
            if (InvokeRequired)
            {
                Invoke(new StringDelegate(DataReceive), new object[] { dataIn });
                return;
            }
            AddLine(dataIn, 1);
        }

        /*----------------------------------------------------------
         * Timers
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         *
         * [✓] ReconnectTick
         * [✓] M1Repeat
         * [✓] M2Repeat
         * [✓] M3Repeat
         * [✓] M4Repeat
         * [✓] M5Repeat
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ReconnectTick
         * 
         * Timer-triggered port reconnection handler.
         *---------------------------------------------------------*/
        private void ReconnectTick(object sender, EventArgs e)
        {
            ConnectButton.Text = Port.IsOpen ? "Disconnect" : "Connect";
            ConnectButton.ForeColor = Port.IsOpen ? Color.Red : Color.Green;
            if (!Port.IsOpen) Port.Open();
        }

        /*----------------------------------------------------------
         * M1Tick
         * 
         * Timer tick handler for M1 macro repeat.
         *---------------------------------------------------------*/
        private void M1Tick(object sender, EventArgs e)
        {
            MacroTick(CurrentM1Repeat, M1ToolStripMenuItem, ref M1Flicker);
        }

        /*----------------------------------------------------------
         * M2Tick
         * 
         * Timer tick handler for M2 macro repeat.
         *---------------------------------------------------------*/
        private void M2Tick(object sender, EventArgs e)
        {
            MacroTick(CurrentM2Repeat, M2ToolStripMenuItem, ref M2Flicker);
        }
        /*----------------------------------------------------------
         * M3Tick
         * 
         * Timer tick handler for M3 macro repeat.
         *---------------------------------------------------------*/
        private void M3Tick(object sender, EventArgs e)
        {
            MacroTick(CurrentM3Repeat, M3ToolStripMenuItem, ref M3Flicker);
        }

        /*----------------------------------------------------------
         * M4Tick
         * 
         * Timer tick handler for M4 macro repeat.
         *---------------------------------------------------------*/
        private void M4Tick(object sender, EventArgs e)
        {
            MacroTick(CurrentM4Repeat, M4ToolStripMenuItem, ref M4Flicker);
        }

        /*----------------------------------------------------------
         * M5Tick
         * 
         * Timer tick handler for M5 macro repeat.
         *---------------------------------------------------------*/
        private void M5Tick(object sender, EventArgs e)
        {
            MacroTick(CurrentM5Repeat, M5ToolStripMenuItem, ref M5Flicker);
        }

        /*----------------------------------------------------------
         * Helper Functions
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         * 
         * [✓] AssignButtonClickHandlers
         * [✓] LoadGroupContent
         * [✓] AddLine
         * [✓] WelcomeMsg
         * [✓] SetAdvancedMode
         * [✓] SetMacroSetMode
         * [~] MacroTick  - needs optimization
         * [✓] ErrorMessage
         * [✓] InfoMessage
         * [✓] MacroButton_MouseDown
         * [✓] WndProc
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
        * AssignButtonClickHandlers
        * 
        * Attaches click event handlers to MacroButton controls (0-15).
        *---------------------------------------------------------*/
        private void AssignButtonClickHandlers()
        {
            for (int i = 0; i <= 15; i++)
            {
                /* Find the button in the form */
                Button macroButton = (Button)this.Controls.Find($"MacroButton{i}", true).FirstOrDefault();
                if (macroButton != null)
                {
                    /* Store current button number to avoid closure issue */
                    int buffer = i;

                    /* Set click handler for this button */
                    macroButton.Click += (sender, e) => MacroButton_Click(buffer);
                }
            }
        }

        /*----------------------------------------------------------
        * LoadGroupContent
        * 
        * Loads text labels for RadioButtons (0-7) and MacroButtons (0-15)
        *---------------------------------------------------------*/
        private void LoadGroupContent()
        {
            /* Update RadioButton texts (0-7) */
            for (int i = 0; i <= 7; i++)
            {
                /* Find RadioButton control by name (searches child controls recursively) */
                RadioButton radioButton = (RadioButton)this.Controls.Find($"RadioButton{i}", true).FirstOrDefault();

                /* Apply text from settings if the control exists */
                if (radioButton != null)
                    radioButton.Text = Settings.Page.Text[i + PageIndex];
            }

            /* Update MacroButton texts (0-15) */
            for (int i = 0; i <= 15; i++)
            {
                /* Find MacroButton control by name (searches child controls recursively) */
                Button macroButton = (Button)this.Controls.Find($"MacroButton{i}", true).FirstOrDefault();

                /* Apply text from settings with MacroBank and CurrentRadioValue offsets */
                if (macroButton != null)
                    macroButton.Text = Settings.MacroText.Text[i + MacroBank + CurrentRadioValue];
            }
        }

        /*----------------------------------------------------------
        * AddLine
        * 
        * Adds a line of text to the RichTextBox with configurable formatting.
        * *---------------------------------------------------------*/
        private void AddLine(string line, int type)
        {
            /* Set the selection color based on message type */
            if (type == 0)
                /* Transmit color */
                RichTextBox.SelectionColor = Color.FromArgb(Settings.Color.Transmit.R, Settings.Color.Transmit.G, Settings.Color.Transmit.B);
            else
                /* Receive color */
                RichTextBox.SelectionColor = Color.FromArgb(Settings.Color.Receive.R, Settings.Color.Receive.G, Settings.Color.Receive.B);

            string buffer = "";

            /* Check if HexOutput is enabled */
            if (Settings.RichTextBox.HexOutput)
            {
                /* Convert the line to bytes and format as a hex string */
                byte[] lineBytes = Encoding.UTF8.GetBytes(line + buffer);
                string lineAsString = BitConverter.ToString(lineBytes).Replace("-", " ");

                /* Append the hex-formatted line to the RichTextBox */
                RichTextBox.AppendText($" [{lineAsString}]");
            }

            else
                /* Append the plain text line to the RichTextBox */
                RichTextBox.AppendText(line + buffer);

            /* Scroll to the latest line if auto-scroll is enabled */
            if (Settings.RichTextBox.Scroll)
            {
                RichTextBox.SelectionStart = RichTextBox.Text.Length;
                RichTextBox.ScrollToCaret();
            }
        }

        /*----------------------------------------------------------
        * WelcomeMsg
        * 
        * Displays an animated ASCII art welcome message in a RichTextBox.
        * *---------------------------------------------------------*/
        private async void WelcomeMsg()
        {
            RichTextBox.BackColor = Color.Black;
            string[] asciiLines = new string[]
            {
                "",
                "",
                "",
                "",
                "",
                "                                   Welcome to 4Term Terminal!",
                "  ____________/\\\\\\_____/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\__________________________________________________",
                "   __________/\\\\\\\\\\____\\///////\\\\\\/////___________________________________________________",
                "    ________/\\\\\\/\\\\\\__________\\/\\\\\\________________________________________________________",
                "     ______/\\\\\\/\\/\\\\\\__________\\/\\\\\\___________/\\\\\\\\\\\\\\\\___/\\\\/\\\\\\\\\\\\\\_____/\\\\\\\\\\__/\\\\\\\\\\___",
                "      ____/\\\\\\/__\\/\\\\\\__________\\/\\\\\\_________/\\\\\\/////\\\\\\_\\/\\\\\\/////\\\\\\__/\\\\\\///\\\\\\\\\\///\\\\\\_",
                "       __/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\_______\\/\\\\\\________/\\\\\\\\\\\\\\\\\\\\\\__\\/\\\\\\___\\///__\\/\\\\\\_\\//\\\\\\__\\/\\\\\\_",
                "        _\\///////////\\\\\\//________\\/\\\\\\_______\\//\\\\///////___\\/\\\\\\_________\\/\\\\\\__\\/\\\\\\__\\/\\\\\\_",
                "         ___________\\/\\\\\\__________\\/\\\\\\________\\//\\\\\\\\\\\\\\\\\\\\_\\/\\\\\\_________\\/\\\\\\__\\/\\\\\\__\\/\\\\\\_",
                "          ___________\\///___________\\///__________\\//////////__\\///__________\\///___\\///___\\///__",
                "                                                                           Version: 1.1.0-beta x64"
            };
            /* Colors to cycle through */
            Color[] colors = new Color[]
            {
                Color.Firebrick,       // deep red
                Color.DarkOrange,      // rich orange
                Color.Gold,            // warm yellow
                Color.MediumSeaGreen,  // muted green
                Color.SteelBlue,       // soft blue-gray
                Color.MediumSlateBlue, // cool purple-blue
                Color.Tomato,          // bright red-orange
                Color.MediumTurquoise, // vibrant teal
                Color.SandyBrown,      // light brown-orange
                Color.MediumVioletRed  // purple-pink
            };
            RichTextBox.Clear();
            for (int i = 0; i < asciiLines.Length; i++)
            {
                RichTextBox.SelectionColor = colors[i % colors.Length];
                RichTextBox.AppendText(asciiLines[i] + "\r\n");
                await Task.Delay(100);
            }
            await Task.Delay(250);
            RichTextBox.Clear();
            RichTextBox.ForeColor = Color.FromArgb(Settings.Color.Transmit.R, Settings.Color.Transmit.G, Settings.Color.Transmit.B);
            RichTextBox.BackColor = Color.FromArgb(Settings.Color.Back.R, Settings.Color.Back.G, Settings.Color.Back.B);
        }

        /*----------------------------------------------------------
         * SetAdvancedMode
         * 
         * Enables or disables advanced UI controls and flags.
         *---------------------------------------------------------*/
        private void SetAdvancedMode(bool enable)
        {
            Settings.RichTextBox.AdvancedMode = AdvancedModeToolStripMenuItem.Checked =
                DtrToolStripMenuItem.Visible = DtrToolStripMenuItem.Enabled =
                RtsToolStripMenuItem.Visible = RtsToolStripMenuItem.Enabled =
                M1ToolStripMenuItem.Visible = M1ToolStripMenuItem.Enabled =
                M2ToolStripMenuItem.Visible = M2ToolStripMenuItem.Enabled =
                M3ToolStripMenuItem.Visible = M3ToolStripMenuItem.Enabled =
                M4ToolStripMenuItem.Visible = M4ToolStripMenuItem.Enabled =
                M5ToolStripMenuItem.Visible = M5ToolStripMenuItem.Enabled = enable;
        }

        /*----------------------------------------------------------
         * SetMacroSetMode
         * 
         * Enables or disables macro UI controls and flags.
         *---------------------------------------------------------*/
        private void SetMacroSetMode(bool enable)
        {

            MacroSetPanel.Visible = MacroSetPanel.Enabled = MacroModeToolStripMenuItem.Checked = enable;
                
            UndoToolStripMenuItem.Enabled = CutToolStripMenuItem.Enabled =
                CopyToolStripMenuItem.Enabled = PasteToolStripMenuItem.Enabled =
                DeleteToolStripMenuItem.Enabled = SelectAllToolStripMenuItem.Enabled = !enable;
        }

        /*----------------------------------------------------------
         * MacroTick
         * 
         * Toggles flicker effect and sends macro command if port is open.
         *---------------------------------------------------------*/
        private void MacroTick(int current, ToolStripMenuItem item, ref bool flicker)
        {
            flicker = !flicker;
            item.BackColor = flicker ? Color.Green : SystemColors.Control;
            /* Check if port is open */
            if (Port.IsOpen)
            {
                /* Retrieve the macro command based on current index and bank */
                string command = Settings.MacroCommand.Text[current];

                /* Send the command only if it's not empty */
                if (!string.IsNullOrEmpty(command))
                {
                    string lineEnding = Port.Send(command);

                    if (Settings.RichTextBox.LocalEcho)
                    {
                        AddLine(command + lineEnding, 0);
                    }
                }
            }
        }

        /*----------------------------------------------------------
         * ErrorMessage
         * 
         * Displays an error message in the RichTextBox.
         *---------------------------------------------------------*/
        private void ErrorMessage(string message)
        {
            RichTextBox.SelectionColor = Color.FromArgb(Settings.Color.Transmit.R, Settings.Color.Transmit.G, Settings.Color.Transmit.B);
            RichTextBox.AppendText("[ Error ] " + message);
        }

        /*----------------------------------------------------------
         * InfoMessage
         * 
         * Displays an informational message in the RichTextBox.
         *---------------------------------------------------------*/
        private void InfoMessage(string message)
        {
            RichTextBox.SelectionColor = Color.FromArgb(Settings.Color.Transmit.R, Settings.Color.Transmit.G, Settings.Color.Transmit.B);
            RichTextBox.AppendText("[ Info ] " + message);
        }

        /*----------------------------------------------------------
         * MacroButton_MouseDown
         * 
         * Toggles macro editing mode on right-click.
         *---------------------------------------------------------*/
        private void MacroButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MacroEditingFlag = !MacroEditingFlag;
                SetMacroSetMode(MacroEditingFlag);
            }
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
