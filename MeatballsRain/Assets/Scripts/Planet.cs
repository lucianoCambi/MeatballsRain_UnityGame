using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


	public static float startSize;

    public static float Size

	{
		get { return myTransform.localScale.x; }
	}

	public static int Score
	{
        get { return (int)(startSize - (Size * 63)); }
	}

	private static Transform myTransform;

	public float shrinkSpeed = .05f;

	void Awake ()
	{
		myTransform = transform;
		startSize = myTransform.localScale.x * 63;
    }

	void Update ()
	{
		if (GameManager.Instance.IsGamePlaying()) {
			transform.localScale *= 1f - shrinkSpeed * Time.deltaTime;
		}
	}

}
