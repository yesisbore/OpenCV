using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Example01
{
    internal class Image
    {

        static void Main(string[] args)
        {
            //MouseCallBack();
            //Video();
            //Camera();
            //ImageMerge();
            //DrawShape();
            TrackBar();
        }

        private static void ImageShow()
        {
            const string filePath = "C:/OpenCV/Image/Image01.png";
            const string windowName = "src";

            Mat src = Cv2.ImRead(filePath, ImreadModes.ReducedColor2);

            // 윈도우 생성
            Cv2.NamedWindow(windowName, WindowFlags.GuiExpanded);
            // 윈도우 속성 0 Normal , 1은 full
            Cv2.SetWindowProperty(windowName, WindowPropertyFlags.Fullscreen, 0);
            Cv2.ImShow(windowName, src);
            Cv2.WaitKey(0);
            Cv2.DestroyWindow(windowName);
        }

        private static void MouseCallBack()
        {
            var size = new Size(500,500);
            var scalar = new Scalar(255,255,255);
            Mat src = new Mat(size,MatType.CV_8UC3,scalar);

            Cv2.ImShow("draw",src);
            MouseCallback cvMouseCallback = new MouseCallback(Event);
            Cv2.SetMouseCallback("draw",cvMouseCallback,src.CvPtr);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }
        private static void Event(MouseEventTypes @event, int x,int y, MouseEventFlags flags, IntPtr userData)
        {
            Mat data = new Mat(userData);

            if(flags == MouseEventFlags.LButton)
            {
                Cv2.Circle(data,new Point(x,y),10,new Scalar(0,0,255),-1);
                Cv2.ImShow("draw",data);
            }
        }

        private static void Video()
        {
            var videoSrc = "C:/OpenCV/Video/TestVideo01.mp4";
            VideoCapture capture = new VideoCapture(videoSrc);
            Mat frame = new Mat();  

            while(true)
            {
                if(capture.PosFrames == capture.FrameCount) capture.Open(videoSrc);

                capture.Read(frame);
                Cv2.ImShow("VideoFrame",frame);

                if(Cv2.WaitKey(33) == 'q') break;
            }
            capture.Release();
            Cv2.DestroyAllWindows();
        }

        private static void Camera()
        {
            VideoCapture capture = new VideoCapture(0);  
            Mat frame = new Mat();
            capture.Set(VideoCaptureProperties.FrameWidth, 640);
            capture.Set(VideoCaptureProperties.FrameHeight, 480);

            while (true)
            {
                if(capture.IsOpened() == true)
                {
                    capture.Read(frame);
                    Cv2.ImShow("VideoFrame",frame);
                    if(Cv2.WaitKey(33) == 'q') break;
                }
            }

            capture.Release();
            Cv2.DestroyAllWindows();
        }

        private static void ImageMerge()
        {
            var imageSrc01 = "C:/OpenCV/Image/one.jpg";
            var imageSrc02 = "C:/OpenCV/Image/two.jpg";
            var imageSrc03 = "C:/OpenCV/Image/three.jpg";
            var imageSrc04 = "C:/OpenCV/Image/four.jpg";
            Mat one = new Mat(imageSrc01);
            Mat two = new Mat(imageSrc02);
            Mat three = new Mat(imageSrc03);
            Mat four = new Mat(imageSrc04);

            Mat left = new Mat();
            Mat right = new Mat();
            Mat dst = new Mat();

            Cv2.VConcat(new Mat[] {one, three}, left);
            Cv2.VConcat(new Mat[] {two, four}, right);
            Cv2.HConcat(new Mat[] {left, right}, dst);

            Cv2.ImShow("dst",dst);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        private static void DrawShape()
        {
            Mat img = new Mat(new Size(1366,768),MatType.CV_8UC3);

            Cv2.Line(img,new Point(100,100),new Point(1200,100),new Scalar(0,0,255),3,LineTypes.AntiAlias);
            Cv2.Circle(img,new Point(300,300),50,new Scalar(0,255,0),Cv2.FILLED,LineTypes.Link4);
            Cv2.Rectangle(img,new Point(500,200),new Point(1000,400),new Scalar(255,0,0),5);
            Cv2.Ellipse(img,new Point(1200,300),new Size(100,50),0,90,180,new Scalar(255,255,0),2);

            List<List<Point>> pts1 = new List<List<Point>>();   
            List<Point> pt1 = new List<Point>()
            {
                new Point(100,500),
                new Point(300,500),
                new Point(200,600)
            };    

            List<Point> pt2 = new List<Point>()
            {
                new Point(400,500),
                new Point(500,500),
                new Point(600,700),
                new Point(500,650),
            };
            pts1.Add(pt1);
            pts1.Add(pt2);
            Cv2.Polylines(img,pts1,true,new Scalar(0,255,255),2);

            Point[] pt3 = new Point[]
            {
                new Point(700,500),
                new Point(800,500),
                new Point(700,600)
            };
            Point[][] pts2 = new Point[][] {pt3};
            Cv2.FillPoly(img,pts2,new Scalar(255,0,255),LineTypes.AntiAlias);

            Cv2.PutText(img,"OpenCV",new Point(900,600),HersheyFonts.HersheyComplex 
                | HersheyFonts.Italic,2.0,new Scalar(255,255,255),3);
            
            Cv2.ImShow("Img",img);
            Cv2.WaitKey();
            Cv2.DestroyAllWindows();
        }

        private static void TrackBar()
        {
            int value = 0;
            string windowName = "Palette";
            Cv2.NamedWindow(windowName);
            Cv2.CreateTrackbar("Color",windowName,ref value,255);

            while (true)
            {
                int pixel = Cv2.GetTrackbarPos("Color",windowName);
                Mat src = new Mat(new Size(500,500),MatType.CV_8UC3,new Scalar(pixel,value,value));

                Cv2.ImShow("Pallete",src);
                if(Cv2.WaitKey(33)== 'q') break;
            }
            Cv2.DestroyAllWindows();
        }
    }
}