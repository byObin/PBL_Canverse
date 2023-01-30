using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnderTreeNPC : MonoBehaviour
{
    public GameObject npcChat;
    public Text dialogueTxt;
    public GameObject pdBtn;
    public GameObject okBtn;
    public GameObject pdPanel;

    public int ChatCount = 0;

    

    void OnMouseDown()
    {
        if (GameObject.Find("Player").GetComponent<Sample_Player_Move>().didPlayerTalkNPC == false)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "여기서 의심받지 않고 지내려면, 주민등록부터 해야해. 주민센터를 방문해서 주민등록을 먼저 하렴!";
            GameObject.Find("Player").GetComponent<Sample_Player_Move>().didPlayerTalkNPC = true;
        }
        else
        {
            if (ChatCount == 0)
            {
                npcChat.SetActive(true);
                dialogueTxt.text = "내가 잃어버렸던 열쇠를 찾았구나. 정말 고마워! 우리 집에서 환영 파티를 해 줄게." +
                    "집 비밀번호와 관련된 힌트를 알려줄게!";
                pdBtn.SetActive(true);

                ChatCount++;
            }

            else if (ChatCount == 1)
            {
                pdPanel.SetActive(true);
            }
        }

    }

   
}


