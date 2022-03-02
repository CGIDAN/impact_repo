using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlanetRunner
{
	public class WaypointWalker : MonoBehaviour
	{

		[Tooltip("Add GameObjects as Waypoints")]
		public List<GameObject> WaypointPositions;
		public float speed = 1;

		private int currentWaypoint = 0;

		private Vector3 targetPositionDelta;
		private Vector3 moveDirection = Vector3.zero;

		void FixedUpdate()
		{
			WaypointWalk();
			Move();
		}

		void WaypointWalk()
		{

			if (WaypointPositions.Count > 0)
			{

				GameObject wp = (GameObject)WaypointPositions[currentWaypoint];
				Vector3 targetPosition = wp.transform.position;

				targetPositionDelta = targetPosition - transform.position;

				// if i´m near the next waypoint count one high
				if (targetPositionDelta.sqrMagnitude <= 0.01f)
				{

					currentWaypoint++;

					// If the last waypoint reached, start again
					if (currentWaypoint >= WaypointPositions.Count)
					{
						currentWaypoint = 0;
					}
				}
			}
		}

		protected virtual void Move()
		{
			moveDirection = targetPositionDelta.normalized * speed;
			transform.Translate(moveDirection * Time.deltaTime, Space.World);
		}
	}
}