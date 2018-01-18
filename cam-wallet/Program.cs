using Cam.Core;
using Cam.Implementations.Blockchains.LevelDB;
using Cam.Network;
using Cam.Properties;
using Cam.UI;
using Cam.Wallets;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cam
{
    internal static class Program
    {
        public static LocalNode LocalNode;
        public static Wallet CurrentWallet;
        public static MainForm MainForm;

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (FileStream fs = new FileStream("error.log", FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter w = new StreamWriter(fs))
            {
                PrintErrorLogs(w, (Exception)e.ExceptionObject);
            }
        }










































        [STAThread]
        public static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = 

         System.Globalization.CultureInfo.CreateSpecificCulture("en");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            XDocument xdoc = null;
            try
            {

            }
            catch { }
            if (xdoc != null)
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                Version minimum = Version.Parse(xdoc.Element("update").Attribute("minimum").Value);
                if (version < minimum)
                {
                    using (UpdateDialog dialog = new UpdateDialog(xdoc))
                    {
                        dialog.ShowDialog();
                    }
                    return;
                }
            }

            const string PeerStatePath = "peers.dat";
            if (File.Exists(PeerStatePath))
                using (FileStream fs = new FileStream(PeerStatePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    LocalNode.LoadState(fs);
                }

            using (Blockchain.RegisterBlockchain(new LevelDBBlockchain(Settings.Default.Paths.Chain)))

                
            using (LocalNode = new LocalNode())
            {
                LocalNode.UpnpEnabled = true;
                Application.Run(MainForm = new MainForm(xdoc));
            }
            using (FileStream fs = new FileStream(PeerStatePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                LocalNode.SaveState(fs);
            }
        }

        private static void PrintErrorLogs(StreamWriter writer, Exception ex)
        {
            writer.WriteLine(ex.GetType());
            writer.WriteLine(ex.Message);
            writer.WriteLine(ex.StackTrace);
            if (ex is AggregateException ex2)
            {
                foreach (Exception inner in ex2.InnerExceptions)
                {
                    writer.WriteLine();
                    PrintErrorLogs(writer, inner);
                }
            }
            else if (ex.InnerException != null)
            {
                writer.WriteLine();
                PrintErrorLogs(writer, ex.InnerException);
            }
        }
    }
}
