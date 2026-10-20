using GameEngine2D.GameObjects;
using GameEngine2D.Rendering;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace GameEngine2D.Components
{
    public class RigidBody
    {
        // Static list containing all active rigidbodies
        public static List<RigidBody> allRigidBodies = new List<RigidBody>();
        // The GameObject this RigidBody will move
        public GameObject? AttachedObject;
        
        public float FallSpeed;
        public float weight = 1;
        public bool ApplyGravity = false;
        //creating a rigidbody
        public void StartRigidBody(GameObject gameobject)
        {
            AttachedObject = gameobject;
            FallSpeed = weight * 2;
            allRigidBodies.Add(this);
            return;
        }
        public void RigidBodyLogic()
        {
            if (ApplyGravity == true && AttachedObject != null)
            {
                //gravity
                AttachedObject.Position = new Vector2(
                   AttachedObject.Position.X,
                   AttachedObject.Position.Y + FallSpeed
               );
            }
        }
       
        //calls all instances off rigidbodys logic
        //loop called in Update()
        public static void UpdateAllRigidBody()
        {
            foreach (var rigidbody in allRigidBodies)
            {
                rigidbody.RigidBodyLogic();
            }
        }
        //USER FUNCTIONS
        //user function for moving rigidbody
        public void RigidBodyMoveCall(Vector2 movedirection, float speed)
        {
            //movedirection.x/y should  = -1/ 1 
            //called like RigidBodyMoveCall(movedirection, 20f)
            if (movedirection.X == -1
                || movedirection.X == 1
                || movedirection.Y == 1
                || movedirection.Y == -1)
            {
                if (AttachedObject != null)
                {
                    AttachedObject.Position = AttachedObject.Position + (movedirection * speed);
                }
            }
        }
        public void RigidBodyAddForce(Vector2 direction, float speed)
        {
            if (AttachedObject != null)
            {
                AttachedObject.Position = AttachedObject.Position + (direction * speed);
            }
        }
        //user function`s for enabling/disable gravity
        public void RigidBodyEnableGravity()
        {
            ApplyGravity = true;
        }
        public void RigidBodyDisableGravity()
        {
            ApplyGravity = false;
        }
        //change weight
        public void RigidBodyChangeWeight(float Iweight)
        {
            weight = Iweight;
        }
        
    }
}
