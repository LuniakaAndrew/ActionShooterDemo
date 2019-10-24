using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemySpawner : SingletonComponent<EnemySpawner>
{
    #region Singleton

    public static EnemySpawner Instance
    {
        get { return ((EnemySpawner) _Instance); }
        set { _Instance = value; }
    }

    #endregion

    #region Parameters

    [SerializeField] private Enemy[] _enemies;

    #endregion

    #region Controls

    public List<Enemy> GetEnemies()
    {
        return new List<Enemy>(_enemies);
    }

    #endregion
}