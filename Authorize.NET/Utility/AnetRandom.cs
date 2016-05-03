using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizeNet.Utility
{
    public class AnetRandom
    {
        private const int BufferSize = 1024;  // must be a multiple of 4
        private readonly byte[] RandomBuffer;
        private int BufferOffset;
        private readonly RNGCryptoServiceProvider rngCryptoServiceProvider;
        private int seed;

        public AnetRandom() : this(0)
        {
        }

        public AnetRandom(int seed)
        {
            this.seed = seed;
            RandomBuffer = new byte[BufferSize];
            rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            BufferOffset = RandomBuffer.Length;
        }
        private void FillBuffer()
        {
            rngCryptoServiceProvider.GetBytes(RandomBuffer);
            BufferOffset = 0;
        }
        private int Next()
        {
            if (BufferOffset >= RandomBuffer.Length)
            {
                FillBuffer();
            }
            int val = BitConverter.ToInt32(RandomBuffer, BufferOffset) & 0x7fffffff;
            BufferOffset += sizeof(int);
            return val;
        }
        public int Next(int maxValue)
        {
            return seed >= maxValue ? Next() % maxValue : Next() % (maxValue - seed) + seed;
        }

        public int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentOutOfRangeException("maxValue must be greater than or equal to minValue");
            }

            int range = maxValue - minValue;
            return minValue + Next(range);
        }
    }
}
