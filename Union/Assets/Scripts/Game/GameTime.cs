﻿namespace Union.Services.Game
{
    public class GameTime
    {
        public float playTime { get; private set; }

        public void UpdatePlayTime(float addTime)
        {
            this.playTime += addTime;
        }
    }
}