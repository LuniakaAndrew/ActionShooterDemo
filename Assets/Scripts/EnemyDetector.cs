using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public Action<Vector3> onEnemyDetect;
    [SerializeField] private Player _player;
    [SerializeField] private float _distanceToEnemy;
    [SerializeField] private EnemySpawner _enemySpawner;
    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        _enemies = _enemySpawner.GetEnemies();
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (!_enemies[i].isDetected &&
                Vector3.Distance(_player.transform.position, _enemies[i].transform.position) <= _distanceToEnemy)
            {
                _enemies[i].isDetected = true;
                onEnemyDetect?.Invoke(_enemies[i].transform.position);
            }
        }
    }
}