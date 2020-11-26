using System;
using System.Collections.Generic;
using ENet;
using NetCode2.Client.Realtime.Connection;
using NetCode2.Client.Realtime.Service;
using UnityEngine;
using Event = ENet.Event;
using EventType = ENet.EventType;

namespace NetCode2.Client.Network.Enet
{
    public class EnetClient : IGamePlayConnection
    {
        public const byte ChannelsCount = 4;
        public const byte OutgoingUnrelibleChannelId = 0;
        public const byte IngoingUnrelibleChannelId = 1;
        public const byte OutgoingRelibleChannelId = 2;
        public const byte IngoingRelibleChannelId = 3;

        private readonly ENetClientSettings eNetClientSettings;
        private bool isClientDisconnected;
        private Queue<INetworkData> incomingQueue;
        private Host client;
        private Address address;
        private Peer peer;

        public ConnectionState ConnectionState { get; private set; }

        public EnetClient(ENetClientSettings eNetClientSettings)
        {
            this.eNetClientSettings = eNetClientSettings;

            isClientDisconnected = false;

            incomingQueue = new Queue<INetworkData>();

            Library.Initialize();

            client = new Host();
            address.SetHost(eNetClientSettings.ServerHostName);
            address.Port = eNetClientSettings.ServerPort;
            client.Create();
        }

        public void Connect()
        {
            try
            {
                if (ConnectionState == ConnectionState.Disconnected)
                {
                    peer = client.Connect(address, ChannelsCount);
                    ConnectionState = ConnectionState.Connecting;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void ServiceAll()
        {
            bool isServicing = true;
            while (isServicing)
            {
                client.Service(0, out var @event);
                isServicing = Service(ref @event);
            }
        }

        public void ServiceOnce()
        {
            client.Service(0, out var @event);
            Service(ref @event);
        }

        public void SendReliable(byte[] data, int length)
        {
            Send(data, length, OutgoingRelibleChannelId, PacketFlags.Reliable);
        }

        public bool HasData()
        {
            return incomingQueue.Count > 0;
        }

        public INetworkData GetData()
        {
            return incomingQueue.Dequeue();
        }

        private void Send(byte[] data, int length, byte channelId, PacketFlags flags)
        {
            Packet packet = default;
            packet.Create(data, length, flags);
            peer.Send(channelId, ref packet);
        }

        private bool Service(ref Event @event)
        {
            switch (@event.Type)
            {
                case EventType.None:
                    return false;
                case EventType.Connect:
                    HandleConnect();
                    return true;
                case EventType.Disconnect:
                    HandleDisconnect();
                    return true;
                case EventType.Timeout:
                    HandleTimeout();
                    return true;
                case EventType.Receive:
                    Debug.Log($"Packet received from server - Channel ID: {@event.ChannelID}, Data Length: {@event.Packet.Length}");
                    incomingQueue.Enqueue(new ENetNetworkData(@event.Packet, @event.Packet.Length));
                    return true;
            }

            return false;
        }

        private void HandleConnect()
        {
            ConnectionState = ConnectionState.Connected;
        }

        private void HandleDisconnect()
        {
            ConnectionState = ConnectionState.Disconnected;
        }

        private void HandleTimeout()
        {
            ConnectionState = ConnectionState.Disconnected;
        }
    }
}