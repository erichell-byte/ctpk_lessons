using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class GameController : MonoBehaviour
	{
		[SerializeField] private BuildsController buildsController;
		[SerializeField] private EnemyController enemyController;
		[SerializeField] private PlayerController playerController;
		[SerializeField] private UiController uiController;

		private int score;

		private void Start()
		{
			buildsController.OnBuildCreated += enemyController.SetAgentDestination;
			playerController.OnBuildCollected += IncreaseScore;
			enemyController.OnBuildCollected += DecreaseScore;
			uiController.UpdateScore(score);
		}

		private void IncreaseScore()
		{
			score++;
			uiController.UpdateScore(score);
		}

		private void DecreaseScore()
		{
			score--;
			uiController.UpdateScore(score);
		}

		private void OnDestroy()
		{
			buildsController.OnBuildCreated -= enemyController.SetAgentDestination;
			playerController.OnBuildCollected -= IncreaseScore;
			enemyController.OnBuildCollected -= DecreaseScore;
		}
	}
}