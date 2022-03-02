using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class DamageController : MonoBehaviour
	{

		// If the player hits a enemy collider the player died
		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.gameObject.tag.Equals(Const.PLAYER))
			{
				col.gameObject.GetComponent<PlayerController>().PlayerDie();
			}
		}
	}
}
