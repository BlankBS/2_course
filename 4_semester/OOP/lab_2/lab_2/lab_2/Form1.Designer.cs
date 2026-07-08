namespace lab_2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkBalcony = new System.Windows.Forms.CheckBox();
            this.chkBasement = new System.Windows.Forms.CheckBox();
            this.chkToilet = new System.Windows.Forms.CheckBox();
            this.chkBath = new System.Windows.Forms.CheckBox();
            this.chkKitchen = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpYearBuilt = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numRooms = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numFloor = new System.Windows.Forms.NumericUpDown();
            this.trackArea = new System.Windows.Forms.TrackBar();
            this.GB_areaAmount = new System.Windows.Forms.GroupBox();
            this.lblAreaValue = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbNoRepair = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.rbDesign = new System.Windows.Forms.RadioButton();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFlat = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtHouse = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDevName = new System.Windows.Forms.TextBox();
            this.cmbDevType = new System.Windows.Forms.ComboBox();
            this.txtDevAddress = new System.Windows.Forms.TextBox();
            this.txtDevTIN = new System.Windows.Forms.TextBox();
            this.BtnCalculate = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.listOutput = new System.Windows.Forms.ListBox();
            this.txtTIN = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackArea)).BeginInit();
            this.GB_areaAmount.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(215)))), ((int)(((byte)(141)))));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpYearBuilt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbMaterial);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numRooms);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numFloor);
            this.groupBox1.Controls.Add(this.trackArea);
            this.groupBox1.Controls.Add(this.GB_areaAmount);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 680);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Квартира";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkBalcony);
            this.groupBox2.Controls.Add(this.chkBasement);
            this.groupBox2.Controls.Add(this.chkToilet);
            this.groupBox2.Controls.Add(this.chkBath);
            this.groupBox2.Controls.Add(this.chkKitchen);
            this.groupBox2.Location = new System.Drawing.Point(17, 295);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // chkBalcony
            // 
            this.chkBalcony.AutoSize = true;
            this.chkBalcony.Location = new System.Drawing.Point(100, 61);
            this.chkBalcony.Name = "chkBalcony";
            this.chkBalcony.Size = new System.Drawing.Size(77, 20);
            this.chkBalcony.TabIndex = 4;
            this.chkBalcony.Text = "Балкон";
            this.chkBalcony.UseVisualStyleBackColor = true;
            // 
            // chkBasement
            // 
            this.chkBasement.AutoSize = true;
            this.chkBasement.Location = new System.Drawing.Point(100, 35);
            this.chkBasement.Name = "chkBasement";
            this.chkBasement.Size = new System.Drawing.Size(79, 20);
            this.chkBasement.TabIndex = 3;
            this.chkBasement.Text = "Подвал";
            this.chkBasement.UseVisualStyleBackColor = true;
            // 
            // chkToilet
            // 
            this.chkToilet.AutoSize = true;
            this.chkToilet.Location = new System.Drawing.Point(12, 74);
            this.chkToilet.Name = "chkToilet";
            this.chkToilet.Size = new System.Drawing.Size(77, 20);
            this.chkToilet.TabIndex = 2;
            this.chkToilet.Text = "Туалет";
            this.chkToilet.UseVisualStyleBackColor = true;
            // 
            // chkBath
            // 
            this.chkBath.AutoSize = true;
            this.chkBath.Location = new System.Drawing.Point(12, 48);
            this.chkBath.Name = "chkBath";
            this.chkBath.Size = new System.Drawing.Size(70, 20);
            this.chkBath.TabIndex = 1;
            this.chkBath.Text = "Ванна";
            this.chkBath.UseVisualStyleBackColor = true;
            // 
            // chkKitchen
            // 
            this.chkKitchen.AutoSize = true;
            this.chkKitchen.Location = new System.Drawing.Point(12, 22);
            this.chkKitchen.Name = "chkKitchen";
            this.chkKitchen.Size = new System.Drawing.Size(66, 20);
            this.chkKitchen.TabIndex = 0;
            this.chkKitchen.Text = "Кухня";
            this.chkKitchen.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this.label5.Location = new System.Drawing.Point(273, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 32);
            this.label5.TabIndex = 9;
            this.label5.Text = "Год дома";
            // 
            // dtpYearBuilt
            // 
            this.dtpYearBuilt.Location = new System.Drawing.Point(203, 245);
            this.dtpYearBuilt.Name = "dtpYearBuilt";
            this.dtpYearBuilt.Size = new System.Drawing.Size(247, 22);
            this.dtpYearBuilt.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this.label4.Location = new System.Drawing.Point(6, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Материал дома";
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Items.AddRange(new object[] {
            "Кирпич",
            "Панель",
            "Монолит"});
            this.cmbMaterial.Location = new System.Drawing.Point(29, 243);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(121, 24);
            this.cmbMaterial.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this.label3.Location = new System.Drawing.Point(253, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Комнат";
            // 
            // numRooms
            // 
            this.numRooms.Location = new System.Drawing.Point(250, 157);
            this.numRooms.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numRooms.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRooms.Name = "numRooms";
            this.numRooms.Size = new System.Drawing.Size(120, 22);
            this.numRooms.TabIndex = 5;
            this.numRooms.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this.label1.Location = new System.Drawing.Point(38, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Этаж";
            // 
            // numFloor
            // 
            this.numFloor.Location = new System.Drawing.Point(17, 157);
            this.numFloor.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFloor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFloor.Name = "numFloor";
            this.numFloor.Size = new System.Drawing.Size(120, 22);
            this.numFloor.TabIndex = 3;
            this.numFloor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // trackArea
            // 
            this.trackArea.Location = new System.Drawing.Point(6, 73);
            this.trackArea.Maximum = 300;
            this.trackArea.Minimum = 10;
            this.trackArea.Name = "trackArea";
            this.trackArea.Size = new System.Drawing.Size(444, 56);
            this.trackArea.TabIndex = 2;
            this.trackArea.Value = 10;
            // 
            // GB_areaAmount
            // 
            this.GB_areaAmount.Controls.Add(this.lblAreaValue);
            this.GB_areaAmount.Location = new System.Drawing.Point(117, 10);
            this.GB_areaAmount.Name = "GB_areaAmount";
            this.GB_areaAmount.Size = new System.Drawing.Size(206, 57);
            this.GB_areaAmount.TabIndex = 2;
            this.GB_areaAmount.TabStop = false;
            // 
            // lblAreaValue
            // 
            this.lblAreaValue.AutoSize = true;
            this.lblAreaValue.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this.lblAreaValue.Location = new System.Drawing.Point(6, 18);
            this.lblAreaValue.Name = "lblAreaValue";
            this.lblAreaValue.Size = new System.Drawing.Size(40, 32);
            this.lblAreaValue.TabIndex = 1;
            this.lblAreaValue.Text = "10";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDesign);
            this.groupBox3.Controls.Add(this.rbStandard);
            this.groupBox3.Controls.Add(this.rbNoRepair);
            this.groupBox3.Location = new System.Drawing.Point(250, 295);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // rbNoRepair
            // 
            this.rbNoRepair.AutoSize = true;
            this.rbNoRepair.Location = new System.Drawing.Point(6, 22);
            this.rbNoRepair.Name = "rbNoRepair";
            this.rbNoRepair.Size = new System.Drawing.Size(112, 20);
            this.rbNoRepair.TabIndex = 0;
            this.rbNoRepair.TabStop = true;
            this.rbNoRepair.Text = "Без ремонта";
            this.rbNoRepair.UseVisualStyleBackColor = true;
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Location = new System.Drawing.Point(6, 47);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(114, 20);
            this.rbStandard.TabIndex = 1;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Стандартная";
            this.rbStandard.UseVisualStyleBackColor = true;
            // 
            // rbDesign
            // 
            this.rbDesign.AutoSize = true;
            this.rbDesign.Location = new System.Drawing.Point(6, 73);
            this.rbDesign.Name = "rbDesign";
            this.rbDesign.Size = new System.Drawing.Size(77, 20);
            this.rbDesign.TabIndex = 2;
            this.rbDesign.TabStop = true;
            this.rbDesign.Text = "Дизайн";
            this.rbDesign.UseVisualStyleBackColor = true;
            // 
            // txtCountry
            // 
            this.txtCountry.Location = new System.Drawing.Point(176, 17);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(172, 22);
            this.txtCountry.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label6.Location = new System.Drawing.Point(6, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 30);
            this.label6.TabIndex = 13;
            this.label6.Text = "Страна";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label7.Location = new System.Drawing.Point(6, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 30);
            this.label7.TabIndex = 15;
            this.label7.Text = "Город";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(176, 47);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(172, 22);
            this.txtCity.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label8.Location = new System.Drawing.Point(7, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 30);
            this.label8.TabIndex = 17;
            this.label8.Text = "Район";
            // 
            // txtDistrict
            // 
            this.txtDistrict.Location = new System.Drawing.Point(177, 77);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(172, 22);
            this.txtDistrict.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label9.Location = new System.Drawing.Point(7, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 30);
            this.label9.TabIndex = 23;
            this.label9.Text = "Квартира";
            // 
            // txtFlat
            // 
            this.txtFlat.Location = new System.Drawing.Point(177, 165);
            this.txtFlat.Name = "txtFlat";
            this.txtFlat.Size = new System.Drawing.Size(172, 22);
            this.txtFlat.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label10.Location = new System.Drawing.Point(6, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 30);
            this.label10.TabIndex = 21;
            this.label10.Text = "Дом";
            // 
            // txtHouse
            // 
            this.txtHouse.Location = new System.Drawing.Point(176, 135);
            this.txtHouse.Name = "txtHouse";
            this.txtHouse.Size = new System.Drawing.Size(172, 22);
            this.txtHouse.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label11.Location = new System.Drawing.Point(6, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 30);
            this.label11.TabIndex = 19;
            this.label11.Text = "Улица";
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(176, 105);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(172, 22);
            this.txtStreet.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(215)))), ((int)(((byte)(141)))));
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Location = new System.Drawing.Point(486, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(456, 240);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Адрес";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.txtFlat);
            this.groupBox7.Controls.Add(this.txtCountry);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.txtCity);
            this.groupBox7.Controls.Add(this.txtHouse);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.txtDistrict);
            this.groupBox7.Controls.Add(this.txtStreet);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Location = new System.Drawing.Point(15, 28);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(363, 196);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(215)))), ((int)(((byte)(141)))));
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(486, 267);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(456, 240);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Застройщик";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.txtTIN);
            this.groupBox6.Controls.Add(this.txtDevTIN);
            this.groupBox6.Controls.Add(this.txtDevAddress);
            this.groupBox6.Controls.Add(this.cmbDevType);
            this.groupBox6.Controls.Add(this.txtDevName);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Location = new System.Drawing.Point(15, 28);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(363, 196);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label13.Location = new System.Drawing.Point(6, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 30);
            this.label13.TabIndex = 13;
            this.label13.Text = "Название";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label15.Location = new System.Drawing.Point(6, 39);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 30);
            this.label15.TabIndex = 15;
            this.label15.Text = "Компания";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label16.Location = new System.Drawing.Point(6, 97);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 30);
            this.label16.TabIndex = 19;
            this.label16.Text = "Улица";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label17.Location = new System.Drawing.Point(7, 69);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 30);
            this.label17.TabIndex = 17;
            this.label17.Text = "Юр. адрес";
            // 
            // txtDevName
            // 
            this.txtDevName.Location = new System.Drawing.Point(176, 12);
            this.txtDevName.Name = "txtDevName";
            this.txtDevName.Size = new System.Drawing.Size(187, 22);
            this.txtDevName.TabIndex = 20;
            // 
            // cmbDevType
            // 
            this.cmbDevType.FormattingEnabled = true;
            this.cmbDevType.Items.AddRange(new object[] {
            "ООО",
            "ЗАО",
            "ОАО",
            "ИП"});
            this.cmbDevType.Location = new System.Drawing.Point(176, 40);
            this.cmbDevType.Name = "cmbDevType";
            this.cmbDevType.Size = new System.Drawing.Size(187, 24);
            this.cmbDevType.TabIndex = 21;
            // 
            // txtDevAddress
            // 
            this.txtDevAddress.Location = new System.Drawing.Point(176, 69);
            this.txtDevAddress.Name = "txtDevAddress";
            this.txtDevAddress.Size = new System.Drawing.Size(187, 22);
            this.txtDevAddress.TabIndex = 22;
            // 
            // txtDevTIN
            // 
            this.txtDevTIN.Location = new System.Drawing.Point(176, 97);
            this.txtDevTIN.Name = "txtDevTIN";
            this.txtDevTIN.Size = new System.Drawing.Size(187, 22);
            this.txtDevTIN.TabIndex = 23;
            // 
            // BtnCalculate
            // 
            this.BtnCalculate.Location = new System.Drawing.Point(970, 83);
            this.BtnCalculate.Name = "BtnCalculate";
            this.BtnCalculate.Size = new System.Drawing.Size(106, 96);
            this.BtnCalculate.TabIndex = 26;
            this.BtnCalculate.Text = "Рассчитать стоимость";
            this.BtnCalculate.UseVisualStyleBackColor = true;
            this.BtnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(970, 218);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(106, 96);
            this.BtnSave.TabIndex = 27;
            this.BtnSave.Text = "Сохранить";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(970, 348);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(106, 96);
            this.BtnLoad.TabIndex = 28;
            this.BtnLoad.Text = "Загрузить из файла";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // listOutput
            // 
            this.listOutput.FormattingEnabled = true;
            this.listOutput.ItemHeight = 16;
            this.listOutput.Location = new System.Drawing.Point(1101, 22);
            this.listOutput.Name = "listOutput";
            this.listOutput.Size = new System.Drawing.Size(426, 468);
            this.listOutput.TabIndex = 29;
            // 
            // txtTIN
            // 
            this.txtTIN.Location = new System.Drawing.Point(176, 125);
            this.txtTIN.Name = "txtTIN";
            this.txtTIN.Size = new System.Drawing.Size(187, 22);
            this.txtTIN.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12.2F);
            this.label12.Location = new System.Drawing.Point(7, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 30);
            this.label12.TabIndex = 25;
            this.label12.Text = "ИНН";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 740);
            this.Controls.Add(this.listOutput);
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnCalculate);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackArea)).EndInit();
            this.GB_areaAmount.ResumeLayout(false);
            this.GB_areaAmount.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox GB_areaAmount;
        private System.Windows.Forms.Label lblAreaValue;
        private System.Windows.Forms.TrackBar trackArea;
        private System.Windows.Forms.NumericUpDown numFloor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpYearBuilt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numRooms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkBalcony;
        private System.Windows.Forms.CheckBox chkBasement;
        private System.Windows.Forms.CheckBox chkToilet;
        private System.Windows.Forms.CheckBox chkBath;
        private System.Windows.Forms.CheckBox chkKitchen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbDesign;
        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.RadioButton rbNoRepair;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFlat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtHouse;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbDevType;
        private System.Windows.Forms.TextBox txtDevName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDevTIN;
        private System.Windows.Forms.TextBox txtDevAddress;
        private System.Windows.Forms.Button BtnCalculate;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.ListBox listOutput;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTIN;
    }
}

