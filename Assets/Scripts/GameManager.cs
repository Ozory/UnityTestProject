using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int Ovelhas {get;set;}
	public Canvas HD;

	private Text Pontos;

	private bool executeFocus = true;
	// Use this for initialization
	void Start () {
		Pontos = HD.gameObject.GetComponentsInChildren<Text>()[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(HD)
		{
			Pontos.text = "Pontos: "+Ovelhas.ToString();
		}
	}
}
