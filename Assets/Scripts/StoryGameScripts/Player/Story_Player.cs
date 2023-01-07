using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Player : MonoBehaviour
{
   

  

    public float speed = 5;


    void Update()
    {
        float posX = Input.GetAxisRaw("Horizontal");    //keybord input
        float posY = Input.GetAxisRaw("Vertical");         //keybord input


        transform.Translate(new Vector2(posX, posY) * Time.deltaTime * speed);


    }


    // Start is called before the first frame update
    void Start()
    {

    }
}
