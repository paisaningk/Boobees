using System;
using System.IO;
using UnityEngine;

namespace Script.Menu
{
    public class ScreenCapture : MonoBehaviour
    {
        // Set your screenshot resolutions
        public int captureWidth = 1920;
        public int captureHeight = 1080;

        //configure with raw, jpg, png or ppm (simple raw format)
        public enum Format { RAW, JPG, PNG, PPM };
        public Format format = Format.JPG;
        //folder to write output(defaults to data patch)
        private string outputFolder;

        //private variables needed for screenshot
        private Rect rect;
        private RenderTexture renderTexture;
        private Texture2D screenShot;

        public bool isProcressing;

        // Start is called before the first frame update
        void Start()
        {
            outputFolder = Application.persistentDataPath + "/Screenshots/";
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
                Debug.Log("save path will be :" + outputFolder);
            }
        }

        private string CreateFileName(int width, int height)
        {
            //timestamp to append to the screenshot filename
            string timestamp = DateTime.Now.ToString("YYYMMMddTHHmmss");
            // use wideth, height and timestamp for unique file
            var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", outputFolder, width, height, timestamp, format.ToString().ToLower());

            //return filename
            return filename;
        }

        private void CaptureScreenShot()
        {
            isProcressing = true;

            //create screenshot object
            if (renderTexture == null)
            {
                //create ofscreen render texture to be rendered into
                rect = new Rect(0, 0, captureWidth, captureHeight);
                renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
                screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
            }

            //get main camera and render its outout into the off screen render texture create above
            Camera camera = Camera.main;
            camera.targetTexture = renderTexture;
            camera.Render();


            //mark the render texture as active and read the curren pixel data into Texture2D
            RenderTexture.active = renderTexture;
            screenShot.ReadPixels(rect, 0, 0);

            //reset the texture and remove the render texture from the camera since were done raeaing the screen data
            camera.targetTexture = null;
            RenderTexture.active = null;

            //get our filename
            string filename = CreateFileName((int)rect.width, (int)rect.height);

            //get file header
            byte[] fileHeader = null;
            byte[] fileData = null;

            //set the format abd encode based on it
            if (format == Format.RAW)
            {
                fileData = screenShot.GetRawTextureData();
            }

            else if (format == Format.PNG)
            {
                fileData = screenShot.EncodeToPNG();
            }

            else if (format == Format.JPG)
            {
                fileData = screenShot.EncodeToJPG();
            }

            else // for pmm files
            {
                //Create a file header ppm files
                string headerStr = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
                fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
                fileData = screenShot.GetRawTextureData();
            }

            // create new thread to offload the saving form the main thread
            new System.Threading.Thread(() =>
                {
                    var file = System.IO.File.Create(filename);
                    if (fileHeader != null)
                    {
                        file.Write(fileHeader, 0, fileHeader.Length);

                    }
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                    Debug.Log(string.Format("Screenshot Saved {0}, size {1}", filename, fileData.Length));

                    isProcressing = false;
                }
            ).Start();

            Destroy(renderTexture);
            renderTexture = null;
            screenShot = null;

        }

        public void TakeScreenShot()
        {
            if (!isProcressing)
            {
                CaptureScreenShot();
            }
            else
            {

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
