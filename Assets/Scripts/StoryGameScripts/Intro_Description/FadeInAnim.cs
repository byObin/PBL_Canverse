using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAnim : MonoBehaviour
{
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if(time < 3f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 3);
        }
        else // 3초가 지나면 자동으로 꺼짐
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.SetActive(true);
        time = 0;
    }
}
