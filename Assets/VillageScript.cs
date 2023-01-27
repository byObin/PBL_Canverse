using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageScript : MonoBehaviour
{
    public GameObject PanelFirstMsg;
    public GameObject pdPanel;
    public GameObject npcChat;
    public GameObject pdBtn;

    void Start()
    {
        PanelFirstMsg.SetActive(true);
    }

    public void pdPanelOpen()
    {
        pdPanel.SetActive(true);
        npcChat.SetActive(false);
        pdBtn.SetActive(false);
    }
}
