using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    SkillTree skilltree = null;

    Button button = null;

    ///// <summary>�X�L�����������Ă��邩</summary>
    //bool skillcheck = false;
    //public int Skillnumber { get => skillnumber; set => skillnumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        skilltree = GetComponent<SkillTree>();//�����������Ă���X�L���c���[�������Ă���B
        button = GetComponent<Button>();
    }

    public void OnCllik()
    {
        skilltree.SkillPointJudge(skilltree.OpenSkillPoint,skilltree._arrayNumber,button);
    }
}
