using UnityEngine;
using System.Collections;

namespace PlanetRunner
{
	public class PlayerSpaceCheck : MonoBehaviour
	{

		private PlayerController playerController;
		private GameController gameController;

		private float timer = 0;
		public int SpaceDeadTime = 6;

		void Awake()
		{
			playerController = gameObject.GetComponent<PlayerController>();

			gameController = GameObject.FindGameObjectWithTag(Const.GAMECONTROLLER).GetComponent<GameController>();
		}

		void Update()
		{

			if (!playerController.grounded)
			{
				timer += Time.deltaTime;


			}
			else
			{
				timer = 0;
			}

			if (timer >= SpaceDeadTime)
			{
				gameController.DoSetGameOverLevel();
			}
		}
	}
}
