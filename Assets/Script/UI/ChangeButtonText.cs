using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ChangeButtonText : MonoBehaviour
{
	[SerializeField] private Button button;
	private TextMeshProUGUI buttonText;

	// Start is called before the first frame update
	void Start()
	{
		// ボタンのテキストを獲得
		buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "StageSelect")
		{
			buttonText.text = "タイトルに戻る";
		}
		else if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			buttonText.text = "ステージセレクトに戻る";
		}
	}
}
