using System;
using Engine;

namespace SaveLoad
{
    [Serializable]
    public class SaveLevel : ILevel
    {
        public string Path;
        public string Name => Path;
    }
}