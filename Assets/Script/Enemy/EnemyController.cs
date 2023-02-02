using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    int _enemyHp = 5000;
    /// <summary>Enemyの速さ</summary>
    [SerializeField]
    float _moveSpeed = 3.0f;
    /// <summary>プレイヤーを見つけることができる距離</summary>
    [SerializeField, Header("プレイヤーを見つけられる距離")]
    float _playerSensedis = 5f;
    [SerializeField, Header("動き出すまでの時間")]
    float _moveTime = 5f;
    [SerializeField, Header("カウント用")]
    float _timer = 0f;
    /// <summary>目的地が切り替わる距離</summary>
    [SerializeField, Header("目的地が切り替わる距離")]
    float _changeDis = 5f;
    [SerializeField, Header("プレイヤーに攻撃する距離")]
    float _attackDis = 1f;
    /// <summary>EnemyのX軸とＺ軸の移動範囲</summary>
    [SerializeField, Header("X軸とＺ軸の移動範囲")]
    float _xz = 30f;
    /// <summary>パリィされる時間</summary>
    float _parrylimit = 0.5f;

    float _enemyPosX;
    float _enemyPosZ;
    int _pattern = 0;
    GameObject _player = null;
    NavMeshAgent _agent = null;
    /// <summary>Enemyの生成された初期地点</summary>
    Vector3 _enemypos;
    /// <summary>プレイヤーの地点、または移動目的地</summary>
    Vector3 _targetpos;
    Vector3 _destination = new Vector3(0, 0, 0);
    Animator _anim = null;
    /// <summary>プレイヤーを見つけているかどうか</summary>
    bool _playerFound = false;
    ///// <summary>攻撃中かどうか</summary>
    //bool attack = false;//パリィ可能な攻撃のみ使用予定
    /// <summary>パリィが出来るかどうか</summary>
    bool _parry = false;//アニメーションイベントで使用予定
    [Tooltip("死んだかどうか")]
    bool _die = false;
    public int EnemyHp { get => _enemyHp; set => _enemyHp = value; }
    public bool Parry { get => _parry; set => _parry = value; }
    public bool Die { get => _die;}

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _enemypos = transform.position;
        _destination = transform.position;
        //Debug.Log(Vector3.Distance(transform.position, targetpos));
    }

    // Update is called once per frame
    void Update()
    {
        _targetpos = _player.transform.position;
        var distance = Vector3.Distance(transform.position, _targetpos);
        if (distance >= _playerSensedis)//プレイヤー索敵範囲外
        {
            _pattern = 1;
        }
        if (distance <= _playerSensedis)//プレイヤー索敵範囲内
        {
            _playerFound = true;
            _pattern = 2;
        }
        switch (_pattern)
        {
            case 1:
                if (_playerFound)//プレイヤーを見失った時、自分が生成された場所へ戻る
                {
                    _destination = _enemypos;
                    _agent.SetDestination(_destination);
                    _playerFound = false;
                }
                if (Vector3.Distance(transform.position, _destination) <= _changeDis)//目的地周辺に来たら
                {
                    _moveTime += Time.deltaTime;//立ち止まる時間を作りたいため
                    if (_moveTime >= _timer)//時間が来たら
                    {
                        MovePosition(transform.position);//自分を中心とした一定範囲の中からランダムで座標計算
                        _moveTime -= _moveTime;
                    }
                }
                break;
            case 2:
                if (Vector3.Distance(transform.position, _targetpos) > _attackDis)//見つけているが攻撃が届かない部分の処理
                {
                    _agent.SetDestination(_targetpos);//目的地を常にプレイヤーに変更 
                }
                else
                {
                    //攻撃アニメーションの再生。
                }
                break;
        }
    }

    private void MovePosition(Vector3 enemyPos)//目的地計算
    {
        _enemyPosX = Random.Range(enemyPos.x - _xz, enemyPos.x + _xz);
        _enemyPosZ = Random.Range(enemyPos.z - _xz, enemyPos.z + _xz);
        if (_enemyPosX > _enemypos.x + _xz ||
             _enemyPosX < _enemypos.x - _xz &&
             _enemyPosZ > _enemypos.z + _xz ||
             _enemyPosZ < _enemypos.z - _xz)
        {
            MovePosition(enemyPos);//やり直し
        }
        else
        {
            _destination = new Vector3(_enemyPosX, _enemypos.y, _enemyPosZ);
            _agent.SetDestination(_destination);//NavMeshAgentの情報を取得し,新しく目的地（pos）を設定する。
        }
    }
    private void LateUpdate()
    {
        if (_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
        }
        //anim.SetFloat("Pos", Vector3.Distance(transform.position, targetpos));
    }

    public void Damage(int damage)
    {
        _enemyHp -= damage;
        if (_enemyHp <= 0)
        {
            _die = true;
            //アニメーションを流す、当たり判定となっているコライダーのリストを作っておき、当たり判定を消す。
            Debug.Log("敵が死んだ");
        }
    }
}
