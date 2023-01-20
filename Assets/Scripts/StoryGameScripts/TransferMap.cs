using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) // 콜라이더에 닿는 순간 실행되는 내장함수
    {
        if(collision.gameObject.name == "Player" ) // 박스 콜라이더에 닿은 객체의 이름 반환
        {
            SceneManager.LoadScene(transferMapName);

        }
        
    }

}
