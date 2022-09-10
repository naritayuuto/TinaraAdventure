using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skilltree : MonoBehaviour
{
    ///<summary>skillnumberと数が同じ</summary>
    [SerializeField] SkillButton[] skillButton;
    PlayerController player = null;
    int count = 0;
    /// <summary>何番目のスキルが解放されているのかを管理している</summary>
    bool[] skillActive;
    /// <summary>スキルを解放するためのpoint、通常攻撃で敵を攻撃した場合溜める</summary>
    private float skillpoint = 0f;
    public SkillButton[] SkillButton { get => skillButton; set => skillButton = value; }
    public bool[] SkillActive { get => skillActive; set => skillActive = value; }
    public float Skillpoint { get => skillpoint; set => skillpoint = value; }
    public PlayerController Player { get => player;}

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        skillActive = new bool[skillButton.Length];
        for (int i = 0; i < skillButton.Length; i++)
        {
            skillButton[i].Skillnumber = i+1;
            skillButton[i].SkillId = (skillId)i+1;
            skillActive[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void SkillJudge(int num)
    {
        if(2 < num)//skillButton[3]以上だったら
        {
            for(int i = 0; i < num; i++)
            {
                if (skillButton[i].Skillcheck)
                {
                    count++;
                }
            }
            if(2 <= count)
            {//解放されているか確認
                skillButton[num].Skillcheck = true;
                skillActive[num] = true;
            }
        }
        else
        {//解放されているか確認
            skillButton[num].Skillcheck = true;
            skillActive[num] = true;
        }
        skillButton[num].Yobidasi();
    }
    public virtual void SkillAction(){}
}
