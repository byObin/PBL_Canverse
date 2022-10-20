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
    }

    void Update()
    {
        if (PV.IsMine)
        {
            float posX = Input.GetAxisRaw("Horizontal");    //keybord input
            float posY = Input.GetAxisRaw("Vertical");         //keybord input

            transform.Translate(new Vector2(posX, posY) * Time.deltaTime * speed);
        }
        // IsMine�� �ƴ� �͵��� �ε巴�� ��ġ ����ȭ
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    //�͸��� ���� �� �г��� USER�� ����, ���� ������� �ٲ�
    void OnTriggerEnter2D(Collider2D collider)
    {
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
    }

    //�͸��� Ż�� �� ���󺹱�
    void OnTriggerExit2D(Collider2D collider)
    {
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


