using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerState playerState;

    PlayerStateIdle stateIdle;
    PlayerStateRoll stateRoll;
    PlayerStateShoot stateShoot;
    PlayerStateWalk stateWalk;

    // Start is called before the first frame update
    void Start()
    {
        playerState = stateIdle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
