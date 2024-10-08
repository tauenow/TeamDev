using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
	public Canvas option;
	public Button NextButton;
	public Button OptButton;
	[SerializeField]
	private GameObject map;
	private CursorManager cursorManager;

	// Start is called before the first frame update
	void Start()
	{
		option.enabled = false;

		if (map != null)
		{
			cursorManager = map.GetComponent<CursorManager>();
		}
		else
		{
			return;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void CreateOption()
	{
		option.enabled = true;
		NextButton.enabled = false;
		OptButton.enabled = false;
		if (map != null)
		{
			cursorManager.enabled = false;
		}
		else
		{
			return;
		}
	}

	public void DestroyOption()
	{
		option.enabled = false;
		NextButton.enabled = true;
		OptButton.enabled = true;
		if (map != null)
		{
			cursorManager.enabled = true;
		}
		else
		{
			return;
		}
	}
}
