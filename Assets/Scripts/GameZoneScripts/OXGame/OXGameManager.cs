using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
        
        //문제 생성
        generateQuestion();
    }

   
    public void retry()
    {
        GoPanel.SetActive(false);
        Questionpanel.SetActive(true);

        generateQuestion();
    }

    // 게임 종료(문제 다 풀었을 떄)
    void GameOver()
    {
        Questionpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreText.text = score + "/" + totalQuestions;

    }

    //맞았을 때
    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    
    //틀렸을 때
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    // 보기 설정
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

    // 문제 만들어줌
    void generateQuestion()
    {
        // 문제 진행
        if (QnA.Count > 0)
        {
            // 0 ~ QnA 중 랜덤으로 currentQuestion에 입력
            currentQuestion = Random.Range(0, QnA.Count);
            // currentQuestion(현재 문제)이 QuestionText에 나타남
            QuestionText.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else        //문제 다 풀었을 경우
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }

}
