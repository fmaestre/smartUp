
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;


namespace SmartUp
{

    public partial class FrmFromSerialGeneric : Form
    {
        private string _interfaceName = "";
        private int _ReadCounts = 0;
        SerialPort mySerialPort;
        delegate void SetTextCallback(string text);

        public FrmFromSerialGeneric(string interfaceName)
        {
            try
            {
                InitializeComponent();
                _interfaceName = interfaceName;
                
                this.Text += "- [" + interfaceName + "] " + Program.pos["serialport"] + " ***" + Program.AssemblyName + "***";
                openPort();
                mySerialPort.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                lblError.Visible = true;
                lblError.Text = string.Format("SocketException: {0}", ex.Message);
            }
        }
        
        private void openPort()
        {
            mySerialPort            = new SerialPort(Program.pos["serialport"]);
            mySerialPort.BaudRate   = int.Parse(Program.pos["BaudRate"]);//9600;
            mySerialPort.Parity     = (Parity)Enum.Parse(typeof(Parity), Program.pos["Parity"]);   //Parity.None;
            mySerialPort.StopBits   = (StopBits)Enum.Parse(typeof(StopBits), Program.pos["StopBits"]);//StopBits.One;
            mySerialPort.DataBits   = int.Parse(Program.pos["DataBits"]);//8;
            mySerialPort.Handshake  = (Handshake)Enum.Parse(typeof(Handshake), Program.pos["Handshake"]);//Handshake.XOnXOff;
            mySerialPort.RtsEnable  = bool.Parse(Program.pos["RtsEnable"]); //TRUE
            mySerialPort.DtrEnable  = bool.Parse(Program.pos["DtrEnable"]); //TRUE
            mySerialPort.ReadTimeout = int.Parse(Program.pos["ReadTimeout"]); //10000;
            mySerialPort.ReadBufferSize = int.Parse(Program.pos["ReadBufferSize"]); //4090;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);

            mySerialPort.Open();
        }
        
        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {                       
            SetTextCallback method = new SetTextCallback(SetText);
            SerialPort sp = (SerialPort)sender;

            this.Invoke(method, new object[] { sp.ReadExisting() });

        }
        private void SetText(string newText)
        {
            if (!string.IsNullOrEmpty(newText))
            {
                if (++_ReadCounts == int.Parse(Program.pos["OutBufferSize"])) { txtResults.Text = ""; _ReadCounts = 0;}

                txtResults.Text += newText;
                //save to file
                var path = Program.pos["SavePath"]; //the path to save interface files
                var file = path + _interfaceName + DateTime.Today.ToString("MMddyyyy" ) + ".txt";
                
                if (!System.IO.File.Exists(file))
                {
                    //System.IO.File.Create(file).Close();                    
                    System.IO.File.Create(file).Dispose(); //2021Feb23 - to fix The Process Cannot Access The File XYZ Because It Is Being Used By Another Process
                }

                var allText = System.IO.File.ReadAllText(file);

                System.IO.File.WriteAllText(file, allText + newText);
            }
        }   

    }
}


