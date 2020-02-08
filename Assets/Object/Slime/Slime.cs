using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float Volume {get;protected set;}
    
    class SlimeCollisionScanner{
        public Slime Parent{get;}
        public Vector2 ParentPosition2D{ get{ return new Vector2(Parent.transform.position.x,Parent.transform.position.y); } }
        public int Scansize{get;}
        private Vector2[] directions{get;}
        public RaycastHit2D[] Hitinfo{get;private set;}

        public  Vector2[] ScanPosition_old{get;private set;}
        public  Vector2[] ScanPosition{get;}
        private bool[]    ScanPosition_collided{get;}
        public  Vector2[] ScanPosition_delta{get;}
        
        public SlimeCollisionScanner(Slime parent,int scansize){
            Parent = parent;
            Scansize = scansize;
            directions = new Vector2[Scansize];
            Hitinfo = new RaycastHit2D[Scansize];

            ScanPosition_old = new Vector2[Scansize];
            ScanPosition = new Vector2[Scansize];
            ScanPosition_collided = new bool[Scansize];
            ScanPosition_delta = new Vector2[Scansize];

            for(int i = 0 ; i < Scansize ; i++){
                directions[i] = new Vector2(Mathf.Cos(2 * Mathf.PI * i / Scansize),Mathf.Sin(2 * Mathf.PI * i / Scansize));

                ScanPosition_old[i] = Mathf.Sqrt(Parent.Volume / Mathf.PI) * directions[i];
                ScanPosition[i] = ScanPosition_old[i];
                ScanPosition_delta[i] = ScanPosition[i] - ScanPosition_old[i];
            }
        }

        public void Scan(){
            float ScanPosition_MaxRange = Mathf.Sqrt(Parent.Volume / Mathf.PI) * 1.5f;
            
            ScanPosition_old = ScanPosition;
            var parentPosition2D = ParentPosition2D;
            for(int i = 0 ; i < Scansize ; i++){
                Hitinfo[i] = Physics2D.Raycast(Parent.transform.position, directions[i], ScanPosition_MaxRange);
                if(Hitinfo[i].collider != null){
                    ScanPosition[i] = Hitinfo[i].point - parentPosition2D;
                    ScanPosition_collided[i] = true;
                }
                else{
                    ScanPosition_collided[i] = false;
                }
            }
            //solve ax^2+bx+c=0 for x
            float a = 0;
            float b = 0;
            float c = -2 * Parent.Volume / Mathf.Sin(2 * Mathf.PI / Scansize);
            for(int i = 0 ; i < Scansize ; i++){
                if(ScanPosition_collided[i] == true){
                    if(ScanPosition_collided[(i + 1) % Scansize] == true){
                        c += ScanPosition[i].magnitude * ScanPosition[(i + 1) % Scansize].magnitude;
                    }
                    else{
                        b += ScanPosition[(i + 1) % Scansize].magnitude;
                    }
                }
                else{
                    if(ScanPosition_collided[(i + 1) % Scansize] == true){
                        b += ScanPosition[i].magnitude;
                    }
                    else{
                        a++;
                    }
                }
            }

            if(a != 0){
                float x = (-b + Mathf.Sqrt(b*b-4*a*c))/2/a;
                for(int i = 0 ; i < Scansize ; i++){
                    if(ScanPosition_collided[i] == false){
                        ScanPosition[i] = x * directions[i];
                    }
                }
            }
            for(int i = 0 ; i < Scansize ; i++){
                ScanPosition_delta[i] = ScanPosition[i] - ScanPosition_old[i];
            }
        }
    }

    [SerializeField]
    private int VERTEX_POLYGON_SIZE = 32;
    [SerializeField]
    private float VOLUME_SIZE = 32;

    public int size_vertex {get;private set;}
    public float size_volume {get;private set;}

    void Awake() {
        size_vertex = VERTEX_POLYGON_SIZE;
        size_volume = VOLUME_SIZE;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
