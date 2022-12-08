using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerInfo : MonoBehaviour
{
    //ī�޶� ����
    public Camera getCamera;

    public GameObject reportUserPanel;

    void Start()
    {
        reportUserPanel.SetActive(false);
    }

    void Update()
    {
        //���콺 Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = getCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null)
            {
                GameObject obj = hit.transform.gameObject;
                
                //�ٸ� ����ڸ� Ŭ���� ��� �Ű� â ����
                if (obj.CompareTag("Player"))    
                {
                    Debug.Log("�ٸ� ����� Ŭ��");
                    reportUserPanel.SetActive(true);
                }
            }
           
        }
    }
}
