using System.Net.Http;
using System.Text.RegularExpressions;
using Glasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlassesButton : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image glassesSprite;
    
    private string pattern = @"(\d{1,4}[,\.]\d{2}\s*[€$¥₣£]|[€$¥₣£]\s*\d{1,4}[,\.]\d{2})";

    #endregion

    #region Methods

    public void SetProductData(GlassesData glassesData)
    {
        glassesSprite.sprite = glassesData.sprite;
        SetProductName(glassesData);
        SetProductPrice(glassesData);
    }

    private void SetProductName(GlassesData glassesData)
    {
        nameText.text = string.IsNullOrEmpty(glassesData.name) ? "Undefined" : glassesData.name;
    }

    private void SetProductPrice(GlassesData glassesData)
    {
        if (string.IsNullOrEmpty(glassesData.price))
        {
            GetProductPriceFromUrl(glassesData.url);
            glassesData.price = priceText.text;
        }
        else
        {
            priceText.text = glassesData.price;
        }
    }

    private void GetProductPriceFromUrl(string productUrl)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(productUrl).Result;
        string html = response.Content.ReadAsStringAsync().Result;
        
        Regex priceRegex = new Regex(pattern);
        Match match = priceRegex.Match(html);

        priceText.text = match.Success ? match.Groups[1].Value : "Undefined";
    }

    #endregion
}
