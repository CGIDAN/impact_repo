using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class GameOverController : MonoBehaviour
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
				Debug.Log("GameOverController: GameController not found");
			}
		}

		void OnTriggerEnter2D(Collider2D col)
		{

			if (col.gameObject.tag.Equals(Const.PLAYER))
			{
				gameController.DoSetGameOverLevel();
			}
		}
	}

}
