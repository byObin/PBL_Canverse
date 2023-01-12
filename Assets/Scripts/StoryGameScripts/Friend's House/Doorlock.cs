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

    private string password = "1234"; // 임의 변수

    // Start is called before the first frame update
    public void Password_Button_Click()
    {
        if (Password_InputField.text == password)
        {
            Debug.Log("친구 집 내부로 이동");
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(false);
            SceneManager.LoadScene("Friend'sHouse");


        }
        else
        {
            Debug.Log("틀렸습니다. 다시 시도하세요.");
        }
        
        


        
    }

    // Update is called once per frame
 
}
