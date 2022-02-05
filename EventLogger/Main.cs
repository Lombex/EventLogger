using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Logging;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Configuration;

namespace MainSpace
{
    public class Main : MelonMod
    {
        #pragma warning disable CS0108, CS0612
        public static readonly HarmonyLib.Harmony Harmony = new HarmonyLib.Harmony("EventLogger"); 
        public override void OnApplicationStart()
        {
            Directory.CreateDirectory("./Mods/EventLogger/Recieved");
            if (File.Exists("./Mods/EventLogger/Config.json")) Config.Instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText("./Mods/EventLogger/Config.json"));
            RecievedEvents.PhotonRecieverPatch(Harmony);           
            LoggerInstance.Msg("EventLogger successfully loaded!");
        }

        public override void OnApplicationQuit()
        {
            Config.SaveSettings();

            string USpeakData = JsonConvert.SerializeObject(RecievedEvents.USpeakData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/USpeak.json", USpeakData);

            string PastEventData = JsonConvert.SerializeObject(RecievedEvents.PastEvent, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/PastEvent.json", PastEventData);

            string SyncEventsData = JsonConvert.SerializeObject(RecievedEvents.SyncEventsData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/SyncEvent.json", SyncEventsData);

            string SyncFinishedData = JsonConvert.SerializeObject(RecievedEvents.SyncFinished, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/SyncFinished.json", SyncFinishedData);

            string ProcessEventData = JsonConvert.SerializeObject(RecievedEvents.ProcessEventData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/ProcessEvent.json", ProcessEventData);

            string UnreliableSerializationData = JsonConvert.SerializeObject(RecievedEvents.UnreliableSerializationData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/UnreliableSerialization.json", UnreliableSerializationData);

            string InterestManagementData = JsonConvert.SerializeObject(RecievedEvents.InterestManagementData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/InterestManagement.json", InterestManagementData);

            string ReliableSerializationData = JsonConvert.SerializeObject(RecievedEvents.ReliableSerializationData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/ReliableSerialization.json", ReliableSerializationData);

            string ExecutiveActionData = JsonConvert.SerializeObject(RecievedEvents.ExecutiveActionData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/ExecutiveAction.json", ExecutiveActionData);

            string RatelimitValueSyncData = JsonConvert.SerializeObject(RecievedEvents.RatelimitValueSyncData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/RatelimitValueSync.json", RatelimitValueSyncData);

            string InstantiateData = JsonConvert.SerializeObject(RecievedEvents.InstantiateData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/Instantiate.json", InstantiateData);

            string OwnershipTransferData = JsonConvert.SerializeObject(RecievedEvents.OwnershipTransferData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/OwnershipTransfer.json", OwnershipTransferData);

            string PhotonAuthEventData = JsonConvert.SerializeObject(RecievedEvents.PhotonAuthEventData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/PhotonAuthEvent.json", PhotonAuthEventData);

            string StatsData = JsonConvert.SerializeObject(RecievedEvents.StatsData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/Stats.json", StatsData);

            string PropertiesChangedData = JsonConvert.SerializeObject(RecievedEvents.PropertiesChangedData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/PropertiesChanged.json", PropertiesChangedData);

            string LeaveData = JsonConvert.SerializeObject(RecievedEvents.LeaveData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/Leave.json", LeaveData);

            string JoinData = JsonConvert.SerializeObject(RecievedEvents.JoinData, Formatting.Indented);
            File.WriteAllText("./Mods/EventLogger/Recieved/Join.json", JoinData);

        }
    }
}
