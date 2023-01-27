using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab = null;
    [Tooltip("��������Enemy")]
    GameObject[] _enemys;
    [SerializeField, Header("������")]
    int _generatedNum = 3;
    [Tooltip("�����̈ʒu")]
    private Vector3 _pos;
    [SerializeField, Header("Enemy�𐶐�����c�͈�")]
    float _generatPosX = 0f;
    [SerializeField, Header("Enemy�𐶐����鉡�͈�")]
    float _generatPosZ = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _enemys = new GameObject[_generatedNum];
        _pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Generat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(var enemy in _enemys)
            {
                Destroy(enemy);
            }
        }
    }
    void Generat()
    {
        for (int i = 0; i < _generatedNum; i++)
        {
            _generatPosX = Random.Range(_pos.x - _generatPosX, _pos.x + _generatPosX);
            _generatPosZ = Random.Range(_pos.z - _generatPosZ, _pos.z + _generatPosZ);
            GameObject enemy = Instantiate(_enemyPrefab, new Vector3(_generatPosX, _pos.y, _generatPosZ), Quaternion.identity);
            _enemys[i] = enemy;
        }
    }
}
