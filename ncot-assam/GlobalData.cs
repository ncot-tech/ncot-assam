using Microsoft.Xna.Framework;
using RoomGen;

namespace ncot_assam
{
    public static class GlobalData
    {
        public static RoomManager roomManager;
        public static Room currentRoom;
        public static Vector2 currentRoomLocation { get { return _currentRoomLocation; } set { nextRoomLocation = value; } }
        public static Vector2 nextRoomLocation { get; private set; }
        private static Vector2 _currentRoomLocation;

        public static void Init()
        {
            roomManager = new RoomManager();
            roomManager.Generate();
            currentRoomLocation = Vector2.Zero;
            RoomSwitched();            
        }

        public static void RoomSwitched()
        {
            _currentRoomLocation = nextRoomLocation;
            currentRoom = roomManager.GetRoom(currentRoomLocation);
        }
    }
}
