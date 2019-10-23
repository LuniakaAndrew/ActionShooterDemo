using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    public List<Enemy> GetEnemies()
    {
        return new List<Enemy>(_enemies);
    }
}