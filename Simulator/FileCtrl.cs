using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{
    class FileCtrl
    {
        private string inputFilePath;
        uint[] data;
        long dataLen;
        
        public FileCtrl()
        {
            inputFilePath = null;
            data = null;
            dataLen = 0;
        }

        public void genFilePathWithDialog()
        {
            OpenFileDialog inputFileDialog = new OpenFileDialog();
            inputFileDialog.Filter = "MIPS Bindary File|*.BIN";
            if (inputFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = inputFileDialog.FileName;
            }
        }

        public bool isFileExist()
        {
            if (inputFilePath == null)
            {
                return false;
            }
            return File.Exists(inputFilePath);
        }

        public uint[] readFile()
        {
            if (!isFileExist())
            {
                return null;
            }
            FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open);
            BinaryReader inputFileReader = new BinaryReader(inputFileStream);
            long inputFileLength = inputFileStream.Length;
            if (inputFileLength % sizeof(uint) != 0 || inputFileLength == 0)
            {
                return null;
            }
            dataLen = inputFileLength / sizeof(uint);
            data = new uint[dataLen];
            for (long i = 0; i < inputFileLength / sizeof(uint); ++i)
            {
                data[i] = inputFileReader.ReadUInt32();
            }
            inputFileReader.Close();
            return data;
        }
        
        public long getDataLen()
        {
            return dataLen;
        }

    }
}
