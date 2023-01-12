using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderTreeNPC : MonoBehaviour
{
    public GameObject npcChatPanel;

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("npc is clicked");
            npcChatPanel.SetActive(true);
            
        }
    }*/

    void OnMouseDown()
    {
        Debug.Log("npc is clicked");
        npcChatPanel.SetActive(true);
    }
}
