using System;
using Glasses.Data;
using Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Glasses.Common
{
    public class GlassesColorController : MonoBehaviour
    {
        #region Events

        public event Action<Color> OnFrameColorChanged;
        public event Action<Color> OnLensColorChanged;

        #endregion
        
        #region Fields

        [SerializeField] private TryOnScreen tryOnScreen;
        
        [Header("Frame")]
        [SerializeField] private Button frameButton;
        [SerializeField] private Button frame2Button;
        [SerializeField] private Button frame3Button;
        
        [Header("Lens")]
        [SerializeField] private Button lensButton;
        [SerializeField] private Button lens2Button;
        [SerializeField] private Button lens3Button;
        
        private GlassesColorData colorData;

        #endregion

        #region Methods

        public void Activate()
        {
            colorData = tryOnScreen.GetGlassesColorData();
            InitButtonsColor();
            SubscribeButtonsCallbacks();
        }

        public void Deactivate()
        {
            UnsubscribeButtonsCallbacks();
        }

        private void InitButtonsColor()
        {
            SetButtonColor(frameButton, colorData.frame1Color);
            SetButtonColor(frame2Button, colorData.frame2Color);
            SetButtonColor(frame3Button, colorData.frame3Color);

            SetButtonColor(lensButton, colorData.lens1Color);
            SetButtonColor(lens2Button, colorData.lens2Color);
            SetButtonColor(lens3Button, colorData.lens3Color);
        }

        private void SetButtonColor(Button button, Color color)
        {
            var buttonImage = button.GetComponent<Image>();
            Color newColor = new Color(color.r, color.g, color.b, 1f);
            buttonImage.color = newColor;
        }

        #endregion

        #region Event Registry
        
        private void SubscribeButtonsCallbacks()
        {
            frameButton.onClick.AddListener(()=>OnFrameButtonClicked(colorData.frame1Color));
            frame2Button.onClick.AddListener(()=>OnFrameButtonClicked(colorData.frame2Color));
            frame3Button.onClick.AddListener(()=>OnFrameButtonClicked(colorData.frame3Color));
            
            lensButton.onClick.AddListener(()=>OnLensButtonClicked(colorData.lens1Color));
            lens2Button.onClick.AddListener(()=>OnLensButtonClicked(colorData.lens2Color));
            lens3Button.onClick.AddListener(()=>OnLensButtonClicked(colorData.lens3Color));
        }

        private void UnsubscribeButtonsCallbacks()
        {
            frameButton.onClick.RemoveAllListeners();
            frame2Button.onClick.RemoveAllListeners();
            frame3Button.onClick.RemoveAllListeners();
            
            lensButton.onClick.RemoveAllListeners();
            lens2Button.onClick.RemoveAllListeners();
            lens3Button.onClick.RemoveAllListeners();
        }
        
        private void OnFrameButtonClicked(Color color)
        {
            OnFrameColorChanged?.Invoke(color);
        }

        private void OnLensButtonClicked(Color color)
        {
            OnLensColorChanged?.Invoke(color);
        }

        #endregion
    }
}