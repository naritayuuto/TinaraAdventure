using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAnim : MonoBehaviour
{
    /// <summary>ガード判定用コライダー</summary>
    [SerializeField]
    GameObject _guardCollider = null;
    [SerializeField, Tooltip("playerの武器"), Header("LongSwordMesh")]
    GameObject _weapon = null;
    /// <summary>weaponの判定用コライダー</summary>
    Collider _attackCollider = null;
    [Tooltip("Playerのanimator")]
    Animator _anim = default;
    [Tooltip("アニメーション再生中かどうか")]
    bool animPlay = false;
    Rigidbody2D _rb = null;
    PlayerAttackParam _attackParam;
    public Animator Anim { get => _anim; set => _anim = value; }
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (!_weapon) Debug.LogError("武器がありません");
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
            //_anim.SetBool("Guard",guard);//ガード用
            //_anim.SetBool("Parrysuccess", parrysuccess);//パリィ成功時true
        }
    }
    private void AttackColliderActive()//武器の当たり判定を出す、animationイベント専用関数
    {
        _attackCollider.enabled = true;
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
        _attackParam.AttackDamage = Random.Range(_attackParam.MinAttackDamage, _attackParam.MaxAttackDamage);
    }

    public void AttackDamageAdd(int damage)//Attackスキルを使用したときに攻撃力を変えている。
    {
        AttackDamageDecision();
        _attackParam.AttackDamage += damage;
    }
    public void AttackDamageKeep()//レベルアップ時使用。
    {
        _attackParam.KeepAttackDamage = _attackParam.AttackDamage;
    }
    public void ReturnAttackDamage()//攻撃スキルをしていない時に呼ぶ
    {
        _attackParam.AttackDamage = _attackParam.KeepAttackDamage;
    }

    public void DamageAnimation()
    {
        //animatoinの記述
    }
}
