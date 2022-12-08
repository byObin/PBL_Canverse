using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerInfo : MonoBehaviour
{
    //카메라 지정
    public Camera getCamera;

    public GameObject reportUserPanel;

    void Start()
    {
        reportUserPanel.SetActive(false);
    }

    void Update()
    {
        //마우스 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = getCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null)
            {
                GameObject obj = hit.transform.gameObject;
                
                //다른 사용자를 클릭할 경우 신고 창 띄우기
                if (obj.CompareTag("Player"))    
                {
                    Debug.Log("다른 사용자 클릭");
                    reportUserPanel.SetActive(true);
                }
            }
           
        }
    }
}
