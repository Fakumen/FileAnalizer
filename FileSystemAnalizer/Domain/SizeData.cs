using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public class SizeData
    {
        public enum ByteUnit : long
        {
            Byte = 1,
            Kilobyte = Byte << 10,
            Megabyte = Byte << 20,
            Gigabyte = Byte << 30,
            Terabyte = Byte << 40
        }

        public readonly long SizeInBytes;
        public static SizeData None = new SizeData(0);

        public ByteUnit BestFittingUnits
        {
            get
            {
                if (SizeInBytes == 0)
                    return ByteUnit.Byte;
                return new[] { ByteUnit.Byte, ByteUnit.Kilobyte, ByteUnit.Megabyte, ByteUnit.Gigabyte, ByteUnit.Terabyte }
                .Where(byteUnit => SizeInBytes >= (long)byteUnit)
                .Max();
            }
        }

        public double GetInUnits(ByteUnit units) => (double)SizeInBytes / (long)units;

        public SizeData(long sizeInBytes)
        {
            if (sizeInBytes < 0)
                throw new ArgumentException();
            SizeInBytes = sizeInBytes;
        }
    }
}