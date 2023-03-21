using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField,Tooltip("�K�[�h����p�R���C�_�[")]
    GameObject _guardCollider = null;
    [SerializeField, Tooltip("player�̕���"), Header("LongSwordMesh")]
    GameObject _weapon = null;
    [Tooltip("weapon�̔���p�R���C�_�[")]
    Collider _attackCollider = null;
    [Tooltip("Player��animator")]
    Animator _anim = default;
    [Tooltip("����̃G�t�F�N�g���o���Ă���")]
    TrailRenderer _trailRenderer = null;
    PlayerController _player = null;
    Rigidbody2D _rb = null;
    public Animator Anim { get => _anim; set => _anim = value; }
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (!_weapon) Debug.LogError("���킪����܂���");
        _attackCollider = _weapon.GetComponent<BoxCollider>();
        _trailRenderer = _weapon.GetComponentInChildren<TrailRenderer>();
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            AttackDamageDecision();
            _anim.SetBool("Attack", true);
        }
        if(!_anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && _trailRenderer.emitting)
        {
            _trailRenderer.emitting = false;
        }
    }
    private void LateUpdate()
    {
        _anim.SetBool("Attack", false);
    }
    private void AttackColliderActive()//����̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        _attackCollider.enabled = true;
        _trailRenderer.emitting = true;
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
        _player._playerAttackParam.AttackDamage = Random.Range(_player._playerAttackParam.MinAttackDamage, _player._playerAttackParam.MaxAttackDamage);
    }

    public void AttackDamageAdd(int damage)//Attack�X�L�����g�p�����Ƃ��ɍU���͂�ς��Ă���B
    {
        AttackDamageDecision();
        _player._playerAttackParam.AttackDamage += damage;
    }
    public void AttackDamageKeep()//���x���A�b�v���g�p�B
    {
        _player._playerAttackParam.KeepAttackDamage = _player._playerAttackParam.AttackDamage;
    }
    public void ReturnAttackDamage()//�U���X�L�������Ă��Ȃ����ɌĂ�
    {
        _player._playerAttackParam.AttackDamage = _player._playerAttackParam.KeepAttackDamage;
    }

    public void DamageAnimation()
    {
        _anim.Play("Damage");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invalid">�U�������o�t���؂ꂽ�ꍇTrue���n�����</param>
    public void InvalidBuffAnimation(bool invalid)
    {
        if (!invalid)
        {
            //_anim.Play("�U���𖳌��ɂ������Ɏg���A�j���[�V�����̖��O");
        }
        else
        {
            //_anim.Play("�U���𖳌��ɂ���o�t���؂ꂽ�Ƃ��Ɏg���A�j���[�V�����̖��O");
        }
    }
}
