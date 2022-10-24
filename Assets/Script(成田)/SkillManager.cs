using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ここではskillbuttonにそれぞれ何のスキルを持つのか割り振りする。
public class SkillManager : MonoBehaviour//skillを管理する。目次のようなもので何が解放されているのかの情報を持っている。
{
    [SerializeField]
    SkillButton[] buttons;//SkillButtonが持つskillnumberを記録するために使用する。_skillActiveの要素番号に結びつけ。

    bool[] _skillActive;//直接的にいじることはない

    List<ISkill> _skill = new List<ISkill>();

    float skillPoint = 0f;

    ///<summary>healスキルの数</summary>
    private int healcount = 1;
    ///<summary>attackスキルの数</summary>
    private int attackcount = 2;
    ///<summary>buffスキルの数</summary>
    private int buffcount = 3;
    public float SkillPoint { get => skillPoint; set => skillPoint = value; }
    public bool[] SkillActive { get => _skillActive; set => _skillActive = value; }
    public SkillButton[] Buttons { get => buttons;}

    private void Start()
    {
        _skillActive = new bool[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < healcount)//healのスキル
            {
                buttons[i].Skillnumber = i + 1;
                buttons[i].SkillId = SkillId.heal;
            }
            else if (i < attackcount + healcount)//attackのスキル
            {
                buttons[i].Skillnumber = i - healcount + 1;
                buttons[i].SkillId = SkillId.attack;
            }
            else//buffのスキル
            {
                buttons[i].Skillnumber = i - attackcount;
                buttons[i].SkillId = SkillId.buff;
            }
            buttons[i].ArrayNumber = i;
            buttons[i].Skillpoint += 2 * buttons[i].Skillnumber;
        }
    }

    private void Update()
    {
        
    }
}
