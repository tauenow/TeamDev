using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
	[Header("�I�v�V�����̃L�����o�X")]
	[SerializeField] private Canvas option = default;
	[Header("�\������Ă���{�^��")]
	[SerializeField] private Button[] buttons = default;
	[Header("�{�^���̐�")]
	[SerializeField] private int buttonCount = 0;
	// �J�[�\���}�l�[�W��-
	private CursorManager cursorManager;
	// Do Once�p
	private bool isAction;

	// Start is called before the first frame update
	void Start()
	{
		option.enabled = false;
		isAction = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene" && isAction == true)
		{
			cursorManager = GameObject.Find("map(Clone)").GetComponent<CursorManager>();
			isAction = false;
		}
	}

	public void CreateOption()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager.enabled = false;
		}
		SetOption(true);
	}

	public void DestroyOption()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager.enabled = true;
		}
		SetOption(false);
	}

	void SetOption(bool enable)
	{
		option.enabled = enable;

		for (int i = 0; i < buttonCount; i++)
		{
			buttons[i].enabled = !enable;
		}
	}
}
