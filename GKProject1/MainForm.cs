using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKProject1
{
    public partial class MainForm : Form
    {
        const int DISTANCE = 8; // Constant variable used in functions: isMouseOnEdge(), etc.

        private PointF currentPointF; // Current mouse location.
        private List<PointF> Verticles; //PointFs that user puts during constructing polygon.
        private List<Polygon> Polygons; // List of current existing polygons.
        private bool DrawingLine; // Do we want to draw line form last vertile to mouse location?
        private bool DrawingPolygon; // Do we want to let user put PointFs on screen?
        private bool IsMouseDown; // Is mouse button down? Used for moving elements.

        //LAB
        private (RelationType type, int idx1, int idx2) possibleRelation = (RelationType.None, -1, -1);


        public MainForm()
        {
            ResetApp();
            InitializeComponent();
            RedrawBitmap();
        }
        private void ResetApp()
        {
            Polygons = new List<Polygon>();
            currentPointF = new PointF(0, 0);
            Verticles = new List<PointF>();
            DrawingLine = false;
            DrawingPolygon = false;
            IsMouseDown = false;
            CurrentMovingObject = (null, -1, -1);
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
        }

        private void Draw_Polygon_Button_Click(object sender, EventArgs e)
        {
            DrawingPolygon = true;
        }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
        }

        private void DrawingArea_MouseUp(object sender, MouseEventArgs e)
        {
            switch(possibleRelation.type)
            {
                case RelationType.Horizontal:
                {
                    if (!CurrentMovingObject.polygon.TrySetRelation(possibleRelation.idx1, possibleRelation.idx2, RelationType.Horizontal))
                    {
                        MessageBox.Show("Cannot set hotizontal relation.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        possibleRelation = (RelationType.None, -1, -1);
                        }
                        break;
                }
                case RelationType.Vertical:
                {

                    if (!CurrentMovingObject.polygon.TrySetRelation(possibleRelation.idx1, possibleRelation.idx2, RelationType.Vertical))
                    {
                        MessageBox.Show("Cannot set hotizontal relation.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        possibleRelation = (RelationType.None, -1, -1);
                        }
                        break;
                }
                default:
                { 
                    break;
                }
            }

            OldObject = null;
            possibleRelation = (RelationType.None, -1, -1);
            IsMouseDown = false;    
            CurrentMovingObject = (null, -1, -1);
        }


        private void newBlueprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetApp();
            RedrawBitmap();
        }


        private void BresenhamCheckBox_Click(object sender, EventArgs e)
        {
            RedrawBitmap();
        }

        private void Clear_All_Click(object sender, EventArgs e)
        {
            ResetApp();
            RedrawBitmap();
        }

        private void Example_Polygon1_Click(object sender, EventArgs e)
        {
            List<PointF> list = new List<PointF>();
            list.Add(new PointF(400, 300));
            list.Add(new PointF(700, 450));
            list.Add(new PointF(800, 550));
            list.Add(new PointF(1000, 300));
            list.Add(new PointF(700, 100));
            list.Add(new PointF(700, 200));
            list.Add(new PointF(600, 200));
            list.Add(new PointF(600, 100));
            Polygons.Add(new Polygon(list));
            Polygons[Polygons.Count-1].relations[1] = RelationType.ConstantLength;
            Polygons[Polygons.Count - 1].relations[2] = RelationType.ConstantLength;
            Polygons[Polygons.Count - 1].relations[4] = RelationType.Vertical;
            Polygons[Polygons.Count - 1].relations[5] = RelationType.Horizontal;
            Polygons[Polygons.Count - 1].relations[6] = RelationType.Vertical;
            RedrawBitmap();
        }

        private void ExamplePolygonButton2_Click(object sender, EventArgs e)
        {
            List<PointF> list = new List<PointF>();
            list.Add(new PointF(400, 400));
            list.Add(new PointF(550, 400));
            list.Add(new PointF(550, 500));
            list.Add(new PointF(800, 500));
            list.Add(new PointF(800, 250));
            list.Add(new PointF(700, 150));
            Polygons.Add(new Polygon(list));
            Polygons[Polygons.Count - 1].relations[0] = RelationType.Horizontal;
            Polygons[Polygons.Count - 1].relations[1] = RelationType.Vertical;
            Polygons[Polygons.Count - 1].relations[4] = RelationType.ConstantLength;
            RedrawBitmap();
        }

        private void ExamplePolygonButton3_Click(object sender, EventArgs e)
        {         
            List<PointF> list = new List<PointF>();
            list.Add(new PointF(300, 100));
            list.Add(new PointF(300, 500));
            list.Add(new PointF(1000, 500));
            list.Add(new PointF(1000, 100));
            Polygons.Add(new Polygon(list));
            Polygons[Polygons.Count - 1].relations[0] = RelationType.Vertical;
            Polygons[Polygons.Count - 1].relations[1] = RelationType.Horizontal;
            Polygons[Polygons.Count - 1].relations[2] = RelationType.Vertical;
            Polygons[Polygons.Count - 1].relations[3] = RelationType.Horizontal;
            RedrawBitmap();
        }

    }

    
}
