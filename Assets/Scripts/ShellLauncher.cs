using System.Collections;
using UnityEngine;

public class ShellLauncher : MonoBehaviour
{
    public GameObject shell;

    public float h = 25;
    public float gravity = -18;

    public bool debugPath;

    public void Launch(Vector3 target)
    {
        StartCoroutine(LaunchArc(target));
    }

    private IEnumerator LaunchArc(Vector3 target)
    {
        LaunchData launchData = CalculateLaunchData(target);
        Vector3 startPoint = shell.transform.position;
        int resolution = 100;
        float pause = launchData.timeToTarget / resolution;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float) resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime +
                                   Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = startPoint + displacement;

            shell.transform.position = drawPoint;
            yield return new WaitForSeconds(pause);
        }
    }

    private LaunchData CalculateLaunchData(Vector3 target)
    {
        float displacementY = target.y - shell.transform.position.y;
        Vector3 displacementXZ =
            new Vector3(target.x - shell.transform.position.x, 0,
                target.z - shell.transform.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

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
}