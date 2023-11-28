using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace EventBus_Demo
{
    public class ImageColorChangerBasedOnTheme : MonoBehaviour
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
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
            if(image == null)
            {
                return;
            }
            image.color = ThemeManager.instance.GetThemeData(theme).mainBgColor;
        }
    }

}

