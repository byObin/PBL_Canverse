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
        NicknameTxt.text = "큐레이터";

        if (ChatCount == 0)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "중요한 키를 잃어버린 것 같아! " +
                "박물관 안에서 잃어버린 것 같은데... " +
                "혹시 발견하게 된다면 나에게 가져다 줄 수 있을까?";
            ChatCount++;
        }
        else if (ChatCount == 1)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "열쇠를 찾았구나 고마워! 그런데 내가 필요한 열쇠는 하나야. " +
                "저 열쇠는 조력자가 가지고 다녔던 것 같은데... 그에게 열쇠를 가져다 줄래?";
            ChatCount++;
        }
        else if (ChatCount == 2)
        {
            npcChat.SetActive(true);
            dialogueTxt.text = "박물관 구경하러 왔구나! 즐겁게 관람하다 가렴.";
            ChatCount++;
        }

    }

}
