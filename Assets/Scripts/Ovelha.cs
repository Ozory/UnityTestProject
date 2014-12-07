using UnityEngine;
using System.Collections;

public class Ovelha : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="Player")
		{
			GameManager.Ovelhas++;
			Destroy(this.gameObject);
		}
	}
}
