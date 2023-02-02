using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour//playerに付いているscript全てをpublicで設定する。スキルを使う時に呼ぶため。
{
    /// <summary>playerの速さ</summary>
    [SerializeField,Tooltip("playerの速度"),Header("動く速度")]
    float _moveSpeed = 3f;
    /// <summary>playerの回転速度</summary>
    [SerializeField]
    float _turnSpeed = 3f;
    [SerializeField,Tooltip("スキルを使用するために押すボタン"),Header("PlayerUseCanvasのSkillButton")]
    GameObject _button = null; 
    /// <summary>時間</summary>
    private float _timer = 0.0f;
    /// <summary>攻撃判定用コライダーのアクティブタイム</summary>
    private float attackJudgeTime = 0.5f;
    /// <summary>ガード判定用コライダーのアクティブタイム</summary>
    private float guardJudgeTime = 0.5f;
    /// <summary>防御判定用,コライダーActive用</summary>
    bool guard = false;
    /// <summary>パリィ判定用</summary>
    bool parrysuccess = false;
    public PlayerHp _playerHp = null;
    public PlayerAnim _playerAnimAndcollider = null;
    public PlayerUseSkill _playerSkill = null;
    public PlayerAttackParam _playerAttackParam = null;
    Rigidbody _rb = default;
    public bool Guard { get => guard; set => guard = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public Rigidbody Rb { get => _rb;}

    /// <summary>入力された方向の XZ 平面でのベクトル</summary>

    private void Awake()
    {
        GameManager.Instance.GetPlayerObject(this);
    }
    void Start()
    {
        _playerAnimAndcollider = GetComponent<PlayerAnim>();
        _playerSkill = GetComponent<PlayerUseSkill>();
        if (!_button) Debug.LogError("ボタンをセットしてください");
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
    }
}
