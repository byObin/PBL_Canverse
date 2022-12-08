using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportUserBtn : MonoBehaviour
{
    public GameObject reportUserPanel;

    public void OnClick()
    {
        //신고 사유 버튼 클릭 시 신고 패널 비활성화
        reportUserPanel.SetActive(false);
    }
}
