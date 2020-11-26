using System;
using System.Collections.Generic;
using NetCode2.Server.Realtime.Contracts;

namespace NetCode2.Server.Common.Meta.Communication
{
    public class RoomMetaData
    {
        public Guid RoomId { get; set; }

        public ISet<ClientId> Players { get; private set; }

        public RoomMetaData()
        {
            Players = new HashSet<ClientId>();
        }
    }
}