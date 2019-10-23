using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ShellLauncher _shellLauncher;

    [SerializeField] private EnemyDetector _enemyDetector;

    // Start is called before the first frame update
    private void Awake()
    {
        _enemyDetector.onEnemyDetect += FireInEnemy;
    }

    private void FireInEnemy(Vector3 target)
    {
        _shellLauncher.Launch(target);
    }
}