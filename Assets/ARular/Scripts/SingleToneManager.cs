using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JoystickLab
{
    public class SingleToneManager<T> : MonoBehaviour where T:MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get { return instance; }
            set
            {
                if (instance == null)
                {
                    instance = value;
                    DontDestroyOnLoad(instance.gameObject);
                }
                else if (instance != value)
                {
                    Destroy(value.gameObject);
                }
            
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this as T;
        }
    }

}
