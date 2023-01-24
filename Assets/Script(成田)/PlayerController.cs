using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    /// <summary>player�̑���</summary>
    [SerializeField,Tooltip("player�̑��x"),Header("�������x")]
    float _moveSpeed = 3f;
    /// <summary>player�̉�]���x</summary>
    [SerializeField]
    float _turnSpeed = 3f;
    /// <summary>player�̃W�����v��</summary>
    [SerializeField]
    float _jumpPower = 3f;
    /// <summary>player�̊�{�U����</summary>
    [SerializeField]
    int _attackDamage = 500;//�֐��ŕύX����B
    int _keepAttackDamage = 0;
    /// <summary>player�̕���z��</summary>
    [SerializeField,Tooltip("LongSwordMesh"),Header("LongSwordMesh")]
    GameObject _weapon = null;
    [SerializeField,Tooltip("�X�L�����g�p���邽�߂ɉ����{�^��"),Header("PlayerUseCanvas��SkillButton")]
    GameObject _button = null;
    TextMeshProUGUI _skillText = null;
    /// <summary>�K�[�h����p�R���C�_�[</summary>
    [SerializeField]
    GameObject guardCollider = null;
    /// <summary>weapon�̔���p�R���C�_�[</summary>
    Collider attackCollider = null;
    /// <summary>����</summary>
    private float timer = 0.0f;
    /// <summary>�U������p�R���C�_�[�̃A�N�e�B�u�^�C��</summary>
    private float attackJudgeTime = 0.5f;
    /// <summary>�K�[�h����p�R���C�_�[�̃A�N�e�B�u�^�C��</summary>
    private float guardJudgeTime = 0.5f;
    /// <summary>�ڒn����</summary>
    bool isGrounded = true;
    /// <summary>�h�䔻��p,�R���C�_�[Active�p</summary>
    bool guard = false;
    /// <summary>�A�j���[�V�����Đ������ǂ���</summary>
    bool animPlay = false;
    /// <summary>�p���B����p</summary>
    bool parrysuccess = false;
    public bool Guard { get => guard; set => guard = value; }
    public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public Animator Anim { get => _anim; set => _anim = value; }
    public Playerhp Hp { get => hp; set => hp = value; }

    List<ISkill> _skills = new List<ISkill>();
    ISkill _skill = null;
    int skillnum = 0;
    Playerhp hp = null;
    Rigidbody _rb = default;
    Animator _anim = default;
    /// <summary>���͂��ꂽ������ XZ ���ʂł̃x�N�g��</summary>

    void Start()
    {
        //if(!attackCollider)
        //{
        //    Debug.LogError("�U������p�̃R���C�_�[���Z�b�g����Ă��܂���");
        //}
        //else if(!guardCollider)
        //{
        //    Debug.LogError("�K�[�h����p�̃R���C�_�[���Z�b�g����Ă��܂���");
        //}
        
        if (!_weapon) Debug.LogError("���킪����܂���");
        if (!_button) Debug.LogError("�{�^�����Z�b�g���Ă�������");
        _skillText = _button.GetComponentInChildren<TextMeshProUGUI>();
        attackCollider = _weapon.GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        hp = GetComponent<Playerhp>();
        _keepAttackDamage = _attackDamage;
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        timer += Time.deltaTime;
        //if(playerDamagehp <= 0)
        //{
        //    //GameOver
        //}
        //if (normalAttack)
        //{
        //    timer -= timer;
        //    AttackColliderActive();
        //    normalAttack = false;
        //}
        //if(timer > attackJudgeTime)
        //{
        //    attackCollider.SetActive(false);   
        //}
        if(guard)
        {
            timer -= timer;
            GuardColliderActive();
            guard = false;
        }
        if(timer > guardJudgeTime)
        {
            guardCollider.SetActive(false);
        }
        // ���͕����̃x�N�g���v�Z
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            // �����̓��͂��Ȃ����́Ay �������̑��x��ێ�
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // �J��������ɂ���
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            //�ړ��̏���
            Vector3 velo = dir.normalized * _moveSpeed;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            NormalAttack();
            _anim.Play("NormalAttack1");
        }
    }
    void LateUpdate()
    {
        // �A�j���[�V�����̏���
        if (_anim)
        {
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
            _anim.SetBool("IsGrounded", isGrounded);//�ڒn����p
            _anim.SetBool("Guard",guard);//�K�[�h�p
            _anim.SetBool("Parrysuccess", parrysuccess);//�p���B������true
        }
    }
    private void AttackColliderActive()//����̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        attackCollider.enabled = true;
    }

    private void AttackColliderNotActive()//����̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        attackCollider.enabled = false;
    }
    private void GuardColliderActive()//�h��p�̓����蔻����o���Aanimation�C�x���g��p�֐�
    {
        guardCollider.SetActive(true);//�p���B�ƘA������悤��
    }
    private void NormalAttackPlay()//animation�C�x���g�p
    {
        //normalAttack = true;
    }

    public void NormalAttack()
    {
        _attackDamage = Random.Range(400, 600);
    }

    public void AttackDamageKeep(int damage)
    {
        _keepAttackDamage = _attackDamage;
        _attackDamage = damage;
    }
    public void AttackDamageKeep()//���x���A�b�v���g�p�B
    {
        _keepAttackDamage = _attackDamage;
    }
    public void ReturnAttackDamage()//�U���X�L�������Ă��Ȃ����ɌĂ�
    {
        _attackDamage = _keepAttackDamage;
    }
    public void ParryJudge(bool judge)
    {
        if(!judge)
        {
            return;
        }
        else
        {
            parrysuccess = true;//parry�p��animation�𗬂�
        }
    }
    void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    public void AddSkill(ISkill skill)
    {
        if (_skill == null)
        {
            _skills.Add(skill);
            _skill = skill;
            _skillText.text = _skill.Name;
        }
        else
        {
            _skills.Add(skill);
            skillnum = 0;
        }
    }

    public void NextSkill()//Next�{�^�����������Ƃ�
    {
        if (_skills != null)
        {
            skillnum++;
            _skill = _skills[skillnum % _skills.Count];
            _skillText.text = _skill.Name;
        }

    }
    public void UseSkill()//�X�L���{�^�����������Ƃ�
    {  
        if(_skill != null)
        {
            _skill.Action(this);
        }
    }
}
