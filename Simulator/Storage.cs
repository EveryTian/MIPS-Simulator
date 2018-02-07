using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    class Storage
    {
        public uint[] registers { get; set; }
        public uint registerPC { get; set; }
        public uint[] memory { get; set; }

        public Storage()
        {
            registers = new uint[32];
            registerPC = 0x3000;
            memory = new uint[ConstVar.MAX_WORDS];
        }

        public void setMemory(uint wordAddress, uint content)
        {
            memory[wordAddress % ConstVar.MAX_WORDS] = content;
        }

        public uint getRealPCValue()
        {
            // Actually, also the value of PC used in this program is a psedo PC value and this value is PC / 4.
            return registerPC * 4;
        }

    }

    public class ConstVar
    {
        public const uint MAX_WORDS = 0x8000; // MAX: 0x7FFFFFFE
        public const uint DEFAULT_PC = 0x3000;
        public const uint REGISTER_NUM = 33; // 32 + PC
    }
}
