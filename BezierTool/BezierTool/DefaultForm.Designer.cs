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
            this.btnPolygon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurveSize = new System.Windows.Forms.TextBox();
            this.txtPolygonSize = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btndPointsAdd
            // 
            this.btndPointsAdd.Location = new System.Drawing.Point(13, 13);
            this.btndPointsAdd.Name = "btndPointsAdd";
            this.btndPointsAdd.Size = new System.Drawing.Size(265, 37);
            this.btndPointsAdd.TabIndex = 0;
            this.btndPointsAdd.Text = "Add Default Points form .txt File";
            this.btndPointsAdd.UseVisualStyleBackColor = true;
            this.btndPointsAdd.Click += new System.EventHandler(this.btndPointsAdd_Click);
            // 
            // btndPointColor
            // 
            this.btndPointColor.Location = new System.Drawing.Point(13, 56);
            this.btndPointColor.Name = "btndPointColor";
            this.btndPointColor.Size = new System.Drawing.Size(265, 37);
            this.btndPointColor.TabIndex = 1;
            this.btndPointColor.Text = "Color of dPoints";
            this.btndPointColor.UseVisualStyleBackColor = true;
            this.btndPointColor.Click += new System.EventHandler(this.btndPointColor_Click);
            // 
            // btncPointColor
            // 
            this.btncPointColor.Location = new System.Drawing.Point(13, 99);
            this.btncPointColor.Name = "btncPointColor";
            this.btncPointColor.Size = new System.Drawing.Size(265, 37);
            this.btncPointColor.TabIndex = 2;
            this.btncPointColor.Text = "Color of cPoints";
            this.btncPointColor.UseVisualStyleBackColor = true;
            this.btncPointColor.Click += new System.EventHandler(this.btncPointColor_Click);
            // 
            // btnpPointColor
            // 
            this.btnpPointColor.Location = new System.Drawing.Point(13, 142);
            this.btnpPointColor.Name = "btnpPointColor";
            this.btnpPointColor.Size = new System.Drawing.Size(265, 37);
            this.btnpPointColor.TabIndex = 3;
            this.btnpPointColor.Text = "Color of pPoints";
            this.btnpPointColor.UseVisualStyleBackColor = true;
            this.btnpPointColor.Click += new System.EventHandler(this.btnpPointColor_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(322, 379);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 37);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(13, 185);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(265, 37);
            this.btnPolygon.TabIndex = 5;
            this.btnPolygon.Text = "Color of cPoint Polygons";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Thickness of Curves:                      px";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Thickness of Polygon Lines:           px";
            // 
            // txtCurveSize
            // 
            this.txtCurveSize.Location = new System.Drawing.Point(213, 269);
            this.txtCurveSize.Name = "txtCurveSize";
            this.txtCurveSize.Size = new System.Drawing.Size(34, 26);
            this.txtCurveSize.TabIndex = 8;
            this.txtCurveSize.Text = "1";
            // 
            // txtPolygonSize
            // 
            this.txtPolygonSize.Location = new System.Drawing.Point(213, 298);
            this.txtPolygonSize.Name = "txtPolygonSize";
            this.txtPolygonSize.Size = new System.Drawing.Size(34, 26);
            this.txtPolygonSize.TabIndex = 9;
            this.txtPolygonSize.Text = "1";
            // 
            // DefaultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 428);
            this.Controls.Add(this.txtPolygonSize);
            this.Controls.Add(this.txtCurveSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPolygon);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnpPointColor);
            this.Controls.Add(this.btncPointColor);
            this.Controls.Add(this.btndPointColor);
            this.Controls.Add(this.btndPointsAdd);
            this.Name = "DefaultForm";
            this.Text = "DefaultForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btndPointsAdd;
        private System.Windows.Forms.Button btndPointColor;
        private System.Windows.Forms.Button btncPointColor;
        private System.Windows.Forms.Button btnpPointColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurveSize;
        private System.Windows.Forms.TextBox txtPolygonSize;
    }
}