using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField,Tooltip("ガード判定用コライダー")]
    GameObject _guardCollider = null;
    [SerializeField, Tooltip("playerの武器"), Header("LongSwordMesh")]
    GameObject _weapon = null;
    [Tooltip("weaponの判定用コライダー")]
    Collider _attackCollider = null;
    [Tooltip("Playerのanimator")]
    Animator _anim = default;
    [Tooltip("武器のエフェクトを出している")]
    TrailRenderer _trailRenderer = null;
    PlayerController _player = null;
    Rigidbody2D _rb = null;
    public Animator Anim { get => _anim; set => _anim = value; }
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (!_weapon) Debug.LogError("武器がありません");
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
    private void AttackColliderActive()//武器の当たり判定を出す、animationイベント専用関数
    {
        _attackCollider.enabled = true;
        _trailRenderer.emitting = true;
    }

    private void AttackColliderNotActive()//武器の当たり判定を出す、animationイベント専用関数
    {
        _attackCollider.enabled = false;
    }
    private void GuardColliderActive()//防御用の当たり判定を出す、animationイベント専用関数
    {
        _guardCollider.SetActive(true);//パリィと連動するように
    }
    private void NormalAttackPlay()//animationイベント用
    {
        //normalAttack = true;
    }

    public void AttackDamageDecision()//攻撃アニメーションとセットで使う
    {
        _player._playerAttackParam.AttackDamage = Random.Range(_player._playerAttackParam.MinAttackDamage, _player._playerAttackParam.MaxAttackDamage);
    }

    public void AttackDamageAdd(int damage)//Attackスキルを使用したときに攻撃力を変えている。
    {
        AttackDamageDecision();
        _player._playerAttackParam.AttackDamage += damage;
    }
    public void AttackDamageKeep()//レベルアップ時使用。
    {
        _player._playerAttackParam.KeepAttackDamage = _player._playerAttackParam.AttackDamage;
    }
    public void ReturnAttackDamage()//攻撃スキルをしていない時に呼ぶ
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
    /// <param name="invalid">攻撃無効バフが切れた場合Trueが渡される</param>
    public void InvalidBuffAnimation(bool invalid)
    {
        if (!invalid)
        {
            //_anim.Play("攻撃を無効にした時に使うアニメーションの名前");
        }
        else
        {
            //_anim.Play("攻撃を無効にするバフが切れたときに使うアニメーションの名前");
        }
    }
}
