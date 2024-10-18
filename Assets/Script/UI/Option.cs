using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
	[SerializeField]
	private Canvas option = default;
	[SerializeField]
	private Button Button1 = default;
	[SerializeField]
	private Button Button2 = default;
	[SerializeField]
	private Button Button3 = default;
	[SerializeField]
	private Button OptButton = default;


	private CursorManager cursorManager;

	// Start is called before the first frame update
	void Start()
	{
		option.enabled = false;

		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager = GameObject.Find("map(Clone)").GetComponent<CursorManager>();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void CreateOption()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager.enabled = false;
		}
		else
		{
			option.enabled = true;

			if (Button1 != null)
			{
				Button1.enabled = false;
			}

			if (Button2 != null)
			{
				Button2.enabled = false;
			}

			if (Button3 != null)
			{
				Button3.enabled = false;
			}

			OptButton.enabled = false;
		}
	}

	public void DestroyOption()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager.enabled = true;
		}
		else
		{
			option.enabled = false;

			if (Button1 != null)
			{
				Button1.enabled = true;
			}

			if (Button2 != null)
			{
				Button2.enabled = true;
			}

			if (Button3 != null)
			{
				Button3.enabled = true;
			}

			OptButton.enabled = true;
		}
	}
}
