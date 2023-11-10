using System.Collections.Generic;
using UnityEngine;

namespace Glasses
{
    [CreateAssetMenu(fileName = "GlassesCollection", menuName = "ProductData/GlassesCollection", order = 2)]
    public class GlassesCollection : ScriptableObject
    {
        public List<GlassesData> glassesData;
    }
}