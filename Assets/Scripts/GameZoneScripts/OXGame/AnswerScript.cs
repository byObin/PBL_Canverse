using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public OXGameManager oxgameManager;

    public void Answer()
    {
        if(isCorrect)   //참일 경우
        {
            Debug.Log("정답");
            oxgameManager.correct();
        }
        else    
        {
            Debug.Log("오답");
            oxgameManager.wrong();
        }
    }

}
