using UnityEngine;
using System.Collections.Generic;

public class DungeonPrototype : MonoBehaviour
{
    List<RectInt> rooms = new List<RectInt>();

    public bool splitVertically;
    public bool splitHorizontally;

    public bool V1;

    RectInt bigRoom = new RectInt(0, 0, 100, 50);


    RectInt leftRoomVerticallyV1 = new RectInt(0, 0, 49, 25);
    RectInt rightRoomVerticallyV1 = new RectInt(50, 0, 50, 25);

    RectInt leftRoomHorizontallyV1 = new RectInt(0, 0, 100, 24);
    RectInt middleRoomHorizontally1V1 = new RectInt(0, 0, 100, 36);
    RectInt middleRoomHorizontally2V1 = new RectInt(0, 0, 100, 37);
    RectInt rightRoomHorizontallyV1 = new RectInt(0, 25, 100, 25);


    RectInt leftRoomVerticallyV2 = new RectInt(0, 0, 49, 50);
    RectInt middleRoomVertically1 = new RectInt(0, 0, 74, 50);
    RectInt middleRoomVertically2 = new RectInt(0, 0, 75, 50);
    RectInt rightRoomVerticallyV2 = new RectInt(50, 0, 50, 50);

    RectInt leftRoomHorizontallyV2 = new RectInt(0, 0, 50, 24);
    RectInt rightRoomHorizontallyV2 = new RectInt(0, 25, 50, 25);

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
        if (V1 == true)
        {
            if (splitHorizontally == true)
            {
                rooms.Add(leftRoomHorizontallyV1);
                rooms.Add(rightRoomHorizontallyV1);
                rooms.Add(middleRoomHorizontally1V1);
                rooms.Add(middleRoomHorizontally2V1);
            }
            else
            {
                rooms.Remove(leftRoomHorizontallyV1);
                rooms.Remove(rightRoomHorizontallyV1);
                rooms.Remove(middleRoomHorizontally1V1);
                rooms.Remove(middleRoomHorizontally2V1);
            }
        }
        else
        {
            if (splitHorizontally == true)
            {
                rooms.Add(leftRoomHorizontallyV2);
                rooms.Add(rightRoomHorizontallyV2);
            }
            else
            {
                rooms.Remove(leftRoomHorizontallyV2);
                rooms.Remove(rightRoomHorizontallyV2);
            }
        }


    }

    private void SplitItVertically()
    {
        if (V1 == true)
        {
            if (splitVertically == true)
            {
                rooms.Add(leftRoomVerticallyV1);
                rooms.Add(rightRoomVerticallyV1);
            }
            else
            {
                rooms.Remove(leftRoomVerticallyV1);
                rooms.Remove(rightRoomVerticallyV1);
            }
        }
        else
        {
            if (splitVertically == true)
            {
                rooms.Add(leftRoomVerticallyV2);
                rooms.Add(rightRoomVerticallyV2);
                rooms.Add(middleRoomVertically1);
                rooms.Add(middleRoomVertically2);
            }
            else
            {
                rooms.Remove(leftRoomVerticallyV2);
                rooms.Remove(rightRoomVerticallyV2);
                rooms.Remove(middleRoomVertically1);
                rooms.Remove(middleRoomVertically2);
            }
        }

    }
}
