using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    Sprite circleTexture;

    LinkedList<GameObject> balloons;

    void Start()
    {
        NetworkClientProcessing.SetGameLogic(this);
        balloons = new LinkedList<GameObject>();
    }

    void Update()
    {

    }

    public void SpawnNewBalloon(Vector2 screenPosition, int balloonId)
    {
        if (circleTexture == null)
            circleTexture = Resources.Load<Sprite>("Circle");

        GameObject balloon = new GameObject("Balloon");

        balloon.AddComponent<SpriteRenderer>();
        balloon.GetComponent<SpriteRenderer>().sprite = circleTexture;
        balloon.AddComponent<CircleClick>();
        balloon.AddComponent<CircleCollider2D>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
        pos.z = 0;
        balloon.transform.position = pos;

        balloon.GetComponent<CircleClick>().balloonId = balloonId;

        balloons.AddLast(balloon);
    }

    public void PopBalloon(int balloonId)
    {
        GameObject destroyThis = null;

        foreach (GameObject b in balloons)
        {
            if (b.GetComponent<CircleClick>().balloonId == balloonId)
            {
                destroyThis = b;
                break;
            }
        }

        if (destroyThis != null)
        {
            Destroy(destroyThis);
            balloons.Remove(destroyThis);
        }
    }
}
