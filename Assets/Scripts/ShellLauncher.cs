using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ShellLauncher : MonoBehaviour
{
    #region Parameters

    [SerializeField] private float _height = 25f;
    [SerializeField] private float _gravity = -18;

    #endregion

    public GameObject _shell;

    #region Controls

    public void Launch(Vector3 target)
    {
        StartCoroutine(LaunchArc(target));
    }

    private IEnumerator LaunchArc(Vector3 target)
    {
        float currentHeight = _height;
        Vector3 currentTarget = target;
        float distance = (currentTarget - transform.position).magnitude;
        Vector3 direction = (currentTarget - transform.position).normalized;
        Vector3 startPoint = _shell.transform.position;
        for (int i = 0; i < 3; i++)
        {
            LaunchData launchData = CalculateLaunchData(target, startPoint, currentHeight);

            int resolution = 100;
            float pause = launchData.timeToTarget / resolution;
            for (int j = 1; j <= resolution; j++)
            {
                float simulationTime = j / (float) resolution * launchData.timeToTarget;
                Vector3 displacement = launchData.initialVelocity * simulationTime +
                                       Vector3.up * _gravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = startPoint + displacement;
                Debug.DrawLine (startPoint, drawPoint, Color.green);
                _shell.transform.position = drawPoint;
                yield return new WaitForSeconds(pause);
            }

            currentHeight /= 2f;
            distance -= distance * 0.5f;
            startPoint = target;
            target += direction * distance;
        }
    }

    private LaunchData CalculateLaunchData(Vector3 target, Vector3 shell, float height)
    {
        float displacementY = target.y - shell.y;
        Vector3 displacementXZ =
            new Vector3(target.x - shell.x, 0,
                target.z - shell.z);
        float time = Mathf.Sqrt(-2 * height / _gravity) + Mathf.Sqrt(2 * (displacementY - height) / _gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(_gravity), time);
    }

    #endregion

    #region Classes

    private struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    #endregion
}