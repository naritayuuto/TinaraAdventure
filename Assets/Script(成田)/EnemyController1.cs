using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController1 : MonoBehaviour
{
    [SerializeField]
    int enemyHp = 5000;
    /// <summary>Enemyの速さ</summary>
    [SerializeField]
    float moveSpeed = 3.0f;
    /// <summary>プレイヤーを見つけることができる距離</summary>
    [SerializeField]
    float playerSensedis = 5f;
    [SerializeField]
    float moveTimer = 5f;
    [SerializeField]
    float time = 5f;
    /// <summary>目的地が切り替わる距離</summary>
    [SerializeField]
    float changepos = 5f;
    /// <summary>EnemyのX軸とＺ軸の移動範囲</summary>
    [SerializeField]
    float xz = 30f;
    float enemyPosX;
    float enemyPosZ;
    GameObject player = null;
    NavMeshAgent agent = null;
    /// <summary>Enemyの生成された初期地点</summary>
    Vector3 enemypos;
    Vector3 targetpos;
    Vector3 destination = new Vector3(0, 0, 0);
    Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemypos = transform.position;
        destination = transform.position;
        Debug.Log(Vector3.Distance(transform.position, targetpos));
    }

    // Update is called once per frame
    void Update()
    {
        targetpos = player.transform.position;
        var distance = Vector3.Distance(transform.position, targetpos);
        if (distance >= playerSensedis)//範囲外
        {
            if (Vector3.Distance(transform.position,destination) <= changepos)//目的地周辺に来たら
            {
                moveTimer += Time.deltaTime;
                if (moveTimer >= time)//時間が来たら
                {
                    MovePosition(transform.position);//新しく目的地をセット
                    moveTimer -= moveTimer;
                }
            }
        }
        else if (distance <= playerSensedis)//範囲内
        {
            agent.SetDestination(targetpos);
            if (Vector3.Distance(transform.position, targetpos) <= 1f)
            {
                //animationを流す。
            }
        }
    }
    private void MovePosition(Vector3 enemyPos)
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
            anim.SetFloat("Pos", Vector3.Distance(transform.position, targetpos));
        }
    }
}
