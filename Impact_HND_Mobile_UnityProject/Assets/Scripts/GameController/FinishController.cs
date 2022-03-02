using UnityEngine;
using System.Collections;

namespace PlanetRunner
{

	public class FinishController : MonoBehaviour
	{

		private GameController gameController;

		void Awake()
		{
			GameObject go = GameObject.FindGameObjectWithTag(Const.GAMECONTROLLER);

			if (go != null)
			{
				gameController = go.GetComponent<GameController>();

			}
			else
			{
				Debug.Log("FinishController: GameController not found");
			}
		}

		void OnTriggerEnter2D(Collider2D col)
		{

			if (col.gameObject.tag.Equals(Const.PLAYER))
			{
				gameController.DoSetFinishLevel();
			}
		}
	}
}
