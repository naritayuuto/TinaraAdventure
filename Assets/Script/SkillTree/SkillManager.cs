using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�����ł�skillbutton�ɂ��ꂼ�ꉽ�̃X�L�������̂�����U�肷��B
public class SkillManager : MonoBehaviour//skill���Ǘ�����B�ڎ��̂悤�Ȃ��̂ŉ����������Ă���̂��̏��������Ă���B
{
    [SerializeField]
    SkillButton[] buttons;//SkillButton������arraynumber���L�^���邽�߂Ɏg�p����B_skillActive�̗v�f�ԍ��Ɍ��т��B

    bool[] _skillActive;//���ړI�ɂ����邱�Ƃ͂Ȃ�

    List<SkillTree> skillTree = new List<SkillTree>();
    [Tooltip("�X�L���|�C���g������ϐ�")]
    float _skillPoint = 0f;
    [Tooltip("�G���U�������Ƃ��ɉ��Z����l")]
    float _attackSkillPoint = 0.5f;
    [Tooltip("�G��|�����Ƃ��ɉ��Z����l")]
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
