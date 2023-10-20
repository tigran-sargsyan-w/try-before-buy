using UnityEngine;

namespace Screens
{
    [CreateAssetMenu(fileName = "ScreenInfoData", menuName = "ScreenDescription/ScreenInfoData", order = 1)]
    public class ScreenInfoData : ScriptableObject
    {
        [TextArea(10, 100)]
        public string descriptionText;

        public string emailAddress;
        public string phoneNumber;
        public string websiteURL;
    }
}