using UnityEngine;

public class Shell : MonoBehaviour
{
    #region Parameters

    [SerializeField] private float _damageRadius = 1f;

    #endregion

    #region Properties

    public float DamageRadius
    {
        get => _damageRadius;
        set => _damageRadius = value;
    }

    #endregion
}