using GameEngine2D.GameObjects;
using GameEngine2D.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace GameEngine2D.Components
{
    public class HandleComponents
    {
        public static string[] allcomponets = { "rigidbody", "sprite" };
        public static void AddComponentToGameObject(GameObject gameobject, string component)
        {
            if (gameobject != null && 
                component != null)
            {
                if (component == "rigidbody" && !gameobject.AllActiveComponents.Contains("rigidbody"))
                {
                   

                    RigidBody rb = new RigidBody();
                    gameobject.attachtedrigidbody = rb;
                    gameobject.AllActiveComponents.Add("rigidbody");
                    rb.StartRigidBody(gameobject);
                    MessageBox.Show($"Added: {component} To: {gameobject.Name}");

                }
                else if (component == "sprite" && !gameobject.AllActiveComponents.Contains("sprite"))
                {
                    Sprite sp = new Sprite();
                    sp.Type = "Square";
                    gameobject.attachtedsprite = sp;
                    gameobject.AllActiveComponents.Add("sprite");
                    RenderGameObjects.CreateRenderOfGameObject(gameobject);
                    MessageBox.Show($"Added: {component} To: {gameobject.Name}");
                }
                else if (component.EndsWith(".cs"))
                {
                    string scriptFileName = component.Trim();
                    string scriptsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
                    string fullScriptPath = Path.Combine(scriptsDirectory, scriptFileName);
                    Type? scriptType = null;
                    // Check if the file exists in the external scripts folder
                    if (File.Exists(fullScriptPath))
                    {
                        scriptType = ScriptCompiler.CompileScriptFromFile(fullScriptPath);
                    }
                    else
                    {
                        //Check if it's already compiled inside the project assembly
                        string className = scriptFileName.Replace(".cs", "");
                        scriptType = typeof(Script).Assembly.GetTypes()
                            .FirstOrDefault(t => typeof(Script).IsAssignableFrom(t) && t.Name.Equals(className, StringComparison.OrdinalIgnoreCase));
                    }
                    if (scriptType != null)
                    {
                        Script scr = (Script)Activator.CreateInstance(scriptType)!;
                        scr.gameobject = gameobject;
                        gameobject.AllScripts.Add(scr);
                        gameobject.AllActiveComponents.Add(scriptType.Name);
                        scr.Start();
                        MessageBox.Show($"Attached script: {scriptType.Name} to {gameobject.Name}");
                    }
                }
                else
                {
                    MessageBox.Show("couldnt find a valid type of component");
                }
            }
            else
            {
                MessageBox.Show("either gameobject/component is null - please enter a valid gameobject + comoponet");
            }
        }
        public static void RemoveComponentFromGameObject(GameObject gameobject, string component)
        {
            if (gameobject == null)
            {
                return;
            }
            if (component == "rigidbody" && gameobject.attachtedrigidbody != null)
            {
                RigidBody rb = gameobject.attachtedrigidbody;
                //remove from global physcics loop
                RigidBody.allRigidBodies.Remove(rb);
                rb.AttachedObject = null;

                gameobject.attachtedrigidbody = null;
                gameobject.AllActiveComponents.Remove("rigidbody");
            }
            else if (component == "sprite" && gameobject.attachtedsprite != null)
            {
                Sprite sprite = gameobject.attachtedsprite;
                //remove visible sprite from game canvas
                if (sprite.RenderElement != null)
                {
                    Renderer.Screen?.Children.Remove(sprite.RenderElement);

                }
                //remove from gameobject
                gameobject.attachtedsprite = null;
                gameobject.AllActiveComponents.Remove("sprite");
            }
        }
    }

}
