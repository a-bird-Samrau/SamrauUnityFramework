using UnityEngine;
using UnityEngine.UI;

namespace SamrauFramework
{
    public static class Extensions
    {
        public static void Activate(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public static void Deactivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    
        public static void SetSprite(this Image image, Sprite sprite, Color color)
        {
            image.sprite = sprite;
            image.color = color;
        }
    
        public static void Clear(this Image image)
        {
            image.SetSprite(null, Color.clear);
        }
    
        public static void Scale(this Transform transform, float amount)
        {
            transform.localScale = Vector3.one * amount;
        }
    }
}