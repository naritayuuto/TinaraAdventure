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

    [SerializeField,Header("PlayerPrefab"),Tooltip("�v���C���[�̃I�u�W�F�N�g")]
    GameObject _player = null;

    public SkillManager SkillManager { get => _skillManager; }
    public GameObject Player { get => _player; }

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
    }
}
