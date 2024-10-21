using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNum", menuName = "ScriptableObjects/CreateStageNumAsset")]
public class StageScriptableObject : UnityEngine.ScriptableObject
{
	[SerializeField] public int StageNum = 1;
	[SerializeField] public int DifficultyIndex = 1;
	public List<bool> isClearList = new List<bool>();
	[SerializeField] public int ButtonNum = 0;
	public int SelectFaze = 0;
}
