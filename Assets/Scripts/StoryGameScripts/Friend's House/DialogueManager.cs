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
        count = 0; 
        text.text = "";
        listSentences = new List<string>(); // 배열 
    }

    // 대화창 보여주는 함수
    public void ShowDialogue(Dialogue dialogue) // 커스텀 클래스를 인자로
    {
        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]); // 리스트에 받아오기
        }

        StartCoroutine(StartDialogueCorutine()); // 코루틴 실행 (대화 시작 ..)
    }

    public void ExitDialogue()
    {
        // 죄다 초기화
        text.text = "";
        count = 0;
        listSentences.Clear();
        // 근데 대사바 ..도 ..
    }

    IEnumerator StartDialogueCorutine()
    {
        

        for (int i = 0; i < listSentences[count].Length; i++) // 0번째 문장의 총 길이만큼 ..
        {
            text.text += listSentences[count][i]; // 1번째 문장 1글자씩 출력, 가나다라마바사아
            yield return new WaitForSeconds(0.01f);

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            count++;
            text.text = "";

            if(count != listSentences.Count - 1) // count가 리스트 개수와 동일하면 끝내야 함
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
