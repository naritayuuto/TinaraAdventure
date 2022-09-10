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
    Heal heal = null;
    Skilltree skilltree = null;
    Rigidbody _rb = default;
    Animator anim = null;
    /// <summary>入力された方向の XZ 平面でのベクトル</summary>
    Vector3 _dir;

    public float Playerhp { get => playerhp; set => playerhp = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
        if (!heal) Debug.LogError("スキル名Healをセットしてください");
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

            Vector3 velo = dir.normalized * moveSpeed; // 入力した方向に移動する
            velo.y = _rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            _rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }
        if (Input.GetButtonDown("Space"))
        {
            if (skilltree.SkillActive[(int)skillId.heal])//１
            {
                heal.SkillAction();
            }
        }
    }


    void LateUpdate()
    {
        Vector3 velocity = _rb.velocity;
        velocity.y = 0; // 上下方向の速度は無視する
        anim.SetFloat("Speed", velocity.magnitude);
    }
}
