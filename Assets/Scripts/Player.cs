using UnityEngine;

public class Player : MonoBehaviour
{
    #region Parameters

    [SerializeField] private ShellLauncher _shellLauncher;

    #endregion


    #region Controls

    private void Awake()
    {
        EnemyDetector.Instance.onEnemyDetect += FireInEnemy;
    }

    private void FireInEnemy(Enemy enemy)
    {
        _shellLauncher.Launch(enemy.transform.position);
    }

    #endregion
}