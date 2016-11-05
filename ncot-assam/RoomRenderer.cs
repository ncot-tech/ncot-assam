using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ncot_assam;
using Nez;
using Nez.Sprites;
using System;

namespace RoomGen
{
    class RoomRenderer : RenderableComponent
    {
        public override float width { get { return _width; } }
        public override float height { get { return _height; } }

        //delegate int genCoord(int xy);
        //delegate int genCoord2(int xy, int off);
        //genCoord makeGridCoord;
        //genCoord2 makeDoorCoord;
        NezSpriteFont font;

        Texture2D wallTexture;
        Texture2D exitTexture;

        //bool _changeRooms;
        float _width;
        float _height;
        //int colliderCounter;
        //int exitColliderCounter;
        //Collider[] _colliders;
        //Collider[] _exitColliders;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            font = new NezSpriteFont(entity.scene.content.Load<SpriteFont>("DebugFont"));
            //makeGridCoord = xy => 32 + (xy * (64 + 2));
            //makeDoorCoord = (xy, off) => off + (xy * (64 + 2));
            _width = Core.graphicsDevice.Viewport.Width;
            _height = Core.graphicsDevice.Viewport.Height;

            wallTexture = new Texture2D(Core.graphicsDevice, 1, 1);
            wallTexture.SetData(new[] { Color.Red });
            exitTexture = new Texture2D(Core.graphicsDevice, 1, 1);
            exitTexture.SetData(new[] { Color.Green });

            CreateRoom();
        }

        public RoomRenderer()
        {
            
        }

        private void CreateWall(Exit id, float x, float y, float w, float h)
        {
            var wall = entity.scene.createEntity("Wall-" + id.ToString());
            wall.transform.setPosition(x + (w/2), y +(h/2));
            wall.transform.setScale(new Vector2(w, h));
            wall.addComponent(new Sprite(wallTexture));
            wall.addCollider(new BoxCollider());
        }

        private void CreateExit(Exit id, float x, float y, float w, float h)
        {
            var collider = new BoxCollider();
            collider.isTrigger = true;
            var wall = entity.scene.createEntity("Exit-" + id.ToString());
            wall.transform.setPosition(x + (w / 2), y + (h / 2));
            wall.transform.setScale(new Vector2(w, h));
            wall.addComponent(new Sprite(exitTexture));
            wall.addCollider(collider);
        }

        public void CreateRoom()
        {
            if (GlobalData.currentRoom.CheckValidExit(Exit.NORTH) == true)    // N
            {
                CreateWall(Exit.NORTH, 0, 0, (_width / 2) - 128, 4);
                CreateWall(Exit.NORTH, (_width / 2) + 128, 0, (_width / 2) - 128, 4);
                CreateExit(Exit.NORTH, (_width / 2) - 128, 0, 256, 4);
                Debug.log("North Exit");
            }
            else
            {
                CreateWall(Exit.NORTH, 0, 0, _width, 4);
            }

            if (GlobalData.currentRoom.CheckValidExit(Exit.EAST) == true)  // E
            {
                CreateWall(Exit.EAST, _width - 4, 0, 4, (_height / 2) - 128);
                CreateWall(Exit.EAST, _width - 4, (_height / 2) + 128, 4, (_height / 2) - 128);
                CreateExit(Exit.EAST, _width - 4, (_height / 2) - 128, 4, 256);
                Debug.log("East Exit");
            }
            else
            {
                CreateWall(Exit.EAST, _width - 4, 0, 4, _height);
            }

            if (GlobalData.currentRoom.CheckValidExit(Exit.SOUTH) == true)  // S
            {
                CreateWall(Exit.SOUTH, 0, _height - 4, (_width / 2) - 128, 4);
                CreateWall(Exit.SOUTH, (_width / 2) + 128, _height - 4, (_width / 2) - 128, 4);
                CreateExit(Exit.SOUTH, (_width / 2) - 128, _height - 4, 256, 4);
                Debug.log("South Exit");
            }
            else
            {
                CreateWall(Exit.SOUTH, 0, _height - 4, _width, 4);
            }

            if (GlobalData.currentRoom.CheckValidExit(Exit.WEST) == true)  // W
            {
                CreateWall(Exit.WEST, 0, 0, 4, (_height / 2) - 128);
                CreateWall(Exit.WEST, 0, (_height / 2) + 128, 4, (_height / 2) - 128);
                CreateExit(Exit.WEST, 0, (_height / 2) - 128, 4, 256);
                Debug.log("West Exit");
            }
            else
            {
                CreateWall(Exit.WEST, 0, 0, 4, _height);
            }
        }

        public override void render(Graphics graphics, Camera camera)
        {
            string exitLabel = "(" + GlobalData.currentRoom.location.ToString() + ")";
            Vector2 FontOrigin = font.measureString(exitLabel) / 2;
            Vector2 FontPos = new Vector2(_width / 2, _height / 2);
            graphics.batcher.drawString(font, exitLabel, FontPos, Color.Yellow, 0, FontOrigin, 1.0f, SpriteEffects.None, 1);  
        }
    }
}
