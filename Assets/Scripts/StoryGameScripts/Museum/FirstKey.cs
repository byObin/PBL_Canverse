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
        NicknameTxt.text = "��";
        dialogueTxt.text = "���踦 ã�Ҵ�! Ȥ�� �𸣴� ���� �� ã�ƺ���?";
        firstKey.SetActive(false);
    }
}
