﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviour2D : MonoBehaviour
{
    public Vector2 Position2D{ get{ return new Vector2(transform.position.x,transform.position.y); } }
}
