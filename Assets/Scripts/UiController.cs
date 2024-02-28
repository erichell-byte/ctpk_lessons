using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text hintText;

	private void Awake()
	{
		hintText.text = "Press Left mouse button to hit";
	}

	public void UpdateScore(int score)
	{
		scoreText.text = score.ToString();
	}
}
