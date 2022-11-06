using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillId
{
    None,
    heal,
    attack,
    buff
}
public class SkillTree1 : MonoBehaviour
{
    [SerializeField]
    SkillTree1 _parent = null;//一つ上。

    List<SkillTree1> _childs = new List<SkillTree1>();//自分自身の下に付いている子供たち
    [SerializeField, SerializeReference, SubclassSelector]
    ISkill _skill = null;//最初から持っておく。
    PlayerController player = null;
    bool kaihou = false;

    public SkillTree1 Parent { get => _parent; set => _parent = value; }
    public List<SkillTree1> Childs { get => _childs; set => _childs = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ChildAdd(this);//親がセットされていたら子供として親のListに追加する。
    }

    public void SkillPointJudge(float skillpoint,int arraynumber)//ポイントが足りているか、skillの配列順番
    {
    }

    public void SkillAdd()
    {
        player.AddSkill(_skill);
    }
    public void ChildAdd(SkillTree1 child)
    {
        if(_parent)
        {
            _parent.Childs.Add(child);
        }
    }

    public void AllOpen()//自分より上のスキルを全て使えるようにする
    {
        kaihou = true;
        if(_parent)
        {
            _parent.AllOpen();
        }
    }
}
