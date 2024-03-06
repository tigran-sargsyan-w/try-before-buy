using System.Collections.Generic;
using Glasses.Data;
using UnityEngine;

namespace Glasses.Common
{
    [CreateAssetMenu(fileName = "GlassesCollection", menuName = "ProductData/GlassesCollection", order = 2)]
    public class GlassesCollection : ScriptableObject
    {
        public List<GlassesData> glassesData;
    }
}