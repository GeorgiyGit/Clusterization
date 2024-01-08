using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClusteringAlgorithms
{
    public class MyDBSCANClustering
    {
        public double eps;
        public int minPts;
        public double[][] data;  // supplied in cluster()
        public int[] labels;  // supplied in cluster()

        public MyDBSCANClustering(double eps, int minPts)
        {
            this.eps = eps;
            this.minPts = minPts;
        }

        public int[] Cluster(double[][] data)
        {
            this.data = data;  // by reference
            this.labels = new int[this.data.Length];
            for (int i = 0; i < labels.Length; ++i)
                this.labels[i] = -2;  // unprocessed

            int cid = -1;  // offset the start
            for (int i = 0; i < this.data.Length; ++i)
            {
                if (this.labels[i] != -2)  // has been processed
                    continue;

                List<int> neighbors = this.RegionQuery(i);
                if (neighbors.Count < this.minPts)
                {
                    this.labels[i] = -1;  // noise
                }
                else
                {
                    ++cid;
                    this.Expand(i, neighbors, cid);
                }
            }

            return this.labels;
        }

        private List<int> RegionQuery(int p)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < this.data.Length; ++i)
            {
                double dist = EucDistance(this.data[p], this.data[i]);
                if (dist < this.eps)
                    result.Add(i);
            }
            return result;
        }

        private void Expand(int p, List<int> neighbors, int cid)
        {
            this.labels[p] = cid;
            for (int i = 0; i < neighbors.Count; ++i)
            {
                int pn = neighbors[i];
                if (this.labels[pn] == -1)  // noise
                    this.labels[pn] = cid;
                else if (this.labels[pn] == -2)  // unprocessed
                {
                    this.labels[pn] = cid;
                    List<int> newNeighbors = this.RegionQuery(pn);
                    if (newNeighbors.Count >= this.minPts)
                        neighbors.AddRange(newNeighbors);
                }
            } // for
        }

        private static double EucDistance(double[] x1, double[] x2)
        {
            int dim = x1.Length;
            double sum = 0.0;
            for (int i = 0; i < dim; ++i)
                sum += (x1[i] - x2[i]) * (x1[i] - x2[i]);
            return Math.Sqrt(sum);
        }
    }
}
