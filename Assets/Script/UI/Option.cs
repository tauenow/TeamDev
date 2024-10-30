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
	private Button[] buttons;

	private CursorManager cursorManager;

	// Start is called before the first frame update
	void Start()
	{
		option.enabled = false;
	}

	private void Update()
	{
		if (option.enabled == true && cursorManager != null)
		{
			cursorManager.enabled = false;
		}
	}

	public void CreateOption()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager = GameObject.Find("map(Clone)").GetComponent<CursorManager>();
			cursorManager.enabled = false;
		}

		option.enabled = true;

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].enabled = false;
		}
	}

	public void DestroyOption()
	{
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			cursorManager = GameObject.Find("map(Clone)").GetComponent<CursorManager>();
			cursorManager.enabled = true;
		}

		option.enabled = false;

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].enabled = true;
		}
	}
}
