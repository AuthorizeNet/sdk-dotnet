using System;
using System.Security.Cryptography;

namespace AuthorizeNet.Utility
{
    public class AnetRandom
    {
        private const int BufferSize = 1024;  // must be a multiple of 4
        private readonly byte[] randomBuffer;
        private int bufferOffset;
        private readonly RNGCryptoServiceProvider rngCryptoServiceProvider;
        private readonly int seed;

        public AnetRandom() : this(0)
        {
        }

        public AnetRandom(int seed)
        {
            this.seed = seed;
            randomBuffer = new byte[BufferSize];
            rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            bufferOffset = randomBuffer.Length;
        }

        private void FillBuffer()
        {
            rngCryptoServiceProvider.GetBytes(randomBuffer);
            bufferOffset = 0;
        }

        private int Next()
        {
            if (bufferOffset >= randomBuffer.Length)
            {
                FillBuffer();
            }

            // BitConverter.ToInt32 gets the next four bytes in the array and returns a 32 bit integer.
            int val = BitConverter.ToInt32(randomBuffer, bufferOffset) & 0x7fffffff;

            //this makes sure number is positive.
            bufferOffset += sizeof(int);
            return val;
        }

        // if seed is greater than or equal to max value, next() % maxValue is always less than seed.
        // if seed is less than max value, (maxValue - seed) ensures that this method result is always less than seed.
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

            var range = maxValue - minValue;
            return minValue + Next(range);
        }
    }
}
