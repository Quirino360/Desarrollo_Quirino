using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PLAYER_STATES
{
    STATE_IDDLE = 0,
    STATE_WALK,
    STATE_SHOOT,
    STATE_ROLL
}
public abstract class PlayerState : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Exit();

    public abstract void UpdateState();

}
