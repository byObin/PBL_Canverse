using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour // 싱글톤화가 되어 있어야 함.
{
    static public DatabaseManager instance; // 싱글톤화
    // DB매니저의 인스턴스를 담는 전역변수. 이 게임 내에서 매니저 인스턴스는 이 instance에 담긴 것만 존재하게 됨.

    private void Awake()
    {
        if (instance != null) // 씬 이동 되었는데 그 씬에도 DB매니저가 존재할 수 있음.
        {
            Destroy(this.gameObject); // 그럴 경우, 이미 사용하던 인스턴스를 계속 사용하고, 자신(새로운 씬의 DB매니저)을 삭제함
        }
        else // 전역변수 instance에 DB매니저 인스턴스가 담겨있지 않은 경우
        {
            DontDestroyOnLoad(this.gameObject); // 씬 전환이 되더라도 파괴되지 않도록 함.
            instance = this; // 자신을 instance에 넣어줌
        }
    }


    public string[] var_name; // 변수 이름
    public float[] var; // 배열 변수

    public string[] switch_name; // float값 기록하는 변수
    public bool[] switches; // T/F 기록하는 배열 변수 ex. boss가 25번째 스위치면 이를 true로 변경하는 등

    public List<Item> itemList = new List<Item>(); // 아이템 리스트 생성. 

    void Start()
    {
        // 아이템을 직접 구현하고, 아이템을 DB에 추가해주기.
        itemList.Add(new Item(10001, "빨간 포션", "체력을 50 채워주는 물약", Item.ItemType.Use)); // 하나 채운 거임.
        
    }

  
}
