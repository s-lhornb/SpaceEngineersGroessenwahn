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
        String state = "calm";

        public Program()
        {
            log = GridTerminalSystem.GetBlockWithName("log") as IMyProgrammableBlock;
            if (Me.CustomData.Length > 0)
            {
                String[] p = Me.CustomData.Split('/');
                subs = p[0].Split(',').ToList<String>();
                state = p[1];
            }
            log.TryRun("Loaded broker with " + subs.Count() + " subs (" + String.Join(",", subs) + ")");
        }
        
        public void Main(string e)
        {
            log.TryRun("New Event " + e);
            String[] p = e.Split(':');
            if (p.Count() != 2) log.TryRun("Event formatting error");
            switch (p[0])
            {
                case "sub":
                    log.TryRun("New sub " + p[1]);
                    subs.Add(p[1]);
                    break;
                default:
                    handleEvent(p[0], p[1]);
                    break;
            }
        }

        public void handleEvent(String lvl, String msg)
        {
            state = "TODO"; //Process lvl and msg
            foreach (String s in subs)
            {
                IMyTerminalBlock block = GridTerminalSystem.GetBlockWithName(s);
                if (block is IMyProgrammableBlock)
                {
                    if((block as IMyProgrammableBlock).TryRun(state))
                    {
                        log.TryRun("Told " + s + " state " + state);
                    } else
                    {
                        log.TryRun("Failed telling " + s + " state " + state);
                    }
                }
            }
        }

        public void Save()
        {
            Me.CustomData = String.Join(",", subs) + "/" + state;
        }
#if DEBUG
    }
}
#endif