using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeLookAround : MonoBehaviour
{
    [SerializeField]
    SlimeController _slimeController = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _slimeController._playerFound = true;
            Debug.Log(true);
        }
    }

}
