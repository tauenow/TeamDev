using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNum", menuName = "ScriptableObjects/CreateStageNumAsset")]
public class StageScriptableObject : UnityEngine.ScriptableObject
{
	// ステージ番号
	[SerializeField] public int StageNum = 1;
	// 難易度
	[SerializeField] public int DifficultyIndex = 1;
	// 各ステージのクリア判定
	public List<bool> isClearList = new List<bool>();
	// チュートリアルクリアしてるか
	[SerializeField] public bool tutorialClear = false;
	// ボタンの番号
	[SerializeField] public int ButtonNum = 0;
	// 各ステージの色
	public string colorName = "None";
}
