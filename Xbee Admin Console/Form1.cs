using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using XBee;
using XBee.Frames.AtCommands;
using XBee.Frames;
using XbeeUtils;
using Singleton;
using System.Configuration;
using BusinessLayer;
namespace XbeeAdminConsole
{
    public partial class Form1 : Form
    {
        XbeeSingleton instancia = XbeeSingleton.Instance;

        void CargarConfiguracionMysql()
        {
            instancia.SqlServidor = ConfigurationManager.AppSettings["servidor"];
            instancia.SqlUsuario = ConfigurationManager.AppSettings["usuario"];
            instancia.SqlPassword = ConfigurationManager.AppSettings["password"];
            instancia.SqlBaseDatos = ConfigurationManager.AppSettings["bd"];
        }

        public Form1()
        {
            InitializeComponent();
            CargarConfiguracionMysql();
        }
        
        public List<LogPantalla> ListadoLog = new List<LogPantalla>();
                
        private Main claseMain = new Main();

        private void Form1_Load(object sender, EventArgs e)
        {
            claseMain.MonitoreoEvent += MonitoreoProceso_Main;
            
            //string tramaRecibida = "T:7705904:3";
            //string tramaRecibida = "ET:2:5129970:38974204:007560:1642918:13253948:7950:0:0:0:";
            //string tramaRecibida = "I:3:VXF168:1234567890";
            ////string tramaRecibida = "N:7";
            //string tramaRecibida = "P:45:3:3";
            ////string tramaRecibida = "P:4:3:2";
            ////string tramaRecibida = "ET:2:0004602921:0034969731:007560:0001471717:0011889213:007950:0:0:0:";
            ////string tramaRecibida = "I:2:JEX30B:58000";
            ////string tramaRecibida = "E:12345:2";
            //string tramaRecibida = "M:15";
            
            
            //////string tramaRecibida = "P:4:1:2";
            //////string tramaRecibida = "T:12345:2";
            //////string tramaRecibida = "E:12345:2";
            //////string tramaRecibida = "M:29";

            //string tramaRecibida = "F:2:30f98b0d";
            //NodosXbee _nodoPrueba = new NodosXbee(null, "MOD POS 1", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.moduloPOS, 1);
            //string[] arrayTramaRecibida = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida);
            //claseMain.ProcesarTrama(arrayTramaRecibida, _nodoPrueba);

            //string tramaRecibida1 = "ET:2:5129970:38977204:007560:1642918:13253948:7950:0:0:0:";
            //NodosXbee _nodoPrueba1 = new NodosXbee(null, "MOD POS 1", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.Dispensador, 1);
            //string[] arrayTramaRecibida1 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida1);
            //claseMain.ProcesarTrama(arrayTramaRecibida1, _nodoPrueba1);

            string tramaRecibida2 = "I:2:VXF168:5555";
            NodosXbee _nodoPrueba2 = new NodosXbee(null, "MOD POS 1", "MACPRUEBA", "MACIMPRESION", 0, Enumeraciones.TipoDispositivo.moduloPOS, 1);
            string[] arrayTramaRecibida2 = UtilidadesTramas.ObtieneArrayTrama(tramaRecibida2);
            claseMain.ProcesarTrama(arrayTramaRecibida2, _nodoPrueba2);
        }
        
        public delegate void AsignarRegistrosRejilla();
        public AsignarRegistrosRejilla delegadoRegistrosRejilla;

        BindingSource source;
        void RefrescarRejilla()
        {
            
            source = new BindingSource();
            source.DataSource = ListadoLog;
            gcRejilla.DataSource = null;
            gcRejilla.DataSource = source;
            gcRejilla.Refresh();
            gcRejilla.AutoGenerateColumns = true;
        }

        void MonitoreoProceso_Main(object sender, MonitoreoEventArgs e)
        {
            //if (ListadoLog == null)
            //{
            //    ListadoLog = new List<LogPantalla>();
            //}
            LogPantalla newLog = new LogPantalla();
            newLog.Mensaje = e.Texto;
            newLog.Fecha = DateTime.Now;
            ListadoLog.Add(newLog);
            if (this.gcRejilla.InvokeRequired == true)
            {
                AsignarRegistrosRejilla d = new AsignarRegistrosRejilla(RefrescarRejilla);
                this.Invoke(d, new object[] { });
            }
            else
            {
                RefrescarRejilla();
            }
            
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            ListadoLog.Clear();
            gcRejilla.DataSource = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            claseMain.Desconectar();
            claseMain.ConectaryDescubrirRed();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            claseMain.ConectaryDescubrirRed();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                claseMain.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al desconectar " + ex.Message);
                throw;
            }
            
        }



        //void DataReceivedXbee(object sender, SourcedDataReceivedEventArgs e)
        //{

        //    string data = System.Text.Encoding.UTF8.GetString(e.Data);
        //    XBeeNode nodeXbee = ListNodes.Find(x => object.Equals(x.Address.LongAddress.Value, e.Address.LongAddress.Value));
        //    if (nodeXbee != null)
        //    {
        //        nodeXbee.TransmitDataAsync(Encoding.UTF8.GetBytes("C       Hello!"),false);
        //    }
        //}

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    dataGridView1.DataSource = ListNodes;
        //    var localNode = controller.Local;
        //    var serialNumber = localNode.GetSerialNumber();
        //    //localNode.Reset();
            

        //    //controller.Dispose();

        //    controller.Close();
        //}

        //public void NodeDiscovered_controller(object sender, NodeDiscoveredEventArgs args)
        //{
        //    ListNodes.Add(args.Node);
        //    string xxx = string.Format( "Discovered {0}", args.Name);



        //}

     


       
    }

    
}
