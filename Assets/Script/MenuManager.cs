using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField, Header("チュートリアル終了ボタン")]
    GameObject _tutorialButton = null;
    [SerializeField, Header("操作説明ボタン")]
    GameObject _operationButton = null;
    [SerializeField, Header("メニューを閉じるボタン")]
    GameObject _menuOffButton = null;
    [SerializeField, Header("スキル説明ボタン")]
    GameObject _SkillOprButton = null;
    [SerializeField]
    GameObject[] _buttons;
    GameObject _indestructibleEnemy = null;
    bool _tutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        _indestructibleEnemy = GameObject.Find("IndestructibleEnemy");

    }

    // Update is called once per frame
    void Update()
    {
        if (_indestructibleEnemy && !_tutorial)
        {
            _tutorial = true;
            _tutorialButton.SetActive(true);
        }
    }
    public void ButtonActive(bool active)
    {
        foreach (var _button in _buttons)
        {
            _button.SetActive(active);
        }
    }
}
