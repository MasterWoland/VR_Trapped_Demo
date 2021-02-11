using nl.allon.events;
using nl.allon.managers;
using nl.allon.views;
using UnityEngine;

namespace nl.allon.controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameStateEvent _gameStateEvent = null;
        [SerializeField] private GameObject _mainMenuViewPrefab = default;
        private MainMenuView _view = null;
        private bool _isMenuActive = false;

        private void Awake()
        {
            _view = Instantiate(_mainMenuViewPrefab, transform).GetComponent<MainMenuView>();
        }

        private void HideMenu()
        {
            _view.Hide();
        }
        
        #region EVENTS
        private void OnEnable()
        {
            _gameStateEvent.Handler += OnGameStateEvent;
        }

        private void OnGameStateEvent(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.MENU:
                    _isMenuActive = true;
                    Debug.Log("[MENU] Activate Menu "+_isMenuActive);
                    break;
                default:
                    if (_isMenuActive)
                    {
                        // we de-activate the menu
                        HideMenu();
                        _isMenuActive = false;
                    }
                    break;
            }
        }
        #endregion
    }
}