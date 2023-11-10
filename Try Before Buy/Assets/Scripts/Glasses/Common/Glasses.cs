using UnityEngine;

namespace Glasses.Common
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
        }
        
        public void SetLensColor(Color color)
        {
            lensMaterialCopy.color = color;
        }

        private void CopyMaterial()
        {
            frameMaterialCopy = new Material(defaultFrameMaterial);
            lensMaterialCopy = new Material(defaultLensMaterial);
            
            frame.GetComponent<MeshRenderer>().material = frameMaterialCopy;
            lens.GetComponent<MeshRenderer>().material = lensMaterialCopy;
        }

        #endregion
        
        #region Editor Methods

        private void OnValidate()
        {
            frame = gameObject;
            lens = frame.transform.GetChild(0).gameObject;
            defaultFrameMaterial = frame.GetComponent<MeshRenderer>().sharedMaterial;
            defaultLensMaterial = lens.GetComponent<MeshRenderer>().sharedMaterial;
        }

        #endregion
    }
}