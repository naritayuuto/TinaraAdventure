using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    int enemyHp = 5000;
    /// <summary>Enemyの速さ</summary>
    [SerializeField]
    float moveSpeed = 3.0f;
    /// <summary>プレイヤーを見つけることができる距離</summary>
    [SerializeField, Header("プレイヤーを見つけられる距離")]
    float playerSensedis = 5f;
    [SerializeField, Header("動き出すまでの時間")]
    float moveTime = 5f;
    [SerializeField, Header("カウント用")]
    float timer = 0f;
    /// <summary>目的地が切り替わる距離</summary>
    [SerializeField, Header("目的地が切り替わる距離")]
    float changeDis = 5f;
    [SerializeField, Header("プレイヤーに攻撃する距離")]
    float attackDis = 1f;
    /// <summary>EnemyのX軸とＺ軸の移動範囲</summary>
    [SerializeField,Header("X軸とＺ軸の移動範囲")]
    float xz = 30f;
    /// <summary>パリィされる時間</summary>
    float parrylimit = 0.5f;

    float enemyPosX;
    float enemyPosZ;
    int pattern = 0;
    GameObject player = null;
    NavMeshAgent agent = null;
    /// <summary>Enemyの生成された初期地点</summary>
    Vector3 enemypos;
    Vector3 targetpos;
    Vector3 destination = new Vector3(0, 0, 0);
    Animator anim = null;
    /// <summary>プレイヤーを見つけているかどうか</summary>
    bool playerFound = false;
    /// <summary>攻撃中かどうか</summary>
    bool attack = false;//パリィ可能な攻撃のみ使う予定
    /// <summary>パリィが出来るかどうか</summary>
    bool parry = false;//

    public int EnemyHp { get => enemyHp; set => enemyHp = value; }
    public bool Parry { get => parry; set => parry = value; }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        enemypos = transform.position;
        destination = transform.position;
        //Debug.Log(Vector3.Distance(transform.position, targetpos));
    }

    // Update is called once per frame
    void Update()
    {
        targetpos = player.transform.position;
        var distance = Vector3.Distance(transform.position, targetpos);
        if (distance >= playerSensedis)//プレイヤー索敵範囲外
        {
            pattern = 1;
        }
        if (distance <= playerSensedis)//プレイヤー索敵範囲内
        {
            playerFound = true;
            pattern = 2;
        }
        switch (pattern)
        {
            case 1:
                if (playerFound)//プレイヤーを見失った時、自分が生成された場所へ戻る
                {
                    destination = enemypos;
                    agent.SetDestination(destination);
                    playerFound = false;
                }
                if (Vector3.Distance(transform.position, destination) <= changeDis)//目的地周辺に来たら
                {
                    moveTime += Time.deltaTime;//立ち止まる時間を作りたいため
                    if (moveTime >= timer)//時間が来たら
                    {
                        MovePosition(transform.position);//自分を中心とした一定範囲の中からランダムで座標計算
                        moveTime -= moveTime;
                    }
                }
                break;
            case 2:
                if (Vector3.Distance(transform.position, targetpos) > attackDis)//見つけているが攻撃が届かない部分の処理
                {
                    agent.SetDestination(targetpos);//目的地を常にプレイヤーに変更 
                }
                break;//switch文を抜ける
        }
    }

    private void MovePosition(Vector3 enemyPos)//目的地計算
    {
        enemyPosX = Random.Range(enemyPos.x - xz, enemyPos.x + xz);
        enemyPosZ = Random.Range(enemyPos.z - xz, enemyPos.z + xz);
        if (enemyPosX > enemypos.x + xz ||
             enemyPosX < enemypos.x - xz &&
             enemyPosZ > enemypos.z + xz ||
             enemyPosZ < enemypos.z - xz)
        {
            MovePosition(enemyPos);//やり直し
        }
        else
        {
            destination = new Vector3(enemyPosX, enemypos.y, enemyPosZ);
            agent.SetDestination(destination);//NavMeshAgentの情報を取得し,新しく目的地（pos）を設定する。
        }
    }
    private void LateUpdate()
    {
        if (anim)
        {
            anim.SetFloat("Speed", agent.velocity.magnitude);
            //anim.SetFloat("Pos", Vector3.Distance(transform.position, targetpos));
        }
    }

    public void Damage(int damage)
    {
        enemyHp -= damage;
        if(enemyHp <= 0)
        {
            Debug.Log("死");
        }
    }
}
