using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour//移動のanimationと処理、ボタンを取ってきてboolを確認、trueの場合skillを使えるようにする
{
    [SerializeField]
    float speed = 1.0f;
    Skilltree skilltree = null;
    Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
