using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus_Demo
{
    public class ThemeManager : MonoBehaviour
    {
        [HideInInspector]
        public static ThemeManager instance;

        [Header("White Theme")]
        public ThemeDetailsData whiteThemeData;

        [Header("Dark Theme")]
        public ThemeDetailsData darkThemeData;

        [Header("Blue Theme")]
        public ThemeDetailsData blueThemeData;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }


        public ThemeDetailsData GetThemeData(THEME theme)
        {
            switch (theme)
            {
                case THEME.WHITE:
                    return whiteThemeData;
                case THEME.DARK:
                    return darkThemeData;
                case THEME.BLUE:
                    return blueThemeData;
            }

            return whiteThemeData;
        }
    }

    [Serializable]
    public class ThemeDetailsData
    {
        public Color textColor;
        public Color mainBgColor;
        public Color buttonTextColor;
        public Color buttonBgColor;
    }

}
