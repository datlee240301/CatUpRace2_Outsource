using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace Remi.InApp
{
    public class InAppReward : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private EnumRewardType _rewardType;
        public EnumRewardType rewardType => _rewardType;
        [SerializeField]
        private int _rewardAmount;
        public int rewardAmount => _rewardAmount;

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI _amoutText;

        protected void OnEnable()
        {
            if (_amoutText != null)
                _amoutText.text = "+" + _rewardAmount.ToString();
        }
    }

    public enum EnumRewardType
    {
        NONE = 0,
        REMOVEADS = 1,
        FIND = 2,
        COMPASS = 3
    }
}


