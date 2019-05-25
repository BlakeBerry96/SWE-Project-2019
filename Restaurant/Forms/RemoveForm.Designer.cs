namespace Restaurant {
    partial class RemoveForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoveForm));
            this.label1 = new System.Windows.Forms.Label();
            this.Header_Label = new System.Windows.Forms.Label();
            this.Employee_Box = new System.Windows.Forms.ComboBox();
            this.Confirm_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Username";
            // 
            // Header_Label
            // 
            this.Header_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header_Label.Location = new System.Drawing.Point(12, 30);
            this.Header_Label.Name = "Header_Label";
            this.Header_Label.Size = new System.Drawing.Size(360, 37);
            this.Header_Label.TabIndex = 7;
            this.Header_Label.Text = "Remove";
            this.Header_Label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Employee_Box
            // 
            this.Employee_Box.DropDownHeight = 200;
            this.Employee_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Employee_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Employee_Box.FormattingEnabled = true;
            this.Employee_Box.IntegralHeight = false;
            this.Employee_Box.Location = new System.Drawing.Point(19, 110);
            this.Employee_Box.Name = "Employee_Box";
            this.Employee_Box.Size = new System.Drawing.Size(353, 28);
            this.Employee_Box.TabIndex = 6;
            // 
            // Confirm_Button
            // 
            this.Confirm_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Confirm_Button.Location = new System.Drawing.Point(272, 319);
            this.Confirm_Button.Name = "Confirm_Button";
            this.Confirm_Button.Size = new System.Drawing.Size(100, 30);
            this.Confirm_Button.TabIndex = 9;
            this.Confirm_Button.Text = "Confirm";
            this.Confirm_Button.UseVisualStyleBackColor = true;
            this.Confirm_Button.Click += new System.EventHandler(this.confirmClick);
            // 
            // Remove_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.Confirm_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Header_Label);
            this.Controls.Add(this.Employee_Box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Remove_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.formLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Header_Label;
        private System.Windows.Forms.ComboBox Employee_Box;
        private System.Windows.Forms.Button Confirm_Button;
    }
}