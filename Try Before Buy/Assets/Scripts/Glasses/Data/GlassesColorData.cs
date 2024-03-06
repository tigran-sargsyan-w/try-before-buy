using UnityEngine;

namespace Glasses.Data
{
    [CreateAssetMenu(fileName = "GlassesColorData", menuName = "GlassesColorData", order = 0)]
    public class GlassesColorData : ScriptableObject
    {
        [Header("Frames Colors")]
        public Color frame1Color;
        public Color frame2Color;
        public Color frame3Color;
        [Header("Lens Colors")]
        public Color lens1Color;
        public Color lens2Color;
        public Color lens3Color;
    }
}