using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace RPG.Entities
{
    public class SpellParticle : Sprite
    {
        private Vector2f _Destination;
        private float _SpeedModifier = 0.2f;
        private Enumerations.ParticleType _ParticleType;

        private float ParticleAliveTime = 0f;

        public SpellParticle(Texture texture, Vector2f position, Vector2f destination, Enumerations.ParticleType particleType) : base(texture)
        {
            this.Texture = texture;
            this.Position = position;
            this._Destination = destination;
            this._ParticleType = particleType;

            ParticleFactory.AddParticle(this);  
        }

        /// <summary>
        /// Updates the physical location of the particle, called on every redraw.
        /// </summary>
        public void Update(float elapsedTime)
        {
            //TODO iterate spell texture on each call to make it look animated.

            switch (_ParticleType)
            {
                case Enumerations.ParticleType.Homing:
                    UpdateHoming(elapsedTime);
                    break;
                case Enumerations.ParticleType.Charged:
                    UpdateCharged();
                    break;
                case Enumerations.ParticleType.Instant:
                    UpdateInstant(elapsedTime);
                    break;
            }
        }
        
        private void UpdateHoming(float elapsedTime)
        {
            elapsedTime *= this._SpeedModifier;
           
             Vector2f direction = new Vector2f(_Destination.X - this.Position.X, _Destination.Y - this.Position.Y);
             this.Rotation = RPG.Globals.Funcs.GetRotation(this.Position, this._Destination);
            
             this.Position += direction * _SpeedModifier; // direction * elapsedTime

            if (InDisposeRange(direction))
            {
                ParticleFactory.RemoveParticle(this);
            }
        }

        private void UpdateCharged()
        {

        }

        private void UpdateInstant(float elapsedTime)
        {
            this.ParticleAliveTime += elapsedTime;
            this.Position = _Destination;

            if (this.ParticleAliveTime > 3f)
            {
                ParticleFactory.RemoveParticle(this);
            }
        }

        private bool InDisposeRange(Vector2f distance)
        {
            if (distance.X < -3 || distance.X > 3)
                return false;

            return true;
        }
    }
}
