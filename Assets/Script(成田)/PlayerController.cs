using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
  None,
  Attack,
  guard
}
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
    /// <summary>playerの武器配列</summary>
    [SerializeField]
    GameObject[] weapons;
    /// <summary>攻撃判定用コライダー</summary>
    [SerializeField]
    GameObject attackCollider = null;
    /// <summary>ガード判定用コライダー</summary>
    [SerializeField]
    GameObject guardCollider = null;
    /// <summary>時間</summary>
    private float timer = 0.0f;
    /// <summary>攻撃判定用コライダーのアクティブタイム</summary>
    private float attackJudgeTime = 0.5f;
    /// <summary>ガード判定用コライダーのアクティブタイム</summary>
    private float guardJudgeTime = 0.5f;
    /// <summary>接地判定</summary>
    bool isGrounded = true;
    /// <summary>通常攻撃判定用</summary>
    bool normalAttack = false;
    /// <summary>通常攻撃判定用</summary>
    bool guard = false;

    bool parrysuccess = false;
    public bool Guard { get => guard; set => guard = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }

    //Heal heal = null;
    //Skilltree[] newSkill = null;
    List<Skilltree> heal = new List<Skilltree>();
    List<Skilltree> attack = new List<Skilltree>();
    List<Skilltree> buff = new List<Skilltree>();
    Skilltree skilltree = null;
    Rigidbody _rb = default;
    Animator anim = default;
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
        _rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
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
        if (normalAttack)
        {
            timer -= timer;
            AttackColliderActive();
            normalAttack = false;
        }
        if(timer > attackJudgeTime)
        {
            attackCollider.SetActive(false);   
        }
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
        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        //}
        if(Input.GetButtonDown("Jump"))
        {
            anim.Play("NormalAttack");
        }
    }
    void LateUpdate()
    {
        // アニメーションの処理
        if (anim)
        {
            anim.SetBool("IsGrounded", isGrounded);//接地判定用
            anim.SetBool("Guard",guard);//ガード用
            anim.SetBool("Parrysuccess", parrysuccess);//パリィ成功時true
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            anim.SetFloat("Speed", walkSpeed.magnitude);
        }
    }
    private void AttackColliderActive()
    {
        attackCollider.SetActive(true);
    }
    
    private void GuardColliderActive()
    {
        guardCollider.SetActive(true);
    }
    private void NormalAttackPlay()//animationイベント用
    {
        normalAttack = true;
    }

    public void NormalAttack()//animationイベント用
    {
        attackDamage = Random.Range(400, 600);
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
    void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
    //public void AddSkill(int skillId, int skillnumber)//SkillButtonが押されたら呼び出す、あくまでも使えるスキルのListを作っている部分
    //{
    //    {
    //        switch ((SkillId)skillId)
    //        {
    //            case SkillId.heal://Listで作り直してAddで追加、enumはあくまでも種類分けなので回復力が違うHealが出てきた場合困る
    //                //newSkill[skillId] = new Heal();
    //                heal.Add();
    //                break;
    //            case SkillId.attack:
    //                attack.Add();
    //                break;
    //            case SkillId.buff:
    //                buff.Add();
    //                break;
    //                //スキルの作成が終わり次第ここに追加
    //        }
    //    }
    //}

}
