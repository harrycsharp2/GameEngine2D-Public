using GameEngine2D.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameEngine2D.Components
{
    public class Sprite
    {
        public static string[] alltypes = { "Square", "Circle" };
        public string Type { get; set; }
        public FrameworkElement? RenderElement { get; set; }

        public void StartSprite(string type)
        {
            Type = type;
        }
    }
}
