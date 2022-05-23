using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Example01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Cv2.GetVersionString());

            // 이미지 크기 표현 
            //Mat color = new Mat(new Size(width,height),MatType.cv_8uc3);
            //Mat gray = new Mat(rows,cols,MatType.cv_8uc1);

            Console.WriteLine("벡터 이용하기"); // 벡터 이용하기 
            Vec4d vector1 = new Vec4d(1.0, 2.0, 3.0, 4.0);
            Vec4d vector2 = new Vec4d(1.0, 2.0, 3.0, 4.0);

            Console.WriteLine(vector1.Item0);
            Console.WriteLine(vector1[1]);
            Console.WriteLine(vector1.Equals(vector2));

            Console.WriteLine();
            Console.WriteLine("포인터 이용하기");
            Vec3d Vector = new Vec3d(1.0,2.0,3.0);
            Point3d pt1 = new Vec3d(1.0, 2.0, 3.0);
            Point3d pt2 = Vector;

            Console.WriteLine(pt1);
            Console.WriteLine(pt2);
            Console.WriteLine(pt1.X);

            Console.Read();
        }
    }
}
