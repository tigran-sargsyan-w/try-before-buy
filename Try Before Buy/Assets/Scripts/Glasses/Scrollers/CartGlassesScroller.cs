using System.Collections.Generic;
using Enums;
using Glasses.Common;
using Glasses.Data;
using Screens;
using UnityEngine;

namespace Glasses.Scrollers
{
    public class CartGlassesScroller : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GlassesCollection glassesCollection;
        [SerializeField] private GameObject glassesButtonPrefab;
        [SerializeField] private Transform glassesButtonsParent;

        private CartScreen cartScreen;
        private GlassesCollection cartGlassesCollection;
        private List<GameObject> instantiatedButtons = new List<GameObject>();

        #endregion

        #region Methods

        public void Setup()
        {
            SetCartGlassesCollection();
            foreach (var glassesData in glassesCollection.glassesData)
            {
                if (glassesData == null) return;
                var glassesButton = Instantiate(glassesButtonPrefab, glassesButtonsParent);
                var button = glassesButton.GetComponent<GlassesButton>();
                button.SetProductData(glassesData);
                button.InitCartButton(DetermineCartButtonState(glassesData));
                instantiatedButtons.Add(button.gameObject);
                button.CartButtonClickedCallback += OnGlassesCartButtonClicked;
            }
        }   
        
        public void DeleteAllButtons()
        {
            foreach (var button in instantiatedButtons)
            {
                Destroy(button.gameObject);
            }
            instantiatedButtons.Clear();
        }

        private void SetCartGlassesCollection()
        {
            cartScreen = gameObject.GetComponentInParent<CartScreen>();
            cartGlassesCollection = cartScreen.CartGlassesCollection;
        }

        private CartButtonState DetermineCartButtonState(GlassesData glassesData)
        {
            return !cartGlassesCollection.glassesData.Contains(glassesData)
                ? CartButtonState.AddToCart
                : CartButtonState.RemoveFromCart;
        }

        private void OnGlassesCartButtonClicked(GlassesData glassesData)
        {
            if (!cartGlassesCollection.glassesData.Contains(glassesData))
            {
                cartGlassesCollection.glassesData.Add(glassesData);
            }
            else
            {
                cartGlassesCollection.glassesData.Remove(glassesData);
                RemoveButtonByGlassesData(glassesData);
            }
            cartScreen.UpdateCartDisplayBasedOnEmptiness();
        }

        private void RemoveButtonByGlassesData(GlassesData glassesData)
        {
            instantiatedButtons.RemoveAll(button =>
            {
                var glassesButton = button.GetComponent<GlassesButton>();
                bool isDataIdentical = glassesButton.GetGlassesData() == glassesData;
                if (isDataIdentical)
                {
                    Destroy(button.gameObject);
                }
                return isDataIdentical;
            });
        }

        #endregion
    }
}