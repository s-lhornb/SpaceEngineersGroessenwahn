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

namespace SoundAlarm
{
    public sealed class Program : MyGridProgram
    {
#endif
        IMyProgrammableBlock log;

        public Program()
        {
            log = GridTerminalSystem.GetBlockWithName("log") as IMyProgrammableBlock;
        }

        public void Main(string cmd)
        {
            List<IMySoundBlock> sbs = new List<IMySoundBlock>();
            GridTerminalSystem.GetBlocksOfType<IMySoundBlock>(sbs);
            String sound = null;
            switch (cmd)
            {
                case "alarm1":
                case "alarm2":
                    sound = "Alert 1";
                    break;
                case "enemydetected":
                    sound = "Enemy detected";
                    break;
                case "stopalarm":
                    foreach (IMySoundBlock sb in sbs)
                    {
                        //TODO set sound and volume to prev state
                        sb.Stop();
                        loadSbState(sb);
                    }
                    break;
                default:
                    //Don't do anything if the cmd/event is unknown
                    break;
            }
            if (sound != null)
            {
                log.TryRun("SbControl: Sounding " + sound + " for " + cmd);
                foreach (IMySoundBlock sb in sbs)
                {
                    saveSbState(sb);
                    sb.Enabled = true;
                    sb.LoopPeriod = 30 * 60;
                    sb.Range = 100;
                    sb.Volume = 100;
                    sb.SelectedSound = sound;
                    sb.Play();
                }
            }
        }

        public void Save()
        {

        }

        private void saveSbState(IMySoundBlock sb)
        {

        }

        private void loadSbState(IMySoundBlock sb)
        {

        }
#if DEBUG
    }
}
#endif