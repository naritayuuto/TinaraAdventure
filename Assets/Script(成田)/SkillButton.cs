using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    PlayerController player = null;
    [SerializeField]
    SkillId skillId = SkillId.heal;
    /// <summary>配列番号</summary>
    [SerializeField]
    int arrayNumber = 0;
    /// <summary>何系統の何番目のスキルなのか</summary>
    [SerializeField]
    int skillnumber = 0;
    /// <summary>解放するために必要なポイント</summary>
    [SerializeField]
    int skillpoint = 0;

    SkillManager _skillManager = null;

    SkillTree1 skilltree = null;

    Button button = null;

    /// <summary>スキルが解放されているか</summary>
    bool skillcheck = false;
    public int Skillnumber { get => skillnumber; set => skillnumber = value; }

    public int ArrayNumber { get => arrayNumber; set => arrayNumber = value; }
    public bool Skillcheck { get => skillcheck; set => skillcheck = value; }
    public SkillId SkillId { get => skillId; set => skillId = value; }
    public int Skillpoint { get => skillpoint; set => skillpoint = value; }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        skilltree = gameObject.GetComponent<SkillTree1>();//自分が持っているスキルツリーを持ってくる。
        _skillManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
        button = GetComponent<Button>();
    }

    public void OnCllik()
    {
        skilltree.SkillPointJudge(skillpoint,arrayNumber);
    }

    public void Yobidasi(ISkill skill)
    {
       if(skillcheck)
        {
            Debug.Log(skillId + "の" + skillnumber + "番目呼ばれました");
            player.AddSkill(skill);
            _skillManager.SkillPoint -= skillpoint;
            _skillManager.SkillActive[arrayNumber] = true;
            button.interactable = false;
        }
       else
        {
            Debug.Log("まだ開放されていません");
        }
    }
}
