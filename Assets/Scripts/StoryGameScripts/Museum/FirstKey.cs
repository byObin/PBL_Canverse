using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstKey : MonoBehaviour
{
    public GameObject npcChat;
    public GameObject firstKey;
    public Text NicknameTxt;
    public Text dialogueTxt;


    void OnMouseDown()
    {
        npcChat.SetActive(true);
        NicknameTxt.text = "나";
        dialogueTxt.text = "열쇠를 찾았다! 혹시 모르니 조금 더 찾아볼까?";
        firstKey.SetActive(false);
    }
}
