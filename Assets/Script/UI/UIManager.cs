using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
	[SerializeField] private int SelectSetNum = 0;

	private void Start()
	{
		if (ScriptableObject.ButtonNum != 0)
		{
			ScriptableObject.ButtonNum = 0;
		}

		ScriptableObject.SelectFaze = 0;
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

	public void SetSelectNum()
	{
		ScriptableObject.ButtonNum = SelectSetNum;
	}

	public void SetSelectIndex()
	{
		if (ScriptableObject.SelectFaze < 3)
		{
			ScriptableObject.SelectFaze++;
		}
	}
}
