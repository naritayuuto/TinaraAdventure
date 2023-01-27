using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ここではskillbuttonにそれぞれ何のスキルを持つのか割り振りする。
public class SkillManager : MonoBehaviour//skillを管理する。目次のようなもので何が解放されているのかの情報を持っている。
{
    [SerializeField]
    SkillButton[] buttons;//SkillButtonが持つarraynumberを記録するために使用する。_skillActiveの要素番号に結びつけ。

    bool[] _skillActive;//直接的にいじることはない

    List<SkillTree> skillTree = new List<SkillTree>();
    [Tooltip("スキルポイントを入れる変数")]
    float _skillPoint = 0f;
    [Tooltip("敵を攻撃したときに加算する値")]
    float _attackSkillPoint = 0.5f;
    [Tooltip("敵を倒したときに加算する値")]
    float _defeatSkillPoint = 5f;
    
    public float SkillPoint { get => _skillPoint; set => _skillPoint = value; }
    public bool[] SkillActive { get => _skillActive; set => _skillActive = value; }
    public SkillButton[] Buttons { get => buttons;}

    private void Awake()
    {
        _skillActive = new bool[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].ArrayNumber = i;
            //buttons[i].Skillpoint += 2 * buttons[i].ArrayNumber;
        }
    }
    public void AddSkillPoint(bool enemyDie)
    {
        _skillPoint = enemyDie == true ? _skillPoint + _attackSkillPoint : _skillPoint + _defeatSkillPoint;
    }
}
