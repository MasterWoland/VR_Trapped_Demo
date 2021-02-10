using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.events;
using UnityEngine;
using UnityEngine.UI;

namespace nl.allon.views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private CanvasGroup _canvasGroup = null;
        [SerializeField] private Toggle _racketToggle = null;
        [SerializeField] private Button _continueButton = null;

        // events
        [SerializeField] private SimpleEvent _mainMenuContinueEvent = default;
        [SerializeField] private BoolEvent _useRightHandForRacketEvent = default;

        private void Awake()
        {
            _racketToggle.onValueChanged.AddListener(OnRacketToggle);
            _continueButton.onClick.AddListener(OnContinue);
        }

        private void Start()
        {
            Camera cam = Camera.main;
            if (cam == null) Debug.LogError("[MainMenuView] No Main Camera found");
            else _canvas.worldCamera = cam;
        }

        private void OnContinue()
        {
            _mainMenuContinueEvent?.Dispatch();
        }
        
        private void OnRacketToggle(bool useRightHand)
        {
            _useRightHandForRacketEvent?.Dispatch(useRightHand);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            // MRA: create fade out (make sure alpha = 1 on Reset) 
        }
    }
}