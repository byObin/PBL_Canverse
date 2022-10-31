using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView PV;
    public Text NickNameText;

    [SerializeField][Range(1f, 10f)] float moveSpeed = 5f; // Inspector에서 스피드 조절 가능
    SpriteRenderer rend; // 이미지 좌우반전용 변수 rend

    //bool isGround;
    Vector3 curPos;

    public bool isInAnoZone=false;
    public Transform darkPanel;
    
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

    //익명존 입장 시 닉네임 USER로 변경, 유령 모습으로 바뀜
    void OnTriggerEnter2D(Collider2D collider)
    {
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
    }

    //익명존 탈출 시 원상복구
    void OnTriggerExit2D(Collider2D collider)
    {
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
        Sprite[] sprites = Resources.LoadAll<Sprite>("images/player");
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


