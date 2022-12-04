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

        // 일단 테스트 .. 실제로는 DB에서 숫자iD 값 찾아서 가져와야 함.
        inventoryItemList.Add(new Item(10001, "우주복", "속도 +50", Item.ItemType.Use)); // 하나 채운 거임.
        inventoryItemList.Add(new Item(10002, "젤리", "속도 +100", Item.ItemType.Use)); // 하나 채운 거임.
        inventoryItemList.Add(new Item(10003, "노랑 포션", "체력을 50000 채워주는 물약", Item.ItemType.Use)); // 하나 채운 거임.
        inventoryItemList.Add(new Item(10004, "주황 포션", "체력을 0 채워주는 물약", Item.ItemType.Use)); // 하나 채운 거임.
        inventoryItemList.Add(new Item(10005, "???", "비밀의 물약", Item.ItemType.Use)); // 하나 채운 거임.
    }

    public void RemoveSlot() // 슬롯들이 잠깐 보이지 않도록 ..?
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem(); // 슬롯 내 내용들이 안 보이도록 
            slots[i].gameObject.SetActive(false);
        }

    } // 인벤토리 슬롯 초기화




    public void ShowTab()
    {
        RemoveSlot(); // 일단, 보이지 않게 하고
        SelectedTab(); // 탭이 빛나도록 함. 아래 구현되어 있음 ~!

    } // 탭 활성화하여 보여주기
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

    } // 선택된 탭을 제외하고 다른 모든 탭의 컬러 알파값을 0으로 조정
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
    } // 선택된 탭 반짝임 효과




    public void ShowItem()
    {
        inventoryTabList.Clear(); // 기존 꺼는 초기화
        RemoveSlot(); // 슬롯도 초기화?
        selectedItem = 0;

        switch (selectedTab) // 고른 탭에 따라 리스트에 다른 게 들어가도록?
        {
            case 0: // 소모품 탭일 경우
                for(int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType) // 소모품일 경우
                        inventoryTabList.Add(inventoryItemList[i]); // 추가해줌.
                }
                break;
            case 1: // 장비 탭일 경우
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType) // 소모품일 경우
                        inventoryTabList.Add(inventoryItemList[i]); // 추가해줌.
                }
                break;
            case 2: // 퀘스트 탭일 경우
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType) // 소모품일 경우
                        inventoryTabList.Add(inventoryItemList[i]); // 추가해줌.
                }
                break;
            case 3: // 기타 탭일 경우
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType) // 소모품일 경우
                        inventoryTabList.Add(inventoryItemList[i]); // 추가해줌.
                }
                break;



        } // 탭에 따른 아이템 분류, 그것을 인벤토리 탭 리스트에 추가

        // 인벤토리 탭 리스트의 내용을, 인벤토리 슬롯에 추가
        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true); // 일단 활성화시키고,
            slots[i].Additem(inventoryTabList[i]); // 넣어주기

        }

        SelectedItem(); // 선택된 것만 빛날 수 있도록
    } // 아이템 활성화 (inventoryTabList에 조건에 맞는 아이템들만 넣어주고, 인벤토리 슬롯에 출력)
    public void SelectedItem()
    {
        StopAllCoroutines(); // 기존의 모든 코루틴을 정지

        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription; // 있다면 설명 넣어줌
            StartCoroutine(SelectedItemEffectCoroutine());

        }
        else
            Description_Text.text = "해당 타입의 아이템을 가지고 있지 않습니당.";
    } // 선택된 아이템을 제외하고 다른 모든 탭의 컬러 알파값을 0으로 조정
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated) // 아이템이 활성화되어 있다면, 계속 빛나도록
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f) // 반투명으로 
            {
                color.a += 0.03f; // 빠르게 빛나도록
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color; // 점점 올라가다가 반 넘어가면 탈출
                yield return waitTime; // 설정한 시간만큼 기다리도록 ?????
            }

            while (color.a < 0f) // 다시 반대로
            {
                color.a -= 0.03f; // 빠르게 빛나도록
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime; // 설정한 시간만큼 기다리도록 ?????
            }

            yield return new WaitForSeconds(0.3f); // 걍

        }
    } // 선택된 아이템이 빛나도록




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

            if (activated) // I 눌러서 인벤토리가 활성화되었고
            {
                if (tabActivated) // 탭도 I눌러서 같이 활성화된 경우에
                {
                    if(Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽 방향키를 누르는데,
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0; // 마지막에는 다시 0번 탭으로 초기화

                        SelectedTab(); // 탭이 바뀌었으니까 함수도 호출
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow)) // 왼쪽 방향키를 누르는데,
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;

                        SelectedTab(); // 탭이 바뀌었으니까 함수도 호출
                    }
                    else if(Input.GetKeyDown(KeyCode.Z)) // Z키를 누르는 경우
                    {
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color; // 탭이 짙어지도록

                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true; // 중복 방지를 위해, 이게 true일 때 키 입력이 되지 않도록 (?)

                        ShowItem();
                    }

                } // 탭 활성화시 키 입력 처리

                else if (itemActivated)
                {
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2) // 아래 버튼 누를 경우 +2씩 증가해야 함.
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            SelectedItem(); // 반짝이도록


                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            SelectedItem(); // 반짝이도록
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            SelectedItem(); // 반짝이도록

                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            SelectedItem(); // 반짝이도록

                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec) // Z키 중복을 방지??
                        {
                            if (selectedTab == 0) // 소모품
                            {
                                stopKeyInput = true;
                                // 물약을 마실 것인지? 같은 선택지 호출
                            }
                            else if (selectedTab == 1)
                            {
                                // 장비 장착
                            }
                            else
                            {
                                // 비프음 출력.
                            }

                        }

                    }
                    
                    if (Input.GetKeyDown(KeyCode.X)) // 아이템 고르다가 다시 탭으로 돌아가도록. 취소 키
                    {
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();

                    }
                } // 아이템 활성화시 키 입력 처리

                if (Input.GetKeyUp(KeyCode.Z)) // 중복 실행 방지 .. 흠
                    preventExec = false;
            }
        }   
    }

}
