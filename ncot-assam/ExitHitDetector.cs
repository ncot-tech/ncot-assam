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

            /// Todo: This is probably a bad thing to do!
            RoomRenderer roomRenderer = other.entity.scene.findEntity("room-manager-entity").getComponent<RoomRenderer>();
            
            switch (other.entity.name)
            {
                case "exit-0":
                    roomRenderer.SwitchRoom(Exit.NORTH);
                    //local.entity.transform.setPosition(new Vector2(100, 100));
                    break;
                case "exit-1":
                    roomRenderer.SwitchRoom(Exit.EAST);
                    //local.entity.transform.setPosition(new Vector2(100, 100));
                    break;
                case "exit-2":
                    roomRenderer.SwitchRoom(Exit.SOUTH);
                    //local.entity.transform.setPosition(new Vector2(100, 100));
                    break;
                case "exit-3":
                    roomRenderer.SwitchRoom(Exit.WEST);
                    //local.entity.transform.setPosition(new Vector2(100, 100));
                    break;
            }
        }

        public void onTriggerExit(Collider other, Collider local)
        {
            // Debug.log("onTriggerExit: {0} entered {1}", other, local);
        }
    }
}
