using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = default;

    public static GameManager Instance { get => _instance;}

    [SerializeField, Header("スキルマネージャー")]
    SkillManager _skillManager = null;

    [Tooltip("プレイヤー")]
    GameObject _player = null;

    PlayerAnim _playerAnim = null;
    PlayerAttackParam _playerAttackParam = null;
    PlayerHp _playerHp = null;
    PlayerController _playerController = null;
    PlayerUseSkill _playerUseSkill = null;
    public SkillManager SkillManager { get => _skillManager; }
    public GameObject Player { get => _player; }
    public PlayerAnim PlayerAnim { get => _playerAnim; }
    public PlayerAttackParam PlayerAttackParam { get => _playerAttackParam; }
    public PlayerHp PlayerHp { get => _playerHp; }
    public PlayerController PlayerController { get => _playerController; }
    public PlayerUseSkill PlayerUseSkill { get => _playerUseSkill; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _playerAnim = _player.GetComponent<PlayerAnim>();
        _playerAttackParam = _player.GetComponent<PlayerAttackParam>();
        _playerHp = _player.GetComponent<PlayerHp>();
        _playerUseSkill = _player.GetComponent<PlayerUseSkill>();
    }
}
