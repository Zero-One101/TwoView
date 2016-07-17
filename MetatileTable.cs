using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoView
{
    /// <summary>
    /// Holds a table of 128 16x16 metatiles, composed of 4 8x8 tiles
    /// </summary>
    class MetatileTable
    {
        private const int tableSize = 128;
        private readonly Metatile[] metatiles = new Metatile[tableSize];

        /// <summary>
        /// Generates the metatile table from ROM
        /// </summary>
        /// <param name="data">The ROM data</param>
        /// <param name="offset">The offset in the ROM to read from</param>
        public void Read(byte[] data, int offset)
        {
            for (var i = 0; i < tableSize; i++)
            {
                metatiles[i].Read(data, offset);
                offset += Metatile.Size;
            }
        }

        /// <summary>
        /// Writes the metatile table to the ROM
        /// </summary>
        /// <param name="data">The ROM data</param>
        /// <param name="offset">The offset in the ROM to write to</param>
        public void Write(byte[] data, int offset)
        {
            for (var i = 0; i < tableSize; i++)
            {
                metatiles[i].Write(data, offset);
                offset += Metatile.Size;
            }
        }

        /// <summary>
        /// Returns a metatile specified by an index
        /// </summary>
        /// <param name="index">The index of the metatile</param>
        /// <returns>The metatile specified by the index</returns>
        public Metatile GetMetatile(int index)
        {
            return metatiles[index];
        }

        /// <summary>
        /// Sets a metatile in the table, specified by an index
        /// </summary>
        /// <param name="index">The index of the metatile</param>
        /// <param name="metatile">The metatile value to set the index to</param>
        public void SetMetatile(int index, Metatile metatile)
        {
            metatiles[index] = metatile;
        }
    }

    /// <summary>
    /// Holds a single 16x16 metatile, composed of 4 8x8 tiles
    /// </summary>
    struct Metatile
    {
        public const int Size = 4;

        private byte tl, tr, bl, br;

        /// <summary>
        /// Returns the value of a specified index
        /// </summary>
        /// <param name="index">The index of the 8x8 tile, where 0 is top left and 1 is top right</param>
        /// <returns>The value of the index</returns>
        private byte GetByte(int index)
        {
            switch (index)
            {
                case 0:
                    return tl;
                case 1:
                    return tr;
                case 2:
                    return bl;
                case 3:
                    return br;
                default:
                    throw new ArgumentException("Specified index is out of range.");
            }
        }

        /// <summary>
        /// Sets the index to the specified value
        /// </summary>
        /// <param name="index">The index of the 8x8 tile, where 0 is top left and 1 is top right</param>
        /// <param name="value"></param>
        private void SetByte(int index, byte value)
        {
            switch (index)
            {
                case 0:
                    tl = value;
                    break;
                case 1:
                    tr = value;
                    break;
                case 2:
                    bl = value;
                    break;
                case 3:
                    br = value;
                    break;
                default:
                    throw new ArgumentException("Specified index is out of range.");
            }
        }

        /// <summary>
        /// Read data from the ROM into a metatile
        /// </summary>
        /// <param name="data">The ROM data</param>
        /// <param name="offset">The position in the ROM at which the data begins</param>
        public void Read(byte[] data, int offset)
        {
            for (var i = 0; i < Size; i++)
            {
                SetByte(i, data[offset + i]);
            }
        }

        /// <summary>
        /// Write metatile data to the ROM
        /// </summary>
        /// <param name="data">Th ROM data</param>
        /// <param name="offset">The position in the ROM at which the data begins</param>
        public void Write(byte[] data, int offset)
        {
            for (var i = 0; i < Size; i++)
            {
                data[offset + i] = GetByte(i);
            }
        }
    }
}
