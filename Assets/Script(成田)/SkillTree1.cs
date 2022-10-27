using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillId
{
    heal = 1,
    attack,
    buff
}
public class SkillTree1 : MonoBehaviour
{
    SkillManager skillManager = null;//skillpointを使うため。

    SkillTree1 parent = null;//一つ上。

    List<SkillTree1> childs = new List<SkillTree1>();//自分自身の下に付いている物。このSkilltreeが持っているスキル。

    [SerializeField, SerializeReference, SubclassSelector]
    ISkill skill;//最初から持っておく。

    // Start is called before the first frame update
    void Start()
    {
        skillManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
    }

    public void SkillPointJudge(float skillpoint,int arraynumber)//ポイントが足りているか、skillの配列順番
    {
        if (skillManager.SkillPoint < skillpoint)
        {
            return;
        }
        else
        {
            skillManager.Buttons[arraynumber].Yobidasi(skill);
        }
    }

    public virtual void Action() { }
}
