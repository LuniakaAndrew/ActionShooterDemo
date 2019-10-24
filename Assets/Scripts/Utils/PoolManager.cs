using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class PoolManager : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private PoolPart[] _pools;

        #endregion

        #region Fields

        [System.Serializable]
        private class PoolPart
        {
            #region Parameters

            [SerializeField] private string _name;
            [SerializeField] private int _count;
            [SerializeField] private GameObject _prefab;
            [SerializeField] private Transform _objectsParent;

            #endregion

            #region Fields

            private ObjectPooling _specificPool = new ObjectPooling();

            #endregion

            #region Properties

            public string Name
            {
                get { return _name; }
            }

            public int Count
            {
                get { return _count; }
            }

            public GameObject Prefab
            {
                get { return _prefab; }
            }

            public Transform ObjectsParent
            {
                get { return _objectsParent; }
            }

            #endregion

            #region Controls

            public GameObject GetObject(string name)
            {
                return _specificPool.GetObject(name);
            }

            public void Initialize(int count, GameObject sample, Transform objectsParent, string poolName)
            {
                _specificPool.Initialize(count, sample, objectsParent, poolName);
            }

            public void ReturnObject(GameObject objectForReturn)
            {
                _specificPool.ReturnObject(objectForReturn);
            }

            #endregion
        }

        #endregion

        #region Singleton

        public static PoolManager _instance;

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("There is no PoolManager in scene");
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            Initialize();
        }

        #endregion

        #region Controls

        private void Initialize()
        {
            GameObject poolParent = new GameObject();
            poolParent.name = "Pool";
            for (int i = 0; i < _pools.Length; ++i)
                if (_pools[i].Prefab != null)
                    _pools[i].Initialize(_pools[i].Count, _pools[i].Prefab, poolParent.transform, _pools[i].Name);
        }

        public GameObject GetObject(string name)
        {
            if (_pools != null)
            {
                for (int i = 0; i < _pools.Length; ++i)
                {
                    if (string.Equals(_pools[i].Name, name))
                    {
                        GameObject result = _pools[i].GetObject(name);
                        result.transform.SetParent(_pools[i].ObjectsParent);
                        result.SetActive(true);
                        return result;
                    }
                }
            }

            return null;
        }

        public void ReturnObject(PoolObject objectForReturn)
        {
            if (_pools != null)
            {
                for (int i = 0; i < _pools.Length; ++i)
                {
                    if (string.Equals(_pools[i].Name, objectForReturn.PoolName()))
                    {
                        _pools[i].ReturnObject(objectForReturn.gameObject);
                        return;
                    }
                }
            }
        }

        #endregion

        #region Classes

        private class ObjectPooling
        {
            #region Fields

            private List<GameObject> _objects;
            private Transform _objectsParent;
            private GameObject _sample;

            #endregion

            #region Controls

            public void Initialize(int count, GameObject sample, Transform objectsParent, string poolName)
            {
                _objects = new List<GameObject>();
                _objectsParent = objectsParent;
                _sample = sample;
                for (int i = 0; i < count; ++i)
                    _objects.Add(AddObject(sample, objectsParent, poolName));
            }

            public GameObject GetObject(string poolName)
            {
                if (_objects.Count == 0)
                    return AddObject(_sample, _objectsParent, poolName);
                else
                {
                    GameObject temp = _objects[0];
                    _objects.Remove(temp);
                    return temp;
                }
            }

            public void ReturnObject(GameObject objectForReturn)
            {
                _objects.Add(objectForReturn);
                objectForReturn.transform.SetParent(_objectsParent);
                objectForReturn.SetActive(false);
            }

            private GameObject AddObject(GameObject sample, Transform objectsParent, string poolName)
            {
                GameObject temp = GameObject.Instantiate(sample);
                temp.name = sample.name;
                temp.GetComponent<PoolObject>().poolName = poolName;
                temp.transform.SetParent(objectsParent);
                temp.SetActive(false);
                return temp;
            }

            #endregion
        }

        #endregion
    }
}