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
        NicknameTxt.text = "��";
        dialogueTxt.text = "���踦 �� ã�ҳ�! �̰� ������ ������ �� ����. ť������ �Բ� ������ �������!";
    }
}
