using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab = null;
    [Tooltip("生成したEnemy")]
    GameObject[] _enemys;
    [SerializeField, Header("生成数")]
    int _generatedNum = 3;
    [Tooltip("自分の位置")]
    private Vector3 _pos;
    [SerializeField, Header("Enemyを生成する縦範囲")]
    float _generatPosX = 0f;
    [SerializeField, Header("Enemyを生成する横範囲")]
    float _generatPosZ = 0f;
    [SerializeField, Tooltip("プレイヤーが範囲外に出てからEnemyが生きていられる秒数")]
    float _exitTime = 30f;
    float _timer = 0f;
    
    bool _exitCheck = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemys = new GameObject[_generatedNum];
        _pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _timer = _exitCheck ? _timer += Time.deltaTime : 0;

        if(_timer >= _exitTime && _exitCheck)
        {
            foreach (var enemy in _enemys)
            {
                Destroy(enemy);
            }
            _timer = 0;
            _exitCheck = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("入った");
            Generat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        _exitCheck = true;
        Debug.Log("出た");
    }
    void Generat()
    {
        if (!_exitCheck)
        {
            for (int i = 0; i < _generatedNum; i++)
            {
                float generatPosX = Random.Range(_pos.x - _generatPosX, _pos.x + _generatPosX);
                float generatPosZ = Random.Range(_pos.z - _generatPosZ, _pos.z + _generatPosZ);
                GameObject enemy = Instantiate(_enemyPrefab, new Vector3(generatPosX, _pos.y, generatPosZ), Quaternion.identity);
                _enemys[i] = enemy;
            }
            Debug.Log("生成");
        }
        else
        {
            Debug.Log("生成失敗");
            _exitCheck = false;
        }
    }
}
