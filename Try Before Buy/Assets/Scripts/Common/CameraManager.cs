using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CameraManager : MonoBehaviour
    {
        public RawImage rawImage;
        public AspectRatioFitter fit;
    
        private WebCamTexture webcamTexture;
        private WebCamDevice[] webCamDevices;
        string frontCamName = null;
        string backCamName = null;
    
        private void Start()
        {
            if (WebCamTexture.devices.Length > 0)
            {
                
                webCamDevices = WebCamTexture.devices;
                foreach(var camDevice in webCamDevices){ 
                    Debug.Log(camDevice.name);
                    if (!camDevice.isFrontFacing)
                    {
                        backCamName = camDevice.name;
                    }
                    else if(camDevice.isFrontFacing){
                        frontCamName = camDevice.name;
                    }
                }
                webcamTexture = new WebCamTexture(frontCamName, rawImage.mainTexture.width, rawImage.mainTexture.height);

                rawImage.texture = webcamTexture;

                // Запускаем камеру.
                webcamTexture.Play();
                
                float ratio = (float)webcamTexture.width / (float)webcamTexture.height;
                fit.aspectRatio = ratio;
            }
            else
            {
                Debug.Log("Камера не найдена на устройстве.");
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

        
    }
}