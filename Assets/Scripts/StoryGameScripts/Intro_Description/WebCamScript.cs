using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamScript : MonoBehaviour
{
    static WebCamTexture cam; // 디바이스 카메라

    // Use this for initialization
    void Start()
    {

        //transform.localScale = new Vector3((float)Screen.width, (float)Screen.height, 1);


        if (cam == null)
            cam = new WebCamTexture();

        GetComponent<MeshRenderer>().material.mainTexture = cam;

        if (!cam.isPlaying)
            cam.Play();
    }


    // 지정한 경로( _savepath)에 PNG 파일형식으로 저장합니다.
    private string _SavePath = @"F:\Unity\Snaps\"; //경로 바꾸세요!
    int _CaptureCounter = 0; // 파일명을 위한

    public void TakeSnapshot()
    {
        Texture2D snap = new Texture2D(cam.width, cam.height);
        snap.SetPixels(cam.GetPixels());
        snap.Apply();

        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
    }
}
