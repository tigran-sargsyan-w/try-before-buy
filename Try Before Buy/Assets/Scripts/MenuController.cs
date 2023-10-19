using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Fields

    public BottomMenuButton[] bottomMenuButtons;

    private HorizontalLayoutGroup layoutGroup;

    #endregion

    #region Unity Lifecycle

    private void OnValidate()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        EditorApplication.hierarchyChanged += UpdateArrayBasedOnButtonIndex;
    }

    private void Start()
    {
        SubscribeOnButtonsEvent();
        bottomMenuButtons[2].SetButtonScale();
        RefreshHorizontalLayout();
    }

    private void OnDestroy()
    {
        UnsubscribeOnButtonsEvent();
        EditorApplication.hierarchyChanged -= UpdateArrayBasedOnButtonIndex;
    }

    #endregion

    #region Methods

    private void UpdateArrayBasedOnButtonIndex()
    {
        bottomMenuButtons = bottomMenuButtons.OrderBy(b =>
        {
            if (b == null || b.transform == null) return -1;
            return b.transform.GetSiblingIndex();
        }).ToArray();
    }

    private void SubscribeOnButtonsEvent()
    {
        foreach (var menuButton in bottomMenuButtons)
        {
            menuButton.ButtonClickedCallback += OnButtonClicked;
        }
    }
    
    private void UnsubscribeOnButtonsEvent()
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
    }

    private void RefreshHorizontalLayout()
    {
        layoutGroup.SetLayoutHorizontal();
    }

    #endregion
}
