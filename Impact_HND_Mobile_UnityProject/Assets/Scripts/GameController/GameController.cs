using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace PlanetRunner
{
	public class GameController : MonoBehaviour
	{

		[Tooltip("Wait X seconds on start to run")]
		public float WaitSecondsOnStart = 1.5f;

		public GameObject FinishDialog;
		public GameObject GameOverDialog;

		[HideInInspector]
		public bool StartGame = false;

		private bool gameFinished = false;

		void Start()
		{
			StartCoroutine(WaitAfterStartForSeconds());
		}

		IEnumerator WaitAfterStartForSeconds()
		{
			yield return (new WaitForSeconds(WaitSecondsOnStart));
			StartGame = true;
		}

		public void DoSetFinishLevel()
		{
			StartGame = false;

			gameFinished = true;
			FinishDialog.SetActive(true);
		}

		public void DoSetGameOverLevel()
		{
			StartGame = false;

			gameFinished = true;
			GameOverDialog.SetActive(true);

			GameObject player = GameObject.FindGameObjectWithTag(Const.PLAYER);

			if (player != null)
			{
				Destroy(player);
			}
		}

		void Update()
		{

			if (gameFinished)
			{

				bool doRestartScene = false;

				// If the game is finished or game over. After click space or tap restart scene
				if (Input.GetButtonDown("Jump"))
				{
					doRestartScene = true;
				}

				if (Input.touchCount > 0)
				{

					foreach (Touch t in Input.touches)
					{

						if (t.phase == TouchPhase.Began)
						{
							doRestartScene = true;
						}
					}
				}

				if (doRestartScene)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				}
			}
		}
	}
}
