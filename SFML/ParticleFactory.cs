using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Entities;

namespace RPG
{
    public static class ParticleFactory
    {
        private static List<SpellParticle> Particles = new List<SpellParticle>();


        public static void AddParticle(SpellParticle newParticle)
        {
            Particles.Add(newParticle);
            Console.WriteLine("Particles: " + Particles.Count);
        }

        public static SpellParticle[] GetParticles()
        {
            return Particles.ToArray();
        }

        public static SpellParticle GetParticle(int index)
        {
            return Particles.ElementAt(index);
        }

        public static void RemoveParticle(SpellParticle particle)
        {
            Particles.Remove(particle);
            Console.WriteLine("Particles: " + Particles.Count);
        }

    }
}
