using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;

namespace RoomGen
{
    class RoomRenderer : RenderableComponent
    {
        public override float width { get { return _width; } }
        public override float height { get { return _height; } }

        delegate int genCoord(int xy);
        delegate int genCoord2(int xy, int off);
        genCoord makeGridCoord;
        genCoord2 makeDoorCoord;
        RoomManager roomManager;
        NezSpriteFont font;

        float _width;
        float _height;
        int colliderCounter;
        int exitColliderCounter;
        Collider[] _colliders;
        Collider[] _exitColliders;

        public RoomRenderer(RoomManager rm, SpriteFont fnt)
        {
            roomManager = rm;
            font = new NezSpriteFont(fnt);
            makeGridCoord = xy => 32 + (xy * (64 + 2));
            makeDoorCoord = (xy, off) => off + (xy * (64 + 2));
            _width = Core.graphicsDevice.Viewport.Width;
            _height = Core.graphicsDevice.Viewport.Height;
            colliderCounter = 0;
            exitColliderCounter = 0;
        }

        private void addCollider(float x, float y, float w, float h)
        {
            var collider = new BoxCollider(x,y, w, h);
            collider.entity = entity;
            _colliders[colliderCounter] = collider;
            Physics.addCollider(collider);
            colliderCounter++;
        }

        private void addExitCollider(int id, float x, float y, float w, float h)
        {
            var collider = new BoxCollider(x, y, w, h);
            collider.entity = entity;
            _exitColliders[exitColliderCounter] = collider;
            collider.isTrigger = true;
            //Flags.setFlag(ref collider.physicsLayer, id);
            //Physics.addCollider(collider);
            entity.addCollider(collider);
            exitColliderCounter++;
        }

        private void createColliders(bool[] roomExits)
        {
            _colliders = new Collider[roomExits.Length + 4];
            _exitColliders = new Collider[roomExits.Length];

            if (roomExits[0] == true)    // N
            {
                addCollider(0, 0, (_width / 2) - 128, 4);
                addCollider((_width / 2) + 128, 0, (_width / 2) - 128, 4);
                addExitCollider(0, (_width / 2) - 128, 0, 128, 4);
            }
            else
            {
                addCollider(0, 0, _width, 4);
            }

            if (roomExits[1] == true)    // E
            {
                addCollider(_width - 4, 0, 4, (_height / 2) - 128);
                addCollider(_width - 4, (_height /2 ) + 128, 4, (_height / 2) - 128);
                addExitCollider(1, _width - 4, (_height / 2) - 128, 4, 256);
            }
            else
            {
                addCollider(_width - 4, 0, 4, _height);
            }

            if (roomExits[2] == true)    // S
            {
                addCollider(0, _height - 4, (_width / 2) - 128, 4);
                addCollider((_width / 2) + 128, _height - 4, (_width / 2) - 128, 4);
                addExitCollider(2, (_width / 2) - 128, _height - 4, 256, 4);
            }
            else
            {
                addCollider(0, _height - 4, _width, 4);
            }

            if (roomExits[3] == true)    // W
            {
                addCollider(0, 0, 4, (_height / 2) - 128);
                addCollider(0, (_height / 2) + 128, 4, (_height / 2) - 128);
                addExitCollider(3, 0, (_height / 2) - 128, 4, 256);
            }
            else
            {
                addCollider(0, 0, 4, _height);
            }
        }

        private void clearColliders()
        {
            if (_colliders == null)
                return;

            foreach (var collider in _colliders)
                Physics.removeCollider(collider);
            _colliders = null;

            foreach (var collider in _exitColliders)
                Physics.removeCollider(collider);
            _exitColliders = null;

            colliderCounter = 0;
            exitColliderCounter = 0;
        }

        public override void render(Graphics graphics, Camera camera)
        {
            string exitLabel = "(" + roomManager.currentRoomCoords.ToString() + ")";
            Vector2 FontOrigin = font.measureString(exitLabel) / 2;
            Vector2 FontPos = new Vector2(_width / 2, _height / 2);
            graphics.batcher.drawString(font, exitLabel, FontPos, Color.Yellow, 0, FontOrigin, 1.0f, SpriteEffects.None, 1);

            bool[] roomExits = roomManager.GetCurrentExits();
            if (_colliders == null)
            {
                createColliders(roomExits);
                colliderCounter = 0;
                exitColliderCounter = 0;
            }

            if (roomExits[0] == true)    // N
            {
                graphics.batcher.drawRect(0, 0, (_width / 2) - 128, 4, Color.White);
                graphics.batcher.drawRect((_width / 2) + 128, 0, (_width / 2) - 128, 4, Color.White);
            }
            else
            {
                graphics.batcher.drawRect(0, 0, _width, 4, Color.White);
            }

            if (roomExits[1] == true)  // E
            {
                graphics.batcher.drawRect(_width - 4, 0, 4, (_height / 2) - 128, Color.White);
                graphics.batcher.drawRect(_width - 4, (_height / 2) + 128, 4, (_height / 2) - 128, Color.White);
            }
            else
            {
                graphics.batcher.drawRect(_width - 4, 0, 4, _height, Color.White);
            }

            if (roomExits[2] == true)  // S
            {
                graphics.batcher.drawRect(0, _height - 4, (_width / 2) - 128, 4, Color.White);
                graphics.batcher.drawRect((_width / 2) + 128, _height - 4, (_width / 2) - 128, 4, Color.White);
            }
            else
            {
                graphics.batcher.drawRect(0, _height - 4, _width, 4, Color.White);
            }

            if (roomExits[3] == true)  // W
            {
                graphics.batcher.drawRect(0, 0, 4, (_height / 2) - 128, Color.White);
                graphics.batcher.drawRect(0, (_height / 2) + 128, 4, (_height / 2) - 128, Color.White);
            }
            else
            {
                graphics.batcher.drawRect(0, 0, 4, _height, Color.White);
            }
        }

        public bool SwitchRoom(Exit exit)
        {
            return roomManager.ExitRoomToThe(exit);
        }
    }
}
