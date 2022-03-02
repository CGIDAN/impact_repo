using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class PlanetController : MonoBehaviour
	{

		public int mass = 3;
		public int size = 100;
		public int gravitation = 30;

		[HideInInspector]
		public System.Guid planetId;

		void Awake()
		{
			// Each planet have a own unique id
			planetId = System.Guid.NewGuid();
		}
	}
}
