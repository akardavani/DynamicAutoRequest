namespace DynamicAutoRequest
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
            txtRequest = new TextBox();
            btnSaveData = new Button();
            btnSendRequest = new Button();
            txtRequestTime = new TextBox();
            btnTest = new Button();
            txtTotalRequests = new TextBox();
            txtBatchSize = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtDelay = new TextBox();
            label5 = new Label();
            chkAdd = new CheckBox();
            txtEnd = new TextBox();
            txtStart = new TextBox();
            chkLog = new CheckBox();
            cmbProvider = new ComboBox();
            SuspendLayout();
            // 
            // txtRequest
            // 
            txtRequest.Location = new Point(14, 12);
            txtRequest.Margin = new Padding(4, 3, 4, 3);
            txtRequest.Multiline = true;
            txtRequest.Name = "txtRequest";
            txtRequest.Size = new Size(642, 428);
            txtRequest.TabIndex = 0;
            // 
            // btnSaveData
            // 
            btnSaveData.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            btnSaveData.Location = new Point(225, 460);
            btnSaveData.Margin = new Padding(4, 3, 4, 3);
            btnSaveData.Name = "btnSaveData";
            btnSaveData.Size = new Size(128, 51);
            btnSaveData.TabIndex = 1;
            btnSaveData.Text = "Save";
            btnSaveData.UseVisualStyleBackColor = true;
            btnSaveData.Click += btnSaveData_Click;
            // 
            // btnSendRequest
            // 
            btnSendRequest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSendRequest.Font = new Font("Microsoft Sans Serif", 36F, FontStyle.Bold);
            btnSendRequest.ForeColor = Color.ForestGreen;
            btnSendRequest.Location = new Point(681, 227);
            btnSendRequest.Margin = new Padding(4, 3, 4, 3);
            btnSendRequest.Name = "btnSendRequest";
            btnSendRequest.Size = new Size(229, 83);
            btnSendRequest.TabIndex = 2;
            btnSendRequest.Text = "Send";
            btnSendRequest.UseVisualStyleBackColor = true;
            btnSendRequest.Click += btnSendRequest_Click;
            // 
            // txtRequestTime
            // 
            txtRequestTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtRequestTime.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            txtRequestTime.ForeColor = SystemColors.WindowText;
            txtRequestTime.Location = new Point(681, 169);
            txtRequestTime.Margin = new Padding(4, 3, 4, 3);
            txtRequestTime.Name = "txtRequestTime";
            txtRequestTime.Size = new Size(229, 35);
            txtRequestTime.TabIndex = 3;
            txtRequestTime.Text = "08:44:54.800";
            txtRequestTime.TextAlign = HorizontalAlignment.Center;
            // 
            // btnTest
            // 
            btnTest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTest.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
            btnTest.Location = new Point(681, 316);
            btnTest.Margin = new Padding(4, 3, 4, 3);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(229, 83);
            btnTest.TabIndex = 2;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // txtTotalRequests
            // 
            txtTotalRequests.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTotalRequests.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            txtTotalRequests.Location = new Point(825, 25);
            txtTotalRequests.Margin = new Padding(4, 3, 4, 3);
            txtTotalRequests.Name = "txtTotalRequests";
            txtTotalRequests.Size = new Size(85, 35);
            txtTotalRequests.TabIndex = 4;
            txtTotalRequests.Text = "200";
            txtTotalRequests.TextAlign = HorizontalAlignment.Center;
            // 
            // txtBatchSize
            // 
            txtBatchSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBatchSize.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            txtBatchSize.Location = new Point(829, 70);
            txtBatchSize.Margin = new Padding(4, 3, 4, 3);
            txtBatchSize.Name = "txtBatchSize";
            txtBatchSize.Size = new Size(81, 35);
            txtBatchSize.TabIndex = 4;
            txtBatchSize.Text = "3";
            txtBatchSize.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label3.Location = new Point(681, 32);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(136, 20);
            label3.TabIndex = 5;
            label3.Text = "TotalRequests :";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label4.Location = new Point(700, 80);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(101, 20);
            label4.TabIndex = 5;
            label4.Text = "BatchSize :";
            // 
            // txtDelay
            // 
            txtDelay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDelay.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            txtDelay.Location = new Point(842, 114);
            txtDelay.Margin = new Padding(4, 3, 4, 3);
            txtDelay.Name = "txtDelay";
            txtDelay.Size = new Size(51, 35);
            txtDelay.TabIndex = 4;
            txtDelay.Text = "30";
            txtDelay.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            label5.Location = new Point(720, 124);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(64, 20);
            label5.TabIndex = 5;
            label5.Text = "Delay :";
            // 
            // chkAdd
            // 
            chkAdd.AutoSize = true;
            chkAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            chkAdd.Location = new Point(14, 458);
            chkAdd.Name = "chkAdd";
            chkAdd.Size = new Size(126, 25);
            chkAdd.TabIndex = 6;
            chkAdd.Text = "افزودن به قبلی";
            chkAdd.UseVisualStyleBackColor = true;
            // 
            // txtEnd
            // 
            txtEnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtEnd.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            txtEnd.ForeColor = SystemColors.WindowText;
            txtEnd.Location = new Point(738, 482);
            txtEnd.Margin = new Padding(4, 3, 4, 3);
            txtEnd.Name = "txtEnd";
            txtEnd.Size = new Size(136, 29);
            txtEnd.TabIndex = 3;
            txtEnd.Text = "08:45:00";
            txtEnd.TextAlign = HorizontalAlignment.Center;
            // 
            // txtStart
            // 
            txtStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtStart.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            txtStart.ForeColor = SystemColors.WindowText;
            txtStart.Location = new Point(738, 447);
            txtStart.Margin = new Padding(4, 3, 4, 3);
            txtStart.Name = "txtStart";
            txtStart.Size = new Size(136, 29);
            txtStart.TabIndex = 3;
            txtStart.Text = "08:44:55";
            txtStart.TextAlign = HorizontalAlignment.Center;
            // 
            // chkLog
            // 
            chkLog.AutoSize = true;
            chkLog.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            chkLog.Location = new Point(14, 489);
            chkLog.Name = "chkLog";
            chkLog.Size = new Size(53, 25);
            chkLog.TabIndex = 6;
            chkLog.Text = "لاگ";
            chkLog.UseVisualStyleBackColor = true;
            // 
            // cmbProvider
            // 
            cmbProvider.Font = new Font("Nazanin", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 178);
            cmbProvider.FormattingEnabled = true;
            cmbProvider.Location = new Point(427, 466);
            cmbProvider.Name = "cmbProvider";
            cmbProvider.Size = new Size(132, 38);
            cmbProvider.TabIndex = 7;
            cmbProvider.SelectedIndexChanged += cmbProvider_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 527);
            Controls.Add(cmbProvider);
            Controls.Add(chkLog);
            Controls.Add(chkAdd);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtDelay);
            Controls.Add(label3);
            Controls.Add(txtBatchSize);
            Controls.Add(txtTotalRequests);
            Controls.Add(txtStart);
            Controls.Add(txtEnd);
            Controls.Add(txtRequestTime);
            Controls.Add(btnTest);
            Controls.Add(btnSendRequest);
            Controls.Add(btnSaveData);
            Controls.Add(txtRequest);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.Button btnSaveData;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.TextBox txtRequestTime;
        private System.Windows.Forms.Button btnTest;
        private TextBox txtTotalRequests;
        private TextBox txtBatchSize;
        private Label label3;
        private Label label4;
        private TextBox txtDelay;
        private Label label5;
        private CheckBox chkAdd;
        private TextBox txtEnd;
        private TextBox txtStart;
        private CheckBox chkLog;
        private ComboBox cmbProvider;
    }
}

