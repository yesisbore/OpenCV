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
        const string imgPath01 = "C:/OpenCV/Image/Image01.png";
        const string imgPath02 = "C:/OpenCV/Image/Image02.png";
        const string imgPath03 = "C:/OpenCV/Image/Image03.png";
        const string imgPath04 = "C:/OpenCV/Image/Image04.png";
        const string apple = "C:/OpenCV/Image/apple.jpg";

        static void Main(string[] args)
        {
            //MouseCallBack();
            //Video();
            //Camera();
            //ImageMerge();
            //DrawShape();
            //TrackBar();
            //ImageTransform();
            //HueDetection();
            //Binary();
            //Blur();
            //ImageTransformation();
            //Rotate();
            //AffineTransform();
            MorpDilate();
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

        private static void ImageTransform()
        {
            Mat src = Cv2.ImRead(imgPath01);
            Mat dst = new Mat(src.Size(),MatType.CV_8UC1);
            Cv2.CvtColor(src,dst,ColorConversionCodes.BGR2GRAY);
            
            Cv2.ImShow("dst",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void HueDetection()
        {
            Mat src = Cv2.ImRead(apple);
            Mat hsv = new Mat(src.Size(),MatType.CV_8UC3);
            Mat lowerRed = new Mat(src.Size(),MatType.CV_8UC3);
            Mat upperRed = new Mat(src.Size(),MatType.CV_8UC3);
            Mat addedRed = new Mat(src.Size(),MatType.CV_8UC3);
            Mat dst = new Mat(src.Size(),MatType.CV_8UC3);

            Cv2.CvtColor(src,hsv,ColorConversionCodes.BGR2HSV);

            Cv2.InRange(hsv, new Scalar(0,100,100), new Scalar(5,255,255),lowerRed);
            Cv2.InRange(hsv, new Scalar(170,100,100), new Scalar(179,255,255),upperRed);
            Cv2.AddWeighted(lowerRed,1.0,upperRed,1.0,0.0,addedRed);

            Cv2.BitwiseAnd(hsv,hsv,dst,addedRed);
            Cv2.CvtColor(dst,dst,ColorConversionCodes.HSV2BGR);

            Cv2.ImShow("HueDetection",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void Binary()
        {
            Mat src = Cv2.ImRead(apple);
            Mat gray = new Mat (src.Size(),MatType.CV_8UC1);
            Mat binary = new Mat (src.Size(),MatType.CV_8UC1);

            Cv2.CvtColor(src, gray,ColorConversionCodes.BGR2GRAY);
            //Cv2.Threshold(gray,binary,127,255,ThresholdTypes.Otsu);
            Cv2.AdaptiveThreshold(gray,binary,255,AdaptiveThresholdTypes.GaussianC,ThresholdTypes.Binary,25,5);

            Cv2.ImShow("Binary",binary);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void Blur()
        {
            Mat src = Cv2.ImRead(apple);
            Mat dst = new Mat(src.Size(),MatType.CV_8UC3);

            Cv2.GaussianBlur(src,dst,new Size(9,9),3,3,BorderTypes.Isolated);

            Cv2.ImShow("dst",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void ImageTransformation()
        {
            Mat src = Cv2.ImRead(imgPath03);
            Mat dst = new Mat (src.Size(), MatType.CV_8UC3);

            Cv2.PyrUp(src,dst,new Size(src.Width*2+1, src.Height*2-1));

            Cv2.ImShow("dst",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void Rotate()
        {
            Mat src = Cv2.ImRead(apple);
            Mat dst = new Mat();

            Cv2.Flip(src,dst,FlipMode.Y);
            Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(src.Width/2,src.Height/2),90.0,1.0);

            Cv2.WarpAffine(dst,dst,matrix,new Size(src.Width,src.Height));

            Cv2.ImShow("dst",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void AffineTransform()
        {
            Mat src = Cv2.ImRead(apple);
            Mat dst = new Mat();

            List<Point2f> srcPts = new List<Point2f>()
            {
                new Point2f(0.0f,0.0f),
                new Point2f(0.0f,src.Height),
                new Point2f(src.Width,src.Height)
            };
            List<Point2f> dstPts = new List<Point2f>()
            {
                new Point2f(300.0f,300.0f),
                new Point2f(300.0f,src.Height),
                new Point2f(src.Width - 400.0f,src.Height- 200.0f)
            };
            Mat M = Cv2.GetAffineTransform(srcPts,dstPts);

            Cv2.WarpAffine(
                src,dst,M,new Size(src.Width,src.Height),
                borderValue: new Scalar(127,127,127,0));

            Cv2.ImShow("dst",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        private static void MorpDilate()
        {
            Mat src = Cv2.ImRead(apple,ImreadModes.Grayscale);
            Mat dst = new Mat();

            Mat kernal = Cv2.GetStructuringElement(MorphShapes.Cross,new Size(7,7));
            Cv2.Dilate(src,dst,kernal,new Point(-1,-1),3,BorderTypes.Reflect101,new Scalar(0));

            Cv2.ImShow("dst",dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}