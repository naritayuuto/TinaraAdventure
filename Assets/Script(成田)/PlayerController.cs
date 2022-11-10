using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    /// <summary>playerの速さ</summary>
    [SerializeField]
    float moveSpeed = 3f;
    /// <summary>playerの回転速度</summary>
    [SerializeField]
    float turnSpeed = 3f;
    /// <summary>playerのジャンプ力</summary>
    [SerializeField]
    float jumpPower = 3f;
    /// <summary>playerの基本攻撃力</summary>
    [SerializeField]
    int attackDamage = 500;//関数で変更する。
    int keepAttackDamage = 0;
    /// <summary>playerの武器配列</summary>
    [SerializeField]
    GameObject weapon = null;
    [SerializeField]
    GameObject button = null;
    TextMeshProUGUI skillText = null;
    ///// <summary>攻撃判定用コライダー</summary>
    //[SerializeField]
    //GameObject attackCollider = null;
    //↑ここの部分は武器に当たり判定を付けることで解決。
    /// <summary>ガード判定用コライダー</summary>
    [SerializeField]
    GameObject guardCollider = null;
    Collider attackCollider = null;
    /// <summary>時間</summary>
    private float timer = 0.0f;
    /// <summary>攻撃判定用コライダーのアクティブタイム</summary>
    private float attackJudgeTime = 0.5f;
    /// <summary>ガード判定用コライダーのアクティブタイム</summary>
    private float guardJudgeTime = 0.5f;
    /// <summary>接地判定</summary>
    bool isGrounded = true;
    ///// <summary>通常攻撃判定用,コライダーActive用</summary>
    //bool normalAttack = false;
    //↑ここの部分は武器に当たり判定を付けることで解決。
    /// <summary>防御判定用,コライダーActive用</summary>
    bool guard = false;
    /// <summary>アニメーション再生中かどうか</summary>
    bool animPlay = false;
    /// <summary>パリィ判定用</summary>
    bool parrysuccess = false;
    public bool Guard { get => guard; set => guard = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Playerhp Hp { get => hp; set => hp = value; }

    List<ISkill> _skills = new List<ISkill>();
    ISkill _skill;
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
        
        if (!weapon) Debug.LogError("武器がありません");
        if (!button) Debug.LogError("ボタンをセットしてください");
        skillText = button.GetComponentInChildren<TextMeshProUGUI>();
        attackCollider = weapon.GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        hp = GetComponent<Playerhp>();
        keepAttackDamage = attackDamage;
        //skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
        //if (!heal) Debug.LogError("スキル名Healをセットしてください");
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
            Vector3 velo = dir.normalized * moveSpeed;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            NormalAttack();
            _anim.Play("NormalAttack");
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
        attackDamage = Random.Range(400, 600);
    }

    public void AttackDamageKeep(int damage)
    {
        keepAttackDamage = attackDamage;
        attackDamage = damage;
    }
    public void AttackDamageKeep()//レベルアップ時使用。
    {
        keepAttackDamage = attackDamage;
    }
    public void ReturnAttackDamage()//攻撃スキルをしていない時に呼ぶ
    {
        attackDamage = keepAttackDamage;
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
        if (_skills == null)
        {
            _skills.Add(skill);
            _skill = skill;
            skillText.text = skill.Name;
        }
        else
        {
            _skills.Add(skill);
            skillnum = 0;
        }
    }

    public void NextSkill()
    {
        if (_skills != null)
        {
            skillnum++;
            _skill = _skills[skillnum % _skills.Count];
            skillText.text = _skill.Name;
        }

    }
    public void UseSkill()
    {  
        if(_skill != null)
        {
            _skill.Action(this);
        }
    }
}
