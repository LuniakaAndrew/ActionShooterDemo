using UnityEngine;

namespace Utils
{
    public class PoolObject : MonoBehaviour, IPoolObject
    {
        public string poolName;

        public string PoolName()
        {
            return poolName;
        }
    }
}