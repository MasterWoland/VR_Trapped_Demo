using nl.allon.configs;
using UnityEngine;

namespace nl.allon.views
{
    public class LevelView : MonoBehaviour
    {
        private Transform _transform = default;
        private GameObject _environment = default;
        
        private void Awake()
        {
            _transform = transform;
            gameObject.SetActive(false);
        }

        public void PrepareNewLevel(LevelConfig config)
        {
            if (_environment != null)
            {
                Debug.Log("[LevelView] We already have an environment "+_environment.name);
                _environment = null; // MRA: or must we destroy it?
            }
            else
            {
                _environment = Instantiate(config.EnvironmentPrefab, _transform);
                this.gameObject.SetActive(true);
            }
        }
    }
}