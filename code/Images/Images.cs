using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine2D.scripts;
using GameEngine2D.Rendering;
using GameEngine2D.Components;
using System.Numerics;
using System.Windows;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace GameEngine2D.Images
{
    public class Images
    {
        public static List<Images> AllImages = new List<Images>();
        public List<string> ActiveComponents = new List<string>();
        public List<Script> AllScripts = new List<Script>();
        public Image UIimage;
        public RigidBody rb;
        public BoxColider bc;
        public Vector2 Size;
        public Vector2 Position;
        public string ImageDirecitory = "";
        public string Name;
        public bool isactive = true;

        public void CreateImage(string filename, string name)
        {

            if (Directory.Exists("images"))
            {
                MessageBox.Show("found image file");
                string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                string fullimagepath = Path.Combine(imagesDirectory, filename);
                if (File.Exists(fullimagepath))
                {
                    UIimage = new Image();
                    UIimage.Source = new BitmapImage(new Uri(fullimagepath));
                    Size = new Vector2(40, 40);
                    Position = new Vector2(400, 400);
                    ImageDirecitory = fullimagepath;
                    Name = name;
                    AllImages.Add(this);
                    RenderImages.CreateFirstRenderOfImage(this);
                }
                else
                {
                    MessageBox.Show("couldnt find the entered image filename in `imager` file");
                }
            }
            else if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
                MessageBox.Show("couldnt find image file so one was created - try again");
                return;
            }
        }
        public virtual void OnClick()
        {
            if (Application.Current.MainWindow is MainWindow mw)
            {
                MainWindow.CurrentSelectedGameObject = null;
                MainWindow.CurrentSelectedImage = this;
                mw.UpdateInspectorGameObject(Size, Position, Name, "image", ActiveComponents);
            }
        }
    }
}
