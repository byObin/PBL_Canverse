using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImgChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("gem-1");
            spriteR.sprite = sprites[0];
        }
    }*/

   /* void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            Debug.Log("익명채팅구역에서 탈출");
        }
    }*/
}
