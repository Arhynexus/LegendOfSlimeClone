using System.Collections;
using UnityEngine;
using TowerDefenceClone;
using CosmoSimClone;
using System.Collections.Generic;

namespace LegendOfSlimeClone
{


    public class EnemySpawner : Spawner
    {
        [SerializeField] private EnemySlime m_EnemyPrefab;
        [SerializeField] private Transform[] m_SpawnPoints;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EnemyPrefab);
            return e.gameObject;
        }

        protected override void SpawnEntities()
        {
            for (int i = 0; i < NumSpawns; i++)
            {
                var e = GenerateSpawnedEntity();
                e.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;
            }
        }
    }
}