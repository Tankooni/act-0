using UnityEngine;
using System.Collections;

public class BlobMovementStateMachine : MonoBehaviour
{
    public bool IsActive = true;
    public Transform ActivePrfab;
    Transform MyTransform;

	IEnumerator Start()
    {
        MyTransform = transform;
        while(true)
        {
            yield return new WaitForSeconds(0);
            if(IsActive)
            {
                Transform spawnedObject = (Transform)Instantiate(ActivePrfab, MyTransform.position + MyTransform.up, MyTransform.rotation);
                spawnedObject.rigidbody.AddForce(MyTransform.up * Random.value * 10f);
            }
        }
	}
	
	void Update()
    {
	
	}
}
