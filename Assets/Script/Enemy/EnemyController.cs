using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    int _enemyHp = 5000;
    /// <summary>Enemy�̑���</summary>
    [SerializeField]
    float _moveSpeed = 3.0f;
    /// <summary>�v���C���[�������邱�Ƃ��ł��鋗��</summary>
    [SerializeField, Header("�v���C���[���������鋗��")]
    float _playerSensedis = 5f;
    [SerializeField, Header("�����o���܂ł̎���")]
    float _moveTime = 5f;
    [SerializeField, Header("�J�E���g�p")]
    float _timer = 0f;
    /// <summary>�ړI�n���؂�ւ�鋗��</summary>
    [SerializeField, Header("�ړI�n���؂�ւ�鋗��")]
    float _changeDis = 5f;
    [SerializeField, Header("�v���C���[�ɍU�����鋗��")]
    float _attackDis = 1f;
    /// <summary>Enemy��X���Ƃy���̈ړ��͈�</summary>
    [SerializeField, Header("X���Ƃy���̈ړ��͈�")]
    float _xz = 30f;
    /// <summary>�p���B����鎞��</summary>
    float _parrylimit = 0.5f;

    float _enemyPosX;
    float _enemyPosZ;
    int _pattern = 0;
    GameObject _player = null;
    NavMeshAgent _agent = null;
    /// <summary>Enemy�̐������ꂽ�����n�_</summary>
    Vector3 _enemypos;
    /// <summary>�v���C���[�̒n�_�A�܂��͈ړ��ړI�n</summary>
    Vector3 _targetpos;
    Vector3 _destination = new Vector3(0, 0, 0);
    Animator _anim = null;
    /// <summary>�v���C���[�������Ă��邩�ǂ���</summary>
    bool _playerFound = false;
    ///// <summary>�U�������ǂ���</summary>
    //bool attack = false;//�p���B�\�ȍU���̂ݎg�p�\��
    /// <summary>�p���B���o���邩�ǂ���</summary>
    bool _parry = false;//�A�j���[�V�����C�x���g�Ŏg�p�\��
    [Tooltip("���񂾂��ǂ���")]
    bool _die = false;
    public int EnemyHp { get => _enemyHp; set => _enemyHp = value; }
    public bool Parry { get => _parry; set => _parry = value; }
    public bool Die { get => _die;}

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
        switch (_pattern)
        {
            case 1:
                if (_playerFound)//�v���C���[�������������A�������������ꂽ�ꏊ�֖߂�
                {
                    _destination = _enemypos;
                    _agent.SetDestination(_destination);
                    _playerFound = false;
                }
                if (Vector3.Distance(transform.position, _destination) <= _changeDis)//�ړI�n���ӂɗ�����
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
                    _agent.SetDestination(_targetpos);//�ړI�n����Ƀv���C���[�ɕύX 
                }
                else
                {
                    //�U���A�j���[�V�����̍Đ��B
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

    public void Damage(int damage)
    {
        _enemyHp -= damage;
        if (_enemyHp <= 0)
        {
            _die = true;
            //�A�j���[�V�����𗬂��A�����蔻��ƂȂ��Ă���R���C�_�[�̃��X�g������Ă����A�����蔻��������B
            Debug.Log("�G������");
        }
    }
}
