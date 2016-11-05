using System;
using Nez;
using RoomGen;
using Microsoft.Xna.Framework;

namespace ncot_assam
{
    class ExitHitDetector : Component, ITriggerListener
    {
        public void onTriggerEnter(Collider other, Collider local)
        {
            Debug.log("onTriggerEnter: {0} entered {1}", other, local);
            Debug.log("Trying to exit through: {0}", other.entity.name);

            switch (other.entity.name)
            {
                case "Exit-NORTH":
                    GlobalData.currentRoomLocation = GlobalData.currentRoomLocation + new Vector2(0, -1);
                    break;
                case "Exit-EAST":
                    GlobalData.currentRoomLocation = GlobalData.currentRoomLocation + new Vector2(1, 0);
                    break;
                case "Exit-SOUTH":
                    GlobalData.currentRoomLocation = GlobalData.currentRoomLocation + new Vector2(0, 1);
                    break;
                case "Exit-WEST":
                    GlobalData.currentRoomLocation = GlobalData.currentRoomLocation + new Vector2(-1, 0);
                    break;
            }
        }

        public void onTriggerExit(Collider other, Collider local)
        {
            // Debug.log("onTriggerExit: {0} entered {1}", other, local);
        }
    }
}
