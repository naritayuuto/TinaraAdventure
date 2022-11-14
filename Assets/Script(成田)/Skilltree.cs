using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree : MonoBehaviour
{
    [SerializeField]
    SkillTree _parent = null;//一つ上。

    List<SkillTree> _childs = new List<SkillTree>();//自分自身の下に付いている子供たち

    [SerializeField, SerializeReference, SubclassSelector]
    ISkill _skill = null;//最初から持っておく。
    PlayerController player = null;
    SkillManager sManager = null;
    [SerializeField]
    GameObject myButton;
    public SkillTree Parent { get => _parent; set => _parent = value; }
    public List<SkillTree> Childs { get => _childs; set => _childs = value; }

    private void Awake()
    {
        ChildAdd(this);//親がセットされていたら子供として親のListに追加する。
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
    }

    public void SkillPointJudge(float skillpoint,int arraynumber,UnityEngine.UI.Button button)//ポイントが足りているか、skillの配列順番
    {
        if(sManager.SkillPoint >= skillpoint)
        {
            sManager.SkillPoint -= skillpoint;
            sManager.SkillActive[arraynumber] = true;
            SkillAdd();
            button.interactable = false;
            Debug.Log("解放出来ました");
        }
        else
        {
            Debug.Log("解放出来ません");
        }
    }

    public void SkillAdd()
    {
        player.AddSkill(_skill);
    }
    public void ChildAdd(SkillTree child)
    {
        if(_parent)
        {
            _parent.Childs.Add(child);
        }
    }

    public void AllOpen()//自分より下のスキルを全て使えるようにする
    {
        if (_childs != null)//子を持っていたら
        {
            foreach(var child in _childs)
            {
                child.AllOpen();
            }
        }
    }
}
