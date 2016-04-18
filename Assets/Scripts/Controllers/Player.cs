using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Se o player está no chão
    /// </summary>
    public bool IsGrounded { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter( Collision collision )
    {
        this.IsGrounded = true;
    }

    void OnCollisionStay( Collision collision )
    {
        this.IsGrounded = true;
    }

    void OnCollisionExit( Collision collision )
    {
        this.IsGrounded = false;
    }

}
