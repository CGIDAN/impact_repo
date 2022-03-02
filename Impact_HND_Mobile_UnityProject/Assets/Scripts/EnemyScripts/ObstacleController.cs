using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class ObstacleController : MonoBehaviour
	{

		public GameObject Obstacle;
		private ObstacleWaypointController owc;

		void Awake()
		{
			owc = Obstacle.GetComponent<ObstacleWaypointController>();
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.tag.Equals(Const.PLAYER))
			{
				// If player is "in sight" ride out
				owc.DirectionStartPoint(false);
			}
		}

		void OnTriggerExit2D(Collider2D col)
		{
			if (col.gameObject.tag.Equals(Const.PLAYER))
			{
				// If player is "not in sight" ride in
				owc.DirectionStartPoint(true);
			}
		}
	}
}
