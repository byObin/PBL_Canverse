using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMapScript : MonoBehaviour
{
    public GameObject mapPanel;

    void Update()
    {
        //mapItem.onClick.AddListener(OpenMap);
        if (GameObject.Find("Player").GetComponent<Sample_Player_Move>().mapIsClicked == true)
        {
            mapPanel.SetActive(true);
            GameObject.Find("Player").GetComponent<Sample_Player_Move>().mapIsClicked = false;
        }

    }

    /*void OpenMap()
    {
        mapPanel.SetActive(true);

    }*/
   
}
