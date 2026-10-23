using GameEngine2D.GameObjects;
using GameEngine2D.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngine2D.Components
{
    public class Sprite
    {
        public static string[] alltypes = { "Square", "Circle" };
        public string Type { get; set; }
        public FrameworkElement? RenderElement { get; set; }
        public GameObject attachtedgameobject { get; set; }
        public void StartSprite(string type)
        {
            Type = type;
        }
        public void ChangeSprite(string type )
        {
            if (alltypes.Contains(type))
            {
                Type = type;
                if (RenderElement != null)
                {
                    Renderer.Screen?.Children.Remove(RenderElement);
                    RenderElement = null;
                }
                RenderGameObjects.CreateRenderOfGameObject(attachtedgameobject);
            }
        }
    }
}
