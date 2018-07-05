namespace HHCallTools
{
    partial class FormCall
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lable4 = new System.Windows.Forms.Label();
            this.lable5 = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDayCalls = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAssignto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "LoginName:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(248, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // txtUserId
            // 
            this.txtUserId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserId.Font = new System.Drawing.Font("新細明體", 11F);
            this.txtUserId.Location = new System.Drawing.Point(104, 17);
            this.txtUserId.MaxLength = 20;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(100, 25);
            this.txtUserId.TabIndex = 2;
            this.txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserId_KeyPress);
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("新細明體", 11F);
            this.txtPwd.Location = new System.Drawing.Point(325, 15);
            this.txtPwd.MaxLength = 30;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(99, 25);
            this.txtPwd.TabIndex = 3;
            // 
            // lable4
            // 
            this.lable4.AutoSize = true;
            this.lable4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lable4.Location = new System.Drawing.Point(6, 37);
            this.lable4.Name = "lable4";
            this.lable4.Size = new System.Drawing.Size(69, 16);
            this.lable4.TabIndex = 4;
            this.lable4.Text = "StartDate:";
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lable5.Location = new System.Drawing.Point(236, 37);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(66, 16);
            this.lable5.TabIndex = 5;
            this.lable5.Text = "EndDate:";
            // 
            // txtStartDate
            // 
            this.txtStartDate.Font = new System.Drawing.Font("新細明體", 11F);
            this.txtStartDate.Location = new System.Drawing.Point(92, 28);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(100, 25);
            this.txtStartDate.TabIndex = 6;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Font = new System.Drawing.Font("新細明體", 11F);
            this.txtEndDate.Location = new System.Drawing.Point(313, 28);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(99, 25);
            this.txtEndDate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "DayCalls:";
            // 
            // txtDayCalls
            // 
            this.txtDayCalls.Font = new System.Drawing.Font("新細明體", 11F);
            this.txtDayCalls.Location = new System.Drawing.Point(92, 87);
            this.txtDayCalls.MaxLength = 5;
            this.txtDayCalls.Name = "txtDayCalls";
            this.txtDayCalls.Size = new System.Drawing.Size(30, 25);
            this.txtDayCalls.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAssignto);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lable4);
            this.groupBox1.Controls.Add(this.txtDayCalls);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEndDate);
            this.groupBox1.Controls.Add(this.txtStartDate);
            this.groupBox1.Controls.Add(this.lable5);
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 131);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // txtAssignto
            // 
            this.txtAssignto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAssignto.Font = new System.Drawing.Font("新細明體", 11F);
            this.txtAssignto.Location = new System.Drawing.Point(313, 87);
            this.txtAssignto.Name = "txtAssignto";
            this.txtAssignto.Size = new System.Drawing.Size(99, 25);
            this.txtAssignto.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F);
            this.label6.Location = new System.Drawing.Point(236, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Assignto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "d/m/y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "d/m/y";
            // 
            // btnAccept
            // 
            this.btnAccept.Font = new System.Drawing.Font("新細明體", 11F);
            this.btnAccept.Location = new System.Drawing.Point(189, 208);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(91, 26);
            this.btnAccept.TabIndex = 11;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("新細明體", 11F);
            this.btnCancel.Location = new System.Drawing.Point(363, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 26);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 246);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCall";
            this.Text = "HHCallTools v1.0";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lable4;
        private System.Windows.Forms.Label lable5;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDayCalls;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAssignto;
        private System.Windows.Forms.Label label6;
    }
}