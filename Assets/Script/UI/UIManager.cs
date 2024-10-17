using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.Net.NetworkInformation;
using DG.Tweening;
using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
	[Header("マップデータ")]
	/// <summary>
	/// マップ用データ
	/// </summary>
	[SerializeField] private StageScriptableObject ScriptableObject;
	[SerializeField] private int SetNum = default;

	[Header("オプションデータ")]
	/// <summary>
	/// オプション画面データ
	/// </summary>
	[SerializeField] private Canvas option = default;
	[SerializeField] private Button Button1 = default;
	[SerializeField] private Button Button2 = default;
	[SerializeField] private Button Button3 = default;
	[SerializeField] private Button OptButton = default;
	[SerializeField] private GameObject map = default;
	private CursorManager cursorManager;

	// フェード用データ
	[NonSerialized] public bool isFade = false;

	// Start is called before the first frame update
	void Start()
	{
		if (SceneManager.GetActiveScene().name != "TitleScene")
		{
			if (option)
			{
				// オプションの表示をオフに
				option.enabled = false;
			}

			// マップがあればカーソルマネージャを獲得
			if (map != null)
			{
				cursorManager = map.GetComponent<CursorManager>();
			}
		}
	}

	public void AddDifficulty()
	{
		// 難易度を指定するインデックスの増加
		ScriptableObject.DifficultyIndex++;

		if (ScriptableObject.DifficultyIndex > 3)
		{
			ScriptableObject.DifficultyIndex = 3;
		}
	}

	public void SubStructDifficulty()
	{
		// 難易度を指定するインデックスの増加
		ScriptableObject.DifficultyIndex--;

		if (ScriptableObject.DifficultyIndex < 0)
		{
			ScriptableObject.DifficultyIndex = 1;
		}
	}

	public void SetStageNum()
	{
		Debug.Log(ScriptableObject.DifficultyIndex);

		switch (ScriptableObject.DifficultyIndex)
		{
			case 1:
				// マップの番号をセットした内容に変更
				ScriptableObject.StageNum = SetNum;
				break;
			case 2:
				// マップの番号をセットした内容に変更
				ScriptableObject.StageNum = SetNum + 3;
				break;
			case 3:
				// マップの番号をセットした内容に変更
				ScriptableObject.StageNum = SetNum + 6;
				break;
		}
	}

	public void FadeStart()
	{
		if (!isFade)
		{
			// フェードを開始
			isFade = true;
		}
	}

	public void CreateOption()
	{
		option.enabled = true;

		if (Button1 != null)
		{
			Button1.enabled = false;
		}

		if (Button2 != null)
		{
			Button2.enabled = false;
		}

		if (Button3 != null)
		{
			Button3.enabled = false;
		}

		OptButton.enabled = false;
		if (map != null)
		{
			cursorManager.enabled = false;
		}
	}

	public void DestroyOption()
	{
		option.enabled = false;

		if (map != null)
		{
			cursorManager.enabled = true;
		}

		if (!isFade)
		{
			if (Button1 != null)
			{
				Button1.enabled = true;
			}

			if (Button2 != null)
			{
				Button2.enabled = true;
			}

			if (Button3 != null)
			{
				Button3.enabled = true;
			}

			OptButton.enabled = true;
		}
	}
}
