using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour//player�ɕt���Ă���script�S�Ă�public�Őݒ肷��B�X�L�����g�����ɌĂԂ��߁B
{
    /// <summary>player�̑���</summary>
    [SerializeField,Tooltip("player�̑��x"),Header("�������x")]
    float _moveSpeed = 3f;
    /// <summary>player�̉�]���x</summary>
    [SerializeField]
    float _turnSpeed = 3f;
    [SerializeField,Tooltip("�X�L�����g�p���邽�߂ɉ����{�^��"),Header("PlayerUseCanvas��SkillButton")]
    GameObject _button = null; 
    /// <summary>����</summary>
    private float _timer = 0.0f;
    /// <summary>�U������p�R���C�_�[�̃A�N�e�B�u�^�C��</summary>
    private float attackJudgeTime = 0.5f;
    /// <summary>�K�[�h����p�R���C�_�[�̃A�N�e�B�u�^�C��</summary>
    private float guardJudgeTime = 0.5f;
    /// <summary>�h�䔻��p,�R���C�_�[Active�p</summary>
    bool guard = false;
    /// <summary>�p���B����p</summary>
    bool parrysuccess = false;
    public PlayerHp _playerHp = null;
    public PlayerAnim _playerAnimAndcollider = null;
    public PlayerUseSkill _playerSkill = null;
    public PlayerAttackParam _playerAttackParam = null;
    Rigidbody _rb = default;
    public bool Guard { get => guard; set => guard = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public Rigidbody Rb { get => _rb;}

    /// <summary>���͂��ꂽ������ XZ ���ʂł̃x�N�g��</summary>

    private void Awake()
    {
        GameManager.Instance.GetPlayerObject(this);
    }
    void Start()
    {
        _playerAnimAndcollider = GetComponent<PlayerAnim>();
        _playerSkill = GetComponent<PlayerUseSkill>();
        if (!_button) Debug.LogError("�{�^�����Z�b�g���Ă�������");
        _rb = GetComponent<Rigidbody>(); 
        _playerHp = GetComponent<PlayerHp>();
        _playerAnimAndcollider = GetComponent<PlayerAnim>();
        _playerSkill = GetComponent<PlayerUseSkill>();
        _playerAttackParam = GetComponent<PlayerAttackParam>();
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        // ���͕����̃x�N�g���v�Z
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // �����̓��͂��Ȃ����́Ay �������̑��x��ێ�
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // �J��������ɂ���
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            //�ړ��̏���
            Vector3 velo = dir.normalized * _moveSpeed;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }
    }
}
