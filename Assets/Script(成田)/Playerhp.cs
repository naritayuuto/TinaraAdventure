using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Playerhp : MonoBehaviour
{
    /// <summary> playerの体力</summary>
    [SerializeField]
    private int playerHp = 5000;
    /// <summary> playerの増減する体力</summary>
    [SerializeField]
    private int playerDamagehp = 0;
    /// <summary>プレイヤーのHP表示用テキスト</summary>
    [SerializeField]
    Text playerHpText = null;
    [SerializeField]
    Slider hpslider = null;
    public int PlayerDamagehp { get => playerDamagehp; set => playerDamagehp = value; }
    // Start is called before the first frame update
    void Start()
    {
        playerDamagehp = playerHp;
    }

    // Update is called once per frame
    void Update()
    {
        playerHpText.text = playerDamagehp.ToString() + "/" + playerHp.ToString();
    }

    void Damage(int damage)
    {
        playerDamagehp -= damage;
        hpslider.value = playerDamagehp / playerHp;
    }
}
