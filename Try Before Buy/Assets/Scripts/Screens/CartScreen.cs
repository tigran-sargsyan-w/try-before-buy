using System;
using System.Linq;
using Glasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class CartScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private GlassesCollection cartGlassesCollection;
        [SerializeField] private CartGlassesScroller cartGlassesScroller;
        [SerializeField] private GameObject cartEmptinessObject;
        [SerializeField] private GameObject totalPriceObject;
        [SerializeField] private TextMeshProUGUI totalPriceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private string amazonCartUrl = "https://www.amazon.com/gp/cart/view.html";

        #endregion

        #region Properties

        public GlassesCollection CartGlassesCollection => cartGlassesCollection;

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void OnEnable()
        {
            UpdateCartDisplayBasedOnEmptiness();
            cartGlassesScroller.Setup();
        }

        private void OnDisable()
        {
            cartGlassesScroller.DeleteAllButtons();
        }

        #endregion

        #region Methods

        public void UpdateCartDisplayBasedOnEmptiness()
        {
            var isCartEmpty = cartGlassesCollection.glassesData.Count == 0;
            cartEmptinessObject.gameObject.SetActive(isCartEmpty);
            totalPriceObject.SetActive(!isCartEmpty);
            buyButton.gameObject.SetActive(!isCartEmpty);
            if (!isCartEmpty) CalculateAndSetTotalPrice();
        }

        private void SubscribeToEvents()
        {
            buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void UnsubscribeFromEvents()
        {
            buyButton.onClick.RemoveAllListeners();
        }

        private void OnBuyButtonClicked()
        {
            Application.OpenURL(amazonCartUrl);
        }

        private void CalculateAndSetTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var glassesData in cartGlassesCollection.glassesData)
            {
                string priceWithoutCurrency = new string(glassesData.price
                    .Where(c => Char.IsDigit(c) || c == '.' || c == ',')
                    .ToArray());
                if (decimal.TryParse(priceWithoutCurrency, out decimal parsedPrice))
                {
                    totalPrice += parsedPrice;
                }
                else
                {
                    Debug.Log("Invalid price format: " + totalPrice);
                }

                totalPriceText.text = $"{totalPrice}€";
            }
        }

        #endregion
    }
}