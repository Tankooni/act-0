using UnityEngine;
using System.Collections;

public class RiseAndDestroy : MonoBehaviour
{
    public float LiveTime = 2f;
    Transform _transform;
    Vector3 _origLocScale;
    float StartTime;

	// Use this for initialization
	void Start()
    {
        _transform = transform;
        _origLocScale = transform.localScale;
        //Destroy(gameObject, LiveTime);
        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update()
    {
        //_transform.position += Vector3.up * Time.deltaTime;
        //_transform.localScale = new Vector3(_origLocScale.x * (Mathf.Sin(Time.time - StartTime)+1), _origLocScale.x * (Mathf.Sin(Time.time - StartTime)+1), _origLocScale.x * (Mathf.Sin(Time.time - StartTime)+1));
	}
}
