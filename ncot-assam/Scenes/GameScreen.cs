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
        private Entity player;
        private Texture2D playerTexture;

        public GameScreen() : base()
        {
            
        }

        public void InitLevel()
        {
            this.destroyAllEntities();
            var room = createEntity("room-entity");
            room.addComponent(new RoomRenderer());

            player = createEntity("player", new Vector2(Screen.width / 2, Screen.height / 2), playerTexture);
        }

        public override void initialize()
        {
            base.initialize();
            setDesignResolution(1024, 768, SceneResolutionPolicy.None);
            Screen.setSize(1024, 768);

            clearColor = Color.DarkGray;
            addRenderer(new DefaultRenderer());

            playerTexture = content.Load<Texture2D>("atariBee");
            Physics.gravity = Vector2.Zero;
            Input.gamePads[0].isLeftStickVertcialInverted = true;

            InitLevel();
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
