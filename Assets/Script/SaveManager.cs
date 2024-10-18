using System;
using System.Collections;
using System.Collections.Generic;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	// �ۑ�����ScriptableObject
	[SerializeField] private StageScriptableObject GameIndex;
	// �Z�[�u�̐ݒ�
	private QuickSaveSettings saveSettings;

	// Start is called before the first frame update
	void Start()
	{
		saveSettings = new QuickSaveSettings();
		saveSettings.SecurityMode = SecurityMode.Aes;
		saveSettings.Password = "CUNEY";
		saveSettings.CompressionMode = CompressionMode.Gzip;
	}

	private void OnApplicationQuit()
	{
		Save();
	}

	// �Z�[�u
	public void Save()
	{
		Debug.Log("�Z�[�u�f�[�^�ۑ���:" + Application.persistentDataPath);

		// QuickSaveWriter�̃C���X�^���X���쐬
		QuickSaveWriter writer = QuickSaveWriter.Create("SaveData", saveSettings);

		// �f�[�^����������
		writer.Write("ClearList", GameIndex.isClearList);
		writer.Write("Difficulty", GameIndex.DifficultyIndex);

		// �ύX�𔽉f
		writer.Commit();
	}

	// ���[�h
	public void Load()
	{
		if (FileAccess.Exists("SaveData") == false)
		{
			return;
		}

		// QuickSaveReader�̃C���X�^���X���쐬
		QuickSaveReader reader = QuickSaveReader.Create("SaveData", saveSettings);

		// �f�[�^��ǂݍ���
		GameIndex.isClearList = reader.Read<List<bool>>("ClearList");
		GameIndex.DifficultyIndex = reader.Read<int>("Difficulty");
	}
}
