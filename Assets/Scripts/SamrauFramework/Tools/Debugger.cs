using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework.Tools
{
    public class Debugger : Core.Behaviour
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}