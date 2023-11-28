using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EventBus_Demo
{
    public class ButtonThemeColorChangerBasedOnTheme : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
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
            if (button == null)
            {
                return;
            }
            button.image.color = ThemeManager.instance.GetThemeData(theme).buttonBgColor;
            button.gameObject.GetComponentInChildren<TMP_Text>().color = ThemeManager.instance.GetThemeData(theme).buttonTextColor;
        }
    }
}


