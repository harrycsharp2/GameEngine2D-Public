using System.Windows;
using System.Windows.Controls;
namespace GameEngine2D.Rendering
{
    public static class Renderer
    {
        //draws ui
        public static Canvas Screen { get; set; }
        public static void Draw(UIElement element)
        {
            Screen?.Children.Add(element);
        }
    }
}