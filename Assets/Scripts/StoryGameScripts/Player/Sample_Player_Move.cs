using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sample_Player_Move : MonoBehaviour
{
    static public Sample_Player_Move instance; // �ڱ� �ڽ��� ������ �޴� �ν��Ͻ�

    public float moveSpeed = 5;
    private Animator anim;
    private BoxCollider2D boxCollider; // �߰�

    public bool didPlayerReadFirstMsg = false;
    public bool didPlayerTalkNPC = false;

    public bool mapIsClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) // �ν��Ͻ��� ó�� ������ ����
        {
            DontDestroyOnLoad(this.gameObject); // �� ��ȯ �� ������� �ʵ���
            boxCollider = GetComponent<BoxCollider2D>();
            anim = GetComponent<Animator>();
            instance = this;
        }
        else // �� ������ �����Ǵ� �÷��̾�� �ı�
            Destroy(this.gameObject);
        
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
                    mapIsClicked = true;
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
            gameObject.transform.position = new Vector3(28.3f, -11.77f, -6);
        }

        //Museum Scene
        if (collider.gameObject.name == "MusDoor")
        {
            SceneManager.LoadScene("Museum");
            gameObject.transform.position = new Vector3(2.326f, -34.8f, 0);

        }

        //TownOffice Scene
        if (collider.gameObject.name == "OffiDoor")
        {
            SceneManager.LoadScene("TownOffice");
            gameObject.transform.position = new Vector3(-9.58f, -11.57f, 0);

        }

        //Friend'sHouse Scene
        if (collider.gameObject.name == "FirDoor")
        {
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(true);

        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "FirDoor") // �ٽ� ģ�� �� ���� �ε����� ����� �г� ��Ȱ��ȭ
        {
            GameObject.Find("Friend'sHouse").transform.Find("Canvas").transform.Find("DoorlockPanel").gameObject.SetActive(false);
        }
    }

}
