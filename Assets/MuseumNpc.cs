using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumNpc : MonoBehaviour
{
    public GameObject npcChatPanel;


    void OnMouseDown()
    {
        Debug.Log("Musuem npc is clicked!");
        npcChatPanel.SetActive(true);
    }
}
