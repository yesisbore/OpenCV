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
            ImageMerge();
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
    }
}