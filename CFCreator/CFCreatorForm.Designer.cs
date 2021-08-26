
namespace CFCreator
{
    partial class CFCreatorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tgtRowLabel = new System.Windows.Forms.Label();
            this.TgtRows = new System.Windows.Forms.NumericUpDown();
            this.tgtColsLabel = new System.Windows.Forms.Label();
            this.TgtCols = new System.Windows.Forms.NumericUpDown();
            this.DrawWafer = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.CreateCF = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TgtRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TgtCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CreateCF, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(615, 531);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tgtRowLabel);
            this.flowLayoutPanel1.Controls.Add(this.TgtRows);
            this.flowLayoutPanel1.Controls.Add(this.tgtColsLabel);
            this.flowLayoutPanel1.Controls.Add(this.TgtCols);
            this.flowLayoutPanel1.Controls.Add(this.DrawWafer);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(104, 445);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // tgtRowLabel
            // 
            this.tgtRowLabel.AutoSize = true;
            this.tgtRowLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tgtRowLabel.Location = new System.Drawing.Point(3, 0);
            this.tgtRowLabel.Name = "tgtRowLabel";
            this.tgtRowLabel.Size = new System.Drawing.Size(90, 15);
            this.tgtRowLabel.TabIndex = 0;
            this.tgtRowLabel.Text = "Target Rows";
            // 
            // TgtRows
            // 
            this.TgtRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TgtRows.Location = new System.Drawing.Point(3, 18);
            this.TgtRows.Name = "TgtRows";
            this.TgtRows.Size = new System.Drawing.Size(90, 23);
            this.TgtRows.TabIndex = 1;
            this.TgtRows.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // tgtColsLabel
            // 
            this.tgtColsLabel.AutoSize = true;
            this.tgtColsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tgtColsLabel.Location = new System.Drawing.Point(3, 44);
            this.tgtColsLabel.Name = "tgtColsLabel";
            this.tgtColsLabel.Size = new System.Drawing.Size(90, 15);
            this.tgtColsLabel.TabIndex = 2;
            this.tgtColsLabel.Text = "Target Columns";
            // 
            // TgtCols
            // 
            this.TgtCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TgtCols.Location = new System.Drawing.Point(3, 62);
            this.TgtCols.Name = "TgtCols";
            this.TgtCols.Size = new System.Drawing.Size(90, 23);
            this.TgtCols.TabIndex = 3;
            this.TgtCols.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // DrawWafer
            // 
            this.DrawWafer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawWafer.Location = new System.Drawing.Point(3, 91);
            this.DrawWafer.Name = "DrawWafer";
            this.DrawWafer.Size = new System.Drawing.Size(90, 53);
            this.DrawWafer.TabIndex = 1;
            this.DrawWafer.Text = "Draw Wafer";
            this.DrawWafer.UseVisualStyleBackColor = true;
            this.DrawWafer.Click += new System.EventHandler(this.DrawWafer_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(113, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(499, 445);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // CreateCF
            // 
            this.CreateCF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CreateCF.Location = new System.Drawing.Point(113, 454);
            this.CreateCF.Name = "CreateCF";
            this.CreateCF.Size = new System.Drawing.Size(499, 74);
            this.CreateCF.TabIndex = 3;
            this.CreateCF.Text = "Create Cycle File";
            this.CreateCF.UseVisualStyleBackColor = true;
            this.CreateCF.Click += new System.EventHandler(this.CreateCF_Click);
            // 
            // CFCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CFCreatorForm";
            this.Text = "CFCreator";
            this.Load += new System.EventHandler(this.CFCreatorForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TgtRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TgtCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button DrawWafer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label tgtRowLabel;
        private System.Windows.Forms.NumericUpDown TgtRows;
        private System.Windows.Forms.Label tgtColsLabel;
        private System.Windows.Forms.NumericUpDown TgtCols;
        private System.Windows.Forms.Button CreateCF;
    }
}

