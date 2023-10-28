using System.Linq;
using Glasses;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Screens
{
    public class TryOnScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private MenuController menuController;
        [SerializeField] private GameObject hintText;
        
        [SerializeField] private ARSessionOrigin arSessionOrigin;
        [SerializeField] private ARFaceManager arFaceManager;
        

        private GlassesData glassesData;
        
        private GameObject cachedFace;
        private GameObject cachedGlasses;

        #endregion

        #region Unity Lifecycle

        private void OnEnable()
        {
            if (CheckIfHintNeeded()) return;

            arFaceManager.facesChanged += OnFacesChanged;
            OnSessionStart();
        }

        private void OnDisable()
        {
            arFaceManager.facesChanged -= OnFacesChanged;
            OnSessionEnd();
        }

        #endregion

        #region Methods

        public void GoToTryOnScreen(GlassesData glassesData)
        {
            this.glassesData = glassesData;
            menuController.OnButtonClicked(2);
            OnSessionStart();
        }

        private bool CheckIfHintNeeded()
        {
            if (glassesData == null)
            {
                hintText.gameObject.SetActive(true);
                return true;
            }

            hintText.gameObject.SetActive(false);
            return false;
        }

        private void OnSessionStart()
        {
            arSessionOrigin.enabled = true;
            arFaceManager.enabled = true;
        }

        private void OnSessionEnd()
        {
            Destroy(cachedGlasses);
            arSessionOrigin.enabled = false;
            arFaceManager.enabled = false;
        }

        private void OnFacesChanged(ARFacesChangedEventArgs obj)
        {
            var objUpdated = obj.added;
            if (objUpdated == null || objUpdated.Count == 0) return;
            var arFace = objUpdated.Last();
            cachedFace = arFace.gameObject;// parent of face mesh
            var glasses = glassesData.glassesPrefab;
            cachedGlasses = Instantiate(glasses,cachedFace.transform);
        }

        #endregion
    }
}