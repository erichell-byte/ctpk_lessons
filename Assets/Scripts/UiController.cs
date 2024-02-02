using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreText;

	public void UpdateScore(int score)
	{
		scoreText.text = score.ToString();
	}

}
