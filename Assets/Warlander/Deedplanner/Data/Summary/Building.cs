using System.Collections.Generic;

namespace Warlander.Deedplanner.Data.Summary
{
    public class Building
    {
        private List<Room> rooms;

        public Building(List<Room> newRooms)
        {
            rooms = newRooms;
        }
    }
}