using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointCheck : MonoBehaviour
{
    [SerializeField]
    SkillManager _sManager = null;
    [SerializeField,Tooltip("�X�L���|�C���g�����p")]
    GameObject _spoCanvas = null;
    [SerializeField, Tooltip("�X�L���c���[�����p")]
    GameObject _stoCanvas = null;
    [SerializeField, Tooltip("�Ñ��p")]
    GameObject _sCanvas = null;
    const int _checkPoint = 1;
    [Tooltip("�ꕪ")]
    const int _minutes = 60;
    bool _attackCheck = false;
    bool _openCheck = false;
    bool _sCheck = false;
    float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _stoCanvas.SetActive(_openCheck);
        _spoCanvas.SetActive(_attackCheck);
        _sCanvas.SetActive(_sCheck);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _minutes && !_sCheck)
        {
            _sCheck = true;
            _sCanvas.SetActive(_sCheck);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(_sManager.SkillPoint < _checkPoint && !_attackCheck)
        {
            _attackCheck = true;
            _spoCanvas.SetActive(_attackCheck);
        }
    }
    public void SkillOper()
    {
        if(!_openCheck)
        {
            _openCheck = true;
            _stoCanvas.SetActive(_openCheck);
        }
    }
}
