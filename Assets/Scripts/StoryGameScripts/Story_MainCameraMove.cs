using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_MainCameraMove : MonoBehaviour
{
    static public Story_MainCameraMove instance;

    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    public float offsetX;
    public float offsetY;
    public float offsetZ = -10.0f;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); // ���� ������ ��ü�� �ı�
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;

        }
        
    }

    void Update()
    {
        // DontDestroyOnLoad(this.gameObject);
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        
        /* ����
        Vector3 FixedPos =
            new Vector3(
                target.transform.position.x + offsetX,
                target.transform.position.y + offsetY,
                target.transform.position.z + offsetZ
                );
        transform.position = FixedPos; */
    }

}
