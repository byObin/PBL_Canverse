using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using static Singleton;
using ExitGames.Client.Photon;
using Player = Photon.Realtime.Player;


public class MultiManager : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public List<PlayerInfo> playerInfos;
    public bool isStart, isEnd;




    void MasterInitPlayerInfo()
    {
        // 게임시작시 초기화
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Photon.Realtime.Player player = PhotonNetwork.PlayerList[i];
            playerInfos.Add(new PlayerInfo(player.NickName, player.ActorNumber));
        }
        MasterSendPlayerInfo(INIT);
    }

    void MasterRemovePlayerInfo(int actorNum)
    {
        // OnPlayerLeftRoom으로 방을 나갈경우 플레이어 제거
        PlayerInfo playerInfo = playerInfos.Find(x => x.actorNum == actorNum);
        playerInfos.Remove(playerInfo);
        MasterSendPlayerInfo(REMOVE);
    }

    [PunRPC]
    public void MasterReceiveRPC(byte code, int actorNum, int colActorNum)
    {
        MasterSendPlayerInfo(code);
    }



    void MasterSendPlayerInfo(byte code)
    {
        // 방장은 PlayerInfo 정렬 후 보내기
        // playerInfos.Sort((p1, p2) => p2.lifeTime.CompareTo(p1.lifeTime));

        string jdata = JsonUtility.ToJson(new Serialization<PlayerInfo>(playerInfos));
        PV.RPC("OtherReceivePlayerInfoRPC", RpcTarget.Others, code, jdata);
    }

    [PunRPC]
    void OtherReceivePlayerInfoRPC(byte code, string jdata)
    {
        // 다른 사람은 PlayerInfo 받기
        playerInfos = JsonUtility.FromJson<Serialization<PlayerInfo>>(jdata).target;
    }

    [PunRPC]
    void StartSyncRPC()
    {
        isStart = true;
    }




    IEnumerator Loading()
    {
        S.SetTag("loadScene", true);
        while (!S.AllhasTag("loadScene")) yield return null;

        // 모두 씬에 있어야 생성할 수 있음, 에디터와 클라는 에디터가 마스터
        //PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-5f, 5f), 0, 0), QI);
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), QI);

        while (!S.AllhasTag("loadPlayer")) yield return null;
    }

    IEnumerator Start()
    {
        yield return Loading();

        if (S.master())
        {
            MasterInitPlayerInfo();
            yield return new WaitForSeconds(3);
            PV.RPC("StartSyncRPC", RpcTarget.AllViaServer);
        }
    }

    /*
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // 마스터가 나가면 바뀐 마스터가 호출되서 성공
        if (S.master())
        {
            MasterRemovePlayerInfo(otherPlayer.ActorNumber);
        }
    }
    */
}