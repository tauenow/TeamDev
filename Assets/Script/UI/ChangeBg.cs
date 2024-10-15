using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBg : MonoBehaviour
{
	[SerializeField] private StageScriptableObject ScriptableNum;
	[SerializeField] private Sprite[] ChangeSprites;

	private Image ChangeImage;

	// Start is called before the first frame update
	void Start()
	{
		ChangeImage = GetComponent<Image>();

		if (ScriptableNum.StageNum >= 0 && ScriptableNum.StageNum < 4)
		{
			ChangeImage.sprite = ChangeSprites[0];
		}
		else if (ScriptableNum.StageNum >= 4 && ScriptableNum.StageNum < 7)
		{
			ChangeImage.sprite = ChangeSprites[1];
		}
		else if (ScriptableNum.StageNum >= 7 && ScriptableNum.StageNum < 10)
		{
			ChangeImage.sprite = ChangeSprites[2];
		}
	}
}
