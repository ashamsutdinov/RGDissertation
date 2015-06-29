namespace RGLines
{
    partial class RGLinesDirect
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.rgParamsGroup = new System.Windows.Forms.GroupBox();
            this.bnApplyRSettings = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAcc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlpha = new System.Windows.Forms.TextBox();
            this.txtN = new System.Windows.Forms.TextBox();
            this.lineParamsGroup = new System.Windows.Forms.GroupBox();
            this.cbShowTransformedLine = new System.Windows.Forms.CheckBox();
            this.btnHideLine = new System.Windows.Forms.Button();
            this.btnAppyLineSettings = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRStep = new System.Windows.Forms.TextBox();
            this.btnDefB = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRMax = new System.Windows.Forms.TextBox();
            this.txtRMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLineB = new System.Windows.Forms.TextBox();
            this.txtLineA = new System.Windows.Forms.TextBox();
            this.trackLine = new System.Windows.Forms.TrackBar();
            this.txtStepR = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.trackLineGroup = new System.Windows.Forms.GroupBox();
            this.areaParamsGroup = new System.Windows.Forms.GroupBox();
            this.checkBoxOnlyArc = new System.Windows.Forms.CheckBox();
            this.btnHideArea = new System.Windows.Forms.Button();
            this.btnApplyArea = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAreaH1 = new System.Windows.Forms.TextBox();
            this.trackAreaGroup = new System.Windows.Forms.GroupBox();
            this.txtAreaIteration = new System.Windows.Forms.TextBox();
            this.trackArea = new System.Windows.Forms.TrackBar();
            this.btnAreaNextIteration = new System.Windows.Forms.Button();
            this.btnAreaPrevIteration = new System.Windows.Forms.Button();
            this.txtAreaH2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.rgParamsGroup.SuspendLayout();
            this.lineParamsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackLine)).BeginInit();
            this.trackLineGroup.SuspendLayout();
            this.areaParamsGroup.SuspendLayout();
            this.trackAreaGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackArea)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(16, 15);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(907, 837);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // rgParamsGroup
            // 
            this.rgParamsGroup.Controls.Add(this.bnApplyRSettings);
            this.rgParamsGroup.Controls.Add(this.label3);
            this.rgParamsGroup.Controls.Add(this.txtAcc);
            this.rgParamsGroup.Controls.Add(this.label2);
            this.rgParamsGroup.Controls.Add(this.label1);
            this.rgParamsGroup.Controls.Add(this.txtAlpha);
            this.rgParamsGroup.Controls.Add(this.txtN);
            this.rgParamsGroup.Location = new System.Drawing.Point(973, 15);
            this.rgParamsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rgParamsGroup.Name = "rgParamsGroup";
            this.rgParamsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rgParamsGroup.Size = new System.Drawing.Size(363, 121);
            this.rgParamsGroup.TabIndex = 1;
            this.rgParamsGroup.TabStop = false;
            this.rgParamsGroup.Text = "RG Params";
            // 
            // bnApplyRSettings
            // 
            this.bnApplyRSettings.Location = new System.Drawing.Point(165, 85);
            this.bnApplyRSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bnApplyRSettings.Name = "bnApplyRSettings";
            this.bnApplyRSettings.Size = new System.Drawing.Size(195, 28);
            this.bnApplyRSettings.TabIndex = 6;
            this.bnApplyRSettings.Text = "Apply";
            this.bnApplyRSettings.UseVisualStyleBackColor = true;
            this.bnApplyRSettings.Click += new System.EventHandler(this.BnApplyClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Acc";
            // 
            // txtAcc
            // 
            this.txtAcc.Location = new System.Drawing.Point(75, 87);
            this.txtAcc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAcc.Name = "txtAcc";
            this.txtAcc.Size = new System.Drawing.Size(81, 22);
            this.txtAcc.TabIndex = 4;
            this.txtAcc.Text = "0,00000001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Alpha";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "N";
            // 
            // txtAlpha
            // 
            this.txtAlpha.Location = new System.Drawing.Point(75, 55);
            this.txtAlpha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAlpha.Name = "txtAlpha";
            this.txtAlpha.Size = new System.Drawing.Size(81, 22);
            this.txtAlpha.TabIndex = 1;
            this.txtAlpha.Text = "1,7";
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(75, 23);
            this.txtN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(81, 22);
            this.txtN.TabIndex = 0;
            this.txtN.Text = "2";
            // 
            // lineParamsGroup
            // 
            this.lineParamsGroup.Controls.Add(this.cbShowTransformedLine);
            this.lineParamsGroup.Controls.Add(this.btnHideLine);
            this.lineParamsGroup.Controls.Add(this.btnAppyLineSettings);
            this.lineParamsGroup.Controls.Add(this.label8);
            this.lineParamsGroup.Controls.Add(this.txtRStep);
            this.lineParamsGroup.Controls.Add(this.btnDefB);
            this.lineParamsGroup.Controls.Add(this.label7);
            this.lineParamsGroup.Controls.Add(this.label4);
            this.lineParamsGroup.Controls.Add(this.txtRMax);
            this.lineParamsGroup.Controls.Add(this.txtRMin);
            this.lineParamsGroup.Controls.Add(this.label5);
            this.lineParamsGroup.Controls.Add(this.label6);
            this.lineParamsGroup.Controls.Add(this.txtLineB);
            this.lineParamsGroup.Controls.Add(this.txtLineA);
            this.lineParamsGroup.Location = new System.Drawing.Point(973, 143);
            this.lineParamsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lineParamsGroup.Name = "lineParamsGroup";
            this.lineParamsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lineParamsGroup.Size = new System.Drawing.Size(363, 204);
            this.lineParamsGroup.TabIndex = 7;
            this.lineParamsGroup.TabStop = false;
            this.lineParamsGroup.Text = "Line Params";
            // 
            // cbShowTransformedLine
            // 
            this.cbShowTransformedLine.AutoSize = true;
            this.cbShowTransformedLine.Location = new System.Drawing.Point(165, 98);
            this.cbShowTransformedLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbShowTransformedLine.Name = "cbShowTransformedLine";
            this.cbShowTransformedLine.Size = new System.Drawing.Size(111, 21);
            this.cbShowTransformedLine.TabIndex = 13;
            this.cbShowTransformedLine.Text = "Transformed";
            this.cbShowTransformedLine.UseVisualStyleBackColor = true;
            // 
            // btnHideLine
            // 
            this.btnHideLine.Location = new System.Drawing.Point(165, 127);
            this.btnHideLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHideLine.Name = "btnHideLine";
            this.btnHideLine.Size = new System.Drawing.Size(189, 28);
            this.btnHideLine.TabIndex = 12;
            this.btnHideLine.Text = "Hide";
            this.btnHideLine.UseVisualStyleBackColor = true;
            this.btnHideLine.Click += new System.EventHandler(this.BtnHideLineClick);
            // 
            // btnAppyLineSettings
            // 
            this.btnAppyLineSettings.Location = new System.Drawing.Point(165, 162);
            this.btnAppyLineSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAppyLineSettings.Name = "btnAppyLineSettings";
            this.btnAppyLineSettings.Size = new System.Drawing.Size(189, 28);
            this.btnAppyLineSettings.TabIndex = 7;
            this.btnAppyLineSettings.Text = "Apply";
            this.btnAppyLineSettings.UseVisualStyleBackColor = true;
            this.btnAppyLineSettings.Click += new System.EventHandler(this.BtnAppyLineSettingsClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 166);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "RStep";
            // 
            // txtRStep
            // 
            this.txtRStep.Location = new System.Drawing.Point(75, 162);
            this.txtRStep.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRStep.Name = "txtRStep";
            this.txtRStep.Size = new System.Drawing.Size(81, 22);
            this.txtRStep.TabIndex = 9;
            this.txtRStep.Text = "0,001";
            // 
            // btnDefB
            // 
            this.btnDefB.Location = new System.Drawing.Point(165, 52);
            this.btnDefB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDefB.Name = "btnDefB";
            this.btnDefB.Size = new System.Drawing.Size(189, 28);
            this.btnDefB.TabIndex = 8;
            this.btnDefB.Text = "Default \'B\' value";
            this.btnDefB.UseVisualStyleBackColor = true;
            this.btnDefB.Click += new System.EventHandler(this.BtnDefBClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 134);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "RMax";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 107);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "RMin";
            // 
            // txtRMax
            // 
            this.txtRMax.Location = new System.Drawing.Point(75, 130);
            this.txtRMax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRMax.Name = "txtRMax";
            this.txtRMax.Size = new System.Drawing.Size(81, 22);
            this.txtRMax.TabIndex = 5;
            this.txtRMax.Text = "20";
            // 
            // txtRMin
            // 
            this.txtRMin.Location = new System.Drawing.Point(75, 98);
            this.txtRMin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRMin.Name = "txtRMin";
            this.txtRMin.Size = new System.Drawing.Size(81, 22);
            this.txtRMin.TabIndex = 4;
            this.txtRMin.Text = "-20";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "A";
            // 
            // txtLineB
            // 
            this.txtLineB.Location = new System.Drawing.Point(75, 55);
            this.txtLineB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLineB.Name = "txtLineB";
            this.txtLineB.Size = new System.Drawing.Size(81, 22);
            this.txtLineB.TabIndex = 1;
            this.txtLineB.Text = "0";
            // 
            // txtLineA
            // 
            this.txtLineA.Location = new System.Drawing.Point(75, 23);
            this.txtLineA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLineA.Name = "txtLineA";
            this.txtLineA.Size = new System.Drawing.Size(81, 22);
            this.txtLineA.TabIndex = 0;
            this.txtLineA.Text = "0";
            // 
            // trackLine
            // 
            this.trackLine.Location = new System.Drawing.Point(12, 59);
            this.trackLine.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackLine.Name = "trackLine";
            this.trackLine.Size = new System.Drawing.Size(343, 56);
            this.trackLine.TabIndex = 8;
            this.trackLine.ValueChanged += new System.EventHandler(this.TrackLineValueChanged);
            // 
            // txtStepR
            // 
            this.txtStepR.Enabled = false;
            this.txtStepR.Location = new System.Drawing.Point(12, 27);
            this.txtStepR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStepR.Name = "txtStepR";
            this.txtStepR.Size = new System.Drawing.Size(192, 22);
            this.txtStepR.TabIndex = 9;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(213, 23);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(67, 28);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "<-";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBackClick);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(288, 23);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(67, 28);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "->";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNextClick);
            // 
            // trackLineGroup
            // 
            this.trackLineGroup.Controls.Add(this.txtStepR);
            this.trackLineGroup.Controls.Add(this.trackLine);
            this.trackLineGroup.Controls.Add(this.btnNext);
            this.trackLineGroup.Controls.Add(this.btnBack);
            this.trackLineGroup.Location = new System.Drawing.Point(973, 354);
            this.trackLineGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackLineGroup.Name = "trackLineGroup";
            this.trackLineGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackLineGroup.Size = new System.Drawing.Size(363, 123);
            this.trackLineGroup.TabIndex = 12;
            this.trackLineGroup.TabStop = false;
            this.trackLineGroup.Text = "Track Line";
            // 
            // areaParamsGroup
            // 
            this.areaParamsGroup.Controls.Add(this.txtAreaH2);
            this.areaParamsGroup.Controls.Add(this.checkBoxOnlyArc);
            this.areaParamsGroup.Controls.Add(this.btnHideArea);
            this.areaParamsGroup.Controls.Add(this.btnApplyArea);
            this.areaParamsGroup.Controls.Add(this.label11);
            this.areaParamsGroup.Controls.Add(this.txtAreaH1);
            this.areaParamsGroup.Location = new System.Drawing.Point(973, 585);
            this.areaParamsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.areaParamsGroup.Name = "areaParamsGroup";
            this.areaParamsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.areaParamsGroup.Size = new System.Drawing.Size(363, 134);
            this.areaParamsGroup.TabIndex = 13;
            this.areaParamsGroup.TabStop = false;
            this.areaParamsGroup.Text = "Area Params";
            // 
            // checkBoxOnlyArc
            // 
            this.checkBoxOnlyArc.AutoSize = true;
            this.checkBoxOnlyArc.Location = new System.Drawing.Point(75, 53);
            this.checkBoxOnlyArc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxOnlyArc.Name = "checkBoxOnlyArc";
            this.checkBoxOnlyArc.Size = new System.Drawing.Size(51, 21);
            this.checkBoxOnlyArc.TabIndex = 13;
            this.checkBoxOnlyArc.Text = "Arc";
            this.checkBoxOnlyArc.UseVisualStyleBackColor = true;
            // 
            // btnHideArea
            // 
            this.btnHideArea.Location = new System.Drawing.Point(8, 98);
            this.btnHideArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHideArea.Name = "btnHideArea";
            this.btnHideArea.Size = new System.Drawing.Size(67, 28);
            this.btnHideArea.TabIndex = 12;
            this.btnHideArea.Text = "Hide";
            this.btnHideArea.UseVisualStyleBackColor = true;
            this.btnHideArea.Click += new System.EventHandler(this.BtnHideAreaClick);
            // 
            // btnApplyArea
            // 
            this.btnApplyArea.Location = new System.Drawing.Point(75, 98);
            this.btnApplyArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApplyArea.Name = "btnApplyArea";
            this.btnApplyArea.Size = new System.Drawing.Size(280, 28);
            this.btnApplyArea.TabIndex = 7;
            this.btnApplyArea.Text = "Apply";
            this.btnApplyArea.UseVisualStyleBackColor = true;
            this.btnApplyArea.Click += new System.EventHandler(this.BtnApplyAreaClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 23);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 17);
            this.label11.TabIndex = 6;
            this.label11.Text = "H";
            // 
            // txtAreaH1
            // 
            this.txtAreaH1.Location = new System.Drawing.Point(75, 20);
            this.txtAreaH1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAreaH1.Name = "txtAreaH1";
            this.txtAreaH1.Size = new System.Drawing.Size(129, 22);
            this.txtAreaH1.TabIndex = 4;
            this.txtAreaH1.Text = "0";
            // 
            // trackAreaGroup
            // 
            this.trackAreaGroup.Controls.Add(this.txtAreaIteration);
            this.trackAreaGroup.Controls.Add(this.trackArea);
            this.trackAreaGroup.Controls.Add(this.btnAreaNextIteration);
            this.trackAreaGroup.Controls.Add(this.btnAreaPrevIteration);
            this.trackAreaGroup.Location = new System.Drawing.Point(971, 726);
            this.trackAreaGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackAreaGroup.Name = "trackAreaGroup";
            this.trackAreaGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackAreaGroup.Size = new System.Drawing.Size(363, 123);
            this.trackAreaGroup.TabIndex = 13;
            this.trackAreaGroup.TabStop = false;
            this.trackAreaGroup.Text = "Track Area";
            // 
            // txtAreaIteration
            // 
            this.txtAreaIteration.Enabled = false;
            this.txtAreaIteration.Location = new System.Drawing.Point(12, 27);
            this.txtAreaIteration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAreaIteration.Name = "txtAreaIteration";
            this.txtAreaIteration.Size = new System.Drawing.Size(192, 22);
            this.txtAreaIteration.TabIndex = 9;
            this.txtAreaIteration.Text = "0";
            // 
            // trackArea
            // 
            this.trackArea.Location = new System.Drawing.Point(12, 59);
            this.trackArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackArea.Maximum = 5;
            this.trackArea.Name = "trackArea";
            this.trackArea.Size = new System.Drawing.Size(343, 56);
            this.trackArea.TabIndex = 8;
            this.trackArea.ValueChanged += new System.EventHandler(this.TrackAreaValueChanged);
            // 
            // btnAreaNextIteration
            // 
            this.btnAreaNextIteration.Location = new System.Drawing.Point(288, 23);
            this.btnAreaNextIteration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAreaNextIteration.Name = "btnAreaNextIteration";
            this.btnAreaNextIteration.Size = new System.Drawing.Size(67, 28);
            this.btnAreaNextIteration.TabIndex = 11;
            this.btnAreaNextIteration.Text = "->";
            this.btnAreaNextIteration.UseVisualStyleBackColor = true;
            this.btnAreaNextIteration.Click += new System.EventHandler(this.BtnAreaNextIterationClick);
            // 
            // btnAreaPrevIteration
            // 
            this.btnAreaPrevIteration.Location = new System.Drawing.Point(213, 23);
            this.btnAreaPrevIteration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAreaPrevIteration.Name = "btnAreaPrevIteration";
            this.btnAreaPrevIteration.Size = new System.Drawing.Size(67, 28);
            this.btnAreaPrevIteration.TabIndex = 10;
            this.btnAreaPrevIteration.Text = "<-";
            this.btnAreaPrevIteration.UseVisualStyleBackColor = true;
            this.btnAreaPrevIteration.Click += new System.EventHandler(this.BtnAreaPrevIterationClick);
            // 
            // txtAreaH2
            // 
            this.txtAreaH2.Location = new System.Drawing.Point(213, 20);
            this.txtAreaH2.Margin = new System.Windows.Forms.Padding(4);
            this.txtAreaH2.Name = "txtAreaH2";
            this.txtAreaH2.Size = new System.Drawing.Size(129, 22);
            this.txtAreaH2.TabIndex = 14;
            this.txtAreaH2.Text = "0";
            // 
            // RGLinesDirect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 864);
            this.Controls.Add(this.trackAreaGroup);
            this.Controls.Add(this.areaParamsGroup);
            this.Controls.Add(this.trackLineGroup);
            this.Controls.Add(this.lineParamsGroup);
            this.Controls.Add(this.rgParamsGroup);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "RGLinesDirect";
            this.Text = "RG Lines";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.rgParamsGroup.ResumeLayout(false);
            this.rgParamsGroup.PerformLayout();
            this.lineParamsGroup.ResumeLayout(false);
            this.lineParamsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackLine)).EndInit();
            this.trackLineGroup.ResumeLayout(false);
            this.trackLineGroup.PerformLayout();
            this.areaParamsGroup.ResumeLayout(false);
            this.areaParamsGroup.PerformLayout();
            this.trackAreaGroup.ResumeLayout(false);
            this.trackAreaGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.GroupBox rgParamsGroup;
        private System.Windows.Forms.Button bnApplyRSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.GroupBox lineParamsGroup;
        private System.Windows.Forms.Button btnDefB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRMax;
        private System.Windows.Forms.TextBox txtRMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLineB;
        private System.Windows.Forms.TextBox txtLineA;
        private System.Windows.Forms.Button btnAppyLineSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRStep;
        private System.Windows.Forms.TrackBar trackLine;
        private System.Windows.Forms.TextBox txtStepR;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox trackLineGroup;
        private System.Windows.Forms.Button btnHideLine;
        private System.Windows.Forms.GroupBox areaParamsGroup;
        private System.Windows.Forms.Button btnHideArea;
        private System.Windows.Forms.Button btnApplyArea;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAreaH1;
        private System.Windows.Forms.GroupBox trackAreaGroup;
        private System.Windows.Forms.TextBox txtAreaIteration;
        private System.Windows.Forms.TrackBar trackArea;
        private System.Windows.Forms.Button btnAreaNextIteration;
        private System.Windows.Forms.Button btnAreaPrevIteration;
        private System.Windows.Forms.CheckBox checkBoxOnlyArc;
        private System.Windows.Forms.CheckBox cbShowTransformedLine;
        private System.Windows.Forms.TextBox txtAreaH2;
    }
}

