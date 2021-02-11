using nl.allon.configs;
using Pixelplacement;
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

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            Camera cam = Camera.main;
            if (cam == null) Debug.LogError("[LevelInfoView] No Main Camera found");
            else _canvas.worldCamera = cam;
        }
        
        public void Appear()
        {
            float duration = 1f; // MRA: Magic Number Alert!
            Tween.Value(0f, 1f, OnUpdateFade, duration, 0f, null);
        }

        public void Hide()
        {
            float duration = 0.5f;// MRA: Magic Number Alert!
            Tween.Value(1f, 0f, OnUpdateFade, duration, 0f, null);
        }

        private void OnUpdateFade(float alpha)
        {
            _canvasGroup.alpha = alpha;
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