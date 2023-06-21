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
    [SerializeField, Tooltip("�v���C���[���͈͊O�ɏo�Ă���Enemy�������Ă�����b��")]
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
            Debug.Log("������");
            Generat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        _exitCheck = true;
        Debug.Log("�o��");
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
            Debug.Log("����");
        }
        else
        {
            Debug.Log("�������s");
            _exitCheck = false;
        }
    }
}
