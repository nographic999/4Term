using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Xml;

namespace _4Term
{
    public class Xml
    {
        /*----------------------------------------------------------
         * Class variables
         * -------------------------------------------------------*/

        /* Path to the XML configuration file for quick method. */
        public static string FileNameq = "config.xml";

        /* Loaded XML document instance for quick method. */
        public static System.Xml.XmlDocument XmlFile = null;

        /* Root element of the loaded XML document for quick method. */
        public static System.Xml.XmlElement RootElement = null;

        /*----------------------------------------------------------
         * Form
         * ---------------------------------------------------------
         * DEVELOPMENT STATUS
         * 
         * ---------------------------------------------------------
         * Standard method
         * ---------------------------------------------------------
         * 
         * [✓] ReadBoolParameter
         * [✓] ReadIntParameter
         * [✓] ReadStringParameter
         * [✓] WriteBoolParameter
         * [✓] WriteIntParameter
         * [✓] WriteStringParameter
         * ---------------------------------------------------------
         * Quick method (file loaded into memory)
         * ---------------------------------------------------------
         * 
         * [✓] CreateNewQ
         * [✓] LoadFileQ
         * [✓] ReadBoolParmQ
         * [✓] ReadIntParmQ
         * [✓] ReadStringParmQ
         * [✓] WriteBoolParmQ
         * [✓] WriteIntParmQ
         * [✓] WriteStringParmQ
         * [✓] CloseFileQ
         *---------------------------------------------------------*/

        /*----------------------------------------------------------
         * ReadBoolParameter
         * 
         * Reads a bool from XML file at given XPath.
         * Returns false if not found or invalid.
         *---------------------------------------------------------*/
        public static bool ReadBoolParameter(string filename, string xpath)
        {
            /* Chech if file exists */
            if (!System.IO.File.Exists(filename))
                return false;

            /* Load Xml file */
            var XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(filename);

            /* Get the root element of the XML document */
            var root = XmlFile.DocumentElement;

            /* Find the boolean node */
            var BoolNode = root.SelectSingleNode(xpath);
            if (BoolNode != null && bool.TryParse(BoolNode.InnerText, out bool result))
            {
                return result;
            }

            /* Return false if node not found or value is not a valid bool */
            return false;
        }

        /*----------------------------------------------------------
         * ReadIntParameter
         * 
         * Reads an int from XML file at given XPath.
         * Returns -1 if not found or invalid.
         *---------------------------------------------------------*/
        public static int ReadIntParameter(string filename, string xpath)
        {
            /* Chech if file exists */
            if (!System.IO.File.Exists(filename))
                return -1;

            /* Load Xml file */
            var XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(filename);

            /* Get the root element of the XML document */
            var root = XmlFile.DocumentElement;

            var IntNode = root.SelectSingleNode(xpath);
            if (IntNode != null && int.TryParse(IntNode.InnerText, out int result))
            {
                return result;
            }

            /* Return -1 if node not found or value is not a valid int */
            return -1;
        }


        /*----------------------------------------------------------
         * ReadStringParameter
         * 
         * Reads a string from XML file at given XPath.
         * Returns null if not found.
         *---------------------------------------------------------*/
        public static string ReadStringParameter(string filename, string xpath)
        {
            /* Check if file exists */
            if (!System.IO.File.Exists(filename))
                return null;

            /* Load XML file */
            var XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(filename);

            /* Get the root element of the XML document */
            var root = XmlFile.DocumentElement;

            /* Select the node based on XPath */
            var StringNode = root.SelectSingleNode(xpath);

            /* Return the node's inner text if node exists, else null */
            if (StringNode != null)
            {
                return StringNode.InnerText;
            }

            /* Return null if node not found */
            return null;
        }

        /*----------------------------------------------------------
         * WriteBoolParameter
         * 
         * Writes a bool to XML file node at given XPath.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool WriteBoolParameter(string filename, string xpath, bool value)
        {
            /* Check if file exists */
            if (!System.IO.File.Exists(filename))
                return false;

            /* Load XML file */
            var XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(filename);

            /* Get the root element of the XML document */
            var root = XmlFile.DocumentElement;

            /* Find the boolean node */
            var BoolNode = root.SelectSingleNode(xpath);

            if (BoolNode != null)
            {
                /* Update the node's inner text with the boolean value as a lowercase string */
                BoolNode.InnerText = value.ToString().ToLower();

                /* Save the changes back to the file */
                XmlFile.Save(filename);

                /* Return true to indicate success */
                return true;
            }

            /* Return false if the specified node was not found */
            return true;
        }


        /*----------------------------------------------------------
         * WriteIntParameter
         * 
         * Writes an int to XML file node at given XPath.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool WriteIntParameter(string filename, string xpath, int value)
        {
            /* Check if file exists */
            if (!System.IO.File.Exists(filename))
                return false;

            /* Load XML file */
            var XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(filename);

            /* Get the root element of the XML document */
            var root = XmlFile.DocumentElement;

            /* Find the integer node */
            var IntNode = root.SelectSingleNode(xpath);

            if (IntNode != null)
            {
                /* Update the node's inner text with the integer value */
                IntNode.InnerText = value.ToString();

                /* Save the changes back to the file */
                XmlFile.Save(filename);

                /* Return true to indicate success */
                return true;
            }

            /* Return false if the specified node was not found */
            return false;
        }

        /*----------------------------------------------------------
         * WriteStringParameter
         * 
         * Writes a string to XML file node at given XPath.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool WriteStringParameter(string filename, string xpath, string value)
        {
            /* Check if file exists */
            if (!System.IO.File.Exists(filename))
                return false;

            /* Load XML file */
            var XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(filename);

            /* Get the root element of the XML document */
            var root = XmlFile.DocumentElement;

            /* Find the string node */
            var StringNode = root.SelectSingleNode(xpath);

            if (StringNode != null)
            {
                /* Update the node's inner text with the string value */
                StringNode.InnerText = value;

                /* Save the changes back to the file */
                XmlFile.Save(filename);

                /* Return true to indicate success */
                return true;
            }

            /* Return false if the specified node was not found */
            return false;
        }

        /*----------------------------------------------------------
         * CreateNewQ
         *
         * Prompts the user to create a new XML configuration file
         * if it doesn't exist. Returns true if the file was created.
         *---------------------------------------------------------*/
        public static bool CreateNewQ()
        {
            /* Check if file exists */
            if (System.IO.File.Exists(FileNameq))
                return true;

            /* Show message box */
            DialogResult result = MessageBox.Show(
                "The configuration file does not exist. Would you like to create a new configuration file?",
                "Create New Configuration",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    XmlFile = new System.Xml.XmlDocument();

                    /* Create XML declaration explicitly */
                    System.Xml.XmlDeclaration xmlDeclaration = XmlFile.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlFile.AppendChild(xmlDeclaration);

                    /* Create <Settings> root element */
                    System.Xml.XmlElement root = XmlFile.CreateElement("Settings");
                    XmlFile.AppendChild(root);

                    /* Save the XML document */
                    XmlFile.Save(FileNameq);

                    RootElement = root;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /*----------------------------------------------------------
         * LoadFileQ
         * 
         * Loads XML file into memory.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool LoadFileQ()
        {
            /* Check if file exists */
            if (!System.IO.File.Exists(FileNameq))
                return false;

            /* Load XML file into the class-level XmlFile */
            XmlFile = new System.Xml.XmlDocument();
            XmlFile.Load(FileNameq);

            /* Optionally, you can verify the root element exists */
            RootElement = XmlFile.DocumentElement;
            if (RootElement == null)
                return false;

            /* Return true if load succeeded and root element is found */
            return true;
        }

        /*----------------------------------------------------------
         * ReadBoolParmQ
         * 
         * Reads a bool from loaded XML at XPath.
         * Returns false if not found or invalid.
         *---------------------------------------------------------*/
        public static bool ReadBoolParmQ(string xpath)
        {
            /* Find the boolean node */
            var BoolNode = RootElement.SelectSingleNode(xpath);
            if (BoolNode != null && bool.TryParse(BoolNode.InnerText, out bool result))
            {
                return result;
            }

            /* Return false if node not found or value is not a valid bool */
            return false;
        }

        /*----------------------------------------------------------
         * ReadIntParmQ
         * 
         * Reads an int from loaded XML at XPath.
         * Returns -1 if not found or invalid.
         *---------------------------------------------------------*/
        public static int ReadIntParmQ(string xpath)
        {
            /* Find the integer node */
            var IntNode = RootElement.SelectSingleNode(xpath);
            if (IntNode != null && int.TryParse(IntNode.InnerText, out int result))
            {
                return result;
            }

            /* Return -1 if node not found or value is not a valid int */
            return -1;
        }

        /*----------------------------------------------------------
         * ReadStringParmQ
         * 
         * Reads a string from loaded XML at XPath.
         * Returns null if not found.
         *---------------------------------------------------------*/
        public static string ReadStringParmQ(string xpath)
        {
            /* Find the string node */
            var StringNode = RootElement.SelectSingleNode(xpath);
            if (StringNode != null)
            {
                return StringNode.InnerText;
            }

            /* Return null if node not found */
            return null;
        }

        /*----------------------------------------------------------
         * WriteBoolParmQ
         * 
         * Writes a bool to loaded XML node at XPath.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool WriteBoolParmQ(string xpath, bool value)
        {
            /* Split the XPath into individual node names */
            string[] parts = xpath.Split('/');

            /* Start from the root element */
            System.Xml.XmlNode current = RootElement;

            /* Traverse or create each node along the XPath */
            foreach (string part in parts)
            {
                var next = current.SelectSingleNode(part);

                /* If node doesn't exist, create it */
                if (next == null)
                {
                    next = XmlFile.CreateElement(part);
                    current.AppendChild(next);
                }

                /* Move to the next node */
                current = next;
            }

            /* Set the node's inner text to the bool value */
            current.InnerText = value.ToString().ToLower();

            /* Indicate success */
            return true;
        }


        /*----------------------------------------------------------
         * WriteIntParmQ
         * 
         * Writes an int to loaded XML node at XPath.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool WriteIntParmQ(string xpath, int value)
        {
            /* Split the XPath into individual node names */
            string[] parts = xpath.Split('/');

            /* Start from the root element */
            System.Xml.XmlNode current = RootElement;

            /* Traverse or create each node along the XPath */
            foreach (string part in parts)
            {
                var next = current.SelectSingleNode(part);

                /* If node doesn't exist, create it */
                if (next == null)
                {
                    next = XmlFile.CreateElement(part);
                    current.AppendChild(next);
                }

                /* Move to the next node */
                current = next;
            }

            /* Set the node's inner text to the intvalue */
            current.InnerText = value.ToString();

            /* Indicate success */
            return true;
        }


        /*----------------------------------------------------------
         * WriteStringParmQ
         * 
         * Writes a string to loaded XML node at XPath.
         * Returns true if successful.
         *---------------------------------------------------------*/
        public static bool WriteStringParmQ(string xpath, string value)
        {
            /* Split the XPath into individual node names */
            string[] parts = xpath.Split('/');

            /* Start from the root element */
            System.Xml.XmlNode current = RootElement;

            /* Traverse or create each node along the XPath */
            foreach (string part in parts)
            {
                var next = current.SelectSingleNode(part);

                /* If node doesn't exist, create it */
                if (next == null)
                {
                    next = XmlFile.CreateElement(part);
                    current.AppendChild(next);
                }

                /* Move to the next node */
                current = next;
            }

            /* Set the node's inner text to the string value */
            current.InnerText = value;

            /* Indicate success */
            return true;
        }

        /*----------------------------------------------------------
         * CloseFileQ
         * 
         * Saves and clears loaded XML from memory.
         *---------------------------------------------------------*/
        public static void CloseFileQ()
        {
            /* Clear the cached XmlDocument to release references */
            XmlFile.Save(FileNameq);
            XmlFile = null;
            RootElement = null;
        }
    }
}
