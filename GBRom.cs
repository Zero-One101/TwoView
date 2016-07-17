using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoView
{
    class GBRom
    {
        private const int bytesPerTile = 16;
        private const int tilesLength = 256*bytesPerTile;
        internal byte[] romData;

        public GBRom()
        {
            if (!LoadRom())
            {
                throw new BadRomException("Failed to load ROM");
            }
        }

        /// <summary>
        /// Prompts the user for a ROM and loads it
        /// </summary>
        /// <returns>True if successfully loaded, else false</returns>
        public bool LoadRom()
        {
            var openFileDialog = new OpenFileDialog
            {
                AutoUpgradeEnabled = true,
                Filter = "GB ROM|*.gb",
                FileName = "Metroid II"
            };

            try
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return false;
                using (var fStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var binReader = new BinaryReader(fStream);
                    romData = binReader.ReadBytes((int) fStream.Length);
                }

                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
    }
}
