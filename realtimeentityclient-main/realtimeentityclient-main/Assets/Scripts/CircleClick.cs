using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleClick : MonoBehaviour
{
    public int balloonId;
    void Start()
    {

    }
    void Update()
    {

    }
    void OnMouseDown()
    {
        NetworkClientProcessing.SendMessageToServer(ClientToServerSignifiers.BalloonClicked + "," + balloonId, TransportPipeline.ReliableAndInOrder);
    }
}
