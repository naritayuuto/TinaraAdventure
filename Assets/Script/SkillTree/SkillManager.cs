using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�����ł�skillbutton�ɂ��ꂼ�ꉽ�̃X�L�������̂�����U�肷��B
public class SkillManager : MonoBehaviour//skill���Ǘ�����B�ڎ��̂悤�Ȃ��̂ŉ����������Ă���̂��̏��������Ă���B
{
    [SerializeField, Tooltip("SkillButton���t���Ă���Canvas")]
    GameObject _skillCanvas = null;
    [Tooltip("SkillTree���t�����{�^��")]
    List<SkillButton> _skillButtons = new List<SkillButton>();//SkillButton������arraynumber���L�^���邽�߂Ɏg�p����B_skillActive�̗v�f�ԍ��Ɍ��т��B
    [Tooltip("�ǂ̃X�L����������ꂽ����ێ����镨")]
    bool[] _skillActive;//���ړI�ɂ����邱�Ƃ͂Ȃ�
    [Tooltip("_skillButton��SkillTree")]
    List<SkillTree> _skillTree = new List<SkillTree>();
    [Tooltip("�X�L���|�C���g������ϐ�")]
    float _skillPoint = 0f;
    [Tooltip("�G���U�������Ƃ��ɉ��Z����l")]
    float _attackSkillPoint = 0.5f;
    [Tooltip("�G��|�����Ƃ��ɉ��Z����l")]
    float _defeatSkillPoint = 5f;
    public float SkillPoint { get => _skillPoint; set => _skillPoint = value; }
    public bool[] SkillActive { get => _skillActive; set => _skillActive = value; }

    private void Awake()
    {
        GameManager.Instance.GetSkillManager(this);
    }
    void Start()
    {
        _skillCanvas.SetActive(true);
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Skilltree");
        foreach(var buttons in obj)
        {
            _skillButtons.Add(buttons.GetComponent<SkillButton>());
            _skillTree.Add(buttons.GetComponent<SkillTree>());
        }
        _skillActive = new bool[_skillButtons.Count];
        for (int i = 0; i < _skillButtons.Count; i++)
        {
            _skillButtons[i].ArrayNumber = i;
        }
        //�ŏ�����false���Ɨv�f�ԍ����U���Ȃ�����
        _skillCanvas.SetActive(false);
    }
    public void AddSkillPoint(bool enemyDie)
    {
        _skillPoint = enemyDie == true ? _skillPoint + _attackSkillPoint : _skillPoint + _defeatSkillPoint;
    }
}
