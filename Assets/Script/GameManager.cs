using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = default;

    public static GameManager Instance { get => _instance;}

    [SerializeField, Header("�X�L���}�l�[�W���[")]
    SkillManager _skillManager = null;

    [SerializeField,Header("Player"),Tooltip("�v���C���[")]
    PlayerController _player = null;

    public PlayerController Player { get => _player; }
    public SkillManager SkillManager { get => _skillManager;}

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
    }
}
