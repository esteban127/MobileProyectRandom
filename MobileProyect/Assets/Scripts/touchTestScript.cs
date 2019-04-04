using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class touchTestScript : MonoBehaviour
{
    enum swipeDirection
    {
        top,
        rightTop,
        right,
        rightDown,
        down,
        leftDown,
        left,
        leftTop,
    }
    enum gestures
    {
        pull,
        push,
        clockwiseCircle,
        andtiClockwiseCircle
    }
    [SerializeField] float minSwipeDistance = 5;
    [SerializeField] Text outputText;
    List<int> idToActualizate;
    List<int> idToDelete;
    List<Swipe> swipes;
    
    void Start()
    {
        swipes = new List<Swipe>();
        idToActualizate = new List<int>();
        idToDelete = new List<int>();
    }
    void Update()
    {

        if (Input.touches.Length > 0)
        {            
            for (int i = 0; i < Input.touches.Length; i++)
            {                
                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    swipes.Add(new Swipe(Input.touches[i].position, Input.touches[i].fingerId));
                }
                if (Input.touches[i].phase == TouchPhase.Moved)
                {
                    GetSwipeByID(Input.touches[i].fingerId).End = Input.touches[i].position;
                    GetSwipeByID(Input.touches[i].fingerId).Tap = false;
                    idToActualizate.Add(Input.touches[i].fingerId);
                }
                if (Input.touches[i].phase == TouchPhase.Ended)
                {
                    GetSwipeByID(Input.touches[i].fingerId).End = Input.touches[i].position;
                    GetSwipeByID(Input.touches[i].fingerId).Ended = true;
                    idToDelete.Add(Input.touches[i].fingerId);
                }

                if (i == Input.touches.Length - 1)
                {
                    ReadAllSwipes();
                    foreach (int act in idToActualizate)
                    {
                        GetSwipeByID(act).Origin = GetSwipeByID(act).End;
                    }
                    idToActualizate.Clear();
                    foreach (int del in idToDelete)
                    {
                        swipes.Remove(GetSwipeByID(del));
                    }
                    idToDelete.Clear();
                }                
            }
        }
        
    }   

    private Swipe GetSwipeByID(int fingerID)
    {
        foreach(Swipe s in swipes)
        {
            if (s.ID == fingerID)
                return s;
        }
        throw new IndexOutOfRangeException();
    }

    private void ReadSwipe(Swipe swipe)
    {
        if(swipe.Movement().magnitude> minSwipeDistance)
        {

            switch (swipe.Direction())
            {
                case swipeDirection.top:
                    outputText.text = "top swipe";
                    break;
                case swipeDirection.rightTop:
                    outputText.text = "rightTop swipe";
                    break;
                case swipeDirection.right:
                    outputText.text = "right swipe";
                    break;
                case swipeDirection.rightDown:
                    outputText.text = "rightDown swipe";
                    break;
                case swipeDirection.down:
                    outputText.text = "down swipe";
                    break;
                case swipeDirection.leftDown:
                    outputText.text = "leftDown swipe";
                    break;
                case swipeDirection.left:
                    outputText.text = "left swipe";
                    break;
                case swipeDirection.leftTop:
                    outputText.text = "leftTop swipe";
                    break;
            }
        }
        else
        {
            if(swipe.Ended && swipe.Tap)
                outputText.text = "Tap";
            
        }
    }

    private void ReadAllSwipes()
    {
        float distance = 1000000000.0f;
        int[] closestPair = new int[2];
        List<Swipe> currentSwipes = new List<Swipe>();
        foreach (Swipe s in swipes)
        {
            if (s.Movement().magnitude > minSwipeDistance)
            {
                currentSwipes.Add(s);               
            }
        }
        if (currentSwipes.Count <= 1)
        {
            foreach (Swipe s in swipes)
            {
                ReadSwipe(s);
            }
        }            
        else
        {
            
            int counter = 1;
            for (int i = 0; i < currentSwipes.Count; i++)
            {
                for (int j = counter; j < currentSwipes.Count; j++)
                {
                    if ((currentSwipes[i].End - currentSwipes[j].End).magnitude< distance)
                    {
                        distance = (currentSwipes[i].End - currentSwipes[j].End).magnitude;
                        closestPair[0] = i;
                        closestPair[1] = j;                        
                    }                    
                }
                counter++;
            }
            if((currentSwipes[closestPair[0]].End - currentSwipes[closestPair[1]].End).magnitude < (currentSwipes[closestPair[0]].Origin - currentSwipes[closestPair[1]].Origin).magnitude)
                outputText.text = "Achicando";
            else
                outputText.text = "Agrandando";

        }
    }

    class Swipe
    {
        int fingerID;
        public int ID { get { return fingerID; } }
        Vector2 origin;
        Vector2 end = new Vector2(-1,-1);      
        public Vector2 Origin{get{return origin;}set{origin = value;}}
        public Vector2 End{get{return end;}set{end = value;}}
        bool ended = false;
        public bool Ended { get { return ended; } set { ended = value; } }
        bool tap = true;
        public bool Tap { get { return tap; } set { tap = value; } }

        public Swipe(Vector2 N_origin,int finger)
        {
            origin = N_origin;
            fingerID = finger;
        }

        public Vector2 Movement()
        {
            if (end.x < 0)
                return new Vector2(0, 0);

            return end - origin;
        }
        public swipeDirection Direction()
        {
            Vector2 direction = Movement().normalized;

            if (direction.y > 0.8)
                return swipeDirection.top;
            if (direction.y < -0.8)
                return swipeDirection.down;

            if (direction.x > 0.2)
            {
                if (direction.y > 0.2)
                    return swipeDirection.rightTop;
                if (direction.y < -0.2)
                    return swipeDirection.rightDown;

                return swipeDirection.right;
            }
            else
            {
                if (direction.y > 0.2)
                    return swipeDirection.leftTop;
                if (direction.y < -0.2)
                    return swipeDirection.leftDown;

                return swipeDirection.left;
            }
        }
    }       

    class Gesture
    {
        List<swipeDirection> recordedDirections = new List<swipeDirection>();
        int duration = 0;
        public int Duration { get { return duration; } }

        public void RecordGesture(Swipe swipe)
        {
            duration++;
            if(recordedDirections.Count == 0 || recordedDirections[recordedDirections.Count - 1]!= swipe.Direction())
            {
                recordedDirections.Add(swipe.Direction());
            } 
        }

        public gestures ReadGesture()
        {
            return gestures.c
        }


    }


}
