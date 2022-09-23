using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GBJam : MonoBehaviour {

	[SerializeField] AudioSource sfx;
	[SerializeField] float duration = 3f;
	[SerializeField] float speed = 25f;

	RectTransform rt;
	float movement;
	bool playingSFX;

	void Start() 
	{
		rt = GetComponent<RectTransform>();
	}
	
	void Update () 
	{
		if (rt.anchoredPosition.y > 0) 
		{
			movement = speed * Time.deltaTime;
			rt.localPosition = new Vector3 (rt.localPosition.x, rt.localPosition.y - movement, rt.localPosition.z);
		} 
		else if (!playingSFX) 
		{
			sfx.Play();
			playingSFX = true;
			StartCoroutine(NextScene());
		}
	}

	IEnumerator NextScene()
    {
		yield return new WaitForSeconds(duration);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
