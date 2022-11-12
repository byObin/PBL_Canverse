using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour { // 인벤토리 구축

    public string key_sound; // 등등 sound 변수 많았는데 안 하는 걸루 ..

    private InventorySlot[] slots; // 인벤토리 슬롯들

    private List<Item> inventoryItemList; // 플레이어가 소지한 아이템 리스트
    private List<Item> inventoryTabList; // 선택한 탭에 따라 다르게 보여질 아이템 리스트

    public Text Description_Text; // 상단 부연 설명
    public string[] tabDescription; // 선택한 탭에 따라 부연 설명 ??

    public Transform tf; // slot 부모 객체 (Grid Slot). 이거 이용해서 아래 슬롯들 찾을 예정

    public GameObject go; // 인벤토리 활성화, 비활성화
    public GameObject[] selectedTabImages; // 네 개의 패널들(탭)

    private int selectedItem; // 선택된 아이템을 정수로 관리할 예정
    private int selectedTab; // 선택된 탭

    private bool activated; // 인벤토리 활성화시 True;
    private bool tabActivated; // 탭 활성화시 True;
    private bool itemActivated; // 아이템 활성화시 True;
    private bool stopKeyInput; // 키 입력 제한 (소비할 때 질의가 나올텐데, 그 때 키 입력 방지)
    private bool preventExec; // 중복실행 제한

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    
    // Start is called before the first frame update
    void Start() {
        inventoryItemList = new List<Item>(); // 생성자 이용해서 리스트 만듦
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>(); // 부모인 Grid Slot 내의 슬롯들이 저장됨?
    }

    public void ShowTab()
    {
        RemoveSlot(); // 일단, 보이지 않게 하고
        SelectedTab(); // 탭이 빛나도록 함. 아래 구현되어 있음 ~!

    }

    public void RemoveSlot() // 슬롯들이 잠깐 보이지 않도록 ..?
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem(); // 슬롯 내 내용들이 안 보이도록 
            slots[i].gameObject.SetActive(false);
        }

    }

    public void SelectedTab()
    {
        StopAllCoroutines(); // 기존에 돌던 코루틴들 종료
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f; // 투명하도록
        for(int i = 0; i< selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color; // 일단 다 투명하도록
        }
        Description_Text.text = tabDescription[selectedTab]; // 0번으로 초기화 했었음. 소모품 탭의 설명이 출력됨.
        StartCoroutine(SelectedTabEffectCoroutine()); // 선택된 것만 빛나도록 하는 코루틴 실행

    }

    IEnumerator SelectedTabEffectCoroutine()
    {
        while(tabActivated) // 탭이 활성화되어 있다면, 계속 빛나도록
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while(color.a < 0.5f) // 반투명으로 
            {
                color.a += 0.03f; // 빠르게 빛나도록
                selectedTabImages[selectedTab].GetComponent<Image>().color = color; // 점점 올라가다가 반 넘어가면 탈출
                yield return waitTime; // 설정한 시간만큼 기다리도록 ?????
            }

            while (color.a < 0f) // 다시 반대로
            {
                color.a -= 0.03f; // 빠르게 빛나도록
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime; // 설정한 시간만큼 기다리도록 ?????
            }

            yield return new WaitForSeconds(0.3f); // 걍

        }
    }

    // Update is called once per frame
    void Update() {
        if (!stopKeyInput) // 활성화되지 않았을 경우, 활성화되도록 함
        {
            if(Input.GetKeyDown(KeyCode.I)) // I가 입력될 경우
            {
                activated = !activated; // T는 F로, F는 T로 바꿈

                if(activated)
                {
                    // 플레이어가 못 움직이도록 하는데 ,, 우린 다른 방법 찾아야 함.
                    go.SetActive(true); // 인벤토리 활성화
                    selectedTab = 0; // 0번 즉, 소모품 탭으로 초기화
                    tabActivated = true; // 탭 활성화
                    itemActivated = false;

                    ShowTab(); // -> 슬롯들 보이지 않게 하고, 탭 빛나도록 하는 함수 호출

                }
                else // 다시 I 누르면 돌아가도록
                {
                    StopAllCoroutines(); // 기존에 돌던 코루틴들 종료
                    go.SetActive(false); // 인벤토리 비활성화
                    tabActivated = false; // 탭 비활성화
                    itemActivated = false;
                    // 그리고 플레이어 이동 가능하도록 했는데 우린 불가 ..

                }


            }

        }
        
    }
}
