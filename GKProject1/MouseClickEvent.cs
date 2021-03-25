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
        private void DrawingArea_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        DrawingArea_LeftMouseClick(e);
                        break;
                    }

                case MouseButtons.Right:
                    {
                        DrawingArea_RightMouseClick(e);
                        break;
                    }
            }
            RedrawBitmap();
        }
        private void DrawingArea_LeftMouseClick(MouseEventArgs e)
        {
            PointF newPoint = currentPointF;
            if (DrawingPolygon)
            {
                if (posRel != RelationType.None)
                {
                    if (posRel == RelationType.Vertical) newPoint = new PointF(Verticles[Verticles.Count - 1].X, currentPointF.Y);
                    if (posRel == RelationType.Horizontal) newPoint = new PointF(currentPointF.X, Verticles[Verticles.Count - 1].Y);
                    if (TryPutVerticle(newPoint))
                    {
                        if (Verticles.Count > 0) DrawingLine = true;
                    }
                }
                else
                {
                    if (TryPutVerticle(newPoint))
                    {
                        if (Verticles.Count > 0) DrawingLine = true;
                    }
                }
            }
            RightClickedObject = (null, -1, -1);
        }

        private void DrawingArea_MouseDoubleClick(object sender, MouseEventArgs e) 
        {
            //Complete the polygon and save it to list.
            if (Verticles.Count >= 2) CompletePolygon();
            RedrawBitmap();
        }
        private void MovePolygonToTop(Polygon p)
        {
            if (p == null) return;
            Polygons.Remove(p);
            Polygons.Add(p);
        }
        private void CompletePolygon()
        {
            if (Verticles.Count > 2)
            {
                Polygons.Add(new Polygon(Verticles));
            }
            Verticles = new List<PointF>();
            DrawingLine = false;
            DrawingPolygon = false;
        }
        private bool TryPutVerticle(PointF p)
        {
            if (PointFColidesWithPolygons(p)) return false;

            if (Verticles.Count == 0)
            {
                Verticles.Add(p);
                return true;
            }

            foreach (PointF PointF in Verticles) //Chcecking another verticles
            {
                if (Math.Abs(PointF.X - p.X) < 8 && Math.Abs(PointF.Y - p.Y) < 8) //Verticle colides with Point
                {
                    if (PointF == Verticles[0])
                    {
                        CompletePolygon();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            Verticles.Add(p);
            return true;
        }
        public bool PointFColidesWithPolygons(PointF p)
        {
            foreach (Polygon polygon in Polygons)
            {
                foreach (PointF v in polygon.verticles)
                {
                    if (Math.Abs(p.X - v.X) < 8 && Math.Abs(p.Y - v.Y) < 8) //Verticle colides
                        return true;
                }
            }
            return false;
        }

    }
}
