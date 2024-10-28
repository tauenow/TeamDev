using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDraw : MonoBehaviour
{
	[Header("���L�I�u�W�F�N�g")]
	[SerializeField] private StageScriptableObject scriptableObject;

	[Header("�\������e�L�X�g")]
	[SerializeField] private TMP_Text text;

	[Header("�e�t�F�[�Y���Ƃ̃e�L�X�g")]
	[SerializeField] private String[] tutoText;

	[Header("���̕�����\������܂ł̎���[s]")]
	[SerializeField] private float delayDuration = 0.1f;

	// �R���[�`���i�[�p�ϐ�
	private Coroutine showCoroutine;

	private void Start()
	{
		scriptableObject.textIndex = 0;
		Show();
	}

	private void Update()
	{
		AddTextIndex();
	}

	/// <summary>
	/// �������艉�o��\������
	/// </summary>
	public void Show()
	{
		// �O��̉��o�����������Ă�����A��~
		if (showCoroutine != null)
			StopCoroutine(showCoroutine);

		// ������̍X�V
		if (scriptableObject.textIndex < 7)
			text.text = tutoText[scriptableObject.textIndex];

		// �P�������\�����鉉�o�̃R���[�`�������s����
		showCoroutine = StartCoroutine(ShowCoroutine());
	}

	// �P�������\�����鉉�o�̃R���[�`��
	private IEnumerator ShowCoroutine()
	{
		// �ҋ@�p�R���[�`��
		// GC Alloc���ŏ������邽�߃L���b�V�����Ă���
		var delay = new WaitForSeconds(delayDuration);

		// �e�L�X�g�S�̂̒���
		var length = text.text.Length;

		// �P�������\�����鉉�o
		for (var i = 0; i < length; i++)
		{
			// ���X�ɕ\���������𑝂₵�Ă���
			text.maxVisibleCharacters = i;

			// ��莞�ԑҋ@
			yield return delay;
		}

		// ���o���I�������S�Ă̕�����\������
		text.maxVisibleCharacters = length;

		showCoroutine = null;
	}

	void AddTextIndex()
	{
		//if (Input.touchCount <= 0)
		//{ return; }

		//Touch touch = Input.GetTouch(0);
		if (scriptableObject.textIndex != 1)
		{
			if (/*touch.phase == TouchPhase.Began ||*/ Input.GetMouseButtonDown(0))
			{
				if (showCoroutine == null && scriptableObject.textIndex < 7)
				{
					scriptableObject.textIndex++;
					if (scriptableObject.textIndex <= 6)
						Show();
				}
				if (scriptableObject.textIndex == 7)
				{
					scriptableObject.tutorialClear = true;
				}
			}
		}
	}
}
