using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNum", menuName = "ScriptableObjects/CreateStageNumAsset")]
public class StageScriptableObject : UnityEngine.ScriptableObject
{
	[SerializeField] public int StageNum = 1;
}
