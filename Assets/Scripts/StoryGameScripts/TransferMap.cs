using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) // �ݶ��̴��� ��� ���� ����Ǵ� �����Լ�
    {
        if(collision.gameObject.name == "Player" ) // �ڽ� �ݶ��̴��� ���� ��ü�� �̸� ��ȯ
        {
            SceneManager.LoadScene(transferMapName);

        }
        
    }

}
