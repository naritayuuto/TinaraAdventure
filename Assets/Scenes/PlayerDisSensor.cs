using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisSensor : MonoBehaviour
{
    SpiderAttackParam _sPAttackParam = null;
    private void OnTriggerEnter(Collider other)
    {
        _sPAttackParam._playerDis = true;
    }
}
