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
    }

    public void ShowTab()
    {
        RemoveSlot(); // �ϴ�, ������ �ʰ� �ϰ�
        SelectedTab(); // ���� �������� ��. �Ʒ� �����Ǿ� ���� ~!

    }

    public void RemoveSlot() // ���Ե��� ��� ������ �ʵ��� ..?
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem(); // ���� �� ������� �� ���̵��� 
            slots[i].gameObject.SetActive(false);
        }

    }

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

    }

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
    }

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

        }
        
    }
}
