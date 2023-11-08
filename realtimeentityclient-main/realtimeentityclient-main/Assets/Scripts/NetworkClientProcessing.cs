using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if (signifier == ServerToClientSignifiers.SpawnBalloon)
        {
            float screenPositionXPercent = float.Parse(csv[1]);
            float screenPositionYPercent = float.Parse(csv[2]);
            int balloonId = int.Parse(csv[3]);
            Vector2 screenPosition = new Vector2(screenPositionXPercent * (float)Screen.width, screenPositionYPercent * (float)Screen.height);
            gameLogic.SpawnNewBalloon(screenPosition, balloonId);
        }
        else if (signifier == ServerToClientSignifiers.BalloonPopped)
        {
            gameLogic.PopBalloon(int.Parse(csv[1]));
        }
    }

    static public void SendMessageToServer(string msg, TransportPipeline pipeline)
    {
        networkClient.SendMessageToServer(msg, pipeline);
    }

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkClient networkClient;
    static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkClient NetworkClient)
    {
        networkClient = NetworkClient;
    }
    static public NetworkClient GetNetworkedClient()
    {
        return networkClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int BalloonClicked = 1;
}

static public class ServerToClientSignifiers
{
    public const int SpawnBalloon = 1;
    public const int BalloonPopped = 2;
}

#endregion

