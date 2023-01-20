using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sample_Player_Move : MonoBehaviour
{
    public float moveSpeed = 5;
    private Animator anim;
    public GameObject mapPanel;
    private BoxCollider2D boxCollider; // 추가


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // 추가
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>(); // 추가
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        }

        if (Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));

        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                Debug.Log("touched Object is " + touchedObject);
                if (touchedObject.name == "MapItem")
                {
                    mapPanel.SetActive(true);
                }
            }

        }

    }

  
    void OnTriggerEnter2D(Collider2D collider)
    {

        //Library Scene
        if (collider.gameObject.name == "LibDoor")
        {
            SceneManager.LoadScene("Library");
        }

        //Museum Scene
        if (collider.gameObject.name == "MusDoor")
        {
            SceneManager.LoadScene("Museum");
        }

        //TownOffice Scene
        if (collider.gameObject.name == "OffiDoor")
        {
            SceneManager.LoadScene("TownOffice");
        }

        //Friend'sHouse Scene
        if (collider.gameObject.name == "FirDoor")
        {
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(true);

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
