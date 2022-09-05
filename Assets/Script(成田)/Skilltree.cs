using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skilltree : MonoBehaviour
{
    ///<summary>skillnumber‚Æ”‚ª“¯‚¶</summary>
    [SerializeField]
    SkillButton[] skillButton;
    int count = 0;
    bool[] skillnumber;
    public SkillButton[] SkillButton { get => skillButton; set => skillButton = value; }
    public bool[] Skillnumber { get => skillnumber; set => skillnumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skillButton.Length; i++)
        {
            skillButton[i].Skillnumber = i;
            skillnumber[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void SkillJudge(int num)
    {
        if(2 < num)//skillButton[3]ˆÈã‚¾‚Á‚½‚ç
        {
            for(int i = 0; i < num; i++)
            {
                if (skillButton[i].Skillcheck)
                {
                    count++;
                }
            }
            if(2 <= count)
            {
                skillButton[num].Skillcheck = true;
                skillnumber[num] = true;
            }
        }
        else
        {
            skillButton[num].Skillcheck = true;
            skillnumber[num] = true;
        }
        skillButton[num].Yobidasi();
    }
}
