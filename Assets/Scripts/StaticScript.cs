using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}



[System.Serializable]
public class PlayerInfo
{
    public PlayerInfo(string _nickName, int _actorNum)
    {
        nickName = _nickName;
        actorNum = _actorNum;
    }

    public string nickName;
    public int actorNum;
}