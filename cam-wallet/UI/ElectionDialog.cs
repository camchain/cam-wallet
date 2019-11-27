using Cam.Cryptography.ECC;
using Cam.IO;
using Cam.Network.P2P.Payloads;
using Cam.SmartContract;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Cam.UI
{
    public partial class ElectionDialog : Form
    {
        public ElectionDialog()
        {
            InitializeComponent();
        }

        public StateTransaction GetTransaction()
        {
            ECPoint pubkey = (ECPoint)comboBox1.SelectedItem;
            return Program.CurrentWallet.MakeTransaction(new StateTransaction
            {
                Version = 0,
                Descriptors = new[]
                {
                    new StateDescriptor
                    {
                        Type = StateType.Validator,
                        Key = pubkey.ToArray(),
                        Field = "Registered",
                        Value = BitConverter.GetBytes(true)
                    }
                }
            });
        }

        private void ElectionDialog_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Program.CurrentWallet.GetAccounts().Where(p => !p.WatchOnly && p.Contract.Script.IsStandardContract()).Select(p => p.GetKey().PublicKey).ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                button1.Enabled = true;
                ECPoint pubkey = (ECPoint)comboBox1.SelectedItem;
                StateTransaction tx = new StateTransaction
                {
                    Version = 0,
                    Descriptors = new[]
                    {
                        new StateDescriptor
                        {
                            Type = StateType.Validator,
                            Key = pubkey.ToArray(),
                            Field = "Registered",
                            Value = BitConverter.GetBytes(true)
                        }
                    }
                };
                label3.Text = $"{tx.SystemFee} gas";
            }
        }
    }
}
