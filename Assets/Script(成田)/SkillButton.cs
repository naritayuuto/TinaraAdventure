using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillId
{ 
    heal = 1,
    attack,
    buff
}
public class SkillButton : MonoBehaviour
{
    [SerializeField]
    SkillId skillId = SkillId.heal;
    /// <summary>何系統の何番目のスキルなのか</summary>
    [SerializeField]
    int skillnumber = 0;
    /// <summary>解放するために必要なポイント</summary>
    [SerializeField]
    int skillpoint = 0;
    [SerializeField]
    Image lineimage = null;

    Skilltree skilltree = null;

    Button button = null;

    PlayerController player = null;
    /// <summary>スキルが解放されているか</summary>
    bool skillcheck = false;
    public int Skillnumber { get => skillnumber; set => skillnumber = value; }
    public bool Skillcheck { get => skillcheck; set => skillcheck = value; }
    public SkillId SkillId { get => skillId; set => skillId = value; }
    public int Skillpoint { get => skillpoint; set => skillpoint = value; }
    // Start is called before the first frame update
    void Start()
    {
        skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCllik()
    {
        skilltree.SkillPointJudge(skillpoint,skillnumber,(int)skillId);
    }

    public void Yobidasi()
    {
       if(skillcheck)
        {
            Debug.Log(skillId + "の" + skillnumber + "番目呼ばれました");
          //player.AddSkill((int)skillId, skillnumber);
            player.Skillpoint -= skillpoint;
            button.interactable = false;
            if(lineimage)
            {
                lineimage.color = Color.black;
            }
        }
       else
        {
            Debug.Log("まだ開放されていません");
        }
    }
}
