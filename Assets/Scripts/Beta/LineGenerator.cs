using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    List<RectInt> rooms = new List<RectInt>();

    [SerializeField] int minRoomWidth = 10;
    [SerializeField] int minRoomHeight = 10;

    RectInt DungeonMap = new RectInt(0, 0, 100, 100);

    public bool splitHorizontally = true;

    //TODO Split the rooms into smaller rooms, until the minimum is met, this way you have a dungeon.
    void Start()
    {
        rooms.Add(DungeonMap);
    }

    // Update is called once per frame
    void Update()
    {
        DebugDrawRooms();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SplitRooms();
        }
    }

    private void DebugDrawRooms()
    {
        foreach (RectInt room in rooms)
        {
            AlgorithmsUtils.DebugRectInt(room, Color.red, 0);
        }
    }

    private void SplitRooms()
    {
        // TODO
        // Have the new rooms replace the rooms, so that it splits that room in half instead of the big first one, but at the same time I need to keep track and display all rooms.
        //List<RectInt>newRooms = new List<RectInt>();

        if (splitHorizontally)
        {
            RectInt HorizontalLeftRoomSplit = new RectInt(0, 0, 100, Random.Range(0 + minRoomWidth, 100 - minRoomHeight));
            RectInt HorizontaRightRoomSplit = new RectInt(0, 0, 100, HorizontalLeftRoomSplit.height + 1);
            rooms.Add(HorizontalLeftRoomSplit);
            rooms.Add(HorizontaRightRoomSplit);
            splitHorizontally = !splitHorizontally;
            Debug.Log("splitHorizontally is" + splitHorizontally);
        }
        else
        {
            RectInt VerticalLeftRoomSplit = new RectInt(0, 0, Random.Range(0 + minRoomWidth, 100 - minRoomHeight), 100);
            RectInt VerticalRightRoomSplit = new RectInt(0,0, VerticalLeftRoomSplit.width + 1, 100);
            rooms.Add(VerticalLeftRoomSplit);
            rooms.Add(VerticalRightRoomSplit);
            splitHorizontally = !splitHorizontally;
            Debug.Log("splitHorizontally is" + splitHorizontally);
        }
        
    }
}
