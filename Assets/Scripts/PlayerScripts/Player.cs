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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            Debug.Log("�͸�ä�ñ����� ��");
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("images/ghost");
            spriteR.sprite = sprites[0];
            NickNameText.text = "user";

            isInAnoZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            Debug.Log("�͸�ä�ñ������� Ż��");
            SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("images/player");
            spriteR.sprite = sprites[0];
            NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        }
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


