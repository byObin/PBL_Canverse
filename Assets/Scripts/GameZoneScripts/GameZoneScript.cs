using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameZoneScript : MonoBehaviour
{
    public GameObject darkPanel;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾ ���ӱ����� ����");
            darkPanel.SetActive(true);

        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾ ���ӱ������� ����");
            darkPanel.SetActive(false);

        }
    }
}
