using System;
using Custom_Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.Common
{
    public class BottomMenuButton : MonoBehaviour
    {
        #region Events

        public event Action<int> ButtonClickedCallback;

        #endregion
        
        #region Fields
        
        [SerializeField][ReadOnly] 
        private Button button;
        [SerializeField][ReadOnly] 
        private int buttonIndex;
    
        private readonly Vector3 defaultScale = Vector3.one;
        private readonly float enlargeScale = 1.2f;

        #endregion

        #region Unity Lifecycle
        
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        #region Methods

        public void Setup()
        {
            SubscribeOnEvents();
        }
        
        public void SetButtonScale(bool isToEnlarge = true)
        {
            Vector3 scale = (isToEnlarge) ? defaultScale * enlargeScale : defaultScale;
            button.transform.localScale = scale;
        }

        #endregion
    
        #region Event Registry

        private void SubscribeOnEvents()
        {
            button.onClick.AddListener(() => ButtonClickedCallback?.Invoke(buttonIndex));
        }

        private void UnsubscribeFromEvents()
        {
            button.onClick.RemoveAllListeners();
            UnsubscribeFromEditorCallbacks();
        }

        #endregion
        
        #region Editor Methods

        private void OnValidate()
        {
            button = GetComponent<Button>();
            buttonIndex = transform.GetSiblingIndex();
        
            SubscribeToEditorCallbacks();
        }
        
        private void SubscribeToEditorCallbacks()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged += UpdateButtonIndex;
#endif
        }

        private void UnsubscribeFromEditorCallbacks()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged -= UpdateButtonIndex;
#endif
        }

        private void UpdateButtonIndex()
        {
#if UNITY_EDITOR
            if (this == null || transform == null) return;
            buttonIndex = transform.GetSiblingIndex();
#endif
        }

        #endregion
    }
}