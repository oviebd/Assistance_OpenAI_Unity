using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EventBus;

namespace EventBus_Demo
{
    public class TextColorChangerBasedOnTheme : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        void OnEnable()
        {
            EBM.StartListening<THEME>(EventBusEnum.EventName.THEME_CHANGE, OnThemeChanged);
        }

        void OnDisable()
        {
            EBM.StopListening<THEME>(EventBusEnum.EventName.THEME_CHANGE, OnThemeChanged);
        }


        void OnThemeChanged(THEME theme)
        {
            if (text == null)
            {
                return;
            }
            text.color = ThemeManager.instance.GetThemeData(theme).textColor;
        }
    }
}

