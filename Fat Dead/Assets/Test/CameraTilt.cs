using UnityEngine;
using System.Collections;

public class CameraTilt : MonoBehaviour 
{

	public Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
 
    public GameObject player;

	public float shakePwr = 0.1f;
	public float shakeDur = 1f;
	private float shakeTimer;
	private float shakeAmout;

	// Use this for initialization
	void Start()
	{
	}

	void FixedUpdate()
    {
 
       
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
 
        transform.position = new Vector3(posX, posY, transform.position.z);
 
    }

	void Update(){

		if (Input.GetKeyDown (KeyCode.Space)) {
			ShakeCamera (shakePwr, shakeDur);
		}

		if (shakeTimer >= 0) {
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmout;

			transform.position = new Vector3 (transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
			shakeTimer -= Time.deltaTime;
		}
	
	}
    
	public void ShakeCamera(float shakePwr, float shakeDur){
		shakeAmout = shakePwr;
		shakeTimer = shakeDur;
	}
}