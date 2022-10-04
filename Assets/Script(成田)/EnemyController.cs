using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour//今回は人型なのでgamedev 1-3-5を参考に。
{//ダメージの関数は別にあるので、ここでは大まかな動き、animationをどう使うかを考えて組むこと。
    [SerializeField]
    int enemyHp = 5000;
    [SerializeField]
    float moveSpeed = 3.0f;
    float x = 0;
    float z = 0;
    Animator anim = null;
    public int EnemyHp { get => enemyHp; set => enemyHp = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
