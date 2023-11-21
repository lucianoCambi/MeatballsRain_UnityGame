using UnityEngine.UI;
using UnityEngine;

public class ScoreUI : MonoBehaviour {

	public Text text;

	RectTransform rt;
	Vector2 startPos;

	void Start ()
	{
		rt = GetComponent<RectTransform>();
		startPos = rt.anchoredPosition;
    }

	void Update ()
	{
		text.text = Planet.Score.ToString("#") + "m";

		rt.anchoredPosition = Vector2.Lerp(Vector2.zero, startPos, Planet.Size);
	}

}
