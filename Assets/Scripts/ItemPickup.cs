using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public int itemID;
    public int _count;
    public string pickUpSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // ���� ZŰ ������ ����ǰ� ����.
        // �ֿ��� �� �Ҹ����Ե� �ϰ� ..
        
        //Inventory.instance.GetAnItem(itemID, _count); // �κ��丮�� �߰�
        Destroy(this.gameObject); // �� gem object ���ֹ�����


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
