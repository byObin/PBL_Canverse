using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 담당 라이브러리

public class InventorySlot : MonoBehaviour { // 인벤토리 슬롯 관리

    public Image icon;
    public Text itemName_Text;
    public Text itemCount_Text;
    public GameObject selected_Item;

    public void Additem(Item _item)
    {
        itemName_Text.text = _item.itemName; // 아이템 이름 변경
        icon.sprite = _item.itemIcon; // 아이콘 이미지 설정

        if(Item.ItemType.Use == _item.itemType) // 소모품일 경우
        {
            if (_item.itemCount > 0) // 1개 이상이면
                itemCount_Text.text = "x " + _item.itemCount.ToString(); // 아이템 개수 표시
            else
                itemCount_Text.text = ""; // 0개 이하면 표시 없음
        }
    }

    public void RemoveItem() // 아이템 제거
    {
        itemName_Text.text = "";
        itemCount_Text.text = "";
        icon.sprite = null;
    }

}
