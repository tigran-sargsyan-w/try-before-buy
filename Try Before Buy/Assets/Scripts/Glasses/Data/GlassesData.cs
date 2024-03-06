using Custom_Attributes;
using Enums;
using UnityEngine;

namespace Glasses.Data
{
    [CreateAssetMenu(fileName = "GlassesData", menuName = "ProductData/GlassesData", order = 1)]
    public class GlassesData : ScriptableObject
    {
        [SpritePreview] public Sprite sprite;
        public GlassesType glassesType;
        public string url;
        public string name;
        public string description;
        public string price;
        public GameObject glassesPrefab;
    }
}