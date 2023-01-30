using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; // 싱글톤화
    public Text text; // 대사
    private List<string> listSentences; // 커스텀 클래스에 있는 배열
    private int count; // 대화 진행 상황 카운트
    public bool talking = false; // 대화가 이루어지지 않을 때는 z키 막기?
    public GameObject DialoguePanel; // 내가 추가함 비활서오하 하려고

    // 싱글톤화 시키기. 하나만 필요하므로
    private void Awake() 
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // 변수 초기화
        count = 0; // 대화 진행 상황 카운트
        text.text = ""; // 대사
        listSentences = new List<string>(); // 대화 배열
    }

    // 대화창 보여주는 함수
    public void ShowDialogue(Dialogue dialogue) // 커스텀 클래스를 인자로 받음. sentences만 있음 !
    {
        talking = true; // 대화가 이루어지면 이 때부터 z키 입력 가능하도록

        for (int i = 0; i < dialogue.sentences.Length; i++) // 인자로 받은 문장 리스트
        {
            listSentences.Add(dialogue.sentences[i]); // listSentences 변수에 고대로 받아오기
        }

        StartCoroutine(StartDialogueCorutine()); // 코루틴 실행 (대화 시작 ..)
    }

    // 대화창 종료
    public void ExitDialogue()
    {
        // 죄다 초기화
        text.text = "";
        count = 0;
        listSentences.Clear();
        talking = false; // 대화 false
        DialoguePanel.SetActive(false); // 패널도 비활서와
    }

    // 대사 시작
    IEnumerator StartDialogueCorutine()
    {
        for (int i = 0; i < listSentences[count].Length; i++) // 0번째 문장의 총 길이만큼 ..
        {
            text.text += listSentences[count][i]; // 1번째 문장 1글자씩 text 변수에 저장
            yield return new WaitForSeconds(0.01f);

        }

    }

    private void Update()
    {
        if (talking)
        {
            if (Input.GetMouseButtonDown(0)) // 확인버튼 클릭으로 바꾸기 ..
            {
                count++; // 다음 문장 출력해줘야 하니까 !
                text.text = "";

                if (count == listSentences.Count) // count가 리스트 개수와 동일하면 끝내야 함
                {
                    StopAllCoroutines();
                    ExitDialogue(); // 다 비활성화
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCorutine());
                }
            }
        }
    }
}
