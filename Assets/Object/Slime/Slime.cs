using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Slime : MonoBehaviour2D
{
    private static T cyclicAccess<T>(T[] array,int index){
        if(index < 0){
            return cyclicAccess(array,(((-index)/array.Length) + 1)*array.Length - index);
        }
        else{
            return array[index % array.Length];
        }
    }
    private static T[] cyclicArray<T>(T[] array){
        T[] r = new T[array.Length + 1];
        for(int i = 0 ;i < array.Length;i++)
            r[i] = array[i];
        r[array.Length] = array[0];
        return r;
    }

    class SlimeCollisionScanner{
        public Slime Parent{get;}
        public int VertexSize{get;}
        private Vector2[] directions{get;}
        public RaycastHit2D[] Hitinfo{get;private set;}

        public  Vector2[] ScanPoint{get;}
        private bool[] ScanPoint_collided{get;}
        
        public SlimeCollisionScanner(Slime parent,int vertexsize){
            Parent = parent;
            VertexSize = vertexsize;
            directions = new Vector2[VertexSize];
            Hitinfo = new RaycastHit2D[VertexSize];

            ScanPoint = new Vector2[VertexSize];
            ScanPoint_collided = new bool[VertexSize];

            for(int i = 0 ; i < VertexSize ; i++){
                directions[i] = new Vector2(Mathf.Cos(2 * Mathf.PI * i / VertexSize),Mathf.Sin(2 * Mathf.PI * i / VertexSize));
                ScanPoint[i] = Mathf.Sqrt(Parent.Volume / Mathf.PI) * directions[i];
            }
        }

        public void Scan(){
            float ScanPosition_MaxRange = Mathf.Sqrt(Parent.Volume / Mathf.PI) * 1.5f;
            
            var parentPosition2D = Parent.Position2D;
            for(int i = 0 ; i < VertexSize ; i++){
                Hitinfo[i] = Physics2D.Raycast(Parent.transform.position, directions[i], ScanPosition_MaxRange);
                if(Hitinfo[i].collider != null){
                    ScanPoint[i] = Hitinfo[i].point - parentPosition2D;
                    ScanPoint_collided[i] = true;
                }
                else{
                    ScanPoint_collided[i] = false;
                }
            }
            //solve ax^2+bx+c=0 for x
            float a = 0;
            float b = 0;
            float c = -2 * Parent.Volume / Mathf.Sin(2 * Mathf.PI / VertexSize);
            for(int i = 0 ; i < VertexSize ; i++){
                if(Slime.cyclicAccess(ScanPoint_collided,i) == true){
                    if(Slime.cyclicAccess(ScanPoint_collided,i + 1) == true){
                        c += Slime.cyclicAccess(ScanPoint,i).magnitude * Slime.cyclicAccess(ScanPoint,i + 1).magnitude;
                    }
                    else{
                        b += Slime.cyclicAccess(ScanPoint,i + 1).magnitude;
                    }
                }
                else{
                    if(Slime.cyclicAccess(ScanPoint_collided,i + 1) == true){
                        b += Slime.cyclicAccess(ScanPoint,i).magnitude;
                    }
                    else{
                        a++;
                    }
                }
            }

            if(a != 0){
                float x = (-b + Mathf.Sqrt(b*b-4*a*c))/2/a;
                for(int i = 0 ; i < VertexSize ; i++){
                    if(ScanPoint_collided[i] == false){
                        ScanPoint[i] = x * directions[i];
                    }
                }
            }
            else if (b != 0){
                float x = -c/b;
                for(int i = 0 ; i < VertexSize ; i++){
                    if(ScanPoint_collided[i] == false){
                        ScanPoint[i] = x * directions[i];
                    }
                }
            }
            float poly = 0;
            for(int i = 0 ; i < VertexSize ; i++){
                poly += Slime.cyclicAccess(ScanPoint,i).magnitude * Slime.cyclicAccess(ScanPoint,i + 1).magnitude;
            }
            float coef = (2 * Parent.Volume / Mathf.Sin(2 * Mathf.PI / VertexSize)) / poly;
            for(int i = 0 ; i < VertexSize ; i++){
                ScanPoint[i] *= coef;
            }
        }
    }
    
    class SlimeColliderController{
        public SlimeCollisionScanner Scanner{get;}
        public int VertexSize{get;}
        public EdgeCollider2D edgeCollider2D{get;}
        public Rigidbody2D rigidbody2D{get;}
        public Vector2 ParentPosition2D{ get{ return new Vector2(Scanner.Parent.transform.position.x,Scanner.Parent.transform.position.y); } }

        public SlimeColliderController(SlimeCollisionScanner scanner,int vertexsize){
            Scanner = scanner;
            VertexSize = vertexsize;
            edgeCollider2D = Scanner.Parent.GetComponent<EdgeCollider2D>();
            edgeCollider2D.points = Slime.cyclicArray(Scanner.ScanPoint);
            rigidbody2D = Scanner.Parent.GetComponent<Rigidbody2D>();
        }

        public void Attach(){
            var points = Scanner.ScanPoint;
            var delta = Vector2.zero;
            for(int i = 0 ; i < VertexSize ; i++){
                delta += points[i] - edgeCollider2D.points[i];
            }
            var newpoints = new Vector2[VertexSize];
            for(int i = 0 ; i < VertexSize ; i++){
                newpoints[i] = 0.5f*(points[i] - edgeCollider2D.points[i]) + edgeCollider2D.points[i];
            }
            edgeCollider2D.points = Slime.cyclicArray(newpoints);
        }
    }

    [SerializeField]
    public float Volume;
    [SerializeField]
    private int VERTEX_POLYGON_SIZE;

    private SlimeCollisionScanner scanner;
    private SlimeColliderController controller;

    void Awake() {
        scanner = new SlimeCollisionScanner(this,VERTEX_POLYGON_SIZE);
        controller = new SlimeColliderController(scanner,VERTEX_POLYGON_SIZE);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate() {
        scanner.Scan();
        controller.Attach();
    }
}
