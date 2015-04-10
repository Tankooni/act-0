using UnityEngine;
using System.Collections;

public class MoodIndicator : MonoBehaviour
{
    public Texture2D[] moodIndicators;
    public Color angryColor = Color.red;
    public Color happyColor = Color.green;
    public float fadeTime;
    public EnemyMood enemy;

    Transform _transform;
    Transform _cameraTransform;
    Material _material;
    Color _color;
    Quaternion _pointUpAtForward;

	void Awake()
    {
        _transform = transform;
        _cameraTransform = Camera.main.transform;
    }

	void Start()
    {
        _pointUpAtForward = Quaternion.FromToRotation(Vector3.up, Vector3.forward);

        _material = GetComponent<Renderer>().material;
        _material.mainTexture = moodIndicators[Mathf.Clamp(Mathf.RoundToInt(enemy.Mood/(100/moodIndicators.Length)), 0, moodIndicators.Length - 1)];

        var moodRatio = enemy.Mood/100;
        _material.color = _color = new Color(
            angryColor.r * (1 - moodRatio) + happyColor.r * moodRatio,
            angryColor.g * (1 - moodRatio) + happyColor.g * moodRatio,
            angryColor.b * (1 - moodRatio) + happyColor.b * moodRatio);
        Update();
    }
	
	// Update is called once per frame
	void Update()
    {
        transform.rotation = Quaternion.LookRotation(-_cameraTransform.forward, Vector3.up) * _pointUpAtForward;

        _color.a -= Time.deltaTime/fadeTime;
        _material.color = _color;
	}
}
