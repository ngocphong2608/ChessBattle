﻿using UnityEngine;
using System.Collections;
using System;

public abstract class Chessman : MonoBehaviour {
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool isWhite;

    public abstract string Annotation();

    private Quaternion originRotation;

    private void Start()
    {
        originRotation = transform.rotation;
    }

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[8, 8];
    }

    public virtual bool[,] PossibleEat()
    {
        return new bool[8, 8];
    }

    public bool CanGo(int x, int y)
    {
        bool[,] possible = this.PossibleMove();

        return possible[x, y];
    }

    internal void RotateEach(float seconds)
    {
        InvokeRepeating("Rotate", 0f, seconds);
    }

    private int t = 10;
    private int dt = 3;
    private void Rotate()
    {
        transform.Rotate(Vector3.up * t);
        t += dt;
    }

    internal void DestroyAfter(float seconds)
    {
        Invoke("DestroyGameObject", seconds);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private Vector3 newPosition = Vector3.zero;
    internal void MoveAfter(float seconds, Vector3 position)
    {
        newPosition = position;
        Invoke("Move", seconds);
    }

    private void Move()
    {
        transform.position = newPosition;
        CancelInvoke("Rotate");

        // restore origin rotation
        transform.rotation = originRotation;

        // retore rotate speed
        t = 10;
    }
}