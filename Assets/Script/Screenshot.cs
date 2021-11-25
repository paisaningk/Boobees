using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Screenshot : MonoBehaviour
    {
        [SerializeField] private Image adc;
        [SerializeField] private GameObject ui;
        public void CaptureScreenshot()
        {
            var texture2D = new Texture2D(Screen.width / 2, Screen.height , TextureFormat.RGB24,false);
            texture2D.ReadPixels(new Rect(Screen.width /4,0,Screen.width/2 ,Screen.height),0,0 );
            texture2D.Apply();
            var screenshotName = 
                "Screenshot_" + 
                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + 
                ".png";
            NativeGallery.SaveImageToGallery(texture2D, "boobeegame", screenshotName);
        }

        public void eeee()
        {
            ui.SetActive(false);
            string folderPath = Directory.GetCurrentDirectory() + "/Screenshot/";

 
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);
 
            var screenshotName = 
                "Screenshot_" + 
                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + 
                ".png";
            ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName));
            Debug.Log(folderPath + screenshotName);
            ui.SetActive(true);
        }
    }
}
