using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class museumPlayer : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "exitDoor")
        {
            Debug.Log("마을로 이동");
            SceneManager.LoadScene("Village");
        }
    }

}
