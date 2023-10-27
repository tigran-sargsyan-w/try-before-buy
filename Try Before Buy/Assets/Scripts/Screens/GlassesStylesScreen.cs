using System.Collections.Generic;
using Glasses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class GlassesStylesScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private GlassesCollection cartGlassesCollection;
        [SerializeField] private TextMeshProUGUI glassesTypeHeader;
        [Header("Buttons")]
        [SerializeField] private Button metallicTypeButton;
        [SerializeField] private Button plasticTypeButton;
        [Header("Scrollers")]
        [SerializeField] private MetallicGlassesScroller metallicGlassesScroller;
        [SerializeField] private PlasticGlassesScroller plasticGlassesScroller;
        
        private Dictionary<GlassesType,GlassesScroller> scrollerDictionary = new Dictionary<GlassesType, GlassesScroller>();

        #endregion

        public GlassesCollection CartGlassesCollection
        {
            get => cartGlassesCollection; 
            set => cartGlassesCollection = value;
        }
        
        #region Unity Lifecycle

        private void Start()
        {
            SubscribeOnButtonsCallbacks();
            InitializeScrollerDictionary();
        }

        private void OnDestroy()
        {
            UnsubscribeFromButtonsCallbacks();
        }

        #endregion

        #region Methods

        private void SubscribeOnButtonsCallbacks()
        {
            metallicTypeButton.onClick.AddListener(OnMetallicTypeButtonClicked);
            plasticTypeButton.onClick.AddListener(OnPlasticTypeButtonClicked);
        }

        private void UnsubscribeFromButtonsCallbacks()
        {
            metallicTypeButton.onClick.RemoveAllListeners();
            plasticTypeButton.onClick.RemoveAllListeners();
        }
        
        private void InitializeScrollerDictionary()
        {
            scrollerDictionary[GlassesType.Metallic] = metallicGlassesScroller;
            scrollerDictionary[GlassesType.Plastic] = plasticGlassesScroller;
        }
        
        
        private void OnMetallicTypeButtonClicked()
        {
            glassesTypeHeader.gameObject.SetActive(false);
            ToggleScroller(GlassesType.Metallic);
        }

        private void OnPlasticTypeButtonClicked()
        {
            glassesTypeHeader.gameObject.SetActive(false);
            ToggleScroller(GlassesType.Plastic);
        }

        private void ToggleScroller(GlassesType type)
        {
            foreach (var scroller in scrollerDictionary)
            {
                if (scroller.Key == type)
                    scroller.Value.Show();
                else
                    scroller.Value.Hide();
            }
        }

        #endregion
    }
}