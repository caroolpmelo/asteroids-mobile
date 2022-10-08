using UnityEngine;

namespace Game.Common
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Already exists one instance of " + typeof(T));
                return;
            }

            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
