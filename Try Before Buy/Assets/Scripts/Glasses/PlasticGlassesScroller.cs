using UnityEngine;

namespace Glasses
{
    public class PlasticGlassesScroller : MonoBehaviour
    {
        public GlassesCollection glassesCollection;
        public GameObject glassesButtonPrefab;
        public Transform glassesButtonsParent;
        
        private void Start()
        {
            foreach (var glassesData in glassesCollection.glassesData)
            {
                var glassesButton = Instantiate(glassesButtonPrefab, glassesButtonsParent, true);
                var button = glassesButton.GetComponent<GlassesButton>();
                button.SetProductData(glassesData);
            }
            Debug.Log("PlasticGlassesScroller done");
        }
    }
}