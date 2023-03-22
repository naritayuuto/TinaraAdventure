using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Attackjudge : MonoBehaviour//武器に付ける
{
    [SerializeField, Tooltip("ダメージ表示用のUI")]
    GameObject damageUi = null;
    [Tooltip("damageUiのText")]
    TextMeshProUGUI damageText = null;
    [Tooltip("playerのAttackDamageの値を入れる変数")]
    int _playerAttack = 0;
    [Tooltip("hitstopがかかる秒数")]
    float _hitStopTime = 0.15f;
    [Tooltip("当たり判定")]
    Collider _collider = default;
    PlayerAttackParam _playerAttackParam = null;
    PlayerAnim _playerAnim = null;
    private void Start()
    {
        if (!damageUi)
        {
            Debug.LogError("damageUIがありません");
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
            Instantiate(damageUi, hitPos, Quaternion.identity);//ダメージ表示
            enemyHp.Damage(_playerAttack);
            GameManager.Instance.SkillManager.AddSkillPoint(enemyHp.Die);
            HitStop();
            _collider.enabled = false;
        }
    }
    private void HitStop()
    {
        //アニメーションを止める事でヒットストップを再現
        _playerAnim.Anim.speed = 0f;

        var sequence = DOTween.Sequence();
        sequence.SetDelay(_hitStopTime);
        // モーションを再開
        sequence.AppendCallback(() => _playerAnim.Anim.speed = 1f);
    }
}
