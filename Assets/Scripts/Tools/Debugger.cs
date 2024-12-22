using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Tools
{
    public class Debugger : Behaviour
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}