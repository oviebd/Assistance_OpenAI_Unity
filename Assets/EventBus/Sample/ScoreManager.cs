using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;
using TMPro;

namespace EventBus_Demo
{
    public class ScoreManager : MonoBehaviour
    {
        int score = 0;

        [SerializeField] private TMP_Text text;

        void OnEnable()
        {
            EBM.StartListening(EventBusEnum.EventName.COUNT_UPDATED, OnScoreUpdated);
        }

        void OnDisable()
        {
            EBM.StopListening(EventBusEnum.EventName.COUNT_UPDATED, OnScoreUpdated);
        }

        void OnScoreUpdated()
        {
            score += 1;
            text.text = score.ToString();
        }
    }
}

