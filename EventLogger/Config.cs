using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Configuration
{
    public class Config
    {
        private static Config _instance;
        public static Config Instance
        {
            get => _instance ?? (_instance = new Config());
            set { _instance = value; }
        }

        public bool Uspeak = false; // 1
        public bool PastEvent = false; // 3
        public bool SyncEvents = false; // 4
        public bool SyncFinished = false; // 5
        public bool ProcessEvent = false; // 6
        public bool UnreliableSerialization = false; // 7
        public bool InterestManagement = false; // 8
        public bool ReliableSerialization = false; // 9
        public bool ExecutiveAction = false; // 33
        public bool RatelimitValueSync = false; // 34
        public bool RatelimitUpdate = false; // 35
        public bool Instantiate = false; // 202
        public bool OwnershipRequest = false; // 209
        public bool OwnershipTransfer = false; // 210
        public bool PhotonAuthEvent = false; // 223
        public bool Stats = false; // 226
        public bool PropertiesChanged = false; // 253
        public bool Leave = false; // 254
        public bool Join = false; // 255

        public static void SaveSettings() => File.WriteAllText("./Mods/EventLogger/Config.json", JsonConvert.SerializeObject(_instance, Formatting.Indented));
    }
}
