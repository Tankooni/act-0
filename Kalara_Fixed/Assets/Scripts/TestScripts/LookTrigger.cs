using UnityEngine;
using System.Collections;

public class LookTrigger : MonoBehaviour
{
    Transform _transform;

	void Start()
    {
        _transform = transform;
	}
	
	void Update()
    {
        RaycastHit hit;
        if(Physics.SphereCast(_transform.position + _transform.forward * 0.5f, 3f, _transform.forward, out hit, 400))
        {
            hit.collider.SendMessage("LookedAt", SendMessageOptions.DontRequireReceiver);
        }
	}
}
