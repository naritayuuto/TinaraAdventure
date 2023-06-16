using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    SkillTree skilltree = null;

    Button button = null;

    ///// <summary>スキルが解放されているか</summary>
    //bool skillcheck = false;
    //public int Skillnumber { get => skillnumber; set => skillnumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        skilltree = GetComponent<SkillTree>();//自分が持っているスキルツリーを持ってくる。
        button = GetComponent<Button>();
    }

    public void OnCllik()
    {
        skilltree.SkillPointJudge(skilltree.OpenSkillPoint,skilltree._arrayNumber,button);
    }
}
