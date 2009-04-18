using System;

namespace FizzWare.NBuilder
{
    public interface IRandomGenerator
    {
        ushort Next(ushort min, ushort max);
        uint Next(uint min, uint max);
        ulong Next(ulong min, ulong max);

        short Next(short min, short max);
        int Next(int min, int max);
        long Next(long min, long max);

        float Next(float min, float max);
        double Next(double min, double max);
        decimal Next(decimal min, decimal max);

        char Next(char min, char max);
        byte Next(byte min, byte max);

        DateTime Next(DateTime min, DateTime max);

        bool Next();

        // TODO: Add NextString() to this interface
        //string NextString(int minLength, int maxLength);
    }
}