using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroidsRemixed.Etc
{
    public class WaveCalculator
    {
        private const int BOSSEVERY = 5;

        private Random _rnd = new Random();

        public int Rocks { get; private set; }
        public bool BossWave { get; private set; } = false;
        
        public WaveCalculator(int waveNumber)
        {
            if (waveNumber % BOSSEVERY == 0)
                BossWave = true;

            if (!BossWave)
            {
                int difficultyModifier = -_rnd.Next(waveNumber, ((waveNumber * 10) / 2));
                Rocks = (waveNumber * 10) + difficultyModifier;
            }
        }
    }
}
