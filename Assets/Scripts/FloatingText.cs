using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

	public int speed;
	private Text textComp;
	public float alphaFade;
	private RectTransform rect;

	void Awake()
	{
		textComp = GetComponent<Text>();
		rect = GetComponent<RectTransform>();
	}

	void OnEnable()
	{
		SetAlpha(1f);
		textComp.enabled = true;
		StartCoroutine(Fade());

	}

	private void SetAlpha(float a)
	{
		Color temp = textComp.color;
		temp.a = a;
		textComp.color = temp;
	}

	IEnumerator Fade()
	{
		while (textComp.color.a > 0)
		{
			rect.position = rect.position + Vector3.up * speed;
			SetAlpha(textComp.color.a - alphaFade);

			yield return null;
		}
		textComp.enabled = false;
		enabled = false;
	}
}
