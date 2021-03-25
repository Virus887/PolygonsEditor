using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKProject1
{
    public partial class MainForm : Form
    { 
        public enum RelationType
        {
            ConstantLength,
            Horizontal,
            Vertical,
            None
        }

        public class Polygon
        {
            public List<PointF> verticles;
            public GraphicsPath graphicsPath;
            public List<RelationType> relations;          

            public Polygon(List<PointF> v)
            {
                verticles = v;
                graphicsPath = new GraphicsPath();
                relations = new List<RelationType>();
                for (int i = 0; i < v.Count; i++) relations.Add(RelationType.None);
                UpdateGraphicsPathAndCorrectLines();
            }

            #region MOVING
            public bool TryMoveVerticle (PointF where, int idx)
            {
                if (IsUncorrectableVerticle(idx))
                {
                    MovePolygon(verticles[idx], where);
                    return false;
                }

                float dx = where.X - verticles[idx].X;
                float dy = where.Y - verticles[idx].Y;
                verticles[idx] = where;
                int i = 0;              
                int counter = 0;
                float dxf = dx, dxb = dx, dyf = dy, dyb = dy;
                while (counter < verticles.Count - 1)
                {
                    if ((dxf, dyf) != (-1, -1)) (dxf, dyf) = CorrectVerticleForward(dxf, dyf, RealIndex(idx + i), ref counter);
                    if (counter >= verticles.Count - 1) break;
                    if ((dxb, dyb) != (-1, -1)) (dxb, dyb) = CorrectVerticleBackward(dxb, dyb, RealIndex(idx - i), ref counter);
                    if ((dxf, dxb, dyf, dyb) == (-1, -1, -1, -1)) break;
                    if (++i == verticles.Count) break;
                }
                UpdateGraphicsPathAndCorrectLines();
                return true;
            }

            public bool TryMoveEdge(PointF from, PointF to, int idx1)
            {
                float dx = to.X - from.X;
                float dy = to.Y - from.Y;
                int idx2 = RealIndex(idx1 + 1);
                if (IsUncorrectableEdge(idx1, idx2))
                {
                    MovePolygon(from, to);
                    return false;
                }

                verticles[idx1] = new PointF(verticles[idx1].X + dx, verticles[idx1].Y + dy);
                verticles[idx2] = new PointF(verticles[idx2].X + dx, verticles[idx2].Y + dy);

                int i = 0;
                int counter = 0;
                float dxf = dx, dxb = dx, dyf = dy, dyb = dy;
                while (counter < verticles.Count - 2)
                {
                    if ((dxf, dyf) != (-1, -1)) (dxf, dyf) = CorrectVerticleForward(dxf, dyf, RealIndex(idx2 + i), ref counter);
                    if (counter >= verticles.Count - 2) break;
                    if ((dxb, dyb) != (-1, -1)) (dxb, dyb) = CorrectVerticleBackward(dxb, dyb, RealIndex(idx1 - i), ref counter);
                    if ((dxf, dxb, dyf, dyb) == (-1, -1, -1, -1)) break;
                    if (++i == verticles.Count) break;
                }
                UpdateGraphicsPathAndCorrectLines();
                return true;
            }

            private (float dx, float dy) CorrectVerticleForward(float dx, float dy, int idx1, ref int counter)
            {
                int idx2;
                PointF newPoint;
                switch (GetEdgeRelation(idx1, idx2 = RealIndex(idx1 + 1)))
                {
                    case RelationType.ConstantLength:
                        {
                            counter++;
                            newPoint = new PointF(verticles[idx2].X + dx, verticles[idx2].Y + dy);                        
                            break;
                        }
                    case RelationType.Horizontal:
                        {
                            counter++;
                            newPoint = new PointF(verticles[idx2].X , verticles[idx2].Y + dy);                       
                            break;
                        }
                    case RelationType.Vertical:
                        {
                            counter++;
                            newPoint = new PointF(verticles[idx2].X + dx , verticles[idx2].Y );                      
                            break;
                        }
                    default:
                        {
                            return (-1, -1);
                        }
                }
                dx = newPoint.X - verticles[idx2].X;
                dy = newPoint.Y - verticles[idx2].Y;
                verticles[idx2] = newPoint;
                return (dx, dy);
            }


            private (float dx, float dy) CorrectVerticleBackward(float dx, float dy, int idx1, ref int counter)
            {
                int idx2;
                if (idx1 == 0) idx2 = verticles.Count - 1;
                else idx2 = idx1 - 1; 
                PointF newPoint;
                switch (GetEdgeRelation(idx1, idx2))
                {
                    case RelationType.ConstantLength:
                        {
                            counter++;
                            newPoint = new PointF(verticles[idx2].X + dx, verticles[idx2].Y + dy);
                            break;
                        }
                    case RelationType.Horizontal:
                        {
                            counter++;
                            newPoint = new PointF(verticles[idx2].X , verticles[idx2].Y + dy);
                            break;
                        }
                    case RelationType.Vertical:
                        {
                            counter++;
                            newPoint = new PointF(verticles[idx2].X + dx, verticles[idx2].Y );                   
                            break;
                        }
                    default:
                        {                       
                            return (-1, -1);
                        }
                }
                dx = newPoint.X - verticles[idx2].X;
                dy = newPoint.Y - verticles[idx2].Y;
                verticles[idx2] = newPoint;
                return (dx, dy);
            }

            public void MovePolygon(PointF from, PointF to)
            {
                float dx = to.X - from.X;
                float dy = to.Y - from.Y;
                for(int i=0; i<verticles.Count; i++)
                {
                    verticles[i] = new PointF(verticles[i].X + dx, verticles[i].Y + dy);                   
                }
                UpdateGraphicsPathAndCorrectLines();               
            }

            private bool IsUncorrectableVerticle(int idx)
            {
                int l = 0, h = 0, v = 0;
                foreach (RelationType type in relations)
                {
                    if (type == RelationType.ConstantLength) l++;
                    if (type == RelationType.Horizontal) h++;
                    if (type == RelationType.Vertical) v++;
                }
                if (l + h + v < verticles.Count) return false;
                if (l + h + v == verticles.Count && h % 2 != 0 && v%2 != 0) return true;

                int forwardCounter = (verticles.Count-2)/2 + verticles.Count % 2;

                int idx1 = RealIndex(forwardCounter + idx);
                int currIdx = RealIndex(forwardCounter + idx + 1);
                int idx2 = RealIndex(idx1 + 2);

                if (GetEdgeRelation(idx1, currIdx) == RelationType.ConstantLength && GetEdgeRelation(currIdx, idx2) == RelationType.Vertical) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.ConstantLength && GetEdgeRelation(currIdx, idx2) == RelationType.Horizontal) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.Horizontal && GetEdgeRelation(currIdx, idx2) == RelationType.ConstantLength) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.Vertical && GetEdgeRelation(currIdx, idx2) == RelationType.ConstantLength) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.ConstantLength && GetEdgeRelation(currIdx, idx2) == RelationType.ConstantLength) return true;
                return false;
            }

            private bool IsUncorrectableEdge(int idx1, int idx2)
            {
                int l = 0, h = 0, v = 0;
                foreach (RelationType type in relations)
                {
                    if (type == RelationType.ConstantLength) l++;
                    if (type == RelationType.Horizontal) h++;
                    if (type == RelationType.Vertical) v++;
                }
                if (l + h + v == verticles.Count - 1 && GetEdgeRelation(idx1, idx2) == RelationType.None) return true;
                if (l + h + v < verticles.Count) return false;
                if (l + h + v == verticles.Count && h % 2 != 0 && v % 2 != 0) return true;

                int forwardCounter = (verticles.Count - 3) / 2 + (verticles.Count-1) % 2;

                idx1 = RealIndex(forwardCounter + idx1);
                int currIdx = RealIndex(forwardCounter + idx1 + 1);
                idx2 = RealIndex(idx1 + 2);

                if (GetEdgeRelation(idx1, currIdx) == RelationType.ConstantLength && GetEdgeRelation(currIdx, idx2) == RelationType.Vertical) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.ConstantLength && GetEdgeRelation(currIdx, idx2) == RelationType.Horizontal) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.Horizontal && GetEdgeRelation(currIdx, idx2) == RelationType.ConstantLength) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.Vertical && GetEdgeRelation(currIdx, idx2) == RelationType.ConstantLength) return true;
                if (GetEdgeRelation(idx1, currIdx) == RelationType.ConstantLength && GetEdgeRelation(currIdx, idx2) == RelationType.ConstantLength) return true;
                return false;

            }

            #endregion MOVING


            #region EDITING
            public bool TryDeleteVerticle(int idx1)
            {
                if (verticles.Count < 4) return false;
                if (idx1 == -1) return false;
                verticles.RemoveAt(idx1);
                relations.RemoveAt(idx1);
                if (idx1 == 0)
                {
                    relations[verticles.Count-1] = RelationType.None;
                }
                else
                {
                    relations[idx1 - 1] = RelationType.None;
                }
                UpdateGraphicsPathAndCorrectLines();
                return true;
            }

            public bool TryDeleteEdge(int idx1, int idx2)
            {
                if (verticles.Count < 5) return false;
                if (idx1 == -1 || idx2 == -1) return false;

                if (idx1 > idx2) SwapValues(ref idx1, ref idx2);

                verticles.RemoveAt(idx2);
                verticles.RemoveAt(idx1);
                relations.RemoveAt(idx2);
                relations.RemoveAt(idx1);

                if (idx1 == 0)
                {
                    relations[verticles.Count - 1] = RelationType.None;
                }
                else
                {
                    relations[idx1 - 1] = RelationType.None;
                }

                UpdateGraphicsPathAndCorrectLines();
                return true;
            }

            public bool TryPutVerticleOnEdge(int idx1, int idx2, PointF p)
            {
                if (idx1 == -1 || idx2 == -1) return false;

                if (idx1 > idx2) SwapValues(ref idx1, ref idx2);

                //Calculate new PointF:
                PointF newPointF = CalculateRelativePointFToLine(verticles[idx1], verticles[idx2], p);
               
                if (idx1 == 0 && idx2 != 1) // first and last
                {
                    relations[verticles.Count - 1] = RelationType.None;
                    verticles.Add(newPointF);
                    relations.Add(RelationType.None);
                }
                else
                {
                    relations[idx1] = RelationType.None;
                    verticles.Insert(idx2, newPointF);
                    relations.Insert(idx1, RelationType.None);
                }

                UpdateGraphicsPathAndCorrectLines();
                return true;
            }
            #endregion EDITING


            #region  RELATIONS
            public RelationType GetEdgeRelation(int idx1, int idx2)
            {
                if (idx1 == -1 || idx2 == -1) return RelationType.None;

                if (idx1 > idx2) SwapValues(ref idx1, ref idx2);

                if (idx1 == 0 && idx2 == verticles.Count -1) return relations[idx2];
                return relations[idx1];
            }

            public bool TrySetRelation(int idx1, int idx2, RelationType type)
            {
                if (idx1 == -1 || idx2 == -1) throw new Exception("TrySetRelation: index = -1.");

                int l = 0, h = 0, v = 0;
                foreach (RelationType type1 in relations)
                {
                    if (type1 == RelationType.ConstantLength) l++;
                    if (type1 == RelationType.Horizontal) h++;
                    if (type1 == RelationType.Vertical) v++;
                }
                int counter = 0;
                if (l + h + v == relations.Count - 1) counter++;
                //Delete previous relation:
                if (idx1 > idx2) SwapValues(ref idx1, ref idx2);

                if (type == RelationType.Vertical || type == RelationType.Horizontal)
                {
                    // if H+H or V+V
                    if (idx1 == 0 && idx2 == verticles.Count -1)
                    {
                        // "-" i "|"
                        if (GetEdgeRelation(verticles.Count-1, verticles.Count - 2) == type) return false;
                        if (GetEdgeRelation(0, 1) == type) return false;
                        // "L" + "L" + "|"\"-"                      
                        if (GetEdgeRelation(verticles.Count - 1, verticles.Count - 2) == RelationType.ConstantLength) counter++;
                        if (GetEdgeRelation(0, 1) == RelationType.ConstantLength) counter++;
                    }    
                    else
                    {
                        // "-" i "|"
                        if (GetEdgeRelation(idx2, RealIndex(idx2 + 1)) == type) return false;
                        if (GetEdgeRelation(idx1, RealIndex(idx1 - 1)) == type) return false;
                        // "L" + "L" + "|"\"-"   
                        if (GetEdgeRelation(idx2, RealIndex(idx2 + 1)) == RelationType.ConstantLength) counter++;
                        if (GetEdgeRelation(idx1, RealIndex(idx1 - 1)) == RelationType.ConstantLength) counter++;
                    }

                    if (counter == 3) return false;
                }

                if (idx1 == 0 && idx2 == verticles.Count - 1)
                {
                    relations[idx2] = RelationType.None;
                }
                else
                {
                    relations[idx1] = RelationType.None;
                }

                switch (type)
                {
                    case RelationType.ConstantLength:
                        {                          
                            HandleConstantLengthRelationAdding(idx1, idx2);
                            break;
                        }
                    case RelationType.Horizontal:
                        {
                            HandleHorizontalRelationAdding(idx1, idx2);
                            break;
                        }
                    case RelationType.Vertical:
                        {
                            HandleVerticalRelationAdding(idx1, idx2);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (idx1 == 0 && idx2 == verticles.Count - 1)
                {
                    relations[idx2] = type;
                }
                else
                {
                    relations[idx1] = type;
                }

                return true;
            }

            private void HandleConstantLengthRelationAdding(int idx1, int idx2)
            {
                float length = (float)(Math.Sqrt(Math.Pow((verticles[idx1].X - verticles[idx2].X), 2) + Math.Pow(verticles[idx1].Y - verticles[idx2].Y, 2)));
                float constLength = length;

                int l = 0, h = 0, v = 0;
                foreach (RelationType type in relations)
                {
                    if (type == RelationType.ConstantLength) l++;
                    if (type == RelationType.Horizontal) h++;
                    if (type == RelationType.Vertical) v++;
                }

                if (!(l + h + v == verticles.Count - 1 && v%2==1 && h%2==1))
                {
                    string promptValue = Prompt.ShowDialog("Type length:", "Set length relation", length.ToString());
                    if (!float.TryParse(promptValue, out constLength)) //throw new Exception("invalid input.");
                        if (constLength <= 0) throw new Exception("invalid input.");
                }
                float change = constLength / length;

                int changedPoint;
                PointF where;
                if (verticles[idx1].X > verticles[idx2].X)
                {
                    changedPoint = idx1;
                    where = new PointF(verticles[idx2].X + (verticles[idx1].X - verticles[idx2].X) * change,
                                       verticles[idx2].Y + (verticles[idx1].Y - verticles[idx2].Y) * change);
                }
                else
                {
                    changedPoint = idx2;
                    where = new PointF(verticles[idx1].X + (verticles[idx2].X - verticles[idx1].X) * change,
                                       verticles[idx1].Y + (verticles[idx2].Y - verticles[idx1].Y) * change);
                }
                TryMoveVerticle(where, changedPoint);
            }
            private void HandleHorizontalRelationAdding(int idx1, int idx2)
            {
                float length = (float)Math.Sqrt(Math.Pow((verticles[idx1].X - verticles[idx2].X), 2) + Math.Pow(verticles[idx1].Y - verticles[idx2].Y, 2));
                int changedPoint;
                PointF where;

                if (verticles[idx1].X > verticles[idx2].X)
                {
                    changedPoint = idx1;
                    where = new PointF(verticles[idx2].X + length, verticles[idx2].Y);
                }
                else
                {
                    changedPoint = idx2;
                    where = new PointF(verticles[idx1].X + length, verticles[idx1].Y);
                }
                TryMoveVerticle(where, changedPoint);
            }

            private void HandleVerticalRelationAdding(int idx1, int idx2)
            {
                float length = (float)(Math.Sqrt(Math.Pow((verticles[idx1].X - verticles[idx2].X), 2) + Math.Pow(verticles[idx1].Y - verticles[idx2].Y, 2)));

                int changedPoint;
                PointF where;

                if (verticles[idx1].Y > verticles[idx2].Y)
                {
                    changedPoint = idx1;
                    where = new PointF(verticles[idx2].X, verticles[idx2].Y + length);
                }
                else
                {
                    changedPoint = idx2;
                    where = new PointF(verticles[idx1].X, verticles[idx1].Y + length);
                }
                TryMoveVerticle(where, changedPoint);
            }

            #endregion  RELATIONS


            #region  ANOTHER FUNCTIONS

            private void UpdateGraphicsPathAndCorrectLines()
            {
                graphicsPath.Reset();
                for (int i = 0; i < verticles.Count - 1; i++)
                {
                    graphicsPath.AddLine(verticles[i], verticles[i + 1]);
                    if (relations[i] == RelationType.Horizontal) verticles[i] = new PointF(verticles[i].X, verticles[i+1].Y);
                    if (relations[i] == RelationType.Vertical) verticles[i] = new PointF(verticles[i + 1].X, verticles[i].Y);
                }
                if (relations[verticles.Count-1] == RelationType.Horizontal) verticles[verticles.Count - 1] = new PointF(verticles[verticles.Count - 1].X, verticles[0].Y);
                if (relations[verticles.Count - 1] == RelationType.Vertical) verticles[verticles.Count - 1] = new PointF(verticles[0].X, verticles[verticles.Count - 1].Y);
            }

            private void SwapValues(ref int a,ref int b)
            {
                int tmp;
                tmp = a;
                a = b;
                b = tmp;               
            }

            private PointF CalculateRelativePointFToLine(PointF p1, PointF p2, PointF p3)
            {
                float newX = (p1.X + p2.X) / 2;
                float newY = (p1.Y +p2.Y)/ 2;
                return new PointF(newX, newY);
            }

            private int RealIndex(int idx)
            {
                if (idx >= 0 && idx < verticles.Count) return idx;
                while (idx < 0) idx += verticles.Count;
                return idx % verticles.Count;
            }

            #endregion  ANOTHER FUNCTIONS

            #region LAB
            public (RelationType, int i, int i2) GetHintRelation(int idx)
            {
                float x = verticles[idx].X;
                float y = verticles[idx].Y;

                if (GetEdgeRelation(idx, RealIndex(idx + 1)) == RelationType.None && IsNearVertical(verticles[idx], verticles[RealIndex(idx + 1)])) return (RelationType.Vertical,idx, RealIndex(idx + 1));
                if (GetEdgeRelation(idx, RealIndex(idx + 1)) == RelationType.None && IsNearVertical(verticles[idx], verticles[RealIndex(idx - 1)])) return (RelationType.Vertical,idx, RealIndex(idx - 1));
                if (GetEdgeRelation(idx, RealIndex(idx + 1)) == RelationType.None && IsNearHorizontal(verticles[idx], verticles[RealIndex(idx + 1)])) return (RelationType.Horizontal,idx, RealIndex(idx + 1));
                if (GetEdgeRelation(idx, RealIndex(idx + 1)) == RelationType.None && IsNearHorizontal(verticles[idx], verticles[RealIndex(idx - 1)])) return (RelationType.Horizontal,idx, RealIndex(idx - 1));

                return (RelationType.None, -1, -1);
            }
            
            private bool IsNearVertical(PointF p1, PointF p2)
            {
                if (Math.Abs(p1.X - p2.X) < DISTANCE) return true;
                return false;
            }
            private bool IsNearHorizontal(PointF p1, PointF p2)
            {
                if (Math.Abs(p1.Y - p2.Y) < DISTANCE) return true;
                return false;
            }
            #endregion
        }
    }
}

