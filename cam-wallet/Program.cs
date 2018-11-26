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
        /// <summary>
        /// 安装证书服务器的根证书
        /// </summary>
        /// <returns></returns>
        private static bool InstallCertificate()
        {
            if (!Settings.Default.InstallCertificate) return true;
            using (X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            using (X509Certificate2 cert = new X509Certificate2(Resources.CamCertificate))
            {
                store.Open(OpenFlags.ReadOnly);
                if (store.Certificates.Contains(cert)) return true;
            }
            using (X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            using (X509Certificate2 cert = new X509Certificate2(Resources.CamCertificate))
            {
                try
                {
                    store.Open(OpenFlags.ReadWrite);
                    store.Add(cert);
                    return true;
                }
                catch (CryptographicException) { }
                if (MessageBox.Show(Strings.InstallCertificateText, Strings.InstallCertificateCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    Settings.Default.InstallCertificate = false;
                    Settings.Default.Save();
                    return true;
                }
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Application.ExecutablePath,
                        UseShellExecute = true,
                        Verb = "runas",
                        WorkingDirectory = Environment.CurrentDirectory
                    });
                    return false;
                }
                catch (Win32Exception) { }
                MessageBox.Show(Strings.InstallCertificateCancel);
                return true;
            }
        }        

        [STAThread]
        public static void Main()
        {
            uint time = DateTime.Now.AddHours(4).ToTimestamp();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            //判断是否安装了根证书
            if (!InstallCertificate()) return;
            
            const string PeerStatePath = "peers.dat";
            if (File.Exists(PeerStatePath))
            {
                using (FileStream fs = new FileStream(PeerStatePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    LocalNode.LoadState(fs);
                }
            }

            using (Blockchain.RegisterBlockchain(new LevelDBBlockchain(Settings.Default.Paths.Chain)))                
            using (LocalNode = new LocalNode())
            {
                LocalNode.UpnpEnabled = true;
                Application.Run(MainForm = new MainForm());
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
