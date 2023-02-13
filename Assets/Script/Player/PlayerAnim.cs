using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAnim : MonoBehaviour
{
    /// <summary>�K�[�h����p�R���C�_�[</summary>
    [SerializeField]
    GameObject _guardCollider = null;
    [SerializeField, Tooltip("player�̕���"), Header("LongSwordMesh")]
    GameObject _weapon = null;
    /// <summary>weapon�̔���p�R���C�_�[</summary>
    Collider _attackCollider = null;
    [Tooltip("Player��animator")]
    Animator _anim = default;
    [Tooltip("�A�j���[�V�����Đ������ǂ���")]
    bool animPlay = false;
    Rigidbody2D _rb = null;
    PlayerAttackParam _attackParam;
    public Animator Anim { get => _anim; set => _anim = value; }
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (!_weapon) Debug.LogError("���킪����܂���");
        _attackCollider = _weapon.GetComponent<BoxCollider>();
        _attackParam = GetComponent<PlayerAttackParam>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            AttackDamageDecision();
            _anim.Play("NormalAttack");
        }
    }
    private void LateUpdate()
    {
        if (_anim)
        {
            Vector3 walkSpeed = GameManager.Instance.Player.Rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
            //_anim.SetBool("Guard",guard);//�K�[�h�p
            //_anim.SetBool("Parrysuccess", parrysuccess);//�p���B������true
        }
    }
    private void AttackColliderActive()//����̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        _attackCollider.enabled = true;
    }

    private void AttackColliderNotActive()//����̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        _attackCollider.enabled = false;
    }
    private void GuardColliderActive()//�h��p�̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        _guardCollider.SetActive(true);//�p���B�ƘA������悤��
    }
    private void NormalAttackPlay()//animation�C�x���g�p
    {
        //normalAttack = true;
    }

    public void AttackDamageDecision()//�U���A�j���[�V�����ƃZ�b�g�Ŏg��
    {
        _attackParam.AttackDamage = Random.Range(_attackParam.MinAttackDamage, _attackParam.MaxAttackDamage);
    }

    public void AttackDamageAdd(int damage)//Attack�X�L�����g�p�����Ƃ��ɍU���͂�ς��Ă���B
    {
        AttackDamageDecision();
        _attackParam.AttackDamage += damage;
    }
    public void AttackDamageKeep()//���x���A�b�v���g�p�B
    {
        _attackParam.KeepAttackDamage = _attackParam.AttackDamage;
    }
    public void ReturnAttackDamage()//�U���X�L�������Ă��Ȃ����ɌĂ�
    {
        _attackParam.AttackDamage = _attackParam.KeepAttackDamage;
    }

    public void DamageAnimation()
    {
        //animatoin�̋L�q
    }
}
