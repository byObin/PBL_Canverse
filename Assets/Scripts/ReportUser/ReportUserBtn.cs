using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportUserBtn : MonoBehaviour
{
    public GameObject reportUserPanel;

    public void OnClick()
    {
        //�Ű� ���� ��ư Ŭ�� �� �Ű� �г� ��Ȱ��ȭ
        reportUserPanel.SetActive(false);
    }
}
