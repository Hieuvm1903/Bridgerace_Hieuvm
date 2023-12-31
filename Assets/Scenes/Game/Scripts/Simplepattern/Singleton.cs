using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

	private static T _instance;
	private static readonly object _instanceLock = new object();
	private static bool _quitting = false;

	public static T Instance
	{
		get
		{
			lock (_instanceLock)
			{
				if (_instance == null && !_quitting)
				{

					_instance = GameObject.FindObjectOfType<T>();
					if (_instance == null)
					{
						GameObject go = new GameObject(typeof(T).ToString());
						_instance = go.AddComponent<T>();

						DontDestroyOnLoad(_instance.gameObject);
					}
				}

				return _instance;
			}
		}
	}



}