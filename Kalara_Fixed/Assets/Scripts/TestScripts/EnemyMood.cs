using UnityEngine;
using System.Collections;

public class EnemyMood : MonoBehaviour
{
    public GameObject moodIndicatorPrefab;
    public float Mood;

    Transform _transform;
    MoodIndicator _currentIndicator;

	// Use this for initialization
	void Start()
    {
        _transform = transform;
        Mood = Random.Range(30, 99);
	}
	
	// Update is called once per frame
	void Update()
    {
        Mood -= Time.deltaTime/2;
	}

    void LookedAt()
    {
        if(_currentIndicator)
            return;
        _currentIndicator = ((GameObject)Instantiate(moodIndicatorPrefab, _transform.position + _transform.up * 3.5f, Quaternion.identity)).GetComponent<MoodIndicator>();
        _currentIndicator.enemy = this;
    }
}
