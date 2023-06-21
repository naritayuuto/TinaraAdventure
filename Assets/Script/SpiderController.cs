using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderController : MonoBehaviour,IEnemy
{
    GameObject _player = null;
    Vector3 _targetpos;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {        
        _targetpos = _player.transform.position;
        transform.rotation = Quaternion.LookRotation(_targetpos - transform.position);
    }
    public void Die() { }

    public void AfterDeath()
    {
        GameManager.Instance.Clear();
    }

}
