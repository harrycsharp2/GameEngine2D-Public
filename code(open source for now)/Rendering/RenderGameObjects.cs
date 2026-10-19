using GameEngine2D.Components;
using GameEngine2D.GameObjects;
using GameEngine2D.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace GameEngine2D.Rendering
{
    public class RenderGameObjects
    {
        //creates the first renderer off the gameobject
        public static void CreateRenderOfGameObject(GameObject gameobject)
        {
            Sprite sprite = gameobject.attachtedsprite;
            //if gameobject type = square/circle assigns the correct shape to shape
            Shape? shape = null;
            if (sprite.Type == "Square")
            {
                shape = new Rectangle
                {
                    Width = gameobject.Size.X,
                    Height = gameobject.Size.Y,
                    Fill = Brushes.White,
                };
                
            }
            else if (sprite.Type == "Circle")
            {
                shape = new Ellipse
                {
                    Width = gameobject.Size.X,
                    Height = gameobject.Size.Y,
                    Fill = Brushes.White,
                };
            }
            //draws the shape
            if (shape != null)
            {
                Canvas.SetLeft(shape, gameobject.Position.X);
                Canvas.SetTop(shape, gameobject.Position.Y);
                shape.MouseLeftButtonDown += (sender, e) =>
                {
                    gameobject.OnClick();
                    e.Handled = true; // Stop click from going through to things behind it
                };
                sprite.RenderElement = shape;
                Renderer.Draw(shape);
            }
        }
        //updates all gameobjects UI element
        public static void UpdateRenderOnAllGameObjects()
        {
            foreach (var gameobject in GameObject.allgameobjects)
            {
                if (gameobject.attachtedsprite != null && gameobject.attachtedsprite.RenderElement != null)
                {
                    Sprite sprite = gameobject.attachtedsprite;

                    sprite.RenderElement.Width = gameobject.Size.X;
                    sprite.RenderElement.Height = gameobject.Size.Y;

                    Canvas.SetLeft(sprite.RenderElement, gameobject.Position.X);
                    Canvas.SetTop(sprite.RenderElement, gameobject.Position.Y);
                }
            }
        }
    }
}
