using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> _selectList;

    List<SkillSelectTable> _selectTable = new List<SkillSelectTable>();
    List<UnityEngine.UI.Text> _selectText = new List<UnityEngine.UI.Text>();
    CanvasGroup _canvas;

    bool _startEvent = false;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        //_canvas.alpha = 0;
    }

    void Start()
    {
        for (int i = 0; i < _selectList.Count; ++i)
        {
            _selectTable.Add(null);
            _selectText.Add(_selectList[i].GetComponentInChildren<UnityEngine.UI.Text>());
            {
                var index = i;
                var btn = _selectList[i].GetComponentInChildren<UnityEngine.UI.Button>();
                btn.onClick.AddListener(() =>
                {
                    if (_canvas.alpha == 0) return;
                    OnClick(index);
                });
            }
        }
    }

    private void Update()
    {
        if (_startEvent)
        {
            SelectStart();
            _startEvent = false;
        }
    }

    public void SelectStartDelay()
    {
        _startEvent = true;
    }

    public void SelectStart()
    {
        _canvas.alpha = 1;

        List<SkillSelectTable> table = new List<SkillSelectTable>();
        var list = GameData.SkillSelectTable.Where(s => GameManager.Level >= s.Level);//playerのレベルよりスキルの取得レベルが同じ、または小さいとき

        for (int i = 0; i < _selectList.Count; ++i)
        {
            _selectTable[i] = null;
            _selectText[i].text = "";
        }

        for (int i = 0; i < _selectList.Count; ++i)
        {
            int totalProb = list.Sum(s => s.Probability);//全体の合計を算出
            int rand = Random.Range(0, totalProb);//0から全体までの値
            foreach (var s in list)
            {
                if (rand < s.Probability)//ガチャで言うところの排出率
                {
                    _selectTable[i] = s;
                    _selectText[i].text = s.Name;
                    list = list.Where(ls => !(ls.Type == s.Type && ls.TargetId == s.TargetId));//自分自身を除外
                    break;
                }
                rand -= s.Probability;
            }
        }
    }

    public void OnClick(int index)
    {
        GameManager.Instance.LevelUpSelect(_selectTable[index]);

        _canvas.alpha = 0;
    }
}