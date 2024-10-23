using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimetion : MonoBehaviour
{
	private Animator animator;
	private float CurrentTime;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		CurrentTime += Time.deltaTime;

		if (CurrentTime >= 10)
		{
			Debug.Log("Idleda");
			Idle();
			CurrentTime = 0;
		}
		else if (CurrentTime >= 5)
		{
			Debug.Log("Idleda");
			Jump();
		}

	}

	public void Idle()
	{
		animator.SetInteger("TransitonNo", 0);
	}

	public void Yubisasi()
	{

	}

	public void Run()
	{

	}

	public void Jump()
	{
		animator.SetInteger("TransitionNo", 3);
	}

}

