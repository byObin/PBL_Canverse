using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondKey : MonoBehaviour
{
    public GameObject npcChat;
    public Text NicknameTxt;
    public Text dialogueTxt;


    void OnMouseDown()
    {
        npcChat.SetActive(true);
        NicknameTxt.text = "나";
        dialogueTxt.text = "열쇠를 또 찾았네! 이게 마지막 열쇠인 것 같아. 큐레이터 님께 가져다 드려야지!";
    }
}
