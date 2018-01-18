﻿using Cam.Core;
using Cam.SmartContract;
using Cam.VM;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cam.UI
{
    internal partial class DeployContractDialog : Form
    {
        public DeployContractDialog()
        {
            InitializeComponent();
        }

        public InvocationTransaction GetTransaction()
        {
            byte[] script = textBox8.Text.HexToBytes();
            byte[] parameter_list = textBox6.Text.HexToBytes();
            ContractParameterType return_type = textBox7.Text.HexToBytes().Select(p => (ContractParameterType?)p).FirstOrDefault() ?? ContractParameterType.Void;
            bool need_storage = checkBox1.Checked;
            string name = textBox1.Text;
            string version = textBox2.Text;
            string author = textBox3.Text;
            string email = textBox4.Text;
            string description = textBox5.Text;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitSysCall("Cam.Contract.Create", script, parameter_list, return_type, need_storage, name, version, author, email, description);
                return new InvocationTransaction
                {
                    Script = sb.ToArray()
                };
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox1.TextLength > 0
                && textBox2.TextLength > 0
                && textBox3.TextLength > 0
                && textBox4.TextLength > 0
                && textBox5.TextLength > 0
                && textBox8.TextLength > 0;
            try
            {
                textBox9.Text = textBox8.Text.HexToBytes().ToScriptHash().ToString();
            }
            catch (FormatException)
            {
                textBox9.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            textBox8.Text = File.ReadAllBytes(openFileDialog1.FileName).ToHexString();
        }
    }
}
