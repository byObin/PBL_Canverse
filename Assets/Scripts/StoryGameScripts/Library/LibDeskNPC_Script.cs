using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibDeskNPC_Script : MonoBehaviour
{
    public GameObject npcChatPanel;

    void OnMouseDown()
    {
        Debug.Log("Lib desk NPC is clicked");
        npcChatPanel.SetActive(true);
    }
}
