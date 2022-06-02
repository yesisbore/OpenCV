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
# imageSrc01 = "C:/OpenCV/Image/one.jpg"
# imageSrc02 = "C:/OpenCV/Image/two.jpg"
# imageSrc03 = "C:/OpenCV/Image/three.jpg"
# imageSrc04 = "C:/OpenCV/Image/four.jpg"
#
# one = cv2.imread(imageSrc01)
# two = cv2.imread(imageSrc02)
# three = cv2.imread(imageSrc03)
# four = cv2.imread(imageSrc04)
#
# horizontal1 = np.full((50, one.shape[1],3),[0,0,0], dtype=np.uint8)
# horizontal2 = np.full((50, two.shape[1],3),[0,0,0], dtype=np.uint8)
#
# left = cv2.vconcat((one,horizontal1,three))
# right = np.vstack((two,horizontal2,four))
#
# vertical = np.full((left.shape[0],50,3),0,dtype=np.uint8)
#
# dst = cv2.hconcat((left,vertical,right))
#
# cv2.imshow("dst",dst)
# cv2.waitKey()
# cv2.destroyAllWindows()

# 도형 그리기
# img = np.zeros((768,1366,3),dtype= np.uint8)
#
# cv2.line(img,(100,100), (1200,100), (0,0,255), 3, cv2.LINE_AA)
# cv2.circle(img, (300,300), 50, (0,255,0), cv2.FILLED, cv2.LINE_4)
# cv2.rectangle(img, (500,200), (1000,400), (255,0,0), 5, cv2.LINE_8)
# cv2.ellipse(img, (1200,300), (100,50), 0, 90, 180, (255,255,0),2 )
#
# pts1 = np.array([[[100,500],[300,500],[200,600]],[[400,500],[500,500],[600,700]]])
# pts2 = np.array([[700,500],[800,500],[700,600]])
# cv2.polylines(img,pts1,True,(0,255,255),2)
# cv2.fillPoly(img,[pts2],(255,0,255),cv2.LINE_AA)
#
# cv2.putText(img, "OpenCV", (900,600), cv2.FONT_HERSHEY_COMPLEX | cv2.FONT_ITALIC, 2, (255,255,255),3)
#
# cv2.imshow("Img",img)
# cv2.waitKey()
# cv2.destroyAllWindows()

# 트랙바
windowName = "Pallete"

def onChangeBlue(pos):
    global bool
    b = pos
    cv2.imshow(windowName,createImage(b,g,r))

def createImage(b,g,r):
    return np.full((500,500,3),(b,g,r),dtype=np.uint8)

b,g,r = 0,0,0
cv2.namedWindow(windowName)
cv2.createTrackbar("Blue",windowName,55,255,onChangeBlue)
cv2.createTrackbar("Green",windowName,0,255,lambda x:x)
cv2.createTrackbar("Red",windowName,0,255,lambda x:x)

while True:
    g = cv2.getTrackbarPos("Green",windowName)
    r = cv2.getTrackbarPos("Red",windowName)

    cv2.imshow(windowName,createImage(b,g,r))
    if cv2.waitKey(33) & 0xFF == ord('q'):
        break

cv2.destroyAllWindows()

