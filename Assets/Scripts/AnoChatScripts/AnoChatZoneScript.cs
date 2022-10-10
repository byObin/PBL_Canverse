using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnoChatZoneScript : MonoBehaviour
{
    public GameObject darkPanel;
    public GameObject ChattingUI;
    public GameObject AnoChattingUI;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾ �͸�ä�ñ����� ����");
            darkPanel.SetActive(true);

            ChattingUI.SetActive(false);    //��üä�� ui ��
            AnoChattingUI.SetActive(true);    //�͸�ä�� ui ��
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾ �͸�ä�ñ������� ����");
            darkPanel.SetActive(false);

            ChattingUI.SetActive(true);    //��üä�� ui ��
            AnoChattingUI.SetActive(false);    //�͸�ä�� ui ��
        }
    }

}
