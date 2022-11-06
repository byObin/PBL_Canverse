using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour // �̱���ȭ�� �Ǿ� �־�� ��.
{
    static public DatabaseManager instance; // �̱���ȭ
    // DB�Ŵ����� �ν��Ͻ��� ��� ��������. �� ���� ������ �Ŵ��� �ν��Ͻ��� �� instance�� ��� �͸� �����ϰ� ��.

    private void Awake()
    {
        if (instance != null) // �� �̵� �Ǿ��µ� �� ������ DB�Ŵ����� ������ �� ����.
        {
            Destroy(this.gameObject); // �׷� ���, �̹� ����ϴ� �ν��Ͻ��� ��� ����ϰ�, �ڽ�(���ο� ���� DB�Ŵ���)�� ������
        }
        else // �������� instance�� DB�Ŵ��� �ν��Ͻ��� ������� ���� ���
        {
            DontDestroyOnLoad(this.gameObject); // �� ��ȯ�� �Ǵ��� �ı����� �ʵ��� ��.
            instance = this; // �ڽ��� instance�� �־���
        }
    }


    public string[] var_name; // ���� �̸�
    public float[] var; // �迭 ����

    public string[] switch_name; // float�� ����ϴ� ����
    public bool[] switches; // T/F ����ϴ� �迭 ���� ex. boss�� 25��° ����ġ�� �̸� true�� �����ϴ� ��

    public List<Item> itemList = new List<Item>(); // ������ ����Ʈ ����. 

    void Start()
    {
        // �������� ���� �����ϰ�, �������� DB�� �߰����ֱ�.
        itemList.Add(new Item(10001, "���� ����", "ü���� 50 ä���ִ� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.
        
    }

  
}
