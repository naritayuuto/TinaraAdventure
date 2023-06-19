using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLine : MonoBehaviour
{
    List<Image> childs = new List<Image>();
    [Tooltip("”¼“§–¾‚É‚·‚é‚½‚ß‚É“§–¾“x‚Ö‘ã“ü‚·‚é’l")]
    const int _colorNum = 155;
    const int _maxColor = 255;
    // Start is called before the first frame update
    void Start()
    {
        int childCount = gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            childs.Add(child.GetComponent<Image>());
        }
    }
    public void LineNotActive()
    {
        foreach(var child in childs)
        {
            child.color = new Color32(_maxColor,_maxColor,_maxColor,_colorNum);
        }
    }
}
