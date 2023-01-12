using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_Player : MonoBehaviour
{

    public float speed = 5;

    void Update()   // Ű���� ����Ű�� ���� �÷��̾� �̵�
    {
        float posX = Input.GetAxisRaw("Horizontal");    //keybord input
        float posY = Input.GetAxisRaw("Vertical");         //keybord input

        transform.Translate(new Vector2(posX, posY) * Time.deltaTime * speed);
    }


    //�浹ó�� : �÷��̾ �ǹ� ���� �浹 ��
    void OnTriggerEnter2D(Collider2D collider)
    {

        //������ �� �浹 �� Library Scene���� �̵�
        if (collider.gameObject.name == "LibDoor")
        {
             Debug.Log("������ ���� �浹");
             SceneManager.LoadScene("Library");
        }

        //�ڹ��� �� �浹 �� Museum Scene���� �̵�
        if (collider.gameObject.name == "MusDoor")
        {
            Debug.Log("�ڹ��� ���� �浹");
            SceneManager.LoadScene("Museum");
        }

        //���繫�� �� �浹 �� TownOffice Scene���� �̵�
        if (collider.gameObject.name == "OffiDoor")
        {
            Debug.Log("���繫�� ���� �浹");
            SceneManager.LoadScene("TownOffice");
        }

        //ģ�� �� �� �浹 �� Friend'sHouse Scene���� �̵�
        if (collider.gameObject.name == "FirDoor")
        {
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(true);





            // Debug.Log("ģ�� �� ���� �浹");
            // SceneManager.LoadScene("Friend'sHouse");
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "FirDoor") // 다시 친구 집 문에 부딪히면 도어락 패널 비활성화
        {
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(false);
        }
    }


}
