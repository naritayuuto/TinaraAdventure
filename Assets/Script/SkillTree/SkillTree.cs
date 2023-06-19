using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(SkillLine))]
public class SkillTree : MonoBehaviour
{
    [SerializeField]
    SkillTree _parent = null;//���B

    List<SkillTree> _childs = new List<SkillTree>();//�������g�̉��ɕt���Ă���q������

    SkillManager _skillManager = null;
    [Tooltip("�X�L��������ɔ������ɂ���ׂɎg�p")]
    SkillLine _skillLine = null;

    public int _arrayNumber = 0;
    
    /// <summary>������邽�߂ɕK�v�ȃ|�C���g</summary>
    [SerializeField]
    int _openSkillPoint = 1;
    [SerializeField, Tooltip("�}�����ꂷ�邲�Ƃɑ��₷_openSkillPoint�̉��Z�l")]
    int _addPoint = 2;
    [SerializeField, SerializeReference, SubclassSelector]
    ISkill _skill = null;//�ŏ����玝���Ă����B
    public SkillTree Parent { get => _parent; set => _parent = value; }
    public List<SkillTree> Childs { get => _childs; set => _childs = value; }
    public ISkill Skill { get => _skill;}
    public int OpenSkillPoint { get => _openSkillPoint; set => _openSkillPoint = value; }
    public SkillManager SkillManager { get => _skillManager; set => _skillManager = value; }

    private void Awake()
    {
        ChildAdd(this);//�e���Z�b�g����Ă�����q���Ƃ��Đe��List�ɒǉ�����B
        if (_parent)
        {
            _openSkillPoint = _parent.OpenSkillPoint + _addPoint;
        }
    }
    private void Start()
    {
        _skillLine = GetComponent<SkillLine>();
    }
    public void SkillPointJudge(float removeSkillpoint,int arraynumber,Button button)//�|�C���g������Ă��邩�Askill�̔z�񏇔�
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
                Debug.Log("����o���܂���");
            }
            else
            {
                Debug.Log("����o���܂���");
            }
        }
        else
        {
            Debug.Log("����o���܂���");
        }
    }

    public void ChildAdd(SkillTree child)
    {
        if(_parent)
        {
            _parent.Childs.Add(child);
        }
    }

    //public void AllOpen()//������艺�̃X�L����S�Ďg����悤�ɂ���
    //{
    //    float skillpoint = GetComponent<SkillButton>().SkillPoint;
    //    int arraynumber = GetComponent<SkillButton>().ArrayNumber;
    //    Button button = GetComponent<Button>();
    //    SkillPointJudge(skillpoint, arraynumber,button);
    //    if (_childs != null && GameManager.Instance._skillManager.SkillActive[arraynumber])//�q�������Ă�����
    //    {
    //        foreach(var child in _childs)
    //        {
    //            child.AllOpen();
    //        }
    //    }
    //}
}
