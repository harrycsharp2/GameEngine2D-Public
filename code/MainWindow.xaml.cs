using GameEngine2D.Components;
using GameEngine2D.GameObjects;
using GameEngine2D.Rendering;
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

namespace GameEngine2D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// ALL OFF THE XAML/UI CODE IS UI ---- I CANNOT DO UI/XAML FOR SHIT
    /// everything is mine :) (all off the c# and SOME xaml)
    /// </summary>
    /// to do
    /// able to edit componets
    public partial class MainWindow : Window
    {
        public static GameObject CurrentSelectedGameObject;
        //opens window - entry point
        public MainWindow()
        {
            InitializeComponent();
            Renderer.Screen = GameCanvas;
            Loaded += MainWindow_Loaded;
        }
        //called when window is loaded
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Program();
        }
        //main program function
        void Program()
        {
            CompositionTarget.Rendering += Update;
        }
        //Update loop - 60 frames a second
        private void Update(object? sender, EventArgs e)
        {
            //load scripts + boxcoliders
            foreach (var go in GameObject.allgameobjects)
            {
                foreach (Script script in go.AllScripts)
                {
                    script.Update();
                }
                if (go.attachtedboxcolider != null)
                {
                    go.attachtedboxcolider.Loop();
                }
            }
            RenderGameObjects.UpdateRenderOnAllGameObjects();
            RigidBody.UpdateAllRigidBody();
            
        }
        //updates inspector text when called - called on gameobject OnClick
        public void UpdateInspectorGameObject(Vector2 Size, Vector2 Position, string Name, string Type, List<string> allcomps)
        {
            //gameobject information
            InsGOname.Text = $"Name: {Name}";
            InsGOtype.Text = $"Type: {Type}";
            InsGOsizeIPX.Text = $"{Size.X}";
            InsGOsizeIPY.Text = $"{Size.Y}";
            InsGOPositionIPX.Text = $"{Position.X}";
            InsGOPositionIPY.Text = $"{Position.Y}";

            //components tab
            CurrentComponentsIns.Text = $"Current components:";
            foreach (string component in allcomps)
            {
                CurrentComponentsIns.Text = CurrentComponentsIns.Text + $" {component}, ";
            }
        }
        //update gameobject size from inspector
        private void ApplyGONewSizeClick(object sender, RoutedEventArgs e)
        {
            Vector2 newsize;
            if (int.TryParse(InsGOsizeIPX.Text, out int X)) ;
            if (int.TryParse(InsGOsizeIPY.Text, out int Y)) ;
            newsize.X = X;
            newsize.Y = Y;
            CurrentSelectedGameObject.Size = newsize;
        }
        //update gameobject position from inspector
        private void ApplyGONewPositionClick(object sender, RoutedEventArgs e)
        {
            Vector2 newposition;
            if (int.TryParse(InsGOPositionIPX.Text, out int X)) ;
            if (int.TryParse(InsGOPositionIPY.Text, out int Y)) ;
            newposition.X = X;
            newposition.Y = Y;
            CurrentSelectedGameObject.Position = newposition;
        }
        //close app
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }
        //create game object click button
        private void CreateGameObjectClick(object sender, RoutedEventArgs e)
        {
            string name = GameObjectNameTextBox.Text;
            string type = GameObjectTypeTextBox.Text;

            foreach (string Stype in Sprite.alltypes)
            {
                if (Stype == type)
                {
                    GameObject gameobject = new GameObject();
                    gameobject.CreateGameObject(name, type);
                    RenderGameObjects.CreateRenderOfGameObject(gameobject);
                    
                    MessageBox.Show("GameObject Created");
                    return;
                }
            }
            MessageBox.Show("Couldnt find a valid type - Square, Circle");
            return;
        }
        private void AddComponentClick(object sender, RoutedEventArgs e)
        {
            string component = AddCompTextBox.Text;
            if (HandleComponents.allcomponets.Contains(component) || component.EndsWith(".cs"))
            {
                HandleComponents.AddComponentToGameObject(CurrentSelectedGameObject, component);
            }
            else
            {
                MessageBox.Show("invalid component");
            }
        }
        private void RemoveComponentClick(object sender, RoutedEventArgs e)
        {
            string component = AddCompTextBox.Text;
            HandleComponents.RemoveComponentFromGameObject(CurrentSelectedGameObject, component);
        }
        private void ComponentInfoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"sprite, rigidbody, script.cs", 
                "component info", 
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}