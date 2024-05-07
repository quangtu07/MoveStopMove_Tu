using System;
using UnityEngine;
using Lean.Pool;
public class BaseBullet : GameUnit
{
    [SerializeField] float speed = 0.5f;
    protected Character attacker;
    protected bool isRunning;
    protected float time = 0;
    protected float limitTime = 3f;
    protected Action<Character, Character> onHit;
    protected Vector3 direct;

    private void Update()
    {
        if (isRunning && GameManager.Instance.IsState(GameState.GamePlay))
        {
            time += Time.deltaTime;
            Tf.Translate(direct * speed * Time.deltaTime, Space.World);
            if (time > limitTime)
            {
                time = 0;
                OnDespawn();
            }
        }
    }

    public virtual void OnInit(Character attacker, Vector3 target, float size, Action<Character, Character> onHit)
    {
        this.attacker = attacker;
        this.onHit = onHit;
        direct = (target - attacker.Tf.position).normalized;
        isRunning = true;
    }

    public void OnDespawn()
    {
        //Destroy(gameObject);
        //SimplePool.Despawn(this);
        LeanPool.Despawn(this);
    }

    protected virtual void OnStop() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName.TAG_CHARACTER))
        {
            Character victim = CacheCollider<Character>.GetColliderComponent(other);
            if (victim != null && victim != attacker)
            {
                OnDespawn();
                onHit?.Invoke(attacker, victim);
            }          
        }
    }
}
