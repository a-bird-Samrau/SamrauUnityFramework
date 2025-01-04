using System;
using SamrauFramework.Engine;

namespace SamrauFramework.SaveLoad
{
    [Serializable]
    public class SaveLevel : ILevel
    {
        public string Path;
        public string Name => Path;
    }
}