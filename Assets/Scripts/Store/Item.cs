using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    public int itemID; // 아이템의 고유 ID값. 중복 불가능. (50001, 50002)
    public string itemName; // 아이템의 이름. 중복 가능. (고대유물, 고대유물)
    public string itemDescription; // 아이템 설명.
    public int itemCount; // 플레이어의 아이템 소지 개수
    public Sprite itemIcon; // 아이템의 아이콘.
    public ItemType itemType; // 아이템 분류 위함. 아래 4가지 중 하나 가질 수 있음

    public enum ItemType // enum: 열거. 즉, 무슨 종류의 아이템인지 열거.
    {
        Use, // 나중에 변경할 예정
        Equip,
        Quest,
        ETC
    }

    // 생성자를 통해 값을 채워줌.
    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount; // 따로 채워지지 않는 이상 1로 설정
        
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite; // Resources 폴더에서 가져옴. as Sprite로 캐스팅하여 실제로 변환.
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
