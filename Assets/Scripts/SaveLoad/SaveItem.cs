using System;
using Player;
using UnityEngine;

namespace SaveLoad
{
    [Serializable]
    public class SaveItem
    {
        public string Path;
        public int Count;

        public Item GetItemFromResources()
        {
            return Resources.Load<Item>(Path);
        }
    }
}