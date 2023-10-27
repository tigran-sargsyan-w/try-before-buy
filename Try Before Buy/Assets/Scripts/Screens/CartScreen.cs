using System;
using Glasses;
using TMPro;
using UnityEngine;

namespace Screens
{
    public class CartScreen : ScreenBase
    {
        [SerializeField] private TextMeshProUGUI cartEmptinessText;
        [SerializeField] private GlassesCollection cartGlassesCollection;
        [SerializeField] private CartGlassesScroller cartGlassesScroller;
        
        public GlassesCollection CartGlassesCollection
        {
            get => cartGlassesCollection; 
            set => cartGlassesCollection = value;
        }
        
        private void Start()
        {
            SetCartEmptinessTextVisibility();
            //cartGlassesScroller.Setup();
        }

        private void OnEnable()
        {
            cartGlassesScroller.Setup();
        }

        private void OnDisable()
        {
            cartGlassesScroller.DeleteAllButtons();
        }

        private void SetCartEmptinessTextVisibility()
        {
            cartEmptinessText.gameObject.SetActive(cartGlassesCollection.glassesData.Count == 0);
        }
    }
}