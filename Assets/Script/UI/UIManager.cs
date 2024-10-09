using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	/// <summary>
	/// �}�b�v�p�f�[�^
	/// </summary>
	[SerializeField] private StageScriptableObject ScriptableObject;
	[SerializeField] private int SetNum = default;

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
			else
			{
				return;
			}
		}
	}

	public void SetStageNum()
	{
		// �}�b�v�̔ԍ����Z�b�g�������e�ɕύX
		ScriptableObject.StageNum = SetNum;
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
