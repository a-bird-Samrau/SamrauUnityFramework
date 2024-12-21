using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Create Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private ItemCategory _category;
        [SerializeField] private float _weight;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _sprite;

        public string Name => _name;
        public ItemCategory Category => _category;
        public float Weight => _weight;
        public GameObject Prefab => _prefab;
        public Sprite Sprite => _sprite;
    }
}