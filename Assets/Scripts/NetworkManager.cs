using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;

    //패널
    public GameObject DisconnectPanel;
    public GameObject MainPanel;

    //ui
    public GameObject AnoChattingUI;
    public GameObject ButtonUI;

    public GameObject OXGameUI;
    public GameObject GoPanel;
    public GameObject Inventory;

    //darkPanel
    public GameObject Else_GameZone;
    public GameObject Else_StudyZone;
    public GameObject Else_AnoChatZone;
    public GameObject Else_StoreZone;

    //tag
    public GameObject darkPanel;
    public GameObject ChattingUI;

    public PhotonView PV;

    //전체채팅
    public Text UserList;
    public Text[] ChatLog;
    public InputField TextInput;
    string chatters;
    string NickName;

    //익명채팅
    public Text[] AnoChatLog;
    public InputField AnoTextInput;

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
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 5 }, null);
    }

    //시작할 때
    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        AnoChattingUI.SetActive(false);

        Inventory.SetActive(false); 
        OXGameUI.SetActive(false);
        ButtonUI.SetActive(true);
        Spawn();
    }

    
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(117f, 145f), Random.Range(13f, 25f), 0), Quaternion.identity);
        MainPanel.SetActive(true);

        //darkPanel 비활성화
        Else_GameZone.SetActive(false);
        Else_StudyZone.SetActive(false);
        Else_AnoChatZone.SetActive(false);
        Else_StoreZone.SetActive(false);
    }

    public void ExitOXGame()
    {
        OXGameUI.SetActive(false);
        GoPanel.SetActive(false);

        ButtonUI.SetActive(true);
        ChattingUI.SetActive(true);
    }

    //전체 채팅 보내기 버튼
    public void Send()
    {
        string msg = PhotonNetwork.NickName + " : " + TextInput.text;
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + TextInput.text);
        TextInput.text = "";
    }

    //전체 채팅 구현
    [PunRPC] 
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
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatLog.Length; i++) ChatLog[i - 1].text = ChatLog[i].text;
            ChatLog[ChatLog.Length - 1].text = msg;
        }
    }

    //익명 채팅 보내기 버튼
    public void AnoSend()
    {
        NickName = PhotonNetwork.LocalPlayer.NickName;  //원래 닉네임 저장
        PhotonNetwork.LocalPlayer.NickName = "User";    //닉네임 user로 변경

        string msg = PhotonNetwork.NickName + " : " + AnoTextInput.text;
        PV.RPC("AnoChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + AnoTextInput.text);
        AnoTextInput.text = "";
        PhotonNetwork.LocalPlayer.NickName = NickName;
    }

    //익명 채팅 구현
    [PunRPC] 
    void AnoChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < AnoChatLog.Length; i++)
        {
            if (AnoChatLog[i].text == "")
            {
                isInput = true;
                AnoChatLog[i].text = msg;
                break;
            }
        }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < AnoChatLog.Length; i++) AnoChatLog[i - 1].text = AnoChatLog[i].text;
            AnoChatLog[AnoChatLog.Length - 1].text = msg;
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

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void ShowInventory()
    {
        Inventory.SetActive(true);
    }

    public void CloseInventory()
    {
        Inventory.SetActive(false);
    }

    //접속 끊음
    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

 
}
