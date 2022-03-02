using UnityEngine;
using System.Collections;

namespace PlanetRunner
{

	public class DestroyAfterTime : MonoBehaviour
	{

		public float Time;

		void Start()
		{
			Destroy(gameObject, Time);
		}
	}
}
