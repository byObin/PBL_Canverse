using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름


    private void OnTriggerEnter2D(Collider2D collision) // 콜라이더에 닿는 순간 실행되는 내장함수
    {
        if(collision.gameObject.name == "Player" ) // 박스 콜라이더에 닿은 객체의 이름 반환
        {
            Scene scene = SceneManager.GetActiveScene();    // 현재 씬 이름 받아옴
            
            // 각 씬에 따라 건물 나가면 village씬에서 해당 건물 문 앞에 위치하도록 지정
            if(scene.name == "TownOffice") { TownOfficeToVillage(); }
            else if(scene.name == "Library") { LibraryToVillage(); }
            else if (scene.name == "Museum") { MuseumToVillage(); }
            else if (scene.name == "Friend'sHouse") { FriendHouseToVillage(); }

            SceneManager.LoadScene(transferMapName);

        }

    }


    void TownOfficeToVillage()
    {
        GameObject.Find("Player").transform.position = new Vector3(2.19f, 2.61f, 0);
    }

    void LibraryToVillage()
    {
        GameObject.Find("Player").transform.position = new Vector3(-13.13f, -3.26f, 0);
    }

    void MuseumToVillage()
    {
        GameObject.Find("Player").transform.position = new Vector3(15.55f, -15.92f, 0);
    }

    void FriendHouseToVillage()
    {
        GameObject.Find("Player").transform.position = new Vector3(19.99f, 3.56f, 0);
    }

}
