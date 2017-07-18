﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State {

	Idle,
	Focused,
	Clicked

}

public class WaypointBehaviour : MonoBehaviour {


	protected State state = State.Idle;
	protected float scale = 1f;
	protected float animatedLerp = 1f;

	[SerializeField]
	protected float scaleIdleMin;
	[SerializeField]
	protected float scaleIdleMax;
	[SerializeField]
	protected float scaleAnimation;

	[SerializeField]
	protected float scaleFocusMin;
	[SerializeField]
	protected float scaleFocusMax;

	[SerializeField]
	protected AudioClip clip_click;


	protected void Update () {

		switch (state) {

		case State.Idle:
			Idle ();

			break;
		case State.Focused:
			Focus ();

			break;
		default:
			break;
		}

		gameObject.transform.localScale = Vector3.one * scale;
		animatedLerp = Mathf.Abs (Mathf.Cos(Time.time * scaleAnimation));
	}

	protected void Idle () {

		scale = Mathf.Lerp (scaleIdleMin, scaleIdleMax, animatedLerp);

	}

	protected void Focus () {

		scale = Mathf.Lerp (scaleFocusMin, scaleFocusMax, animatedLerp);

	}

	public void Enter () {

		state = state == State.Idle ? State.Focused : state;
	}

	public void Exit () {

		state = State.Idle;
	}

	public void Click () {


		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		iTween.MoveTo (player, 
			iTween.Hash("position", gameObject.transform.position,
						"time", 2, "easytype", "Linear"));
	}

}
