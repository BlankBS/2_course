namespace lab_2
{
    partial class SearchForm
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
            this.txtSearchCity = new System.Windows.Forms.TextBox();
            this.numMinArea = new System.Windows.Forms.NumericUpDown();
            this.btnDoSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMinArea)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearchCity
            // 
            this.txtSearchCity.Location = new System.Drawing.Point(186, 22);
            this.txtSearchCity.Name = "txtSearchCity";
            this.txtSearchCity.Size = new System.Drawing.Size(132, 22);
            this.txtSearchCity.TabIndex = 0;
            // 
            // numMinArea
            // 
            this.numMinArea.Location = new System.Drawing.Point(186, 123);
            this.numMinArea.Name = "numMinArea";
            this.numMinArea.Size = new System.Drawing.Size(120, 22);
            this.numMinArea.TabIndex = 1;
            // 
            // btnDoSearch
            // 
            this.btnDoSearch.Location = new System.Drawing.Point(315, 72);
            this.btnDoSearch.Name = "btnDoSearch";
            this.btnDoSearch.Size = new System.Drawing.Size(75, 23);
            this.btnDoSearch.TabIndex = 2;
            this.btnDoSearch.Text = "Найти";
            this.btnDoSearch.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Regex";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Минимальная площадь";
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 219);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDoSearch);
            this.Controls.Add(this.numMinArea);
            this.Controls.Add(this.txtSearchCity);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            ((System.ComponentModel.ISupportInitialize)(this.numMinArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchCity;
        private System.Windows.Forms.NumericUpDown numMinArea;
        private System.Windows.Forms.Button btnDoSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}