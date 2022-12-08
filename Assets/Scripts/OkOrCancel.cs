using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkOrCancel : MonoBehaviour {

    public GameObject up_Panel;
    public GameObject down_Panel;

    public Text up_Text; // �ؽ�Ʈ �ٲٷ���
    public Text down_Text;

    public bool activated;
    private bool keyInput;
    private bool result;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void Selected() // ����Ű�� ���� ���
    {
        result = !result; // 1�� 0����, 0�� 1��

        if (result) // ��ư ������ ��, �Դٰ��� �ϰ� ��.
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
        up_Text.text = _upText; // ���ڷ� ���� �ؽ�Ʈ�� �ٲ�
        down_Text.text = _downText; // ��������

        up_Panel.gameObject.SetActive(false); // ������ ���õ� ��ó�� ���� .. �� false�� ���õ� ��ó�� ����.
        down_Panel.gameObject.SetActive(true);

        StartCoroutine(ShowTwoChoiceCoroutine());
    }

    public bool GetResult()
    {
        return result;
    }

    IEnumerator ShowTwoChoiceCoroutine() // �ణ�� ������ ��. �ߺ�Ű ó���� ������. 
    {
        yield return new WaitForSeconds(0.01f); // 0.01�� ������ �ΰ� true�� ����.
        keyInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyInput) // Ű �Է� ó�� ����
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected(); // ����Ű ���� ������ ȣ���.
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selected(); // ����Ű ���� ������ ȣ���.
            }
            else if (Input.GetKeyDown(KeyCode.Return)) // ����Ű
            {
                keyInput = false;
                activated = false;


            }
            else if (Input.GetKeyDown(KeyCode.X)) // ���
            {
                keyInput = false;
                activated = false;
                result = false; // ��������ϱ� false�� �ؾ� ��. true�� �������� �Ծ��� ..?

            }

        }
        
    }
}
