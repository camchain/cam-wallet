using Cam.SmartContract;
using System.Linq;
using System.Windows.Forms;

using Cam.Properties;
using System;


namespace Cam.UI
{
    public partial class VerifyContractDialog : Form
    {
        public VerifyContractDialog()
        {
            InitializeComponent();           

        }

        private void btnVerify_Click(object sender, System.EventArgs e)
        {
            try
            {
                byte[] script = Cam.Helper.HexToBytes(textBox4.Text);

                if (script.Length == 40 && script[0] == 0x21 && script[34] == 0x04 && script[39] == 0xd0)
                {
                    Contract contract = new Contract();
                    contract.ParameterList = new ContractParameterType[1] { 0x00 };
                    contract.Script = script;

                    textBox2.Text = contract.Address;

                    if (textBox2.Text != textBox1.Text)
                    {
                        MessageBox.Show(Strings.VerifyContractFail);
                    }
                    else
                    {
                        MessageBox.Show(Strings.VerifyContractSuccess);


                        string opMsg = Helper.ToOpCodeFormat(textBox4.Text);

                        textBox5.Text = opMsg;

                    }
                }
                else
                {
                    MessageBox.Show(Strings.VerifyContractFail2);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Strings.VerifyContractFail2 + ";"+ex.Message);
            }
            
        }
    }
}
