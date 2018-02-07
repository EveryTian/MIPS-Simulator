using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    class MachineCode
    {
        public uint machineCode { get; }
        public uint opcode { get; }
        public uint func { get; }
        public uint rd { get; }
        public uint rs { get; }
        public uint rt { get; }
        public uint shamt { get; }
        public uint imme { get; }
        public uint target { get; }

        public MachineCode(uint machineCode)
        {
            opcode = (machineCode & 0xFC000000) >> 26;
            rd = (machineCode & 0xF800) >> 11;
            rt = (machineCode & 0x1F0000) >> 16;
            rs = (machineCode & 0x3E00000) >> 21;
            shamt = (machineCode & 0x3E0) >> 6;
            func = machineCode & 0x3F;
            imme = machineCode & 0xFFFF;
            imme = (uint)(((int)imme << 16) >> 16);
            target = machineCode & 0x3FFFFFF;
        }
    }
}
