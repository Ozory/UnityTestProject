using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    Player Player;

    Rigidbody PlayerRigd;

    [Range(0,5)]
    public int JumpHeight;

    void Awake()
    {
        this.Player = this.GetComponent<Player>();
        this.PlayerRigd = Player.GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( this.Player.IsGrounded && Input.GetKeyDown( "space" ) )
        {
            ExecuteJump();
        }
    }

    public void ExecuteJump()
    {
        PlayerRigd.AddForce( Vector3.up * ( JumpHeight * 100 ), ForceMode.Acceleration );
    }
}
