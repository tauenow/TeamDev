using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectAnimation : MonoBehaviour
{
	[SerializeField] private StageScriptableObject scriptedObj;
	private UIManager manager;
	[SerializeField] private Button[] animButton;
	public float time, changeSpeed;
	public bool enlarge;

	void Start()
	{
		enlarge = false;
		manager = GetComponent<UIManager>();
	}

	void Update()
	{
		if (scriptedObj.SelectFaze == 1)
		{
			switch (scriptedObj.ButtonNum)
			{
				case 1:
					animButton[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					animButton[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

					changeSpeed = Time.deltaTime * 0.1f;

					if (time < 0)
					{
						enlarge = true;
					}

					if (time > 0.7f)
					{
						enlarge = false;
					}

					if (enlarge == true)
					{
						time += Time.deltaTime;
						animButton[0].transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
					}
					else
					{
						time -= Time.deltaTime;
						animButton[0].transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
					}
					break;
				case 2:
					animButton[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					animButton[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

					changeSpeed = Time.deltaTime * 0.1f;

					if (time < 0)
					{
						enlarge = true;
					}

					if (time > 0.7f)
					{
						enlarge = false;
					}

					if (enlarge == true)
					{
						time += Time.deltaTime;
						animButton[1].transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
					}
					else
					{
						time -= Time.deltaTime;
						animButton[1].transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
					}
					break;
				case 3:
					animButton[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
					animButton[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

					changeSpeed = Time.deltaTime * 0.1f;

					if (time < 0)
					{
						enlarge = true;
					}

					if (time > 0.7f)
					{
						enlarge = false;
					}

					if (enlarge == true)
					{
						time += Time.deltaTime;
						animButton[2].transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
					}
					else
					{
						time -= Time.deltaTime;
						animButton[2].transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
					}
					break;
				default:
					break;
			}
		}
	}
}
