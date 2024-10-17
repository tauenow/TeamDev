using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNum", menuName = "ScriptableObjects/CreateStageNumAsset")]
public class StageScriptableObject : UnityEngine.ScriptableObject
{
	[SerializeField] public int StageNum = 1;
	public List<bool> isClearList = new List<bool>();
	

	
}
