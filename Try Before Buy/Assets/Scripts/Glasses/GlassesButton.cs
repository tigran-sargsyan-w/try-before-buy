using System.Net.Http;
using System.Text.RegularExpressions;
using Glasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlassesButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image glassesSprite;
    
    private string url;
    private string pattern = @"(\d{1,4}[,\.]\d{2}\s*[€$¥₣£]|[€$¥₣£]\s*\d{1,4}[,\.]\d{2})";
    
    public void SetProductData(GlassesData glassesData)
    {
        url = glassesData.url;
        //nameText.text = glassesData.name;
        glassesSprite.sprite = glassesData.sprite;
        GetProductPrice(url);
    }
    
    [ContextMenu("Get Price")]
    public void GetProductPrice(string productUrl)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(productUrl).Result;
        string html = response.Content.ReadAsStringAsync().Result;
        
        Regex priceRegex = new Regex(pattern);
        Match match = priceRegex.Match(html);

        priceText.text = match.Success ? match.Groups[1].Value : "Undefined";
    }
}
