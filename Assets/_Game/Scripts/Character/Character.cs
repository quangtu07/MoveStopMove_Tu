using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;

public class Character : BaseMono
{
    public float attackRange = 9f;

    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator anim;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected WeaponType currentWeaponType;
    [SerializeField] protected HatType currentHatType;
    [SerializeField] protected PantType currentPantType;
    [SerializeField] protected TeamType teamType;
    [SerializeField] protected WeaponDataSO weaponSO;
    [SerializeField] protected Transform weaponParent;
    [SerializeField] protected CharacterWeapon characterWeapon;
    [SerializeField] protected CharacterHat characterHat;
    [SerializeField] protected CharacterPant characterPant;


    protected BaseWeapon currentWeapon;
    protected BaseHat currentHat;
    protected float size = 1;
    protected bool isGround;
    protected bool isDead;
    protected List<Character> targets = new List<Character>();
    protected Character target;
    protected Vector3 targetPoint;
    protected float raycastMaxDistance;
    protected string currentAnimName = AnimName.IDLE;
    protected bool isMoving;
    protected int score;
    protected float maxSize = 4f;
    protected float minSize = 1f;
    protected float sizeUp = 0.1f;

    public List<Character> Targets { get => targets; }

    //private void Start()
    //{
    //    OnInit();
    //}

    public virtual void OnInit(int weaponTypeIndex = 0, int hatTypeIndex = 0, int pantTypeIndex = 0)
    {
        score = 0;
        raycastMaxDistance = 2f;
        currentAnimName = AnimName.IDLE;
        isDead = false;
        ChangeAnim(AnimName.IDLE);
        ClearTarget();
    }

    public void AddScore()
    {
        score++;
        SetSize(minSize + this.score * sizeUp);
    }


    //thay doi kich thuoc
    protected virtual void SetSize(float size)
    {
        size = Mathf.Clamp(size, minSize, maxSize);
        this.size = size;
        Tf.localScale = size * Vector3.one;
    }

    public virtual void OnStopMove()
    {
        //For override
    }

    // Check if character on ground
    protected bool CheckGround()
    {
        return Physics.Raycast(Tf.position, Tf.TransformDirection(Vector3.down), raycastMaxDistance, groundLayer);
    }

    protected virtual void Win()
    {
        OnStopMove();
        ChangeAnim(AnimName.WIN);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected void LoadCurrentWeapon(WeaponType type)
    {
        //characterWeapon.AddWeapon(type);
        //characterWeapon.ChangeWeapon(type);
        currentWeapon = characterWeapon.GetWeapon(type);
    }

    protected void LoadCurrentHat(HatType type)
    {
        characterHat.AddHat(type);
        characterHat.ChangeHat(type);
        currentHat = characterHat.GetHat(type);
    }

    protected void LoadCurrentPant(PantType type)
    {
        characterPant.AddPant(type);
        characterPant.ChangePant(type);
    }

    //them muc tieu
    public virtual void AddTarget(Character target)
    {
        Targets.Add(target);
    }

    //xoas muc tieu
    public virtual void RemoveTarget(Character target)
    {
        this.Targets.Remove(target);
        this.target = null;   
    }

    public virtual void OnAttack()
    {
        target = GetTargetInRange();

        if (target != null && !target.isDead/* && currentSkin.Weapon.IsCanAttack*/)
        {
            targetPoint = target.Tf.position;
            Tf.LookAt(targetPoint + (Tf.position.y - targetPoint.y) * Vector3.up);
        }

    }

    //ban ra vien dan
    public virtual void Throw()
    {
        currentWeapon.Throw(this, targetPoint + Vector3.up, size, OnHitVictim);
    }

    protected virtual void OnHitVictim(Character attacker, Character victim)
    {
        if(!victim.IsDead())
        {
            attacker.AddScore();
            if (attacker is Player)
            {
                UIManager.Instance.GetUI<CanvasGamePlay>().UpdateScore(score);
            }
            victim.OnDeath();
            victim.isDead = true;
            attacker.RemoveTarget(victim);
        }      
    }

    public Character GetTargetInRange()
    {

        Character target = null;
        float distance = float.PositiveInfinity;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i] != this && !targets[i].IsDead() && Vector3.Distance(Tf.position, targets[i].Tf.position) <= attackRange * size + targets[i].size)
            {
                float dis = Vector3.Distance(Tf.position, targets[i].Tf.position);

                if (dis < distance)
                {
                    distance = dis;
                    target = targets[i];
                }
            }
        }

        return target;
    }

    protected void ClearTarget()
    {
        if (Targets.Count > 0)
        {
            Targets.Clear();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void OnDeath()
    {
        ChangeAnim(AnimName.DEAD);
        isDead = true;
        if (this is Player)
        {
            LevelManager.Instance.OnLose();
        }

        if (this is Enemy)
        {
            ChangeAnim(AnimName.DEAD);
            LevelManager.Instance.CurrentLevel.RemoveEnemy(this);
            StartCoroutine(DespawnVictim());

            if (LevelManager.Instance.CurrentLevel.Enemies.Count <= 0)
            {
                LevelManager.Instance.OnWin(score);
            }
        }
    }

    IEnumerator DespawnVictim()
    {
        yield return new WaitForSeconds(1.5f);
        LeanPool.Despawn(this);

    }

}
