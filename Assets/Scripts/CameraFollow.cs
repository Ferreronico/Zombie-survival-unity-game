using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private float targetY;
    public float cameraMinHeight;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

	float nextPlayerSearchTime = 0;

    private void LateUpdate()
    {

		if (target == null) {
			FindPlayer ();
			return;
		}
        // gets y coordinate of player 
        targetY = target.position.y;

        // calculate desired position to camere be at
        Vector3 desiredPosition = target.position + offset;

        //  if player is below camera minimal height
        if (targetY < cameraMinHeight)
        {
            // set desired position Y coordinate equal to camera min height
            desiredPosition.y = cameraMinHeight;

            // do smoothing
            Vector3 smoothedPostion = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPostion;
        }
        else
        {   
            // set desired position Y coordinate to be on player
            desiredPosition.y = targetY;
            // do smoohing
            Vector3 smoothedPostion = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPostion;
        }
       
    }

	void FindPlayer() {
		if (nextPlayerSearchTime <= Time.time) {
			GameObject searchRezult = GameObject.FindGameObjectWithTag ("Player");
			if (searchRezult != null) {
				target = searchRezult.transform;
				nextPlayerSearchTime = Time.time + 0.5f;
			}
		}

	}
}
