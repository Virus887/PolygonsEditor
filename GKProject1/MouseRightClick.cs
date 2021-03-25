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
        private (Polygon polygon, int PointFIdx1, int PointFIdx2) RightClickedObject = (null, -1, -1);
        private void DrawingArea_RightMouseClick(MouseEventArgs e)
        {
            Verticles = new List<PointF>();
            DrawingLine = false;
            DrawingPolygon = false;

            bool found = false;
            if (!DrawingPolygon && !DrawingLine)
            {
                foreach (Polygon polygon in Polygons.Reverse<Polygon>())
                {
                    //Search PointF
                    for (int i = 0; i < polygon.verticles.Count; i++)
                    {
                        if (IsMouseOnPointF(polygon.verticles[i]))
                        {
                            RightClickedObject = (polygon, i, -1);
                            found = true;
                            PointFMenuStrip.Show(DrawingArea, e.Location);
                        }
                        if (found) return;
                    }

                    //Searching edge
                    for (int i = 0; i < polygon.verticles.Count; i++)
                    {
                        if (IsMouseOnEdge(polygon.verticles[i], polygon.verticles[(i + 1) % polygon.verticles.Count]))
                        {
                            RightClickedObject = (polygon, i, (i + 1) % polygon.verticles.Count);
                            found = true;
                            EdgeMenuStrip.Show(DrawingArea, e.Location);
                        }
                        if (found) return;
                    }


                    //Searching polygon
                    if (polygon.graphicsPath.IsVisible(currentPointF))
                    {
                        RightClickedObject = (polygon, -1, -1);
                        found = true;
                        PolygonMenuStrip.Show(DrawingArea, e.Location);
                    }
                    if (found) break;
                }
            }
            RedrawBitmap();
        }

        private void EdgeAddVerticleMenuItem_Click(object sender, EventArgs e)
        {
            RightClickedObject.polygon.TryPutVerticleOnEdge(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2, currentPointF);
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }
        
        private void PointFDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (!RightClickedObject.polygon.TryDeleteVerticle(RightClickedObject.PointFIdx1))
            {
                MessageBox.Show("Polygon must have 3 verticles.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

        private void EdgeDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (!RightClickedObject.polygon.TryDeleteEdge(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2))
            {
                MessageBox.Show("Polygon must have 3 verticles.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

        private void PolygonDeleteMenuItem_Click(object sender, EventArgs e)
        {
            Polygons.Remove(RightClickedObject.polygon);
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

        //RELATIONS
        private void EdgeRelationsMenuItem_MouseEnter(object sender, EventArgs e)
        {
            RelationType type = RightClickedObject.polygon.GetEdgeRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2);
            switch (type)
            {
                case RelationType.ConstantLength:
                    {
                        ConstantLengthMenuItem.Checked = true;
                        HorizontalLineMenuItem.Checked = false;
                        VerticalLineMenuItem.Checked = false;
                        break;
                    }
                case RelationType.Horizontal:
                    {
                        ConstantLengthMenuItem.Checked = false;
                        HorizontalLineMenuItem.Checked = true;
                        VerticalLineMenuItem.Checked = false;
                        break;
                    }
                case RelationType.Vertical:
                    {
                        ConstantLengthMenuItem.Checked = false;
                        HorizontalLineMenuItem.Checked = false;
                        VerticalLineMenuItem.Checked = true;
                        break;
                    }
                default:
                    {
                        ConstantLengthMenuItem.Checked = false;
                        HorizontalLineMenuItem.Checked = false;
                        VerticalLineMenuItem.Checked = false;
                        break;
                    }
            }
        }

        private void ConstantLengthMenuItem_Click(object sender, EventArgs e)
        {
            RelationType type = RightClickedObject.polygon.GetEdgeRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2);

            if (!RightClickedObject.polygon.TrySetRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2, RelationType.ConstantLength))
            {
                MessageBox.Show("Cannot set constant length relation.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

        private void HorizontalLineMenuItem_Click(object sender, EventArgs e)
        {
            RelationType type = RightClickedObject.polygon.GetEdgeRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2);

            if (!RightClickedObject.polygon.TrySetRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2, RelationType.Horizontal))
            {
                MessageBox.Show("Cannot set hotizontal relation.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

        private void VerticalLineMenuItem_Click(object sender, EventArgs e)
        {
            RelationType type = RightClickedObject.polygon.GetEdgeRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2);

            if (!RightClickedObject.polygon.TrySetRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2, RelationType.Vertical))
            {
                MessageBox.Show("Cannot set vertical relation.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

        private void EdgeDeleteRelationMenuItem_Click(object sender, EventArgs e)
        {
            if (!RightClickedObject.polygon.TrySetRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2, RelationType.None))
            {
                MessageBox.Show("Cannot delete relation.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            RightClickedObject = (null, -1, -1);
            CurrentPaintedObject = (null, -1, -1);
            RedrawBitmap();
        }

    }
}


