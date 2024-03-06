using UnityEngine;

namespace Screens.ScreenData
{
    [CreateAssetMenu(fileName = "ScreenData", menuName = "ScreenData/FeedbackAndHelpScreenData", order = 1)]
    public class FeedbackAndHelpScreenData : ScreenData
    {
        [Header("Feedback")]
        public string feedbackEmail;
        public string feedbackEmailSubject;

        [Header("Help")]
        public string helpEmail;
        public string helpEmailSubject;
    }
}