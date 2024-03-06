using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class CameraDeviceController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RawImage rawImage;
        [SerializeField] private AspectRatioFitter fit;
    
        private WebCamTexture webcamTexture;
        private WebCamDevice[] webCamDevices;
        string frontCamName;
        string backCamName;

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            CheckAndInitCamera();
        }

        #endregion

        #region Methods

        private void CheckAndInitCamera()
        {
            if (WebCamTexture.devices.Length > 0)
            {
                webCamDevices = WebCamTexture.devices;
                foreach (var camDevice in webCamDevices)
                {
                    Debug.Log(camDevice.name);
                    if (!camDevice.isFrontFacing)
                    {
                        backCamName = camDevice.name;
                    }
                    else if (camDevice.isFrontFacing)
                    {
                        frontCamName = camDevice.name;
                    }
                }

                webcamTexture = new WebCamTexture(frontCamName, rawImage.mainTexture.width, rawImage.mainTexture.height);
                rawImage.texture = webcamTexture;
                webcamTexture.Play();
                float ratio = (float) webcamTexture.width / webcamTexture.height;
                fit.aspectRatio = ratio;
            }
            else
            {
                Debug.Log("Camera on device not found.");
            }
        }

        public void ChangeCameraToFront()
        {
            webcamTexture.Stop();
            webcamTexture = new WebCamTexture(frontCamName, Screen.width, Screen.height);
            rawImage.texture = webcamTexture;
            webcamTexture.Play();
        }
        
        public void ChangeCameraToBack()
        {
            webcamTexture.Stop();
            webcamTexture = new WebCamTexture(backCamName, Screen.width, Screen.height);
            rawImage.texture = webcamTexture;
            webcamTexture.Play();
        }

        #endregion
    }
}