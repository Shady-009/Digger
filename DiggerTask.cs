using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }
        public int GetDrawingPriority()
        {
            return 0;
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                return true;
            }
            return false;
        }
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }
        public int GetDrawingPriority()
        {
            return 1;
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
            {
                Game.Scores += 10;
            }
            if (conflictedObject is Sack)
            {
                return true;
            }
            return false;
        }
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand movement = new CreatureCommand();
            var key = Game.KeyPressed;

            if (key == Keys.Right && x + 1 < Game.MapWidth)
            {
                movement.DeltaX = 1;
            }
            else if (key == Keys.Left && x - 1 >= 0)
            {
                movement.DeltaX = -1;
            }
            else if (key == Keys.Up && y - 1 >= 0)
            {
                movement.DeltaY = -1;
            }
            else if (key == Keys.Down && y + 1 < Game.MapHeight)
            {
                movement.DeltaY = 1;
            }

            if (Game.Map[x + movement.DeltaX, y + movement.DeltaY] is Sack)
            {
                movement.DeltaX = movement.DeltaY = 0;
            }

            return movement;
        }

    }
    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }
        public int GetDrawingPriority()
        {
            return 2;
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                return true;
            }
            
            return false;
        }
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

    }
    public class Sack : ICreature
    {
        public string GetImageFileName()
        {
            return "Sack.png";
        }
        public int GetDrawingPriority()
        {
            return 3;
        }
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
        public CreatureCommand Act(int x, int y)
        {

            CreatureCommand fallingSack = new CreatureCommand();

            if (x + 1 <= Game.MapWidth && y + 1 < Game.MapHeight)
            {
                fallingSack.DeltaY += 1;
            }
            if (Game.Map[x + fallingSack.DeltaX, y + fallingSack.DeltaY] is Terrain ||
                Game.Map[x + fallingSack.DeltaX, y + fallingSack.DeltaY] is Gold ||
                Game.Map[x + fallingSack.DeltaX, y + fallingSack.DeltaY] is Sack ||
                Game.Map[x + fallingSack.DeltaX, y + fallingSack.DeltaY] is Player)
            {
                fallingSack.DeltaX = fallingSack.DeltaY = 0;
            }
            return fallingSack;
        }
    }
   
}
