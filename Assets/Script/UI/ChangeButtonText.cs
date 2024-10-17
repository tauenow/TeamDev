using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class ChangeButtonText : MonoBehaviour
{
	[SerializeField] private Button button;
	private TextMeshProUGUI buttonText;

	// Start is called before the first frame update
	void Start()
	{
		// �{�^���̃e�L�X�g���l��
		buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "StageSelect")
		{
			buttonText.text = "�^�C�g���ɖ߂�";
		}
		else if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			buttonText.text = "�X�e�[�W�Z���N�g�ɖ߂�";
		}
	}
}
