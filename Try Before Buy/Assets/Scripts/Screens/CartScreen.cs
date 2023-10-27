using Glasses;
using TMPro;

namespace Screens
{
    public class CartScreen : ScreenBase
    {
        public GlassesCollection glassesCollection;
        public TextMeshProUGUI cartEmptinessText;
        
        private void Start()
        {
            SetCartEmptinessTextVisibility();
        }

        private void SetCartEmptinessTextVisibility()
        {
            cartEmptinessText.gameObject.SetActive(glassesCollection.glassesData.Count == 0);
        }
    }
}