using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNum", menuName = "ScriptableObjects/CreateStageNumAsset")]
public class StageScriptableObject : UnityEngine.ScriptableObject
{
	// �X�e�[�W�ԍ�
	[SerializeField] public int StageNum = 1;
	// ��Փx
	[SerializeField] public int DifficultyIndex = 1;
	// �e�X�e�[�W�̃N���A����
	public List<bool> isClearList = new List<bool>();
	// �`���[�g���A���N���A���Ă邩
	[SerializeField] public bool tutorialClear = false;
	// �{�^���̔ԍ�
	[SerializeField] public int ButtonNum = 0;
	// �e�X�e�[�W�̐F
	public string colorName = "None";
}
