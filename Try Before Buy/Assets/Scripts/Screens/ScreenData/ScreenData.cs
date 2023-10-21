using UnityEngine;

namespace Screens.ScreenData
{
    [CreateAssetMenu(fileName = "ScreenData", menuName = "ScreenData/BaseScreenData", order = 1)]
    public class ScreenData : ScriptableObject
    {
        [TextArea(10, 100)]
        public string descriptionText;
    }
}