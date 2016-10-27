using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace ncot_assam
{
    public class SimpleMover : Mover, IUpdatable
    {
        public float speed = 100f;
        VirtualJoystick _leftStick;
        float Angle;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            SetupInput();
        }

        public override void onRemovedFromEntity()
        {
            base.onRemovedFromEntity();
            _leftStick.deregister();
        }

        void SetupInput()
        {
            _leftStick = new VirtualJoystick(true);
            _leftStick.nodes.Add(new VirtualJoystick.PadLeftStick());
            _leftStick.nodes.Add(new VirtualJoystick.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right, Keys.Up, Keys.Down));
        }

        public void update()
        {
            var moveDir = Vector2.Zero;
            CollisionResult collResult;

            if (_leftStick.value.Length() > 0.1)
            {
                Angle = (float)Math.Atan2(_leftStick.value.Y, _leftStick.value.X);
                entity.transform.rotation = Angle;
                move(_leftStick.value * 5, out collResult);
            }
        }
    }
}
