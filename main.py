import socket
import cv2
from cvzone.HandTrackingModule import HandDetector

vw = 1280
vh = 720
ipAddy = "127.0.0.1"
ipPort = 5052

cap = cv2.VideoCapture(1)
cap.set(3, vw)  # prop 3 = width
cap.set(4, vh)  # prop 4 = height

detector = HandDetector(maxHands=1, detectionCon=0.2)

bobSocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddy = (ipAddy, ipPort)

while True:
    success, img = cap.read()
    hands, img = detector.findHands(img)


    # 21 points to transmit
    trackData = []
    if hands:
        hand = hands[0]
        lmList = hand['lmList']
        #for lm in lmList:
        #    data.extend([lm[0], vh - lm[1], lm[2]])

        lm = lmList[0]  # Only take one point
        trackData.extend([lm[0], vh - lm[1], lm[2]])

        bobSocket.sendto(str.encode(str(trackData)), serverAddy)
        print(trackData)


    cv2.imshow("Image", img)
    cv2.waitKey(1)
