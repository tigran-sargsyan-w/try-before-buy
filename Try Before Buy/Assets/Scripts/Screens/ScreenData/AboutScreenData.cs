using UnityEngine;

namespace Screens.ScreenData
{
    [CreateAssetMenu(fileName = "ScreenData", menuName = "ScreenData/AboutScreenData", order = 1)]
    public class AboutScreenData : ScreenData
    {
        public string emailAddress;
        public string phoneNumber;
        public string websiteURL;
    }
}