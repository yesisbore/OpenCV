import cv2
import numpy as np

# 기초
# print(cv2.__version__)

# olor = np.zeros((height,width,3),np.unit8)
# gray = np.zeros((rows,cols,1),np.unit8)

# arr1 = np.arange(12)
# arr2 = np.arange(6,12).reshape(2,3)

# print(arr1)
# print(arr2)


# filename = "C:/OpenCV/Image/Image01.png"
# windowName = "src"
# src = cv2.imread(filename,cv2.IMREAD_GRAYSCALE)
#
# cv2.namedWindow(windowName,flags=cv2.WINDOW_FREERATIO)
# cv2.resizeWindow(windowName,400,200)
# cv2.imshow(windowName,src)
# cv2.waitKey(0)
# cv2.destroyWindow(windowName)

# 마우스 콜백
# def mouse_event(event, x, y, flags, param):
#     global radius
#
#     if event == cv2.EVENT_LBUTTONDOWN:
#         cv2.circle(param, (x, y), radius, (255, 0, 0), 2)
#         cv2.imshow("draw", src)
#
#     elif event == cv2.EVENT_MOUSEWHEEL:
#         if flags > 0:
#             radius += 1
#         elif radius > 1:
#             radius -= 1
#
#
# radius = 3
#
# src = np.full((500, 500, 3), 255, dtype=np.uint8)
#
# cv2.imshow("draw",src)
# cv2.setMouseCallback("draw", mouse_event, src)
# cv2.waitKey()
# cv2.destroyAllWindows()

# Video 출력
# videoSrc = "C:/OpenCV/Video/TestVideo01.mp4"
# capture = cv2.VideoCapture(videoSrc)
#
# while True:
#     ret,frame = capture.read()
#
#     if(capture.get(cv2.CAP_PROP_POS_FRAMES) == capture.get(cv2.CAP_PROP_FRAME_COUNT)):
#         capture.open(videoSrc)
#
#     cv2.imshow("VideoFrame",frame)
#     if cv2.waitKey(33) == ord('q'): break
#
# capture.release()
# cv2.destroyAllWindows()

# Camera
# capture = cv2.VideoCapture(0)
# capture.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
# capture.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)
#
# while cv2.waitKey(33) < 0:
#     ret, frame = capture.read()
#     cv2.imshow("VideoFrame", frame)
#
# capture.release()
# cv2.destroyAllWindows()

# 이미지 연결
imageSrc01 = "C:/OpenCV/Image/one.jpg"
imageSrc02 = "C:/OpenCV/Image/two.jpg"
imageSrc03 = "C:/OpenCV/Image/three.jpg"
imageSrc04 = "C:/OpenCV/Image/four.jpg"

one = cv2.imread(imageSrc01)
two = cv2.imread(imageSrc02)
three = cv2.imread(imageSrc03)
four = cv2.imread(imageSrc04)

horizontal1 = np.full((50, one.shape[1],3),[0,0,0], dtype=np.uint8)
horizontal2 = np.full((50, two.shape[1],3),[0,0,0], dtype=np.uint8)

left = cv2.vconcat((one,horizontal1,three))
right = np.vstack((two,horizontal2,four))

vertical = np.full((left.shape[0],50,3),0,dtype=np.uint8)

dst = cv2.hconcat((left,vertical,right))

cv2.imshow("dst",dst)
cv2.waitKey()
cv2.destroyAllWindows()