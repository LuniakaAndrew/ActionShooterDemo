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

    public Action<Vector3> onEnemyDetect;

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
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (!_enemies[i].IsDetected &&
                Vector3.Distance(_player.transform.position, _enemies[i].transform.position) <= _distanceToEnemy)
            {
                _enemies[i].IsDetected = true;
                onEnemyDetect?.Invoke(_enemies[i].transform.position);
            }
        }
    }

    #endregion
}