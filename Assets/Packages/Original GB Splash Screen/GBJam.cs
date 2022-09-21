using UnityEngine;
using System.Collections;

public class GBJam : MonoBehaviour {

	public AudioSource sfx;
	public float speed = 25f;

	private RectTransform rt;
	private float movement;
	private bool playingSFX;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (rt.anchoredPosition.y > 0) {
			movement = speed * Time.deltaTime;
			rt.localPosition = new Vector3 (rt.localPosition.x, rt.localPosition.y - movement, rt.localPosition.z);
		} else if (!playingSFX) {
			sfx.Play();
			playingSFX = true;
		}
	}
}
