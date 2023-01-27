using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue // Ŀ���� Ŭ����
{
    [TextArea(1, 2)]
    public string[] sentences; // ������ �迭�� ����. ���� �����̴ϱ�.
    public Sprite[] sprites; // �츰 �ʿ� ���� ��
    public Sprite[] dialogueWindows;
    
}
