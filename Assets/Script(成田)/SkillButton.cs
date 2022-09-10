using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillId
{ 
    heal = 1,
    attack1,
    attack2,
    attack3,
    attack4,
    attack5
}

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    SkillId skillId = SkillId.heal;
    /// <summary>‰½”Ô–Ú‚ÌƒXƒLƒ‹‚È‚Ì‚©</summary>
    int skillnumber = 0;
    /// <summary>‰ğ•ú‚³‚ê‚Ä‚¢‚é‚©</summary>
    bool skillcheck = false;
    public int Skillnumber { get => skillnumber; set => skillnumber = value; }
    public bool Skillcheck { get => skillcheck; set => skillcheck = value; }
    public SkillId SkillId { get => skillId; set => skillId = value; }

    Skilltree skilltree = null;
    Button button = null;
    // Start is called before the first frame update
    void Start()
    {
        skilltree = GameObject.FindGameObjectWithTag("Skilltree").GetComponent<Skilltree>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCllik()
    {
        skilltree.SkillJudge(skillnumber);
    }

    public void Yobidasi()
    {
       if(skillcheck)
        {
            Debug.Log(skillnumber + "”Ô–ÚŒÄ‚Î‚ê‚Ü‚µ‚½");
            //player‚Éskill’Ç‰Á‚Ìˆ—‚ğ‘‚­
            button.interactable = false;
        }
       else
        {
            Debug.Log("‚Ü‚¾ŠJ•ú‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
        }
    }
}
