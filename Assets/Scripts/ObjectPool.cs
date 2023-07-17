using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;

        [SerializeField]
        private GameObject objectToPool;
        [SerializeField]
        private int amountToPool;
        [SerializeField]
        private Transform sceneObjectsTransform;

        private List<GameObject> pooledObjects;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, sceneObjectsTransform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}