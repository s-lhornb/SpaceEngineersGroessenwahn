#if DEBUG
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using VRageMath;
using VRage.Game;
using VRage.Collections;
using Sandbox.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using Sandbox.Game.EntityComponents;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;

namespace Broker
{
    public sealed class Program : MyGridProgram
    {
#endif
        IMyProgrammableBlock log;
        List<String> subs = new List<String>();

        public Program()
        {
            log = GridTerminalSystem.GetBlockWithName("log") as IMyProgrammableBlock;
            if (Me.CustomData.Length > 0)
            {
                subs = Storage.Split(',').ToList<String>();
            }
            log.TryRun("Loaded broker with " + subs.Count() + " subs (" + String.Join(",", subs) + ")");
        }
        
        public void Main(string e)
        {
            if(e.StartsWith("sub:"))
            {
                String sub = e.Split(':')[1];
                log.TryRun("New sub " + sub);
                subs.Add(sub);
            } else
            {
                log.TryRun("New Event " + e);
                handleEvent(e);
            }
        }

        public void handleEvent(String inboundEvent)
        {
            List<String> outboundEvents = new List<String>();
            switch(inboundEvent)
            {
                case "TODO":
                    break;
                default:
                    outboundEvents.Add(inboundEvent);
                    break;
            }
            foreach (String outboundEvent in outboundEvents) {
                foreach (String s in subs)
                {
                    IMyTerminalBlock block = GridTerminalSystem.GetBlockWithName(s);
                    if (block is IMyProgrammableBlock)
                    {
                        if ((block as IMyProgrammableBlock).TryRun(outboundEvent))
                        {
                            log.TryRun("Sent event " + outboundEvent + " to " + s);
                        } else
                        {
                            log.TryRun("Couldn't send event " + outboundEvent + " to " + s);
                        }
                    }
                }
            }
        }

        public void Save()
        {
            Me.CustomData = String.Join(",", subs);
        }
#if DEBUG
    }
}
#endif