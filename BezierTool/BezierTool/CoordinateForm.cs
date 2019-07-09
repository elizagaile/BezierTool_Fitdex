using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BezierTool
{
    public partial class FormCoordinates : Form
    {
        FormMain.FormType formType; // reason for opening this form
        FormMain.BezierType curveType; // type of curve being used in this form

        private List<TextBox> coordinates = new List<TextBox>(); //list of textBoxes for point coordinates
        private string labelType = ""; //point type for labels - "C" for control points, "P" for knot points
        private int namingCounter = 1; //count of point coordinates, used for naming textboxes and labels
        public static bool curveAdded = false; //to determine if a curve was added successfully
        public static Tuple<PointF, PointF> scaleReal = null;


        // Initialization.
        public FormCoordinates(FormMain.FormType thisFormType, FormMain.BezierType thisCurveType)
        {
            InitializeComponent();

            // For scrolling of point list:
            tlpCoordinates.HorizontalScroll.Maximum = 0;
            tlpCoordinates.AutoScroll = false;
            tlpCoordinates.VerticalScroll.Visible = false;
            tlpCoordinates.AutoScroll = true;

            curveType = thisCurveType;
            formType = thisFormType;

            if (formType == FormMain.FormType.Add)
            {
                InitializeAdd();
            }

            else if (formType == FormMain.FormType.Modify)
            {
                InitializeModify();
            }

            else if (formType == FormMain.FormType.Output)
            {
                InitializeOutput();
            }

            else if (formType == FormMain.FormType.Scale)
            {
                InitializeScale();
            }
        }
        

        // Initialize form for adding a new curve.
        private void InitializeAdd()
        {
            this.Text = "New <" + curveType + "> curve";
            
            if (curveType == FormMain.BezierType.LineSegment)
            {
                labelType = "Nr";
                gbCoordinates.Text = "Input endpoints of the line segment:";
                btnAddRow.Visible = false;
                btnDeleteRow.Visible = false;

                for (int i = 0; i < 2; i++)
                {
                    AddRow();
                }
            }

            else if (curveType == FormMain.BezierType.cPoints)
            {
                labelType = "C";
                for (int i = 0; i < 4; i++)
                {
                    AddRow();
                }
            }

            else if (curveType == FormMain.BezierType.pPoints || curveType == FormMain.BezierType.LeastSquares || curveType == FormMain.BezierType.Composite)
            {
                labelType = "P";
                for (int i = 0; i < 4; i++)
                {
                    AddRow();
                }
            }
            
            // <4 cPoint> and <4 pPoint> curves have exactly 4 input points; no need to add or delete input lines.
            if (curveType == FormMain.BezierType.cPoints || curveType == FormMain.BezierType.pPoints)
            {
                gbCoordinates.Text = "Input <" + curveType + "> control point coordinates:";
                btnAddRow.Visible = false;
                btnDeleteRow.Visible = false;
            }

            // Count of <Least Squares> and <Composite> input point count can vary; its possible to add and delete input lines.
            else if (curveType == FormMain.BezierType.LeastSquares || curveType == FormMain.BezierType.Composite)
            {
                gbCoordinates.Text = "Input <" + curveType + "> knot point coordinates:";
                btnAddRow.Visible = true;
                btnDeleteRow.Visible = true;
            }

            return;
        }
        

        // Initialize form for modifying a curve.
        private void InitializeModify()
        {
            this.Text = "Modify <" + curveType + "> curve";

            btnAddRow.Visible = false; // can't add or delete input lines when modifying a curve
            btnDeleteRow.Visible = false;

            List<PointF> pointList = new List<PointF>();
            int i = FormMain.localPoint.Item1;

            if (FormMain.modifyPointType == FormMain.BezierType.cPoints)
            {
                gbCoordinates.Text = "Modify <" + curveType + "> control point coordinates:";
                labelType = "C";
                pointList = ScaleOutputPoints(FormMain.cPointsAll[i]);
            }

            else if (FormMain.modifyPointType == FormMain.BezierType.pPoints)
            {
                gbCoordinates.Text = "Modify <" + curveType + "> knot point coordinates:";
                labelType = "P";
                pointList = ScaleOutputPoints(FormMain.pPointsAll[i]);
            }

            // It is possible to modify only one <Composite> knot point at a time.
            if (curveType == FormMain.BezierType.Composite)
            {
                int j = FormMain.localPoint.Item2; //get which knot point is being modified
                namingCounter = j + 1; //labels start at 1, lists at 0
                AddRow();

                coordinates[0].Text = "" + Math.Round(pointList[j].X, 4);
                coordinates[1].Text = "" + Math.Round(pointList[j].Y, 4);

                return;
            }

            // Make new input row for each point:
            for (int j = 0; j < pointList.Count; j++) 
            {
                AddRow();
            }
            
            // After making input rows, fill each textbox with appropriate coordinate:
            for (int j = 0; j < pointList.Count; j++)
            {
                coordinates[2 * j].Text = "" + Math.Round(pointList[j].X, 4);
                coordinates[2 * j + 1].Text = "" + Math.Round(pointList[j].Y, 4);
            }

            return;
        }


        // Initialize form for outputting line coordinates.
        private void InitializeOutput()
        {
            this.Text = "Output <" + curveType + "> curve";

            btnAddRow.Visible = false; // can't add or delete input lines when viewing point coordinates
            btnDeleteRow.Visible = false;
            btnResetInput.Visible = false; // can't reset point coordinates when viewing
            btnSubmitInput.Visible = false; // can't submit point coordinates when viewing

            List<PointF> pointList = new List<PointF>();
            int i = FormMain.localPoint.Item1;

            if (FormMain.outputPointType == FormMain.BezierType.cPoints)
            {
                gbCoordinates.Text = "List of <" + curveType + "> control point coordinates:";
                labelType = "C";
                pointList = ScaleOutputPoints(FormMain.cPointsAll[i]);
            }

            else if (FormMain.outputPointType == FormMain.BezierType.pPoints)
            {
                gbCoordinates.Text = "List of <" + curveType + "> knot point coordinates:";
                labelType = "P";
                pointList = ScaleOutputPoints(FormMain.pPointsAll[i]);
            }

            // Make new input row for each point:
            for (int j = 0; j < pointList.Count; j++)
            {
                AddRow();
            }

            // After making input rows, fill each textbox with appropriate coordinate:
            for (int j = 0; j < pointList.Count; j++)
            {
                coordinates[2 * j].Text = "" + Math.Round(pointList[j].X, 4);
                coordinates[2 * j + 1].Text = "" + Math.Round(pointList[j].Y, 4);
            }

            return;
        }


        // Initialize form for setting a scale by mouse.
        private void InitializeScale()
        {
            this.Text = "Set Scale";
            labelType = "nr";
            gbCoordinates.Text = "Set scale point coordinates!";
            btnAddRow.Visible = false; // can't add or delete input lines when viewing point coordinates
            btnDeleteRow.Visible = false;
            AddRow();
            AddRow();
        }


        // Add a new row.
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            AddRow();
        }


        //Delete a row of input.
        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (curveType == FormMain.BezierType.LeastSquares && tlpCoordinates.RowCount <= 9) // 4 rows minimum plus 5 rows from table design equals 9 rows
            {
                MessageBox.Show("<Least Squares> curves can't have less than 4 knot points!");
                return;
            }

            if (curveType == FormMain.BezierType.Composite && tlpCoordinates.RowCount <= 7) // 2 rows minimum plus 5 rows from design equals 7 rows
            {
                MessageBox.Show("<Composite> curves can't have less than 2 knot points!");
                return;
            }


            // Remove all controls from the last row:
            for (int i = 0; i < tlpCoordinates.ColumnCount; i++)
            {
                tlpCoordinates.Controls.RemoveAt(tlpCoordinates.Controls.Count - 1);
            }

            //remove textboxes from input list
            coordinates.RemoveAt(coordinates.Count - 1);
            coordinates.RemoveAt(coordinates.Count - 1);

            tlpCoordinates.RowCount--; // remove last row
            namingCounter--;
        }


        // Clear all coordinates in textboxes.
        private void btnResetInput_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                coordinates[i].Text = "";
            }
        }


        //Submit the input of this form to FormMain.
        private void btnSubmitInput_Click(object sender, EventArgs e)
        {
            //Check if all textboxes are filled:
            foreach (TextBox coordinate in coordinates)
            {
                if (coordinate.Text == "")
                {
                    MessageBox.Show("Must fill all values!");
                    return;
                }
            }

            List<PointF> pointList = new List<PointF>();
            float x, y;


            // Put all values from textboxes in a list of control points:
            for (int j = 0; j < coordinates.Count; j += 2)
            {
                x = float.Parse(coordinates[j].Text);
                y = float.Parse(coordinates[j + 1].Text);
                PointF tmp = new PointF(x, y);

                pointList.Add(tmp);
            }


            if (formType == FormMain.FormType.Scale)
            {
                if (pointList[0].X == pointList[1].X && pointList[0].Y == pointList[1].Y)
                {
                    MessageBox.Show("Values must be different!");
                    return;
                }

                scaleReal = new Tuple<PointF, PointF>(pointList[0], pointList[1]);

                this.Close();
                return;
            }

            int i = 0;// describes where to save new list of coordinates; need to set value for code to work; chosen arbitrary

            // If adding a new line, it will be the last line in the representitive lists.
            if (formType == FormMain.FormType.Add)
            {
                i = FormMain.allCurves.Count - 1;
            }

            // If modifying a line, get its location in the representitive lists.
            else if (formType == FormMain.FormType.Modify)
            {
                i = FormMain.localPoint.Item1;
            }

            if (curveType == FormMain.BezierType.cPoints || curveType == FormMain.BezierType.LineSegment)
            {
                FormMain.cPointsAll[i] = ScaleInputPoints(pointList);
                curveAdded = true; //line was added successfully
            }

            else if (curveType == FormMain.BezierType.pPoints || curveType == FormMain.BezierType.LeastSquares)
            {
                FormMain.pPointsAll[i] = ScaleInputPoints(pointList);
                curveAdded = true; //curve was added successfully
            }

            else if (curveType == FormMain.BezierType.Composite && formType == FormMain.FormType.Add)
            {
                FormMain.pPointsAll[i] = ScaleInputPoints(pointList);
                curveAdded = true; //curve was added successfully
            }

            else if (curveType == FormMain.BezierType.Composite && formType == FormMain.FormType.Modify)
            {
                FormMain.ModifypPointComposite(ScaleInputPoints(pointList)[0]);
            }

            this.Close();
        }


        // Add new row of coordinates to the form.
        private void AddRow()
        {
            if (formType == FormMain.FormType.Add && (tlpCoordinates.RowCount > FormMain.maxPointCount + 4))
            {
                MessageBox.Show("Maximum count of input points is " + FormMain.maxPointCount + "!");
                return;
            }

            tlpCoordinates.RowCount = tlpCoordinates.RowCount + 1;// add new empty row

            Label newLabel = new Label // new label for coordinates
            {
                Text = "" + labelType + namingCounter
            };
            tlpCoordinates.Controls.Add(newLabel);

            TextBox xCoordinate = new TextBox // new textbox for x coordinate
            {
                Name = "x" + namingCounter
            };
            tlpCoordinates.Controls.Add(xCoordinate);
            coordinates.Add(xCoordinate);

            TextBox yCoordinate = new TextBox // new textbox for y coordinate
            {
                Name = "y" + namingCounter
            };
            tlpCoordinates.Controls.Add(yCoordinate);
            coordinates.Add(yCoordinate);

            Label newEmpty = new Label // table has an empty column where scroll bar goes
            {
                Text = ""
            };
            tlpCoordinates.Controls.Add(newEmpty);

            newLabel.Anchor = AnchorStyles.Bottom; // need to fix anchors, this doeasn work

            namingCounter++;

            return;
        }


        // Scale input points so they match the scale of canva.
        private List<PointF> ScaleInputPoints(List<PointF> pointList)
        {
            List<PointF> scaleList = new List<PointF>();

            for (int i = 0; i < pointList.Count; i++)
            {
                PointF tmp = new PointF
                {
                    X = pointList[i].X / FormMain.scalePropX - FormMain.shiftVector.X,
                    Y = pointList[i].Y / FormMain.scalePropY - FormMain.shiftVector.Y
                };

                scaleList.Add(tmp);
            }

            return scaleList;
        }


        // Scale ouput points so they match the scale of canva.
        private List<PointF> ScaleOutputPoints(List<PointF> pointList)
        {
            List<PointF> scaleList = new List<PointF>();

            for (int i = 0; i< pointList.Count; i++)
            {
                PointF tmp = new PointF
                {
                    X = (pointList[i].X + FormMain.shiftVector.X) * FormMain.scalePropX,
                    Y = (pointList[i].Y + FormMain.shiftVector.Y) * FormMain.scalePropY
                };

                scaleList.Add(tmp);
            }

            return scaleList;
        }
        
    }
}