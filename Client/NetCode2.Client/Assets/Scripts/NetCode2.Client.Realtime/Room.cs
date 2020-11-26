using System;
using System.Collections.Generic;
using System.Diagnostics;
using NetCode2.Client.Realtime.Connection;
using NetCode2.Client.Realtime.Service;
using NetCode2.Client.Realtime.Simulation;
using NetCode2.Common.Realtime.Data.Commands;
using NetCode2.Common.Realtime.Service;
using Debug = UnityEngine.Debug;

namespace NetCode2.Client.Realtime
{
    public class Room
    {
        private readonly INetworkService networkService;
        public IGamePlayConnection Connection { get; }

        public ClientSimulation LocalPlayerSimulation { get; private set; }

        private readonly Stopwatch stopwatch = new Stopwatch();
        private long lastTickTime;

        public Room(IGamePlayConnection connection)
        {
            Connection = connection;

            var networkHandlers = new Dictionary<NetworkDataCode, IDataHandler>();
            networkService = new NetworkService(connection, networkHandlers);
        }

        public void StartGame()
        {
            LocalPlayerSimulation = new ClientSimulation(networkService);
            stopwatch.Start();
            lastTickTime = stopwatch.ElapsedMilliseconds;
        }

        public void Update()
        {
            Debug.LogError($"stopwatch.ElapsedMilliseconds: {stopwatch.ElapsedMilliseconds}, lastTickTime: {lastTickTime}");
            if ((stopwatch.ElapsedMilliseconds - lastTickTime) / 1000f > 1f)
            {
                Byte result = 155;
                var byteCommand = new ByteCommand(result);
                LocalPlayerSimulation.AddCommand(byteCommand);
                lastTickTime = stopwatch.ElapsedMilliseconds;
            }
        }
    }
}