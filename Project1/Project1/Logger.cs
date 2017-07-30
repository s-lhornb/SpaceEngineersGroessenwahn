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

namespace Logger
{
    public sealed class Program : MyGridProgram
    {
#endif
        //=======================================================================
        //////////////////////////BEGIN//////////////////////////////////////////
        //=======================================================================

        IMyTextPanel display;

        public Program()
        {
            display = GridTerminalSystem.GetBlockWithName("logPanel") as IMyTextPanel;
            display.WritePublicTitle("Log");
            display.WritePublicText(Storage);
            this.Log("Log started");
        }

        private void Log(string msg)
        {
            display.WritePublicText(DateTime.Now.ToString("h:mm:ss") + " " + msg + "\r\n", true);
        }

        public void Main(string msg)
        {
            this.Log(msg);
        }

        public void Save()
        {
            Storage = display.GetPublicText();
        }

        //=======================================================================
        //////////////////////////END////////////////////////////////////////////
        //=======================================================================
#if DEBUG
    }
}
#endif