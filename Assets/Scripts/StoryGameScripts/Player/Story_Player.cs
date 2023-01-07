using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_Player : MonoBehaviour
{

    public float speed = 5;

    void Update()   // 키보드 방향키에 따라 플레이어 이동
    {
        float posX = Input.GetAxisRaw("Horizontal");    //keybord input
        float posY = Input.GetAxisRaw("Vertical");         //keybord input


        transform.Translate(new Vector2(posX, posY) * Time.deltaTime * speed);

    }


    //충돌처리 : 플레이어가 건물 문과 충돌 시
    void OnTriggerEnter2D(Collider2D collider)
    {

        //도서관 문 충돌 시 Library Scene으로 이동
        if (collider.gameObject.name == "LibDoor")
        {
             Debug.Log("도서관 문에 충돌");
             SceneManager.LoadScene("Library");
        }

        //박물관 문 충돌 시 Museum Scene으로 이동
        if (collider.gameObject.name == "MusDoor")
        {
            Debug.Log("박물관 문에 충돌");
            SceneManager.LoadScene("Museum");
        }

        //동사무소 문 충돌 시 TownOffice Scene으로 이동
        if (collider.gameObject.name == "OffiDoor")
        {
            Debug.Log("동사무소 문에 충돌");
            SceneManager.LoadScene("TownOffice");
        }

        //친구 집 문 충돌 시 Friend'sHouse Scene으로 이동
        if (collider.gameObject.name == "FirDoor")
        {
            Debug.Log("친구 집 문에 충돌");
            SceneManager.LoadScene("Friend'sHouse");
        }

    }

}
