using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using Enums;
using Glasses.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Glasses.Common
{
    public class GlassesButton : MonoBehaviour
    {
        #region Events

        public event Action<GlassesData> TryOnButtonClickedCallback;
        public event Action<GlassesData> CartButtonClickedCallback;

        #endregion
        
        #region Fields

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
        private readonly string pattern = @"(\d{1,4}[,\.]\d{2}\s*[€$¥₣£]|[€$¥₣£]\s*\d{1,4}[,\.]\d{2})";
        private readonly Color red = new Color(173/255f,63/255f,63/255f);
        private readonly Color green = new Color(63/255f,115/255f,63/255f);
        private CartButtonState cartButtonState;

        #endregion

        #region Unity Lifecycle

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        #region Methods

        public void SetProductData(GlassesData data)
        {
            glassesData = data;
            glassesSprite.sprite = data.sprite;
            SetProductName(data);
            SetProductPrice(data);
            SubscribeOnEvents(data);
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
        
        private void ToggleCartButtonState()
        {
            InitCartButton(cartButtonState == CartButtonState.AddToCart
                ? CartButtonState.RemoveFromCart
                : CartButtonState.AddToCart);
        }

        private void SetProductName(GlassesData data)
        {
            nameText.text = string.IsNullOrEmpty(data.name) ? "Undefined" : data.name;
        }

        private void SetProductPrice(GlassesData data)
        {
            if (string.IsNullOrEmpty(data.price))
            {
                GetProductPriceFromUrl(data.url);
                data.price = priceText.text;
            }
            else
            {
                priceText.text = data.price;
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
        
        #region Event Registry

        private void SubscribeOnEvents(GlassesData data)
        {
            tryOnButton.onClick.AddListener(() => OnTryOnButtonClicked(data));
            cartButton.onClick.AddListener(() => OnCartButtonClicked(data));
        }

        private void UnsubscribeFromEvents()
        {
            tryOnButton.onClick.RemoveAllListeners();
            cartButton.onClick.RemoveAllListeners();
            
            TryOnButtonClickedCallback = null;
            CartButtonClickedCallback = null;
        }

        private void OnTryOnButtonClicked(GlassesData data)
        {
            TryOnButtonClickedCallback?.Invoke(data);
        }

        private void OnCartButtonClicked(GlassesData data)
        {
            ToggleCartButtonState();
            CartButtonClickedCallback?.Invoke(data);
        }

        #endregion
    }
}