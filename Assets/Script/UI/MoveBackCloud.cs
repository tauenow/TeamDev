using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MoveBackCloud : MonoBehaviour
{
	[Header("�X�N���v�^�u���I�u�W�F�N�g")]
	[SerializeField] private StageScriptableObject scriptableObj;
	[Header("��������v���n�u")]
	[SerializeField] private GameObject prefab;
	[Header("�ړ����x")]
	[SerializeField] private float Speed = -1.0f;
	[Header("�폜������W")]
	[SerializeField] private float DestroyPos = -20.0f;
	[Header("�������J�n������W")]
	[SerializeField] private float CreatePos = -1.0f;
	[Header("�����������W")]
	[SerializeField] private float GeneratePos = 0.0f;
	[Header("�ϊ�����摜")]
	[SerializeField] private Image Image;
	[Header("�ϊ��Ɏg�p����摜")]
	[SerializeField] private Sprite[] changeSprites;
	[Header("��������L�����o�X")]
	[SerializeField] private Canvas canvas;

	// �w�i�̖���(�Q���ȏ�͐������Ȃ�����)
	private int BGcount = 0;
	// �w�i�̈ʒu
	private Vector3 pos;

	private void Start()
	{
		switch (scriptableObj.DifficultyIndex)
		{
			case 1:
				Image.sprite = changeSprites[0];
				break;
			case 2:
				Image.sprite = changeSprites[1];
				break;
			case 3:
				Image.sprite = changeSprites[2];
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		pos = transform.position;

		// �ړ�����
		pos.x += Speed * Time.deltaTime;
		transform.position = pos;

		// �w�i�������悤����
		if (pos.x <= CreatePos && BGcount < 1)
		{
			CreateCloud();
		}

		// �폜����
		if (pos.x <= DestroyPos)
		{
			// �폜
			Destroy(gameObject);
			BGcount--;
		}

		// ��Փx�ɍ��킹�ĉ摜��ύX
		switch (scriptableObj.DifficultyIndex)
		{
			case 1:
				Image.sprite = changeSprites[0];
				break;
			case 2:
				Image.sprite = changeSprites[1];
				break;
			case 3:
				Image.sprite = changeSprites[2];
				break;
		}
	}

	void CreateCloud()
	{
		Vector3 CreatePos1 = new Vector3(GeneratePos, 0.0f, 0.0f);
		GameObject createPrefab = Instantiate(prefab, CreatePos1, Quaternion.identity);
		createPrefab.transform.SetParent(canvas.transform, false);
		BGcount++;
	}
}
