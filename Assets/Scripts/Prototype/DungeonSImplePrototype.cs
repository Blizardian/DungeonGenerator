using UnityEngine;
using System.Collections.Generic;

public class DungeonSimplePrototype : MonoBehaviour
{
    RectInt room1 = new RectInt(0, 0, 49, 50);
    RectInt room2 = new RectInt(50, 0, 50, 50);
    //RectInt room = new RectInt(0, 0, 100, 50);

    List<RectInt> rooms = new List<RectInt>();

    public bool splitVertically;
    public bool splitHorizontally;

    RectInt bigRoom = new RectInt(0, 0, 100, 50);


    RectInt leftRoomVertically = new RectInt(0, 0, 49, 50);
    RectInt rightRoomVertically = new RectInt(50, 0, 50, 50);

    RectInt leftRoomHorizontally = new RectInt(0, 0, 100, 24);
    RectInt rightRoomHorizontally = new RectInt(0, 25, 100, 25);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rooms.Add(bigRoom);
    }

    // Update is called once per frame
    void Update()
    {
        SplitItVertically();

        splitItHorizontally();

        foreach (RectInt room in rooms)
        {
            AlgorithmsUtils.DebugRectInt(room, Color.red, 0);
        }
    }

    private void splitItHorizontally()
    {
        if (splitHorizontally == true)
        {
            rooms.Add(leftRoomHorizontally);
            rooms.Add(rightRoomHorizontally);
        }
        else
        {
            rooms.Remove(leftRoomHorizontally);
            rooms.Remove(rightRoomHorizontally);
        }
    }

    private void SplitItVertically()
    {
        if (splitVertically == true)
        {
            rooms.Add(leftRoomVertically);
            rooms.Add(rightRoomVertically);
        }
        else
        {
            rooms.Remove(leftRoomVertically);
            rooms.Remove(rightRoomVertically);
        }
    }
}
