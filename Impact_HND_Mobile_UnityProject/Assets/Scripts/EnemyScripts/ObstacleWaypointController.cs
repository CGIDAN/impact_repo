using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlanetRunner
{
	public class ObstacleWaypointController : MonoBehaviour
	{

		public GameObject StartPosition;
		public GameObject EndPosition;

		[Tooltip("How fast should the obstacle appear")]
		public float speed = 1;

		private bool lastWaypointStartPosition = true;
		private Vector3 targetPositionDelta;
		private Vector3 moveDirection = Vector3.zero;
		private bool doStartWalking = false;

		void Awake()
		{
			lastWaypointStartPosition = true;
			gameObject.transform.position = StartPosition.transform.position;
		}

		void FixedUpdate()
		{

			if (doStartWalking)
			{
				WaypointWalk();
				Move();
			}
		}

		void WaypointWalk()
		{

			GameObject wp;

			if (lastWaypointStartPosition)
			{
				wp = EndPosition;
			}
			else
			{
				wp = StartPosition;
			}

			Vector3 targetPosition = wp.transform.position;

			targetPositionDelta = targetPosition - transform.position;

			// If obstacle is neared the start or end position stop moving
			if (targetPositionDelta.sqrMagnitude <= 0.01f)
			{

				doStartWalking = false;

				if (lastWaypointStartPosition)
				{
					lastWaypointStartPosition = false;
				}
				else
				{
					lastWaypointStartPosition = true;
				}
			}
		}

		protected virtual void Move()
		{
			moveDirection = targetPositionDelta.normalized * speed;
			transform.Translate(moveDirection * Time.deltaTime, Space.World);
		}

		/*
		 * Is triggered from the ObstacleController and from the ChangeDirectionScript.
		 * If the player hits the outer circle collider, the obstacle starts going to the EndPosition.
		 */
		public void DirectionStartPoint(bool value)
		{
			if (value)
			{
				lastWaypointStartPosition = false;
			}
			else
			{
				lastWaypointStartPosition = true;
			}

			doStartWalking = true;
		}
	}
}