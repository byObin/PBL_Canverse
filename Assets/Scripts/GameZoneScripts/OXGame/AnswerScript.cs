using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public OXGameManager oxgameManager;

    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("����");
            oxgameManager.correct();
        }
        else
        {
            Debug.Log("����");
            oxgameManager.wrong();
        }
    }

}
