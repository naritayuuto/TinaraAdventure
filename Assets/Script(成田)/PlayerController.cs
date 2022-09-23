using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 3;
    [SerializeField]
    float turnSpeed = 3f;
    [SerializeField]
    private float playerhp = 5000f;
    [SerializeField]
    GameObject[] weapons;
    [SerializeField]
    float skillpoint = 0.0f;
    private float getpoint = 0.5f;
    //[SerializeField]
    //Heal heal = null;
    //Skilltree[] newSkill = null;
    List<Skilltree> heal = new List<Skilltree>();
    List<Skilltree> attack = new List<Skilltree>();
    List<Skilltree> buff = new List<Skilltree>();
    Skilltree skilltree = null;
    Rigidbody _rb = default;
    Animator anim = null;
    /// <summary>入力された方向の XZ 平面でのベクトル</summary>
    Vector3 _dir;

    public float Playerhp { get => playerhp; set => playerhp = value; }
    public float Skillpoint { get => skillpoint; set => skillpoint = value; }
    public float Getpoint { get => getpoint; set => getpoint = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        //if (!heal) Debug.LogError("スキル名Healをセットしてください");
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

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

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            //移動の処理
            Vector3 velo = dir.normalized * moveSpeed;
            velo.y = _rb.velocity.y;   
            _rb.velocity = velo;   
        }
        //if (Input.GetButtonDown("Space"))
        //{
        //    if (skilltree.SkillActive[(int)SkillId.heal])//１
        //    {
        //        //newSkill[(int)SkillId.heal].SkillAction();
        //    }
        //}
    }
    public void GetSkillPoint(float point)
    {
        skillpoint += point;
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
    void LateUpdate()
    {
        Vector3 velocity = _rb.velocity;
        velocity.y = 0; // 上下方向の速度は無視する
        //anim.SetFloat("Speed", velocity.magnitude);
    }
}
