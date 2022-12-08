using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkOrCancel : MonoBehaviour {

    public GameObject up_Panel;
    public GameObject down_Panel;

    public Text up_Text; // 텍스트 바꾸려고
    public Text down_Text;

    public bool activated;
    private bool keyInput;
    private bool result;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void Selected() // 방향키가 눌릴 경우
    {
        result = !result; // 1은 0으로, 0은 1로

        if (result) // 암튼 눌렸을 때, 왔다갔다 하게 함.
        {
            up_Panel.gameObject.SetActive(false);
            down_Panel.gameObject.SetActive(true);
        }
        else
        {
            up_Panel.gameObject.SetActive(true);
            down_Panel.gameObject.SetActive(false);
        }
    }

    public void ShowTwoChoice(string _upText, string _downText)
    {
        activated = true;
        result = true;
        up_Text.text = _upText; // 인자로 들어온 텍스트로 바꿈
        down_Text.text = _downText; // 마찬가지

        up_Panel.gameObject.SetActive(false); // 위에가 선택된 것처럼 연출 .. 즉 false면 선택된 것처럼 보임.
        down_Panel.gameObject.SetActive(true);

        StartCoroutine(ShowTwoChoiceCoroutine());
    }

    public bool GetResult()
    {
        return result;
    }

    IEnumerator ShowTwoChoiceCoroutine() // 약간의 유예를 줌. 중복키 처리를 방지함. 
    {
        yield return new WaitForSeconds(0.01f); // 0.01초 유예를 두고 true를 만듦.
        keyInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyInput) // 키 입력 처리 구현
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected(); // 방향키 눌릴 때마다 호출됨.
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selected(); // 방향키 눌릴 때마다 호출됨.
            }
            else if (Input.GetKeyDown(KeyCode.Return)) // 엔터키
            {
                keyInput = false;
                activated = false;


            }
            else if (Input.GetKeyDown(KeyCode.X)) // 취소
            {
                keyInput = false;
                activated = false;
                result = false; // 취소했으니까 false로 해야 함. true면 아이템이 먹어짐 ..?

            }

        }
        
    }
}
