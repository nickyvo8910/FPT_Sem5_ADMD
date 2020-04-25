using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class CameraAccess : MonoBehaviour
{

    public GameObject buttonOk;
    public GameObject camera;
    public GameObject camera2;

	// Use this for initialization
    private WebCamTexture webCamTexture;

	void Start () {

        var deviceName = WebCamTexture.devices[0].name;

        webCamTexture = new WebCamTexture(deviceName, 720, 1280, 30);

        webCamTexture.Play();


        camera.GetComponent<UITexture>().mainTexture = webCamTexture;

        Debug.Log("init WcCamera");

        
	    EventDelegate.Add(buttonOk.GetComponent<UIButton>().onClick, CaptureImage);
	}

    private void CaptureImage()
    {
        Debug.Log("Stop WcCamera");
        
        Debug.Log("Save image");
        Texture2D texture2D = new Texture2D(webCamTexture.width, webCamTexture.height);

        texture2D.SetPixels(webCamTexture.GetPixels());
        texture2D.Apply();
        webCamTexture.Stop();
        File.WriteAllBytes(Application.persistentDataPath + "/imagedemo.png", texture2D.EncodeToPNG());

//        File.WriteAllBytes(Application.dataPath + "/image1.png",(gameObject.GetComponent<UITexture>().mainTexture as Texture2D).EncodeToPNG());
    }

    
}
