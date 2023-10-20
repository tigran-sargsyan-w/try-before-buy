using System.Linq;
using Screens;
using UnityEditor;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    #region Fields

    [SerializeField] private ScreenBase[] screens;

    #endregion
    
    #region Unity Lifecycle

    private void OnValidate()
    {
        SubscribeToEditorCallbacks();
    }

    private void Awake()
    {
        HideAllScreens();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEditorCallbacks();
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

    private void SubscribeToEditorCallbacks()
    {
#if UNITY_EDITOR
        EditorApplication.hierarchyChanged += UpdateArrayBasedOnButtonIndex;
#endif
    }

    private void UnsubscribeFromEditorCallbacks()
    {
#if UNITY_EDITOR
        EditorApplication.hierarchyChanged -= UpdateArrayBasedOnButtonIndex;
#endif
    }

    private void UpdateArrayBasedOnButtonIndex()
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
