using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Glasses
{
    public class GlassesButton : MonoBehaviour
    {
        #region Fields
        public event Action<GlassesData> TryOnButtonClickedCallback;
        public event Action<GlassesData> CartButtonClickedCallback;
        
        
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image glassesSprite;
    
        [SerializeField] private Button tryOnButton;
        [SerializeField] private Button cartButton;
        [SerializeField] private Image cartButtonImage;
        [SerializeField] private Image cartButtonImageBg;
        [SerializeField] private Sprite addToCartSprite;
        [SerializeField] private Sprite removeFromCartSprite;
        
        private GlassesData glassesData;
        private string pattern = @"(\d{1,4}[,\.]\d{2}\s*[€$¥₣£]|[€$¥₣£]\s*\d{1,4}[,\.]\d{2})";
        private Color red = new Color(173/255f,63/255f,63/255f);
        private Color green = new Color(63/255f,115/255f,63/255f);
        private CartButtonState cartButtonState;

        #endregion

        #region Unity Lifecycle

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        #region Methods

        public void SetProductData(GlassesData glassesData)
        {
            this.glassesData = glassesData;
            glassesSprite.sprite = glassesData.sprite;
            SetProductName(glassesData);
            SetProductPrice(glassesData);
            SubscribeOnEvents(glassesData);
        }

        public GlassesData GetGlassesData()
        {
            return glassesData;
        }

        public void InitCartButton(CartButtonState state)
        {
            switch (state)
            {
                case CartButtonState.AddToCart:
                    cartButtonState = CartButtonState.AddToCart;
                    cartButtonImage.sprite = addToCartSprite;
                    cartButtonImageBg.color = green;
                    break;
                case CartButtonState.RemoveFromCart:
                    cartButtonState = CartButtonState.RemoveFromCart;
                    cartButtonImage.sprite = removeFromCartSprite;
                    cartButtonImageBg.color = red;
                    break;
            }
        }

        private void SubscribeOnEvents(GlassesData glassesData)
        {
            tryOnButton.onClick.AddListener(() => OnTryOnButtonClicked(glassesData));
            cartButton.onClick.AddListener(() => OnCartButtonClicked(glassesData));
        }

        private void UnsubscribeFromEvents()
        {
            tryOnButton.onClick.RemoveAllListeners();
            cartButton.onClick.RemoveAllListeners();
        }

        private void OnTryOnButtonClicked(GlassesData glassesData)
        {
            TryOnButtonClickedCallback?.Invoke(glassesData);
        }

        private void OnCartButtonClicked(GlassesData glassesData)
        {
            ToggleCartButtonState();
            CartButtonClickedCallback?.Invoke(glassesData);
        }
        
        private void ToggleCartButtonState()
        {
            InitCartButton(cartButtonState == CartButtonState.AddToCart
                ? CartButtonState.RemoveFromCart
                : CartButtonState.AddToCart);
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
}