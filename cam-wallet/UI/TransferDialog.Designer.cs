namespace Cam.UI
{
    partial class TransferDialog
    {



        private System.ComponentModel.IContainer components = null;




        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code




        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txOutListBox1 = new Cam.UI.TxOutListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();



            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";



            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);



            resources.ApplyResources(this.button3, "button3");
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;



            resources.ApplyResources(this.button4, "button4");
            this.button4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;



            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.txOutListBox1);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;



            resources.ApplyResources(this.button1, "button1");
            this.button1.Image = global::Cam.Properties.Resources.remark;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);



            resources.ApplyResources(this.txOutListBox1, "txOutListBox1");
            this.txOutListBox1.Asset = null;
            this.txOutListBox1.Name = "txOutListBox1";
            this.txOutListBox1.ReadOnly = false;
            this.txOutListBox1.ScriptHash = null;
            this.txOutListBox1.ItemsChanged += new System.EventHandler(this.txOutListBox1_ItemsChanged);



            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;



            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";



            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";



            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";



            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";



            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TransferDialog";
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private TxOutListBox txOutListBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
    }
}