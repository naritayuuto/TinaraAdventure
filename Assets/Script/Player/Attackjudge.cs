using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attackjudge : MonoBehaviour//����ɕt����
{
    [SerializeField, Tooltip("�_���[�W�\���p��UI")]
    GameObject damageUi = null;
    [Tooltip("damageUi��Text")]
    TextMeshProUGUI damageText = null;
    [Tooltip("player��AttackDamage�̒l������ϐ�")]
    int _playerAttack = 0;
    [Tooltip("����I�ɓ����蔻��������������s�����߂̃^�C�}�[")]
    float _timer = 0;
    [Tooltip("�����蔻����ɕW�I�������Ă��Ȃ��ꍇ�ɓ����蔻��������b��")]
    float _colliderActiveTime = 0.5f;
    [Tooltip("�����蔻��")]
    Collider _collider = default;
    public Vector3 _iTransform;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUI������܂���");
        }
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    private void Update()
    {
        if (_collider.enabled == true)
        {
            _timer += Time.deltaTime;
            if (_timer >= _colliderActiveTime)
            {
                _collider.enabled = false;
                _timer = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHp>(out EnemyHp enemyHp))
        {
            _playerAttack = (int)Mathf.Round(GameManager.Instance.Player._playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, transform.position, Quaternion.identity);//�_���[�W�\��
            enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(enemyHp.Die);
            transform.position = _iTransform;
            _collider.enabled = false;
        }
    }
    public void TransformMove(Vector3 pos)
    {
        _iTransform = transform.position;
        transform.position = pos;
    }

    public void ColliderActive()
    {
        _collider.enabled = true;
    }
}
