using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class First_Msg_Panel : MonoBehaviour
{
    static public First_Msg_Panel instance; // �ڱ� �ڽ��� ������ �޴� �ν��Ͻ�

    public Button panelOkBtn;
    public GameObject FirstMsgPanel;
    public bool firstMsgIsPrinted = false;


    void Start()
    {
        if (instance == null) // �ν��Ͻ��� ó�� ������ ����
        {
            DontDestroyOnLoad(this.gameObject); // �� ��ȯ �� ������� �ʵ���
            instance = this;
        }
        else // �� ������ �����Ǵ� �÷��̾�� �ı�
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
