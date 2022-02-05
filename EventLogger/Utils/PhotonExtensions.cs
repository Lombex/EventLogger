﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Realtime;
using Photon.Pun;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Utils
{
    public static class PhotonExtensions
    {
        private static readonly Dictionary<int, Player> ActorIDToPlayer = new Dictionary<int, Player>();
        public static List<Player> AllPhotonPlayers(this LoadBalancingClient Instance)
        {
            List<Player> PlayerList = new List<Player>();
            foreach (var players in Instance.prop_Player_0.prop_Room_0.field_Private_Dictionary_2_Int32_Player_0) PlayerList.Add(players.Value);
            return PlayerList;
        }
        public static LoadBalancingClient LoadBalancingClient => PhotonNetwork.field_Public_Static_LoadBalancingClient_0;
        public static int GetPlayerActorID(this Player player) => player.field_Private_Int32_0;
        public static void RefreshPhotonPlayers() 
        { 
            ActorIDToPlayer.Clear();
            foreach (var player in AllPhotonPlayers(LoadBalancingClient)) ActorIDToPlayer.Add(player.GetPlayerActorID(), player);
        } 
        public static Player GetPhotonPlayer(this LoadBalancingClient Instance, int ActorID)
        {
            if (ActorIDToPlayer.ContainsKey(ActorID)) return ActorIDToPlayer[ActorID];
            else
            {
                PhotonExtensions.RefreshPhotonPlayers();
                return ActorIDToPlayer[ActorID];
            }
        }
        public static int GetCurrentActorID(this LoadBalancingClient Instance)
        {
            return GetPlayerActorID(Instance.prop_Player_0);
        }
    }
    public static class Serialization
    {
        public static byte[] ToByteArray(Il2CppSystem.Object obj)
        {
            if (obj == null) return null;
            var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var ms = new Il2CppSystem.IO.MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        public static byte[] ToByteArray(object obj)
        {
            if (obj == null) return null;
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null) return default;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
        public static T IL2CPPFromByteArray<T>(byte[] data)
        {
            if (data == null) return default(T);
            var bf = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var ms = new Il2CppSystem.IO.MemoryStream(data);
            object obj = bf.Deserialize(ms);
            return (T)obj;
        }
        public static T FromIL2CPPToManaged<T>(Il2CppSystem.Object obj)
        {
            return FromByteArray<T>(ToByteArray(obj));
        }
        public static T FromManagedToIL2CPP<T>(object obj)
        {
            return IL2CPPFromByteArray<T>(ToByteArray(obj));
        }
        public static byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
    }
}
