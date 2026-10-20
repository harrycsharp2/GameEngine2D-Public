using GameEngine2D.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameEngine2D.GameObjects
{
    public class GameObject
    {
        //class containing all gameobjects information
        //list of all gameobjects
        public static List<GameObject> allgameobjects = new List<GameObject>();
        public List<string> AllActiveComponents = new List<string>();
        public List<Script> AllScripts = new List<Script>();
        public RigidBody attachtedrigidbody;
        public Sprite attachtedsprite;
        public string Name { get; set; }    
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; } 
        //handles creation of gameobjects --- assigns vars 
        public void CreateGameObject(string name, string type)
        {
            Name = name;
            HandleComponents.AddComponentToGameObject(this, "sprite");
            attachtedsprite.StartSprite(type);
            attachtedsprite.attachtedgameobject = this;
            //automaticly set position and size open creation
            Position = new Vector2(400, 200);
            Size = new Vector2(10, 10);
            allgameobjects.Add(this);
        }
        //handle resize
        public void ResizeGameObject(Vector2 newsize)
        {
            Size = newsize;
        }
        //if gameobject is clicked
        public virtual void OnClick()
        {
            // Find the currently open MainWindow
            if (Application.Current.MainWindow is MainWindow mw)
            {
                MainWindow.CurrentSelectedGameObject = this;
                //safely get the type
                string spriteType = attachtedsprite != null ? attachtedsprite.Type : "None";
                mw.UpdateInspectorGameObject(Size, Position, Name, spriteType, AllActiveComponents);
            }
        }
    }
}
