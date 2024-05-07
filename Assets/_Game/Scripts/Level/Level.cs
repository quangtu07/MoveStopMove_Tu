using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : BaseMono
{
    [SerializeField] public Transform playerStartPoint;
    [SerializeField] Enemy enemyPrefab; // Prefab của enemy
    [SerializeField] float spawnRadius = 100f; // Bán kính tối đa để spawn
    [SerializeField] Transform spawnerCenter;
    List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies { get => enemies; }

    public void OnInit(int numberOfEnemies)
    {
        ClearLevel();
        SpawnEnemies(numberOfEnemies);
    }

    void SpawnEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPoint = spawnerCenter.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            // Thử tìm một vị trí trên NavMesh từ điểm ngẫu nhiên với một khoảng cách tối đa là spawnRadius
            if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas))
            {
                // Nếu tìm thấy, spawn enemy tại vị trí hit.position
                //Instantiate(enemyPrefab, hit.position, Quaternion.identity);
                Enemy newEnemy = LeanPool.Spawn(enemyPrefab, hit.position, Quaternion.identity, spawnerCenter);
                Enemies.Add(newEnemy);
                newEnemy.OnInit();
            }
        }
    }

    public void RemoveEnemy(Character enemy)
    {
        Enemies.Remove((Enemy) enemy);
    }

    public void ClearLevel()
    {
        if (Enemies.Count > 0)
        {
            Enemies.Clear();
        }
        LeanPool.DespawnAll();
    }
}
