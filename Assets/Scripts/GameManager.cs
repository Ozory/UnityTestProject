using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int Ovelhas {get;set;}
	public Canvas HD;

	public Transform objeto;

	private Text Pontos;

	private bool executeFocus = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(HD)
		{
			Pontos = HD.gameObject.GetComponentsInChildren<Text>()[0];
			Pontos.text = "Pontos: "+Ovelhas.ToString();
			if(Ovelhas>1 && executeFocus==true)
			{
				CameraManager f = Camera.main.GetComponent<CameraManager>() as CameraManager;
//				f.ChangeTarget(objeto,10f,5f, 0);
				f.ZoomIn(5);
				executeFocus=false;
			}
		}
	}
}
