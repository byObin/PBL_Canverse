using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OXGameManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] optioins;
    public int currentQuestion;

    public GameObject Questionpanel;
    public GameObject GoPanel; 
    public GameObject OXGameUI;

    public Text QuestionText;
    public Text ScoreText;

    int totalQuestions = 0;
    public int score;

    private void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestion();
    }

   
    public void retry()
    {
        Questionpanel.SetActive(true);
        GoPanel.SetActive(false);
        OXGameUI.SetActive(false);

        //√ ±‚»≠
        totalQuestions = QnA.Count;
        score = 0;
    }


    void GameOver()
    {
        Questionpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreText.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {

        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < optioins.Length; i++)
        {
            optioins[i].GetComponent<AnswerScript>().isCorrect = false;
            optioins[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                optioins[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {

        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionText.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }

}
