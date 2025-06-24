using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace _4Term
{
    partial class About_Form : Form
    {
        /*----------------------------------------------------------
         * Assembly Attribute Accessors
         * Provides properties to access assembly metadata such as
         * title, version, description, product, copyright, and company.
         * -------------------------------------------------------*/
        #region Assembly Attribute Accessors
        /* Generic helper to fetch a specific assembly attribute. */
        private static T GetAssemblyAttribute<T>() where T : Attribute
        {
            return (T)Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(T), false)
                .FirstOrDefault();
        }
        /* Gets the assembly title from AssemblyTitleAttribute. */
        public string AssemblyTitle
        {
            get
            {
                var attr = GetAssemblyAttribute<AssemblyTitleAttribute>();
                if (attr != null && !string.IsNullOrEmpty(attr.Title))
                    return attr.Title;

                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        /* Gets the assembly version as a string. */
        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        /* Gets the assembly description from AssemblyDescriptionAttribute. */
        public string AssemblyDescription => GetAssemblyAttribute<AssemblyDescriptionAttribute>()?.Description ?? "";
        /* Gets the assembly product name from AssemblyProductAttribute. */
        public string AssemblyProduct => GetAssemblyAttribute<AssemblyProductAttribute>()?.Product ?? "";
        /* Gets the assembly copyright from AssemblyCopyrightAttribute. */
        public string AssemblyCopyright => GetAssemblyAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? "";
        /* Gets the assembly company name from AssemblyCompanyAttribute. */
        public string AssemblyCompany => GetAssemblyAttribute<AssemblyCompanyAttribute>()?.Company ?? "";
        #endregion

        /*----------------------------------------------------------
        * Form
        * ---------------------------------------------------------
        * DEVELOPMENT STATUS
        *
        * [✓] About_Form(constructor)
        * [✓] CloseButton_Click
        *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * About_Form
         * 
         * Main application form constructor.
         * Initializes UI components and sets up core event handlers.
         *---------------------------------------------------------*/
        public About_Form()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();

            string license = assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                .FirstOrDefault(attr => attr.Key == "License")?.Value ?? "License info not found";

            this.Text = $"About {AssemblyTitle}";
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = $"Version {AssemblyVersion}";
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            this.labelMetadata.Text = license;
        }

        /*----------------------------------------------------------
         * CloseButton_Click
         * 
         * Closes the About form when the Close button is clicked
         *---------------------------------------------------------*/
        private void CloseButton_Click(object sender, EventArgs e) => Close();

        /*----------------------------------------------------------
         * Helper Functions
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         * 
         * [✓] WndProc
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
        * WndProc
        * 
        * Blocks right-clicks on title bar/borders.
        *---------------------------------------------------------*/
        protected override void WndProc(ref Message buffer)
        {
            if (buffer.Msg != 0xA3)
                base.WndProc(ref buffer);
        }
    }
}
