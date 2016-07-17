using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoView
{
    /// <summary>
    /// Holds a 2bpp Gameboy tileset as 8bpp
    /// </summary>
    class GBTileset
    {
        private const int tilesPerSet = 256;
        private const int rawTileSize = 16;
        private const int rawTilesetSize = rawTileSize*tilesPerSet;
        private const int loadedTileSize = 64;
        private const int loadedTilesetSize = loadedTileSize*tilesPerSet;

        private readonly byte[] tileData = new byte[loadedTilesetSize];

        /// <summary>
        /// Reads a tileset from ROM
        /// </summary>
        /// <param name="romData"></param>
        /// <param name="offset"></param>
        /// <param name="tileIndex"></param>
        /// <param name="count"></param>
        public void ReadTiles(byte[] romData, int offset, int tileIndex, int count)
        {
            if (romData == null) throw new ArgumentNullException(nameof(romData));
            if (count <= 0) throw new ArgumentException("Count must be a positive value greater than zero.");
            if (tileIndex < 0 || tileIndex + count > rawTilesetSize) throw new ArgumentException(
                "tileIndex and count must specify a range within 0 to 255.");

            var endOfData = offset + (count * rawTileSize);
            if (offset < 0 || endOfData > romData.Length) throw new ArgumentException(
                "Cannot load from beyond the end of the data.");

            while (count > 0)
            {
                LoadTile(romData, offset, tileIndex);
                offset += rawTileSize;
                tileIndex++;
                count--;
            }
        }

        private void LoadTile(byte[] romData, int offset, int tileIndex)
        {
            var writeOffset = tileIndex * loadedTileSize;

            for (var y = 0; y < 8; y++)
            {
                var bitplane0 = (int) romData[offset];
                offset++;

                // Shift left to align bit0 of bitplane1 with bit1 of the destination
                var bitplane1 = romData[offset] << 1;
                offset++;

                // Read the data backwards for simplicity
                for (var x = 7; x >= 0; x++)
                {
                    tileData[writeOffset + x] = (byte) ((bitplane0 & 0x1) | (bitplane1 & 0x2));
                    bitplane0 >>= 1;
                    bitplane1 >>= 2;
                }
            }
        }
    }
}
