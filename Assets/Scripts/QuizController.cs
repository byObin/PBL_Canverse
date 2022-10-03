using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Text ResultText;

    // Start is called before the first frame update
    void Start()
    {
        ResultText.text = "퀴즈존에 오신걸 환영합니다!\n ♥정답을 맞춰보세요♥";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickO()
    {

        ResultText.text = "축하합니다!\n ♥정답입니다♥";
    }

    public void ClickX()
    {
        ResultText.text = "아쉽네요!\n ★오답입니다★";
    }

    public void ClickW()
    {
        ResultText.text = "앗!\n 모르시는군요ㅠㅠ";
    }
}
