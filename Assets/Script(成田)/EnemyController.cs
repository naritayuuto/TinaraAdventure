using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour//今回は人型なのでgamedev 1-3-5を参考に。
{//ダメージの関数は別にあるので、ここでは大まかな動き、animationをどう使うかを考えて組むこと。
    /// <summary>Enemyの体力</summary>
    [SerializeField]
    int enemyHp = 5000;
    /// <summary>Enemyの速さ</summary>
    [SerializeField]
    float moveSpeed = 3.0f;
    /// <summary>EnemyのX軸とＺ軸の移動範囲</summary>
    [SerializeField]
    float xz = 30f;
    /// <summary>Enemyの動き出す間隔</summary>
    [SerializeField]
    int moveInterval = 10;
    ///// <summary>EnemyのX軸の移動範囲</summary>
    //float enemyMoveRangeX = 0f;
    ///// <summary>EnemyのZ軸の移動範囲</summary>
    //float enemyMoveRangeZ = 0f;
    /// <summary>parryがtrueになった時からカウントする時間</summary>
    float parrytimer = 0.0f;
    /// <summary>動いている間カウントする時間</summary>
    float movetimer = 0.0f;
    /// <summary>パリィされる時間</summary>
    float parrylimit = 0.5f;
    float enemyPosX;
    float enemyPosZ;
    /// <summary>Enemyの初期位置</summary>
    Vector3 enemyInitialPosition;
    bool attack = false;
    bool parry = false;
    /// <summary>playerを見つけたかどうか</summary>
    bool playerSense = false;
    public int EnemyHp { get => enemyHp; set => enemyHp = value; }
    public bool Parry { get => parry; }//攻撃のanimation中に0.5秒間だけtrueにする。
    Animator anim = null;
    Rigidbody rb = null;
    PlayerController playerStatus = null;
    GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = player.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        enemyInitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movetimer += Time.deltaTime;
        var playerpos = player.transform.position;

        if(Vector3.Distance(transform.position, playerpos) > 50)
        {
            if (movetimer > moveInterval)
            {
                MovePosition(transform.position);
                movetimer -= movetimer;
            }
        }
        else
        {
            if ((transform.position - player.transform.position).magnitude > 5)
            {
                var targetpos = transform.position + player.transform.position;
                transform.LookAt(targetpos);
                rb.velocity = targetpos * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
                PlayerSenseAttack();//未完成
            }
        }
        if (parry)
        {
            parrytimer += Time.deltaTime;
            if (parrytimer > parrylimit)
            {
                parry = false;
            }
        }
    }
    private void ParryActive()//攻撃用animation用関数
    {
        parry = true;
    }
    //transform.position = Vector3.MoveTowards(自分の位置, 目的地, speed);
    private void MovePosition(Vector3 enemyPos)
    {//enemyPosxが0、xzが50だった場合、-50～+50まで。
        enemyPosX = Random.Range(enemyPos.x - xz, enemyPos.x + xz);
        enemyPosZ = Random.Range(enemyPos.z - xz, enemyPos.z + xz);
        if (enemyPosX > enemyInitialPosition.x + xz ||
             enemyPosX < enemyInitialPosition.x - xz &&
             enemyPosZ > enemyInitialPosition.z + xz ||
             enemyPosZ < enemyInitialPosition.z - xz)
        {
            MovePosition(enemyPos);
        }
        else
        {
            transform.position = Vector3.MoveTowards(enemyPos, new Vector3(enemyPosX, enemyPos.y, enemyPosZ), moveSpeed / 2);
        }
    }

    private void PlayerSenseAttack()
    {

    }
    //private void OnTriggerEnter(Collider other)//enemyを包み込むようにコライダーを設置する予定
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerSense = false;
    //        player = other.gameObject;
    //    }
    //}
}
