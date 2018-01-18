using Cam.SmartContract;
using Cam.Wallets;
using System.Windows.Forms;

namespace Cam.UI
{
    internal partial class ContractDetailsDialog : Form
    {
        public ContractDetailsDialog(Contract contract)
        {
            InitializeComponent();
            textBox1.Text = Wallet.ToAddress(contract.ScriptHash);
            textBox2.Text = contract.ScriptHash.ToString();
            textBox3.Text = contract.Script.ToHexString();
        }
    }
}
