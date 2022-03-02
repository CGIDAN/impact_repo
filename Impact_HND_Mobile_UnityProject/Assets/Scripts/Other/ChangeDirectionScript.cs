using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class ChangeDirectionScript : MonoBehaviour
	{

		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.tag.Equals(Const.PLAYER))
			{

				PlayerController playerController = col.gameObject.GetComponent<PlayerController>();

				// Change planer walking direction
				if (playerController.AutoMoveRight)
				{
					playerController.AutoMoveRight = false;
				}
				else
				{
					playerController.AutoMoveRight = true;
				}

				// After hit the obstacle should go to the StartPosition
				gameObject.GetComponent<ObstacleWaypointController>().DirectionStartPoint(false);

				// Turn off the collider
				gameObject.GetComponent<Collider2D>().enabled = false;

				// And enable the collider after time
				StartCoroutine(EnableColliderAfterTime());
			}
		}

		IEnumerator EnableColliderAfterTime()
		{
			yield return (new WaitForSeconds(0.5f));
			gameObject.GetComponent<Collider2D>().enabled = true;
		}
	}
}
