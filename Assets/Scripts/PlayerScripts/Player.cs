using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    static public Player instance; // �ν��Ͻ��� ���� -> �ٸ� ��ũ��Ʈ���� ����� �� �ֵ���

    public PhotonView PV;
    public Text NickNameText;

    [SerializeField][Range(1f, 10f)] float moveSpeed = 10f; // Inspector���� ���ǵ� ���� ����
    SpriteRenderer rend; // �̹��� �¿������ ���� rend

    //bool isGround;
    Vector3 curPos;

    public bool isInAnoZone=false;
    public Transform darkPanel;
    public int OXGameCount = 0;

    public BoxCollider2D boxCollider; // �� ..
    public Animator animator; // �� ..
    
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        // �г���
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.yellow : Color.red;

        if (PV.IsMine)
        {
            // 2D ī�޶�
            var CM = GameObject.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
            
            
            //Vector3 pos = CM.transform.position);
            //if (pos.x < 94.84781f) pos.x = 94.84781f;
            /*if (pos.x > 1f) pos.x = 1f;
            if (pos.y < 0f) pos.y = 0f;
            if (pos.y > 1f) pos.y = 1f;*/
            //transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

        rend = GetComponent<SpriteRenderer>(); // �¿���� ���� rend �ʱ�ȭ
    }

    /* �¿���� ���� ��
     void Update()
     {
         if (PV.IsMine)
         {
             float posX = Input.GetAxisRaw("Horizontal");    //keybord input
             float posY = Input.GetAxisRaw("Vertical");         //keybord input

             transform.Translate(new Vector2(posX, posY) * Time.deltaTime * moveSpeed);
         }

         // IsMine�� �ƴ� �͵��� �ε巴�� ��ġ ����ȭ
         else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
         else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
     } 
    */

    // �¿���� ���� ��
    void Update() 
    {
        Vector3 flipMove = Vector3.zero; // Vector3.zero�� new Vector(0, 0, 0)�� ����

        if (PV.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal"); // �¿�

            if (x < 0) // ����
            {
                rend.flipX = false; // rend �״��
            }
            else if (x > 0) // ������
            {
                rend.flipX = true; // rend �¿����
            }

            float y = Input.GetAxisRaw("Vertical"); // ����
            flipMove = new Vector3(x, y, 0);

            transform.Translate(flipMove * Time.deltaTime * moveSpeed);
        }

        // IsMine�� �ƴ� �͵��� �ε巴�� ��ġ ����ȭ
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    //�浹ó��
    void OnTriggerEnter2D(Collider2D collider)
    {
        //�͸��� ���� �� �г��� USER�� ����, ���� ������� �ٲ�
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            if (PV.IsMine)
            {
                PV.RPC("AnoRPC", RpcTarget.All);

                GameObject.Find("MainPanel").transform.Find("Else_AnoChatZone").gameObject.SetActive(true); //��ũ�г� Ȱ��ȭ

                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(false); //��üä�� ui ��Ȱ��ȭ
                GameObject.Find("MainPanel").transform.Find("AnoChattingUI").gameObject.SetActive(true); //�͸�ä�� ui Ȱ��ȭ
            }
        }

        //������ -> ��ũ�г�
        if (collider.gameObject.name == "GameZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("�÷��̾ ���ӱ����� ����");
                GameObject.Find("MainPanel").transform.Find("Else_GameZone").gameObject.SetActive(true); //��ũ�г� Ȱ��ȭ
            }
        }

        //ox������ �� ��
        if (collider.gameObject.name == "OXGameZone")
        {

            if (PV.IsMine)
            {
                OXGameCount++;

                Debug.Log("�÷��̾ OX ���ӱ����� ����");
                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(false); //��üä�� ui ��Ȱ��ȭ
                GameObject.Find("MainPanel").transform.Find("ButtonUI").gameObject.SetActive(false); //��ư ui ��Ȱ��ȭ

                /*
                GameObject.Find("OXGameZone").transform.Find("OXGameUI").gameObject.SetActive(true); //OX ���� UI Ȱ��ȭ
                GameObject.Find("OXGameUI").transform.Find("QuestionPanel").gameObject.SetActive(true);
                */
            }

            if(OXGameCount >= 2)
            {
                PhotonNetwork.LoadLevel("OXGameScene");
            }
        }


        //������ -> ��ũ�г�
        if (collider.gameObject.name == "StudyZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("�÷��̾ ���α����� ����");
                GameObject.Find("MainPanel").transform.Find("Else_StudyZone").gameObject.SetActive(true); //��ũ�г� Ȱ��ȭ
            }
        }

        //���� -> ��ũ�г�
        if (collider.gameObject.name == "StoreZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("�÷��̾ ������ ����");
                GameObject.Find("MainPanel").transform.Find("Else_StoreZone").gameObject.SetActive(true); //��ũ�г� Ȱ��ȭ
            }
        }
    }

    //�浹ó��
    void OnTriggerExit2D(Collider2D collider)
    {
        //�͸��� Ż�� �� ���󺹱�
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            if (PV.IsMine)
            {
                PV.RPC("AnoExitRPC", RpcTarget.All);

                GameObject.Find("MainPanel").transform.Find("Else_AnoChatZone").gameObject.SetActive(false); //��ũ�г� ��Ȱ��ȭ

                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(true); //��üä�� ui Ȱ��ȭ
                GameObject.Find("MainPanel").transform.Find("AnoChattingUI").gameObject.SetActive(false); //�͸�ä�� ui ��Ȱ��ȭ
            }
        }

        //������ ����� ��ũ�г� ��Ȱ��ȭ
        if (collider.gameObject.name == "GameZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("�÷��̾ ���ӱ������� ����");
                GameObject.Find("MainPanel").transform.Find("Else_GameZone").gameObject.SetActive(false); //��ũ�г� ��Ȱ��ȭ
                
                GameObject.Find("MainPanel").transform.Find("OXGameUI").gameObject.SetActive(false); //OXGame UI ��Ȱ��ȭ
               
                GameObject.Find("MainPanel").transform.Find("ButtonUI").gameObject.SetActive(true); //��ư ui Ȱ��ȭ
                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(true); //ä�� ui Ȱ��ȭ
                
            }
        }

        //OX ������ ����� ���󺹱�
        if (collider.gameObject.name == "OXGameZone")
        {
            if (PV.IsMine)
            {
                OXGameCount--;

                Debug.Log("�÷��̾ OX ���ӱ����� ����");
                

                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(true); //��üä�� ui Ȱ��ȭ
                GameObject.Find("MainPanel").transform.Find("ButtonUI").gameObject.SetActive(true); //��ư ui Ȱ��ȭ
            }
        }


        //������ ����� ��ũ�г� ��Ȱ��ȭ
        if (collider.gameObject.name == "StudyZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("�÷��̾ ���� �������� ����");
                GameObject.Find("MainPanel").transform.Find("Else_StudyZone").gameObject.SetActive(false); //��ũ�г� ��Ȱ��ȭ
            }
        }

        //���� ����� ��ũ�г� ��Ȱ��ȭ
        if (collider.gameObject.name == "StoreZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("�÷��̾ ���� �������� ����");
                GameObject.Find("MainPanel").transform.Find("Else_StoreZone").gameObject.SetActive(false); //��ũ�г� ��Ȱ��ȭ
            }
        }
    }

    [PunRPC] 
    void AnoRPC()
    {
        Debug.Log("�͸�ä�ñ����� ��");
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("images/ghost");
        spriteR.sprite = sprites[0];
        NickNameText.text = "user";
    }

    [PunRPC]
    void AnoExitRPC()
    {
        Debug.Log("�͸�ä�ñ������� Ż��");
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("images/purple_origin");
        spriteR.sprite = sprites[0];
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)   
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }

}


