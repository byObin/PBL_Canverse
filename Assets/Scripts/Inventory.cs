using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour { // �κ��丮 ����

    public string key_sound; // ��� sound ���� ���Ҵµ� �� �ϴ� �ɷ� ..

    private InventorySlot[] slots; // �κ��丮 ���Ե�

    private List<Item> inventoryItemList; // �÷��̾ ������ ������ ����Ʈ
    private List<Item> inventoryTabList; // ������ �ǿ� ���� �ٸ��� ������ ������ ����Ʈ

    public Text Description_Text; // ��� �ο� ����
    public string[] tabDescription; // ������ �ǿ� ���� �ο� ���� ??

    public Transform tf; // slot �θ� ��ü (Grid Slot). �̰� �̿��ؼ� �Ʒ� ���Ե� ã�� ����

    public GameObject go; // �κ��丮 Ȱ��ȭ, ��Ȱ��ȭ
    public GameObject[] selectedTabImages; // �� ���� �гε�(��)

    private int selectedItem; // ���õ� �������� ������ ������ ����
    private int selectedTab; // ���õ� ��

    private bool activated; // �κ��丮 Ȱ��ȭ�� True;
    private bool tabActivated; // �� Ȱ��ȭ�� True;
    private bool itemActivated; // ������ Ȱ��ȭ�� True;
    private bool stopKeyInput; // Ű �Է� ���� (�Һ��� �� ���ǰ� �����ٵ�, �� �� Ű �Է� ����)
    private bool preventExec; // �ߺ����� ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    


    // Start is called before the first frame update
    void Start() {
        inventoryItemList = new List<Item>(); // ������ �̿��ؼ� ����Ʈ ����
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>(); // �θ��� Grid Slot ���� ���Ե��� �����?

        // �ϴ� �׽�Ʈ .. �����δ� DB���� ����iD �� ã�Ƽ� �����;� ��.
        inventoryItemList.Add(new Item(10001, "���ֺ�", "�ӵ� +50", Item.ItemType.Use)); // �ϳ� ä�� ����.
        inventoryItemList.Add(new Item(10002, "����", "�ӵ� +100", Item.ItemType.Use)); // �ϳ� ä�� ����.
        inventoryItemList.Add(new Item(10003, "��� ����", "ü���� 50000 ä���ִ� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.
        inventoryItemList.Add(new Item(10004, "��Ȳ ����", "ü���� 0 ä���ִ� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.
        inventoryItemList.Add(new Item(10005, "???", "����� ����", Item.ItemType.Use)); // �ϳ� ä�� ����.
    }

    public void RemoveSlot() // ���Ե��� ��� ������ �ʵ��� ..?
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem(); // ���� �� ������� �� ���̵��� 
            slots[i].gameObject.SetActive(false);
        }

    } // �κ��丮 ���� �ʱ�ȭ




    public void ShowTab()
    {
        RemoveSlot(); // �ϴ�, ������ �ʰ� �ϰ�
        SelectedTab(); // ���� �������� ��. �Ʒ� �����Ǿ� ���� ~!

    } // �� Ȱ��ȭ�Ͽ� �����ֱ�
    public void SelectedTab()
    {
        StopAllCoroutines(); // ������ ���� �ڷ�ƾ�� ����
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f; // �����ϵ���
        for(int i = 0; i< selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color; // �ϴ� �� �����ϵ���
        }
        Description_Text.text = tabDescription[selectedTab]; // 0������ �ʱ�ȭ �߾���. �Ҹ�ǰ ���� ������ ��µ�.
        StartCoroutine(SelectedTabEffectCoroutine()); // ���õ� �͸� �������� �ϴ� �ڷ�ƾ ����

    } // ���õ� ���� �����ϰ� �ٸ� ��� ���� �÷� ���İ��� 0���� ����
    IEnumerator SelectedTabEffectCoroutine()
    {
        while(tabActivated) // ���� Ȱ��ȭ�Ǿ� �ִٸ�, ��� ��������
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while(color.a < 0.5f) // ���������� 
            {
                color.a += 0.03f; // ������ ��������
                selectedTabImages[selectedTab].GetComponent<Image>().color = color; // ���� �ö󰡴ٰ� �� �Ѿ�� Ż��
                yield return waitTime; // ������ �ð���ŭ ��ٸ����� ?????
            }

            while (color.a < 0f) // �ٽ� �ݴ��
            {
                color.a -= 0.03f; // ������ ��������
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime; // ������ �ð���ŭ ��ٸ����� ?????
            }

            yield return new WaitForSeconds(0.3f); // ��

        }
    } // ���õ� �� ��¦�� ȿ��




    public void ShowItem()
    {
        inventoryTabList.Clear(); // ���� ���� �ʱ�ȭ
        RemoveSlot(); // ���Ե� �ʱ�ȭ?
        selectedItem = 0;

        switch (selectedTab) // �� �ǿ� ���� ����Ʈ�� �ٸ� �� ������?
        {
            case 0: // �Ҹ�ǰ ���� ���
                for(int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Use == inventoryItemList[i].itemType) // �Ҹ�ǰ�� ���
                        inventoryTabList.Add(inventoryItemList[i]); // �߰�����.
                }
                break;
            case 1: // ��� ���� ���
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType) // �Ҹ�ǰ�� ���
                        inventoryTabList.Add(inventoryItemList[i]); // �߰�����.
                }
                break;
            case 2: // ����Ʈ ���� ���
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType) // �Ҹ�ǰ�� ���
                        inventoryTabList.Add(inventoryItemList[i]); // �߰�����.
                }
                break;
            case 3: // ��Ÿ ���� ���
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType) // �Ҹ�ǰ�� ���
                        inventoryTabList.Add(inventoryItemList[i]); // �߰�����.
                }
                break;



        } // �ǿ� ���� ������ �з�, �װ��� �κ��丮 �� ����Ʈ�� �߰�

        // �κ��丮 �� ����Ʈ�� ������, �κ��丮 ���Կ� �߰�
        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true); // �ϴ� Ȱ��ȭ��Ű��,
            slots[i].Additem(inventoryTabList[i]); // �־��ֱ�

        }

        SelectedItem(); // ���õ� �͸� ���� �� �ֵ���
    } // ������ Ȱ��ȭ (inventoryTabList�� ���ǿ� �´� �����۵鸸 �־��ְ�, �κ��丮 ���Կ� ���)
    public void SelectedItem()
    {
        StopAllCoroutines(); // ������ ��� �ڷ�ƾ�� ����

        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;
            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;
            Description_Text.text = inventoryTabList[selectedItem].itemDescription; // �ִٸ� ���� �־���
            StartCoroutine(SelectedItemEffectCoroutine());

        }
        else
            Description_Text.text = "�ش� Ÿ���� �������� ������ ���� �ʽ��ϴ�.";
    } // ���õ� �������� �����ϰ� �ٸ� ��� ���� �÷� ���İ��� 0���� ����
    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated) // �������� Ȱ��ȭ�Ǿ� �ִٸ�, ��� ��������
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f) // ���������� 
            {
                color.a += 0.03f; // ������ ��������
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color; // ���� �ö󰡴ٰ� �� �Ѿ�� Ż��
                yield return waitTime; // ������ �ð���ŭ ��ٸ����� ?????
            }

            while (color.a < 0f) // �ٽ� �ݴ��
            {
                color.a -= 0.03f; // ������ ��������
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime; // ������ �ð���ŭ ��ٸ����� ?????
            }

            yield return new WaitForSeconds(0.3f); // ��

        }
    } // ���õ� �������� ��������




    // Update is called once per frame
    void Update() {
        if (!stopKeyInput) // Ȱ��ȭ���� �ʾ��� ���, Ȱ��ȭ�ǵ��� ��
        {
            if(Input.GetKeyDown(KeyCode.I)) // I�� �Էµ� ���
            {
                activated = !activated; // T�� F��, F�� T�� �ٲ�

                if(activated)
                {
                    // �÷��̾ �� �����̵��� �ϴµ� ,, �츰 �ٸ� ��� ã�ƾ� ��.
                    go.SetActive(true); // �κ��丮 Ȱ��ȭ
                    selectedTab = 0; // 0�� ��, �Ҹ�ǰ ������ �ʱ�ȭ
                    tabActivated = true; // �� Ȱ��ȭ
                    itemActivated = false;

                    ShowTab(); // -> ���Ե� ������ �ʰ� �ϰ�, �� �������� �ϴ� �Լ� ȣ��
                }
                else // �ٽ� I ������ ���ư�����
                {
                    StopAllCoroutines(); // ������ ���� �ڷ�ƾ�� ����
                    go.SetActive(false); // �κ��丮 ��Ȱ��ȭ
                    tabActivated = false; // �� ��Ȱ��ȭ
                    itemActivated = false;
                    // �׸��� �÷��̾� �̵� �����ϵ��� �ߴµ� �츰 �Ұ� ..
                }
            }

            if (activated) // I ������ �κ��丮�� Ȱ��ȭ�Ǿ���
            {
                if (tabActivated) // �ǵ� I������ ���� Ȱ��ȭ�� ��쿡
                {
                    if(Input.GetKeyDown(KeyCode.RightArrow)) // ������ ����Ű�� �����µ�,
                    {
                        if (selectedTab < selectedTabImages.Length - 1)
                            selectedTab++;
                        else
                            selectedTab = 0; // ���������� �ٽ� 0�� ������ �ʱ�ȭ

                        SelectedTab(); // ���� �ٲ�����ϱ� �Լ��� ȣ��
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow)) // ���� ����Ű�� �����µ�,
                    {
                        if (selectedTab > 0)
                            selectedTab--;
                        else
                            selectedTab = selectedTabImages.Length - 1;

                        SelectedTab(); // ���� �ٲ�����ϱ� �Լ��� ȣ��
                    }
                    else if(Input.GetKeyDown(KeyCode.Z)) // ZŰ�� ������ ���
                    {
                        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<Image>().color = color; // ���� £��������

                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true; // �ߺ� ������ ����, �̰� true�� �� Ű �Է��� ���� �ʵ��� (?)

                        ShowItem();
                    }

                } // �� Ȱ��ȭ�� Ű �Է� ó��

                else if (itemActivated)
                {
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2) // �Ʒ� ��ư ���� ��� +2�� �����ؾ� ��.
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            SelectedItem(); // ��¦�̵���


                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            SelectedItem(); // ��¦�̵���
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            SelectedItem(); // ��¦�̵���

                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            SelectedItem(); // ��¦�̵���

                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec) // ZŰ �ߺ��� ����??
                        {
                            if (selectedTab == 0) // �Ҹ�ǰ
                            {
                                stopKeyInput = true;
                                // ������ ���� ������? ���� ������ ȣ��
                            }
                            else if (selectedTab == 1)
                            {
                                // ��� ����
                            }
                            else
                            {
                                // ������ ���.
                            }

                        }

                    }
                    
                    if (Input.GetKeyDown(KeyCode.X)) // ������ ���ٰ� �ٽ� ������ ���ư�����. ��� Ű
                    {
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();

                    }
                } // ������ Ȱ��ȭ�� Ű �Է� ó��

                if (Input.GetKeyUp(KeyCode.Z)) // �ߺ� ���� ���� .. ��
                    preventExec = false;
            }
        }   
    }

}
