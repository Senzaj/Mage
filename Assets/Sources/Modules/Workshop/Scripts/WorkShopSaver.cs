using System;
using System.Collections.Generic;
using Sources.Modules.Common;
using Sources.Modules.Weapons.Scripts;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts
{
    public class WorkShopSaver : Saver<SpellSlotDates>
    {
        public event Action<SpellSlotDates> RequestSave;
        public static WorkShopSaver Instance { get; private set; }
        
        public override void Init(SpellSlotDates dates)
        {
            if (Instance == null)
            {
                Instance = this;
                base.Init(dates);
            }
        }

        public override SpellSlotDates GetData()
        {
#if UNITY_EDITOR
            return null;
#endif
            MyData.ActiveSpells ??= new List<SpellType>();
            MyData.SlotDates ??= new List<SpellType>();

            return new SpellSlotDates
            {
                ActiveSpells = new List<SpellType>(MyData.ActiveSpells),
                SlotDates = new List<SpellType>(MyData.SlotDates)
            };
        }
        
        public override void SaveData(SpellSlotDates data)
        {
#if UNITY_EDITOR
            return;
#endif
            
            MyData = new SpellSlotDates()
            {
                ActiveSpells = new List<SpellType>(data.ActiveSpells),
                SlotDates = new List<SpellType>(data.SlotDates)
            };

            RequestSave?.Invoke(MyData);
        }
    }
}