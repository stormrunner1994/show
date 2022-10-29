﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningDots
{
    public class Verlauf
    {
        List<GenInfo> genInfos = new List<GenInfo>();
        int populationSize;

        public Verlauf(int populationSize)
        {
            this.populationSize = populationSize;
        }

        public void Clear()
        {
            genInfos.Clear();
        }

        public GenInfo GetLastGenInfo()
        {
            if (genInfos.Count == 0) return null;
            return genInfos.Last();
        }

        public void AddGenInfo(double avgFitness, double worstFitness, double bestFitness, int dead, int reachedGoal,
            int maxSteps)
        {
            genInfos.Add(new GenInfo(genInfos.Count, populationSize, avgFitness, worstFitness, bestFitness, dead, reachedGoal, maxSteps));
        }

        public List<GenInfo> GetGenInfos()
        {
            return genInfos;
        }
    }

    public class GenInfo
    {
        private int dead = -1;
        private int reachedGoal = -1;
        private int iGen = -1;
        private double worstFitness = -1;
        private double bestFitness = -1;
        private double diffFitness = -1;
        private int populationSize = 0;
        private double avgFitness = -1;
        private int diffFitnessPercentage = -1;
        private int maxSteps = 0;
        // wie oft wurd eine Rang als Parent ausgewählt
        private Dictionary<int, int> dictRanksChosenAsParent = new Dictionary<int, int>();

        public GenInfo(int iGen, int populationSize, double avgFitness, double worstFitness, double bestFitness
            , int dead, int reachedGoal, int maxSteps)
        {
            this.maxSteps = maxSteps;
            this.dead = dead;
            this.reachedGoal = reachedGoal;
            this.iGen = iGen;
            this.avgFitness = avgFitness;
            this.populationSize = populationSize;
            for (int a = 0; a < populationSize; a++)
                dictRanksChosenAsParent.Add(a + 1, 0);
            setFitnessRange(worstFitness, bestFitness);
        }


        public void setFitnessRange(double worstFitness, double bestFitness)
        {
            this.worstFitness = worstFitness;
            this.bestFitness = bestFitness;
            diffFitness = Math.Abs(bestFitness - worstFitness);
            diffFitnessPercentage = (int)(((bestFitness / worstFitness) - 1) * 100);
        }

        public void RankChosen(int rank)
        {
            dictRanksChosenAsParent[rank]++;
        }

        public string GetInfo()
        {
            return "Gen: " + (iGen +1) + ":\nBest: " + bestFitness + "\nWorst: " +
                worstFitness + "\nAvg: " + avgFitness + "\nDiff%: " + diffFitnessPercentage+ "\nDead: " + dead
                + "\nReachedGoal: " + reachedGoal + "\nChosenRank: " + GetChosenRankRatio()
                + "\nMaxSteps: " + maxSteps;
        }

        private double GetChosenRankRatio()
        {
            double ratio = 0;
            int count = 0;

            foreach(var pair in dictRanksChosenAsParent)
            {
                ratio += pair.Key * pair.Value;
                count += pair.Value;
            }

            return ratio/count;
        }

    }

}
