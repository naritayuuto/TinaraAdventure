using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField, Tooltip("Enemyの速さ")]
    float _moveSpeed = 3.0f;
    [SerializeField, Header("プレイヤーを見つけられる距離"), Tooltip("プレイヤーを見つけることができる距離")]
    float _playerSensedis = 10f;
    [SerializeField, Header("動き出すまでの時間"), Tooltip("動き出すまでの時間")]
    float _moveTime = 5f;
    [SerializeField, Header("目的地が切り替わる距離"), Tooltip("目的地が切り替わる距離")]
    float _changeDis = 5f;
    [SerializeField, Header("プレイヤーに攻撃する距離"), Tooltip("プレイヤーに攻撃する距離")]
    float _attackDis = 2.5f;
    [SerializeField, Header("X軸とＺ軸の移動範囲"), Tooltip("EnemyのX軸とＺ軸の移動範囲")]
    float _xz = 10f;
    [Tooltip("目的地のX座標")]
    float _enemyPosX;
    [Tooltip("目的地のZ座標")]
    float _enemyPosZ;
    //[Header("攻撃アニメーションの数"), Tooltip("ランダムで決める、値は攻撃アニメーションの個数")]
    //int _attackPattern;
    [SerializeField,Header("攻撃アニメーションの個数＋１の値")]
    int _attackMaxNum = 3;
    [Tooltip("1の時 = player以外を目的地とする。2の時 = Playerを目標にし、攻撃まで行う")]
    int _pattern = 0;
    [SerializeField, Header("次の攻撃までの待機時間")]
    float _coolActionTime = 3f;
    [Header("カウント用"), Tooltip("攻撃用のタイマー")]
    float _timer = 0f;
    GameObject _player = null;
    NavMeshAgent _agent = null;
    [Tooltip("Enemyの生成された初期地点")]
    Vector3 _enemypos;
    [Tooltip("プレイヤーの地点、または移動目的地")]
    Vector3 _targetpos;
    [Tooltip("行先")]
    Vector3 _destination = default;
    Animator _anim = null;
    [Tooltip("プレイヤーを見つけているかどうか")]
    bool _playerFound = false;
    [Tooltip("プレイヤーを攻撃出来る距離の場合true")]
    bool _attack = false;

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
        if (_attack)
        {
            _timer += Time.deltaTime;
            if (_timer >= _coolActionTime)
            {
                _anim.SetInteger("AttackPattern", Random.Range(0, _attackMaxNum));
                _timer = 0;
            }
            else
            {
                _anim.SetInteger("AttackPattern", _attackMaxNum);
            }
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
                else if (Vector3.Distance(transform.position, _destination) <= _changeDis)//目的地周辺に来たら
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
                    _attack = false;
                    _agent.SetDestination(_targetpos);//目的地を常にプレイヤーに変更
                }
                else
                {
                    _attack = true;
                    transform.rotation = Quaternion.LookRotation(_targetpos - transform.position);
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
}
