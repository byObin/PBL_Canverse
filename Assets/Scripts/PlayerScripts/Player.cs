using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    static public Player instance; // 인스턴스로 만듦 -> 다른 스크립트에서 사용할 수 있도록

    public PhotonView PV;
    public Text NickNameText;

    [SerializeField][Range(1f, 10f)] float moveSpeed = 10f; // Inspector에서 스피드 조절 가능
    SpriteRenderer rend; // 이미지 좌우반전용 변수 rend

    //bool isGround;
    Vector3 curPos;

    public bool isInAnoZone=false;
    public Transform darkPanel;
    public int OXGameCount = 0;

    public BoxCollider2D boxCollider; // 음 ..
    public Animator animator; // 음 ..
    
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
        // 닉네임
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.yellow : Color.red;

        if (PV.IsMine)
        {
            // 2D 카메라
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

        rend = GetComponent<SpriteRenderer>(); // 좌우반전 변수 rend 초기화
    }

    /* 좌우반전 적용 전
     void Update()
     {
         if (PV.IsMine)
         {
             float posX = Input.GetAxisRaw("Horizontal");    //keybord input
             float posY = Input.GetAxisRaw("Vertical");         //keybord input

             transform.Translate(new Vector2(posX, posY) * Time.deltaTime * moveSpeed);
         }

         // IsMine이 아닌 것들은 부드럽게 위치 동기화
         else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
         else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
     } 
    */

    // 좌우반전 적용 후
    void Update() 
    {
        Vector3 flipMove = Vector3.zero; // Vector3.zero는 new Vector(0, 0, 0)과 동일

        if (PV.IsMine)
        {
            float x = Input.GetAxisRaw("Horizontal"); // 좌우

            if (x < 0) // 왼쪽
            {
                rend.flipX = false; // rend 그대로
            }
            else if (x > 0) // 오른쪽
            {
                rend.flipX = true; // rend 좌우반전
            }

            float y = Input.GetAxisRaw("Vertical"); // 상하
            flipMove = new Vector3(x, y, 0);

            transform.Translate(flipMove * Time.deltaTime * moveSpeed);
        }

        // IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    //충돌처리
    void OnTriggerEnter2D(Collider2D collider)
    {
        //익명존 입장 시 닉네임 USER로 변경, 유령 모습으로 바뀜
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            if (PV.IsMine)
            {
                PV.RPC("AnoRPC", RpcTarget.All);

                GameObject.Find("MainPanel").transform.Find("Else_AnoChatZone").gameObject.SetActive(true); //다크패널 활성화

                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(false); //전체채팅 ui 비활성화
                GameObject.Find("MainPanel").transform.Find("AnoChattingUI").gameObject.SetActive(true); //익명채팅 ui 활성화
            }
        }

        //게임존 -> 다크패널
        if (collider.gameObject.name == "GameZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("플레이어가 게임구역에 들어옴");
                GameObject.Find("MainPanel").transform.Find("Else_GameZone").gameObject.SetActive(true); //다크패널 활성화
            }
        }

        //ox게임존 들어갈 때
        if (collider.gameObject.name == "OXGameZone")
        {

            if (PV.IsMine)
            {
                OXGameCount++;

                Debug.Log("플레이어가 OX 게임구역에 들어옴");
                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(false); //전체채팅 ui 비활성화
                GameObject.Find("MainPanel").transform.Find("ButtonUI").gameObject.SetActive(false); //버튼 ui 비활성화

                /*
                GameObject.Find("OXGameZone").transform.Find("OXGameUI").gameObject.SetActive(true); //OX 게임 UI 활성화
                GameObject.Find("OXGameUI").transform.Find("QuestionPanel").gameObject.SetActive(true);
                */
            }

            if(OXGameCount >= 2)
            {
                PhotonNetwork.LoadLevel("OXGameScene");
            }
        }


        //공부존 -> 다크패널
        if (collider.gameObject.name == "StudyZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("플레이어가 공부구역에 들어옴");
                GameObject.Find("MainPanel").transform.Find("Else_StudyZone").gameObject.SetActive(true); //다크패널 활성화
            }
        }

        //상점 -> 다크패널
        if (collider.gameObject.name == "StoreZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("플레이어가 상점에 들어옴");
                GameObject.Find("MainPanel").transform.Find("Else_StoreZone").gameObject.SetActive(true); //다크패널 활성화
            }
        }
    }

    //충돌처리
    void OnTriggerExit2D(Collider2D collider)
    {
        //익명존 탈출 시 원상복구
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            if (PV.IsMine)
            {
                PV.RPC("AnoExitRPC", RpcTarget.All);

                GameObject.Find("MainPanel").transform.Find("Else_AnoChatZone").gameObject.SetActive(false); //다크패널 비활성화

                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(true); //전체채팅 ui 활성화
                GameObject.Find("MainPanel").transform.Find("AnoChattingUI").gameObject.SetActive(false); //익명채팅 ui 비활성화
            }
        }

        //게임존 벗어나면 다크패널 비활성화
        if (collider.gameObject.name == "GameZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("플레이어가 게임구역에서 나감");
                GameObject.Find("MainPanel").transform.Find("Else_GameZone").gameObject.SetActive(false); //다크패널 비활성화
                
                GameObject.Find("MainPanel").transform.Find("OXGameUI").gameObject.SetActive(false); //OXGame UI 비활성화
               
                GameObject.Find("MainPanel").transform.Find("ButtonUI").gameObject.SetActive(true); //버튼 ui 활성화
                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(true); //채팅 ui 활성화
                
            }
        }

        //OX 게임존 벗어나면 원상복구
        if (collider.gameObject.name == "OXGameZone")
        {
            if (PV.IsMine)
            {
                OXGameCount--;

                Debug.Log("플레이어가 OX 게임구역을 나감");
                

                GameObject.Find("MainPanel").transform.Find("ChattingUI").gameObject.SetActive(true); //전체채팅 ui 활성화
                GameObject.Find("MainPanel").transform.Find("ButtonUI").gameObject.SetActive(true); //버튼 ui 활성화
            }
        }


        //공부존 벗어나면 다크패널 비활성화
        if (collider.gameObject.name == "StudyZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("플레이어가 공부 구역에서 나감");
                GameObject.Find("MainPanel").transform.Find("Else_StudyZone").gameObject.SetActive(false); //다크패널 비활성화
            }
        }

        //상점 벗어나면 다크패널 비활성화
        if (collider.gameObject.name == "StoreZone")
        {
            if (PV.IsMine)
            {
                Debug.Log("플레이어가 상점 구역에서 나감");
                GameObject.Find("MainPanel").transform.Find("Else_StoreZone").gameObject.SetActive(false); //다크패널 비활성화
            }
        }
    }

    [PunRPC] 
    void AnoRPC()
    {
        Debug.Log("익명채팅구역에 들어감");
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("images/ghost");
        spriteR.sprite = sprites[0];
        NickNameText.text = "user";
    }

    [PunRPC]
    void AnoExitRPC()
    {
        Debug.Log("익명채팅구역에서 탈출");
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


