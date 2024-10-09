using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	[SerializeField]
	private GameObject map = default;
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
		Button1.enabled = false;
		Button2.enabled = false;
		Button3.enabled = false;
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
		Button1.enabled = true;
		Button2.enabled = true;
		Button3.enabled = true;
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
