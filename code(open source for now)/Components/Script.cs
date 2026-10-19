using GameEngine2D.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine2D.Components
{
    public class Script
    {
        public GameObject gameobject { get; set; }
        public virtual void Start() { }
        public virtual void Update() { }
    }
}
