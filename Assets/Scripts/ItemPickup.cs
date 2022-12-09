using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public int itemID;
    public int _count;
    public string pickUpSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 원래 Z키 누르면 실행되게 했음.
        // 주웠을 때 소리나게도 하고 ..
        
        //Inventory.instance.GetAnItem(itemID, _count); // 인벤토리에 추가
        Destroy(this.gameObject); // 이 gem object 없애버리기


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
