﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Geometry.Layouter
{

    public class ArchimedeanSpiral : ISpiral
    {
        private const double GrowthRate = 1;
        private const double DistBetweenCoils = 1;
        private const double StepTheta = 0.1;
        private readonly double strainOnX;
        private readonly double strainOnY;
        private IEnumerator<PointF> points;
        public Point Center { get; }

        public ArchimedeanSpiral(double strainOnX=1, double strainOnY=1)
        {
            Center = Point.Empty;
            this.strainOnX = strainOnX;
            this.strainOnY = strainOnY;
            points = GetPoints();
        }

        private IEnumerator<PointF> GetPoints()    
        {
            yield return Center;
            var theta = 0.0;
            while (true)
            {
                theta += StepTheta;
                var angle = 0.1 * theta;
                var x = (float)(((DistBetweenCoils + GrowthRate * angle) * Math.Cos(angle) + Center.X) * strainOnX);
                var y = (float)(((DistBetweenCoils + GrowthRate * angle) * Math.Sin(angle) + Center.Y) * strainOnY);
                yield return new PointF(x, y);
            }
        }

        public PointF GetNextPoint()
        {
            points.MoveNext();
            return points.Current;
        }

        public void Restart() => points = GetPoints();

    }
}
