using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AuthorizeNet
{
    /// <summary>
    /// Source Code from MSDN article http://msdn.microsoft.com/en-us/magazine/cc163367.aspx
    /// </summary>
    public class CryptoRandom
    {
        private RNGCryptoServiceProvider _rng =
        new RNGCryptoServiceProvider();
        private byte[] _uint32Buffer = new byte[4];

        public CryptoRandom() { }
        public CryptoRandom(Int32 ignoredSeed) { }

        public Int32 Next()
        {
            _rng.GetBytes(_uint32Buffer);
            return BitConverter.ToInt32(_uint32Buffer, 0) & 0x7FFFFFFF;
        }

        public Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return Next(0, maxValue);
        }

        public Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                _rng.GetBytes(_uint32Buffer);
                UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        public double NextDouble()
        {
            _rng.GetBytes(_uint32Buffer);
            UInt32 rand = BitConverter.ToUInt32(_uint32Buffer, 0);
            return rand / (1.0 + UInt32.MaxValue);
        }

        public void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            _rng.GetBytes(buffer);
        }
    }
}
