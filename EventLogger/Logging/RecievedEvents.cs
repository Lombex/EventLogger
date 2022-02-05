using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MelonLoader;
using Utils;
using Configuration;
using System.Reflection;
using System.Collections;
using UnityEngine;

namespace Logging
{
    public static class RecievedEvents
    {
        [Obsolete]
        public static void PhotonRecieverPatch(HarmonyLib.Harmony Instance)
        {
            try
            {
                Instance.Patch(typeof(Photon.Realtime.LoadBalancingClient).GetMethod(nameof(Photon.Realtime.LoadBalancingClient.OnEvent)), typeof(RecievedEvents).GetPatch(nameof(OnRecievedEvents)));
            } catch (Exception e)
            {
                MelonLogger.Warning("Failed to patch LoadBalancingClient OnEvent: " + e.Message);
            }

            try
            {
                Instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Boolean_Player_VrcEvent_VrcBroadcastType_0", BindingFlags.Public | BindingFlags.Instance), typeof(RecievedEvents).GetPatch(nameof(OnEventDispatcher)));
            } catch (Exception e)
            {
                MelonLogger.Warning("Failed to patch VRC_EventDispatcherRFC: " + e.Message);
            }
        }

        public static Dictionary<(string, string), (List<string>, List<string>)> USpeakData = new Dictionary<(string, string), (List<string>, List<string>)>();
        public static List<(string, string)> PastEvent = new List<(string, string)>();
        public static Dictionary<(string, string), (List<string[]>, List<string>)> SyncEventsData = new Dictionary<(string, string), (List<string[]>, List<string>)>();
        public static List<(string, string)> SyncFinished = new List<(string, string)>();
        public static Dictionary<(string, string), (List<float[]>, List<Int16>, List<string>, List<string>)> UnreliableSerializationData = new Dictionary<(string, string), (List<float[]>, List<Int16>, List<string>, List<string>)>();
        public static List<string> InterestManagementData = new List<string>();
        public static Dictionary<(string, string), (List<string>, List<string>)> ReliableSerializationData = new Dictionary<(string, string), (List<string>, List<string>)>();
        public static Dictionary<(string, string), List<string>> ExecutiveActionData = new Dictionary<(string, string), List<string>>();
        public static List<string> RatelimitValueSyncData = new List<string>();
        public static List<string> RatelimitUpdateData = new List<string>();
        public static Dictionary<(string, string), List<string>> InstantiateData = new Dictionary<(string, string), List<string>>();
        public static Dictionary<(string, string), List<string>> OwnershipTransferData = new Dictionary<(string, string), List<string>>();
        public static List<string> PhotonAuthEventData = new List<string>();
        public static List<string> StatsData = new List<string>();
        public static Dictionary<(string, string), List<string>> PropertiesChangedData = new Dictionary<(string, string), List<string>>();
        public static Dictionary<(string, string), List<string>> LeaveData = new Dictionary<(string, string), List<string>>();
        public static Dictionary<(string, string), List<string>> JoinData = new Dictionary<(string, string), List<string>>();
        private static bool OnRecievedEvents(EventData __0)
        {
            object Il2CppData = Serialization.FromIL2CPPToManaged<object>(__0.Parameters);
            string ManagedData = JsonConvert.SerializeObject(Il2CppData, Formatting.Indented);       
            switch (__0.Code)
            {
                case 1:
                    if (!Config.Instance.Uspeak) return true;
                    JObject ParsedUSpeak = JObject.Parse(ManagedData);
                    Photon.Realtime.Player USpeakSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedUSpeak["254"].ToObject<int>());
                    if (!USpeakData.ContainsKey((USpeakSender.field_Public_Player_0.field_Private_APIUser_0.displayName, USpeakSender.field_Public_Player_0.field_Private_APIUser_0.id))) 
                        USpeakData.Add((USpeakSender.field_Public_Player_0.field_Private_APIUser_0.displayName, USpeakSender.field_Public_Player_0.field_Private_APIUser_0.id), (new List<string>(), new List<string>()));
                    else
                    {
                        USpeakData[(USpeakSender.field_Public_Player_0.field_Private_APIUser_0.displayName, USpeakSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item1.Add(ParsedUSpeak["245"].ToObject<string>());
                        USpeakData[(USpeakSender.field_Public_Player_0.field_Private_APIUser_0.displayName, USpeakSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item2.Add(Utilities.PrintBytes(Serialization.FromIL2CPPToManaged<byte[]>(__0.CustomData)));
                    }
                    return true;
                case 3:
                    if (!Config.Instance.PastEvent) return true;
                    JObject ParsedPastEvent = JObject.Parse(ManagedData);
                    Photon.Realtime.Player PastEventSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedPastEvent["254"].ToObject<int>());
                    if (!PastEvent.Contains((PastEventSender.field_Public_Player_0.field_Private_APIUser_0.displayName, PastEventSender.field_Public_Player_0.field_Private_APIUser_0.id)))
                        PastEvent.Add((PastEventSender.field_Public_Player_0.field_Private_APIUser_0.displayName, PastEventSender.field_Public_Player_0.field_Private_APIUser_0.id));
                    return true;
                case 4:
                    if (!Config.Instance.SyncEvents) return true;
                    JObject ParsedSyncEvents = JObject.Parse(ManagedData);
                    Photon.Realtime.Player SyncEventsSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedSyncEvents["254"].ToObject<int>());
                    if (!SyncEventsData.ContainsKey((SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.displayName, SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.id)))
                    {
                        SyncEventsData.Add((SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.displayName, SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.id), (new List<string[]>(), new List<string>()));
                        SyncEventsData[(SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.displayName, SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item1.Add(ParsedSyncEvents["245"].ToObject<string[]>());
                        SyncEventsData[(SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.displayName, SyncEventsSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item2.Add(Utilities.PrintBytes(Serialization.ToByteArray(__0.CustomData)));
                    }                
                    return true;
                case 5:
                    if (!Config.Instance.SyncFinished) return true;
                    JObject ParsedSyncFinished = JObject.Parse(ManagedData);
                    Photon.Realtime.Player SyncFinishedSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedSyncFinished["254"].ToObject<int>());
                    if (!SyncFinished.Contains((SyncFinishedSender.field_Public_Player_0.field_Private_APIUser_0.displayName, SyncFinishedSender.field_Public_Player_0.field_Private_APIUser_0.id)))
                        SyncFinished.Add((SyncFinishedSender.field_Public_Player_0.field_Private_APIUser_0.displayName, SyncFinishedSender.field_Public_Player_0.field_Private_APIUser_0.id));
                    return true;
                case 6:
                    return true;
                case 7:
                    if (!Config.Instance.UnreliableSerialization) return true;
                    JObject ParsedUnreliableSerialization = JObject.Parse(ManagedData);
                    Photon.Realtime.Player UnreliableSerializationSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedUnreliableSerialization["254"].ToObject<int>());
                    try
                    {
                        if (UnreliableSerializationSender.field_Public_Player_0.prop_VRCPlayer_0 != null)
                        {
                            byte[] UnreliableSerializationByteData = Serialization.FromIL2CPPToManaged<byte[]>(__0.CustomData);
                            float[] PlayerPositions = { BitConverter.ToSingle(UnreliableSerializationByteData, 48), BitConverter.ToSingle(UnreliableSerializationByteData, 52), BitConverter.ToSingle(UnreliableSerializationByteData, 56) };
                            Int16 Ping = BitConverter.ToInt16(UnreliableSerializationByteData, 68);
                            if (!UnreliableSerializationData.ContainsKey((UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)))
                                UnreliableSerializationData.Add((UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id), (new List<float[]>(), new List<Int16>(), new List<string>(), new List<string>()));
                            else
                            {
                                UnreliableSerializationData[(UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item1.Add(PlayerPositions);
                                UnreliableSerializationData[(UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item2.Add(Ping);
                                UnreliableSerializationData[(UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item3.Add(ParsedUnreliableSerialization["245"].ToObject<string>());
                                UnreliableSerializationData[(UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, UnreliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item4.Add(Utilities.PrintBytes(Serialization.FromIL2CPPToManaged<byte[]>(__0.CustomData)));
                            }
                        }
                    } catch { }
                    return true;
                case 8:
                    if (!Config.Instance.InterestManagement) return true;
                    if (!InterestManagementData.Contains(ManagedData)) InterestManagementData.Add(ManagedData);
                    return true;
                case 9:
                    if (!Config.Instance.ReliableSerialization) return true;
                    JObject ParsedReliableSerialization = JObject.Parse(ManagedData);
                    Photon.Realtime.Player ReliableSerializationSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedReliableSerialization["254"].ToObject<int>());
                    if (!ReliableSerializationData.ContainsKey((ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)))
                        ReliableSerializationData.Add((ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id), (new List<string>(), new List<string>()));
                    else
                    {
                        ReliableSerializationData[(ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item1.Add(ParsedReliableSerialization["245"].ToObject<string>());
                        ReliableSerializationData[(ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.displayName, ReliableSerializationSender.field_Public_Player_0.field_Private_APIUser_0.id)].Item2.Add(Utilities.PrintBytes(Serialization.FromIL2CPPToManaged<byte[]>(__0.CustomData)));
                    }
                    return true;
                case 33:
                    if (!Config.Instance.ExecutiveAction && ManagedData == null) return true;
                    JObject ParsedExecutiveAction = JObject.Parse(ManagedData);
                    if (ParsedExecutiveAction["245"].SelectToken("1") == null) 
                        if (ExecutiveActionData.ContainsKey(("GlobalVrcEvent", "GlobalVrcEvent"))) ExecutiveActionData[("GlobalVrcEvent", "GlobalVrcEvent")].Add(ManagedData);
                        else ExecutiveActionData.Add(("GlobalVrcEvent", "GlobalVrcEvent"), new List<string>() { ManagedData });
                    else
                    {
                        int ActorID = ParsedExecutiveAction["245"].SelectToken("1").ToObject<int>();
                        if (ActorID <= 0) return true;
                        Photon.Realtime.Player ExecutiveActionSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ActorID);
                        MelonCoroutines.Start(RecievedEventSync(ExecutiveActionSender, ManagedData, EventType.ExecutiveActionEvent));
                    }
                    return true;
                case 34:
                    if (!Config.Instance.RatelimitValueSync) return true;
                    if (!RatelimitValueSyncData.Contains(ManagedData)) RatelimitValueSyncData.Add(ManagedData);
                    return true;
                case 35:
                    if (!Config.Instance.RatelimitUpdate) return true;
                    if (!RatelimitUpdateData.Contains(ManagedData)) RatelimitUpdateData.Add(ManagedData);
                    return true;
                case 202:
                    if (!Config.Instance.Instantiate) return true;
                    JObject ParsedInstantiate = JObject.Parse(ManagedData);
                    Photon.Realtime.Player InstantiateSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedInstantiate["254"].ToObject<int>());
                    MelonCoroutines.Start(RecievedEventSync(InstantiateSender, ManagedData, EventType.InstantiateEvent));    
                    return true;
                /*case 209:
                    if (!Config.Instance.OwnershipRequest) return true;
                    JObject ParsedOwnershipRequest = JObject.Parse(ManagedData);
                    // need to log first
                    return true;*/
                case 210:
                    if (!Config.Instance.OwnershipTransfer) return true;
                    JObject ParsedOwnershipTransfer = JObject.Parse(ManagedData);
                    Photon.Realtime.Player OwnershipTransferSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedOwnershipTransfer["254"].ToObject<int>());
                    MelonCoroutines.Start(RecievedEventSync(OwnershipTransferSender, ManagedData, EventType.OwnershipTransferEvent));
                    return true;
                case 223:
                    if (!Config.Instance.PhotonAuthEvent) return true;
                    if (!PhotonAuthEventData.Contains(ManagedData)) PhotonAuthEventData.Add(ManagedData);
                    return true;
                case 226:
                    if (!Config.Instance.Stats) return true;
                    if (!StatsData.Contains(ManagedData)) StatsData.Add(ManagedData);
                    return true;
                case 253:
                    if (!Config.Instance.PropertiesChanged) return true;
                    JObject ParsedPropertiesChanged = JObject.Parse(ManagedData);
                    string PropertiesChangedSenderID = ParsedPropertiesChanged["251"]["user"].SelectToken("id").ToObject<string>();
                    string PropertiesChangedSenderDisplayName = ParsedPropertiesChanged["251"]["user"].SelectToken("displayName").ToObject<string>();
                    if (!PropertiesChangedData.ContainsKey((PropertiesChangedSenderDisplayName, PropertiesChangedSenderID)))
                        PropertiesChangedData.Add((PropertiesChangedSenderDisplayName, PropertiesChangedSenderID), new List<string>() { ManagedData });
                    else PropertiesChangedData[(PropertiesChangedSenderDisplayName, PropertiesChangedSenderID)].Add(ManagedData);
                    return true;
                case 254:
                    if (!Config.Instance.Leave) return true;                 
                    JObject ParsedLeave = JObject.Parse(ManagedData);
                    Photon.Realtime.Player LeaveSender = PhotonExtensions.LoadBalancingClient.GetPhotonPlayer(ParsedLeave["254"].ToObject<int>());
                    if (LeaveSender.field_Public_Player_0.field_Private_APIUser_0 == null) return true;
                    if (!LeaveData.ContainsKey((LeaveSender.field_Public_Player_0.field_Private_APIUser_0.displayName, LeaveSender.field_Public_Player_0.field_Private_APIUser_0.id)))
                        LeaveData.Add((LeaveSender.field_Public_Player_0.field_Private_APIUser_0.displayName, LeaveSender.field_Public_Player_0.field_Private_APIUser_0.id), new List<string>() { ManagedData });
                    else LeaveData[(LeaveSender.field_Public_Player_0.field_Private_APIUser_0.displayName, LeaveSender.field_Public_Player_0.field_Private_APIUser_0.id)].Add(ManagedData);
                    return true;
                case 255:
                    if (!Config.Instance.Join) return true;
                    JObject ParsedJoin = JObject.Parse(ManagedData);
                    string JoinSenderID = ParsedJoin["249"]["user"].SelectToken("id").ToObject<string>();
                    string JoinSenderDisplayName = ParsedJoin["249"]["user"].SelectToken("displayName").ToObject<string>();
                    if (!JoinData.ContainsKey((JoinSenderDisplayName, JoinSenderID)))
                        JoinData.Add((JoinSenderDisplayName, JoinSenderID), new List<string>() { ManagedData });
                    else JoinData[(JoinSenderDisplayName, JoinSenderID)].Add(ManagedData);
                    return true;
                default:
                    Console.WriteLine($"Unknown Event Found: [Code: {__0.Code}] Data:\n" + ManagedData);
                    return true;
            }
        }        
        public enum EventType
        {
            ExecutiveActionEvent,
            InstantiateEvent,
            OwnershipTransferEvent
        }
        private static IEnumerator RecievedEventSync(Photon.Realtime.Player player, string Data, EventType type)
        {
            while (true)
            {
                if (player.field_Public_Player_0 != null && player.field_Public_Player_0.field_Private_APIUser_0 != null)
                {
                    switch (type)
                    {
                        case EventType.ExecutiveActionEvent:
                            if (ExecutiveActionData.ContainsKey((player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id))) ExecutiveActionData[(player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id)].Add(Data);
                            else ExecutiveActionData.Add((player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id), new List<string>() { Data });
                            break;
                        case EventType.InstantiateEvent:
                            if (!InstantiateData.ContainsKey((player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id)))
                                InstantiateData.Add((player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id), new List<string>() { Data });
                            else InstantiateData[(player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id)].Add(Data);
                            break;
                        case EventType.OwnershipTransferEvent:
                            if (!OwnershipTransferData.ContainsKey((player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id)))
                                OwnershipTransferData.Add((player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id), new List<string>() { Data });
                            else OwnershipTransferData[(player.field_Public_Player_0.field_Private_APIUser_0.displayName, player.field_Public_Player_0.field_Private_APIUser_0.id)].Add(Data);
                            break;
                    }
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        public static Dictionary<(string, string), List<(VRC.SDKBase.VRC_EventHandler.VrcEventType, int, VRC.SDKBase.VRC_EventHandler.VrcBroadcastType, string, string)>> ProcessEventData = new Dictionary<(string, string), List<(VRC.SDKBase.VRC_EventHandler.VrcEventType, int, VRC.SDKBase.VRC_EventHandler.VrcBroadcastType, string, string)>>();
        private static bool OnEventDispatcher(VRC.Player __0, VRC.SDKBase.VRC_EventHandler.VrcEvent __1, VRC.SDKBase.VRC_EventHandler.VrcBroadcastType __2)
        {
            if (Config.Instance.ProcessEvent && __0 != null && __0.field_Private_APIUser_0.id != VRC.Core.APIUser.CurrentUser.id)
            {
                if (!ProcessEventData.ContainsKey((__0.field_Private_APIUser_0.displayName, __0.field_Private_APIUser_0.id)))
                    ProcessEventData.Add((__0.field_Private_APIUser_0.displayName, __0.field_Private_APIUser_0.id), new List<(VRC.SDKBase.VRC_EventHandler.VrcEventType, int, VRC.SDKBase.VRC_EventHandler.VrcBroadcastType, string, string)>());
                else ProcessEventData[(__0.field_Private_APIUser_0.displayName, __0.field_Private_APIUser_0.id)].Add((__1.EventType, __1.ParameterInt, __2, __1.ParameterString, __1.ParameterObject.name));
            }
            return true;
        }
    }
}