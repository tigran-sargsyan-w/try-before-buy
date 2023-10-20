using System.Linq;
using CustomAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Fields

    [SerializeField] private BottomMenuButton[] bottomMenuButtons;
    [SerializeField] private ScreenController screenController;

    [SerializeField][ReadOnly] 
    private HorizontalLayoutGroup layoutGroup;

    #endregion

    #region Unity Lifecycle

    private void OnValidate()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        SubscribeToEditorCallbacks();
    }

    private void Awake()
    {
        SubscribeOnButtonsCallbacks();
        OnButtonClicked(2);
        RefreshHorizontalLayout();
    }

    private void OnDestroy()
    {
        UnsubscribeFromButtonsCallbacks();
        UnsubscribeFromEditorCallbacks();
    }

    #endregion

    #region Methods

    private void SubscribeOnButtonsCallbacks()
    {
        foreach (var menuButton in bottomMenuButtons)
        {
            menuButton.Setup();
            menuButton.ButtonClickedCallback += OnButtonClicked;
        }
    }

    private void UnsubscribeFromButtonsCallbacks()
    {
        foreach (var menuButton in bottomMenuButtons)
        {
            menuButton.ButtonClickedCallback -= OnButtonClicked;
        }
    }

    private void OnButtonClicked(int buttonIndex)
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
        bottomMenuButtons = bottomMenuButtons.OrderBy(b =>
        {
            if (b == null || b.transform == null) return -1;
            return b.transform.GetSiblingIndex();
        }).ToArray();
#endif
    }

    #endregion
}
