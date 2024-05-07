using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharacterSight : MonoBehaviour
{
    [SerializeField] Character character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName.TAG_CHARACTER))
        {
            Character target = CacheCollider<Character>.GetColliderComponent(other);
            if (!target.IsDead())
            {
                character.AddTarget(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName.TAG_CHARACTER))
        {
            Character target = CacheCollider<Character>.GetColliderComponent(other);
            character.RemoveTarget(target);
        }
    }
}
