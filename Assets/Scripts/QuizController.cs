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
        ResultText.text = "�������� ���Ű� ȯ���մϴ�!\n �������� ���纸���䢾";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickO()
    {

        ResultText.text = "�����մϴ�!\n �������Դϴ٢�";
    }

    public void ClickX()
    {
        ResultText.text = "�ƽ��׿�!\n �ڿ����Դϴ١�";
    }

    public void ClickW()
    {
        ResultText.text = "��!\n �𸣽ô±���Ф�";
    }
}
