using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCloud : MonoBehaviour
{
	// ScriptableObject�̊l��
	[SerializeField] private StageScriptableObject ScriptableNum;

	// �����I�u�W�F�N�g
	[SerializeField] GameObject MoningImage1;
	[SerializeField] GameObject MoningImage2;
	[SerializeField] GameObject LunchImage1;
	[SerializeField] GameObject LunchImage2;
	[SerializeField] GameObject EveningImage1;
	[SerializeField] GameObject EveningImage2;

	// �����̃L�����o�X�̐���
	[SerializeField]
	GameObject canvas;

	// �����҂�����
	[SerializeField]
	private float CreateDeray;

	// �����ꏊ
	private float RandPosY = 0.0f;
	private int RandIndex = 0;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("CreateImage");
	}

	IEnumerator CreateImage()
	{
		if (ScriptableNum.StageNum >= 0 && ScriptableNum.StageNum < 4)
		{
			while (true)
			{
				RandIndex = Random.Range(1, 3);

				switch (RandIndex)
				{
					case 1:
						RandPosY = Random.Range(-960.0f, 575.0f);
						Vector3 CreatePos1 = new Vector3(1100.0f, RandPosY, 0.0f);
						GameObject prefab1 = Instantiate(MoningImage1, CreatePos1, Quaternion.identity);
						prefab1.transform.SetParent(canvas.transform, false);
						yield return new WaitForSeconds(CreateDeray);
						break;
					case 2:
						RandPosY = Random.Range(-960.0f, 575.0f);
						Vector3 CreatePos2 = new Vector3(1100.0f, RandPosY, 0.0f);
						GameObject prefab2 = Instantiate(MoningImage2, CreatePos2, Quaternion.identity);
						prefab2.transform.SetParent(canvas.transform, false);
						yield return new WaitForSeconds(CreateDeray);
						break;
				}
			}
		}
		else if (ScriptableNum.StageNum >= 4 && ScriptableNum.StageNum < 7)
		{
			while (true)
			{
				RandIndex = Random.Range(1, 3);

				switch (RandIndex)
				{
					case 1:
						RandPosY = Random.Range(-960.0f, 575.0f);
						Vector3 CreatePos1 = new Vector3(1100.0f, RandPosY, 0.0f);
						GameObject prefab1 = Instantiate(LunchImage1, CreatePos1, Quaternion.identity);
						prefab1.transform.SetParent(canvas.transform, false);
						yield return new WaitForSeconds(CreateDeray);
						break;
					case 2:
						RandPosY = Random.Range(-960.0f, 575.0f);
						Vector3 CreatePos2 = new Vector3(1100.0f, RandPosY, 0.0f);
						GameObject prefab2 = Instantiate(LunchImage2, CreatePos2, Quaternion.identity);
						prefab2.transform.SetParent(canvas.transform, false);
						yield return new WaitForSeconds(CreateDeray);
						break;
				}
			}
		}
		else if (ScriptableNum.StageNum >= 7 && ScriptableNum.StageNum < 10)
		{
			while (true)
			{
				RandIndex = Random.Range(1, 3);

				switch (RandIndex)
				{
					case 1:
						RandPosY = Random.Range(-960.0f, 575.0f);
						Vector3 CreatePos1 = new Vector3(1100.0f, RandPosY, 0.0f);
						GameObject prefab1 = Instantiate(EveningImage1, CreatePos1, Quaternion.identity);
						prefab1.transform.SetParent(canvas.transform, false);
						yield return new WaitForSeconds(CreateDeray);
						break;
					case 2:
						RandPosY = Random.Range(-960.0f, 575.0f);
						Vector3 CreatePos2 = new Vector3(1100.0f, RandPosY, 0.0f);
						GameObject prefab2 = Instantiate(EveningImage2, CreatePos2, Quaternion.identity);
						prefab2.transform.SetParent(canvas.transform, false);
						yield return new WaitForSeconds(CreateDeray);
						break;
				}
			}
		}
	}
}
