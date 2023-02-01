using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    /// <summary>配列番号</summary>
    [SerializeField]
    int arrayNumber = 0;

    SkillTree skilltree = null;

    Button button = null;

    ///// <summary>スキルが解放されているか</summary>
    //bool skillcheck = false;
    //public int Skillnumber { get => skillnumber; set => skillnumber = value; }
    public int ArrayNumber { get => arrayNumber; set => arrayNumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        skilltree = GetComponent<SkillTree>();//自分が持っているスキルツリーを持ってくる。
        button = GetComponent<Button>();
    }

    public void OnCllik()
    {
        skilltree.SkillPointJudge(skilltree.OpenSkillPoint,arrayNumber,button);
    }
}
