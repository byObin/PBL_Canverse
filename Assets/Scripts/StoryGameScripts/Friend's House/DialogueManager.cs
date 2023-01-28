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
    public bool talking = false; // ��ȭ�� �̷������ ���� ���� zŰ ����?
    public GameObject DialoguePanel; // ���� �߰��� ��Ȱ������ �Ϸ���

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
        count = 0; // ��ȭ ���� ��Ȳ ī��Ʈ
        text.text = ""; // ���
        listSentences = new List<string>(); // ��ȭ �迭
    }

    // ��ȭâ �����ִ� �Լ�
    public void ShowDialogue(Dialogue dialogue) // Ŀ���� Ŭ������ ���ڷ� ����. sentences�� ���� !
    {
        talking = true; // ��ȭ�� �̷������ �� ������ zŰ �Է� �����ϵ���

        for (int i = 0; i < dialogue.sentences.Length; i++) // ���ڷ� ���� ���� ����Ʈ
        {
            listSentences.Add(dialogue.sentences[i]); // listSentences ������ ���� �޾ƿ���
        }

        StartCoroutine(StartDialogueCorutine()); // �ڷ�ƾ ���� (��ȭ ���� ..)
    }

    // ��ȭâ ����
    public void ExitDialogue()
    {
        // �˴� �ʱ�ȭ
        text.text = "";
        count = 0;
        listSentences.Clear();
        talking = false; // ��ȭ false
        DialoguePanel.SetActive(false); // �гε� ��Ȱ����
    }

    // ��� ����
    IEnumerator StartDialogueCorutine()
    {
        for (int i = 0; i < listSentences[count].Length; i++) // 0��° ������ �� ���̸�ŭ ..
        {
            text.text += listSentences[count][i]; // 1��° ���� 1���ھ� text ������ ����
            yield return new WaitForSeconds(0.01f);

        }

    }

    private void Update()
    {
        if (talking)
        {
            if (Input.GetMouseButtonDown(0)) // Ȯ�ι�ư Ŭ������ �ٲٱ� ..
            {
                count++; // ���� ���� �������� �ϴϱ� !
                text.text = "";

                if (count == listSentences.Count) // count�� ����Ʈ ������ �����ϸ� ������ ��
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
}
