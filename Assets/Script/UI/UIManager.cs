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
	[Header("�}�b�v�f�[�^")]
	/// <summary>
	/// �}�b�v�p�f�[�^
	/// </summary>
	[SerializeField] private StageScriptableObject ScriptableObject;
	[SerializeField] private int SetNum = default;

	[Header("�I�v�V�����f�[�^")]
	/// <summary>
	/// �I�v�V������ʃf�[�^
	/// </summary>
	[SerializeField] private Canvas option = default;
	[SerializeField] private Button Button1 = default;
	[SerializeField] private Button Button2 = default;
	[SerializeField] private Button Button3 = default;
	[SerializeField] private Button OptButton = default;
	[SerializeField] private GameObject map = default;
	private CursorManager cursorManager;

	// �t�F�[�h�p�f�[�^
	[NonSerialized] public bool isFade = false;

	// Start is called before the first frame update
	void Start()
	{
		if (SceneManager.GetActiveScene().name != "TitleScene")
		{
			if (option)
			{
				// �I�v�V�����̕\�����I�t��
				option.enabled = false;
			}

			// �}�b�v������΃J�[�\���}�l�[�W�����l��
			if (map != null)
			{
				cursorManager = map.GetComponent<CursorManager>();
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

	public void FadeStart()
	{
		if (!isFade)
		{
			// �t�F�[�h���J�n
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
