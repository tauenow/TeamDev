using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StageNum", menuName = "ScriptableObjects/CreateStageNumAsset")]
public class StageScriptableObject : UnityEngine.ScriptableObject
{
	[Header("�X�e�[�W�ԍ�")]
	[SerializeField] public int StageNum = 1;
	[Header("��Փx")]
	[SerializeField] public int DifficultyIndex = 1;
	[Header("�e�X�e�[�W�̃N���A����(�X�e�[�W�J�n��)")]
	public List<bool> oldIsClear = new List<bool>();
	[Header("�e�X�e�[�W�̃N���A����()")]
	public List<bool> isClearList = new List<bool>();
	[Header("�`���[�g���A���N���A���Ă邩")]
	[SerializeField] public bool tutorialClear = false;
	[Header("�{�^���̔ԍ�")]
	[SerializeField] public int ButtonNum = 0;
	[Header("�e�X�e�[�W�̐F")]
	public string colorName = "None";
	[Header("�`���[�g���A���e�L�X�g�̔ԍ�")]
	public int textIndex = 0;
}
