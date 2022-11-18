using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using Player = Photon.Realtime.Player;


public class Singleton : MonoBehaviourPunCallbacks
{
	public const byte INIT = 0, REMOVE = 1;
	public static readonly Quaternion QI = Quaternion.identity;

	public PhotonView PV;

	#region ½Ì±ÛÅæ
	public static Singleton S;
	void Awake()
	{
		if (null == S)
		{
			S = this;
			DontDestroyOnLoad(this);
		}
		else Destroy(this);
	}
	#endregion



	#region Set Get
	public bool master() => PhotonNetwork.LocalPlayer.IsMasterClient;

	public int actorNum(Photon.Realtime.Player player = null)
	{
		if (player == null) player = PhotonNetwork.LocalPlayer;
		return player.ActorNumber;
	}

	public void destroy(List<GameObject> GO)
	{
		for (int i = 0; i < GO.Count; i++) PhotonNetwork.Destroy(GO[i]);
	}

	public void SetPos(Transform Tr, Vector3 target)
	{
		Tr.position = target;
	}

	public void SetTag(string key, object value, Photon.Realtime.Player player = null)
	{
		if (player == null) player = PhotonNetwork.LocalPlayer;
		player.SetCustomProperties(new Hashtable { { key, value } });
	}

	public object GetTag(Photon.Realtime.Player player, string key)
	{
		if (player.CustomProperties[key] == null) return null;
		return player.CustomProperties[key].ToString();
	}

	public bool AllhasTag(string key)
	{
		for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
			if (PhotonNetwork.PlayerList[i].CustomProperties[key] == null) return false;
		return true;
	}
	#endregion



	void Setting()
	{
		Screen.SetResolution(1920, 1080, false);
		//PhotonNetwork.NickName = "ÇÃ·¹ÀÌ¾î" + Random.Range(0, 100);
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.SendRate = 40;
		PhotonNetwork.SerializationRate = 20;
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	void Start()
	{
		Setting();
	}


	/*
	void OnGUI()
	{
		GUI.skin.label.fontSize = 30;
		GUI.skin.button.fontSize = 30;
		GUILayout.BeginVertical("Box", GUILayout.Width(400), GUILayout.MinHeight(400));


		GUILayout.Label("¼­¹ö½Ã°£ : " + PhotonNetwork.Time);
		GUILayout.Label("»óÅÂ : " + PhotonNetwork.NetworkClientState);
		GUILayout.Label("¾À : " + SceneManager.GetActiveScene().name);


		if (PhotonNetwork.IsConnected)
		{
			if (GUILayout.Button("¿¬°á²÷±â"))
			{

				PhotonNetwork.Disconnect();

			}
		}
		else
		{
			if (GUILayout.Button("Á¢¼Ó"))
			{

				PhotonNetwork.ConnectUsingSettings();

			}
		}


		if (PhotonNetwork.InRoom && master())
		{
			if (GUILayout.Button("·Îºñ ¾À"))
			{
				if (PV.IsMine)
				{
					PhotonNetwork.LoadLevel("SampleScene");
				}
			}
			if (GUILayout.Button("°ÔÀÓ ¾À"))
			{
				if (PV.IsMine)
				{
					PhotonNetwork.LoadLevel("GameScene");
				}
			}
		}
		GUILayout.EndVertical();
	}
	*/
}