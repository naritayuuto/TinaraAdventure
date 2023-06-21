using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour,IEnemy
{
    [SerializeField, Tooltip("Enemyの速さ")]
    float _moveSpeed = 3.0f;
    [SerializeField, Header("動き出すまでの時間"), Tooltip("動き出すまでの時間")]
    float _moveTime = 5f;
    [SerializeField, Header("目的地が切り替わる距離"), Tooltip("目的地が切り替わる距離")]
    float _changeDis = 5f;
    [SerializeField, Header("プレイヤーに攻撃する距離"), Tooltip("プレイヤーに攻撃する距離")]
    float _attackDis = 2.5f;
    [SerializeField, Header("次の攻撃までの待機時間")]
    float _coolActionTime = 3f;
    [Header("カウント用"), Tooltip("攻撃用のタイマー")]
    float _timer = 0f;
    [SerializeField, Header("X軸とＺ軸の移動範囲"), Tooltip("EnemyのX軸とＺ軸の移動範囲")]
    float _xz = 10f;
    [Tooltip("目的地のX座標")]
    float _enemyPosX;
    [Tooltip("目的地のZ座標")]
    float _enemyPosZ;
    [SerializeField, Header("攻撃アニメーションの個数-1の値")]
    int _attackMaxNum = 1;
    [Tooltip("1の時 = player以外のランダムな座標を目的地とする。2の時 = Playerを目標にし、攻撃まで行う")]
    int _movePattern = 0;
    [Tooltip("Enemyの生成された初期地点")]
    Vector3 _enemypos;
    [Tooltip("プレイヤーの座標")]
    Vector3 _targetpos;
    [Tooltip("行先")]
    Vector3 _destination = default;
    [Tooltip("プレイヤーが見つかったらTrue")]
    public bool _playerFound = false;
    [Tooltip("プレイヤーを攻撃出来る距離の場合true")]
    bool _attack = false;
    NavMeshAgent _agent = null;
    Animator _anim = null;
    GameObject _player = null;
    [SerializeField,Header("攻撃判定用collider")]
    Collider _collider = null;
    bool _die = false;

    // Start is called before the first frame update
    void Start()
    {
        _collider.enabled = false;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _player = GameManager.Instance.Player;
        _enemypos = transform.position;
        _destination = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_die)
        {
            if (_playerFound)
            {
                _targetpos = _player.transform.position;
                _movePattern = 2;
            }
            else
            {
                _movePattern = 1;
            }
            if (_attack)
            {
                _timer += Time.deltaTime;
                if (_timer >= _coolActionTime)
                {
                    _anim.SetInteger("AttackPattern", Random.Range(0, _attackMaxNum));
                    _timer = 0;
                }
            }
            switch (_movePattern)
            {
                case 1:
                    if (Vector3.Distance(transform.position, _destination) <= _changeDis)//目的地周辺に来たら
                    {
                        _moveTime += Time.deltaTime;//立ち止まる時間を作りたいため
                        if (_moveTime >= _timer)//時間が来たら
                        {
                            MovePosition(_enemypos);//生成初期地を中心とした一定範囲の中からランダムで座標計算
                            _moveTime = 0;
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
                        _agent.SetDestination(transform.position);
                        //プレイヤーの方向を向く
                        transform.rotation = Quaternion.LookRotation(_targetpos - transform.position);
                    }
                    break;
            }
        }
    }
    private void LateUpdate()
    {
        if (_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
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
    public void ColliderActive()
    {
        _collider.enabled = true;
    }

    public void Die()
    {
        _die = true;
    }
}
