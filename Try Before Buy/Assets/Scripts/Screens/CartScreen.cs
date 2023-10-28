using System;
using System.Linq;
using Glasses;
using TMPro;
using UnityEngine;

namespace Screens
{
    public class CartScreen : ScreenBase
    {
        [SerializeField] private GlassesCollection cartGlassesCollection;
        [SerializeField] private CartGlassesScroller cartGlassesScroller;
        [SerializeField] private GameObject cartEmptinessObject;
        [SerializeField] private GameObject totalPriceObject;
        [SerializeField] private TextMeshProUGUI totalPriceText;
        
        public GlassesCollection CartGlassesCollection => cartGlassesCollection;

        private void OnEnable()
        {
            UpdateCartDisplayBasedOnEmptiness();
            cartGlassesScroller.Setup();
        }

        private void OnDisable()
        {
            cartGlassesScroller.DeleteAllButtons();
        }

        public void UpdateCartDisplayBasedOnEmptiness()
        {
            var isCartEmpty = cartGlassesCollection.glassesData.Count == 0;
            cartEmptinessObject.gameObject.SetActive(isCartEmpty);
            totalPriceObject.SetActive(!isCartEmpty);
            if (!isCartEmpty) CalculateAndSetTotalPrice();
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
    }
}