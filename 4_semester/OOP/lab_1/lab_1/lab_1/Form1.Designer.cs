namespace lab_1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRoomWidth = new System.Windows.Forms.TextBox();
            this.txtRoomLength = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWaste = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMatLength = new System.Windows.Forms.TextBox();
            this.txtMatWidth = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTotalQuantity = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblResArreaFt2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblResAreaM2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRoomWidth
            // 
            this.txtRoomWidth.BackColor = System.Drawing.Color.BurlyWood;
            this.txtRoomWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomWidth.Location = new System.Drawing.Point(11, 61);
            this.txtRoomWidth.Name = "txtRoomWidth";
            this.txtRoomWidth.Size = new System.Drawing.Size(374, 28);
            this.txtRoomWidth.TabIndex = 0;
            // 
            // txtRoomLength
            // 
            this.txtRoomLength.BackColor = System.Drawing.Color.BurlyWood;
            this.txtRoomLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomLength.Location = new System.Drawing.Point(11, 134);
            this.txtRoomLength.Name = "txtRoomLength";
            this.txtRoomLength.Size = new System.Drawing.Size(374, 28);
            this.txtRoomLength.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(1259, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(175, 64);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ширина комнаты, м";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Длина комнаты, м";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRoomWidth);
            this.groupBox1.Controls.Add(this.txtRoomLength);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(29, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 180);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Комната";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtWaste);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtMatLength);
            this.groupBox2.Controls.Add(this.txtMatWidth);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(29, 301);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 250);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Материалы";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(6, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Запас на обрезку, %";
            // 
            // txtWaste
            // 
            this.txtWaste.BackColor = System.Drawing.Color.BurlyWood;
            this.txtWaste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWaste.Location = new System.Drawing.Point(11, 211);
            this.txtWaste.Name = "txtWaste";
            this.txtWaste.Size = new System.Drawing.Size(374, 28);
            this.txtWaste.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ширина материала, м";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Длина материала, м";
            // 
            // txtMatLength
            // 
            this.txtMatLength.BackColor = System.Drawing.Color.BurlyWood;
            this.txtMatLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatLength.Location = new System.Drawing.Point(11, 134);
            this.txtMatLength.Name = "txtMatLength";
            this.txtMatLength.Size = new System.Drawing.Size(374, 28);
            this.txtMatLength.TabIndex = 10;
            // 
            // txtMatWidth
            // 
            this.txtMatWidth.BackColor = System.Drawing.Color.BurlyWood;
            this.txtMatWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatWidth.Location = new System.Drawing.Point(11, 61);
            this.txtMatWidth.Name = "txtMatWidth";
            this.txtMatWidth.Size = new System.Drawing.Size(374, 28);
            this.txtMatWidth.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotalQuantity);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblResArreaFt2);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblResAreaM2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(809, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(430, 242);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Результат";
            // 
            // txtTotalQuantity
            // 
            this.txtTotalQuantity.AutoSize = true;
            this.txtTotalQuantity.Location = new System.Drawing.Point(7, 202);
            this.txtTotalQuantity.Name = "txtTotalQuantity";
            this.txtTotalQuantity.Size = new System.Drawing.Size(158, 28);
            this.txtTotalQuantity.TabIndex = 14;
            this.txtTotalQuantity.Text = "0 шт./рулонов";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(6, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(214, 25);
            this.label8.TabIndex = 13;
            this.label8.Text = "Итоговое количество";
            // 
            // lblResArreaFt2
            // 
            this.lblResArreaFt2.AutoSize = true;
            this.lblResArreaFt2.Location = new System.Drawing.Point(7, 129);
            this.lblResArreaFt2.Name = "lblResArreaFt2";
            this.lblResArreaFt2.Size = new System.Drawing.Size(51, 28);
            this.lblResArreaFt2.TabIndex = 12;
            this.lblResArreaFt2.Text = "0 ft²";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(6, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Футы²";
            // 
            // lblResAreaM2
            // 
            this.lblResAreaM2.AutoSize = true;
            this.lblResAreaM2.Location = new System.Drawing.Point(7, 55);
            this.lblResAreaM2.Name = "lblResAreaM2";
            this.lblResAreaM2.Size = new System.Drawing.Size(55, 28);
            this.lblResAreaM2.TabIndex = 10;
            this.lblResAreaM2.Text = "0 м²";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "м²";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculate.Location = new System.Drawing.Point(535, 267);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(169, 64);
            this.btnCalculate.TabIndex = 12;
            this.btnCalculate.Text = "рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1446, 654);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRoomWidth;
        private System.Windows.Forms.TextBox txtRoomLength;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMatLength;
        private System.Windows.Forms.TextBox txtMatWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWaste;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtTotalQuantity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblResArreaFt2;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblResAreaM2;
    }
}

