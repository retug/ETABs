
namespace ETABS_Plugin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ShowReactionForcesBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LoadCombinationComBox = new System.Windows.Forms.ComboBox();
            this.ShowLoadCombinationsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(57, 43);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(553, 102);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Hellow World!";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ShowReactionForcesBtn
            // 
            this.ShowReactionForcesBtn.Location = new System.Drawing.Point(57, 169);
            this.ShowReactionForcesBtn.Name = "ShowReactionForcesBtn";
            this.ShowReactionForcesBtn.Size = new System.Drawing.Size(154, 48);
            this.ShowReactionForcesBtn.TabIndex = 1;
            this.ShowReactionForcesBtn.Text = "Show Reaction Forces";
            this.ShowReactionForcesBtn.UseVisualStyleBackColor = true;
            this.ShowReactionForcesBtn.Click += new System.EventHandler(this.ShowReactionFrocesBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(57, 247);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(985, 277);
            this.dataGridView1.TabIndex = 2;
            // 
            // LoadCombinationComBox
            // 
            this.LoadCombinationComBox.FormattingEnabled = true;
            this.LoadCombinationComBox.Location = new System.Drawing.Point(471, 196);
            this.LoadCombinationComBox.Name = "LoadCombinationComBox";
            this.LoadCombinationComBox.Size = new System.Drawing.Size(506, 21);
            this.LoadCombinationComBox.TabIndex = 3;
            // 
            // ShowLoadCombinationsBtn
            // 
            this.ShowLoadCombinationsBtn.Location = new System.Drawing.Point(233, 169);
            this.ShowLoadCombinationsBtn.Name = "ShowLoadCombinationsBtn";
            this.ShowLoadCombinationsBtn.Size = new System.Drawing.Size(154, 48);
            this.ShowLoadCombinationsBtn.TabIndex = 4;
            this.ShowLoadCombinationsBtn.Text = "Show Load Combo";
            this.ShowLoadCombinationsBtn.UseVisualStyleBackColor = true;
            this.ShowLoadCombinationsBtn.Click += new System.EventHandler(this.ShowLoadCombinationsBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 583);
            this.Controls.Add(this.ShowLoadCombinationsBtn);
            this.Controls.Add(this.LoadCombinationComBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ShowReactionForcesBtn);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "My First Plugin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ShowReactionForcesBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox LoadCombinationComBox;
        private System.Windows.Forms.Button ShowLoadCombinationsBtn;
    }
}