using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class First_Msg_Panel : MonoBehaviour
{
    //static public First_Msg_Panel instance; // �ڱ� �ڽ��� ������ �޴� �ν��Ͻ�

    public Button panelOkBtn;
    public GameObject FirstMsgPanel;


    void Start()
    {

        if (GameObject.Find("Player").GetComponent<Sample_Player_Move>().didPlayerReadFirstMsg == true)
        {
            FirstMsgPanel.SetActive(false);
        }
        panelOkBtn.onClick.AddListener(activeoff);
    }

    void activeoff()
    {
        FirstMsgPanel.SetActive(false);
        GameObject.Find("Player").GetComponent<Sample_Player_Move>().didPlayerReadFirstMsg = true;
    }
}
