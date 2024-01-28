﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.Scripts
{
    public class LaughPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killPanelTitleText = null;
        [SerializeField] private TextMeshProUGUI killPanelDescriptionText = null;
        [SerializeField] private Button continueButton;

        [SerializeField] private string title;
        [SerializeField] private string description;
        
        [SerializeField] private string successTitle;
        [SerializeField] private string successDescription;
        [SerializeField] private string failureBribeTitle;
        [SerializeField] private string failureBribeDescription;
        [SerializeField] private string failureNoBribeTitle;
        [SerializeField] private string failureNoBribeDescription;
        [SerializeField] private string failureTitle;
        [SerializeField] private string failureDescription;
        [SerializeField] private string noneTitle;
        [SerializeField] private string noneDescription;

        Dictionary<LaughOutcome, Tuple<string, string>> outcomeDescription = new Dictionary<LaughOutcome, Tuple<string, string>>();

        private void Start()
        {
            gameObject.SetActive(false);
            outcomeDescription = new Dictionary<LaughOutcome, Tuple<string, string>>()
            {
                { LaughOutcome.None, new Tuple<string, string>(noneTitle, noneDescription) },
                { LaughOutcome.FailureBribe, new Tuple<string, string>(failureBribeTitle, failureBribeDescription) },
                { LaughOutcome.FailureNoBribe, new Tuple<string, string>(failureNoBribeTitle, failureNoBribeDescription) },
                { LaughOutcome.Success, new Tuple<string, string>(successTitle, successDescription) },
            };
        }
    }
}