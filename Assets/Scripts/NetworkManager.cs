using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject MainPanel;
    public PhotonView PV;
    public GameObject AnoChattingUI;

    //��üä��
    public Text UserList;
    public Text[] ChatLog;
    public InputField TextInput;
    string chatters;
    string NickName;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 8 }, null);
    }

    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        AnoChattingUI.SetActive(false);
        Spawn();
    }

    
    public void Spawn()
    {
        // PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(107f, 135f), Random.Range(10f, 22f), 0), Quaternion.identity);
        MainPanel.SetActive(true);

    }

    //ä�� ������ ��ư
    public void Send()
    {
        string msg = PhotonNetwork.NickName + " : " + TextInput.text;
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + TextInput.text);
        TextInput.text = "";
    }

    //�͸� ä�� ������ ��ư
    public void AnoSend()
    {
        NickName = PhotonNetwork.LocalPlayer.NickName;
        PhotonNetwork.LocalPlayer.NickName = "User";

        string msg = PhotonNetwork.NickName + " : " + TextInput.text;
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + TextInput.text);
        TextInput.text = "";
        PhotonNetwork.LocalPlayer.NickName = NickName;
    }

    //ä�� ����
    [PunRPC] // RPC�� �÷��̾ �����ִ� �� ��� �ο����� �����Ѵ�
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatLog.Length; i++)
        {
            if (ChatLog[i].text == "")
            {
                isInput = true;
                ChatLog[i].text = msg;
                break;
            }
        }
        if (!isInput) // ������ ��ĭ�� ���� �ø�
        {
            for (int i = 1; i < ChatLog.Length; i++) ChatLog[i - 1].text = ChatLog[i].text;
            ChatLog[ChatLog.Length - 1].text = msg;
        }
    }


    //user list
    void chatterUpdate()
    {
        chatters = "Player List\n";
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            chatters += player.NickName + "\n";
        }
        UserList.text = chatters;
    }


    void Update() 
    {
        chatterUpdate();
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) PhotonNetwork.Disconnect(); 
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        MainPanel.SetActive(false);
    }
}
