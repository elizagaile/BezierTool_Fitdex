# BezierTool_darbs


##### Construct and draw:

&lt;dPoints&gt; - import default points from a .txt file.
&lt;Line segments&gt; - endpoints of line segment are viewed as control points. 
&lt;4 cPoints&gt; - classic Bezier curve constructed from four control points.
&lt;4 pPoints&gt; - exact Bezier curve interpolation through four knot points.
&lt;Least squares&gt; - Bezier curve interpolation through four or more knot points. Usually not exact, but finds the closest fit using least squares.
&lt;Composite&gt; - B-spline through at least two knot points. Constructs and draws separate Bezier curve between every two succeeding points while ensuring C2 continuity throughout the spline. Specific method of construction was designed by the author.

Control and knot points can be defined by mouse, using keyboard or from a .txt file.
&lt;4 pPoints&gt; and &lt;Least Squares&gt; curves use a parametrization method. Three methods are implemented - <Uniform>, <Chord length> and <Centripental> parametrization.


##### Modify:

&lt;4 cPoints&gt; can be modified by changing control points.
&lt;4 pPoints&gt; and &lt;Least Squares&gt; can be modified by changing knot points and parametrization methods.
&lt;Composite&gt; can be modified by changing control points in two ways (depends on mouse button being used) and by changing knot points.

Control and knot points can be defined by mouse or by using keyboard.
Each individual line can be deleted.


##### Output:

It is possible to output control or knot point coordinates (if the respective line have such) of each individual line as well as it is possible to outpull all objects and their parametrs and a part of default settings in one .txt file.

Output of individual lines can be showed on screen or in a .txt file.


##### Input:

It is possible to import a .txt file from which many lines can be reconstructed at once and a part of default setting can be adjusted. View file import_example.txt.


##### Visual:

Canva shows origin point, axis, scale and darker regions where at least on of x, y coordinates is negative. However it is possible to draw on these negative borders.
It is possible to set a different size of the canva and to set a different scale - pheraps to match the scale of backround image (which can be uploaded from the computer).
It is possible to change the visibility of backround image and of control and knot points and polygon lines.
Color and size of curves, polygons, control, knot points and default points can be changed. It is possible to change color of each individual curve.

Canva can be zoomed and panned.


:white_flower: :dragon: :tanabata_tree: :tanabata_tree:

