using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetMissionWard : MonoBehaviour
{
	[Header("���L�I�u�W�F�N�g")][SerializeField] private StageScriptableObject scriptableObject;
	[Header("�ύX����摜")][SerializeField] private Image image;
	[Header("�؂�ւ���p�̉摜")][SerializeField] private Sprite[] sprites;

	// Start is called before the first frame update
	void Start()
	{
		if (scriptableObject.colorName == "red")
		{
			image.sprite = sprites[0];
		}
		else if (scriptableObject.colorName == "blue")
		{
			image.sprite = sprites[1];
		}
		else if (scriptableObject.colorName == "yellow")
		{
			image.sprite = sprites[2];
		}
		else if (scriptableObject.colorName == "green")
		{
			image.sprite = sprites[3];
		}
	}
}
