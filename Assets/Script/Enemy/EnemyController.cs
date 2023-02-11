using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField, Tooltip("Enemy�̑���")]
    float _moveSpeed = 3.0f;
    [SerializeField, Header("�v���C���[���������鋗��"), Tooltip("�v���C���[�������邱�Ƃ��ł��鋗��")]
    float _playerSensedis = 10f;
    [SerializeField, Header("�����o���܂ł̎���"), Tooltip("�����o���܂ł̎���")]
    float _moveTime = 5f;
    [SerializeField, Header("�ړI�n���؂�ւ�鋗��"), Tooltip("�ړI�n���؂�ւ�鋗��")]
    float _changeDis = 5f;
    [SerializeField, Header("�v���C���[�ɍU�����鋗��"), Tooltip("�v���C���[�ɍU�����鋗��")]
    float _attackDis = 2.5f;
    [SerializeField, Header("X���Ƃy���̈ړ��͈�"), Tooltip("Enemy��X���Ƃy���̈ړ��͈�")]
    float _xz = 10f;
    [Tooltip("�ړI�n��X���W")]
    float _enemyPosX;
    [Tooltip("�ړI�n��Z���W")]
    float _enemyPosZ;
    //[Header("�U���A�j���[�V�����̐�"), Tooltip("�����_���Ō��߂�A�l�͍U���A�j���[�V�����̌�")]
    //int _attackPattern;
    [SerializeField,Header("�U���A�j���[�V�����̌��{�P�̒l")]
    int _attackMaxNum = 3;
    [Tooltip("1�̎� = player�ȊO��ړI�n�Ƃ���B2�̎� = Player��ڕW�ɂ��A�U���܂ōs��")]
    int _pattern = 0;
    [SerializeField, Header("���̍U���܂ł̑ҋ@����")]
    float _coolActionTime = 3f;
    [Header("�J�E���g�p"), Tooltip("�U���p�̃^�C�}�[")]
    float _timer = 0f;
    GameObject _player = null;
    NavMeshAgent _agent = null;
    [Tooltip("Enemy�̐������ꂽ�����n�_")]
    Vector3 _enemypos;
    [Tooltip("�v���C���[�̒n�_�A�܂��͈ړ��ړI�n")]
    Vector3 _targetpos;
    [Tooltip("�s��")]
    Vector3 _destination = default;
    Animator _anim = null;
    [Tooltip("�v���C���[�������Ă��邩�ǂ���")]
    bool _playerFound = false;
    [Tooltip("�v���C���[���U���o���鋗���̏ꍇtrue")]
    bool _attack = false;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _enemypos = transform.position;
        _destination = transform.position;
        //Debug.Log(Vector3.Distance(transform.position, targetpos));
    }

    // Update is called once per frame
    void Update()
    {
        _targetpos = _player.transform.position;
        var distance = Vector3.Distance(transform.position, _targetpos);
        if (distance >= _playerSensedis)//�v���C���[���G�͈͊O
        {
            _pattern = 1;
        }
        if (distance <= _playerSensedis)//�v���C���[���G�͈͓�
        {
            _playerFound = true;
            _pattern = 2;
        }
        if (_attack)
        {
            _timer += Time.deltaTime;
            if (_timer >= _coolActionTime)
            {
                _anim.SetInteger("AttackPattern", Random.Range(0, _attackMaxNum));
                _timer = 0;
            }
            else
            {
                _anim.SetInteger("AttackPattern", _attackMaxNum);
            }
        }
        switch (_pattern)
        {
            case 1:
                if (_playerFound)//�v���C���[�������������A�������������ꂽ�ꏊ�֖߂�
                {
                    _destination = _enemypos;
                    _agent.SetDestination(_destination);
                    _playerFound = false;
                }
                else if (Vector3.Distance(transform.position, _destination) <= _changeDis)//�ړI�n���ӂɗ�����
                {
                    _moveTime += Time.deltaTime;//�����~�܂鎞�Ԃ���肽������
                    if (_moveTime >= _timer)//���Ԃ�������
                    {
                        MovePosition(transform.position);//�����𒆐S�Ƃ������͈͂̒����烉���_���ō��W�v�Z
                        _moveTime -= _moveTime;
                    }
                }
                break;
            case 2:
                if (Vector3.Distance(transform.position, _targetpos) > _attackDis)//�����Ă��邪�U�����͂��Ȃ������̏���
                {
                    _attack = false;
                    _agent.SetDestination(_targetpos);//�ړI�n����Ƀv���C���[�ɕύX
                }
                else
                {
                    _attack = true;
                    transform.rotation = Quaternion.LookRotation(_targetpos - transform.position);
                }
                break;
        }
    }

    private void MovePosition(Vector3 enemyPos)//�ړI�n�v�Z
    {
        _enemyPosX = Random.Range(enemyPos.x - _xz, enemyPos.x + _xz);
        _enemyPosZ = Random.Range(enemyPos.z - _xz, enemyPos.z + _xz);
        if (_enemyPosX > _enemypos.x + _xz ||
             _enemyPosX < _enemypos.x - _xz &&
             _enemyPosZ > _enemypos.z + _xz ||
             _enemyPosZ < _enemypos.z - _xz)
        {
            MovePosition(enemyPos);//��蒼��
        }
        else
        {
            _destination = new Vector3(_enemyPosX, _enemypos.y, _enemyPosZ);
            _agent.SetDestination(_destination);//NavMeshAgent�̏����擾��,�V�����ړI�n�ipos�j��ݒ肷��B
        }
    }

    private void LateUpdate()
    {
        if (_anim)
        {
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
        }
        //anim.SetFloat("Pos", Vector3.Distance(transform.position, targetpos));
    }
}
