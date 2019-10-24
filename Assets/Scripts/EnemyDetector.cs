using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyDetector : SingletonComponent<EnemyDetector>
{
    #region Singleton

    public static EnemyDetector Instance
    {
        get { return ((EnemyDetector) _Instance); }
        set { _Instance = value; }
    }

    #endregion

    #region Actions

    public Action<Enemy> onEnemyDetect;

    #endregion

    #region Parameters

    [SerializeField] private Player _player;
    [SerializeField] private float _distanceToEnemy;

    #endregion

    #region Fields

    private List<Enemy> _enemies = new List<Enemy>();

    #endregion

    #region Controls

    private void Start()
    {
        _enemies = EnemySpawner.Instance.GetEnemies();
    }

    private void Update()
    {
        if (EnemyNear(_player.transform.position, _distanceToEnemy))
        {
        }
    }

    public bool EnemyNear(Vector3 pos, float radius)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (!_enemies[i].IsDetected &&
                Vector3.Distance(pos, _enemies[i].transform.position) <= radius)
            {
                _enemies[i].IsDetected = true;
                onEnemyDetect?.Invoke(_enemies[i]);
            }
        }

        return false;
    }


    public List<Enemy> GetNearEnemies(Vector3 pos, float radius)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (!_enemies[i].IsDead &&
                Vector3.Distance(pos, _enemies[i].transform.position) <= radius)
            {
                enemies.Add(_enemies[i]);
            }
        }

        return new List<Enemy>(enemies);
    }

    #endregion
}