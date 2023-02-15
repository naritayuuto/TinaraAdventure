using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour//����ɕt����
{
    [SerializeField, Tooltip("�_���[�W�\���p��UI")]
    GameObject damageUi = null;
    [SerializeField, Header("player"), Tooltip("�v���C���[")]
    PlayerController _player = null;
    [Tooltip("damageUi��Text")]
    TextMeshProUGUI damageText = null;
    [Tooltip("�U������������Enemy")]
    EnemyHp _enemyHp = null;
    [Tooltip("player��AttackDamage�̒l������ϐ�")]
    int _playerAttack = 0;
    [Tooltip("����I�ɓ����蔻��������������s�����߂̃^�C�}�[")]
    float _timer = 0;
    [Tooltip("�����蔻����Ƀ����X�^�[�������Ă��Ȃ��ꍇ�ɓ����蔻��������b��")]
    float _colliderActiveTime = 0.5f;
    public Vector3 _Itransform;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUI������܂���");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _timer += Time.deltaTime;
            if (_timer >= _colliderActiveTime)
            {
                gameObject.SetActive(false);
                _timer = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Attack");
            //Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            _playerAttack = (int)Mathf.Round(GameManager.Instance.Player._playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, transform.position, Quaternion.identity);//�_���[�W�\��
            _enemyHp = other.gameObject.GetComponent<EnemyHp>();
            _enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(_enemyHp.Die);
            if (_enemyHp.Die)
            {
                _enemyHp = null;
            }
            transform.position = _Itransform;
            gameObject.SetActive(false);
        }
    }
    public void TransformMove(Vector3 pos)
    {
        _Itransform = transform.position;
        transform.position = pos;
    }
}
