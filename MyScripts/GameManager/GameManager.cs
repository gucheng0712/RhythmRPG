using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{


    public abstract class GameManager : MonoBehaviour
    {

    }


    // Create an instance for all GameManager_XXXX which inherited from this GameManager<T>
    public abstract class GameManager<T> : GameManager where T : GameManager<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
            }
        }
        protected virtual void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }

}
