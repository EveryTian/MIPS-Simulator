using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    public partial class MainForm : Form
    {
        private Storage centralCtrlData = new Storage();
        bool isDataExist = false;
        uint[] data = null;
        long dataLen = 0;
        public MainForm()
        {
            InitializeComponent();
            memoryListView.BeginUpdate();
            for (long i = 0; i < (long)ConstVar.MAX_WORDS; ++i)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.ImageIndex = (int)i;
                lvi.Text = "";
                lvi.SubItems.Add("x" + (i * 4).ToString("X8"));
                lvi.SubItems.Add("x00000000");
                lvi.SubItems.Add("");
                memoryListView.Items.Add(lvi);
            }
            memoryListView.Items[(int)ConstVar.DEFAULT_PC].SubItems[0].Text = "->";
            memoryListView.EndUpdate();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oriStatus = toolStripStatusLabelMachineStatus.Text;
            toolStripStatusLabelMachineStatus.Text = "SELECT";
            FileCtrl inputFile = new FileCtrl();
            inputFile.genFilePathWithDialog();
            data = inputFile.readFile();
            dataLen = inputFile.getDataLen();
            if (data != null)
            {
                loadData();
                isDataExist = true;
                toolStripStatusLabelMachineStatus.Text = "READY";
            }
            else
            {
                isDataExist = false;
                toolStripStatusLabelMachineStatus.Text = oriStatus;
            }
            reloadFileToolStripMenuItem.Enabled = isDataExist;
        }

        private void loadData()
        {
            string oriStatus = toolStripStatusLabelMachineStatus.Text;
            toolStripStatusLabelMachineStatus.Text = "LOADING";
            Cursor = Cursors.WaitCursor;
            for (long i = 1; i < dataLen && i < ConstVar.MAX_WORDS; ++i)
            {
                centralCtrlData.setMemory((data[0] + (uint)i - 1) % ConstVar.MAX_WORDS, data[i]);
                memoryListView.Items[(int)((data[0] + i - 1) % ConstVar.MAX_WORDS)].SubItems[2].Text = "x" + data[i].ToString("X8");
            }
            updatePC(data[0] % ConstVar.MAX_WORDS);
            Cursor = Cursors.Default;
            toolStripStatusLabelMachineStatus.Text = oriStatus;
        }

        private void updatePC(uint newPCValue)
        {
            newPCValue %= ConstVar.MAX_WORDS;
            memoryListView.Items[(int)centralCtrlData.registerPC].SubItems[0].Text = "";
            centralCtrlData.registerPC = newPCValue;
            updatePCValueLabel();
            memoryListView.Items[(int)newPCValue].SubItems[0].Text = "->";
            memoryListView.EnsureVisible((int)newPCValue);
        }
        private void updatePCValueLabel()
        {
            toolStripStatusLabelPCValue.Text = "x" + centralCtrlData.getRealPCValue().ToString("X8");
        }
        private void incrementPC()
        {
            updatePC(centralCtrlData.registerPC + 1);
        }

        private void reinitializeMemory()
        {
            for (long i = 0; i < ConstVar.MAX_WORDS; ++i)
            {
                centralCtrlData.setMemory((uint)i, 0x0);
                memoryListView.Items[(int)i].SubItems[2].Text = "x00000000";
                memoryListView.Items[(int)i].SubItems[3].Text = "";
            }
            updatePC(ConstVar.DEFAULT_PC);
        }

        private void reinitializeRegisters()
        {
            for (int i = 0; i < 32; ++i)
            {
                centralCtrlData.registers[i] = 0x0;
            }
            updateRegisterLabels();
        }

        private void updateRegisterLabels()
        {
            labelAtValue.Text = "x" + centralCtrlData.registers[1].ToString("X8");
            labelV0Value.Text = "x" + centralCtrlData.registers[2].ToString("X8");
            labelV1Value.Text = "x" + centralCtrlData.registers[3].ToString("X8");
            labelA0Value.Text = "x" + centralCtrlData.registers[4].ToString("X8");
            labelA1Value.Text = "x" + centralCtrlData.registers[5].ToString("X8");
            labelA2Value.Text = "x" + centralCtrlData.registers[6].ToString("X8");
            labelA3Value.Text = "x" + centralCtrlData.registers[7].ToString("X8");
            labelT0Value.Text = "x" + centralCtrlData.registers[8].ToString("X8");
            labelT1Value.Text = "x" + centralCtrlData.registers[9].ToString("X8");
            labelT2Value.Text = "x" + centralCtrlData.registers[10].ToString("X8");
            labelT3Value.Text = "x" + centralCtrlData.registers[11].ToString("X8");
            labelT4Value.Text = "x" + centralCtrlData.registers[12].ToString("X8");
            labelT5Value.Text = "x" + centralCtrlData.registers[13].ToString("X8");
            labelT6Value.Text = "x" + centralCtrlData.registers[14].ToString("X8");
            labelT7Value.Text = "x" + centralCtrlData.registers[15].ToString("X8");
            labelS0Value.Text = "x" + centralCtrlData.registers[16].ToString("X8");
            labelS1Value.Text = "x" + centralCtrlData.registers[17].ToString("X8");
            labelS2Value.Text = "x" + centralCtrlData.registers[18].ToString("X8");
            labelS3Value.Text = "x" + centralCtrlData.registers[19].ToString("X8");
            labelS4Value.Text = "x" + centralCtrlData.registers[20].ToString("X8");
            labelS5Value.Text = "x" + centralCtrlData.registers[21].ToString("X8");
            labelS6Value.Text = "x" + centralCtrlData.registers[22].ToString("X8");
            labelS7Value.Text = "x" + centralCtrlData.registers[23].ToString("X8");
            labelT8Value.Text = "x" + centralCtrlData.registers[24].ToString("X8");
            labelT9Value.Text = "x" + centralCtrlData.registers[25].ToString("X8");
            labelK0Value.Text = "x" + centralCtrlData.registers[26].ToString("X8");
            labelK1Value.Text = "x" + centralCtrlData.registers[27].ToString("X8");
            labelGpValue.Text = "x" + centralCtrlData.registers[28].ToString("X8");
            labelSpValue.Text = "x" + centralCtrlData.registers[29].ToString("X8");
            labelFpValue.Text = "x" + centralCtrlData.registers[30].ToString("X8");
            labelRaValue.Text = "x" + centralCtrlData.registers[31].ToString("X8");
        }

        private void reinitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelMachineStatus.Text = "REINITING";
            Cursor = Cursors.WaitCursor;
            reinitializeMemory();
            reinitializeRegisters();
            Cursor = Cursors.Default;
            toolStripStatusLabelMachineStatus.Text = "IDLE";
        }

        private void reloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (data != null)
            {
                toolStripStatusLabelMachineStatus.Text = "RELOADING";
                loadData();
                toolStripStatusLabelMachineStatus.Text = "READY";
            }
        }

        public void executeOnce()
        {
            excuteMachineCode(centralCtrlData.memory[centralCtrlData.registerPC]);
        }

        public void excuteMachineCode(uint machineCode)
        {
            MachineCode machineCodeAnalysis = new MachineCode(machineCode);
            switch (machineCodeAnalysis.opcode)
            {
                case 0x0:
                    switch (machineCodeAnalysis.func)
                    {
                        case 0x20: excuteAdd(ref machineCodeAnalysis); break;
                        case 0x21: excuteAddu(ref machineCodeAnalysis); break;
                        case 0x24: excuteAnd(ref machineCodeAnalysis); break;
                        case 0x0D: excuteBreak(ref machineCodeAnalysis); break;
                        case 0x1A: excuteDiv(ref machineCodeAnalysis); break;
                        case 0x1B: excuteDivu(ref machineCodeAnalysis); break;
                        case 0x09: excuteJalr(ref machineCodeAnalysis); break;
                        case 0x08: excuteJr(ref machineCodeAnalysis); break;
                        case 0x10: excuteMfhi(ref machineCodeAnalysis); break;
                        case 0x12: excuteMflo(ref machineCodeAnalysis); break;
                        case 0x11: excuteMthi(ref machineCodeAnalysis); break;
                        case 0x13: excuteMtlo(ref machineCodeAnalysis); break;
                        case 0x18: excuteMult(ref machineCodeAnalysis); break;
                        case 0x19: excuteMultu(ref machineCodeAnalysis); break;
                        case 0x27: excuteNor(ref machineCodeAnalysis); break;
                        case 0x25: excuteOr(ref machineCodeAnalysis); break;
                        case 0x00: excuteSll(ref machineCodeAnalysis); break;
                        case 0x04: excuteSllv(ref machineCodeAnalysis); break;
                        case 0x2A: excuteSlt(ref machineCodeAnalysis); break;
                        case 0x2B: excuteSltu(ref machineCodeAnalysis); break;
                        case 0x03: excuteSra(ref machineCodeAnalysis); break;
                        case 0x07: excuteSrav(ref machineCodeAnalysis); break;
                        case 0x02: excuteSrl(ref machineCodeAnalysis); break;
                        case 0x06: excuteSrlv(ref machineCodeAnalysis); break;
                        case 0x22: excuteSub(ref machineCodeAnalysis); break;
                        case 0x23: excuteSubu(ref machineCodeAnalysis); break;
                        case 0x0C: excuteSyscall(ref machineCodeAnalysis); break;
                        case 0x26: excuteXor(ref machineCodeAnalysis); break;
                    }
                    break;
                case 0x08: executeAddi(ref machineCodeAnalysis); break;
                case 0x09: executeAddiu(ref machineCodeAnalysis); break;
                case 0x0C: executeAndi(ref machineCodeAnalysis); break;
                case 0x04: executeBeq(ref machineCodeAnalysis); break;
                case 0x01:
                    if (machineCodeAnalysis.rt == 0x1)
                    {
                        executeBgez(ref machineCodeAnalysis);
                    }
                    else if (machineCodeAnalysis.rt == 0x0)
                    {
                        executeBltz(ref machineCodeAnalysis);
                    }
                    break;
                case 0x07: executeBgtz(ref machineCodeAnalysis); break;
                case 0x06: executeBlez(ref machineCodeAnalysis); break;
                case 0x05: executeBne(ref machineCodeAnalysis); break;
                case 0x20: executeLb(ref machineCodeAnalysis); break;
                case 0x24: executeLbu(ref machineCodeAnalysis); break;
                case 0x21: executeLh(ref machineCodeAnalysis); break;
                case 0x25: executeLhu(ref machineCodeAnalysis); break;
                case 0x0F: executeLui(ref machineCodeAnalysis); break;
                case 0x23: executeLw(ref machineCodeAnalysis); break;
                case 0x31: executeLwc1(ref machineCodeAnalysis); break;
                case 0x0D: executeOri(ref machineCodeAnalysis); break;
                case 0x28: executeSb(ref machineCodeAnalysis); break;
                case 0x0A: executeSlti(ref machineCodeAnalysis); break;
                case 0x0B: executeSltiu(ref machineCodeAnalysis); break;
                case 0x29: executeSh(ref machineCodeAnalysis); break;
                case 0x2B: executeSw(ref machineCodeAnalysis); break;
                case 0x39: executeSwc1(ref machineCodeAnalysis); break;
                case 0x0E: executeXori(ref machineCodeAnalysis); break;
                case 0x02: executeJ(ref machineCodeAnalysis); break;
                case 0x03: executeJal(ref machineCodeAnalysis); break;
                default: break;
            }
            centralCtrlData.registers[0] = 0x0;
            updateRegisterLabels();
        }

        // For executing assembly:
        private void setRegisterValue(uint registerSerNum, uint value)
        {
            centralCtrlData.registers[registerSerNum] = value;
        }
        private uint getRegisterValue(uint registerSerNum)
        {
            return centralCtrlData.registers[registerSerNum];
        }
        private void setByteMemoryValue(uint address, uint value)
        {
            address %= (ConstVar.MAX_WORDS * 4);
            value &= 0xFF;
            switch (address % 4)
            {
                case 3:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFFFFFF00;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= value;
                    break;
                case 2:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFFFF00FF;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= (value << 8);
                    break;
                case 1:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFF00FFFF;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= (value << 16);
                    break;
                case 0:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFFFFFF;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= (value << 24);
                    break;
            }
        }
        private void setHalfWordMemoryValue(uint address, uint value)
        {
            setByteMemoryValue(address, (value & 0xFF00) >> 8);
            setByteMemoryValue(address + 1, value & 0xFF);
        }
        private void setWordMemoryValue(uint address, uint value)
        {
            address %= (ConstVar.MAX_WORDS * 4);
            switch (address % 4)
            {
                case 3:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFFFFFF00;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= ((value & 0xFF000000) >> 24);
                    centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] &= 0xFF;
                    centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] |= ((value & 0xFFFFFF) << 8);
                    break;
                case 2:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFFFF0000;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= ((value & 0xFFFF0000) >> 16);
                    centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] &= 0xFFFF;
                    centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] |= ((value & 0xFFFF) << 16);
                    break;
                case 1:
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] &= 0xFF000000;
                    centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] |= ((value & 0xFFFFFF00) >> 8);
                    centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] &= 0xFFFFFF;
                    centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] |= ((value & 0xFF) << 24);
                    break;
                case 0:
                    centralCtrlData.setMemory((address / 4) % ConstVar.MAX_WORDS, value);
                    break;
            }
        }
        private uint getWordMemoryValue(uint address)
        {
            uint memoryValue;
            address %= (ConstVar.MAX_WORDS * 4);
            switch (address % 4)
            {
                case 3:
                    memoryValue = (centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] & 0xFF) << 24;
                    memoryValue |= ((centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] & 0xFFFFFF00) >> 8);
                    break;
                case 2:
                    memoryValue = (centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] & 0xFFFF) << 16;
                    memoryValue |= ((centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] & 0xFFFF0000) >> 16);
                    break;
                case 1:
                    memoryValue = (centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS] & 0xFFFFFF) << 8;
                    memoryValue |= ((centralCtrlData.memory[(address / 4 + 1) % ConstVar.MAX_WORDS] & 0xFF000000) >> 24);
                    break;
                case 0:
                    memoryValue = centralCtrlData.memory[(address / 4) % ConstVar.MAX_WORDS];
                    break;
                default: // Actually this will not happen.
                    memoryValue = 0x0;
                    break;
            }
            return memoryValue;
        }
        // Execute assembly:
        // R
        private void excuteAdd(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) + getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void excuteAddu(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) + getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void excuteAnd(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) & getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void excuteBreak(ref MachineCode machineCodeAnalysis) { }
        private void excuteDiv(ref MachineCode machineCodeAnalysis) { }
        private void excuteDivu(ref MachineCode machineCodeAnalysis) { }
        private void excuteJalr(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, centralCtrlData.registerPC + 1);
            updatePC(getRegisterValue(machineCodeAnalysis.rs));
        }
        private void excuteJr(ref MachineCode machineCodeAnalysis)
        {
            updatePC(getRegisterValue(machineCodeAnalysis.rs));
        }
        private void excuteMfhi(ref MachineCode machineCodeAnalysis) { }
        private void excuteMflo(ref MachineCode machineCodeAnalysis) { }
        private void excuteMthi(ref MachineCode machineCodeAnalysis) { }
        private void excuteMtlo(ref MachineCode machineCodeAnalysis) { }
        private void excuteMult(ref MachineCode machineCodeAnalysis) { }
        private void excuteMultu(ref MachineCode machineCodeAnalysis) { }
        private void excuteNor(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, ~(getRegisterValue(machineCodeAnalysis.rs) | getRegisterValue(machineCodeAnalysis.rt)));
            incrementPC();
        }
        private void excuteOr(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) | getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void excuteSll(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rt) << (int)machineCodeAnalysis.shamt);
            incrementPC();
        }
        private void excuteSllv(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rt) << (int)getRegisterValue(machineCodeAnalysis.rs));
            incrementPC();
        }
        private void excuteSlt(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, ((int)getRegisterValue(machineCodeAnalysis.rs) < (int)getRegisterValue(machineCodeAnalysis.rt) ? (uint)1 : (uint)0));
            incrementPC();
        }
        private void excuteSltu(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, (getRegisterValue(machineCodeAnalysis.rs) < getRegisterValue(machineCodeAnalysis.rt) ? (uint)1 : (uint)0));
            incrementPC();
        }
        private void excuteSra(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, (uint)((int)getRegisterValue(machineCodeAnalysis.rt) >> (int)machineCodeAnalysis.shamt));
            incrementPC();
        }
        private void excuteSrav(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, (uint)((int)getRegisterValue(machineCodeAnalysis.rt) >> (int)getRegisterValue(machineCodeAnalysis.rs)));
            incrementPC();
        }
        private void excuteSrl(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rt) >> (int)machineCodeAnalysis.shamt);
            incrementPC();
        }
        private void excuteSrlv(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rt) >> (int)getRegisterValue(machineCodeAnalysis.rs));
            incrementPC();
        }
        private void excuteSub(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) - getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void excuteSubu(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) - getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void excuteSyscall(ref MachineCode machineCodeAnalysis) { }
        private void excuteXor(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) ^ getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        // IJ
        private void executeAddi(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rt, getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme);
            incrementPC();
        }
        private void executeAddiu(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rt, getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme);
            incrementPC();
        }
        private void executeAndi(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) & (machineCodeAnalysis.imme & 0xFFFF));
            incrementPC();
        }
        private void executeBeq(ref MachineCode machineCodeAnalysis)
        {
            incrementPC();
            if (machineCodeAnalysis.rs == machineCodeAnalysis.rt)
            {
                updatePC(centralCtrlData.registerPC + (machineCodeAnalysis.imme & 0xFFFF));
            }
        }
        private void executeBgez(ref MachineCode machineCodeAnalysis)
        {
            incrementPC();
            if ((int)machineCodeAnalysis.rs >= 0)
            {
                updatePC(centralCtrlData.registerPC + (machineCodeAnalysis.imme & 0xFFFF));
            }
        }
        private void executeBltz(ref MachineCode machineCodeAnalysis)
        {
            incrementPC();
            if ((int)machineCodeAnalysis.rs < 0)
            {
                updatePC(centralCtrlData.registerPC + (machineCodeAnalysis.imme & 0xFFFF));
            }
        }
        private void executeBgtz(ref MachineCode machineCodeAnalysis)
        {
            incrementPC();
            if ((int)machineCodeAnalysis.rs > 0)
            {
                updatePC(centralCtrlData.registerPC + (machineCodeAnalysis.imme & 0xFFFF));
            }
        }
        private void executeBlez(ref MachineCode machineCodeAnalysis)
        {
            incrementPC();
            if ((int)machineCodeAnalysis.rs <= 0)
            {
                updatePC(centralCtrlData.registerPC + (machineCodeAnalysis.imme & 0xFFFF));
            }
        }
        private void executeBne(ref MachineCode machineCodeAnalysis)
        {
            incrementPC();
            if (machineCodeAnalysis.rs != machineCodeAnalysis.rt)
            {
                updatePC(centralCtrlData.registerPC + (machineCodeAnalysis.imme & 0xFFFF));
            }
        }
        private void executeLb(ref MachineCode machineCodeAnalysis)
        {
            uint byteValue = getWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme);
            byteValue = (uint)(((int)byteValue & 0xFF000000) >> 24);
            setRegisterValue(machineCodeAnalysis.rt, byteValue);
            incrementPC();
        }
        private void executeLbu(ref MachineCode machineCodeAnalysis)
        {
            uint byteValue = getWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme);
            byteValue = (byteValue & 0xFF000000) >> 24;
            setRegisterValue(machineCodeAnalysis.rt, byteValue);
            incrementPC();
        }
        private void executeLh(ref MachineCode machineCodeAnalysis)
        {
            uint halfWordValue = getWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme);
            halfWordValue = (uint)(((int)halfWordValue & 0xFFFF0000) >> 16);
            setRegisterValue(machineCodeAnalysis.rt, halfWordValue);
            incrementPC();
        }
        private void executeLhu(ref MachineCode machineCodeAnalysis)
        {
            uint halfWordValue = getWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme);
            halfWordValue = (halfWordValue & 0xFFFF0000) >> 16;
            setRegisterValue(machineCodeAnalysis.rt, halfWordValue);
            incrementPC();
        }
        private void executeLui(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rt, (machineCodeAnalysis.imme & 0xFFFF) << 16);
            incrementPC();
        }
        private void executeLw(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rt, getWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme));
            incrementPC();
        }
        private void executeLwc1(ref MachineCode machineCodeAnalysis) { }
        private void executeOri(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) | (machineCodeAnalysis.imme & 0xFFFF));
            incrementPC();
        }
        private void executeSb(ref MachineCode machineCodeAnalysis)
        {
            setByteMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme, getRegisterValue(machineCodeAnalysis.rt) & 0xFF);
            incrementPC();
        }
        private void executeSlti(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rt, ((int)getRegisterValue(machineCodeAnalysis.rs) < (int)machineCodeAnalysis.imme ? (uint)1 : (uint)0));
            incrementPC();
        }
        private void executeSltiu(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rt, (getRegisterValue(machineCodeAnalysis.rs) < machineCodeAnalysis.imme ? (uint)1 : (uint)0));
            incrementPC();
        }
        private void executeSh(ref MachineCode machineCodeAnalysis)
        {
            setHalfWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme, getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void executeSw(ref MachineCode machineCodeAnalysis)
        {
            setWordMemoryValue(getRegisterValue(machineCodeAnalysis.rs) + machineCodeAnalysis.imme, getRegisterValue(machineCodeAnalysis.rt));
            incrementPC();
        }
        private void executeSwc1(ref MachineCode machineCodeAnalysis) { }
        private void executeXori(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, getRegisterValue(machineCodeAnalysis.rs) ^ (machineCodeAnalysis.imme & 0xFFFF));
            incrementPC();
        }
        private void executeJ(ref MachineCode machineCodeAnalysis)
        {
            updatePC((((centralCtrlData.registerPC << 2) & 0xF0000000) | (machineCodeAnalysis.target << 2)) >> 2);
        }
        private void executeJal(ref MachineCode machineCodeAnalysis)
        {
            setRegisterValue(machineCodeAnalysis.rd, centralCtrlData.registerPC + 1);
            updatePC((((centralCtrlData.registerPC << 2) & 0xF0000000) | (machineCodeAnalysis.target << 2)) >> 2);
        }
        // Assembly execution defination end.

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void stepExecuteOnce()
        {
            string oriStatus = toolStripStatusLabelMachineStatus.Text;
            toolStripStatusLabelMachineStatus.Text = "RUNNUING";
            executeOnce();
            toolStripStatusLabelMachineStatus.Text = oriStatus;
        }
        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stepExecuteOnce();
        }
        private void stepButton_Click(object sender, EventArgs e)
        {
            stepExecuteOnce();
        }
        
        private void incrementPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            incrementPC();
        }
        private void incrementPCButton_Click(object sender, EventArgs e)
        {
            incrementPC();
        }

        private void stepButton_MouseEnter(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.ShowAlways = true;
            tip.SetToolTip(stepButton, "Step");
        }
        private void incrementPCButton_MouseEnter(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.ShowAlways = true;
            tip.SetToolTip(incrementPCButton, "Increment PC");
        }

        private void loadCommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oriStatus = toolStripStatusLabelMachineStatus.Text;
            toolStripStatusLabelMachineStatus.Text = "SELECT";
            OpenFileDialog commentFileDialog = new OpenFileDialog();
            commentFileDialog.Filter = "Comments File|*.*";
            if (commentFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = File.OpenText(commentFileDialog.FileName);
                toolStripStatusLabelMachineStatus.Text = "LOADING";
                Cursor = Cursors.WaitCursor;
                for (long i = centralCtrlData.registerPC, j = 0; sr.EndOfStream != true && j < ConstVar.MAX_WORDS; ++i, ++j)
                {
                    memoryListView.Items[(int)(i % ConstVar.MAX_WORDS)].SubItems[3].Text = sr.ReadLine();
                }
                sr.Close();
                Cursor = Cursors.Default;
            }
            toolStripStatusLabelMachineStatus.Text = oriStatus;
        }

        //Change Value Dialog Execution:
        private void cvdSetRegisterValueParentFunc(uint registerSerNum)
        {
            string oriStatus = toolStripStatusLabelMachineStatus.Text;
            toolStripStatusLabelMachineStatus.Text = "SETUP";
            ChangeValueDialog diag = new ChangeValueDialog(CvdModeConstVar.REGISTER_MODE, registerSerNum);
            diag.ShowDialog();
            if (diag.DialogResult != DialogResult.OK)
            {
                toolStripStatusLabelMachineStatus.Text = oriStatus;
                return;
            }
            if (diag.idx == CvdModeConstVar.PC_SERNUM)
            {
                updatePC(diag.newValue / 4);
            }
            else
            {
                setRegisterValue(diag.idx, diag.newValue);
                updateRegisterLabels();
            }
            toolStripStatusLabelMachineStatus.Text = oriStatus;
        }
        private void cvdSetPCValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(CvdModeConstVar.PC_SERNUM);
        }
        private void cvdSetRegisterValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(ConstVar.REGISTER_NUM);
        }
        private void cvdSetAtValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(1);
        }
        private void cvdSetV0Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(2);
        }
        private void cvdSetV1Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(3);
        }
        private void cvdSetA0Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(4);
        }
        private void cvdSetA1Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(5);
        }
        private void cvdSetA2Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(6);
        }
        private void cvdSetA3Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(7);
        }
        private void cvdSetT0Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(8);
        }
        private void cvdSetT1Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(9);
        }
        private void cvdSetT2Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(10);
        }
        private void cvdSetT3Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(11);
        }
        private void cvdSetT4Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(12);
        }
        private void cvdSetT5Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(13);
        }
        private void cvdSetT6Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(14);
        }
        private void cvdSetT7Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(15);
        }
        private void cvdSetS0Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(16);
        }
        private void cvdSetS1Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(17);
        }
        private void cvdSetS2Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(18);
        }
        private void cvdSetS3Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(19);
        }
        private void cvdSetS4Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(20);
        }
        private void cvdSetS5Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(21);
        }
        private void cvdSetS6Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(22);
        }
        private void cvdSetS7Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(23);
        }
        private void cvdSetT8Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(24);
        }
        private void cvdSetT9Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(25);
        }
        private void cvdSetK0Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(26);
        }
        private void cvdSetK1Value(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(27);
        }
        private void cvdSetGpValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(28);
        }
        private void cvdSetSpValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(29);
        }
        private void cvdSetFpValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(30);
        }
        private void cvdSetRaValue(object sender, EventArgs e)
        {
            cvdSetRegisterValueParentFunc(31);
        }

        private void aboutTheSimulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = "------Supported Instructions------\n" +
                "R Type\n" +
                "add \trd, rs, rt\t10 0000 0x20\n" +
                "addu\trd, rs, rt\t10 0001 0x21\n" +
                "and \trd, rs, rt\t10 0100 0x24\n" +
                "jalr\trd, rs    \t00 1001 0x09\n" +
                "jr  \t    rs    \t00 1000 0x08\n" +
                "nor \trd, rs, rt\t10 0111 0x27\n" +
                "or  \trd, rs, rt\t10 0101 0x25\n" +
                "sll \trd, rt, sa\t00 0000 0x00\n" +
                "sllv\trd, rt, rs\t00 0100 0x04\n" +
                "slt \trd, rs, rt\t10 1010 0x2A\n" +
                "sltu\trd, rs, rt\t10 1011 0x2B\n" +
                "sra \trd, rt, sa\t00 0011 0x03\n" +
                "srav\trd, rt, rs\t00 0111 0x07\n" +
                "srl \trd, rt, sa\t00 0010 0x02\n" +
                "srlv\trd, rt, rs\t00 0110 0x06\n" +
                "sub \trd, rs, rt\t10 0010 0x22\n" +
                "subu\trd, rs, rt\t10 0011 0x23\n" +
                "xor \trd, rs, rt\t10 0110 0x26\n";
            DialogResult re = MessageBox.Show(info, "About This MIPS Simulator",MessageBoxButtons.OKCancel);
            if (re == DialogResult.OK)
            {
                info = "------Supported Instructions------\n" +
                    "I Type\n" +
                    "addi \trt, rs, immediate\t00 1000 0x08\n" +
                    "addiu\trt, rs, immediate\t00 1001 0x09\n" +
                    "andi \trt, rs, immediate\t00 1100 0x0C\n" +
                    "beq  \trs, rt, label    \t00 0100 0x04\n" +
                    "bgez \trs, label        \t00 0001 0x01  rt = 00001\n" +
                    "bgtz \trs, label        \t00 0111 0x07  rt = 00000\n" +
                    "blez \trs, label        \t00 0110 0x06  rt = 00000\n" +
                    "bltz \trs, label        \t00 0001 0x01  rt = 00000\n" +
                    "bne  \trs, rt, label    \t00 0101 0x05\n" +
                    "lb   \trt, immediate(rs)\t10 0000 0x20\n" +
                    "lbu  \trt, immediate(rs)\t10 0100 0x24\n" +
                    "lh   \trt, immediate(rs)\t10 0001 0x21\n" +
                    "lhu  \trt, immediate(rs)\t10 0101 0x25\n" +
                    "lui  \trt, immediate    \t00 1111 0x0F\n" +
                    "lw   \trt, immediate(rs)\t10 0011 0x23\n" +
                    "ori  \trt, rs, immediate\t00 1101 0x0D\n" +
                    "sb   \trt, immediate(rs)\t10 1000 0x28\n" +
                    "slti \trt, rs, immediate\t00 1010 0x0A\n" +
                    "sltiu\trt, rs, immediate\t00 1011 0x0B\n" +
                    "sh   \trt, immediate(rs)\t10 1001 0x29\n" +
                    "sw   \trt, immediate(rs)\t10 1011 0x2B\n" +
                    "xori \trt, rs, immediate\t00 1110 0x0E\n";
                DialogResult re2 = MessageBox.Show(info, "About This MIPS Simulator",MessageBoxButtons.OKCancel);
                if (re2 == DialogResult.OK)
                {
                    info = "------Supported Instructions------\n" +
                        "J Type\n" +
                        "j  \tlabel\t000010 coded address of label 0x02\n" +
                        "jal\tlabel\t000011 coded address of label 0x03\n\n" +
                        "\t\t\tThanks!\n" +
                        "\t\tBy: REN Haotian (3150104714@zju.edu.cn)\n";
                    MessageBox.Show(info, "About This MIPS Simulator", MessageBoxButtons.OK);
                }

            }
        }

        private void findPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryListView.EnsureVisible((int)centralCtrlData.registerPC);
        }

        private void findPCButton_Click(object sender, EventArgs e)
        {
            memoryListView.EnsureVisible((int)centralCtrlData.registerPC);
        }

        private void findPCButton_MouseEnter(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.ShowAlways = true;
            tip.SetToolTip(findPCButton, "Find PC");
        }

        private void gotoHereButton_Click(object sender, EventArgs e)
        {
            if (memoryListView.SelectedIndices.Count != 0)
            {
                updatePC((uint)memoryListView.SelectedIndices[0]);
            }
        }

        private void gotoHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (memoryListView.SelectedIndices.Count != 0)
            {
                updatePC((uint)memoryListView.SelectedIndices[0]);
            }
        }

        private void cvdSetMemoryParentFunc(uint initAddress)
        {
            string oriStatus = toolStripStatusLabelMachineStatus.Text;
            toolStripStatusLabelMachineStatus.Text = "SETUP";
            ChangeValueDialog diag = new ChangeValueDialog(CvdModeConstVar.MEMORY_MODE, initAddress);
            diag.ShowDialog();
            if (diag.DialogResult != DialogResult.OK)
            {
                toolStripStatusLabelMachineStatus.Text = oriStatus;
                return;
            }
            centralCtrlData.setMemory(diag.idx, diag.newValue);
            memoryListView.Items[(int)diag.idx].SubItems[2].Text = 'x' + centralCtrlData.memory[diag.idx].ToString("X8");
            memoryListView.EnsureVisible((int)diag.idx);
            toolStripStatusLabelMachineStatus.Text = oriStatus;
        }
        private void setMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cvdSetMemoryParentFunc(centralCtrlData.getRealPCValue());
        }
        private void memoryListView_DoubleClick(object sender, EventArgs e)
        {
            if (memoryListView.SelectedIndices.Count != 0)
            {
                cvdSetMemoryParentFunc((uint)memoryListView.SelectedIndices[0] * 4);
            }
        }

    }
}
