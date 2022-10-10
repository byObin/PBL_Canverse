using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyZoneScript : MonoBehaviour
{
    public GameObject darkPanel;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 공부구역에 들어옴");
            darkPanel.SetActive(true);

        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 공부구역에서 나감");
            darkPanel.SetActive(false);

        }
    }
}
