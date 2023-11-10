using Enums;
using Glasses.Common;
using Glasses.Data;
using Screens;
using UnityEngine;

namespace Glasses.Scrollers
{
    public class GlassesScroller : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TryOnScreen tryOnScreen;
        
        [SerializeField] private GlassesCollection glassesCollection;
        [SerializeField] private GameObject glassesButtonPrefab;
        [SerializeField] private Transform glassesButtonsParent;
        
        private GlassesCollection cartGlassesCollection;

        #endregion
        
        #region Methods
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected void Setup()
        {
            SetCartGlassesCollection();
            foreach (var glassesData in glassesCollection.glassesData)
            {
                var glassesButton = Instantiate(glassesButtonPrefab, glassesButtonsParent);
                var button = glassesButton.GetComponent<GlassesButton>();
                button.SetProductData(glassesData);
                button.InitCartButton(DetermineCartButtonState(glassesData));
                button.TryOnButtonClickedCallback += OnGlassesTryOnButtonClicked;
                button.CartButtonClickedCallback += OnGlassesCartButtonClicked;
            }
        }
        
        private CartButtonState DetermineCartButtonState(GlassesData glassesData)
        {
            return !cartGlassesCollection.glassesData.Contains(glassesData)
                ? CartButtonState.AddToCart
                : CartButtonState.RemoveFromCart;
        }
        
        private void OnGlassesTryOnButtonClicked(GlassesData data)
        {
            tryOnScreen.GoToTryOnScreen(data);
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
            }
        }

        private void SetCartGlassesCollection()
        {
            cartGlassesCollection = gameObject.GetComponentInParent<GlassesStylesScreen>().CartGlassesCollection;
        }

        #endregion
    }
}