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
	[Header("�}�b�v�f�[�^")]
	/// <summary>
	/// �}�b�v�p�f�[�^
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
		// ��Փx���w�肷��C���f�b�N�X�̑���
		ScriptableObject.DifficultyIndex++;

		if (ScriptableObject.DifficultyIndex > 3)
		{
			ScriptableObject.DifficultyIndex = 3;
		}
	}

	public void SubStructDifficulty()
	{
		// ��Փx���w�肷��C���f�b�N�X�̑���
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
				// �}�b�v�̔ԍ����Z�b�g�������e�ɕύX
				ScriptableObject.StageNum = SetNum;
				break;
			case 2:
				// �}�b�v�̔ԍ����Z�b�g�������e�ɕύX
				ScriptableObject.StageNum = SetNum + 3;
				break;
			case 3:
				// �}�b�v�̔ԍ����Z�b�g�������e�ɕύX
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
