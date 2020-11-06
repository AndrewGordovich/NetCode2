﻿using NetCode2.Common.Realtime.Data.Commands;

namespace NetCode2.Common.Realtime.Serialization
{
    public class SimulationCommandsSerializer
    {
        private const ushort StreamCapacity = 500;

        private readonly BitOutStream outStream = new BitOutStream(StreamCapacity);
        private readonly BitInStream inStream = new BitInStream(StreamCapacity);

        public byte[] Serialize(SimulationCommand command)
        {
            ByteCommand byteCommand = (ByteCommand) command.GameCommand;

            outStream.WriteByte(155);
            outStream.WriteShort(60000);
            outStream.WriteShort(60000);

            return outStream.ToArray();
        }
    }
}