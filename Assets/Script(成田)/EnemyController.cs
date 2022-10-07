using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour//今回は人型なのでgamedev 1-3-5を参考に。
{//ダメージの関数は別にあるので、ここでは大まかな動き、animationをどう使うかを考えて組むこと。
    /// <summary>Enemyの体力</summary>
    [SerializeField]
    int enemyHp = 5000;
    /// <summary>Enemyの速さ</summary>
    [SerializeField]
    float moveSpeed = 3.0f;
    /// <summary>EnemyのX軸とＺ軸の移動距離</summary>
    [SerializeField]
    float xz = 0f;
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
    public bool Parry { get => parry;}//攻撃のanimation中に0.5秒間だけtrueにする。
    Animator anim = null;
    Rigidbody rb = null;
    PlayerController player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        enemyInitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movetimer += Time.deltaTime;
        if(!playerSense)
        {
            if (movetimer > moveInterval)
            {
                MovePosition(transform.position);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position, moveSpeed);
        }
        if(parry)
        {
            parrytimer += Time.deltaTime;
            if(parrytimer > parrylimit)
            {
                parry = false;
            }
        }
    }
    private void ParryActive()//攻撃用animation用関数、
    {
        parry = true;
    }
    //transform.position = Vector3.MoveTowards(自分の位置, 目的地, speed);

    private void MovePosition(Vector3 enemyPos)
    {//enemyPosxが0、xzが50だった場合、-50～+50まで。
        enemyPosX = Random.Range(enemyPos.x - xz, enemyPos.x + xz);
        enemyPosZ = Random.Range(enemyPos.z - xz, enemyPos.z + xz);
        if ( enemyPosX > enemyInitialPosition.x + xz || 
             enemyPosX < enemyInitialPosition.x - xz &&
             enemyPosZ > enemyInitialPosition.z + xz ||
             enemyPosZ < enemyInitialPosition.z - xz)
        {
            MovePosition(enemyPos);
        }
        else
        {
            transform.position = Vector3.MoveTowards(enemyPos, new Vector3(enemyPosX, enemyPos.y, enemyPosZ), moveSpeed);
        }
    }

    private void PlayerSenseAttack()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerSense = false;
        }
    }
}
