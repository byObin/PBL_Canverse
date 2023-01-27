using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (ChatCount == 0)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "���⼭ �ǽɹ��� �ʰ� ��������, �ֹε�Ϻ��� �ؾ���. �ֹμ��͸� �湮�ؼ� �ֹε���� ���� �Ϸ�!";
            ChatCount++;
        }

        else if (ChatCount == 1)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "���� �Ҿ���ȴ� ���踦 ã�ұ���. ���� ����! �츮 ������ ȯ�� ��Ƽ�� �� �ٰ�." +
                "�� ��й�ȣ�� ���õ� ��Ʈ�� �˷��ٰ�!";
            pdBtn.SetActive(true);

            ChatCount++;
        }

        else if (ChatCount == 2)
        {
            pdPanel.SetActive(true);
        }
    }

   
}


