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
	[SerializeField] private Button[] Buttons;

	private void Start()
	{
		if (GameObject.Find("TutorialButton") != null)
		{
			for (int i = 0; i < Buttons.Length; i++)
			{
				Buttons[i].enabled = true;
			}
		}
		else
		{
			for (int i = 0; i < Buttons.Length - 1; i++)
			{
				Buttons[i].enabled = true;
			}
		}
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
				ScriptableObject.oldIsClear[SetNum] = ScriptableObject.isClearList[SetNum];
				break;
			case 2:
				// �}�b�v�̔ԍ����Z�b�g�������e�ɕύX
				ScriptableObject.StageNum = SetNum + 3;
				ScriptableObject.oldIsClear[SetNum] = ScriptableObject.isClearList[SetNum];
				break;
			case 3:
				// �}�b�v�̔ԍ����Z�b�g�������e�ɕύX
				ScriptableObject.StageNum = SetNum + 6;
				ScriptableObject.oldIsClear[SetNum] = ScriptableObject.isClearList[SetNum];
				break;
		}
	}

	public void SetButtonEnable()
	{
		if (GameObject.Find("TutorialButton") != null)
		{
			for (int i = 0; i < Buttons.Length; i++)
			{
				Buttons[i].enabled = false;
			}
		}
		else
		{
			for (int i = 0; i < Buttons.Length - 1; i++)
			{
				Buttons[i].enabled = false;
			}
		}
	}

	public void ResetTutorial()
	{
		ScriptableObject.textIndex = 0;
		ScriptableObject.tutorialClear = false;
	}

	public void SetSceneName()
	{
		ScriptableObject.oldSceneName = "StageSelect";
	}
}
