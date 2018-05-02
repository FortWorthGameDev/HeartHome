﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour {

    public IsCollidingWithPlayer AvoidObstacle;
    public Transform PlayerEndingPosition;

    CharacterMovement player;

    private void Start()
    {
        player = DontDestroyPlayerOnLoad.playerObject.GetComponent<CharacterMovement>();
        GameEventSystem.Instance.GameEnded.AddListener(GameEndedListener);
    }

    void GameEndedListener()
    {
        player.AutomatedMovement = true;
        StartCoroutine(MovePlayerToFinalPoint());
    }

    IEnumerator MovePlayerToFinalPoint()
    {
        var direction = PlayerEndingPosition.position - player.transform.position;
        while(Vector2.Distance(PlayerEndingPosition.position, player.transform.position) > .03f)
        {
            direction = direction.normalized;
            player.AutoPilot(direction.x, direction.y);
            yield return new WaitForFixedUpdate();
            direction = PlayerEndingPosition.position - player.transform.position;
        }

        Debug.Log("Done moving");
        player.AutoPilot(0f, .01f);   // Face up
        yield return null;
        player.AutoPilot(0f, 0f);   // Stop
    }
}
