using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace BezierTool
{
    public partial class DefaultForm : Form
    {
        public static List<Point> dPoints = new List<Point>();
        public static Color dPointsColor = Color.Green;
        public static Color cPointsColor = Color.Red;
        public static Color pPointsColor = Color.Black;
        public static Color polygonColor = Color.LightGray;
        public static int curveSize = 1;
        public static int polygonSize = 1;

        public DefaultForm()
        {
            InitializeComponent();
            txtCurveSize.Text = "" + curveSize;
            txtPolygonSize.Text = "" + polygonSize;
        }

        private void btndPointsAdd_Click(object sender, EventArgs e)
        {
                Point point = new Point();

                string path = "";
                string textLine = "";

                try
                {
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Title = "Open Text File",
                        Filter = "TXT files|*.txt", // only .txt files are supported
                        InitialDirectory = @"C:\"
                    };

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        path = dialog.FileName;
                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("File upload error!");
                }

                if (File.Exists(path))
                {
                    using (StreamReader file = new StreamReader(path))
                    {
                        while ((textLine = file.ReadLine()) != null)
                        {
                            try
                            {
                                int index = textLine.IndexOf(' ');
                                string xCoordinate = textLine.Substring(0, index);
                                string yCoordinate = textLine.Substring(index + 1);
                                point.X = Convert.ToInt32(xCoordinate);
                                point.Y = Convert.ToInt32(yCoordinate);
                            }

                            catch (Exception)
                            {
                            MessageBox.Show(".txt file was not correct!");
                            }

                            dPoints.Add(point);
                        }
                    }
                }

                else
                {
                    MessageBox.Show("File upload error!");
                }

        }

        private void btndPointColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                dPointsColor = colorDialog1.Color;
            }
        }

        private void btncPointColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cPointsColor = colorDialog1.Color;
            }
        }

        private void btnpPointColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pPointsColor = colorDialog1.Color;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtCurveSize.Text == "" || txtPolygonSize.Text == "")
            {
                MessageBox.Show("Must fill all values!");
            }

            curveSize = Convert.ToInt32(txtCurveSize.Text);
            polygonSize = Convert.ToInt32(txtPolygonSize.Text);

            if (curveSize > 20 || polygonSize > 20)
            {
                MessageBox.Show("Maximum thickness is 20px!");
            }

            this.Close();
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                polygonColor = colorDialog1.Color;
            }
        }

        public static void ResetAll()
        {
            dPoints = new List<Point>();
            dPointsColor = Color.Blue;
            cPointsColor = Color.Red;
            pPointsColor = Color.Black;
            polygonColor = Color.LightGray;
            curveSize = 1;
            polygonSize = 1;
        }

    }
}
