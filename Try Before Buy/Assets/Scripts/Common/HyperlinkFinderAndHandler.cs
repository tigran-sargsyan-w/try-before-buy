using Custom_Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class HyperlinkFinderAndHandler : MonoBehaviour, IPointerClickHandler 
    {
        #region Fields

        [SerializeField][ReadOnly]
        private TMP_Text textMeshPro;

        #endregion

        #region Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenURLOnLinkClick(eventData);
        }

        private void OpenURLOnLinkClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, eventData.position, null);
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }

        #endregion
        
        #region Editor Methods

        private void OnValidate()
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

        #endregion
    }
}