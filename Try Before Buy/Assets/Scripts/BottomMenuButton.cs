using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenuButton : MonoBehaviour
{
    #region Fields

    public event Action<int> ButtonClickedCallback;

    private Button button;
    private int buttonIndex;
    private readonly Vector3 defaultScale = Vector3.one;
    private readonly float enlargeScale = 1.2f;

    #endregion

    #region Unity Lifecycle

    private void OnValidate()
    {
        button = GetComponent<Button>();
        buttonIndex = transform.GetSiblingIndex();
        
        EditorApplication.hierarchyChanged += UpdateButtonIndex;
    }

    private void Start()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #endregion

    #region Methods

    public void SetButtonScale(bool isToEnlarge = true)
    {
        Vector3 scale = (isToEnlarge) ? defaultScale * enlargeScale : defaultScale;
        button.transform.localScale = scale;
    }

    private void UpdateButtonIndex()
    {
        if (this == null || transform == null) return;
        buttonIndex = transform.GetSiblingIndex();
    }

    private void SubscribeEvents()
    {
        button.onClick.AddListener(() => ButtonClickedCallback?.Invoke(buttonIndex));
    }

    private void UnsubscribeEvents()
    {
        button.onClick.RemoveAllListeners();
        EditorApplication.hierarchyChanged -= UpdateButtonIndex;
    }

    #endregion
}