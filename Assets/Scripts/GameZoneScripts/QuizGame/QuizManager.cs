using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> qna;
    public GameObject[] options;
    public int currentQuestion;
    public Text QuestionText;


    // Start is called before the first frame update
    void Start()
    {
        makeQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeQuestion()
    {
        if (qna.Count > 0)
        {
            currentQuestion = Random.Range(0, qna.Count);
            QuestionText.text = qna[currentQuestion].Question;
            setAnswer();
        }
        else
        {
            Debug.Log("문제를 다 풀었습니다.");
        }

    }

    void setAnswer()
    {
        for(int i=0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = qna[currentQuestion].Answers[i];

            if(qna[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }

    public void correct()
    {
        qna.RemoveAt(currentQuestion);
        makeQuestion();
    }
}
