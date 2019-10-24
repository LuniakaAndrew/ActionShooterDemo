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

    private void FireInEnemy(Vector3 target)
    {
        _shellLauncher.Launch(target);
    }

    #endregion
}