using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nl.allon.utils
{
	public abstract class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
	{
		private static  T _instance;
		public static T Instance { get { return _instance; } }
		public static bool IsInitiliazed { get { return _instance != null; } }

		protected virtual void Awake()
		{
			if(_instance != null) {
				Debug.LogErrorFormat("[BaseSingleton] Trying to instantiate a second instance of BaseSingleton class {0}" + GetType().Name);
			} else {
				_instance = (T)this;
			}
		}

		protected virtual void OnDestroy()
		{
			if(_instance == this) {
				_instance = null;
			}
		}
	}
}
