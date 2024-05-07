using System;
using UnityEngine;
using Lean.Pool;


public class BaseWeapon : BaseMono
{
    [SerializeField] BulletType bulletType;
    [SerializeField] BaseBullet bulletPrefab;

    public void Throw(Character character,Vector3 target, float size, Action<Character, Character> onHit)
    {
        BaseBullet bullet = LeanPool.Spawn(bulletPrefab, character.Tf.position + Vector3.up, character.Tf.rotation);
        //BaseBullet bullet = SimplePool.Spawn<BaseBullet>((PoolType)bulletType, character.Tf.position + Vector3.forward + Vector3.up, character.Tf.rotation);
        bullet.OnInit(character, target, size, onHit);
        bullet.Tf.localScale = size * Vector3.one;
    }
}
