using CustomAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class LinkOpener : MonoBehaviour, IPointerClickHandler 
    {
        [SerializeField][ReadOnly]
        private TMP_Text textMeshPro;
        private void OnValidate()
        {
            textMeshPro = GetComponent<TMP_Text>();
        }

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
    }
}