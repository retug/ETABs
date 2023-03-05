
namespace GetSelectedObjects
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
            this.getSelNodeBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.vectorX = new System.Windows.Forms.TextBox();
            this.vectorY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LoadCaseComBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.NumSlices = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.runAnalysisBtn = new System.Windows.Forms.Button();
            this.makeSectionCut = new System.Windows.Forms.Button();
            this.scatterPlot = new LiveCharts.WinForms.CartesianChart();
            this.momentScatterPlot = new LiveCharts.WinForms.CartesianChart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // getSelNodeBtn
            // 
            this.getSelNodeBtn.Location = new System.Drawing.Point(39, 33);
            this.getSelNodeBtn.Name = "getSelNodeBtn";
            this.getSelNodeBtn.Size = new System.Drawing.Size(136, 31);
            this.getSelNodeBtn.TabIndex = 0;
            this.getSelNodeBtn.Text = "Get Start Node";
            this.getSelNodeBtn.UseVisualStyleBackColor = true;
            this.getSelNodeBtn.Click += new System.EventHandler(this.getSelNodeBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(233, 57);
            this.dataGridView1.TabIndex = 1;
            // 
            // vectorX
            // 
            this.vectorX.Location = new System.Drawing.Point(8, 42);
            this.vectorX.Name = "vectorX";
            this.vectorX.Size = new System.Drawing.Size(100, 20);
            this.vectorX.TabIndex = 2;
            this.vectorX.Text = "Enter X Value";
            this.vectorX.Enter += new System.EventHandler(this.vectorX_Enter);
            this.vectorX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vectorX_KeyPress);
            this.vectorX.Leave += new System.EventHandler(this.vectorX_TextChanged);
            // 
            // vectorY
            // 
            this.vectorY.Location = new System.Drawing.Point(125, 42);
            this.vectorY.Name = "vectorY";
            this.vectorY.Size = new System.Drawing.Size(100, 20);
            this.vectorY.TabIndex = 3;
            this.vectorY.Text = "Enter Y Value";
            this.vectorY.Enter += new System.EventHandler(this.vectorY_Enter);
            this.vectorY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vectorX_KeyPress);
            this.vectorY.Leave += new System.EventHandler(this.vectorY_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "X Vector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y Vector";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.vectorY);
            this.groupBox1.Controls.Add(this.vectorX);
            this.groupBox1.Location = new System.Drawing.Point(532, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Vector";
            // 
            // LoadCaseComBox
            // 
            this.LoadCaseComBox.FormattingEnabled = true;
            this.LoadCaseComBox.Location = new System.Drawing.Point(780, 71);
            this.LoadCaseComBox.Name = "LoadCaseComBox";
            this.LoadCaseComBox.Size = new System.Drawing.Size(151, 21);
            this.LoadCaseComBox.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(780, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "Gather Load Cases";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ShowLoadCase_Load);
            // 
            // NumSlices
            // 
            this.NumSlices.Location = new System.Drawing.Point(15, 30);
            this.NumSlices.Name = "NumSlices";
            this.NumSlices.Size = new System.Drawing.Size(154, 20);
            this.NumSlices.TabIndex = 9;
            this.NumSlices.Text = "Number of Sections";
            this.NumSlices.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numSlices_KeyPress);
            this.NumSlices.Leave += new System.EventHandler(this.NumSlices_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NumSlices);
            this.groupBox2.Location = new System.Drawing.Point(937, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(189, 60);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Number of Sections";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 31);
            this.button2.TabIndex = 0;
            this.button2.Text = "Get Areas";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.getSelAreas_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(275, 72);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(219, 57);
            this.dataGridView2.TabIndex = 1;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(15, 571);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(1132, 135);
            this.dataGridView3.TabIndex = 11;
            // 
            // runAnalysisBtn
            // 
            this.runAnalysisBtn.Location = new System.Drawing.Point(532, 107);
            this.runAnalysisBtn.Name = "runAnalysisBtn";
            this.runAnalysisBtn.Size = new System.Drawing.Size(170, 22);
            this.runAnalysisBtn.TabIndex = 12;
            this.runAnalysisBtn.Text = "Run Analysis";
            this.runAnalysisBtn.UseVisualStyleBackColor = true;
            this.runAnalysisBtn.Click += new System.EventHandler(this.runAnalysis_Click);
            // 
            // makeSectionCut
            // 
            this.makeSectionCut.Location = new System.Drawing.Point(708, 107);
            this.makeSectionCut.Name = "makeSectionCut";
            this.makeSectionCut.Size = new System.Drawing.Size(170, 22);
            this.makeSectionCut.TabIndex = 12;
            this.makeSectionCut.Text = "Section Cut";
            this.makeSectionCut.UseVisualStyleBackColor = true;
            this.makeSectionCut.Click += new System.EventHandler(this.makeSectionCutBtn);
            // 
            // scatterPlot
            // 
            this.scatterPlot.Location = new System.Drawing.Point(15, 22);
            this.scatterPlot.Name = "scatterPlot";
            this.scatterPlot.Size = new System.Drawing.Size(1132, 262);
            this.scatterPlot.TabIndex = 14;
            this.scatterPlot.Text = "cartesianChart1";
            this.scatterPlot.Visible = false;
            // 
            // momentScatterPlot
            // 
            this.momentScatterPlot.Location = new System.Drawing.Point(15, 300);
            this.momentScatterPlot.Name = "momentScatterPlot";
            this.momentScatterPlot.Size = new System.Drawing.Size(1132, 265);
            this.momentScatterPlot.TabIndex = 14;
            this.momentScatterPlot.Text = "cartesianChart1";
            this.momentScatterPlot.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.momentScatterPlot);
            this.groupBox3.Controls.Add(this.scatterPlot);
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Location = new System.Drawing.Point(33, 141);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1162, 725);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Results";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1689, 871);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.makeSectionCut);
            this.Controls.Add(this.runAnalysisBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LoadCaseComBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.getSelNodeBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getSelNodeBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox vectorX;
        private System.Windows.Forms.TextBox vectorY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox LoadCaseComBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox NumSlices;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button runAnalysisBtn;
        private System.Windows.Forms.Button makeSectionCut;
        private LiveCharts.WinForms.CartesianChart scatterPlot;
        private LiveCharts.WinForms.CartesianChart momentScatterPlot;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}