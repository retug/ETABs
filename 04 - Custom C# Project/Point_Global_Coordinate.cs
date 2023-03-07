using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GetSelectedObjects
{
    public class ETABS_Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        //This is the constructor, redefine the point?
        /*public ETABS_Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        */
        
    }

    public class MyPoint
    {
        //changed this to xyz from XYZ
        public List<double> xyz { get; set; }
        //public List<double> refPnt { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; } 

        public List<double> LocalCoords { get; set; }
        public List<double> GlobalCoords { get; set; }
        double[] part1 { get; set;}


        //This is the constructor, redefine the point?

        public MyPoint(List<double> xyz)
        {
            X = xyz[0];
            Y = xyz[1];
            Z = xyz[2];
        }
        public void glo_to_loc(GlobalCoordinateSystem globalCoords)
        {
            double[] part1 = new double[] { X - globalCoords.RefPnt[0], Y - globalCoords.RefPnt[1], Z - globalCoords.RefPnt[2] };


            //the class will now have new attribute of local coordinates point.LocalCoords[0] = the X local coordinate system
            LocalCoords = new List<double>() { globalCoords.R_Inv[0, 0] * part1[0] + globalCoords.R_Inv[0, 1] * part1[1] + globalCoords.R_Inv[0, 2] * part1[2] ,
            globalCoords.R_Inv[1, 0] * part1[0] + globalCoords.R_Inv[1, 1] * part1[1] + globalCoords.R_Inv[1, 2] * part1[2],
            globalCoords.R_Inv[2, 0] * part1[0] + globalCoords.R_Inv[2, 1] * part1[1] + globalCoords.R_Inv[2, 2] * part1[2]};
        }
        public void loc_to_glo(GlobalCoordinateSystem globalCoords)
        {
            //this is the ref point
            double[] part1 = new double[] { X, Y, Z };



            //the class will now have new attribute of local coordinates point.LocalCoords[0] = the X local coordinate system
            GlobalCoords = new List<double>() { (globalCoords.R[0, 0] * X + globalCoords.R[0, 1] * Y + globalCoords.R[0, 2] * Z) + globalCoords.RefPnt[0],
            (globalCoords.R[1, 0] * X + globalCoords.R[1, 1] * Y + globalCoords.R[1, 2] * Z) + globalCoords.RefPnt[1],
            (globalCoords.R[2, 0] * X + globalCoords.R[2, 1] * Y + globalCoords.R[2, 2] * Z) + 0}; //unsure why this the way to do this. review in the future. should be the line below, without the 0
            //(globalCoords.R[2, 0] * X + globalCoords.R[2, 1] * Y + globalCoords.R[2, 2] * Z) + globalCoords.RefPnt[2]};
        }
    }
    public class GlobalCoordinateSystem
    {
        public List<double> RefPnt { get; set; }
        public List<double> Vector { get; set; }
        public double hyp { get; set; }
        public double[,] R { get; set; }
        public string inverseMatrixText { get; set; }
        public Matrix<double> R_Matrix { get; set; }
        public double[,] R_Inv { get; set; }
        //This is the constructor, redefine the point?
        public GlobalCoordinateSystem(List<double> xyz, List<double> vector)
        {
            RefPnt = xyz;
            hyp = Math.Sqrt((vector[0] * vector[0] + vector[1] * vector[1]));
            Vector = vector;
            R = new double[,] { { vector[0] / hyp, -vector[1] / hyp, 0 }, { vector[1] / hyp, vector[0] / hyp, 0 }, { 0, 0, 1 } };
            R_Matrix = Matrix<double>.Build.DenseOfArray(R);
            R_Inv = R_Matrix.Inverse().ToArray();
        }
    }
}
