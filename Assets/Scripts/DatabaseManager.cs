using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour // �̱���ȭ�� �Ǿ� �־�� ��.
{
    static public DatabaseManager instance; // �̱���ȭ
    // DB�Ŵ����� �ν��Ͻ��� ��� ��������. �� ���� ������ �Ŵ��� �ν��Ͻ��� �� instance�� ��� �͸� �����ϰ� ��.

    private PlayerStat thePlayerStat;

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

    public void UseItem(int _itemID) // ������ ID���� ���Ǿ��� �� ȿ�� ���� (���⿡ ĳ���� �ٲٰ� �̷� �� ��)
    {
        switch(_itemID)
        {
            case 10001:
                thePlayerStat.currentPoint -= 1;
                // ����� ����Ʈ ���ҽ�Ű��.... ������ �ƴ϶�.. ���Ȱ���..
                break;
            case 10002:
                break;
        }
    }

    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();

        // �������� ���� �����ϰ�, �������� DB�� �߰����ֱ�.
        itemList.Add(new Item(10001, "gem", "�����ִ� gem�� �� ã�ƺ�����!", Item.ItemType.Use)); // �ϳ� ä�� ����.
        itemList.Add(new Item(10002, "���� ����", "ü���� 1004 ä���ִ� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.
        itemList.Add(new Item(10003, "��� ����", "ü���� 50000 ä���ִ� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.
        itemList.Add(new Item(10004, "���� ����", "ü���� 0 ä���ִ� ����", Item.ItemType.Equip)); // �ϳ� ä�� ����.
        itemList.Add(new Item(10005, "pbl ����", "ü���� 4 ä���ִ� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.

    }

  
}
