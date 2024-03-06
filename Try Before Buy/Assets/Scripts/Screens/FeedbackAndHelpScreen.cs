using System;
using Screens.Common;
using Screens.ScreenData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class FeedbackAndHelpScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private FeedbackAndHelpScreenData screenData;
        
        [Header("Feedback")]
        [SerializeField] private TMP_InputField feedbackInputField;
        [SerializeField] private Button sendFeedbackButton;
        [SerializeField] private Button clearFeedbackButton;
        
        [Header("Help")]
        [SerializeField] private TMP_InputField helpInputField;
        [SerializeField] private Button sendHelpButton;
        [SerializeField] private Button clearHelpButton;

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            SubscribeOnButtonsCallbacks();
        }

        private void OnDestroy()
        {
            UnsubscribeFromButtonsCallbacks();
        }

        #endregion

        #region Methods
        
        private void SendEmail(string recipientEmail, string emailSubject, string emailBody)
        {
            string emailUri = $"mailto:{recipientEmail}?" +
                              $"subject={Uri.EscapeDataString(emailSubject)}&" +
                              $"body={Uri.EscapeDataString(emailBody)}";
            Debug.Log(emailUri);
            Application.OpenURL(emailUri);
        }

        private void OnSendFeedbackButtonClicked()
        {
            var text = feedbackInputField.text;
            SendEmail(screenData.feedbackEmail, screenData.feedbackEmailSubject, text);
            Debug.Log($"<color=red>Feedback: {text}</color>");
        }

        private void OnClearFeedbackButtonClicked()
        {
            feedbackInputField.text = "";
        }

        private void OnSendHelpButtonClicked()
        {
            var text = helpInputField.text;
            SendEmail(screenData.helpEmail, screenData.helpEmailSubject, text);
            Debug.Log($"<color=red>Help: {text}</color>");
        }

        private void OnClearHelpButtonClicked()
        {                                        
            helpInputField.text = "";
        }

        #endregion
        
        #region Event Registry

        private void SubscribeOnButtonsCallbacks()
        {
            sendFeedbackButton.onClick.AddListener(OnSendFeedbackButtonClicked);
            clearFeedbackButton.onClick.AddListener(OnClearFeedbackButtonClicked);
            
            sendHelpButton.onClick.AddListener(OnSendHelpButtonClicked);
            clearHelpButton.onClick.AddListener(OnClearHelpButtonClicked);
        }

        private void UnsubscribeFromButtonsCallbacks()
        {
            sendFeedbackButton.onClick.RemoveAllListeners();
            clearFeedbackButton.onClick.RemoveAllListeners();
            
            sendHelpButton.onClick.RemoveAllListeners();
            clearHelpButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}