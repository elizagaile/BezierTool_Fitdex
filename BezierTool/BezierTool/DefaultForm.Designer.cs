namespace BezierTool
{
    partial class DefaultForm
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
            this.btndPointsAdd = new System.Windows.Forms.Button();
            this.btndPointColor = new System.Windows.Forms.Button();
            this.btncPointColor = new System.Windows.Forms.Button();
            this.btnpPointColor = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnPolygonColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurveSize = new System.Windows.Forms.TextBox();
            this.txtPolygonSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPointSize = new System.Windows.Forms.TextBox();
            this.btnSetScale = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCanvaWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCanvaHeight = new System.Windows.Forms.TextBox();
            this.btnCurveColor = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btndPointsAdd
            // 
            this.btndPointsAdd.Location = new System.Drawing.Point(12, 14);
            this.btndPointsAdd.Name = "btndPointsAdd";
            this.btndPointsAdd.Size = new System.Drawing.Size(265, 37);
            this.btndPointsAdd.TabIndex = 0;
            this.btndPointsAdd.Text = "Add Default Points form .txt File";
            this.btndPointsAdd.UseVisualStyleBackColor = true;
            this.btndPointsAdd.Click += new System.EventHandler(this.btndPointsAdd_Click);
            // 
            // btndPointColor
            // 
            this.btndPointColor.Location = new System.Drawing.Point(12, 99);
            this.btndPointColor.Name = "btndPointColor";
            this.btndPointColor.Size = new System.Drawing.Size(265, 37);
            this.btndPointColor.TabIndex = 1;
            this.btndPointColor.Text = "Color of dPoints";
            this.btndPointColor.UseVisualStyleBackColor = true;
            this.btndPointColor.Click += new System.EventHandler(this.btndPointColor_Click);
            // 
            // btncPointColor
            // 
            this.btncPointColor.Location = new System.Drawing.Point(12, 142);
            this.btncPointColor.Name = "btncPointColor";
            this.btncPointColor.Size = new System.Drawing.Size(265, 37);
            this.btncPointColor.TabIndex = 2;
            this.btncPointColor.Text = "Color of cPoints";
            this.btncPointColor.UseVisualStyleBackColor = true;
            this.btncPointColor.Click += new System.EventHandler(this.btncPointColor_Click);
            // 
            // btnpPointColor
            // 
            this.btnpPointColor.Location = new System.Drawing.Point(12, 185);
            this.btnpPointColor.Name = "btnpPointColor";
            this.btnpPointColor.Size = new System.Drawing.Size(265, 37);
            this.btnpPointColor.TabIndex = 3;
            this.btnpPointColor.Text = "Color of pPoints";
            this.btnpPointColor.UseVisualStyleBackColor = true;
            this.btnpPointColor.Click += new System.EventHandler(this.btnpPointColor_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(499, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 37);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnPolygonColor
            // 
            this.btnPolygonColor.Location = new System.Drawing.Point(12, 228);
            this.btnPolygonColor.Name = "btnPolygonColor";
            this.btnPolygonColor.Size = new System.Drawing.Size(265, 37);
            this.btnPolygonColor.TabIndex = 5;
            this.btnPolygonColor.Text = "Color of cPoint Polygons";
            this.btnPolygonColor.UseVisualStyleBackColor = true;
            this.btnPolygonColor.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Thickness of Curves:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Thickness of Polygon Lines: ";
            // 
            // txtCurveSize
            // 
            this.txtCurveSize.Location = new System.Drawing.Point(217, 5);
            this.txtCurveSize.Name = "txtCurveSize";
            this.txtCurveSize.Size = new System.Drawing.Size(34, 26);
            this.txtCurveSize.TabIndex = 8;
            this.txtCurveSize.Text = "1";
            // 
            // txtPolygonSize
            // 
            this.txtPolygonSize.Location = new System.Drawing.Point(217, 37);
            this.txtPolygonSize.Name = "txtPolygonSize";
            this.txtPolygonSize.Size = new System.Drawing.Size(34, 26);
            this.txtPolygonSize.TabIndex = 9;
            this.txtPolygonSize.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Size of Points:";
            // 
            // txtPointSize
            // 
            this.txtPointSize.Location = new System.Drawing.Point(217, 69);
            this.txtPointSize.Name = "txtPointSize";
            this.txtPointSize.Size = new System.Drawing.Size(34, 26);
            this.txtPointSize.TabIndex = 12;
            this.txtPointSize.Text = "2";
            // 
            // btnSetScale
            // 
            this.btnSetScale.Location = new System.Drawing.Point(12, 56);
            this.btnSetScale.Name = "btnSetScale";
            this.btnSetScale.Size = new System.Drawing.Size(265, 37);
            this.btnSetScale.TabIndex = 13;
            this.btnSetScale.Text = "Set Scale";
            this.btnSetScale.UseVisualStyleBackColor = true;
            this.btnSetScale.Click += new System.EventHandler(this.btnSetScale_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(121, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(8, 8);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Width of Canva:";
            // 
            // txtCanvaWidth
            // 
            this.txtCanvaWidth.Location = new System.Drawing.Point(182, 101);
            this.txtCanvaWidth.Name = "txtCanvaWidth";
            this.txtCanvaWidth.Size = new System.Drawing.Size(69, 26);
            this.txtCanvaWidth.TabIndex = 16;
            this.txtCanvaWidth.Text = "125";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Height of Canva:";
            // 
            // txtCanvaHeight
            // 
            this.txtCanvaHeight.Location = new System.Drawing.Point(182, 133);
            this.txtCanvaHeight.Name = "txtCanvaHeight";
            this.txtCanvaHeight.Size = new System.Drawing.Size(69, 26);
            this.txtCanvaHeight.TabIndex = 18;
            this.txtCanvaHeight.Text = "75";
            // 
            // btnCurveColor
            // 
            this.btnCurveColor.Location = new System.Drawing.Point(12, 269);
            this.btnCurveColor.Name = "btnCurveColor";
            this.btnCurveColor.Size = new System.Drawing.Size(265, 37);
            this.btnCurveColor.TabIndex = 19;
            this.btnCurveColor.Text = "Color of Curves";
            this.btnCurveColor.UseVisualStyleBackColor = true;
            this.btnCurveColor.Click += new System.EventHandler(this.btnCurveColor_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtCurveSize);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtCanvaHeight);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPolygonSize);
            this.panel1.Controls.Add(this.txtCanvaWidth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPointSize);
            this.panel1.Location = new System.Drawing.Point(284, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 173);
            this.panel1.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(257, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "px";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "px";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(257, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "px";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(257, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "cm";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(257, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "cm";
            // 
            // DefaultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 317);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCurveColor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSetScale);
            this.Controls.Add(this.btnPolygonColor);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnpPointColor);
            this.Controls.Add(this.btncPointColor);
            this.Controls.Add(this.btndPointColor);
            this.Controls.Add(this.btndPointsAdd);
            this.Name = "DefaultForm";
            this.Text = "DefaultForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btndPointsAdd;
        private System.Windows.Forms.Button btndPointColor;
        private System.Windows.Forms.Button btncPointColor;
        private System.Windows.Forms.Button btnpPointColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnPolygonColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurveSize;
        private System.Windows.Forms.TextBox txtPolygonSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPointSize;
        private System.Windows.Forms.Button btnSetScale;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCanvaWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCanvaHeight;
        private System.Windows.Forms.Button btnCurveColor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}