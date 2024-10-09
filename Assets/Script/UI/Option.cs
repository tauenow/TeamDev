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

		if (Button1 != null)
		{
			Button1.enabled = false;
		}
		else
		{
			return;
		}

		if (Button2 != null)
		{
			Button2.enabled = false;
		}
		else
		{
			return;
		}

		if (Button3 != null)
		{
			Button3.enabled = false;
		}
		else
		{
			return;
		}

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

		if (Button1 != null)
		{
			Button1.enabled = true;
		}
		else
		{
			return;
		}

		if (Button2 != null)
		{
			Button2.enabled = true;
		}
		else
		{
			return;
		}

		if (Button3 != null)
		{
			Button3.enabled = true;
		}
		else
		{
			return;
		}

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
