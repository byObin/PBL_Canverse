using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Doorlock : MonoBehaviour
{
    public InputField Password_InputField;
    public Button Password_Button;
    public GameObject player;

    private string password = "1234"; // ���� ����

    // Start is called before the first frame update
    public void Password_Button_Click()
    {
        if (Password_InputField.text == password)
        {
            Debug.Log("ģ�� �� ���η� �̵�");
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(false);
            SceneManager.LoadScene("Friend'sHouse");
            player.transform.position = new Vector3(3.519f, -18.3f, 0);


        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�. �ٽ� �õ��ϼ���.");
        }
        
        


        
    }

    // Update is called once per frame
 
}
