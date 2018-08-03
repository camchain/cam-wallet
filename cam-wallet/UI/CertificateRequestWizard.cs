using CERTENROLLLib;
using Cam.Wallets;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cam.UI
{
    public partial class CertificateRequestWizard : Form
    {
        public CertificateRequestWizard()
        {
            InitializeComponent();
        }

        private void CertificateRequestWizard_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Program.CurrentWallet.GetAccounts().Where(p => !p.WatchOnly && p.Contract.IsStandard).Select(p => p.GetKey()).ToArray());
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex >= 0 && groupBox1.Controls.OfType<TextBox>().All(p => p.TextLength > 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            KeyPair key = (KeyPair)comboBox1.SelectedItem;
            byte[] pubkey = key.PublicKey.EncodePoint(false).Skip(1).ToArray();
            byte[] prikey;
            using (key.Decrypt())
            {
                const int ECDSA_PRIVATE_P256_MAGIC = 0x32534345;
                prikey = BitConverter.GetBytes(ECDSA_PRIVATE_P256_MAGIC).Concat(BitConverter.GetBytes(32)).Concat(pubkey).Concat(key.PrivateKey).ToArray();
            }
            CX509PrivateKey x509key = new CX509PrivateKey();
            x509key.AlgorithmName = "ECDSA_P256";
            x509key.Import("ECCPRIVATEBLOB", Convert.ToBase64String(prikey));
            Array.Clear(prikey, 0, prikey.Length);
            CX509CertificateRequestPkcs10 request = new CX509CertificateRequestPkcs10();
            request.InitializeFromPrivateKey(X509CertificateEnrollmentContext.ContextUser, x509key, null);
            
            request.Subject = new CX500DistinguishedName();                       
            //使用换行进行分隔，可兼容特殊字符
            request.Subject.Encode(string.Format("CN={0}\r\nC={1}\r\nS={2}\r\nO={3}",textBox1.Text,textBox2.Text,textBox3.Text, "Cayman Islands CAM Digital Asset Management Co.,Ltd."),
                X500NameFlags.XCN_CERT_NAME_STR_CRLF_FLAG);

            request.Encode();

            File.WriteAllText(saveFileDialog1.FileName, "-----BEGIN NEW CERTIFICATE REQUEST-----\r\n" + request.RawData + "-----END NEW CERTIFICATE REQUEST-----\r\n");
            Close();
        }
    }
}
