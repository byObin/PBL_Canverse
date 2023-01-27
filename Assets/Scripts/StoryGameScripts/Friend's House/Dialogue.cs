using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue // 커스텀 클래스
{
    [TextArea(1, 2)]
    public string[] sentences; // 문장을 배열로 만듦. 여러 문장이니까.
    public Sprite[] sprites; // 우린 필요 없을 듯
    public Sprite[] dialogueWindows;
    
}
