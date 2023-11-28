using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventBus
{
    public class EBM : MonoBehaviour
    {
        Hashtable eventHash = new Hashtable();

        private static EBM ebm;

        public static EBM instance
        {
            get
            {
                if (!ebm)
                {
                    ebm = FindObjectOfType(typeof(EBM)) as EBM;

                    if (!ebm)
                    {
                        Debug.LogError("There needs to be one active EBM script on a GameObject in your scene.");
                    }
                    else
                    {
                        ebm.Init();
                    }
                }

                return ebm;
            }
        }

        void Init()
        {
            if (ebm.eventHash == null)
            {
                ebm.eventHash = new Hashtable();
            }
        }

        public static void StartListening<T>(EventBusEnum.EventName eventName, UnityAction<T> listener)
        {
            UnityEvent<T> thisEvent = null;

            string b = GetKey<T>(eventName);
          
            if (instance.eventHash.ContainsKey(b))
            {
                thisEvent = (UnityEvent<T>)instance.eventHash[b];
                thisEvent.AddListener(listener);
                instance.eventHash[eventName] = thisEvent;
            }
            else
            {
                thisEvent = new UnityEvent<T>();
                thisEvent.AddListener(listener);
                instance.eventHash.Add(b, thisEvent);
              
            }
        }

        public static void StartListening(EventBusEnum.EventName eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;

          //  string b = GetKey<T>(eventName);

            if (instance.eventHash.ContainsKey(eventName))
            {
                thisEvent = (UnityEvent)instance.eventHash[eventName];
                thisEvent.AddListener(listener);
                instance.eventHash[eventName] = thisEvent;
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                instance.eventHash.Add(eventName, thisEvent);

            }
        }


        public static void StopListening<T>(EventBusEnum.EventName eventName, UnityAction<T> listener)
        {
            if (ebm == null) return;
            UnityEvent<T> thisEvent = null;
            string key = GetKey<T>(eventName);
            if (instance.eventHash.ContainsKey(key))
            {
                thisEvent = (UnityEvent<T>)instance.eventHash[key];
                thisEvent.RemoveListener(listener);
                instance.eventHash[eventName] = thisEvent;
            }
        }

        public static void StopListening(EventBusEnum.EventName eventName, UnityAction listener)
        {
            if (ebm == null) return;
            UnityEvent thisEvent = null;

            if (instance.eventHash.ContainsKey(eventName))
            {
                thisEvent = (UnityEvent)instance.eventHash[eventName];
                thisEvent.RemoveListener(listener);
                instance.eventHash[eventName] = thisEvent;
            }
        }


        public static void TriggerEvent<T>(EventBusEnum.EventName eventName,T val)
        {
            UnityEvent<T> thisEvent = null;
            string key = GetKey<T>(eventName);
            if (instance.eventHash.ContainsKey(key))
            {
                thisEvent = (UnityEvent<T>)instance.eventHash[key];
                thisEvent.Invoke(val);
            }
        }

        public static void TriggerEvent(EventBusEnum.EventName eventName)
        {
            UnityEvent thisEvent = null;
            if (instance.eventHash.ContainsKey(eventName))
            {
                thisEvent = (UnityEvent)instance.eventHash[eventName];
                thisEvent.Invoke();
            }
        }

        private static string GetKey<T>(EventBusEnum.EventName eventName)
        {
            Type type = typeof(T);
            string key = type.ToString() + eventName.ToString();
            return key;
        }
    }


}


