using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumManager : MonoBehaviour
{
  
    public GameObject npcChat;
    public Text NicknameTxt;
    public Text dialogueTxt;

    public int ChatCount = 0;

   
    void OnMouseDown()
    {
        NicknameTxt.text = "ť������";

        if (ChatCount == 0)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "�߿��� Ű�� �Ҿ���� �� ����! " +
                "�ڹ��� �ȿ��� �Ҿ���� �� ������... " +
                "Ȥ�� �߰��ϰ� �ȴٸ� ������ ������ �� �� ������?";
            ChatCount++;
        }
        else if (ChatCount == 1)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "���踦 ã�ұ��� ����! �׷��� ���� �ʿ��� ����� �ϳ���. " +
                "�� ����� �����ڰ� ������ �ٳ�� �� ������... �׿��� ���踦 ������ �ٷ�?";
            ChatCount++;
        }
        else if (ChatCount == 2)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "�ڹ��� �����Ϸ� �Ա���! ��̰� �����ϴ� ����.";
            ChatCount++;
        }

    }

}
