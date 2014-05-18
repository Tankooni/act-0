using UnityEngine;
using System.Collections;

public class AdjustForwardToPointToCamera : MonoBehaviour
{
    Transform _transform;
    Transform _cameraTransform;

	void Start()
    {
        _transform = transform;
        _cameraTransform = Camera.main.transform;
	}
	
	void Update()
    {
        _transform.rotation = Quaternion.FromToRotation(_transform.forward, _cameraTransform.forward) * _transform.rotation;
	}
}
