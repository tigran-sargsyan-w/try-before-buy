using System.Collections.Generic;
using System.Text.RegularExpressions;
using Screens.Common;
using Screens.ScreenData;
using TMPro;
using UnityEngine;

namespace Screens
{
    public class AboutScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private AboutScreenData screenData;
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
            inputText = screenData.descriptionText;
            replacements = new Dictionary<string, string>
            {
                {"Company Name", $"{Application.companyName}"},
                {"App Name", $"{Application.productName}"},
                {"Version Number", $"{Application.version}"},
                {"Email Address", $"{screenData.emailAddress}"},
                {"Phone Number", $"{screenData.phoneNumber}"},
                {"Links to Social Media", $"{GetCleanedURLWithLink(screenData.websiteURL)}"}
            };
            
            textMeshProUGUI.text = Regex.Replace(inputText, @"\[(.*?)\]", ReplaceMatch);
        }

        private string ReplaceMatch(Match match)
        {
            string matchedValue = match.Groups[1].Value;
            var tryGetMatch = replacements.TryGetValue(matchedValue, out string replacementText);
            return tryGetMatch ? replacementText : "Not Found";
        }

        private string GetCleanedURLWithLink(string originalURL)
        {
            string pattern = @"^(https?://(www\.)?)?([^/]+)";
            Match match = Regex.Match(originalURL, pattern);
            var cleanedURL = match.Success ? match.Groups[3].Value : originalURL;
            var result = $"<link={originalURL}><u>{cleanedURL}</u></link>.";
            return result;
        }
        
        #endregion
    }
}