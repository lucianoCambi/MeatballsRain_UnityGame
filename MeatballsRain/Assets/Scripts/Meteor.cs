using UnityEngine;

public class Meteor : FauxGravityBody {

	public GameObject explosion;

    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float volume;

    public SphereCollider sphereCol;
	public ParticleSystem trail;

	void OnCollisionEnter(Collision col)
	{
		Quaternion rot = Quaternion.LookRotation(transform.position.normalized);
		rot *= Quaternion.Euler(90f, 0f, 0f);
		Instantiate(explosion, col.contacts[0].point, rot);

		sphereCol.enabled = false;
	//	trail.Stop(true, ParticleSystemStopBehavior.StopEmitting);

		this.enabled = false;
        SoundManager.Instance.PlaySound(audioClip, this.transform.position, volume);
        //	GetComponent<AudioSource>().Stop();

        Destroy(gameObject, 0.5f);
	}

}
