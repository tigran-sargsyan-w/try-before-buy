using System.Linq;
using Glasses;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Screens
{
    public class TryOnScreen : ScreenBase
    {
        #region Fields

        [SerializeField] private GlassesColorController glassesColorController;
        
        [SerializeField] private MenuController menuController;
        [SerializeField] private GlassesColorData metallicGlassesColorData;
        [SerializeField] private GlassesColorData plasticGlassesColorData;
        [SerializeField] private GameObject hintText;
        [SerializeField] private GameObject colorPicker;
        
        [SerializeField] private ARSessionOrigin arSessionOrigin;
        [SerializeField] private ARFaceManager arFaceManager;
        

        private GlassesData glassesData;
        private GameObject cachedFace;
        private GameObject cachedGlasses;
        private GlassesType glassesType;

        #endregion

        #region Unity Lifecycle

        private void OnEnable()
        {
            if (CheckIfHintNeeded()) return;
            glassesColorController.OnFrameColorChanged += OnFrameColorClicked;
            glassesColorController.OnLensColorChanged += OnLensColorClicked;
            
            arFaceManager.facesChanged += OnFacesChanged;
            OnSessionStart();
        }

        private void OnDisable()
        {
            glassesColorController.OnFrameColorChanged -= OnFrameColorClicked;
            glassesColorController.OnLensColorChanged -= OnLensColorClicked;
            
            arFaceManager.facesChanged -= OnFacesChanged;
            OnSessionEnd();
        }
        

        #endregion

        #region Methods

        public void GoToTryOnScreen(GlassesData glassesData)
        {
            this.glassesData = glassesData;
            glassesType = glassesData.glassesType;
            menuController.OnButtonClicked(2);
            OnSessionStart();
        }

        public GlassesColorData GetGlassesColorData()
        {
            switch (glassesType)
            {
                case GlassesType.Metallic:
                    return metallicGlassesColorData;
                case GlassesType.Plastic:
                    return plasticGlassesColorData;
                default:
                    return null;
            }
        }
        
        private void OnLensColorClicked(Color color)
        {
            if (cachedGlasses == null) return;
            var glasses = cachedGlasses.GetComponent<Glasses.Glasses>();
            glasses.SetLensColor(color);
        }

        private void OnFrameColorClicked(Color color)
        {
            if (cachedGlasses == null) return;
            var glasses = cachedGlasses.GetComponent<Glasses.Glasses>();
            glasses.SetFrameColor(color);
        }
        
        private bool CheckIfHintNeeded()
        {
            if (glassesData == null)
            {
                hintText.gameObject.SetActive(true);
                colorPicker.SetActive(false);
                return true;
            }

            hintText.gameObject.SetActive(false);
            colorPicker.SetActive(true);
            return false;
        }

        private void OnSessionStart()
        {
            glassesColorController.Activate();
            arSessionOrigin.enabled = true;
            arFaceManager.enabled = true;
        }

        private void OnSessionEnd()
        {
            Destroy(cachedGlasses);
            //destroy glasses material copies
            glassesColorController.Deactivate();
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