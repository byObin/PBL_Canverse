using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderTreeNPC : MonoBehaviour
{
    public GameObject npcChat;
    public GameObject pdQuestion;
    public GameObject OKBtn;
    public Text dialogueTxt;

    public int ChatCount = 0;

    void OnMouseDown()
    {
        if (ChatCount == 0)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "여기서 의심받지 않고 지내려면, 주민등록부터 해야해. 주민센터를 방문해서 주민등록을 먼저 하렴!";
            ChatCount++;
        }

        else if (ChatCount == 1)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "내가 잃어버렸던 열쇠를 찾았구나. 정말 고마워! 우리 집에서 환영 파티를 해 줄게." +
                "집 비밀번호를 알 수 있는 문제를 맞춰 봐!";
            OKBtn.SetActive(false);
            pdQuestion.SetActive(true);
            ChatCount++;
        }
    }

    // 조력자 집 비밀번호 문제 풀기
    public void pdQ()
    {

    }
}
