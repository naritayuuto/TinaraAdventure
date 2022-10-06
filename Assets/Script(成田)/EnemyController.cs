using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour//今回は人型なのでgamedev 1-3-5を参考に。
{//ダメージの関数は別にあるので、ここでは大まかな動き、animationをどう使うかを考えて組むこと。
    /// <summary>Enemyの体力<summary>
    [SerializeField]
    int enemyHp = 5000;
    /// <summary>Enemyの速さ<summary>
    [SerializeField]
    float moveSpeed = 3.0f;
    /// <summary>transformのx<summary>
    float x = 0;
    /// <summary>transformのz<summary>
    float z = 0;
    /// <summary>時間<summary>
    float timer = 0.0f;
    /// <summary>パリィされる時間<summary>
    float parrylimit = 0.5f;
    Animator anim = null;
    bool attack = false;
    bool parry = false;
    public int EnemyHp { get => enemyHp; set => enemyHp = value; }
    public bool Parry { get => parry;}//攻撃のanimation中に0.5秒間だけtrueにする。

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(parry)
        {
            timer += Time.deltaTime;
            if(timer > parrylimit)
            {
                parry = false;
            }
        }
    }
    private void ParryActive()
    {
        parry = true;
    }
}
