using UnityEngine;
using System.Collections;

public class Ovelha : MonoBehaviour
{

    void Awake()
    {
        
    }

    void OnTriggerEnter( Collider other )
    {
        if ( other.tag == "Player" )
        {
            GameManagerScript.Ovelhas = GameManagerScript.Ovelhas + 1;
            Destroy( this.gameObject );
        }
    }
}
