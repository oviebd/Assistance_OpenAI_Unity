using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

namespace EventBus_Demo
{
    public enum THEME
    {
        WHITE,
        DARK,
        BLUE
    }

    public class UiButtonEventTrigger : MonoBehaviour
    {
        public void OnWhiteThemeButtonClicked()
        {
            EBM.TriggerEvent<THEME>(EventBusEnum.EventName.THEME_CHANGE, THEME.WHITE);
        }

        public void OnDarkThemeButtonClicked()
        {
            EBM.TriggerEvent<THEME>(EventBusEnum.EventName.THEME_CHANGE, THEME.DARK);
        }

        public void OnBlueThemeButtonClicked()
        {
            EBM.TriggerEvent<THEME>(EventBusEnum.EventName.THEME_CHANGE, THEME.BLUE);
        }

        public void OnCountButtonPressed()
        {
            EBM.TriggerEvent(EventBusEnum.EventName.COUNT_UPDATED);
        }
    }

}
