using System.Linq;
using Custom_Attributes;
using Screens.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class MenuController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BottomMenuButton[] bottomMenuButtons;
        [SerializeField] private ScreenController screenController;

        [SerializeField][ReadOnly] 
        private HorizontalLayoutGroup layoutGroup;

        #endregion

        #region Unity Lifecycle

        private void Awake()
        {
            SubscribeButtonsCallbacks();
            OnMenuButtonClicked(0);
            RefreshHorizontalLayout();
        }

        private void OnDestroy()
        {
            UnsubscribeButtonsCallbacks();
            UnsubscribeEditorCallbacks();
        }

        #endregion

        #region Methods
        

        public void OnMenuButtonClicked(int buttonIndex)
        {
            for (int i = 0; i < bottomMenuButtons.Length; i++)
            {
                bool isToEnlarge = (i == buttonIndex);
                bottomMenuButtons[i].SetButtonScale(isToEnlarge);
            }
            RefreshHorizontalLayout();
            screenController.ShowScreen(buttonIndex);
        }

        private void RefreshHorizontalLayout()
        {
            layoutGroup.SetLayoutHorizontal();
        }

        #endregion
    
        #region Event Registry

        private void SubscribeButtonsCallbacks()
        {
            foreach (var menuButton in bottomMenuButtons)
            {
                menuButton.Setup();
                menuButton.ButtonClickedCallback += OnMenuButtonClicked;
            }
        }

        private void UnsubscribeButtonsCallbacks()
        {
            foreach (var menuButton in bottomMenuButtons)
            {
                menuButton.ButtonClickedCallback -= OnMenuButtonClicked;
            }
        }

        #endregion
        
        #region Editor Methods

        private void OnValidate()
        {
            layoutGroup = GetComponent<HorizontalLayoutGroup>();
            SubscribeEditorCallbacks();
        }

        private void SubscribeEditorCallbacks()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged += UpdateButtonsBasedOnHierarchy;
#endif
        }

        private void UnsubscribeEditorCallbacks()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged -= UpdateButtonsBasedOnHierarchy;
#endif
        }
    
        private void UpdateButtonsBasedOnHierarchy()
        {
#if UNITY_EDITOR
            bottomMenuButtons = bottomMenuButtons.OrderBy(b =>
            {
                if (b == null || b.transform == null) return -1;
                return b.transform.GetSiblingIndex();
            }).ToArray();
#endif
        }

        #endregion
    }
}
