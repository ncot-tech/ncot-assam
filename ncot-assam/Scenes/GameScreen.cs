using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ncot_assam;
using Nez;
using Nez.Sprites;
using RoomGen;

namespace Scenes
{
    class GameScreen: Scene
    {
        RoomManager roomManager;
        SpriteFont font;

        public GameScreen() : base()
        {
            
        }

        public override void initialize()
        {
            var friction = 1.0f;
            var elasticity = 0.0f;

            base.initialize();
            setDesignResolution(1024, 768, SceneResolutionPolicy.None);
            Screen.setSize(1024, 768);

            clearColor = Color.DarkGray;
            addRenderer(new DefaultRenderer());

            roomManager = new RoomManager();
            roomManager.Generate();

            font = content.Load<SpriteFont>("DebugFont");
            var beeTexture = content.Load<Texture2D>("atariBee");
            Physics.gravity = Vector2.Zero;
            Input.gamePads[0].isLeftStickVertcialInverted = true;

            var entityOne = createEntity(new Vector2(200, 200), 15f, friction, elasticity, Vector2.Zero, beeTexture);
            var roomEntity = createEntity("room-manager-entity");
            roomEntity.addComponent(new RoomRenderer(roomManager, font));
            roomEntity.addComponent(new ExitHitDetector());
        }

        ArcadeRigidbody createEntity(Vector2 position, float mass, float friction, float elasticity, Vector2 velocity, Texture2D texture)
        {
            var rigidbody = new ArcadeRigidbody()
                .setMass(mass)
                .setFriction(friction)
                .setElasticity(elasticity)
                .setVelocity(velocity);

            var entity = createEntity(Utils.randomString(3));
            entity.transform.position = position;
            entity.addComponent(new Sprite(texture));
            entity.addComponent(rigidbody);
            entity.addComponent(new ImpulseMover());
            entity.addCollider(new CircleCollider());
            entity.addComponent(new ExitHitDetector());
            return rigidbody;
        }

        //public bool SwitchRoom()
        //{
        //    bool roomChangeSuccess = false;
        //    bool[] roomExits = roomManager.GetCurrentExits();

        //    if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
        //    {
        //        if (roomExits[0] == true)
        //        {
        //            roomChangeSuccess = roomManager.ExitRoomToThe(Exit.NORTH);
        //        }
        //        else
        //        {
        //            roomChangeSuccess = false;
        //        }
        //    }
        //    else
        //    if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
        //    {
        //        if (roomExits[1] == true)
        //        {
        //            roomChangeSuccess = roomManager.ExitRoomToThe(Exit.EAST);
        //        }
        //        else
        //        {
        //            roomChangeSuccess = false;
        //        }
        //    }
        //    else
        //    if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
        //    {
        //        if (roomExits[2] == true)
        //        {
        //            roomChangeSuccess = roomManager.ExitRoomToThe(Exit.SOUTH);
        //        }
        //        else
        //        {
        //            roomChangeSuccess = false;
        //        }
        //    }
        //    else
        //    if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
        //    {
        //        if (roomExits[3] == true)
        //        {
        //            roomChangeSuccess = roomManager.ExitRoomToThe(Exit.WEST);
        //        }
        //        else
        //        {
        //            roomChangeSuccess = false;
        //        }
        //    }

        //    return roomChangeSuccess;
        //}
    }
}
