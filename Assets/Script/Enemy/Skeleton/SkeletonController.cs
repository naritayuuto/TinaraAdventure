using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SkeletonController : MonoBehaviour
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
    [SerializeField, Header("���̍U���܂ł̑ҋ@����")]
    float _coolActionTime = 3f;
    [Header("�J�E���g�p"), Tooltip("�U���p�̃^�C�}�[")]
    float _timer = 0f;
    [SerializeField, Header("X���Ƃy���̈ړ��͈�"), Tooltip("Enemy��X���Ƃy���̈ړ��͈�")]
    float _xz = 10f;
    [Tooltip("�ړI�n��X���W")]
    float _enemyPosX;
    [Tooltip("�ړI�n��Z���W")]
    float _enemyPosZ;
    [SerializeField, Header("��������̓����蔻��"), Tooltip("��������̓����蔻��")]
    Collider _weaponL;
    [SerializeField, Header("�E������̓����蔻��"), Tooltip("�E������̓����蔻��")]
    Collider _weaponR;
    //[Header("�U���A�j���[�V�����̐�"), Tooltip("�����_���Ō��߂�A�l�͍U���A�j���[�V�����̌�")]
    //int _attackPattern;
    [SerializeField,Header("�U���A�j���[�V�����̌�-1�̒l")]
    int _attackMaxNum = 2;
    [Tooltip("1�̎� = player�ȊO�̃����_���ȍ��W��ړI�n�Ƃ���B2�̎� = Player��ڕW�ɂ��A�U���܂ōs��")]
    int _movePattern = 0;
    [Tooltip("Enemy�̐������ꂽ�����n�_")]
    Vector3 _enemypos;
    [Tooltip("�v���C���[�̍��W")]
    Vector3 _targetpos;
    [Tooltip("�s��")]
    Vector3 _destination = default;
    GameObject _player = null;
    NavMeshAgent _agent = null;
    Animator _anim = null;
    [Tooltip("�v���C���[�������Ă��邩�ǂ���")]
    bool _playerFound = false;
    [Tooltip("�v���C���[���U���o���鋗���̏ꍇtrue")]
    bool _attack = false;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameManager.Instance.Player;
        _anim = GetComponent<Animator>();
        _enemypos = transform.position;
        //�ړ��ꏊ�����߂�B
        MovePosition(_enemypos);
        //Debug.Log(Vector3.Distance(transform.position, targetpos));
    }

    // Update is called once per frame
    void Update()
    {
        _targetpos = _player.transform.position;
        var distance = Vector3.Distance(transform.position, _targetpos);
        if (distance >= _playerSensedis)//�v���C���[���G�͈͊O
        {
            _movePattern = 1;
        }
        if (distance <= _playerSensedis)//�v���C���[���G�͈͓�
        {
            _playerFound = true;
            _movePattern = 2;
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
        switch (_movePattern)
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
                        MovePosition(_enemypos);//���������n�𒆐S�Ƃ������͈͂̒����烉���_���ō��W�v�Z(���������ĕςȂƂ���ɍs���Ȃ��悤��)
                        _moveTime = 0;
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
                    //�v���C���[�̕���������
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

    //�A�j���[�V�����C�x���g�p�֐���
    public void LWeaponActive()
    {
        _weaponL.enabled = true;
    }
    public void RWeaponActive()
    {
        _weaponR.enabled = true;
    }
    public void LWeaponNotActive()
    {
        _weaponL.enabled = false;
    }
    public void RWeaponNotActive()
    {
        _weaponR.enabled = false;
    }
}
