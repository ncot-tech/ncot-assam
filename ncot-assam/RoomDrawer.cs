
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoomGen
{
    class RoomDrawer
    {
        SpriteFont font;
        SpriteBatch spriteBatch;
        RoomManager roomManager;
        delegate int genCoord(int xy);
        delegate int genCoord2(int xy, int off);
        genCoord makeGridCoord;
        genCoord2 makeDoorCoord;
        Texture2D whiteRectangle;

        public RoomDrawer(SpriteBatch sb, RoomManager rm, SpriteFont fnt, GraphicsDevice gd)
        {
            spriteBatch = sb;
            roomManager = rm;
            font = fnt;

            makeGridCoord = xy => 32 + (xy * (64 + 2));

            makeDoorCoord = (xy, off) => off + (xy * (64 + 2));

            whiteRectangle = new Texture2D(gd, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
        }

        //private void DrawGrid()
        //{
        //    spriteBatch.Begin();
        //    for (int y = 0; y < 10; y ++)
        //    {
        //        for (int x = 0; x < 10; x++)
        //        {
        //            if (roomManager.rooms[y, x] != null)
        //            {
        //                // Draw a rect to represent the room
        //                spriteBatch.Draw(whiteRectangle, new Rectangle(makeGridCoord(x), makeGridCoord(y), 64, 64), Color.White);
        //                // Draw a little rect to show a link for each exit

        //                if (roomManager.rooms[y, x].CheckValidExit(0) != -1)
        //                {
        //                    spriteBatch.Draw(whiteRectangle, new Rectangle(makeDoorCoord(x, 32 + 16), makeDoorCoord(y, 32 - 2), 32, 2), Color.White);
        //                }

        //                if (roomManager.rooms[y, x].CheckValidExit(1) != -1)
        //                {
        //                    spriteBatch.Draw(whiteRectangle, new Rectangle(makeDoorCoord(x, 32 + 64), makeDoorCoord(y, 32 + 16), 2, 32), Color.White);
        //                }

        //                if (roomManager.rooms[y, x].CheckValidExit(2) != -1)
        //                {
        //                    spriteBatch.Draw(whiteRectangle, new Rectangle(makeDoorCoord(x, 32 + 16), makeDoorCoord(y, 32 + 64), 32, 2), Color.White);
        //                }

        //                if (roomManager.rooms[y, x].CheckValidExit(3) != -1)
        //                {
        //                    spriteBatch.Draw(whiteRectangle, new Rectangle(makeDoorCoord(x, 32 - 2), makeDoorCoord(y, 32 + 16), 2, 32), Color.White);
        //                }
        //            }
        //        }
        //    }
        //    spriteBatch.End();          
        //}

        public void DrawRoom(int vpWidth, int vpHeight)
        {
            spriteBatch.Begin();
            // Write the room's co-ords on the middle
            string exitLabel = "(" + roomManager.currentRoomCoords.ToString() + ")";
            Vector2 FontOrigin = font.MeasureString(exitLabel) / 2;
            Vector2 FontPos = new Vector2(vpWidth / 2, vpHeight / 2);
            spriteBatch.DrawString(font, exitLabel, FontPos, Color.Yellow, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            bool[] roomExits = roomManager.GetCurrentExits();
            if (roomExits[0] == true)    // N
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, 0, (vpWidth / 2) - 128, 4), Color.White);
                spriteBatch.Draw(whiteRectangle, new Rectangle((vpWidth / 2) + 128, 0, (vpWidth / 2) - 128, 4), Color.White);
            }
            else
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, 0, vpWidth, 4), Color.White);
            }

            if (roomExits[1] == true)  // E
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(vpWidth - 4, 0, 4, (vpHeight / 2) + 128), Color.White);
                spriteBatch.Draw(whiteRectangle, new Rectangle(vpWidth - 4, (vpHeight - 128), 4, (vpHeight / 2) + 128), Color.White);
            }
            else
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(vpWidth - 4, 0, 4, vpHeight), Color.White);
            }

            if (roomExits[2] == true)  // S
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, vpHeight - 4, (vpWidth / 2) - 128, 4), Color.White);
                spriteBatch.Draw(whiteRectangle, new Rectangle((vpWidth / 2) + 128, vpHeight - 4, (vpWidth / 2) - 128, 4), Color.White);
            }
            else
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, vpHeight - 4, vpWidth, 4), Color.White);
            }

            if (roomExits[3] == true)  // W
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, 0, 4, (vpHeight / 2) + 128), Color.White);
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, (vpHeight - 128), 4, (vpHeight / 2) + 128), Color.White);
            }
            else
            {
                spriteBatch.Draw(whiteRectangle, new Rectangle(0, 0, 4, vpHeight), Color.White);
            }

            spriteBatch.End();
        }

        public bool SwitchRoom()
        {
            bool roomChangeSuccess = false;
            bool[] roomExits = roomManager.GetCurrentExits();

            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                if (roomExits[0] == true)
                {
                    roomChangeSuccess = roomManager.ExitRoomToThe(Exit.NORTH);
                }
                else
                {
                    roomChangeSuccess = false;
                }
            }
            else
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                if (roomExits[1] == true)
                {
                    roomChangeSuccess = roomManager.ExitRoomToThe(Exit.EAST);
                }
                else
                {
                    roomChangeSuccess = false;
                }
            }
            else
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
            {
                if (roomExits[2] == true)
                {
                    roomChangeSuccess = roomManager.ExitRoomToThe(Exit.SOUTH);
                }
                else
                {
                    roomChangeSuccess = false;
                }
            }
            else
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                if (roomExits[3] == true)
                {
                    roomChangeSuccess = roomManager.ExitRoomToThe(Exit.WEST);
                }
                else
                {
                    roomChangeSuccess = false;
                }
            }

            return roomChangeSuccess;
        }
    }
}
