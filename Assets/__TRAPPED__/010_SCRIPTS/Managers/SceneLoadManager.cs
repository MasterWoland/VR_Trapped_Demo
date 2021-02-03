﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using nl.allon.utils;
using nl.allon.events;

namespace nl.allon.managers
{
	public enum SCENE_NAME
	{
		Boot = 0,
		Menu = 100, // not present at the moment
		Game = 1
	}

	public class SceneLoadManager : BaseSingleton<SceneLoadManager>
	{
		// LoadSceneMode: Single mode loads a standard Unity Scene which then appears on its own in the Hierarchy window.
		// Additive: loads a Scene which appears in the Hierarchy window while another is active.

		public SCENE_NAME CurrentSceneName = SCENE_NAME.Boot;
		[SerializeField] private SceneEvent _sceneLoadedEvent;
		private List<AsyncOperation> _loadOperations;
		private SCENE_NAME _loadingScene = SCENE_NAME.Boot;

		protected override void Awake()
		{
			base.Awake();
			_loadOperations = new List<AsyncOperation>();
			DontDestroyOnLoad(this.gameObject);
		}

		// public void LoadScene(SCENE_NAME sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
		// {
		// 	// When using SceneManager.LoadScene, the loading does not happen immediately, it completes in the next frame.
		// 	// This semi-asynchronous behavior can cause frame stuttering and can be confusing because load does not complete immediately.
		// 	
		// 	SceneManager.LoadScene(sceneName.ToString(), mode);
		// 	CurrentSceneName = sceneName;
		// }

		public void LoadSceneAsync(SCENE_NAME sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
		{
			// The Application loads the Scene in the background as the current Scene runs.

			AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName.ToString(), mode);

			if (ao == null) {
				Debug.Log("[SceneLoadManager] Unable to load level " + sceneName.ToString());
				return;
			}
					
			// MRA: we may need to unload a scene here
			_loadingScene = sceneName;
			ao.completed += OnLoadOperationComplete;
			_loadOperations.Add(ao);
		}

		// MRA: currently not in use
		public void UnloadLevel(SCENE_NAME sceneName)
		{
			// Destroys all GameObjects associated with the given Scene and removes the Scene from the SceneManager.
			AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName.ToString());
			ao.completed += OnUnloadOperationComplete;
		}

		private void OnLoadOperationComplete(AsyncOperation ao)
		{
			ao.completed -= OnLoadOperationComplete;

			if (_loadOperations.Contains(ao)) {
				_loadOperations.Remove(ao);

				if (_loadOperations.Count <= 0) {
					// the new scene has been loaded
					// MRA: perform a state change

					//Debug.Log("[SceneLoadManager] Dispatching event: scene has finished loading.");
					CurrentSceneName = _loadingScene;
					_sceneLoadedEvent?.Dispatch(CurrentSceneName);
				}
			}
		}

		private void OnUnloadOperationComplete(AsyncOperation ao)
		{
			ao.completed -= OnUnloadOperationComplete;

			// Clean up level
			// MRA: do whatever we need to do after unloading the scene
		}
	}
}
