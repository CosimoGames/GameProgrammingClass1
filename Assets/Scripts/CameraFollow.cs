using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	//Settings
	public Transform target;									//object camera will focus on and follow
	private Transform followTarget;
	//Settings - Camera behavour
	public Vector3 targetOffset =  new Vector3(0f, 6f, -5f);	//how far back should camera be from the lookTarget
	public bool lockRotation;									//should the camera be fixed at the offset (for example: following behind the player)
	public float followSpeed = 2;								//how fast the camera follows
	public float rotateDamping = 100;							//how fast camera rotates to look at target							

	//Settings - Rotate on spot behavour
	public float rotateOnSpotCameraFollowSpeed = 3.5f;
	public Vector3 targetOffsetRotateOnSpot =  new Vector3(0f, 11f, -8f);
	public static bool rotateOnSpot = false;
	public static bool rotateOnSpotCameraLock = true;
	
	//Setup objects
	void Awake()
	{
		followTarget = new GameObject().transform;	//create empty gameObject as camera target, this will follow and rotate around the player
		followTarget.name = "Camera Target";
		if (!target) {
			Debug.LogError ("'CameraFollow script' has no target assigned to it", transform);
		}
	}
	
	//Runs the SmoothFollow & SmoothLookAt camera function every frame
	void Update()
	{
		if (!target)
			return;
		
		SmoothFollow ();
		SmoothLookAt ();

	}

	//rotates the camera smoothly toward the target
	void SmoothLookAt()
	{
		Quaternion rotation = Quaternion.LookRotation (target.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotateDamping * Time.deltaTime);
	}
		
	//move camera smoothly toward its target
	void SmoothFollow()
	{
		//Sets Camera offset
		Vector3 tempTargetOffset;
		float tempFollowSpeed;

		if (rotateOnSpot) {
			tempTargetOffset = targetOffsetRotateOnSpot; //Sets camera offset for rotation on the spot 
			tempFollowSpeed = rotateOnSpotCameraFollowSpeed;
		} else {
			tempTargetOffset = targetOffset; //Sets camera offset for normal movement
			tempFollowSpeed = followSpeed;
		}

		//move the followTarget (empty gameobject created in awake) to correct position each frame
		followTarget.position = target.position;
		followTarget.Translate(tempTargetOffset, Space.Self); //Moves the object relative to itself
		//Sets the offset target as the same rotation as the target 
		//this makes the camera stay behind the player
		if (lockRotation && (!rotateOnSpot || !rotateOnSpotCameraLock)) {
			followTarget.rotation = target.rotation;
		}
	
		//where should the camera be next
		transform.position = Vector3.Lerp(transform.position, followTarget.position, tempFollowSpeed * Time.deltaTime);

	}
}