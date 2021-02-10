using System.Collections;
using System.Collections.Generic;
using nl.allon.configs;
using TMPro;
using UnityEngine;

namespace nl.allon.views
{
    public class LevelInfoView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private TextMeshProUGUI _levelNumText = null;
        [SerializeField] private TextMeshProUGUI _levelNameText = null;
        [SerializeField] private CanvasGroup _canvasGroup = null;
        private int _levelNum;
        private string _levelName;

        private void Start()
        {
            Camera cam = Camera.main;
            if (cam == null) Debug.LogError("[LevelInfoView] No Main Camera found");
            else _canvas.worldCamera = cam;
        }
        
        public void Appear()
        {
            
        }

        public void Hide()
        {
            
        }

        public void SetInfo(LevelConfig config)
        {
            _levelNum = config.LevelNum;
            _levelName = config.LevelName;

            _levelNumText.text = _levelNum.ToString("00");
            _levelNameText.text = _levelName.ToString();
        }
    }
}