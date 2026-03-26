using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    List<RectInt> rooms = new List<RectInt> (); // List with the rooms that can still split
    List<RectInt> finalRooms = new List<RectInt> (); // List with the rooms that can't split anymore

    RectInt OriginalRoom = new RectInt (0,0,100,100); // The size of the original room

    public int heightMinimum = 10; // Minimum height of a room
    public int widthMinimum = 10; // Minimum widht of a room

    public bool manualActivated; // Do we manually create the rooms or automaticly?
    public bool splitHorizontally; // Do we split horizontally or vertically

    public int wallThickness = 1; // The thickness of the wall

    private void Start()
    {
        rooms.Add (OriginalRoom);
    }

    private void Update()
    {
        DebugDrawRooms();

        if (manualActivated)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SplitRooms();
            }
        }
        else
        {
            SplitRooms ();
        }
    }
    private void DebugDrawRooms()
    {
        foreach (RectInt room in rooms) // For each room in rooms
        {
            AlgorithmsUtils.DebugRectInt(room, Color.yellow, 0); // Draw the room yellow
        }
        foreach (RectInt room in finalRooms) // For each room in rooms
        {
            AlgorithmsUtils.DebugRectInt(room, Color.red, 0); // Draw the room red
        }
    }

    private void SplitRooms()
    {
        if (rooms.Count == 0) // If there are no rooms left
        {
            return;
        }

        int randomIndex = Random.Range(0, rooms.Count); // Makes sure the room that is selected is random

        RectInt room = rooms[randomIndex]; // local variabe room will be a room in rooms, it is random
        rooms.RemoveAt(randomIndex); // remove that room from the rooms list

        bool canSplitVertically = room.width >= widthMinimum * 2 + wallThickness; // Checks if the condition is true or false
        bool canSplitHorizontally = room.height >= heightMinimum * 2 + wallThickness; // Checks if the condition is true or false

        if (!canSplitVertically && !canSplitHorizontally)
        {
            finalRooms.Add(room); // Finalize the room since it is too small
            return;
        }

        bool splitVertically = canSplitVertically && (!canSplitHorizontally || Random.value > 0.5f); // Turns the bool true if it the first condition is true and one of the other 2 are true

        if (splitVertically) // Split the room vertically
        {
            int wallPosition = Random.Range(widthMinimum + 1, room.width - widthMinimum - 1); // Place walls randomly but make sure the new rooms meet the minimum requirements

            RectInt left = new RectInt(room.x, room.y, wallPosition, room.height); // Split the left room
            RectInt right = new RectInt(room.x + wallPosition + wallThickness, room.y, room.width - wallPosition - wallThickness, room.height); // Split the right room

            rooms.Add(left); // Add the left room to the rooms list
            rooms.Add(right); // Add the right room to the rooms list
        }
        else // Split the room horizontally
        {
            int wallPosition = Random.Range(heightMinimum + 1, room.height - heightMinimum - 1); // Place walls randomly but make sure the new rooms meet the minimum requirements

            RectInt top = new RectInt(room.x, room.y, room.width, wallPosition); // Split the top room
            RectInt bottom = new RectInt(room.x, room.y + wallPosition + wallThickness, room.width, room.height - wallPosition - wallThickness); // Split the bottom room

            rooms.Add(top); // Add the top room to the rooms list
            rooms.Add(bottom); // Add the bottom room to the rooms list
        }
    }
}
