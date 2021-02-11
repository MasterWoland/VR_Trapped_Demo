using Pixelplacement;
using TMPro;
using UnityEngine;

namespace nl.allon.views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private CanvasGroup _canvasGroup = null;
        [SerializeField] private TextMeshProUGUI _scoreText = default;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            Camera cam = Camera.main;
            if (cam == null) Debug.LogError("[ScoreView] No Main Camera found");
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
    }
}