using System.Linq;
using Screens.Common;
using UnityEditor;
using UnityEngine;

namespace Common
{
    public class ScreenController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private ScreenBase[] screens;

        #endregion
    
        #region Unity Lifecycle
        
        private void Awake()
        {
            HideAllScreens();
        }

        private void OnDestroy()
        {
            UnsubscribeEditorCallbacks();
        }

        #endregion

        #region Methods

        public void ShowScreen(int index)
        {
            for (int i = 0; i < screens.Length; i++)
            {
                screens[i].gameObject.SetActive(i == index);
            }
        }

        private void HideAllScreens()
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Editor Methods

        private void OnValidate()
        {
            SubscribeEditorCallbacks();
        }
        
        private void SubscribeEditorCallbacks()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged += UpdateArrayBasedOnHierarchy;
#endif
        }

        private void UnsubscribeEditorCallbacks()
        {
#if UNITY_EDITOR
            EditorApplication.hierarchyChanged -= UpdateArrayBasedOnHierarchy;
#endif
        }

        private void UpdateArrayBasedOnHierarchy()
        {
#if UNITY_EDITOR
            screens = screens.OrderBy(s =>
            {
                if (s == null || s.transform == null) return -1;
                return s.transform.GetSiblingIndex();
            }).ToArray();
#endif
        }

        #endregion
    }
}
