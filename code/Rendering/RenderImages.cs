using GameEngine2D.Components;
using GameEngine2D.GameObjects;
using GameEngine2D.Rendering;
using GameEngine2D.Images;
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
    public class RenderImages
    {
        public static void CreateFirstRenderOfImage(Images.Images image)
        {
            //image is the gameobject/image
            //the UIimage inside the image class is the UIelement
            if (image != null)
            {
                Image UIimage = image.UIimage;
                if (UIimage != null)
                {
                    UIimage.Width = image.Size.X;
                    UIimage.Height = image.Size.Y;
                    Canvas.SetTop(UIimage, image.Position.Y);
                    Canvas.SetLeft(UIimage, image.Position.X);
                    UIimage.MouseLeftButtonDown += (sender, e) =>
                    {
                        image.OnClick();
                        e.Handled = true;
                    };
                    Renderer.Draw(UIimage);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        public static void RenderAllImages()
        {
            foreach (var image in Images.Images.AllImages)
            {
                if (image.isactive == true 
                    && image.UIimage != null)
                {
                    Image UI = image.UIimage;
                    UI.Width = image.Size.X;
                    UI.Height = image.Size.Y;
                    Canvas.SetLeft(UI, image.Position.X);
                    Canvas.SetTop(UI, image.Position.Y);

                }
            }
        }
    }
}
