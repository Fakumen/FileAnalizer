using FileSystemAnalizer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Domain
{
    public class WeightData
    {
        public enum ByteUnit : long
        {
            [DisplayedName("B")]
            Byte = 1,
            [DisplayedName("kB")]
            Kilobyte = Byte << 10,
            [DisplayedName("MB")]
            Megabyte = Byte << 20,
            [DisplayedName("GB")]
            Gigabyte = Byte << 30,
            [DisplayedName("TB")]
            Terabyte = Byte << 40
        }

        public readonly long WeightInBytes;
        public readonly ByteUnit Units;
        public readonly double WeightInUnits;

        public WeightData(long weightInBytes)
        {
            if (weightInBytes < 0)
                throw new ArgumentException();
            WeightInBytes = weightInBytes;
            Units = ByteUnit.Byte;
            if (weightInBytes > 0)
            {
                Units = new[] { ByteUnit.Byte, ByteUnit.Kilobyte, ByteUnit.Megabyte, ByteUnit.Gigabyte, ByteUnit.Terabyte }
                .Where(byteUnit => weightInBytes >= (long)byteUnit)
                .Max();
            }
            WeightInUnits = (double)weightInBytes / (long)Units;
        }

        public WeightData(long weightInBytes, ByteUnit desiredUnits)
        {
            WeightInBytes = weightInBytes;
            Units = desiredUnits;
            WeightInUnits = (double)weightInBytes / (long)desiredUnits;
        }
    }
}
