using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            base.initialize();
            setDesignResolution(1024, 768, SceneResolutionPolicy.None);
            Screen.setSize(1024, 768);

            clearColor = Color.DarkGray;
            addRenderer(new DefaultRenderer());
            var e0 = createEntity("exit-0");
            var e1 = createEntity("exit-1");
            var e2 = createEntity("exit-2");
            var e3 = createEntity("exit-3");

            roomManager = new RoomManager();
            roomManager.Generate();

            font = content.Load<SpriteFont>("DebugFont");
            var beeTexture = content.Load<Texture2D>("atariBee");
            Physics.gravity = Vector2.Zero;
            Input.gamePads[0].isLeftStickVertcialInverted = true;

            var player = createEntity("player", new Vector2(200, 200), beeTexture);
            var roomEntity = createEntity("room-manager-entity");
            roomEntity.addComponent(new RoomRenderer(roomManager, font));
            

        }

        Entity createEntity(string name, Vector2 position, Texture2D texture)
        {
            var entity = createEntity(name);
            entity.transform.position = position;
            entity.addComponent(new Sprite(texture));
            entity.addComponent(new SimpleMover());
            entity.addCollider(new CircleCollider());
            entity.addComponent(new ExitHitDetector());
            return entity;
        }


        
    }
}
