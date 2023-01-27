using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; // �̱���ȭ
    public Text text; // ���
    private List<string> listSentences; // Ŀ���� Ŭ������ �ִ� �迭
    private int count; // ��ȭ ���� ��Ȳ ī��Ʈ

    // �̱���ȭ ��Ű��. �ϳ��� �ʿ��ϹǷ�
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
        // ���� �ʱ�ȭ
        count = 0; 
        text.text = "";
        listSentences = new List<string>(); // �迭 
    }

    // ��ȭâ �����ִ� �Լ�
    public void ShowDialogue(Dialogue dialogue) // Ŀ���� Ŭ������ ���ڷ�
    {
        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]); // ����Ʈ�� �޾ƿ���
        }

        StartCoroutine(StartDialogueCorutine()); // �ڷ�ƾ ���� (��ȭ ���� ..)
    }

    public void ExitDialogue()
    {
        // �˴� �ʱ�ȭ
        text.text = "";
        count = 0;
        listSentences.Clear();
        // �ٵ� ���� ..�� ..
    }

    IEnumerator StartDialogueCorutine()
    {
        

        for (int i = 0; i < listSentences[count].Length; i++) // 0��° ������ �� ���̸�ŭ ..
        {
            text.text += listSentences[count][i]; // 1��° ���� 1���ھ� ���, �����ٶ󸶹ٻ��
            yield return new WaitForSeconds(0.01f);

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            count++;
            text.text = "";

            if(count != listSentences.Count - 1) // count�� ����Ʈ ������ �����ϸ� ������ ��
            {
                StopAllCoroutines();
                ExitDialogue(); // �� ��Ȱ��ȭ
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(StartDialogueCorutine());
            }

        }
    }


}
