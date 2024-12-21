using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Create Level")]
    public class Level : ScriptableObject,
        ILevel
    {
        [SerializeField] private string _name;
        
        public string Name => _name;
    }
}