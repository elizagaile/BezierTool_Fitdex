namespace BezierTool
{
    partial class FormMain
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
            this.pbCanva = new System.Windows.Forms.PictureBox();
            this.btnUploadBackground = new System.Windows.Forms.Button();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.btnNew4cPoints = new System.Windows.Forms.Button();
            this.btnNew4pPoints = new System.Windows.Forms.Button();
            this.btnNewLeastSquares = new System.Windows.Forms.Button();
            this.btnNewComposite = new System.Windows.Forms.Button();
            this.rbMouseInput = new System.Windows.Forms.RadioButton();
            this.rbKeyboardInput = new System.Windows.Forms.RadioButton();
            this.rbFileInput = new System.Windows.Forms.RadioButton();
            this.lblError = new System.Windows.Forms.Label();
            this.cbShowBackground = new System.Windows.Forms.CheckBox();
            this.btnModifycPoints = new System.Windows.Forms.Button();
            this.btnModifypPoints = new System.Windows.Forms.Button();
            this.rbMouseModify = new System.Windows.Forms.RadioButton();
            this.rbKeyboardModify = new System.Windows.Forms.RadioButton();
            this.groupAddInputType = new System.Windows.Forms.GroupBox();
            this.groupAddType = new System.Windows.Forms.GroupBox();
            this.btnDoneComposite = new System.Windows.Forms.Button();
            this.groupModifyType = new System.Windows.Forms.GroupBox();
            this.groupModifyInput = new System.Windows.Forms.GroupBox();
            this.groupParamType = new System.Windows.Forms.GroupBox();
            this.rbCentripetal = new System.Windows.Forms.RadioButton();
            this.btnChangeParam = new System.Windows.Forms.Button();
            this.rbChord = new System.Windows.Forms.RadioButton();
            this.rbUniform = new System.Windows.Forms.RadioButton();
            this.btnOutputcPoints = new System.Windows.Forms.Button();
            this.groupGetCoordinates = new System.Windows.Forms.GroupBox();
            this.btnOutputpPoints = new System.Windows.Forms.Button();
            this.groupOutput = new System.Windows.Forms.GroupBox();
            this.rbFileOutput = new System.Windows.Forms.RadioButton();
            this.rbScreenOutput = new System.Windows.Forms.RadioButton();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.btnSetScale = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.nudZoom = new System.Windows.Forms.NumericUpDown();
            this.cbShowcPoints = new System.Windows.Forms.CheckBox();
            this.panel_tools = new System.Windows.Forms.Panel();
            this.btnNewSegment = new System.Windows.Forms.Button();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.pnlLastColor = new System.Windows.Forms.Panel();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.pnlCanva = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.lblZoom = new System.Windows.Forms.Label();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.btnExportAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanva)).BeginInit();
            this.groupAddInputType.SuspendLayout();
            this.groupAddType.SuspendLayout();
            this.groupModifyType.SuspendLayout();
            this.groupModifyInput.SuspendLayout();
            this.groupParamType.SuspendLayout();
            this.groupGetCoordinates.SuspendLayout();
            this.groupOutput.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoom)).BeginInit();
            this.panel_tools.SuspendLayout();
            this.pnlCanva.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCanva
            // 
            this.pbCanva.BackColor = System.Drawing.SystemColors.Window;
            this.pbCanva.Location = new System.Drawing.Point(3, 3);
            this.pbCanva.Name = "pbCanva";
            this.pbCanva.Size = new System.Drawing.Size(1071, 969);
            this.pbCanva.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCanva.TabIndex = 0;
            this.pbCanva.TabStop = false;
            this.pbCanva.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanva_Paint);
            this.pbCanva.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanva_MouseDown);
            this.pbCanva.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanva_MouseMove);
            this.pbCanva.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanva_MouseUp);
            // 
            // btnUploadBackground
            // 
            this.btnUploadBackground.Location = new System.Drawing.Point(9, 627);
            this.btnUploadBackground.Name = "btnUploadBackground";
            this.btnUploadBackground.Size = new System.Drawing.Size(314, 37);
            this.btnUploadBackground.TabIndex = 1;
            this.btnUploadBackground.Text = "Upload Background Image";
            this.btnUploadBackground.UseVisualStyleBackColor = true;
            this.btnUploadBackground.Click += new System.EventHandler(this.btnUploadBackground_Click);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Location = new System.Drawing.Point(0, 46);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(322, 37);
            this.btnResetAll.TabIndex = 2;
            this.btnResetAll.Text = "Reset All";
            this.btnResetAll.UseVisualStyleBackColor = true;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // btnNew4cPoints
            // 
            this.btnNew4cPoints.Location = new System.Drawing.Point(6, 25);
            this.btnNew4cPoints.Name = "btnNew4cPoints";
            this.btnNew4cPoints.Size = new System.Drawing.Size(159, 37);
            this.btnNew4cPoints.TabIndex = 4;
            this.btnNew4cPoints.Text = "4 cPoints";
            this.btnNew4cPoints.UseVisualStyleBackColor = true;
            this.btnNew4cPoints.Click += new System.EventHandler(this.btnNew4cPoints_Click);
            // 
            // btnNew4pPoints
            // 
            this.btnNew4pPoints.Location = new System.Drawing.Point(6, 68);
            this.btnNew4pPoints.Name = "btnNew4pPoints";
            this.btnNew4pPoints.Size = new System.Drawing.Size(159, 37);
            this.btnNew4pPoints.TabIndex = 5;
            this.btnNew4pPoints.Text = "4 pPoints";
            this.btnNew4pPoints.UseVisualStyleBackColor = true;
            this.btnNew4pPoints.Click += new System.EventHandler(this.btnNew4pPoints_Click);
            // 
            // btnNewLeastSquares
            // 
            this.btnNewLeastSquares.Location = new System.Drawing.Point(6, 111);
            this.btnNewLeastSquares.Name = "btnNewLeastSquares";
            this.btnNewLeastSquares.Size = new System.Drawing.Size(159, 37);
            this.btnNewLeastSquares.TabIndex = 6;
            this.btnNewLeastSquares.Text = "Least Squares";
            this.btnNewLeastSquares.UseVisualStyleBackColor = true;
            this.btnNewLeastSquares.Click += new System.EventHandler(this.btnNewLeastSquares_Click);
            // 
            // btnNewComposite
            // 
            this.btnNewComposite.Location = new System.Drawing.Point(6, 154);
            this.btnNewComposite.Name = "btnNewComposite";
            this.btnNewComposite.Size = new System.Drawing.Size(159, 37);
            this.btnNewComposite.TabIndex = 7;
            this.btnNewComposite.Text = "Composite";
            this.btnNewComposite.UseVisualStyleBackColor = true;
            this.btnNewComposite.Click += new System.EventHandler(this.btnNewComposite_Click);
            // 
            // rbMouseInput
            // 
            this.rbMouseInput.AutoSize = true;
            this.rbMouseInput.Checked = true;
            this.rbMouseInput.Location = new System.Drawing.Point(6, 25);
            this.rbMouseInput.Name = "rbMouseInput";
            this.rbMouseInput.Size = new System.Drawing.Size(101, 24);
            this.rbMouseInput.TabIndex = 8;
            this.rbMouseInput.TabStop = true;
            this.rbMouseInput.Text = "w/ Mouse";
            this.rbMouseInput.UseVisualStyleBackColor = true;
            // 
            // rbKeyboardInput
            // 
            this.rbKeyboardInput.AutoSize = true;
            this.rbKeyboardInput.Location = new System.Drawing.Point(6, 55);
            this.rbKeyboardInput.Name = "rbKeyboardInput";
            this.rbKeyboardInput.Size = new System.Drawing.Size(120, 24);
            this.rbKeyboardInput.TabIndex = 9;
            this.rbKeyboardInput.Text = "w/ Keyboard";
            this.rbKeyboardInput.UseVisualStyleBackColor = true;
            // 
            // rbFileInput
            // 
            this.rbFileInput.AutoSize = true;
            this.rbFileInput.Location = new System.Drawing.Point(6, 85);
            this.rbFileInput.Name = "rbFileInput";
            this.rbFileInput.Size = new System.Drawing.Size(120, 24);
            this.rbFileInput.TabIndex = 10;
            this.rbFileInput.Text = "From .txt file";
            this.rbFileInput.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Black;
            this.lblError.Location = new System.Drawing.Point(3, 1);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(86, 20);
            this.lblError.TabIndex = 12;
            this.lblError.Text = "Messages:";
            // 
            // cbShowBackground
            // 
            this.cbShowBackground.AutoSize = true;
            this.cbShowBackground.Location = new System.Drawing.Point(9, 670);
            this.cbShowBackground.Name = "cbShowBackground";
            this.cbShowBackground.Size = new System.Drawing.Size(165, 24);
            this.cbShowBackground.TabIndex = 13;
            this.cbShowBackground.Text = "Show Background";
            this.cbShowBackground.UseVisualStyleBackColor = true;
            this.cbShowBackground.CheckStateChanged += new System.EventHandler(this.cbShowBackground_CheckStateChanged);
            // 
            // btnModifycPoints
            // 
            this.btnModifycPoints.Location = new System.Drawing.Point(6, 25);
            this.btnModifycPoints.Name = "btnModifycPoints";
            this.btnModifycPoints.Size = new System.Drawing.Size(159, 37);
            this.btnModifycPoints.TabIndex = 14;
            this.btnModifycPoints.Text = "Modify cPoints";
            this.btnModifycPoints.UseVisualStyleBackColor = true;
            this.btnModifycPoints.Click += new System.EventHandler(this.btnModifycPoints_Click);
            // 
            // btnModifypPoints
            // 
            this.btnModifypPoints.Location = new System.Drawing.Point(6, 68);
            this.btnModifypPoints.Name = "btnModifypPoints";
            this.btnModifypPoints.Size = new System.Drawing.Size(159, 37);
            this.btnModifypPoints.TabIndex = 15;
            this.btnModifypPoints.Text = "Modify pPoints";
            this.btnModifypPoints.UseVisualStyleBackColor = true;
            this.btnModifypPoints.Click += new System.EventHandler(this.btnModifypPoints_Click);
            // 
            // rbMouseModify
            // 
            this.rbMouseModify.AutoSize = true;
            this.rbMouseModify.Checked = true;
            this.rbMouseModify.Location = new System.Drawing.Point(6, 25);
            this.rbMouseModify.Name = "rbMouseModify";
            this.rbMouseModify.Size = new System.Drawing.Size(101, 24);
            this.rbMouseModify.TabIndex = 17;
            this.rbMouseModify.TabStop = true;
            this.rbMouseModify.Text = "w/ Mouse";
            this.rbMouseModify.UseVisualStyleBackColor = true;
            // 
            // rbKeyboardModify
            // 
            this.rbKeyboardModify.AutoSize = true;
            this.rbKeyboardModify.Location = new System.Drawing.Point(6, 55);
            this.rbKeyboardModify.Name = "rbKeyboardModify";
            this.rbKeyboardModify.Size = new System.Drawing.Size(120, 24);
            this.rbKeyboardModify.TabIndex = 18;
            this.rbKeyboardModify.Text = "w/ Keyboard";
            this.rbKeyboardModify.UseVisualStyleBackColor = true;
            // 
            // groupAddInputType
            // 
            this.groupAddInputType.Controls.Add(this.rbMouseInput);
            this.groupAddInputType.Controls.Add(this.rbKeyboardInput);
            this.groupAddInputType.Controls.Add(this.rbFileInput);
            this.groupAddInputType.Location = new System.Drawing.Point(180, 3);
            this.groupAddInputType.Name = "groupAddInputType";
            this.groupAddInputType.Size = new System.Drawing.Size(146, 115);
            this.groupAddInputType.TabIndex = 19;
            this.groupAddInputType.TabStop = false;
            this.groupAddInputType.Text = "Choose points:";
            // 
            // groupAddType
            // 
            this.groupAddType.Controls.Add(this.btnDoneComposite);
            this.groupAddType.Controls.Add(this.btnNew4cPoints);
            this.groupAddType.Controls.Add(this.btnNew4pPoints);
            this.groupAddType.Controls.Add(this.btnNewLeastSquares);
            this.groupAddType.Controls.Add(this.btnNewComposite);
            this.groupAddType.Location = new System.Drawing.Point(3, 46);
            this.groupAddType.Name = "groupAddType";
            this.groupAddType.Size = new System.Drawing.Size(171, 252);
            this.groupAddType.TabIndex = 20;
            this.groupAddType.TabStop = false;
            this.groupAddType.Text = "New Bezier of type:";
            // 
            // btnDoneComposite
            // 
            this.btnDoneComposite.Location = new System.Drawing.Point(6, 197);
            this.btnDoneComposite.Name = "btnDoneComposite";
            this.btnDoneComposite.Size = new System.Drawing.Size(67, 37);
            this.btnDoneComposite.TabIndex = 8;
            this.btnDoneComposite.Text = "Done";
            this.btnDoneComposite.UseVisualStyleBackColor = true;
            this.btnDoneComposite.Click += new System.EventHandler(this.btnDoneComposite_Click);
            // 
            // groupModifyType
            // 
            this.groupModifyType.Controls.Add(this.btnModifycPoints);
            this.groupModifyType.Controls.Add(this.btnModifypPoints);
            this.groupModifyType.Location = new System.Drawing.Point(3, 301);
            this.groupModifyType.Name = "groupModifyType";
            this.groupModifyType.Size = new System.Drawing.Size(171, 114);
            this.groupModifyType.TabIndex = 21;
            this.groupModifyType.TabStop = false;
            this.groupModifyType.Text = "Modify existing line:";
            // 
            // groupModifyInput
            // 
            this.groupModifyInput.Controls.Add(this.rbMouseModify);
            this.groupModifyInput.Controls.Add(this.rbKeyboardModify);
            this.groupModifyInput.Location = new System.Drawing.Point(180, 301);
            this.groupModifyInput.Name = "groupModifyInput";
            this.groupModifyInput.Size = new System.Drawing.Size(146, 114);
            this.groupModifyInput.TabIndex = 22;
            this.groupModifyInput.TabStop = false;
            this.groupModifyInput.Text = "Modify points:";
            // 
            // groupParamType
            // 
            this.groupParamType.Controls.Add(this.rbCentripetal);
            this.groupParamType.Controls.Add(this.btnChangeParam);
            this.groupParamType.Controls.Add(this.rbChord);
            this.groupParamType.Controls.Add(this.rbUniform);
            this.groupParamType.Location = new System.Drawing.Point(180, 124);
            this.groupParamType.Name = "groupParamType";
            this.groupParamType.Size = new System.Drawing.Size(146, 174);
            this.groupParamType.TabIndex = 24;
            this.groupParamType.TabStop = false;
            this.groupParamType.Text = "Paramterization:";
            // 
            // rbCentripetal
            // 
            this.rbCentripetal.AutoSize = true;
            this.rbCentripetal.Location = new System.Drawing.Point(6, 85);
            this.rbCentripetal.Name = "rbCentripetal";
            this.rbCentripetal.Size = new System.Drawing.Size(111, 24);
            this.rbCentripetal.TabIndex = 33;
            this.rbCentripetal.TabStop = true;
            this.rbCentripetal.Text = "Centripetal";
            this.rbCentripetal.UseVisualStyleBackColor = true;
            // 
            // btnChangeParam
            // 
            this.btnChangeParam.Location = new System.Drawing.Point(6, 115);
            this.btnChangeParam.Name = "btnChangeParam";
            this.btnChangeParam.Size = new System.Drawing.Size(134, 37);
            this.btnChangeParam.TabIndex = 2;
            this.btnChangeParam.Text = "Choose Line";
            this.btnChangeParam.UseVisualStyleBackColor = true;
            this.btnChangeParam.Click += new System.EventHandler(this.btnChangeParam_Click);
            // 
            // rbChord
            // 
            this.rbChord.AutoSize = true;
            this.rbChord.Location = new System.Drawing.Point(6, 55);
            this.rbChord.Name = "rbChord";
            this.rbChord.Size = new System.Drawing.Size(131, 24);
            this.rbChord.TabIndex = 1;
            this.rbChord.Text = "Chord Length";
            this.rbChord.UseVisualStyleBackColor = true;
            this.rbChord.CheckedChanged += new System.EventHandler(this.rbChord_CheckedChanged);
            // 
            // rbUniform
            // 
            this.rbUniform.AutoSize = true;
            this.rbUniform.Checked = true;
            this.rbUniform.Location = new System.Drawing.Point(6, 25);
            this.rbUniform.Name = "rbUniform";
            this.rbUniform.Size = new System.Drawing.Size(90, 24);
            this.rbUniform.TabIndex = 0;
            this.rbUniform.TabStop = true;
            this.rbUniform.Text = "Uniform";
            this.rbUniform.UseVisualStyleBackColor = true;
            this.rbUniform.CheckedChanged += new System.EventHandler(this.rbUniform_CheckedChanged);
            // 
            // btnOutputcPoints
            // 
            this.btnOutputcPoints.Location = new System.Drawing.Point(6, 25);
            this.btnOutputcPoints.Name = "btnOutputcPoints";
            this.btnOutputcPoints.Size = new System.Drawing.Size(159, 37);
            this.btnOutputcPoints.TabIndex = 25;
            this.btnOutputcPoints.Text = "cPoints";
            this.btnOutputcPoints.UseVisualStyleBackColor = true;
            this.btnOutputcPoints.Click += new System.EventHandler(this.btnOutputcPoints_Click);
            // 
            // groupGetCoordinates
            // 
            this.groupGetCoordinates.Controls.Add(this.btnOutputpPoints);
            this.groupGetCoordinates.Controls.Add(this.btnOutputcPoints);
            this.groupGetCoordinates.Location = new System.Drawing.Point(3, 421);
            this.groupGetCoordinates.Name = "groupGetCoordinates";
            this.groupGetCoordinates.Size = new System.Drawing.Size(171, 114);
            this.groupGetCoordinates.TabIndex = 26;
            this.groupGetCoordinates.TabStop = false;
            this.groupGetCoordinates.Text = "Get coordinates";
            // 
            // btnOutputpPoints
            // 
            this.btnOutputpPoints.Location = new System.Drawing.Point(6, 68);
            this.btnOutputpPoints.Name = "btnOutputpPoints";
            this.btnOutputpPoints.Size = new System.Drawing.Size(159, 37);
            this.btnOutputpPoints.TabIndex = 26;
            this.btnOutputpPoints.Text = "pPoints";
            this.btnOutputpPoints.UseVisualStyleBackColor = true;
            this.btnOutputpPoints.Click += new System.EventHandler(this.btnOutputpPoints_Click);
            // 
            // groupOutput
            // 
            this.groupOutput.Controls.Add(this.rbFileOutput);
            this.groupOutput.Controls.Add(this.rbScreenOutput);
            this.groupOutput.Location = new System.Drawing.Point(180, 421);
            this.groupOutput.Name = "groupOutput";
            this.groupOutput.Size = new System.Drawing.Size(146, 114);
            this.groupOutput.TabIndex = 27;
            this.groupOutput.TabStop = false;
            this.groupOutput.Text = "Output to";
            // 
            // rbFileOutput
            // 
            this.rbFileOutput.AutoSize = true;
            this.rbFileOutput.Location = new System.Drawing.Point(6, 55);
            this.rbFileOutput.Name = "rbFileOutput";
            this.rbFileOutput.Size = new System.Drawing.Size(79, 24);
            this.rbFileOutput.TabIndex = 1;
            this.rbFileOutput.Text = ".txt file";
            this.rbFileOutput.UseVisualStyleBackColor = true;
            // 
            // rbScreenOutput
            // 
            this.rbScreenOutput.AutoSize = true;
            this.rbScreenOutput.Checked = true;
            this.rbScreenOutput.Location = new System.Drawing.Point(6, 25);
            this.rbScreenOutput.Name = "rbScreenOutput";
            this.rbScreenOutput.Size = new System.Drawing.Size(85, 24);
            this.rbScreenOutput.TabIndex = 0;
            this.rbScreenOutput.TabStop = true;
            this.rbScreenOutput.Text = "Screen";
            this.rbScreenOutput.UseVisualStyleBackColor = true;
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.btnSetScale);
            this.panel_bottom.Controls.Add(this.btnDefault);
            this.panel_bottom.Controls.Add(this.btnResetAll);
            this.panel_bottom.Location = new System.Drawing.Point(1118, 927);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(327, 83);
            this.panel_bottom.TabIndex = 31;
            // 
            // btnSetScale
            // 
            this.btnSetScale.Location = new System.Drawing.Point(0, 3);
            this.btnSetScale.Name = "btnSetScale";
            this.btnSetScale.Size = new System.Drawing.Size(159, 37);
            this.btnSetScale.TabIndex = 41;
            this.btnSetScale.Text = "Set Scale";
            this.btnSetScale.UseVisualStyleBackColor = true;
            this.btnSetScale.Click += new System.EventHandler(this.btnSetScale_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(165, 3);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(158, 37);
            this.btnDefault.TabIndex = 35;
            this.btnDefault.Text = "Form Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // nudZoom
            // 
            this.nudZoom.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudZoom.Location = new System.Drawing.Point(1363, -2);
            this.nudZoom.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudZoom.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudZoom.Name = "nudZoom";
            this.nudZoom.Size = new System.Drawing.Size(59, 26);
            this.nudZoom.TabIndex = 38;
            this.nudZoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudZoom.ValueChanged += new System.EventHandler(this.nudZoom_ValueChanged);
            // 
            // cbShowcPoints
            // 
            this.cbShowcPoints.AutoSize = true;
            this.cbShowcPoints.Checked = true;
            this.cbShowcPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowcPoints.Location = new System.Drawing.Point(180, 670);
            this.cbShowcPoints.Name = "cbShowcPoints";
            this.cbShowcPoints.Size = new System.Drawing.Size(131, 24);
            this.cbShowcPoints.TabIndex = 36;
            this.cbShowcPoints.Text = "Show cPoints";
            this.cbShowcPoints.UseVisualStyleBackColor = true;
            this.cbShowcPoints.CheckedChanged += new System.EventHandler(this.cbShowcPoints_CheckedChanged);
            // 
            // panel_tools
            // 
            this.panel_tools.Controls.Add(this.btnExportAll);
            this.panel_tools.Controls.Add(this.btnNewSegment);
            this.panel_tools.Controls.Add(this.cbShowcPoints);
            this.panel_tools.Controls.Add(this.btnUploadBackground);
            this.panel_tools.Controls.Add(this.btnDeleteObject);
            this.panel_tools.Controls.Add(this.groupAddType);
            this.panel_tools.Controls.Add(this.pnlLastColor);
            this.panel_tools.Controls.Add(this.cbShowBackground);
            this.panel_tools.Controls.Add(this.groupAddInputType);
            this.panel_tools.Controls.Add(this.groupParamType);
            this.panel_tools.Controls.Add(this.groupModifyType);
            this.panel_tools.Controls.Add(this.groupOutput);
            this.panel_tools.Controls.Add(this.btnChangeColor);
            this.panel_tools.Controls.Add(this.groupModifyInput);
            this.panel_tools.Controls.Add(this.groupGetCoordinates);
            this.panel_tools.Location = new System.Drawing.Point(1118, 17);
            this.panel_tools.Name = "panel_tools";
            this.panel_tools.Size = new System.Drawing.Size(330, 773);
            this.panel_tools.TabIndex = 32;
            // 
            // btnNewSegment
            // 
            this.btnNewSegment.Location = new System.Drawing.Point(9, 3);
            this.btnNewSegment.Name = "btnNewSegment";
            this.btnNewSegment.Size = new System.Drawing.Size(159, 37);
            this.btnNewSegment.TabIndex = 41;
            this.btnNewSegment.Text = "Line Segment";
            this.btnNewSegment.UseVisualStyleBackColor = true;
            this.btnNewSegment.Click += new System.EventHandler(this.btnNewSegment_Click);
            // 
            // btnDeleteObject
            // 
            this.btnDeleteObject.Location = new System.Drawing.Point(9, 584);
            this.btnDeleteObject.Name = "btnDeleteObject";
            this.btnDeleteObject.Size = new System.Drawing.Size(314, 37);
            this.btnDeleteObject.TabIndex = 31;
            this.btnDeleteObject.Text = "Choose Object to Delete";
            this.btnDeleteObject.UseVisualStyleBackColor = true;
            this.btnDeleteObject.Click += new System.EventHandler(this.btnDeleteCurve_Click);
            // 
            // pnlLastColor
            // 
            this.pnlLastColor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlLastColor.Location = new System.Drawing.Point(174, 541);
            this.pnlLastColor.Name = "pnlLastColor";
            this.pnlLastColor.Size = new System.Drawing.Size(146, 37);
            this.pnlLastColor.TabIndex = 37;
            this.pnlLastColor.Click += new System.EventHandler(this.pnlLastColor_Click);
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(9, 541);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(159, 37);
            this.btnChangeColor.TabIndex = 36;
            this.btnChangeColor.Text = "Choose Curve";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // pnlCanva
            // 
            this.pnlCanva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCanva.Controls.Add(this.pbCanva);
            this.pnlCanva.Location = new System.Drawing.Point(12, 12);
            this.pnlCanva.Name = "pnlCanva";
            this.pnlCanva.Size = new System.Drawing.Size(1097, 998);
            this.pnlCanva.TabIndex = 35;
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(1303, 1);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(54, 20);
            this.lblZoom.TabIndex = 39;
            this.lblZoom.Text = "Zoom:";
            // 
            // pnlMessage
            // 
            this.pnlMessage.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMessage.Controls.Add(this.lblError);
            this.pnlMessage.Controls.Add(this.lblZoom);
            this.pnlMessage.Controls.Add(this.nudZoom);
            this.pnlMessage.Location = new System.Drawing.Point(12, 1016);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(1429, 28);
            this.pnlMessage.TabIndex = 40;
            // 
            // btnExportAll
            // 
            this.btnExportAll.Location = new System.Drawing.Point(168, 733);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(159, 37);
            this.btnExportAll.TabIndex = 41;
            this.btnExportAll.Text = "Export All";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 1053);
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.pnlCanva);
            this.Controls.Add(this.panel_tools);
            this.Controls.Add(this.panel_bottom);
            this.Name = "FormMain";
            this.Text = "Bezier Tool";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanva)).EndInit();
            this.groupAddInputType.ResumeLayout(false);
            this.groupAddInputType.PerformLayout();
            this.groupAddType.ResumeLayout(false);
            this.groupModifyType.ResumeLayout(false);
            this.groupModifyInput.ResumeLayout(false);
            this.groupModifyInput.PerformLayout();
            this.groupParamType.ResumeLayout(false);
            this.groupParamType.PerformLayout();
            this.groupGetCoordinates.ResumeLayout(false);
            this.groupOutput.ResumeLayout(false);
            this.groupOutput.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudZoom)).EndInit();
            this.panel_tools.ResumeLayout(false);
            this.panel_tools.PerformLayout();
            this.pnlCanva.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanva;
        private System.Windows.Forms.Button btnUploadBackground;
        private System.Windows.Forms.Button btnResetAll;
        private System.Windows.Forms.Button btnNew4cPoints;
        private System.Windows.Forms.Button btnNew4pPoints;
        private System.Windows.Forms.Button btnNewLeastSquares;
        private System.Windows.Forms.Button btnNewComposite;
        private System.Windows.Forms.RadioButton rbMouseInput;
        private System.Windows.Forms.RadioButton rbKeyboardInput;
        private System.Windows.Forms.RadioButton rbFileInput;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.CheckBox cbShowBackground;
        private System.Windows.Forms.Button btnModifycPoints;
        private System.Windows.Forms.Button btnModifypPoints;
        private System.Windows.Forms.RadioButton rbMouseModify;
        private System.Windows.Forms.RadioButton rbKeyboardModify;
        private System.Windows.Forms.GroupBox groupAddInputType;
        private System.Windows.Forms.GroupBox groupAddType;
        private System.Windows.Forms.GroupBox groupModifyType;
        private System.Windows.Forms.GroupBox groupModifyInput;
        private System.Windows.Forms.GroupBox groupParamType;
        private System.Windows.Forms.RadioButton rbChord;
        private System.Windows.Forms.RadioButton rbUniform;
        private System.Windows.Forms.Button btnDoneComposite;
        private System.Windows.Forms.Button btnOutputcPoints;
        private System.Windows.Forms.GroupBox groupGetCoordinates;
        private System.Windows.Forms.Button btnOutputpPoints;
        private System.Windows.Forms.GroupBox groupOutput;
        private System.Windows.Forms.RadioButton rbFileOutput;
        private System.Windows.Forms.RadioButton rbScreenOutput;
        private System.Windows.Forms.Button btnChangeParam;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel_tools;
        private System.Windows.Forms.RadioButton rbCentripetal;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.Panel pnlCanva;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel pnlLastColor;
        private System.Windows.Forms.Button btnChangeColor;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.CheckBox cbShowcPoints;
        private System.Windows.Forms.NumericUpDown nudZoom;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Button btnSetScale;
        private System.Windows.Forms.Button btnNewSegment;
        private System.Windows.Forms.Button btnExportAll;
    }
}

