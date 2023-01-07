using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_MainCameraMove : MonoBehaviour
{
    public GameObject target;

    public float offsetX;
    public float offsetY;
    public float offsetZ = -10.0f;

    void Update()
    {
        Vector3 FixedPos =
            new Vector3(
                target.transform.position.x + offsetX,
                target.transform.position.y + offsetY,
                target.transform.position.z + offsetZ
                );
        transform.position = FixedPos;
    }

}
