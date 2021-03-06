﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projector
{
    class NumberFlow
    {
        private List<double> numbers = new List<double>();
        private List<string> labels = new List<string>();
        private double maxValue = 1;
        private double minValue = 0;
        private int maxEntrys = 100;
        private int readerPos = 0;


        public int Count = 0;
        public int MaxOutput = 100;
        public int getmaxDivisor = 2;


        public void Clear()
        {
            this.numbers.Clear();
            this.labels.Clear();
            Count = 0;
            MaxOutput = 100;
            getmaxDivisor = 2;
            maxValue = 1;
            minValue = 0;
        }

        public double getmax()
        {
            return maxValue;
        }

        public double getMin()
        {
            return minValue;
        }

        private void findMax()
        {
           maxValue = numbers.Max();
        }

        private void findMin()
        {
            minValue = numbers.Min();
        }

        public void add(double Number)
        {
            this.add(Number, "");
        }

        public void add(double Number, string label)
        {
            if (numbers.Count >= maxEntrys)
            {
                //if (numbers[0] == maxValue) findMax();                
                //if (numbers[0] == minValue) findMin();

                numbers.RemoveAt(0);
                labels.RemoveAt(0);
                findMax();
                findMin();
            }

            numbers.Add(Number);
            labels.Add(label);
            if (Number > maxValue) maxValue = Number;
            if (Number < minValue) minValue = Number;
            Count = numbers.Count;
        }


        public double getAtMax(int pos)
        {
            if (pos >= 0 && pos < numbers.Count)
            {
                Double Max = maxValue;
                if (Math.Abs(minValue) > Max) Max = Math.Abs(minValue);

                Double val = numbers[pos];
                Double faktor = MaxOutput / Max;
                val =  val * faktor;
                return val / getmaxDivisor;
            }
            else return 0;
        }


        public double getAt(int pos)
        {
            if (pos >= 0 && pos < numbers.Count) return numbers[pos];
            else return 0;
        }

        public string getLabelAt(int pos)
        {
            if (pos >= 0 && pos < labels.Count) return labels[pos];
            else return "";
        }

        public string getCurrentLabel()
        {
            return getLabelAt(readerPos);
        }

        public double read()
        {
            if (readerPos >= 0 && readerPos < numbers.Count)
            {
                return numbers[readerPos];
            }
            else return 0;
        }

        public double next()
        {
            if (readerPos<maxEntrys) readerPos++;
            return read();
        }

        public double prev()
        {
            if (readerPos>0) readerPos--;
            return read();
        }

    }
}
