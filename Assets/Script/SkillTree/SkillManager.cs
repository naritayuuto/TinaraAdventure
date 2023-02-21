using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//ここではskillbuttonにそれぞれ何のスキルを持つのか割り振りする。
public class SkillManager : MonoBehaviour//skillを管理する。目次のようなもので何が解放されているのかの情報を持っている。
{
    [SerializeField,Header("SkillButtonが付いているCanvas"),Tooltip("SkillButtonが付いているCanvas")]
    GameObject _skillCanvas = null;
    [SerializeField, Header("スキルポイントを表示するText")]
    TextMeshProUGUI _skillPointText = null;
    [Tooltip("SkillTreeが付いたボタン")]
    List<SkillButton> _skillButtons = new List<SkillButton>();//SkillButtonが持つarraynumberを記録するために使用する。_skillActiveの要素番号に結びつけ。
    [Tooltip("どのスキルが解放されたかを保持する物")]
    bool[] _skillActive;//直接的にいじることはない
    [Tooltip("_skillButtonのSkillTree")]
    List<SkillTree> _skillTree = new List<SkillTree>();
    [Tooltip("スキルポイントを入れる変数")]
    float _skillPoint = 0f;
    [Tooltip("敵を攻撃したときに加算する値")]
    float _attackSkillPoint = 0.5f;
    [Tooltip("敵を倒したときに加算する値")]
    float _defeatSkillPoint = 5f;
    PlayerUseSkill _playerUseSkill = null;
    public float SkillPoint { get => _skillPoint; set => _skillPoint = value; }
    public bool[] SkillActive { get => _skillActive; set => _skillActive = value; }

    void Start()
    {
        _playerUseSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUseSkill>();
        _skillCanvas.SetActive(true);
        _skillPointText.text = "スキルポイント : " + _skillPoint.ToString();
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Skilltree");
        foreach(var buttons in obj)
        {
            _skillButtons.Add(buttons.GetComponent<SkillButton>());
            _skillTree.Add(buttons.GetComponent<SkillTree>());
            buttons.GetComponent<SkillTree>().SkillManager = this;
        }
        _skillActive = new bool[_skillButtons.Count];
        for (int i = 0; i < _skillButtons.Count; i++)
        {
            _skillButtons[i].ArrayNumber = i;
        }
        //最初からfalseだと要素番号が振られないため
        _skillCanvas.SetActive(false);
    }
    /// <summary>
    /// enemyを攻撃した場合に呼ぶ関数
    /// </summary>
    /// <param name="enemyDie"></param>
    public void AddSkillPoint(bool enemyDie)
    {
        _skillPoint = enemyDie == true ? _skillPoint + _defeatSkillPoint : _skillPoint + _attackSkillPoint;
        _skillPointText.text = "スキルポイント : " + _skillPoint.ToString();
    }

    public void AddSkill(ISkill skill)
    {
        _playerUseSkill.AddSkill(skill);
    }
}
