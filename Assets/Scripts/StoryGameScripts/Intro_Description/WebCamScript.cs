using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamScript : MonoBehaviour
{
    static WebCamTexture cam; // ����̽� ī�޶�

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


    // ������ ���( _savepath)�� PNG ������������ �����մϴ�.
    private string _SavePath = @"F:\Unity\Snaps\"; //��� �ٲټ���!
    int _CaptureCounter = 0; // ���ϸ��� ����

    public void TakeSnapshot()
    {
        Texture2D snap = new Texture2D(cam.width, cam.height);
        snap.SetPixels(cam.GetPixels());
        snap.Apply();

        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
    }
}
