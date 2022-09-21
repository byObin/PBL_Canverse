using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;


    void Update()
    {
        float posX = Input.GetAxisRaw("Horizontal");    //keybord input
        float posY = Input.GetAxisRaw("Vertical");         //keybord input


        transform.Translate(new Vector2(posX, posY) * Time.deltaTime * speed);


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            Debug.Log("익명채팅구역에 들어감");
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("images/ghost");
            spriteR.sprite = sprites[0];
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            Debug.Log("익명채팅구역에서 탈출");
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("images/player");
            spriteR.sprite = sprites[0];
        }
    }


   
    
}
