using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��� ���̺귯��

public class InventorySlot : MonoBehaviour { // �κ��丮 ���� ����

    public Image icon;
    public Text itemName_Text;
    public Text itemCount_Text;
    public GameObject selected_Item;

    public void Additem(Item _item)
    {
        itemName_Text.text = _item.itemName; // ������ �̸� ����
        icon.sprite = _item.itemIcon; // ������ �̹��� ����

        if(Item.ItemType.Use == _item.itemType) // �Ҹ�ǰ�� ���
        {
            if (_item.itemCount > 0) // 1�� �̻��̸�
                itemCount_Text.text = "x " + _item.itemCount.ToString(); // ������ ���� ǥ��
            else
                itemCount_Text.text = ""; // 0�� ���ϸ� ǥ�� ����
        }
    }

    public void RemoveItem() // ������ ����
    {
        itemName_Text.text = "";
        itemCount_Text.text = "";
        icon.sprite = null;
    }

}
