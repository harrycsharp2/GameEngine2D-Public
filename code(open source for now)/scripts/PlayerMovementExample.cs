using GameEngine2D.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameEngine2D.scripts
{
    public class PlayerMovementExample : Script
    {
        public Vector2 moveposition;
        public RigidBody rb;
        float speed = 10f;
        public override void Start()
        {
            moveposition = new Vector2();
            rb = gameobject.attachtedrigidbody;
        }
        public override void Update()
        {
            //EXAMPLE SCRIPT

            //EXAMPLE SCRIPT

            //EXAMPLE SCRIPT
            if (rb != null)
            {
                moveposition.X = 0;
                moveposition.Y = 0;

                if (Keyboard.IsKeyDown(Key.W))
                {
                    moveposition.Y = -1;
                }

                rb.RigidBodyMoveCall(moveposition, speed);
            }
        }
    }
}
