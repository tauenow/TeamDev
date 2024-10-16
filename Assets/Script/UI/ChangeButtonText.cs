using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ChangeButtonText : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private Sprite[] ChangeSprites;
	private Image buttonTex;

	// Start is called before the first frame update
	void Start()
	{
		// ボタンのテキストを獲得
		buttonTex = button.GetComponentInChildren<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "StageSelect")
		{
			buttonTex.sprite = ChangeSprites[0];
		}
		else if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			buttonTex.sprite = ChangeSprites[1];
		}
	}
}
