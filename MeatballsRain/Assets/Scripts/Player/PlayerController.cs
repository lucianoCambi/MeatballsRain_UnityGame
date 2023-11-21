using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private PlayerCollision player;
	[SerializeField] private AudioClip audioClipDrive;
	[SerializeField] private AudioClip audioClipStart;
	[SerializeField] private float volume;
	private float volumeDrive = 0.2f;

	public float moveSpeed = 10f;
	public float rotationSpeed = 10f;

	private float rotation;
	private Rigidbody rb;

	//[SerializeField] private Button buttonLeft;
	//[SerializeField] private Button buttonRight;

	private bool left = false;
	private bool right = false;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        SoundManager.Instance.PlaySound(audioClipStart, player.transform.position, volume);
    }

	void Update() {
		if (GameManager.Instance.IsGamePlaying()) {
			if (left) {
				rotation = -1;
			}
			if (right) {
				rotation = +1;
			}
			if (!left && !right) {
				rotation = 0;
			}
            
        }
		if (!GameManager.Instance.IsPaused() && !GameManager.Instance.IsGameOver()) {
			SoundManager.Instance.PlaySound(audioClipDrive, player.transform.position, volumeDrive);
		}
		

        //rotation = Input.GetAxisRaw("Horizontal");

    }

    public void MoveLeft(bool _left) {
		left = _left;
    }

    public void MoveRight(bool _right) {
		right = _right;
    }

    void FixedUpdate ()
	{
		
			rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
			Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
			Quaternion deltaRotation = Quaternion.Euler(yRotation);
			Quaternion targetRotation = rb.rotation * deltaRotation;
			rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));
			//transform.Rotate(0f, rotation * rotationSpeed * Time.fixedDeltaTime, 0f, Space.Self);
		
	}

}
