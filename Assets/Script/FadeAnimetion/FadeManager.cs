using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
	// �V�F�[�_�[�̃}�e���A�����Q��
	public Material material;

	// �⊮����l�̊J�n�_�ƏI���_
	public float startValue = 1.0f; // ����
	public float endValue = -1.0f;  // �s����

	// �t�F�[�h�̎���
	public float fadeDuration = 2.0f;

	// �V�[����
	public string nextSceneName;

	// �t�F�[�h��Ԃ��Ǘ�
	private bool isFading = false;
	private float currentTime = 0.0f;

	// �V�F�[�_�[�̃v���p�e�B��
	private string shaderPropertyName = "_Val";

	void Start()
	{
		// �}�e���A�����ݒ肳��Ă��Ȃ��ꍇ�A�����Ŏ擾
		if (material == null)
		{
			Renderer renderer = GetComponent<Renderer>();
			if (renderer != null)
			{
				material = renderer.material;
			}
		}

		// �V�F�[�_�[�̏����l��ݒ�
		material.SetFloat(shaderPropertyName, startValue);
	}

	void Update()
	{
		// �t�F�[�h���ł���΁A�t�F�[�h��i�s
		if (isFading)
		{
			currentTime += Time.deltaTime;

			// �t�F�[�h�i�s�x���v�Z
			float t = currentTime / fadeDuration;
			float currentVal = Mathf.Lerp(startValue, endValue, t);

			// �V�F�[�_�[�� _Val �ɕ⊮�����l��ݒ�
			material.SetFloat(shaderPropertyName, currentVal);

			// �t�F�[�h������������V�[����ύX
			if (currentTime >= fadeDuration)
			{
				SceneManager.LoadScene(nextSceneName); // ���̃V�[���ɐ؂�ւ�
			}
		}
	}

	// �t�F�[�h���J�n����֐� (�{�^���������ꂽ��Ă΂��)
	public void StartFade()
	{
		if (!isFading)
		{
			isFading = true;  // �t�F�[�h���J�n
			currentTime = 0.0f;  // �t�F�[�h���Ԃ����Z�b�g
		}
	}
}