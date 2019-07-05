using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using System.IO;


namespace BezierTool
{
    public partial class FormMain : Form // contains form, all its atributes and functions
    {
        // each Bezier curve is represented in several "representitive" lists: 
        // list of construction type of each curve; "allCurves"
        // list of control points of each curve; "cPoints"
        // list of knot points of each curve (if the curve has knot points); "pPoints"
        // list of parametrization method of each curve (if the curve was interpolated); "parametrization"
        // list of type a <Composite> curve was moved; "movedCurve"
        // the i-th drawn curve is represented by the i-th position in all of the lists

        // all possible construction types of curves:
        public enum BezierType { cPoints, pPoints, LeastSquares, Composite, LineSegment, dPoints, Nothing }; 
        public static List<BezierType> allCurves = new List<BezierType>();

        public static List<List<PointF>> cPointsAll = new List<List<PointF>>();
        public static List<List<PointF>> pPointsAll = new List<List<PointF>>();

        // all possible parametrization types of interpolated curves:
        enum ParamType { Uniform, Chord, Centripetal, Nothing };
        private List<ParamType> parametrization = new List<ParamType>(); //contains parametrization types for drawn curves

        // all possible ways to move a <Composite> curve
        private enum MoveType { LeftClick, RightClick, pPoints, Nothing };
        private List<MoveType> movedCurve = new List<MoveType>();

        // all possible reasons for initialization of coordination form
        public enum FormType { Add, Modify, Output, Scale };

        public static BezierType addType = BezierType.Nothing; // type of curve to be or being added
        public static BezierType modifyCurveType = BezierType.Nothing; // type of curve to be or being modified
        public static BezierType modifyPointType = BezierType.Nothing; // type of point to be or being draged by mouse
        public static BezierType outputPointType = BezierType.Nothing; // type of points to output
        
        public static Tuple<int, int> localPoint = null; //position of a selected point in the representitive lists

        private List<PointF> cPoints = null; // list of control points of a curve
        private List<PointF> pPoints = null; // list of knot points of a curve


        private PointF cPointNew; // location of a new control point for <4 cPoints> curve 

        public const int maxPointCount = 500; // maximum count of points for <Least Squares> and <Composite> curves; chosen arbitrary
        public const float cmTOpx = (float)36.65; // for default screens at work
        int canvaWidthCm = 125; // width of canva in cm
        int canvaHeightCm = 75; // height of canva in cm
        private static PointF origin = new Point(10, 10); //origin point of the canva, to set borders (in cm)

        bool isCompositeDone = false; // indicates if the last segment of type <Composite> needs to be finished
        bool canChangeParam = false; // indicates if option to change parametrization is enabled
        bool isChangingParam = false; // indicates if parametrization of a curve is being changed
        bool canDeleteObject = false; // indicates if option to delete a curve is enabled
        bool canChangeColor = false;

        public static bool isSettingScale = false;
        public static List<PointF> scalePoints = new List<PointF>();
        public static float scalePropX = 1 / cmTOpx; 
        public static float scalePropY = 1 / cmTOpx;
        public static PointF shiftVector = new PointF(- origin.X * cmTOpx, - origin.Y * cmTOpx);

        List<Color> curveColor = new List<Color>();
        Color lastColor = Color.Black;

        String imageLocation = ""; //path of background image

        float zoomAmount = 1;
        List<List<PointF>> cPointsZoom = new List<List<PointF>>();
        List<List<PointF>> pPointsZoom = new List<List<PointF>>();
        List<PointF> dPointsZoom = new List<PointF>();



        public FormMain()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;


            pbCanva.Width = Convert.ToInt32((canvaWidthCm + origin.X) * cmTOpx);
            pbCanva.Height = Convert.ToInt32((canvaHeightCm + origin.Y) * cmTOpx);

            lblxAxis.Text = "(" + Convert.ToInt32((pbCanva.Width + shiftVector.X) * scalePropX) + ", 0)";
            lblyAxis.Text = "(" + Convert.ToInt32((pbCanva.Height + shiftVector.Y) * scalePropY) + ", 0)";
        }


        // Called, when mouse is pressed inside pbCanva. 
        // This function can be used for adding control and knot points,
        // for dragging points with mouse or for selecting a curve to output its point coordinates
        private void pbCanva_MouseDown(object sender, MouseEventArgs e)
        {
            PointF eZoom = new PointF();
            eZoom.X = e.X / zoomAmount;
            eZoom.Y = e.Y / zoomAmount;

            // addding a new control point with mouse for <4 cPoints> curve
            if ((addType == BezierType.cPoints || addType == BezierType.LineSegment) && rbMouseInput.Checked == true)
            {
                AddcPoint(eZoom);
                pbCanva.Invalidate();
            }
            
            // adding a new point with mouse for  <4 pPoints>, <Least Squares> or <Composite> curve
            else if ((addType == BezierType.pPoints || addType == BezierType.LeastSquares || addType == BezierType.Composite) && rbMouseInput.Checked == true)
            {
                AddpPoint(eZoom);
                pbCanva.Invalidate();
            }

            // dragging a control point with mouse
            else if (cPointsAll != null && modifyPointType == BezierType.cPoints)
            {
                FindLocalPoint(cPointsAll, eZoom);

                if (localPoint != null)
                {
                    ModifycPoint(e);
                    pbCanva.Invalidate();
                }
            }

            // dragging a knot point with mouse
            else if (pPointsAll != null && modifyPointType == BezierType.pPoints)
            {
                FindLocalPoint(pPointsAll, eZoom);

                if (localPoint != null)
                {
                    ModifypPoint();
                    pbCanva.Invalidate();
                }
            }

            // changing parametrization method of a drawn curve
            else if (cPointsAll != null && canChangeParam == true && isChangingParam == false)
            {
                FindLocalPoint(cPointsAll, eZoom);

                if (localPoint != null)
                {
                    ChangeParametrization();
                    pbCanva.Invalidate();
                }
            }

            // outputting control point coordinates
            else if (cPointsAll != null && outputPointType == BezierType.cPoints)
            {
                FindLocalPoint(cPointsAll, eZoom);

                if (localPoint == null)
                {
                    return;
                }

                int i = localPoint.Item1;

                // output on screen, initialize the form of coordinates
                if (rbScreenOutput.Checked == true)
                {
                    FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Output, allCurves[i]);
                    form_KeyboardAdd.ShowDialog();
                }

                // output to .txt file
                if (rbFileOutput.Checked == true)
                {
                    OutputPointsToFile(cPointsAll[i]);
                }

                outputPointType = BezierType.Nothing;
                modifyCurveType = BezierType.Nothing; // why is this needed ???
                localPoint = null;

                pbCanva.Invalidate();
            }

            // outputting knot point coordinates
            else if (pPointsAll != null && outputPointType == BezierType.pPoints)
            {
                FindLocalPoint(pPointsAll, eZoom);

                if (localPoint == null)
                {
                    return;
                }

                int i = localPoint.Item1;

                // output on screen, initialize the form of coordinates
                if (rbScreenOutput.Checked == true)
                {
                    FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Output, allCurves[i]);
                    form_KeyboardAdd.ShowDialog();
                }

                // output to .txt file
                else if (rbFileOutput.Checked == true)
                {
                    OutputPointsToFile(pPointsAll[i]);
                }

                outputPointType = BezierType.Nothing;
                modifyCurveType = BezierType.Nothing; // why is this needed ???
                localPoint = null;

                pbCanva.Invalidate();
            }

            else if (cPointsAll != null && canChangeColor == true)
            {
                FindLocalPoint(cPointsAll, eZoom);

                if (localPoint == null)
                {
                    return;
                }

                int i = localPoint.Item1;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    curveColor[i] = colorDialog1.Color;
                }

                pbCanva.Invalidate();
            }
            
            // deleting a curve
            if (cPointsAll != null && canDeleteObject == true)
            {
                FindLocalPoint(cPointsAll, eZoom);

                if (localPoint != null)
                {
                    DeleteCurve(localPoint.Item1);
                    localPoint = null;
                    pbCanva.Invalidate();
                }
            }

            //setting scale
            else if (isSettingScale == true && scalePoints.Count < 2)
            {
                scalePoints.Add(eZoom);
                SetScale();
            }
        }


        // Called, when mouse is moved inside pbCanva. 
        // This function can be used for drawing a dashed line when adding new control point for <4 cPoints> curve,
        // for for modifying points of a curve by mouse.
        private void pbCanva_MouseMove(object sender, MouseEventArgs e)
        {
            PointF eZoom = new PointF(e.X / zoomAmount, e.Y / zoomAmount);

            // get the new control point coordines for <4 cPoints> curve
            if (addType == BezierType.cPoints || addType == BezierType.LineSegment)
            {
                cPointNew = e.Location;
                pbCanva.Invalidate();
            }

            // need to intialize variables; chose 0s arbitrary
            int i = 0;
            int j = 0;

            if (localPoint != null)
            {
                i = localPoint.Item1;
                j = localPoint.Item2;
            }

            // when modifiying <4 cPoints> curve, we can just change point coordinets
            if (modifyCurveType == BezierType.cPoints)
            {
                cPointsAll[i][j] = eZoom;
                pbCanva.Invalidate();
            }

            // when modifiying knot points of <4 pPoints> or <Least Squares> curves, we need to re-calculates control points of the curve
            else if ((modifyCurveType == BezierType.pPoints || modifyCurveType == BezierType.LeastSquares) && modifyPointType == BezierType.pPoints)
            {
                pPointsAll[i][j] = eZoom;
                AddcPointsInterpolation(i);
                pbCanva.Invalidate();
            }

            // when modifiying control points of <Composite> curves, we need to make sure the curve stays C2 continuous
            else if (modifyCurveType == BezierType.Composite && modifyPointType == BezierType.cPoints)
            {
                // using left click, we can drag the control point anywhere, but the 'opposite' control point moves
                // aswell - to maintain continuity
                if (movedCurve[i] == MoveType.LeftClick)
                {
                    cPointsAll[i][j] = eZoom;

                    //starting from the fifth control point, every third point's opposite control point is two points before
                    if (j % 3 == 1 && j != 1)
                    {
                        ModifyHandleComposite(cPointsAll[i][j], cPointsAll[i][j - 1], cPointsAll[i][j - 2], j - 2);
                    }


                    //starting from the third control point, every third point's opposite control point is two points after
                    if (j % 3 == 2 && j != cPointsAll[i].Count - 2)
                    {
                        ModifyHandleComposite(cPointsAll[i][j], cPointsAll[i][j + 1], cPointsAll[i][j + 2], j + 2);
                    }
                }

                // using right click to drag a control point, no other control points will move, but to maintain  C2 continuity
                // we can only move the control point in straight line away from its opposite point
                if (movedCurve[i] == MoveType.RightClick)
                {
                    //starting from the fifth control point, every third point's opposite control point is two points before
                    if (j % 3 == 1 && j != 1)
                    {
                        ModifyHandleCompositeStraight(eZoom, cPointsAll[i][j - 1], cPointsAll[i][j - 2]);
                    }

                    //starting from the third control point, every third point's opposite control point is two points after
                    if (j % 3 == 2 && j != cPointsAll[i].Count - 2)
                    {
                        ModifyHandleCompositeStraight(eZoom, cPointsAll[i][j + 1], cPointsAll[i][j + 2]);
                    }
                }
                pbCanva.Invalidate();
            }

            // when modifiying knot points of <Composite> curves, we need to re-calculates control points of the curve 
            else if (modifyCurveType == BezierType.Composite && modifyPointType == BezierType.pPoints)
            {
                ModifypPointComposite(eZoom);
                
                movedCurve[i] = MoveType.pPoints;
                pbCanva.Invalidate();
            }
        }


        // Called, when mouse is released inside pbCanva after pressing it. 
        // This function can be used for stopping point dragging with mouse.
        private void pbCanva_MouseUp(object sender, MouseEventArgs e)
        {
            if (modifyCurveType != BezierType.Nothing)
            {
                localPoint = null;
            }
            modifyCurveType = BezierType.Nothing;
            pbCanva.Invalidate();
        }


        // Draws all graphics in this programm - points, control point polygons, all bezier curves,
        // as well as calling for functions to get needed control points.
        private void pbCanva_Paint(object sender, PaintEventArgs e)
        {
            if (allCurves != null)
            {
                lblError.Text = "" + allCurves.Count;
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // makes lines look smoother
            
            const int dashLength = 5; // describes length of dashes; chosen arbitrary
            int pointRadius = Math.Max(Convert.ToInt32(DefaultForm.pointSize * zoomAmount), 1); // radius of control points and knot points
            
            SolidBrush dPointBrush = new SolidBrush(DefaultForm.dPointsColor);
            SolidBrush pPointBrush = new SolidBrush(DefaultForm.pPointsColor);
            Pen cPointBrush = new Pen(DefaultForm.cPointsColor)
            {
                Width = Math.Max(Convert.ToInt32(DefaultForm.polygonSize * zoomAmount), 1)
            };
            Pen polygonBrush = new Pen(DefaultForm.polygonColor)
            {
                Width = Math.Max(Convert.ToInt32(DefaultForm.polygonSize * zoomAmount), 1)
            };
            Pen dashedPen = new Pen(DefaultForm.polygonColor)
            {
                DashPattern = new float[] { dashLength, dashLength },
                Width = Math.Max(Convert.ToInt32(DefaultForm.polygonSize * zoomAmount), 1)
            };
            Pen bezierPen = new Pen(lastColor)
            {
                Width = Math.Max(Convert.ToInt32(DefaultForm.curveSize * zoomAmount), 1)
            };


            // Borders and coordinate axes:
            Rectangle rect = new Rectangle(0, 0, pbCanva.Width, Convert.ToInt32(-shiftVector.Y * zoomAmount));
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, rect);

            rect = new Rectangle(0, 0, Convert.ToInt32(-shiftVector.X * zoomAmount), pbCanva.Height);
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, rect);

            e.Graphics.DrawLine(Pens.LightGray, 0, -shiftVector.Y * zoomAmount, pbCanva.Width, -shiftVector.Y * zoomAmount);
            e.Graphics.DrawLine(Pens.LightGray, 0, -shiftVector.Y * zoomAmount, pbCanva.Width, -shiftVector.Y * zoomAmount);
            e.Graphics.DrawLine(Pens.LightGray, -shiftVector.X * zoomAmount, 0, -shiftVector.X * zoomAmount, pbCanva.Height);

            e.Graphics.DrawEllipse(Pens.Black, -shiftVector.X * zoomAmount - 1, -shiftVector.Y * zoomAmount - 1, 2, 2);
            lblOrigin.Left = pbCanva.Left + Convert.ToInt32(-shiftVector.X * zoomAmount) - lblOrigin.Width;
            lblOrigin.Top = pbCanva.Top + Convert.ToInt32(-shiftVector.X * zoomAmount) - lblOrigin.Height;

            e.Graphics.DrawEllipse(Pens.Black, pbCanva.Width - 4, Convert.ToInt32(-shiftVector.Y * zoomAmount) - 1, 2, 2);
            lblxAxis.Left = pbCanva.Left + pbCanva.Width - lblxAxis.Width - 3;
            lblxAxis.Top = pbCanva.Top + Convert.ToInt32(-shiftVector.Y * zoomAmount) - lblxAxis.Height;

            e.Graphics.DrawEllipse(Pens.Black, Convert.ToInt32(-shiftVector.X * zoomAmount) - 1, pbCanva.Height - 4, 2, 2);
            lblyAxis.Left = pbCanva.Left + Convert.ToInt32(-shiftVector.X * zoomAmount) - lblyAxis.Width;
            lblyAxis.Top = pbCanva.Top + pbCanva.Height - lblyAxis.Height - 3;


            MakeZoomLists();

            if (DefaultForm.dPoints != null)
            {
                foreach (PointF dPoint in dPointsZoom)
                {
                    e.Graphics.FillEllipse(dPointBrush, dPoint.X - pointRadius, dPoint.Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                }
            }

            if (cPointsAll == null || pPointsAll == null)
            {
                return;
            }


            // if we are selecting points for <4 cPoints> curve, draw a dashed line from mouse location to previous control point
            if (cPoints != null)
            {
                // <4 cPoints> curves can't have more than 4 control points
                if ((cPoints.Count < 4 && addType == BezierType.cPoints) || (cPoints.Count < 2 && addType == BezierType.LineSegment) && rbMouseInput.Checked == true)
                {
                    PointF tmp = new PointF();
                    tmp.X = cPoints[cPoints.Count - 1].X * zoomAmount;
                    tmp.Y = cPoints[cPoints.Count - 1].Y * zoomAmount;
                    e.Graphics.DrawLine(dashedPen, tmp, cPointNew);
                }
            }


            // go through all lists of knot points
            for (int i = 0; i < pPointsAll.Count; i++)
            {
                if (pPointsAll[i] != null)
                {
                    // draw a black point for every point
                    if (cbShowcPoints.Checked == true)
                    {
                        foreach (PointF pPoint in pPointsZoom[i])
                        {
                            e.Graphics.FillEllipse(pPointBrush, pPoint.X - pointRadius, pPoint.Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                        }
                    }
                    
                    // if <4 pPoints> curve has 4 knot points, but control points haven't been calculated yet, calculate them
                    if (allCurves[i] == BezierType.pPoints && pPointsAll[i].Count == 4 && cPointsAll[i] == null)
                    {
                        AddcPointsInterpolation(i);
                    }

                    // if <Least Squares> curve has atleast 4 knot points, calculate control points
                    if (allCurves[i] == BezierType.LeastSquares && pPointsAll[i].Count >= 4)
                    {
                        AddcPointsInterpolation(i);
                    }

                    // draw <Composite> curve which hasn't been moved
                    if (allCurves[i] == BezierType.Composite && movedCurve[i] == MoveType.Nothing)
                    {
                        if (isCompositeDone == true && pPointsAll[i].Count == 2)
                        {
                            cPointsAll[i] = new List<PointF>();
                            AddOnlycPointsComposite(i);
                        }

                        // if <Composite> curve has more than 3 knot points, calculate control points
                        else if (pPointsAll[i].Count >= 3)
                        {
                            cPointsAll[i] = new List<PointF>();
                            AddcPointsComposite(i);
                        }
                    }
                }
            }

            MakeZoomLists();

            // go through all lists of control points
            for (int i = 0; i < cPointsAll.Count; i++)
            {
                if (cPointsAll[i] != null)
                {
                    //Drawing line segments:

                    if (allCurves[i] == BezierType.LineSegment && cPointsAll[i].Count == 2)
                    {
                        e.Graphics.DrawLine(Pens.Black, cPointsZoom[i][0], cPointsZoom[i][1]);
                    }


                    // Drawing circles for control points:

                    // for <4 cPoints> and <Least Squares> curves draw all control points
                    if ((allCurves[i] == BezierType.cPoints || allCurves[i] == BezierType.LeastSquares) && cbShowcPoints.Checked == true)
                    {
                        foreach (PointF cPoint in cPointsZoom[i])
                        {
                            e.Graphics.DrawEllipse(cPointBrush, cPoint.X - pointRadius, cPoint.Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                        }
                    }
                    
                    //for <4 pPoints> curves draw only middle control points, because end are also points knot points
                    else if (allCurves[i] == BezierType.pPoints && cbShowcPoints.Checked == true)
                    {
                        e.Graphics.DrawEllipse(cPointBrush, cPointsZoom[i][1].X - pointRadius, cPointsZoom[i][1].Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                        e.Graphics.DrawEllipse(cPointBrush, cPointsZoom[i][2].X - pointRadius, cPointsZoom[i][2].Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                    }

                    // for <Composite> curves draw only those control points which are not knot points -
                    // - every third knot point starting from the first is also a control point
                    else if (allCurves[i] == BezierType.Composite && cbShowcPoints.Checked == true)
                    {
                        for (int j = 0; j < cPointsAll[i].Count - 1; j++)
                        {
                            if (j % 3 != 2)
                            {
                                e.Graphics.DrawEllipse(cPointBrush, cPointsZoom[i][j + 1].X - pointRadius, cPointsZoom[i][j + 1].Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                            }
                        }
                    }


                    //Drawing control point polygons / handle lines:

                    if (cPointsAll[i].Count > 1 && cbShowcPoints.Checked == true && (allCurves[i] == BezierType.cPoints || allCurves[i] == BezierType.LeastSquares || allCurves[i] == BezierType.pPoints))
                    {
                        e.Graphics.DrawLines(polygonBrush, cPointsZoom[i].ToArray());
                    }

                    //for <Composite> curves, draw only handle lines
                    else if (allCurves[i] == BezierType.Composite && cbShowcPoints.Checked == true)
                    {
                        for (int j = 0; j < cPointsAll[i].Count - 1; j++)
                        {
                            // connect every control point to the next, 
                            //exept every third starting from first and the last
                            if (j % 3 != 1)
                            {
                                e.Graphics.DrawLine(polygonBrush, cPointsZoom[i][j], cPointsZoom[i][j + 1]);
                            }
                        }
                    }


                    //Drawing all bezier curves:

                    bezierPen.Color = curveColor[i];

                    // <4 cPoints>, <4 pPoints> and <Least Squares> curves have 4 control points
                    if (cPointsAll[i].Count == 4 && (allCurves[i] == BezierType.cPoints || allCurves[i] == BezierType.LeastSquares || allCurves[i] == BezierType.pPoints))
                    {
                        e.Graphics.DrawBezier(bezierPen, cPointsZoom[i][0], cPointsZoom[i][1], cPointsZoom[i][2], cPointsZoom[i][3]);
                    }

                    // draw each segment of <Composite> curve
                    else if (allCurves[i] == BezierType.Composite)
                    {
                        for (int j = 0; j < cPointsAll[i].Count - 3; j += 3)
                        {
                            e.Graphics.DrawBezier(bezierPen, cPointsZoom[i][j], cPointsZoom[i][j + 1], cPointsZoom[i][j + 2], cPointsZoom[i][j + 3]);
                        }
                    }

                }
            }
        }

        
        // Ensures main form is responsive.
        private void FormMain_Resize(object sender, EventArgs e)
        {
            int formWidth = this.Width;
            int formHeight = this.Height;

            // px values are used to make margins
            panel_tools.Left = formWidth - panel_tools.Width - 20; 
            panel_bottom.Left = formWidth - panel_bottom.Width - 25;
            panel_bottom.Top = formHeight - panel_bottom.Height - 65;
            pnlCanva.Width = formWidth - panel_tools.Width - 35;
            pnlCanva.Height = formHeight - 75;
            pnlMessage.Top = formHeight - 65;
            pnlMessage.Width = formWidth - 30;
            nudZoom.Left = pnlMessage.Width - 45;
            lblZoom.Left = pnlMessage.Width - 80;
            
            lblxAxis.Left = pbCanva.Width - lblxAxis.Width;
            lblxAxis.Top = Convert.ToInt32(-shiftVector.Y * zoomAmount) - lblxAxis.Height;

            lblyAxis.Left = Convert.ToInt32(-shiftVector.X * zoomAmount) - lblyAxis.Width;
            lblyAxis.Top = pbCanva.Height - lblyAxis.Height;
        }


        // ???
        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            zoomAmount = (float)nudZoom.Value / 100;

            pbCanva.Width = Convert.ToInt32(zoomAmount * Convert.ToInt32((canvaWidthCm + origin.X) * cmTOpx));
            pbCanva.Height = Convert.ToInt32(zoomAmount * Convert.ToInt32((canvaHeightCm + origin.Y) * cmTOpx));

            pbCanva.Invalidate();
        }


        // ???
        private void btnDefault_Click(object sender, EventArgs e)
        {
            DefaultForm defaultForm = new DefaultForm();
            defaultForm.ShowDialog();

            if (isSettingScale == true)
            {
                ButtonPress();
                scalePoints = new List<PointF>();
                FormCoordinates.scaleReal = null;
                SetScale();
            }

            pbCanva.Invalidate();
        }
        

        // Uploads background image for pbCanva.
        private void btnUploadBackground_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            try
            {
                OpenFileDialog dialog = new OpenFileDialog 
                { 
                    Filter = "All Images|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pbCanva.ImageLocation = imageLocation;
                    cbShowBackground.Checked = true;
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Image upload error!");
            }
        }


        // Changes the visibility of uploaded background picture of pbCanva.
        private void cbShowBackground_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbShowBackground.Checked == false)
            {
                pbCanva.ImageLocation = "";
            }

            else
            {
                pbCanva.ImageLocation = imageLocation;
            }
        }

        // ???
        private void btnNewSegment_Click(object sender, EventArgs e)
        {
            ButtonPress();

            addType = BezierType.LineSegment;

            if(rbMouseInput.Checked == true)
            {
                cPoints = null;
            }


            if (rbKeyboardInput.Checked == true)
            {
                NewCurve(BezierType.LineSegment);

                // when inputting points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Add, addType);
                form_KeyboardAdd.ShowDialog();


                // an error or cancelation occured in the form of coordinates and no curves were added
                if (FormCoordinates.curveAdded == false)
                {
                    DeleteCurve(allCurves.Count - 1); // reverse the actions of newCurve() function
                    return;
                }

                pbCanva.Invalidate();
            }

            if (rbFileInput.Checked == true)
            {
                btnImportAll_Click(sender, e);
                pbCanva.Invalidate();
            }
        }


        // Start a new <4 cPoints> curve.
        private void btnNew4cPoints_Click(object sender, EventArgs e)
        {
            ButtonPress();
            addType = BezierType.cPoints;

            if (rbMouseInput.Checked == true)
            {
                cPoints = null;
            }

            if (rbKeyboardInput.Checked == true)
            {
                NewCurve(BezierType.cPoints);

                // when inputting points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Add, addType);
                form_KeyboardAdd.ShowDialog();


                // an error or cancelation occured in the form of coordinates and no curves were added
                if (FormCoordinates.curveAdded == false)
                {
                    DeleteCurve(allCurves.Count - 1); // reverse the actions of newCurve() function
                    return;
                }

                pbCanva.Invalidate();
            }

            if (rbFileInput.Checked == true)
            {
                btnImportAll_Click(sender, e);
                pbCanva.Invalidate();
            }
        }


        // Start a new <4 pPoints> curve.
        private void btnNew4pPoints_Click(object sender, EventArgs e)
        {
            ButtonPress();

            addType = BezierType.pPoints;

            if (rbMouseInput.Checked == true)
            {
                pPoints = null;
            }

            if (rbKeyboardInput.Checked == true)
            {
                NewCurve(BezierType.pPoints);

                // when inputting points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Add, addType);
                form_KeyboardAdd.ShowDialog();

                // an error or cancelation occured in the form of coordinates and no curves were added
                if (FormCoordinates.curveAdded == false)
                {
                    DeleteCurve(allCurves.Count - 1); 
                    return;
                }

                pbCanva.Invalidate();
            }

            if (rbFileInput.Checked == true)
            {
                btnImportAll_Click(sender, e);
                pbCanva.Invalidate();
            }
        }


        // Start a new <Least Squares> curve.
        private void btnNewLeastSquares_Click(object sender, EventArgs e)
        {
            ButtonPress();

            addType = BezierType.LeastSquares;

            if (rbMouseInput.Checked == true)
            {
                pPoints = null;
            }

            if (rbKeyboardInput.Checked == true)
            {
                NewCurve(BezierType.LeastSquares);

                // when inputting points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Add, addType);
                form_KeyboardAdd.ShowDialog();

                // an error or cancelation occured in the form of coordinates and no curves were added
                if (FormCoordinates.curveAdded == false)
                {
                    DeleteCurve(allCurves.Count - 1); // reverse the actions of newCurve() function
                    return;
                }

                pbCanva.Invalidate();
            }

            if (rbFileInput.Checked == true)
            {
                btnImportAll_Click(sender, e);
                pbCanva.Invalidate();
            }
        }


        // Start a new <Composite> curve.
        private void btnNewComposite_Click(object sender, EventArgs e)
        {
            ButtonPress();

            addType = BezierType.Composite;

            if (rbMouseInput.Checked == true)
            {
                pPoints = null;
            }

            if (rbKeyboardInput.Checked == true)
            {
                NewCurve(BezierType.Composite);

                // when inputting points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Add, addType);
                form_KeyboardAdd.ShowDialog();

                // an error or cancelation occured in the form of coordinates and no curves were added
                if (FormCoordinates.curveAdded == false)
                {
                    DeleteCurve(allCurves.Count - 1); // reverse the actions of newCurve() function
                    return;
                }

                isCompositeDone = true;

                pbCanva.Invalidate();
            }

            if (rbFileInput.Checked == true)
            {
                btnImportAll_Click(sender, e);
                isCompositeDone = true;
                pbCanva.Invalidate();
            }
        }


        // When a <Composite> curve is indicated as done, draw the last segment of it. 
        private void btnDoneComposite_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            isCompositeDone = true;
            pbCanva.Invalidate();
        }


        // Allow to drag control points by mouse.
        private void btnModifycPoints_Click(object sender, EventArgs e)
        {
            ButtonPress();

            modifyPointType = BezierType.cPoints;
        }


        // Allow to drag knot points by mouse.
        private void btnModifypPoints_Click(object sender, EventArgs e)
        {
            ButtonPress();

            modifyPointType = BezierType.pPoints;
        }


        // Allow to change curve parametrization method.
        private void btnChangeParam_Click(object sender, EventArgs e)
        {
            ButtonPress();

            canChangeParam = true;
        }


        // Change parametrization method and redraw the curve.
        private void rbUniform_CheckedChanged(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (isChangingParam == false)
            {
                return;
            }

            int i = localPoint.Item1;

            if (rbUniform.Checked == true)
            {
                parametrization[i] = ParamType.Uniform;
            }

            else if (rbChord.Checked == true)
            {
                parametrization[i] = ParamType.Chord;
            }

            else if (rbCentripetal.Checked == true)
            {
                parametrization[i] = ParamType.Centripetal;
            }

            AddcPointsInterpolation(i);
            pbCanva.Invalidate();
        }


        // Change parametrization method and redraw the curve.
        private void rbChord_CheckedChanged(object sender, EventArgs e)
        {
            rbUniform_CheckedChanged(sender, e);
        }
        

        //???
        private void cbShowcPoints_CheckedChanged(object sender, EventArgs e)
        {
            pbCanva.Invalidate();
        }
        

        // ???
        private void pnlLastColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                lastColor = colorDialog1.Color;
            }

            pnlLastColor.BackColor = lastColor;
        }


        // Enable the option to change color of a curve.
        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            ButtonPress();

            canChangeColor = true;
        }


        // Enable ehe option to output control point coordinates.
        private void btnOutputcPoints_Click(object sender, EventArgs e)
        {
            ButtonPress();

            outputPointType = BezierType.cPoints;
        }


        // Enable option to output knot point coordinates.
        private void btnOutputpPoints_Click(object sender, EventArgs e)
        {
            ButtonPress();

            outputPointType = BezierType.pPoints;
        }
        

        // ???
        private void btnImportAll_Click(object sender, EventArgs e)
        {
            BezierType listType = BezierType.Nothing;
            ParamType paramType = ParamType.Nothing;
            MoveType moveType = MoveType.Nothing;
            cPoints = null;

            PointF point = new PointF();
            int index;

            string path = "";
            string line = "";
            string xText = "", yText = "", subText = "";

            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Title = "Open Text File",
                    Filter = "TXT files|*.txt"
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
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("scalePropX"))
                        {
                            try
                            {
                                scalePropX = Convert.ToSingle(line.Substring(line.IndexOf(' ')));
                            }

                            catch (Exception)
                            {
                                lblError.ForeColor = Color.Red;
                                lblError.Text = "Error: .txt file was not correct!";
                            }
                        }

                        else if (line.Contains("scalePropY"))
                        {
                            try
                            {
                                scalePropY = Convert.ToSingle(line.Substring(line.IndexOf(' ')));
                            }

                            catch (Exception)
                            {
                                lblError.ForeColor = Color.Red;
                                lblError.Text = "Error: .txt file was not correct!";
                            }
                        }

                        else if (line.Contains("shiftVector"))
                        {
                            try
                            {
                                index = line.IndexOf('X') + 2;
                                xText = line.Substring(index, line.IndexOf(',') - index);
                                point.X = Convert.ToSingle(xText);

                                index = line.IndexOf('Y') + 2;
                                yText = line.Substring(index, line.IndexOf('}') - index);
                                point.Y = Convert.ToSingle(yText);

                                shiftVector = point;
                            }

                            catch (Exception)
                            {
                                lblError.ForeColor = Color.Red;
                                lblError.Text = "Error: .txt file was not correct!";
                            }
                        }

                        else if (line.Contains("D"))
                        {
                            try
                            {
                                index = line.IndexOf('(') + 1;
                                xText = line.Substring(index, line.IndexOf(';') - index);
                                point.X = Convert.ToSingle(xText) / scalePropX - shiftVector.X;

                                index = line.IndexOf(';') + 2;
                                yText = line.Substring(index, line.IndexOf(')') - index);
                                point.Y = Convert.ToSingle(yText) / scalePropY - shiftVector.Y;

                                DefaultForm.dPoints.Add(point);
                            }

                            catch (Exception)
                            {
                                lblError.ForeColor = Color.Red;
                                lblError.Text = "Error: .txt file was not correct!";
                            }
                        }

                        else if (line.Contains("cPoints"))
                        {
                            listType = BezierType.cPoints;
                        }
                        else if (line.Contains("pPoints"))
                        {
                            listType = BezierType.pPoints;
                            subText = file.ReadLine();
                            if (subText == "Uniform")
                            {
                                paramType = ParamType.Uniform;
                            }
                            else if (subText == "Chord")
                            {
                                paramType = ParamType.Chord;
                            }
                            else if (subText == "Centripetal")
                            {
                                paramType = ParamType.Centripetal;
                            }
                        }
                        else if (line.Contains("LeastSquares"))
                        {
                            listType = BezierType.LeastSquares;
                            subText = file.ReadLine();
                            if (subText == "Uniform")
                            {
                                paramType = ParamType.Uniform;
                            }
                            else if (subText == "Chord")
                            {
                                paramType = ParamType.Chord;
                            }
                            else if (subText == "Centripetal")
                            {
                                paramType = ParamType.Centripetal;
                            }
                        }
                        else if (line.Contains("Composite"))
                        {
                            listType = BezierType.Composite;
                            subText = file.ReadLine();
                            if (subText == "Nothing")
                            {
                                moveType = MoveType.Nothing;
                            }
                            else if (subText == "LeftClick")
                            {
                                moveType = MoveType.LeftClick;
                            }
                            else if (subText == "RightClick")
                            {
                                moveType = MoveType.RightClick;
                            }
                            else if (subText == "pPoints")
                            {
                                moveType = MoveType.pPoints;
                            }
                        }
                        else if (line.Contains("LineSegment"))
                        {
                            listType = BezierType.LineSegment;
                        }

                        if (line.Contains("<") && !line.Contains("dPoints"))
                        {
                            if (cPoints != null)
                            {
                                for (int i = 0; i < cPoints.Count; i++)
                                {
                                    PointF tmp = new PointF();
                                    tmp.X = cPoints[i].X / scalePropX - shiftVector.X;
                                    tmp.Y = cPoints[i].Y / scalePropY - shiftVector.Y;

                                    cPoints[i] = tmp;

                                }

                                if (pPoints != null)
                                {
                                    for (int i = 0; i < pPoints.Count; i++)
                                    {
                                        PointF tmp = new PointF();
                                        tmp.X = pPoints[i].X / scalePropX - shiftVector.X;
                                        tmp.Y = pPoints[i].Y / scalePropY - shiftVector.Y;

                                        pPoints[i] = tmp;
                                    }
                                }

                                cPointsAll[cPointsAll.Count - 1] = cPoints;
                                pPointsAll[pPointsAll.Count - 1] = pPoints;
                            }

                            NewCurve(listType);

                            if (listType == BezierType.pPoints || listType == BezierType.LeastSquares)
                            {
                                parametrization[parametrization.Count - 1] = paramType;
                            }

                            if (listType == BezierType.Composite)
                            {
                                movedCurve[movedCurve.Count - 1] = moveType;
                            }

                        }

                        else if (line.Contains("C"))
                        {
                            if (cPoints == null)
                            {
                                cPoints = new List<PointF>();
                            }

                            try
                            {
                                index = line.IndexOf('(') + 1;
                                xText = line.Substring(index, line.IndexOf(';') - index);
                                point.X = Convert.ToSingle(xText);

                                index = line.IndexOf(';') + 2;
                                yText = line.Substring(index, line.IndexOf(')') - index);
                                point.Y = Convert.ToSingle(yText);

                                cPoints.Add(point);
                            }

                            catch (Exception)
                            {
                                lblError.ForeColor = Color.Red;
                                lblError.Text = "Error: .txt file was not correct!";
                            }
                        }

                        else if (line.Contains("P"))
                        {
                            if (pPoints == null)
                            {
                                pPoints = new List<PointF>();
                            }

                            try
                            {
                                index = line.IndexOf('(') + 1;
                                xText = line.Substring(index, line.IndexOf(';') - index);
                                point.X = Convert.ToSingle(xText);

                                index = line.IndexOf(';') + 2;
                                yText = line.Substring(index, line.IndexOf(')') - index);
                                point.Y = Convert.ToSingle(yText);

                                pPoints.Add(point);
                            }

                            catch (Exception)
                            {
                                lblError.ForeColor = Color.Red;
                                lblError.Text = "Error: .txt file was not correct!";
                            }
                        }
                    }

                    if (cPoints != null)
                    {
                        for (int i = 0; i < cPoints.Count; i++)
                        {
                            PointF tmp = new PointF();
                            tmp.X = cPoints[i].X / scalePropX - shiftVector.X;
                            tmp.Y = cPoints[i].Y / scalePropY - shiftVector.Y;

                            cPoints[i] = tmp;
                        }

                        if (pPoints != null)
                        {
                            for (int i = 0; i < pPoints.Count; i++)
                            {
                                PointF tmp = new PointF();
                                tmp.X = pPoints[i].X / scalePropX - shiftVector.X;
                                tmp.Y = pPoints[i].Y / scalePropY - shiftVector.Y;

                                pPoints[i] = tmp;
                            }
                        }

                        cPointsAll[cPointsAll.Count - 1] = cPoints;
                        pPointsAll[pPointsAll.Count - 1] = pPoints;
                    }
                }

                pbCanva.Invalidate();
            }

            else
            {
                MessageBox.Show("File upload error!");
            }
        }


        // ???
        private void btnExportAll_Click(object sender, EventArgs e)
        {
            string path = "";

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files|*.txt";
            dialog.Title = "Export All Objects";
            dialog.FileName = "BezierTool";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName != "")
                {
                    FileStream fs = File.Create(dialog.FileName);
                    path = Path.Combine(Path.GetDirectoryName(dialog.FileName), dialog.FileName);
                    fs.Close();
                }
            }

            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Error: Output error!";
                return;
            }

            using (var file = new StreamWriter(path))
            {
                file.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "\n");
                file.WriteLine("scalePropX: " + scalePropX);
                file.WriteLine("scalePropY: " + scalePropY);
                file.WriteLine("shiftVector: " + shiftVector + "\n \n");

                if (DefaultForm.dPoints != null)
                {
                    file.WriteLine("<dPoints>: \n");
                    for (int i = 0; i < DefaultForm.dPoints.Count; i++)
                    {
                        double scaledX = Math.Round((DefaultForm.dPoints[i].X + shiftVector.X) * scalePropX, 4);
                        double scaledY = Math.Round((DefaultForm.dPoints[i].Y + shiftVector.Y) * scalePropY, 4);

                        string line = "D" + (i + 1) + ": (" + scaledX + "; " + scaledY + ")"; // in each line write coordinates of one control point
                        file.WriteLine(line);
                    }
                }

                file.WriteLine("\n");

                if (allCurves == null)
                {
                    return;
                }

                for (int i = 0; i < allCurves.Count; i++)
                {
                    file.WriteLine("<" + allCurves[i] + ">:");

                    if (allCurves[i] == BezierType.pPoints || allCurves[i] == BezierType.LeastSquares)
                    {
                        file.WriteLine("" + parametrization[i]);
                    }

                    if (allCurves[i] == BezierType.Composite)
                    {
                        file.WriteLine("" + movedCurve[i]);
                    }

                    file.WriteLine();

                    for (int j = 0; j < cPointsAll[i].Count; j++)
                    {
                        double scaledX = Math.Round((cPointsAll[i][j].X + shiftVector.X) * scalePropX, 4);
                        double scaledY = Math.Round((cPointsAll[i][j].Y + shiftVector.Y) * scalePropY, 4);

                        string line = "C" + (j + 1) + ": (" + scaledX + "; " + scaledY + ")"; // in each line write coordinates of one control point
                        file.WriteLine(line);
                    }

                    file.WriteLine();


                    if (allCurves[i] == BezierType.pPoints || allCurves[i] == BezierType.LeastSquares || allCurves[i] == BezierType.Composite)
                    {
                        for (int j = 0; j < pPointsAll[i].Count; j++)
                        {
                            double scaledX = Math.Round((pPointsAll[i][j].X + shiftVector.X) * scalePropX, 4);
                            double scaledY = Math.Round((pPointsAll[i][j].Y + shiftVector.Y) * scalePropY, 4);

                            string line = "P" + (j + 1) + ": (" + scaledX + "; " + scaledY + ")"; // in each line write coordinates of one control point
                            file.WriteLine(line);
                        }
                    }

                    file.WriteLine("\n");
                }
                file.Close();
            }
        }
        

        // Enable option to delete a curve.
        private void btnDeleteCurve_Click(object sender, EventArgs e)
        {
            ButtonPress();

            canDeleteObject = true;
        }


        // Reset main form to its inial state, clean pbCanva and reset all variables.
        private void btnResetAll_Click(object sender, EventArgs e)
        {
            ButtonPress();
            isSettingScale = false;

            string message = "Do you want to reset this form?";
            string title = "Reset all";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);

            if (result != DialogResult.OK)
            {
                return;
            }

            DefaultForm.ResetAll();

            allCurves = new List<BezierType>();
            movedCurve = new List<MoveType>();
            parametrization = new List<ParamType>();
            curveColor = new List<Color>();

            cPointsAll = new List<List<PointF>>();
            pPointsAll = new List<List<PointF>>();
            
            FormCoordinates.scaleReal = null;

            cPoints = null;
            pPoints = null;
            scalePoints = null;
            localPoint = null;

            rbMouseInput.Checked = true;
            rbMouseModify.Checked = true;
            lastColor = Color.Black;

            imageLocation = "";
            lblError.Text = "";
            cbShowBackground.Checked = false;
            cbShowcPoints.Checked = true;

            FormMain_Resize(sender, e);

            pbCanva.Invalidate();
        }


        // ???
        private void ButtonPress()
        {
            lblError.Text = "";

            canChangeParam = false;
            isChangingParam = false;
            canDeleteObject = false;
            canChangeColor = false;
            addType = BezierType.Nothing;
            modifyPointType = BezierType.Nothing;
            modifyCurveType = BezierType.Nothing;
            outputPointType = BezierType.Nothing;

        }
        

        // ???
        private void SetScale()
        {
            lblError.ForeColor = Color.Black;
            lblError.Text = "Message: Choose two points on screen with mouse! (" + (2 - scalePoints.Count) + " points left)";

            if (scalePoints.Count != 2)
            {
                return;
            }

            if (FormCoordinates.scaleReal == null)
            {
                FormCoordinates fc = new FormCoordinates(FormType.Scale, BezierType.Nothing);
                fc.ShowDialog();
            }

            if (FormCoordinates.scaleReal == null)
            {
                //kkādu pazinojumu?
                return;
            }

            PointF scaleVector = new PointF
            (
                FormCoordinates.scaleReal.Item2.X - FormCoordinates.scaleReal.Item1.X,
                FormCoordinates.scaleReal.Item2.Y - FormCoordinates.scaleReal.Item1.Y
            );

            PointF screenVector = new PointF
            (
                scalePoints[1].X - scalePoints[0].X,
                scalePoints[1].Y - scalePoints[0].Y
            );

            scalePropX = scaleVector.X / screenVector.X;
            scalePropY = scaleVector.Y / screenVector.Y;

            shiftVector.X = FormCoordinates.scaleReal.Item1.X / scalePropX - scalePoints[0].X;
            shiftVector.Y = FormCoordinates.scaleReal.Item1.Y / scalePropY - scalePoints[0].Y;

            if (DefaultForm.dPoints != null)
            {
                for (int i = 0; i < DefaultForm.dPoints.Count; i++)
                {
                    PointF tmp = new PointF();
                    tmp.X = DefaultForm.dPoints[i].X / scalePropX - shiftVector.X;
                    tmp.Y = DefaultForm.dPoints[i].Y / scalePropY - shiftVector.Y;

                    DefaultForm.dPoints[i] = tmp;
                }
            }

            pbCanva.Invalidate();
        }


        // ???
        private void MakeZoomLists()
        {
            PointF tmp = new PointF();
            dPointsZoom = new List<PointF>();
            cPointsZoom = new List<List<PointF>>();
            pPointsZoom = new List<List<PointF>>();

            if (DefaultForm.dPoints != null)
            {
                for (int i = 0; i < DefaultForm.dPoints.Count; i++)
                {
                    tmp.X = DefaultForm.dPoints[i].X * zoomAmount;
                    tmp.Y = DefaultForm.dPoints[i].Y * zoomAmount;
                    dPointsZoom.Add(tmp);
                }
            }


            if (cPointsAll != null)
            {
                for (int i = 0; i < cPointsAll.Count; i++)
                {
                    List<PointF> cList = new List<PointF>();
                    cPointsZoom.Add(cList);
                    if (cPointsAll[i] != null)
                    {
                        for (int j = 0; j < cPointsAll[i].Count; j++)
                        {
                            tmp.X = cPointsAll[i][j].X * zoomAmount;
                            tmp.Y = cPointsAll[i][j].Y * zoomAmount;
                            cPointsZoom[i].Add(tmp);
                        }
                    }
                }
            }


            if (pPointsAll != null)
            {
                for (int i = 0; i < pPointsAll.Count; i++)
                {
                    List<PointF> pList = new List<PointF>();
                    pPointsZoom.Add(pList);
                    if (pPointsAll[i] != null)
                    {
                        for (int j = 0; j < pPointsAll[i].Count; j++)
                        {
                            tmp.X = pPointsAll[i][j].X * zoomAmount;
                            tmp.Y = pPointsAll[i][j].Y * zoomAmount;
                            pPointsZoom[i].Add(tmp);
                        }
                    }
                }
            }

            return;
        }


        // Start a new curve, add its parametrs to representitive lists.
        private void NewCurve(BezierType curveType)
        {
            allCurves.Add(curveType);

            cPoints = null;
            pPoints = null;

            localPoint = null;

            isChangingParam = false;
            isCompositeDone = false;
            canDeleteObject = false;

            modifyPointType = BezierType.Nothing;
            modifyCurveType = BezierType.Nothing;
            outputPointType = BezierType.Nothing;

            cPointsAll.Add(null);
            pPointsAll.Add(null);
            movedCurve.Add(MoveType.Nothing);
            curveColor.Add(lastColor);

            if (curveType == BezierType.cPoints || curveType == BezierType.Composite || curveType == BezierType.LineSegment)
            {
                parametrization.Add(ParamType.Nothing);
            }

            else if (curveType == BezierType.pPoints || curveType == BezierType.LeastSquares)
            {
                if (rbUniform.Checked == true)
                {
                    parametrization.Add(ParamType.Uniform);
                }

                else if (rbChord.Checked == true)
                {
                    parametrization.Add(ParamType.Chord);
                }

                else if (rbCentripetal.Checked == true)
                {
                    parametrization.Add(ParamType.Centripetal);
                }
            }

            return;
        }


        // Reverse action of newCurve() function.
        // Used when a curve was deleted or when adding a curve by keyboard had an error or cancelation.
        private void DeleteCurve(int i)
        {
            addType = BezierType.Nothing;
            allCurves.RemoveAt(i);
            movedCurve.RemoveAt(i);
            cPointsAll.RemoveAt(i);
            pPointsAll.RemoveAt(i);
            parametrization.RemoveAt(i);

            canDeleteObject = false;

            return;
        }


        // Add new control point by mouse to the last curve.
        private void AddcPoint(PointF mouseLocation)
        {
            // adding the first control point of curve
            if (cPoints == null)
            {
                NewCurve(addType);
                cPoints = new List<PointF> { mouseLocation };
                cPointsAll[cPointsAll.Count - 1] = cPoints;
            }

            // to avoid accidental double clicks
            else if (cPoints.Count < 4 && cPoints[cPoints.Count - 1] != mouseLocation && addType == BezierType.cPoints)
            {
                cPoints.Add(mouseLocation);
            }

            else if (cPoints.Count < 2 && cPoints[cPoints.Count - 1] != mouseLocation && addType == BezierType.LineSegment)
            {
                cPoints.Add(mouseLocation);
            }

            return;
        }


        // Add new knot point by mouse to the last curve.
        private void AddpPoint(PointF mouseLocation)
        {
            // adding the first control point of curve
            if (pPoints == null)
            {
                NewCurve(addType);
                pPoints = new List<PointF> { mouseLocation };
                pPointsAll[pPointsAll.Count - 1] = pPoints;
                
                return;
            }

            // to avoid accidental double clicks
            if (pPoints[pPoints.Count - 1] == mouseLocation)
            {
                return;
            }

            //<4 pPoints> curves can't have more than 4 knot points
            if (addType == BezierType.pPoints && pPoints.Count >= 4)
            {
                return;
            }

            // can't add any more knot points to a finished <Composite> curve
            if (addType == BezierType.Composite && isCompositeDone == true)
            {
                return;
            }

            else if ((addType == BezierType.LeastSquares || addType == BezierType.Composite) && pPoints.Count > maxPointCount)
            {
                return;
            }

            pPoints.Add(mouseLocation);

            return;
        }


        // Calculate and add control points for <Composite> curves with three or more knot points.
        private void AddcPointsComposite(int i)
        {
            int  pCount = pPointsAll[i].Count;

            // first control point is the first knot point:
            cPointsAll[i].Add(pPointsAll[i][0]);

            // add first handle:
            PointF firstHandle = new PointF();
            firstHandle = GetFirstHandle(pPointsAll[i][0], pPointsAll[i][1], pPointsAll[i][2]);
            cPointsAll[i].Add(GetVeryFirstHandle(pPointsAll[i][0], firstHandle, pPointsAll[i][1]));

            // add three new control points for every knot point starting with the third -
            // - every knot point is also a control point and for every but first and last knot point, 
            // we get two handles:
            for (int j = 2; j < pPointsAll[i].Count; j++)
            {
                cPointsAll[i].Add(GetFirstHandle(pPointsAll[i][j - 2], pPointsAll[i][j - 1], pPointsAll[i][j]));
                cPointsAll[i].Add(pPointsAll[i][j - 1]);
                cPointsAll[i].Add(GetSecondHandle(pPointsAll[i][j - 2], pPointsAll[i][j - 1], pPointsAll[i][j]));
            }

            // every <Composite> curve except the last one always needs to be finished
            // that means, it should have three times (every point is a control point and has two handles) 
            // minus two (each end point doesn't have one handle) more control points than knot points
            if ((i != allCurves.Count - 1 && cPointsAll[i].Count < pCount * 3 - 2) || (i == allCurves.Count - 1 && isCompositeDone == true))
            {
                PointF veryLastHandle;
                veryLastHandle = GetVeryLastHandle(pPointsAll[i][pCount - 2], cPointsAll[i][cPointsAll[i].Count - 1], pPointsAll[i][pCount - 1]);
                cPointsAll[i].Add(veryLastHandle);
                cPointsAll[i].Add(pPointsAll[i][pPointsAll[i].Count - 1]);
            }

            return;
        }


        // Finish a <Composite> curve, that has only two control points, but is indicated as finished.
        private void AddOnlycPointsComposite(int i)
        {
            PointF firstcPoint = new PointF();
            PointF firstHandle = new PointF();
            PointF secondHandle = new PointF();
            PointF lastcPoint = new PointF();

            // first and last control points are knot points of the curve:
            firstcPoint = pPointsAll[i][0];
            lastcPoint = pPointsAll[i][1];

            float sin60 = (float)Math.Sin(Math.PI / 3);
            float cos60 = (float)Math.Cos(Math.PI / 3);

            // each control point will be the midpoint of firstcPoint-lastcPoint curve segment, rotated by 60 degrees
            // first we find oordinates of the midpoint:
            float xMidpoint = (float)0.5 * (lastcPoint.X - firstcPoint.X);
            float yMidpoint = (float)0.5 * (lastcPoint.Y - firstcPoint.Y);

            // then we rotate the midpoint by 60 degrees:
            firstHandle.X = cos60 * xMidpoint - sin60 * yMidpoint + firstcPoint.X;
            firstHandle.Y = sin60 * xMidpoint + cos60 * yMidpoint + firstcPoint.Y;

            // for control points of the curve to be on different sides, change the signs for second handle:
            secondHandle.X = cos60 * -xMidpoint - sin60 * -yMidpoint + lastcPoint.X;
            secondHandle.Y = sin60 * -xMidpoint + cos60 * -yMidpoint + lastcPoint.Y;

            cPointsAll[i].Add(firstcPoint);
            cPointsAll[i].Add(firstHandle);
            cPointsAll[i].Add(secondHandle);
            cPointsAll[i].Add(lastcPoint);

            pbCanva.Invalidate();

            return;
        }


        // Add the very first control point that's not a knot point for <Composite> curve with at least three knot points.
        private PointF GetVeryFirstHandle(PointF firstpPoint, PointF nextHandle, PointF secondpPoint)
        {
            PointF veryFirstHandle = new PointF();

            // coordinates of the very first handle is calculated from first, third and fourth control points of the  <Composite> curve

            // We can look at these calculations as vector operations. 
            // First, we calculate dot product of vectors secondpPoint-nextHandle and secondpPoint-firstpPOint:
            float dotProduct = (nextHandle.X - secondpPoint.X) * (firstpPoint.X - secondpPoint.X) +
                                (nextHandle.Y - secondpPoint.Y) * (firstpPoint.Y - secondpPoint.Y);

            //We need to find how long the vector v1 from veryFirstHandle to nextHandle needs to be, so that the middle control points are symmetrical.
            //The symmetry can be achieved if the vector v1 is parallel to vector v2 from secondpPoint to firstpPoint  
            //and has the length: length(v2) - 2*length(projection of vector secondpPoint-nextHandle on to v2). One projection length for each side.
            //Using projection formula, we get: proportion = |v2| - 2 * dot / |v2| . We will multiply this proportion by unit vector
            //parallel to vector v2, which can be expressed as v2 / |v2|. If we devide our proportion with |v2| from the unit vector,
            //we get: proportion = 1 - 2 * dot / |v2|^2

            //That means, the length of the vector we will add equals 
            float prop = 1 - 2 * dotProduct / (float)Math.Pow(GetLength(firstpPoint, secondpPoint), 2);

            //Lastly, to point nextHandle we add vector parallel to vector firstpPoint-secondpPoint scaled by the proportion:
            veryFirstHandle.X = nextHandle.X + prop * (firstpPoint.X - secondpPoint.X);
            veryFirstHandle.Y = nextHandle.Y + prop * (firstpPoint.Y - secondpPoint.Y);

            // We have achieved a "symmetrical" point to nextHandle; both of these points are on the same side of the bezier curve.

            return veryFirstHandle;
        }


        // Calculate coordinates of first handle for <Composite> curves in a way to ensure C2 continuity.
        private PointF GetFirstHandle(PointF prevpPoint, PointF thispPoint, PointF nextpPoint)
        {
            PointF firstHandle = new PointF();
            float lengthPrevThis = GetLength(prevpPoint, thispPoint);
            float lengthThisNext = GetLength(thispPoint, nextpPoint);

            // Distance from first to second handle is half the distance from prevpPoint (a) to nextpPoint (b).
            // The proportions of the length of each handle are the same as proportion ab/bc, where b thispPoint.
            // Methods of calculations for distances and angles of handles can be different and there isn't one best method. 
            // I have discovered that this method works nice most of the time and isn't computationally expensive.

            float proportion = (float)0.5 * lengthPrevThis / (lengthPrevThis + lengthThisNext);

            firstHandle.X = thispPoint.X + proportion * (prevpPoint.X - nextpPoint.X);
            firstHandle.Y = thispPoint.Y + proportion * (prevpPoint.Y - nextpPoint.Y);

            return firstHandle;
        }


        // Calculate coordinates of second handle for <Composite> curves in a way to ensure C2 continuity.
        private PointF GetSecondHandle(PointF prevpPoint, PointF thispPoint, PointF nextpPoint)
        {
            PointF secondHandle = new PointF();
            float lengthPrevThis = GetLength(prevpPoint, thispPoint);
            float lengthThisNext = GetLength(thispPoint, nextpPoint);

            //Calculations are very similar to those in the function GetFirstHandle.

            float proportion = (float)0.5 * lengthThisNext / (lengthPrevThis + lengthThisNext);

            secondHandle.X = thispPoint.X + proportion * (nextpPoint.X - prevpPoint.X);
            secondHandle.Y = thispPoint.Y + proportion * (nextpPoint.Y - prevpPoint.Y);

            return secondHandle;
        }


        // Add two last control points of a <Composite> curve that is indicated as finished and has at least three knot points
        private PointF GetVeryLastHandle(PointF prevpPoint, PointF prevHandle, PointF lastpPoint)
        {
            PointF veryLastHandle = new PointF();

            // Coordinates of the very last handle is calculated from first, second and fourth control points of the <Composite> curve's last segment

            // We can look at these calculations as vector operations. 
            // First we calculate dot product of vectors lastpPoint-prevHandle and lastpPoint-prevHandle:
            float dotProduct = (prevHandle.X - lastpPoint.X) * (prevpPoint.X - lastpPoint.X) + 
                                (prevHandle.Y - lastpPoint.Y) * (prevpPoint.Y - lastpPoint.Y);

            // Calculations are very similar to those in the function GetVeryFirstHandle.
            // To find how long the vector from prevHandle to veryLastHandle needs to be, we find the proportion:
            float proportion = 1 - 2 * dotProduct / (float)Math.Pow(GetLength(prevpPoint, lastpPoint), 2);

            // Lastly, to point prevHandle we add vector parallel to vector prevpPoint-lastpPoint scaled by the  proportion:
            veryLastHandle.X = proportion * (prevpPoint.X - lastpPoint.X) + prevHandle.X;
            veryLastHandle.Y = proportion * (prevpPoint.Y - lastpPoint.Y) + prevHandle.Y;

            // We have achieved a "symmetrical" point to prevHandle; both of these points are on the same side of the bezier curve.

            return veryLastHandle;
        }


        // Find if there is a control or knot point near mouse location
        private void FindLocalPoint(List<List<PointF>> PointsAll, PointF MouseLocation)
        {
            const int localRadius = 7; // radius of neiborghood, used when selecting a point with mouse; chosen arbitrary

            for (int i = 0; i < PointsAll.Count; i++)
            {
                if (PointsAll[i] != null)
                {
                    for (int j = 0; j < PointsAll[i].Count; j++)
                    {
                        if (GetLength(MouseLocation, PointsAll[i][j]) < localRadius)
                        {
                            localPoint = new Tuple<int, int>(i, j);
                        }
                    }
                }
            }

            return;
        }


        // Calculate control points for interpolated curves - <4 pPoints> and <Least Squares>.
        private void AddcPointsInterpolation(int i)
        {
            List<PointF> pList = pPointsAll[i];

            // This method of curve fitting uses least squares method, so that distance errors from given knot points to the Bezier curve 
            // at respective t values is the smallest possible. 
            // To get control point coordinates, we will use formula C = M^1 * ( T^T * T )^1 * T^T * P
            // For more calculation information see documentation.

            // We will represent Bezier curve in its matrix form.

            // Matrix M contains coefficients of an expanded Bezier curve function. We will use only cubic Bezier curves therefor M always is:
            var matrix = Matrix<double>.Build;
            double[,] arrayM4 = new double[4, 4]
                { 
                    { 1, 0, 0, 0 }, 
                    { -3, 3, 0, 0 }, 
                    { 3, -6, 3, 0 }, 
                    { -1, 3, -3, 1 }
                };

            // Matrix P contains coordinates of all knot points:
            double[,] arrayP = new double[pList.Count, 2];
            for (int j = 0; j < pList.Count; j++)
            {
                arrayP[j, 0] = pList[j].X;
                arrayP[j, 1] = pList[j].Y;
            }

            var matrixP = matrix.DenseOfArray(arrayP);
            var matrixM4 = matrix.DenseOfArray(arrayM4);
            var matrixM4Inv = matrixM4.Inverse();

            // Bezier curves are parametric, so we need appropriate t values to tie each knot point to coordinates of points on the curve.
            // This parametrization can be done in different ways; we will store the resulting t values in a list sValues.
            List<double> sValues = new List<double>();

            if (parametrization[i] == ParamType.Uniform)
            {
                sValues = GetsValuesUniform(pList);
            }

            else if (parametrization[i] == ParamType.Chord)
            {
                sValues = GetsValuesChord(pList);
            }

            else if (parametrization[i] == ParamType.Centripetal)
            {
                sValues = GetsValuesCentripetal(pList);
            }

            var matrixS = matrix.DenseOfArray(GetArrayS(sValues));
            var matrixSTranspose = matrixS.Transpose();
            var matrixSSTr = matrixSTranspose * matrixS;
            var matrixSSTrInv = matrixSSTr.Inverse();

            var matrixMul1 = matrixM4Inv * matrixSSTrInv;
            var matrixMul2 = matrixMul1 * matrixSTranspose;

            var matrixC = matrixMul2 * matrixP;

            // if this is the first time calculating control points
            if (cPointsAll[i] == null)
            {
                cPoints = new List<PointF>();

                for (int j = 0; j < 4; j++)
                {
                    PointF tmp = new PointF((float)matrixC[j, 0], (float)matrixC[j, 1]);
                    cPoints.Add(tmp);
                }
                cPointsAll[i] = cPoints;
            }

            // if we are modifying a curve
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    PointF tmp = new PointF((float)matrixC[j, 0], (float)matrixC[j, 1]);
                    cPointsAll[i][j] = tmp;
                }
            }

            return;
        }


        // Bezier curve parametrization method where t values are equally spaced.
        private List<double> GetsValuesUniform(List<PointF> pList)
        {
            List<double> sValues = new List<double>();

            for (int i = 0; i < pList.Count; i++)
            {
                double s = (double)i / (pList.Count - 1);
                sValues.Add(s);
            }
            return (sValues);
        }


        // Bezier curve parametrization method where t values are aligned with distance along the polygon of control points.
        private List<double> GetsValuesChord(List<PointF> pList)
        {
            // At the first point, we're fixing t = 0, at the last point t = 1. Anywhere in between t value is equal to the distance
            // along the polygon scaled to the [0,1] domain.

            List<double> sValues = new List<double>();

            // First we calculate distance along the polygon for each point:
            List<double> dPoints = new List<double> { 0 };
            for (int i = 1; i < pList.Count; i++)
            {
                double d = dPoints[i - 1] + GetLength(pList[i - 1], pList[i]);
                dPoints.Add(d);
            }

            // Then we scale these values to [0, 1] domain:
            for (int i = 0; i < pList.Count; i++)
            {
                double s = dPoints[i] / dPoints[pList.Count - 1];
                sValues.Add(s);
            }

            return (sValues);
        }


        // Bezier curve parametrization method where t values are aligned with square root of the distance along the polygon.
        private List<double> GetsValuesCentripetal(List<PointF> pList)
        {
            // At the first point, we're fixing t = 0, at the last point t = 1. Anywhere in between t value is equal to the
            // square root of the distance along the polygon scaled to the [0,1] domain.

            List<double> sValues = new List<double>();

            // First we calculate the square root of distance along the polygon for each point:
            List<double> dPoints = new List<double> { 0 };
            for (int i = 1; i < pList.Count; i++)
            {
                double d = dPoints[i - 1] + Math.Sqrt(GetLength(pList[i - 1], pList[i]));
                dPoints.Add(d);
            }

            // Then we scale these values to [0, 1] domain:
            for (int i = 0; i < pList.Count; i++)
            {
                double s = dPoints[i] / dPoints[pList.Count - 1];
                sValues.Add(s);
            }

            return (sValues);
        }


        // Make matrix S and fill it using sValues from paramtetrization
        private double[,] GetArrayS(List<double> sValues)
        {
            //  see documentation to see why its done this way
            double[,] arrayS = new double[sValues.Count, 4];

            for (int i = 0; i < sValues.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arrayS[i, j] = Math.Pow(sValues[i], j);
                }
            }
            return arrayS;
        }
        

        // Get length between two points
        private float GetLength(PointF firstPoint, PointF secondPoint)
        {
            return (float)Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2));
        }


        // Modify coordinates of a chosen control point.
        private void ModifycPoint(MouseEventArgs e)
        {
            int i = localPoint.Item1;
            int j = localPoint.Item2;

            modifyCurveType = allCurves[i];

            if (modifyCurveType == BezierType.pPoints || modifyCurveType == BezierType.LeastSquares)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Error: Not allowed to move control points of <" + modifyCurveType + "> curve!";
                modifyCurveType = BezierType.Nothing;
                localPoint = null;
            }

            else if (modifyCurveType == BezierType.Composite)
            {
                if (rbKeyboardModify.Checked == true)
                {
                    lblError.ForeColor = Color.Red;
                    lblError.Text = "Error: Not allowed to move control points of <Composite> curve by keyboard!";
                    localPoint = null;
                    modifyCurveType = BezierType.Nothing;
                }

                // every third control point on a <Composite> curve is also a knot point therefore moved as a knot point
                else if (j % 3 == 0)
                {
                    localPoint = null;
                    modifyCurveType = BezierType.Nothing;
                }

                else if (e.Button == MouseButtons.Left)
                {
                    movedCurve[i] = MoveType.LeftClick;
                }

                else if (e.Button == MouseButtons.Right)
                {
                    movedCurve[i] = MoveType.RightClick;
                }

                return;
            }

            else if (rbKeyboardModify.Checked == true)
            {
                // when modifying points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Modify, modifyCurveType);
                form_KeyboardAdd.ShowDialog();

                movedCurve[i] = MoveType.pPoints;
                modifyCurveType = BezierType.Nothing;
                localPoint = null;
            }

            pbCanva.Invalidate();
            return;
        }


        // Modify coordinates of a chosen knot point.
        private void ModifypPoint()
        {
            int i = localPoint.Item1;
            modifyCurveType = allCurves[i];

            if (rbKeyboardModify.Checked == true)
            {
                // when modifying points by keyboard, intialize the form of coordinates
                FormCoordinates form_KeyboardAdd = new FormCoordinates(FormType.Modify, modifyCurveType);
                form_KeyboardAdd.ShowDialog();

                if (modifyCurveType == BezierType.pPoints || modifyCurveType == BezierType.LeastSquares)
                {
                    AddcPointsInterpolation(i);
                }

                modifyCurveType = BezierType.Nothing;
                localPoint = null;
            }

            pbCanva.Invalidate();
            return;
        }


        // To ensure C2 continuity, when dragging a control point of a <Composite> curve with the left mouse button, 
        // the opposite handle needs to move as well.
        private void ModifyHandleComposite(PointF modifyHandle, PointF middlepPoint, PointF oppositeHandle, int opposite)
        {

            // It doesn't make mathematical sense and makes an error for two control points in <Composite> curve segment to have the same location.
            if (middlepPoint == modifyHandle)
            {
                modifyHandle.X++;
                modifyHandle.Y++;
            }

            //We can look at these calculations as vector operations. We want for vector middle-change to keep its length, 
            //but change its direction so it starts from middle point and is parallel to moving-middle vector.
            //To do that, we take unit vector from moving-middle (devide moving-middle with its length) and multiply that by 
            //middle-change length. Finally, we add that to middle point.

            float proportion = GetLength(middlepPoint, oppositeHandle) / GetLength(modifyHandle, middlepPoint);

            oppositeHandle.X = middlepPoint.X + proportion * (middlepPoint.X - modifyHandle.X);
            oppositeHandle.Y = middlepPoint.Y + proportion * (middlepPoint.Y - modifyHandle.Y);

            cPointsAll[localPoint.Item1][opposite] = oppositeHandle;

            return;
        }


        // To ensure C2 continuity and make sure no other points move when dragging a control point of a <Composite> curve with the right mouse button, 
        //the control point can only be moved in a straight line away from the middle point. 
        private void ModifyHandleCompositeStraight(PointF modifyHandle, PointF middlepPoint, PointF oppositeHandle)
        {
            const int maxDistanceToMouse = 100; // maximum distance between mouse location and control point being dragged; chosen arbitrary

            int i = localPoint.Item1;
            int j = localPoint.Item2;

            if (GetLength(modifyHandle, cPointsAll[i][j]) > maxDistanceToMouse)
            {
                return;
            }

            PointF result = new PointF();

            // To move the control point in straight line, we take unit vector from the middlepPoint to 
            // the place control point was before moving (modifyHandle). It's known that modifyHandle was on the needed curve. 
            // Than we scale this unit vector by the distance mouse is from the middlepPoint and at last add this vector to the middlepPoint.

            float prop = GetLength(middlepPoint, modifyHandle) / GetLength(oppositeHandle, middlepPoint);

            result.X = middlepPoint.X + prop * (middlepPoint.X - oppositeHandle.X);
            result.Y = middlepPoint.Y + prop * (middlepPoint.Y - oppositeHandle.Y);

            cPointsAll[i][j] = result;

            return;
        }


        // Modify coordinates of a chosen knot point of <Composite> curve.
        public static void ModifypPointComposite(PointF mouseLocation)
        {
            int i = localPoint.Item1;
            int j = localPoint.Item2;

            PointF pointOld = new PointF();
            pointOld = pPointsAll[i][j];

            // every knot point of <Composite> curve is also a control point; change both these point coordinates:
            pPointsAll[i][j] = mouseLocation;
            cPointsAll[i][j * 3] = mouseLocation;

            // We can look at these calculations as vector operations. 
            // We want for the adjacent handles of the knot point to stay in the same position relative to the knot point. 
            // To do that, we take vectors from knot point to control points and add those vectors to the new knot point coordinates.

            PointF newcPoint = new PointF();

            // first knot point doesn't have the first handle
            if (j != 0)
            {
                newcPoint.X = mouseLocation.X - pointOld.X + cPointsAll[i][j * 3 - 1].X;
                newcPoint.Y = mouseLocation.Y - pointOld.Y + cPointsAll[i][j * 3 - 1].Y;
                cPointsAll[i][j * 3 - 1] = newcPoint;
            }

            //last knot point doesn't have the second handle
            if (j != pPointsAll[i].Count - 1)
            {
                newcPoint.X = mouseLocation.X - pointOld.X + cPointsAll[i][j * 3 + 1].X;
                newcPoint.Y = mouseLocation.Y - pointOld.Y + cPointsAll[i][j * 3 + 1].Y;
                cPointsAll[i][j * 3 + 1] = newcPoint;
            }

            return;
        }


        // Change parametrization method and show the method being used now.
        private void ChangeParametrization()
        {
            lblError.Text = "";

            int i = localPoint.Item1;

            ParamType paramType = parametrization[i];

            if (allCurves[i] == BezierType.cPoints || allCurves[i] == BezierType.Composite)
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Error: <" + allCurves[i] + "> curves does not use parametrization!";
                return;
            }

            // Show the real parametrization type of the selected curve:

            if (paramType == ParamType.Uniform)
            {
                rbUniform.Checked = true;
            }

            else if (paramType == ParamType.Chord)
            {
                rbChord.Checked = true;
            }

            else if (paramType == ParamType.Centripetal)
            {
                rbCentripetal.Checked = true;
            }

            isChangingParam = true;
        }
        

        // ???
        private void OutputPointsToFile(List<PointF> pointList)
        {
            int i = localPoint.Item1;
            string folderPath = "";

            if (outputPointType == BezierType.cPoints)
            {
                pointList = cPointsAll[i];
            }
            else
            {
                pointList = pPointsAll[i];
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = dialog.SelectedPath;
            }

            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Error: Output error!";
            }

            string path = Path.Combine(folderPath, "points.txt");
            using (var file = new StreamWriter(path, true))
            {
                file.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")); // write date and time in the first line
                file.WriteLine("<" + allCurves[i] + "> curve: \n");

                for (int j = 0; j < pointList.Count; j++)
                {
                    float scaledX = (pointList[j].X + shiftVector.X) * scalePropX;
                    float scaledY = (pointList[j].Y + shiftVector.Y) * scalePropY;

                    string line = "C" + (j + 1) + ": (" + scaledX + "; " + scaledY + ")"; // in each line write coordinates of one control point
                    file.WriteLine(line);
                }

                file.WriteLine("\n \n");
            }
        }


        // Helps with smooth graphics and buffering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        /*
        // ???
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                    zoomAmount += 0.05;
            }

            else if (e.Delta < 0)
            {
                zoomAmount -= 0.05;
            }

            if (zoomAmount > 5 )
            {
                zoomAmount = 5;
            }

            else if (zoomAmount < 0.05)
            {
                zoomAmount = 0.05;
            }

            nudZoom.Value = Convert.ToInt32(zoomAmount * 100);
        }
        */

    }
}