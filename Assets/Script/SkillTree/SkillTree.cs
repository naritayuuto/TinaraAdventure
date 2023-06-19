using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(SkillLine))]
public class SkillTree : MonoBehaviour
{
    [SerializeField]
    SkillTree _parent = null;//一つ上。

    List<SkillTree> _childs = new List<SkillTree>();//自分自身の下に付いている子供たち

    SkillManager _skillManager = null;
    [Tooltip("スキル解放時に半透明にする為に使用")]
    SkillLine _skillLine = null;

    public int _arrayNumber = 0;
    
    /// <summary>解放するために必要なポイント</summary>
    [SerializeField]
    int _openSkillPoint = 1;
    [SerializeField, Tooltip("枝分かれするごとに増やす_openSkillPointの加算値")]
    int _addPoint = 2;
    [SerializeField, SerializeReference, SubclassSelector]
    ISkill _skill = null;//最初から持っておく。
    public SkillTree Parent { get => _parent; set => _parent = value; }
    public List<SkillTree> Childs { get => _childs; set => _childs = value; }
    public ISkill Skill { get => _skill;}
    public int OpenSkillPoint { get => _openSkillPoint; set => _openSkillPoint = value; }
    public SkillManager SkillManager { get => _skillManager; set => _skillManager = value; }

    private void Awake()
    {
        ChildAdd(this);//親がセットされていたら子供として親のListに追加する。
        if (_parent)
        {
            _openSkillPoint = _parent.OpenSkillPoint + _addPoint;
        }
    }
    private void Start()
    {
        _skillLine = GetComponent<SkillLine>();
    }
    public void SkillPointJudge(float removeSkillpoint,int arraynumber,Button button)//ポイントが足りているか、skillの配列順番
    {
        if (!_parent || SkillManager.SkillActive[_parent._arrayNumber] == true)
        {
            if (_skillManager.SkillPoint >= removeSkillpoint)
            {
                _skillManager.SkillPoint -= removeSkillpoint;
                _skillManager.SkillActive[arraynumber] = true;
                _skillManager.AddSkill(_skill);
                button.interactable = false;
                _skillLine.LineNotActive();
                Debug.Log("解放出来ました");
            }
            else
            {
                Debug.Log("解放出来ません");
            }
        }
        else
        {
            Debug.Log("解放出来ません");
        }
    }

    public void ChildAdd(SkillTree child)
    {
        if(_parent)
        {
            _parent.Childs.Add(child);
        }
    }

    //public void AllOpen()//自分より下のスキルを全て使えるようにする
    //{
    //    float skillpoint = GetComponent<SkillButton>().SkillPoint;
    //    int arraynumber = GetComponent<SkillButton>().ArrayNumber;
    //    Button button = GetComponent<Button>();
    //    SkillPointJudge(skillpoint, arraynumber,button);
    //    if (_childs != null && GameManager.Instance._skillManager.SkillActive[arraynumber])//子を持っていたら
    //    {
    //        foreach(var child in _childs)
    //        {
    //            child.AllOpen();
    //        }
    //    }
    //}
}
