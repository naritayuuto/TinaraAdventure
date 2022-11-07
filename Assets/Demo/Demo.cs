using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField, SerializeReference, SubclassSelector]
    List<ISkill> skill = new List<ISkill>();
    public void Be()
    {
    }
}
