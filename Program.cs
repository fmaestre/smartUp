using System; 
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SmartUp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.setConfig(args);
            //Application.Run(new FrmImmulite());
            //Application.Run(new FrmXmlNaval());
            try
            {
                string interface_ = pos["interface"];
                string type_ = pos["type"];
                int frequence = 60000;
                try { frequence = int.Parse(pos["frequence"]); } catch { }

                if (type_ == "fromfile")
                    Application.Run(new FrmFromFile(interface_, frequence));
                else if (type_ == "fromserialSwelab")
                    Application.Run(new FrmFromSerialSwelab(interface_));
                else if (type_ == "fromserialClinitek")
                    Application.Run(new FrmFromSerialClinitek(interface_));
                else if (type_ == "fromserialCoagulyzer_4K")
                    Application.Run(new FrmFromSerialCoagulyzer_4K(interface_));
                else if (type_ == "fromserialST200")
                    Application.Run(new FrmFromSerialST200(interface_));
                else if (type_ == "fromserialClinitekPlayas")
                    Application.Run(new FrmFromSerialClinitekPlayas(interface_));
                else if (type_.Contains("SerialGeneric"))
                    Application.Run(new FrmFromSerialGeneric(interface_));
                else if (type_.Contains("NetworkGeneric"))
                    Application.Run(new FrmFromNetworkGeneric(interface_));
                else if (type_.Contains("SocketGeneric"))
                    Application.Run(new FrmFromSocketGeneric(interface_));
                else if (type_.Contains("FileToSQL"))
                    Application.Run(new frmFileToSQL());
                else if (type_.Contains("SQLGenericToSQL"))
                    Application.Run(new frmSQLGenericToSQL());
                else
                    Application.Run(new FrmImmulite());
            }
            catch (Exception xx)
            {
                MessageBox.Show("[key:interface|type] => " + xx.Message);
               // Application.Run(new FrmImmulite());
            }
        }


        public static string getFileName(int FileType)
        {
            string imagename = string.Empty;
            FileDialog fldlg = new OpenFileDialog();
            //specify your own initial directory
            fldlg.InitialDirectory = @":C\";
            //this will allow only those file extensions to be added
            if (FileType == 0) fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
            else if (FileType == 1) fldlg.Filter = "File (*.xls;*.xlsx)|*.xls;xlsx";
            else if (FileType == 2) fldlg.Filter = "File (*.txt)|*.txt";
            else if (FileType == 3) fldlg.Filter = "File (*.xml;*.mdc;)|*.xml;*.mdc;";

            if (fldlg.ShowDialog() == DialogResult.OK)
            {
                return fldlg.FileName;
            }

            return "";

        }

        public static string AssemblyName {
            get
            {
                return System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            }
        }

        public static List<string> getTextContent(string fileName)
        {
            try
            {
                string ln;
                List<string> fileContent = new List<string>();

                using (StreamReader file = new StreamReader(fileName))
                {
                    //get statuses
                    while ((ln = file.ReadLine()) != null)
                    {
                        fileContent.Add(ln);
                    }
                    file.Close();
                }

                return fileContent;
            }
            catch
            {
                MessageBox.Show("config file not found." + fileName, "SmartUp",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }


        public static Dictionary<string, string> pos = new Dictionary<string, string>();
        public static Dictionary<string, string> db = new Dictionary<string, string>();

        public static void setConfig(string[] args)
        {           
            string[] split;
            string drive = Path.GetPathRoot(Environment.SystemDirectory);
            string configFile = args.Length > 0 ? args[0] : string.Format(@drive + @"SmartUp\config{0}.txt", AssemblyName);
            List<string> ctx = getTextContent(configFile);
            if (ctx.Count > 0)
            {
                for (int i = 1; i <= ctx.Count - 1; i++) {

                    if (ctx[i].Substring(0, 1) == "#") continue; //help so skip

                    split = ctx[i].Split(';');
                    foreach (var s in split)
                    {
                        var v = s.Split('=');
                        if (v[0] == "") continue;
                        pos.Add(v[0], v[1]);
                    }
                }
                split = ctx[0].Split(';');
                foreach (var s in split)
                {
                    var v = s.Split('=');
                    if (v[0] == "") continue;
                    db.Add(v[0], v[1]);
                }
            }

        }

    }

    public sealed class err
    {
        public static void fire(bool p, string e) { if (p) throw new Exception(e); }
        public static void fire(bool p, string e, string extra) { if (p) throw new Exception(e + " : " + extra); }
        public static void fire(bool p, string e, int extra) { if (p) throw new Exception(e + " : " + extra.ToString()); }
        public static void fire(bool p, string e, double extra) { if (p) throw new Exception(e + " : " + extra.ToString()); }
        public static void fire(bool p, string e, ref char[] ex, int exl) { if (p) throw new Exception(e); }
        public static void fire(bool p, ref string e) { if (p) throw new Exception(e); }
        public static void fire(bool p, ref string e, ref string extra) { if (p) throw new Exception(e); }
        public static void fire(bool p, string e, ref string extra) { if (p) throw new Exception(e); }
        public static void fire(bool p, ref string e, ref char[] ex, int exl) { if (p) throw new Exception(e); }
        public static void fire(bool p, ref string e, int extra) { if (p) throw new Exception(e); }
    }
}
