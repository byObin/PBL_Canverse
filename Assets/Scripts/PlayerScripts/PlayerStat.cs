using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int point;
    public int currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentPoint = point;
        
    }

    public void Point(int _point)
    {
        point += _point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
