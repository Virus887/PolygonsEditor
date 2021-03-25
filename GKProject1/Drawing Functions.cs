using GKProject1.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GKProject1
{
    public partial class MainForm : Form
    {
        private void DrawLineBresenham(PointF p1, PointF p2)
        {
            Bitmap myBitmap = (Bitmap)DrawingArea.Image;
            int x1 = (int)p1.X;
            int x2 = (int)p2.X;
            int y1 = (int)p1.Y;
            int y2 = (int)p2.Y;

            // zmienne pomocnicze
            int d, dx, dy, ai, bi, xi, yi;
            int x = x1, y = y1;
            // ustalenie kierunku rysowania
            if (x1 < x2)
            {
                xi = 1;
                dx = x2 - x1;
            }
            else
            {
                xi = -1;
                dx = x1 - x2;
            }
            // ustalenie kierunku rysowania
            if (y1 < y2)
            {
                yi = 1;
                dy = y2 - y1;
            }
            else
            {
                yi = -1;
                dy = y1 - y2;
            }
            // pierwszy piksel
            myBitmap.SetPixel(x, y, Color.Black);

            // oś wiodąca OX
            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;
                // pętla po kolejnych x
                while (x != x2)
                {
                    // test współczynnika
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        x += xi;
                    }
                    myBitmap.SetPixel(x, y, Color.Black);
                }
            }
            // oś wiodąca OY
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;
                // pętla po kolejnych y
                while (y != y2)
                {
                    // test współczynnika
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        y += yi;
                    }
                    myBitmap.SetPixel(x, y, Color.Black);
                }
            }
    }

        private void ClearBitmap()
        {
            Bitmap cleanBitmap = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            Graphics g = Graphics.FromImage(cleanBitmap);
            g.Clear(Color.White);
            DrawingArea.Image = cleanBitmap;
        }

        private void RedrawBitmap()
        {
            ClearBitmap();

            foreach (Polygon p in Polygons)
            {
                if (CurrentPaintedObject.polygon == p) DrawCurrentPaintedObject();
                else if (CurrentMovingObject.polygon == p) DrawCurrentMovingObject();
                else DrawPolygon(p);
            }


            for (int i = 0; i < Verticles.Count; i++)
            {
                DrawPointF(Verticles[i]);
                if (i >= 1)
                {
                    DrawLine(Verticles[i - 1], Verticles[i], RelationType.None);
                }
            }

            if (DrawingLine)
            {
                Graphics g = Graphics.FromImage(DrawingArea.Image);

                if(posRel != RelationType.None)
                {
                    PointF newPoint = new PointF(0,0);
                    if (posRel == RelationType.Vertical) newPoint = new PointF(Verticles[Verticles.Count - 1].X, currentPointF.Y);
                    if (posRel == RelationType.Horizontal) newPoint = new PointF(currentPointF.X, Verticles[Verticles.Count - 1].Y);
                    DrawLine(Verticles[Verticles.Count - 1], newPoint, RelationType.None);
                    DrawPointF(newPoint);
                    DrawRelation(currentPointF, currentPointF, posRel, g);
                }
                else
                {
                    DrawLine(Verticles[Verticles.Count - 1], currentPointF, RelationType.None);
                    DrawPointF(currentPointF);
                }
                   
            }

            if (RightClickedObject.polygon != null) DrawRightClickedObject();
           
        }

        private void DrawRightClickedObject()
        {
            Polygon p = RightClickedObject.polygon;
            if (RightClickedObject.PointFIdx2 != -1)
            {
                DrawHoveredLine(p.verticles[RightClickedObject.PointFIdx1], p.verticles[RightClickedObject.PointFIdx2], p.GetEdgeRelation(RightClickedObject.PointFIdx1, RightClickedObject.PointFIdx2));
            }
            else if (RightClickedObject.PointFIdx1 != -1)
            {
                DrawHoveredPointF(RightClickedObject.polygon.verticles[RightClickedObject.PointFIdx1]);
            }
            else if (RightClickedObject.polygon != null)
            {
                DrawHoveredPolygon(RightClickedObject.polygon);
            }
        }
        private void DrawOldObject()
        {
            if (OldObject != null)
            {
                DrawOldPolygon(OldObject);
            }
        }



        private void DrawCurrentPaintedObject()
        {
            Polygon p = CurrentPaintedObject.polygon;
            if (CurrentPaintedObject.PointFIdx2 != -1)
            {
                DrawPolygon(p);
                DrawHoveredLine(p.verticles[CurrentPaintedObject.PointFIdx1], p.verticles[CurrentPaintedObject.PointFIdx2], p.GetEdgeRelation(CurrentPaintedObject.PointFIdx1, CurrentPaintedObject.PointFIdx2));
            }
            else if (CurrentPaintedObject.PointFIdx1 != -1)
            {
                DrawPolygon(p);
                DrawHoveredPointF(p.verticles[CurrentPaintedObject.PointFIdx1]);
            }
            else
            {
                DrawHoveredPolygon(p);
            }
        }

        private void DrawCurrentMovingObject()
        {
            Polygon p = CurrentMovingObject.polygon;

            if (CurrentMovingObject.PointFIdx2 != -1)
            {
                DrawPolygon(p);
                DrawHoveredLine(p.verticles[CurrentMovingObject.PointFIdx1], p.verticles[CurrentMovingObject.PointFIdx2], p.GetEdgeRelation(RightClickedObject.PointFIdx1, CurrentMovingObject.PointFIdx2));
            }
            else if (CurrentMovingObject.PointFIdx1 != -1)
            {
                DrawPolygon(p);
                DrawHoveredPointF(p.verticles[CurrentMovingObject.PointFIdx1]);
            }
            else if (p != null)
            {
                DrawHoveredPolygon(p);
            }
            if (OldObject != null) DrawOldObject();
        }

        private void DrawPolygon(Polygon p)
        {
            //Fill polygon
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            SolidBrush blueBrush = new SolidBrush(Color.AliceBlue);
            g.FillPolygon(blueBrush , p.verticles.ToArray());


            //Draw polygon
            for (int i = 0; i < p.verticles.Count; i++)
            {
                DrawPointF(p.verticles[i]);
                if (i >= 1)
                {
                    DrawLine(p.verticles[i - 1], p.verticles[i], p.GetEdgeRelation(i-1,i));
                }             
            }
            DrawLine(p.verticles[0], p.verticles[p.verticles.Count - 1], p.GetEdgeRelation(0, p.verticles.Count - 1));
        }

        private void DrawHoveredPolygon(Polygon p)
        {
            //Fill polygon
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            SolidBrush blueBrush = new SolidBrush(Color.LightYellow);
            g.FillPolygon(blueBrush, p.verticles.ToArray());


            //Draw polygon
            for (int i = 0; i < p.verticles.Count; i++)
            {
                DrawPointF(p.verticles[i]);
                if (i >= 1)
                {
                    DrawLine(p.verticles[i - 1], p.verticles[i], p.GetEdgeRelation(i - 1, i));
                }
            }
            DrawLine(p.verticles[0], p.verticles[p.verticles.Count - 1], p.GetEdgeRelation(0, p.verticles.Count - 1));
        }

        private void DrawOldPolygon(Polygon p)
        {
            //Draw polygon
            for (int i = 0; i < p.verticles.Count; i++)
            {
                DrawOldPointF(p.verticles[i]);
                if (i >= 1)
                {
                    DrawOldLine(p.verticles[i - 1], p.verticles[i], p.GetEdgeRelation(i - 1, i));
                }
            }
            DrawOldLine(p.verticles[0], p.verticles[p.verticles.Count - 1], p.GetEdgeRelation(0, p.verticles.Count - 1));
        }

        public void DrawPointF(PointF p)
        {
            Pen pen = new Pen(Color.Black, 5);
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            g.DrawEllipse(pen, new RectangleF(p, new Size(1, 1)));
        }
        public void DrawOldPointF(PointF p)
        {
            Pen pen = new Pen(Color.Green, 5);
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            g.DrawEllipse(pen, new RectangleF(p, new Size(1, 1)));
        }

        public void DrawHoveredPointF(PointF p)
        {
            PointF newPointF = new PointF(p.X - 8, p.Y - 8);
            Pen pen = new Pen(Color.Orange, 5);
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            g.DrawEllipse(pen, new RectangleF(newPointF, new Size(16, 16)));
            if (possibleRelation.type != RelationType.None)
            {
                DrawRelation(p, p, possibleRelation.type, g);
            }
        }

        public void DrawLine(PointF p1, PointF p2, RelationType type)
        {
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            if (BresenhamCheckBox.Checked)
            {
                DrawLineBresenham(p1, p2);
            }
            else
            {
                Pen pen = new Pen(Color.Black, 1);
                g.DrawLine(pen, p1, p2);
            }
            DrawRelation(p1, p2, type, g);
        }

        public void DrawOldLine(PointF p1, PointF p2, RelationType type)
        {
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            Pen pen = new Pen(Color.Green, 1);
            g.DrawLine(pen, p1, p2);
        }

        public void DrawHoveredLine(PointF p1, PointF p2, RelationType type)
        {
            Pen pen = new Pen(Color.Orange, 5);
            Graphics g = Graphics.FromImage(DrawingArea.Image);
            g.DrawLine(pen, p1, p2);
            DrawRelation(p1, p2, type, g);
        }

        public void DrawRelation(PointF p1, PointF p2, RelationType type, Graphics g)
        {
            PointF location = new PointF((p1.X + p2.X) / 2 + DISTANCE, (p1.Y + p2.Y) / 2 + DISTANCE);
            Size size = new Size(DISTANCE+4, DISTANCE+4);
            //Draw relation
            switch (type)
            {
                case RelationType.ConstantLength:
                    {
                        g.DrawImage(Resources.Length, new RectangleF(location, size));
                        break;
                    }
                case RelationType.Horizontal:
                    {
                        g.DrawImage(Resources.Horizontal, new RectangleF(location, size));
                        break;
                    }
                case RelationType.Vertical:
                    {
                        g.DrawImage(Resources.Vertical, new RectangleF(location, size));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }
}
