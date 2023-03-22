using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Attackjudge : MonoBehaviour//����ɕt����
{
    [SerializeField, Tooltip("�_���[�W�\���p��UI")]
    GameObject damageUi = null;
    [Tooltip("damageUi��Text")]
    TextMeshProUGUI damageText = null;
    [Tooltip("player��AttackDamage�̒l������ϐ�")]
    int _playerAttack = 0;
    [Tooltip("hitstop��������b��")]
    float _hitStopTime = 0.15f;
    [Tooltip("�����蔻��")]
    Collider _collider = default;
    PlayerAttackParam _playerAttackParam = null;
    PlayerAnim _playerAnim = null;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUI������܂���");
        }
        _playerAttackParam = GameManager.Instance.PlayerAttackParam;
        _playerAnim = GameManager.Instance.PlayerAnim;
        damageText = damageUi.GetComponentInChildren<TextMeshProUGUI>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHp>(out EnemyHp enemyHp))
        {
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);
            _playerAttack = (int)Mathf.Round(_playerAttackParam.AttackDamage);
            damageText.text = _playerAttack.ToString();
            Instantiate(damageUi, hitPos, Quaternion.identity);//�_���[�W�\��
            enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(enemyHp.Die);
            HitStop();
            _collider.enabled = false;
        }
    }
    private void HitStop()
    {
        //�A�j���[�V�������~�߂鎖�Ńq�b�g�X�g�b�v���Č�
        _playerAnim.Anim.speed = 0f;

        var sequence = DOTween.Sequence();
        sequence.SetDelay(_hitStopTime);
        // ���[�V�������ĊJ
        sequence.AppendCallback(() => _playerAnim.Anim.speed = 1f);
    }
}
