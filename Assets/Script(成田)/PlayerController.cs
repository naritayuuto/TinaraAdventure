using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    /// <summary>playerの速さ</summary>
    [SerializeField,Tooltip("playerの速度"),Header("動く速度")]
    float _moveSpeed = 3f;
    /// <summary>playerの回転速度</summary>
    [SerializeField]
    float _turnSpeed = 3f;
    /// <summary>playerのジャンプ力</summary>
    [SerializeField]
    float _jumpPower = 3f;
    /// <summary>playerの基本攻撃力</summary>
    [SerializeField]
    int _attackDamage = 500;//関数で変更する。
    int _keepAttackDamage = 0;
    /// <summary>playerの武器配列</summary>
    [SerializeField,Tooltip("LongSwordMesh"),Header("LongSwordMesh")]
    GameObject _weapon = null;
    [SerializeField,Tooltip("スキルを使用するために押すボタン"),Header("PlayerUseCanvasのSkillButton")]
    GameObject _button = null;
    TextMeshProUGUI _skillText = null;
    /// <summary>ガード判定用コライダー</summary>
    [SerializeField]
    GameObject guardCollider = null;
    /// <summary>weaponの判定用コライダー</summary>
    Collider attackCollider = null;
    /// <summary>時間</summary>
    private float timer = 0.0f;
    /// <summary>攻撃判定用コライダーのアクティブタイム</summary>
    private float attackJudgeTime = 0.5f;
    /// <summary>ガード判定用コライダーのアクティブタイム</summary>
    private float guardJudgeTime = 0.5f;
    /// <summary>接地判定</summary>
    bool isGrounded = true;
    /// <summary>防御判定用,コライダーActive用</summary>
    bool guard = false;
    /// <summary>アニメーション再生中かどうか</summary>
    bool animPlay = false;
    /// <summary>パリィ判定用</summary>
    bool parrysuccess = false;
    public bool Guard { get => guard; set => guard = value; }
    public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Playerhp Hp { get => hp; set => hp = value; }

    List<ISkill> _skills = new List<ISkill>();
    ISkill _skill = null;
    int skillnum = 0;
    Playerhp hp = null;
    Rigidbody _rb = default;
    Animator _anim = default;
    /// <summary>入力された方向の XZ 平面でのベクトル</summary>

    void Start()
    {
        //if(!attackCollider)
        //{
        //    Debug.LogError("攻撃判定用のコライダーがセットされていません");
        //}
        //else if(!guardCollider)
        //{
        //    Debug.LogError("ガード判定用のコライダーがセットされていません");
        //}
        
        if (!_weapon) Debug.LogError("武器がありません");
        if (!_button) Debug.LogError("ボタンをセットしてください");
        _skillText = _button.GetComponentInChildren<TextMeshProUGUI>();
        attackCollider = _weapon.GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        hp = GetComponent<Playerhp>();
        _keepAttackDamage = _attackDamage;
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        timer += Time.deltaTime;
        //if(playerDamagehp <= 0)
        //{
        //    //GameOver
        //}
        //if (normalAttack)
        //{
        //    timer -= timer;
        //    AttackColliderActive();
        //    normalAttack = false;
        //}
        //if(timer > attackJudgeTime)
        //{
        //    attackCollider.SetActive(false);   
        //}
        if(guard)
        {
            timer -= timer;
            GuardColliderActive();
            guard = false;
        }
        if(timer > guardJudgeTime)
        {
            guardCollider.SetActive(false);
        }
        // 入力方向のベクトル計算
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // 方向の入力がない時は、y 軸方向の速度を保持
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // カメラを基準にする
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            //移動の処理
            Vector3 velo = dir.normalized * _moveSpeed;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            NormalAttack();
            _anim.Play("NormalAttack1");
        }
    }
    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
            _anim.SetBool("IsGrounded", isGrounded);//接地判定用
            _anim.SetBool("Guard",guard);//ガード用
            _anim.SetBool("Parrysuccess", parrysuccess);//パリィ成功時true
        }
    }
    private void AttackColliderActive()//武器の当たり判定を出す、animationイベント専用関数
    {
        attackCollider.enabled = true;
    }

    private void AttackColliderNotActive()//武器の当たり判定を出す、animationイベント専用関数
    {
        attackCollider.enabled = false;
    }
    private void GuardColliderActive()//防御用の当たり判定を出す、animationイベント専用関数
    {
        guardCollider.SetActive(true);//パリィと連動するように
    }
    private void NormalAttackPlay()//animationイベント用
    {
        //normalAttack = true;
    }

    public void NormalAttack()
    {
        _attackDamage = Random.Range(400, 600);
    }

    public void AttackDamageKeep(int damage)
    {
        _keepAttackDamage = _attackDamage;
        _attackDamage = damage;
    }
    public void AttackDamageKeep()//レベルアップ時使用。
    {
        _keepAttackDamage = _attackDamage;
    }
    public void ReturnAttackDamage()//攻撃スキルをしていない時に呼ぶ
    {
        _attackDamage = _keepAttackDamage;
    }
    public void ParryJudge(bool judge)
    {
        if(!judge)
        {
            return;
        }
        else
        {
            parrysuccess = true;//parry用のanimationを流す
        }
    }
    void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    public void AddSkill(ISkill skill)
    {
        if (_skill == null)
        {
            _skills.Add(skill);
            _skill = skill;
            _skillText.text = _skill.Name;
        }
        else
        {
            _skills.Add(skill);
            skillnum = 0;
        }
    }

    public void NextSkill()//Nextボタンを押したとき
    {
        if (_skills != null)
        {
            skillnum++;
            _skill = _skills[skillnum % _skills.Count];
            _skillText.text = _skill.Name;
        }

    }
    public void UseSkill()//スキルボタンを押したとき
    {  
        if(_skill != null)
        {
            _skill.Action(this);
        }
    }
}
