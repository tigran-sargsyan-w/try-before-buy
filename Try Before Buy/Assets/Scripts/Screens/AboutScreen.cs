using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Screens
{
    public class AboutScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private ScreenInfoData screenInfoData;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        private Dictionary<string, string> replacements;
        private string inputText;

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            InitializeDataAndText();
        }

        #endregion

        #region Methods

        private void InitializeDataAndText()
        {
            inputText = screenInfoData.descriptionText;
            replacements = new Dictionary<string, string>
            {
                {"Company Name", $"{Application.companyName}"},
                {"App Name", $"{Application.productName}"},
                {"Version Number", $"{Application.version}"},
                {"Email Address", $"{screenInfoData.emailAddress}"},
                {"Phone Number", $"{screenInfoData.phoneNumber}"},
                {"Links to Social Media", $"{screenInfoData.websiteURL}"}
            };
            
            textMeshProUGUI.text = Regex.Replace(inputText, @"\[(.*?)\]", ReplaceMatch);
        }

        private string ReplaceMatch(Match match)
        {
            string matchedValue = match.Groups[1].Value;
            if (replacements.TryGetValue(matchedValue, out string replacementText))
            {
                return replacementText;
            }
            return "Not Found";
        }

        #endregion
    }
}