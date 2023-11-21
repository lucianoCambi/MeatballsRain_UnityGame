using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public AudioClip deathEffect;
    [SerializeField] private float volume;

    void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Meteor")
		{
            //			Instantiate(deathEffect, transform.position, transform.rotation);
            GameManager.Instance.GameOver();

            SoundManager.Instance.PlaySound(deathEffect, this.transform.position, volume);
            //			AudioManager.instance.Play("PlayerDeath");

            Destroy(gameObject);
		}
	}

}
