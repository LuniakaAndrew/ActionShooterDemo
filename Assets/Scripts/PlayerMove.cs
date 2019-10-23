using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Plane _plane;
    private float _enter;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void UpdateTargets(Vector3 targetPosition)
    {
        _navMeshAgent.destination = targetPosition;
    }

    private void Update()
    {
        if (GetInput())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out _enter))
            {
                Vector3 targetPosition = ray.GetPoint(_enter);
                UpdateTargets(targetPosition);
            }
        }
    }

    private bool GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }
}