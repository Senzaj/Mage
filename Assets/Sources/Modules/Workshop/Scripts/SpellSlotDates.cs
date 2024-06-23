using System;
using System.Collections.Generic;
using Sources.Modules.Weapons.Scripts;

namespace Sources.Modules.Workshop.Scripts
{
    [Serializable]
    public class SpellSlotDates
    {
        public List<SpellType> SlotDates;
        public List<SpellType> ActiveSpells;
    }
}