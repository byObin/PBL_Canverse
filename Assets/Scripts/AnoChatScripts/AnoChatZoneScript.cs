using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class AnoChatZoneScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    public GameObject darkPanel;
    public GameObject ChattingUI;
    public GameObject AnoChattingUI;


    
    [PunRPC]
    void AnoPanelRPC()
    {
        Debug.Log("�÷��̾ �͸�ä�ñ����� ����");
        darkPanel.SetActive(true);

        ChattingUI.SetActive(false);    //��üä�� ui ��
        AnoChattingUI.SetActive(true);    //�͸�ä�� ui ��
    }

    [PunRPC]
    void AnoExitPanelRPC()
    {
        Debug.Log("�÷��̾ �͸�ä�ñ������� ����");
        darkPanel.SetActive(false);

        ChattingUI.SetActive(true);    //��üä�� ui ��
        AnoChattingUI.SetActive(false);    //�͸�ä�� ui ��
    }

    
    /*
    //�͸��� ���� �� ��ũ�г� Ȱ��ȭ, �͸� ä�� ui ����
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            if (PV.IsMine) PV.RPC("AnoPanelRPC", RpcTarget.All);
        }
    }

    //�͸��� Ż�� �� ��ũ�г� ��Ȱ��ȭ, ��ü ä�� ui ����
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "AnonymousChatZone")
        {
            if (PV.IsMine) PV.RPC("AnoExitPanelRPC", RpcTarget.All);
        }
    }

 */
  
}
