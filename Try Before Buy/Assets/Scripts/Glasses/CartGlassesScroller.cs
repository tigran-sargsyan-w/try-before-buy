using System.Collections.Generic;
using Screens;
using UnityEngine;

namespace Glasses
{
    public class CartGlassesScroller : GlassesScroller
    {
        [SerializeField] private GlassesCollection glassesCollection;
        [SerializeField] private GameObject glassesButtonPrefab;
        [SerializeField] private Transform glassesButtonsParent;

        private GlassesCollection cartGlassesCollection;
        private List<GameObject> instantiatedButtons = new List<GameObject>();

        public void Setup()
        {
            SetCartGlassesCollection();
            foreach (var glassesData in glassesCollection.glassesData)
            {
                if (glassesData == null) return;
                var glassesButton = Instantiate(glassesButtonPrefab, glassesButtonsParent, true);
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
                //destroy button
                RemoveButtonByGlassesData(glassesData);
            }
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
        
        private void SetCartGlassesCollection()
        {
            cartGlassesCollection = gameObject.GetComponentInParent<CartScreen>().CartGlassesCollection;
        }
    }
}