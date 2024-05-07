using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziWeapon : BaseWeapon
{
    [SerializeField] Transform bulletPoint;
    [SerializeField] BaseBullet bulletBasePrefab;

    //public override void Shoot(Character character, Character enemy)
    //{
    //    base.Shoot(character, enemy);
    //    //BulletBase b = Instantiate(bulletBasePrefab, bulletPoint.position, bulletPoint.rotation);
    //    BaseBullet b = SimplePool.Spawn<BaseBullet>(BulletType.Uzi, bulletPoint.position, bulletPoint.rotation);
    //    b.OnInit(1, enemy);
    //}
}
