using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    public int itemID; // �������� ���� ID��. �ߺ� �Ұ���. (50001, 50002)
    public string itemName; // �������� �̸�. �ߺ� ����. (�������, �������)
    public string itemDescription; // ������ ����.
    public int itemCount; // �÷��̾��� ������ ���� ����
    public Sprite itemIcon; // �������� ������.
    public ItemType itemType; // ������ �з� ����. �Ʒ� 4���� �� �ϳ� ���� �� ����

    public enum ItemType // enum: ����. ��, ���� ������ ���������� ����.
    {
        Use, // ���߿� ������ ����
        Equip,
        Quest,
        ETC
    }

    // �����ڸ� ���� ���� ä����.
    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount; // ���� ä������ �ʴ� �̻� 1�� ����
        
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite; // Resources �������� ������. as Sprite�� ĳ�����Ͽ� ������ ��ȯ.
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
