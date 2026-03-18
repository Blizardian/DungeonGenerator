using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    List<RectInt> rooms = new List<RectInt>(); // Keep track of the rooms
    List<RectInt> finalRooms = new List<RectInt>(); // Keep track of the rooms that are completed

    [SerializeField] int minRoomWidth = 10; // The minimum width of a room
    [SerializeField] int minRoomHeight = 10; // the minimum height of a room

    [SerializeField] RectInt DungeonMap = new RectInt(0, 0, 100, 100); // A room that resambles the dungeon map

    public bool manualMode = true; // Enable manual mode or not

    public bool roomGenerationComplete = false; // Keeps track if the room generation is completed

    void Start()
    {
        rooms.Add(DungeonMap); // Add the dungeon map to the rooms list
    }

    // Update is called once per frame
    void Update()
    {
        DebugDrawRooms();

        AlgorithmsUtils.DebugRectInt(DungeonMap, Color.white, 0); // Makes the dungeon map white (The boundries)

        if (roomGenerationComplete)
        {
            return;
        }

        if (manualMode) // If manual mode is on
        {
            if (Input.GetKeyDown(KeyCode.Space)) // If space is pressed
            {
                SplitRoom(); // Run the SplitRoom method
            }
        }
        else
        {
            SplitRoom(); // Run the SplitRoom method
        }

        if(rooms.Count == 0) // if there are no rooms left to split
        {
            roomGenerationComplete = true; // Generation is completed, mark the boolean as true
            Debug.Log("Roomgeneration completed!");
        }
    }

    private void DebugDrawRooms()
    {
        foreach (RectInt room in rooms) // For each room in rooms
        {
            AlgorithmsUtils.DebugRectInt(room, Color.yellow, 0); // Draw the room yellow
        }

        foreach (RectInt room in finalRooms) // For each room in finalRooms
        {
            AlgorithmsUtils.DebugRectInt(room, Color.red, 0); // Draw the room red
            Debug.Log("Room in rooms: width = " + room.width + ", height = " + room.height); // Log the width and height to the console
        }
    }

    private void SplitRoom()
    {
        int wallSize = 1; // The size of the wall

        int roomIndex = -1; // Mark the index of a room that can be split, if it is -1 than there are no suitable rooms to split
        
        for (int i = 0; i < rooms.Count; i++) // See if there is a room that can be split
        {
            RectInt splittableRoom = rooms[i]; // local RectInt splittableRoom is now used for calling the room that the index is at

            bool canSplitHorizontal = splittableRoom.height >= minRoomHeight * 2 + wallSize; // Check if horizontal split is possible without making rooms too small
            bool canSplitVertical = splittableRoom.width >= minRoomWidth * 2 + wallSize; // Check if Vertical split is possible without making rooms too small

            if (canSplitHorizontal || canSplitVertical)
            {
                roomIndex = i; // the room can be split, make the roomIndex equal to the room that is splittable
                break; // Stop looking for other rooms
            }
        }

        if(roomIndex == -1) // if there are no rooms left to split, move them to finalRooms
        {
            finalRooms.AddRange(rooms); // Add the rooms that can't be split to the finalRooms
            rooms.Clear(); // Clear rooms, making it empty
            roomGenerationComplete = true; // Mark generation as completed
            return;
        }

        RectInt room = rooms[roomIndex]; // Local RectInt room is now used for calling the room that the roomIndex is equal to
        rooms.RemoveAt(roomIndex); // Remove the value the roomIndex has drom the rooms list

        bool canSplitHorizontally = room.height >= minRoomHeight * 2 + wallSize; // Is the room splittable horizontally?
        bool canSplitVertically = room.width >= minRoomWidth * 2 + wallSize; // Is the room splittable Vertically?


        bool splitHorizontally;
        
        if (canSplitHorizontally && canSplitVertically) // If the room can be split both ways, randomly choose horizontally or vertically
        {
            splitHorizontally = Random.value < 0.5f;
        }
        else
        {
            // If that is not possibe, split the only possible way
            splitHorizontally = canSplitHorizontally;
        }

        if (splitHorizontally)
        {
            // Calculate the min and max Y positions to split the room horizontally
            int minY = room.y + minRoomHeight;
            int maxY = room.yMax - minRoomHeight - wallSize + 1;
                        
            if (minY >= maxY)// If there’s no room to split
            {
                finalRooms.Add(room); // Move the room to the finalRooms
                return;
            }

            int splitY = Random.Range(minY, maxY); // Pick a random Y position to split

            // Create the two new rooms after the horizontal split
            RectInt bottom = new RectInt(room.x, room.y, room.width, splitY - room.y);
            RectInt top = new RectInt(room.x, splitY + wallSize, room.width, room.yMax - (splitY + wallSize));

            // Add the new rooms to the list
            rooms.Add(bottom);
            rooms.Add(top);
        }
        else
        {
            // Calculate the min and max X positions to split the room vertically
            int minX = room.x + minRoomWidth;
            int maxX = room.xMax - minRoomWidth - wallSize + 1;

            if (minX >= maxX) // If there’s no room to split
            {
                finalRooms.Add(room); // Move the room to the finalRooms
                return;
            }
                        
            int splitX = Random.Range(minX, maxX); // Pick a random X position to split

            // Create the two new rooms after the vertical split
            RectInt left = new RectInt(room.x, room.y, splitX - room.x, room.height);
            RectInt right = new RectInt(splitX + wallSize, room.y, room.xMax - (splitX + wallSize), room.height);

            // Add the new rooms to the list
            rooms.Add(left);
            rooms.Add(right);
        }
        Debug.Log("splitHorizontally: " + splitHorizontally); // Log the direction to the console
    }
}