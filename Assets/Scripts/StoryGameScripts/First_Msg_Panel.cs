using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class First_Msg_Panel : MonoBehaviour
{
    static public First_Msg_Panel instance; // 자기 자신을 값으로 받는 인스턴스

    public Button panelOkBtn;
    public GameObject FirstMsgPanel;
    public bool firstMsgIsPrinted = false;


    void Start()
    {
        if (instance == null) // 인스턴스가 처음 생성될 때만
        {
            DontDestroyOnLoad(this.gameObject); // 씬 전환 시 사라지지 않도록
            instance = this;
        }
        else // 그 다음에 생성되는 플레이어는 파기
            Destroy(this.gameObject);

        if (firstMsgIsPrinted == false)
        {
            FirstMsgPanel.SetActive(true);

        }
        panelOkBtn.onClick.AddListener(activeoff);
    }

    void activeoff()
    {
        FirstMsgPanel.SetActive(false);
        firstMsgIsPrinted = true;
    }

    
}
