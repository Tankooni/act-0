using UnityEngine;
using System.Collections;

public class RiseAndDestroy : MonoBehaviour
{
    public float LiveTime = 2f;
    Transform _transform;

	// Use this for initialization
	void Start()
    {
        _transform = transform;
        //Destroy(gameObject, LiveTime);
	}
	
	// Update is called once per frame
	void Update()
    {
        //_transform.position += Vector3.up * Time.deltaTime;
	}
}
