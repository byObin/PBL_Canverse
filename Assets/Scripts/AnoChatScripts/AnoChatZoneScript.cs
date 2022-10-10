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
            Debug.Log("플레이어가 익명채팅구역에 들어옴");
            darkPanel.SetActive(true);

            ChattingUI.SetActive(false);    //전체채팅 ui 끔
            AnoChattingUI.SetActive(true);    //익명채팅 ui 켬
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 익명채팅구역에서 나감");
            darkPanel.SetActive(false);

            ChattingUI.SetActive(true);    //전체채팅 ui 켬
            AnoChattingUI.SetActive(false);    //익명채팅 ui 끔
        }
    }

}
