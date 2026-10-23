using System;
using GameEngine2D.GameObjects;
using GameEngine2D.Components;
using GameEngine2D.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameEngine2D.Components
{
    public class BoxColider
    {
        //has a position and size - get 4 boundary edges from this
        // left = Position.x / right = position.x + size.x / top = postion.y / bottom = position.y + size.y // 
        // to check of box A and box B colilde is it check if it is impossible for them to touch - a.right < b.right - a.left > b.right - a.bottom < b.top - a.top > b.bottom
        //IsColliding=(A.Right>B.Left)∧(A.Left<B.Right)∧(A.Bottom>B.Top)∧(A.Top<B.Bottom)  - if NONE off them are true its overlapping - 1+ true = not overlapping
        //collision resolution - how to stop objects
        //calculate the penetration depth (overlap) how many pixels. 
        //OverlapX=min(A.Right−B.Left, B.Right−A.Left)
        //OverlapY=min(A.Bottom−B.Top, B.Bottom−A.Top)
        //Push along the shallowest axis: Find whichever overlap is smaller:
        // overlapY < overlapX = vertically 
        // overaly x < overlapy = horizontally
        public bool IsActive = true;
        public bool IsColider = true;
        public bool IsTrigger = false;
        public bool HasColided = false;
        public bool HasTriggerd = false;
        public float Left = 0f;
        public float Right = 0f;
        public float Top = 0f;
        public float Bottom = 0f;
        public GameObject AttachtedGameobject;
        public GameObject ColidedObject;
        public void StartColider(GameObject gameobject)
        {
            AttachtedGameobject = gameobject;
            gameobject.attachtedboxcolider = this;
            
            //setting position
            Left = gameobject.Position.X;
            Right = gameobject.Position.X + gameobject.Size.X;
            Top = gameobject.Position.Y;
            Bottom = gameobject.Position.Y + gameobject.Size.Y;
        }
        public void IsColiderLogic()
        {
            if (AttachtedGameobject != null
                && IsActive == true
                && IsColider == true
                && IsTrigger == false)
            {
                Left = AttachtedGameobject.Position.X;
                Right = AttachtedGameobject.Position.X + AttachtedGameobject.Size.X;
                Top = AttachtedGameobject.Position.Y;
                Bottom = AttachtedGameobject.Position.Y + AttachtedGameobject.Size.Y;
                
                //checks all coliders to see if its coliding
                var gameobjectlist = GameObject.allgameobjects.ToList();
                foreach ( var gameobject in gameobjectlist)
                {
                    if (gameobject == this.AttachtedGameobject) continue;
                    if (gameobject.attachtedboxcolider != null)
                    {
                        // CHECK: If ANY of these 4 are true, they are NOT touching at all:
                        if (this.Right < gameobject.attachtedboxcolider.Left ||  // A is completely to the left of B
                            this.Left > gameobject.attachtedboxcolider.Right ||  // A is completely to the right of B
                            this.Bottom < gameobject.attachtedboxcolider.Top ||  // A is completely above B
                            this.Top > gameobject.attachtedboxcolider.Bottom)     // A is completely below B
                        {
                            HasColided = false;
                        }
                        else
                        {
                            HasColided = true;
                            ColidedObject = gameobject;
                            break;
                        }
                    }
                }
                //if has colided ...
                if (HasColided == true && ColidedObject != null)
                {
                    //calculate the penetration depth (overlap) how many pixels. 
                    //OverlapX=min(A.Right−B.Left, B.Right−A.Left) / OverlapY=min(A.Bottom−B.Top, B.Bottom−A.Top)
                    //Push along the shallowest axis: Find whichever overlap is smaller:
                    // overlapY < overlapX = vertically / overaly x < overlapy = horizontally
                    float overlapX = MathF.Min(this.Right - ColidedObject.attachtedboxcolider.Left, ColidedObject.attachtedboxcolider.Right - this.Left);
                    float overlapY = MathF.Min(this.Bottom - ColidedObject.attachtedboxcolider.Top, ColidedObject.attachtedboxcolider.Bottom - this.Top);

                    // Calculate center points of both objects
                    float myCenterX = AttachtedGameobject.Position.X + (AttachtedGameobject.Size.X / 2f);
                    float otherCenterX = ColidedObject.Position.X + (ColidedObject.Size.X / 2f);
                    float myCenterY = AttachtedGameobject.Position.Y + (AttachtedGameobject.Size.Y / 2f);
                    float otherCenterY = ColidedObject.Position.Y + (ColidedObject.Size.Y / 2f);
                    
                    if (overlapX < overlapY)
                    {
                        //horizontally colided
                        if (myCenterX < otherCenterX)
                        {
                            // We are on the left side push left -x
                            RigidBody rb = AttachtedGameobject.attachtedrigidbody;
                            Vector2 d;
                            d.Y = 0;
                            d.X = -1;
                            if (rb != null)
                            {
                                rb.RigidBodyMoveCall(d, overlapX);
                            }
                        }
                        else
                        {
                            //we are on the right side - push right +x
                            RigidBody rb = AttachtedGameobject.attachtedrigidbody;
                            Vector2 d;
                            d.Y = 0;
                            d.X = 1;
                            if (rb != null)
                            {
                                rb.RigidBodyMoveCall(d, overlapX);
                            }
                        }
                    }
                    else if (overlapY < overlapX)
                    {
                        //vertically colided
                        if (myCenterY < otherCenterY)
                        {
                            //hit colidedobjects top --- push up
                            RigidBody rb = AttachtedGameobject.attachtedrigidbody;
                            Vector2 d;
                            d.Y = -1;
                            d.X = 0;
                            if (rb != null)
                            {
                                rb.RigidBodyMoveCall(d, overlapY);
                                //set fallspeed to 0 so the gravity doesnt make the gameobject move into the colider again
                                rb.FallSpeed = 0;
                            }
                        }
                        else
                        {
                            //hit colidedobject bottom - push down
                            RigidBody rb = AttachtedGameobject.attachtedrigidbody;
                            Vector2 d;
                            d.Y = 1;
                            d.X = 0;
                            if (rb != null)
                            {
                                rb.RigidBodyMoveCall(d, overlapY);
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            return;
        }
        public void IsTriggerLogic()
        {

        }
        public void Loop()
        { 
            if (IsTrigger == true && IsColider == false)
            {
                IsTriggerLogic();
            }
            else if (IsTrigger == false && IsColider == true)
            {
                IsColiderLogic();
            }
        }
    }
}
