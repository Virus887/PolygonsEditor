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
    public partial class MainForm: Form
    {

        private (Polygon polygon, int PointFIdx1, int PointFIdx2) CurrentMovingObject;//current animated verticle or edge//FOR MOVING
        private Polygon OldObject = null;
        private (Polygon polygon, int PointFIdx1, int PointFIdx2) CurrentPaintedObject = (null, -1, -1);//FOR ORANGE LINES

        private RelationType posRel = RelationType.None;


        public RelationType GetHintRelation()
        {
            if (IsNearVertical()) return RelationType.Vertical;
            if (IsNearHorizontal()) return RelationType.Horizontal;    
            return RelationType.None;
        }

        private bool IsNearVertical()
        {
            if (Math.Abs(currentPointF.X - Verticles[Verticles.Count-1].X) < DISTANCE) return true;
            return false;
        }
        private bool IsNearHorizontal()
        {
            if (Math.Abs(currentPointF.Y - Verticles[Verticles.Count - 1].Y) < DISTANCE) return true;
            return false;
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            HandleAnimatingObject(e); //move selected object
            currentPointF = e.Location;
            if (DrawingLine)
            {
                if (Hint.Checked) //zmieniamy możliwą relację
                {
                    posRel = GetHintRelation();
                }
                RedrawBitmap();
                return;
            }
            //Searching 
            bool found = false;
            if (!DrawingPolygon && !DrawingLine && CurrentMovingObject.polygon==null)
            {
                foreach (Polygon polygon in Polygons.Reverse<Polygon>())
                {
                    //Search Point
                    for (int i = 0; i < polygon.verticles.Count; i++)
                    {
                        if (IsMouseOnPointF(polygon.verticles[i]))
                        {
                            CurrentPaintedObject = (polygon, i, -1);
                            if (IsMouseDown && CurrentMovingObject.polygon == null)
                            {
                                CurrentMovingObject = (polygon, i, -1);
                                if (OldObject == null)
                                {
                                    List<PointF> v = new List<PointF>();
                                    foreach (PointF p in polygon.verticles) v.Add(new PointF(p.X, p.Y));
                                    OldObject = new Polygon(v);
                                }
                            }
                            found = true;
                        }
                        if (found) break;
                    }
                    if (found) break;
                    //Searching Edge
                    for (int i = 0; i < polygon.verticles.Count; i++)
                    {
                        if (IsMouseOnEdge(polygon.verticles[i], polygon.verticles[(i + 1) % polygon.verticles.Count]))
                        {
                            CurrentPaintedObject = (polygon, i, (i+1)%polygon.verticles.Count);
                            if (IsMouseDown && CurrentMovingObject.polygon == null)
                            {
                                CurrentMovingObject = (polygon, i, (i + 1) % polygon.verticles.Count);
                                if (OldObject == null)
                                {
                                    List<PointF> v = new List<PointF>();
                                    foreach (PointF p in polygon.verticles) v.Add(new PointF(p.X, p.Y));
                                    OldObject = new Polygon(v);
                                }
                            }
                            found = true;
                            if (found) break;
                        }
                    }
                    if (found) break;
                    //Searching Polygon
                    if (polygon.graphicsPath.IsVisible(currentPointF))
                    {
                        CurrentPaintedObject = (polygon, -1, -1);
                        if (IsMouseDown && CurrentMovingObject.polygon == null)
                        {
                            CurrentMovingObject = (polygon, -1, -1);
                            MovePolygonToTop(CurrentMovingObject.polygon);
                            found = true;
                        }
                        found = true;
                        return;
                    }
                    if (found) break;
                }
            }
            if (!found)
            {
                CurrentPaintedObject = (null, -1, -1);
            }

            RedrawBitmap();
            //moving verticle
            if (Hint.Checked && CurrentMovingObject.PointFIdx1 != -1 && CurrentMovingObject.PointFIdx2 == -1) //zmieniamy możliwą relację
            {
                possibleRelation = CurrentMovingObject.polygon.GetHintRelation(CurrentMovingObject.PointFIdx1);
            }

 

        }

        private double DistanceFromPointFToLine(PointF l1, PointF l2, PointF p)
        {
            if (l1.X == l2.X) return Math.Abs(l1.X - p.X);
            if (l1.Y == l2.Y) return Math.Abs(l1.Y - p.Y);

            double A = l1.Y - l2.Y;
            double B = l2.X - l1.X;
            double C = l1.X * l2.Y - l2.X * l1.Y;
            double dist = Math.Abs(A * p.X + B * p.Y + C) / (Math.Sqrt(A * A + B * B));

            return dist;
        }

        private bool IsMouseOnEdge(PointF l1, PointF l2)
        {
            if ((DistanceFromPointFToLine(l1, l2, currentPointF) < DISTANCE)
                && !(l1.Y > currentPointF.Y + DISTANCE/4 && l2.Y > currentPointF.Y + DISTANCE/4)
                && !(l1.Y < currentPointF.Y - DISTANCE/4 && l2.Y < currentPointF.Y - DISTANCE/4)
                && !(l1.X > currentPointF.X + DISTANCE/4 && l2.X > currentPointF.X + DISTANCE/4)
                && !(l1.X < currentPointF.X - DISTANCE/4 && l2.X < currentPointF.X - DISTANCE/4)) return true;
            return false;
        }
        private bool IsMouseOnPointF(PointF p)
        {
            if (Math.Abs(currentPointF.X - p.X) < DISTANCE && Math.Abs(currentPointF.Y - p.Y) < DISTANCE) return true;
            return false;
        }

        private void HandleAnimatingObject(MouseEventArgs e)
        {
            if (!IsMouseDown)
            {
                CurrentMovingObject = (null, -1, -1);
                return;
            }
            if (CurrentMovingObject.PointFIdx2 != -1) //moving edge
            {
                CurrentMovingObject.polygon.TryMoveEdge(currentPointF, e.Location, CurrentMovingObject.PointFIdx1);
                return;
            }
            if (CurrentMovingObject.PointFIdx1 != -1) //moving verticle
            {
                CurrentMovingObject.polygon.TryMoveVerticle(e.Location, CurrentMovingObject.PointFIdx1);
                return;
            }
            if (CurrentMovingObject.polygon != null)  //moving whole polygon
            {
                CurrentMovingObject.polygon.MovePolygon(currentPointF, e.Location);
                return;
            }             
        }
    }
}
