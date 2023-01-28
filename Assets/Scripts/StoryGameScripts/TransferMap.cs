using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�


    private void OnTriggerEnter2D(Collider2D collision) // �ݶ��̴��� ��� ���� ����Ǵ� �����Լ�
    {
        if(collision.gameObject.name == "Player" ) // �ڽ� �ݶ��̴��� ���� ��ü�� �̸� ��ȯ
        {
            Scene scene = SceneManager.GetActiveScene();    // ���� �� �̸� �޾ƿ�
            
            // �� ���� ���� �ǹ� ������ village������ �ش� �ǹ� �� �տ� ��ġ�ϵ��� ����
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
