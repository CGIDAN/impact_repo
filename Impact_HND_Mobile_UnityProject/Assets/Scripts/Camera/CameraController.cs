using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class CameraController : MonoBehaviour
	{

		private Transform player;
		float smooth = 5.0f;
		float tiltAngle = 60.0f;






		void Update()
		{

			FindPlayer();

			// Calculate how fast we should be movin

			if (player != null)
			{
				// Follow the play
				float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
				float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

				transform.position = new Vector3(player.position.x, player.position.y, -10);
				transform.rotation = Quaternion.Slerp(transform.rotation, player.transform.rotation, Time.deltaTime * smooth);
			}
		}

		private void FindPlayer()
		{

			if (player == null)
			{
				GameObject p = GameObject.FindGameObjectWithTag(Const.PLAYER);

				if (p != null)
				{
					player = p.transform;
				}
			}
		}
	}
}