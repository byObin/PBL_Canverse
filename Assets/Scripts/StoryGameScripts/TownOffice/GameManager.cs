/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Talkmanager talkmanager;
    public QuestManager questmanager;
    public Animator talkPanel;
    public Animantor portraitAnim;
    public Image portraitImg;
    public Sprite prevPortrait;
    public Text nametext;
    public TypeEffect talk;
    public GameObject menuSet;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            menuset.SetActive(true);
    }
    
}
*/