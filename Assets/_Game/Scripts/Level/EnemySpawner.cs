using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;

public class EnemySpawner : BaseMono
{
    public Enemy enemyPrefab; // Prefab của enemy
    public float spawnRadius = 100f; // Bán kính tối đa để spawn

    //private void Start()
    //{
    //    SpawnEnemies(20);
    //}

    public void OnInit(int numberOfEnemies)
    {
        SpawnEnemies(numberOfEnemies);
    }

    void SpawnEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            // Thử tìm một vị trí trên NavMesh từ điểm ngẫu nhiên với một khoảng cách tối đa là spawnRadius
            if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas))
            {
                // Nếu tìm thấy, spawn enemy tại vị trí hit.position
                //Instantiate(enemyPrefab, hit.position, Quaternion.identity);
                Enemy newEnemy = LeanPool.Spawn(enemyPrefab, hit.position, Quaternion.identity, Tf);
                newEnemy.OnInit();
            }
        }
    }
}
