using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int col;
    public int row;
    public int targetX;
    public int targetY;
    public int prevCol;
    public int prevRow;
    public bool isMatched = false;
    private GameObject otherDot;
    private Board board;
    private float swipeResist = 1f;
    private Vector2 firstTouch;
    private Vector2 finalTouch;
    private Vector2 tempPos;
    public float swipeAngle = 0;
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        col = targetX;
        row = targetY;
        prevRow = row;
        prevCol = col;
    }

    // Update is called once per frame
    void Update()
    {
        FindMatches();
        targetX = col;
        targetY = row;
        if (Mathf.Abs(targetX-transform.position.x) > .1)
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
        }
        else
        {
            tempPos = new Vector2(targetX, transform.position.y);
            transform.position = tempPos;
            board.arrayDot[col, row] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
        }
        else
        {
            tempPos = new Vector2(transform.position.x, targetY);
            transform.position = tempPos;
            board.arrayDot[col, row] = this.gameObject;
        }
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
            
           
        }
        
        
    }
    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if (otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                Dot dot = otherDot.GetComponent<Dot>();
                dot.row = row;
                dot.col = col;
                col = prevCol;
                row = prevRow;
            }
        }
        otherDot = null;

    }
    private void OnMouseDown()
    {
        firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseUp()
    {
        finalTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
        
    }
    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouch.y - firstTouch.y) > swipeResist || Mathf.Abs(finalTouch.x - firstTouch.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouch.y - firstTouch.y, finalTouch.x - firstTouch.x) * 180 / Mathf.PI;
            MovePieces();
        }
        
        
         
    }
    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && col < board.width-1 && !isMatched)
        {
            //right swipe
            otherDot = board.arrayDot[col + 1, row];
            otherDot.GetComponent<Dot>().col -=1;
            
            col += 1;
           

        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height-1 && !isMatched)
        {
            //Up swipe
            otherDot = board.arrayDot[col, row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            
            row += 1;

        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && col > 0 && !isMatched)
        {
            //left swipe
            otherDot = board.arrayDot[col  -1, row];
            otherDot.GetComponent<Dot>().col += 1;
            
            col -= 1;

        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row >0 && !isMatched)
        {
            //right swipe
            otherDot = board.arrayDot[col , row-1];
            otherDot.GetComponent<Dot>().row += 1;
            
            row -= 1;

        }
        else
        {
            Debug.Log("Error");
        }
        StartCoroutine(CheckMoveCo());
       
    }
    void FindMatches()
    {
        if (col > 0 && col < board.width-1)
        {
            GameObject leftDot1 = board.arrayDot[col - 1, row];
            GameObject rightDot1 = board.arrayDot[col + 1, row];
            if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.transform.tag == transform.tag && rightDot1.transform.tag == transform.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;

                }
            }
         
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.arrayDot[col, row + 1];
            GameObject downDot1 = board.arrayDot[col, row-1];
            if (upDot1 != null && downDot1 != null)
            {
                if (upDot1.transform.tag == transform.tag && downDot1.transform.tag == transform.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;

                }
            }
            
        }
    }
}
