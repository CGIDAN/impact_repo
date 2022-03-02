using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class PlayerGravityController : MonoBehaviour
	{

		public PlayerController playerController;

		void OnTriggerStay2D(Collider2D col)
		{

			if (col.gameObject.tag.Equals(Const.PLANET))
			{
				playerController.SetLookTransform(col.gameObject.transform);

				PlanetController planetController = col.GetComponent<PlanetController>();

				if (planetController != null)
				{
					playerController.SetPlanetIdAndAlign(planetController.planetId, col.transform.position);
				}
			}
		}

		void OnTriggerEnter2D(Collider2D col)
		{

			if (col.gameObject.tag.Equals(Const.PLANET))
			{
				playerController.SetLookTransform(col.gameObject.transform);
			}
		}
	}
}
