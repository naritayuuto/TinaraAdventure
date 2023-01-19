using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree : MonoBehaviour
{
    [SerializeField]
    SkillTree _parent = null;//���B

    List<SkillTree> _childs = new List<SkillTree>();//�������g�̉��ɕt���Ă���q������

    [SerializeField, SerializeReference, SubclassSelector]
    ISkill _skill = null;//�ŏ����玝���Ă����B
    PlayerController player = null;
    public SkillTree Parent { get => _parent; set => _parent = value; }
    public List<SkillTree> Childs { get => _childs; set => _childs = value; }

    private void Awake()
    {
        ChildAdd(this);//�e���Z�b�g����Ă�����q���Ƃ��Đe��List�ɒǉ�����B
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void SkillPointJudge(float removeSkillpoint,int arraynumber,Button button)//�|�C���g������Ă��邩�Askill�̔z�񏇔�
    {
        if(GameManager.Instance._skillManager.SkillPoint >= removeSkillpoint)
        {
            GameManager.Instance._skillManager.SkillPoint -= removeSkillpoint;
            GameManager.Instance._skillManager.SkillActive[arraynumber] = true;
            SkillAdd();
            button.interactable = false;
            Debug.Log("����o���܂���");
        }
        else
        {
            Debug.Log("����o���܂���");
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

    public void AllOpen()//������艺�̃X�L����S�Ďg����悤�ɂ���
    {
        float skillpoint = GetComponent<SkillButton>().SkillPoint;
        int arraynumber = GetComponent<SkillButton>().ArrayNumber;
        Button button = GetComponent<Button>();
        SkillPointJudge(skillpoint, arraynumber,button);
        if (_childs != null && GameManager.Instance._skillManager.SkillActive[arraynumber])//�q�������Ă�����
        {
            foreach(var child in _childs)
            {
                child.AllOpen();
            }
        }
    }
}
