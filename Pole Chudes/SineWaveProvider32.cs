using System;

namespace Pole_Chudes
{
    internal class SineWaveProvider32
    {
        public SineWaveProvider32()
        {
        }

        public int Frequency { get; internal set; }
        public float Amplitude { get; internal set; }

        internal void SetWaveFormat(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}