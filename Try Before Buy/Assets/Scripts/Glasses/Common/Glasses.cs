using UnityEngine;

namespace Glasses
{
    public class Glasses : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject frame;
        [SerializeField] private GameObject lens;
        [SerializeField] private Material defaultFrameMaterial;
        [SerializeField] private Material defaultLensMaterial;
        
        private Material frameMaterialCopy;
        private Material lensMaterialCopy;

        #endregion

        #region Unity Lifecycle

        private void OnValidate()
        {
            frame = gameObject;
            lens = frame.transform.GetChild(0).gameObject;
            defaultFrameMaterial = frame.GetComponent<MeshRenderer>().sharedMaterial;
            defaultLensMaterial = lens.GetComponent<MeshRenderer>().sharedMaterial;
        }

        private void Start()
        {
            CopyMaterial();
        }

        private void OnDestroy()
        {
            Destroy(frameMaterialCopy);
            Destroy(lensMaterialCopy);
        }

        #endregion

        #region Methods

        public void SetFrameColor(Color color)
        {
            frameMaterialCopy.color = color;
            Debug.Log("Set Frame Color Done with color: " + color);
        }
        
        public void SetLensColor(Color color)
        {
            lensMaterialCopy.color = color;
            Debug.Log("Set Lens Color Done with color: " + color);
        }

        private void CopyMaterial()
        {
            frameMaterialCopy = new Material(defaultFrameMaterial);
            lensMaterialCopy = new Material(defaultLensMaterial);
            
            frame.GetComponent<MeshRenderer>().material = frameMaterialCopy;
            lens.GetComponent<MeshRenderer>().material = lensMaterialCopy;
            Debug.Log("CopyMaterial Done");
        }

        #endregion
    }
}