using Screens;
using UnityEngine;

namespace Glasses
{
    public class PlasticGlassesScroller : GlassesScroller
    {
        #region Fields

        [SerializeField] private GlassesCollection glassesCollection;
        [SerializeField] private GameObject glassesButtonPrefab;
        [SerializeField] private Transform glassesButtonsParent;
        
        private GlassesCollection cartGlassesCollection;

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            SetCartGlassesCollection();
            foreach (var glassesData in glassesCollection.glassesData)
            {
                var glassesButton = Instantiate(glassesButtonPrefab, glassesButtonsParent, true);
                var button = glassesButton.GetComponent<GlassesButton>();
                button.SetProductData(glassesData);
                button.InitCartButton(DetermineCartButtonState(glassesData));
                button.CartButtonClickedCallback += OnGlassesCartButtonClicked;
            }
        }

        #endregion

        #region Methods

        private CartButtonState DetermineCartButtonState(GlassesData glassesData)
        {
            return !cartGlassesCollection.glassesData.Contains(glassesData)
                ? CartButtonState.AddToCart
                : CartButtonState.RemoveFromCart;
        }

        private void SetCartGlassesCollection()
        {
            cartGlassesCollection = gameObject.GetComponentInParent<GlassesStylesScreen>().CartGlassesCollection;
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

        #endregion
    }
}