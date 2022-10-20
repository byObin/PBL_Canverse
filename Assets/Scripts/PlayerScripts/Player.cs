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
    public float speed = 5;

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
    }

    void Update()
    {
        if (PV.IsMine)
        {
            float posX = Input.GetAxisRaw("Horizontal");    //keybord input
            float posY = Input.GetAxisRaw("Vertical");         //keybord input

            transform.Translate(new Vector2(posX, posY) * Time.deltaTime * speed);
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


