using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game;
using Sandbox.Game.GameSystems.Conveyors;
using Sandbox.Game.Entities;
using Sandbox.Game.EntityComponents;
using Sandbox.Definitions;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;

using VRage;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;
using VRageMath;
using VRage.Game.Entity;
using VRage.Voxels;

namespace UnstableBlocks
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_CargoContainer), true, "AdminAmmoGenerator")]
    public class AmmoGen : MyGameLogicComponent
    {
        // Builder is nessassary for GetObjectBuilder method as far as I know.
        private MyObjectBuilder_EntityBase builder;
        private Sandbox.ModAPI.IMyCargoContainer m_generator;
        Sandbox.ModAPI.IMyTerminalBlock terminalBlock;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            m_generator = Entity as Sandbox.ModAPI.IMyCargoContainer;
            builder = objectBuilder;

            Entity.NeedsUpdate |= MyEntityUpdateEnum.EACH_FRAME | MyEntityUpdateEnum.EACH_100TH_FRAME;

            terminalBlock = Entity as Sandbox.ModAPI.IMyTerminalBlock;
        }
        public override void UpdateBeforeSimulation100()
        {
            base.UpdateBeforeSimulation100();
            
            if (m_generator.IsWorking)
            {
                IMyInventory inventory = ((Sandbox.ModAPI.IMyTerminalBlock)Entity).GetInventory(0) as IMyInventory;
                int num = new Random().Next(0,1000);
                VRage.MyFixedPoint amount = (VRage.MyFixedPoint)(1);
                if(num < 6) {
                    inventory.AddItems(amount, new MyObjectBuilder_AmmoMagazine() { SubtypeName = "Missile200mm" });
                    terminalBlock.RefreshCustomInfo();
                } else if(num < 21) {
                    inventory.AddItems(amount, new MyObjectBuilder_AmmoMagazine() { SubtypeName = "NATO_25x184mm" });
                    terminalBlock.RefreshCustomInfo();
                } else if(num < 40) {
                    inventory.AddItems(amount, new MyObjectBuilder_AmmoMagazine() { SubtypeName = "NATO_5p56x45mm" });
                    terminalBlock.RefreshCustomInfo();
                }
                if(new Random().Next(0,250) < 1) {
                    amount = (VRage.MyFixedPoint) 0.01;
                    inventory.AddItems(amount, new MyObjectBuilder_Ingot() { SubtypeName = "UnstableMatter" });
                    terminalBlock.RefreshCustomInfo();
                }
            }
        }
        public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false)
        {
            return builder;
        }

    }
}