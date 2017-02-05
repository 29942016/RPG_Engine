using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace RPG.Interfaces
{
    public interface ITargetable
    {
        string GetName();
        float GetHealth();

        bool IsAttackable(ITargetable source);
        bool IsFriendly(ITargetable source);

        FloatRect GetBoundryBox();
        Vector2f GetPosition();
        void ModifyHealth(int amount);
        Vector2f GetCenter();
    }
}
