using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine
{
    public class BoxCollider : Component
    {
        public Rectangle hitbox => new Rectangle((int)myActor.position.X, (int)myActor.position.Y, myActor.width, myActor.height);

        public bool active;

        public BoxCollider(Actor myActor) : base(myActor)
        {

        }

        public override void Update()
        {
            for(int i = 0; i < myActor.myStage.actors.Count; i++)
            {
                if(myActor.myStage.actors[i].HasComponent<BoxCollider>() && myActor.HasComponent<Rigidbody>())
                {
                    Rigidbody rb = myActor.GetComponent<Rigidbody>();
                    Actor other = myActor.myStage.actors[i];

                    if ((rb.velocity.X > 0 && EngineHelpers.IsTouchingLeft(hitbox, other.GetComponent<BoxCollider>().hitbox, rb.velocity)) ||
                        (rb.velocity.X < 0 && EngineHelpers.IsTouchingRight(hitbox, other.GetComponent<BoxCollider>().hitbox, rb.velocity)))
                        rb.velocity.X = 0;

                    if ((rb.velocity.Y > 0 && EngineHelpers.IsTouchingTop(hitbox, other.GetComponent<BoxCollider>().hitbox, rb.velocity)) ||
                        (rb.velocity.Y < 0 && EngineHelpers.IsTouchingBottom(hitbox, other.GetComponent<BoxCollider>().hitbox, rb.velocity)))
                        rb.velocity.Y = 0;
                }
            }

            for (int i = 0; i < myActor.myStage.actors.Count; i++)
            {
                if (myActor.myStage.actors[i].HasComponent<TileCollider>() && myActor.HasComponent<Rigidbody>())
                {
                    for(int j = 0; j < myActor.myStage.actors[i].GetComponent<TileCollider>().rectangles.Count; j++)
                    {
                        TileCollider grid = myActor.myStage.actors[i].GetComponent<TileCollider>();
                        Rectangle tileRect = grid.rectangles[j];

                        Point temp = new Point(myActor.height / 2, myActor.height / 2);
                        Rectangle playerRect = new Rectangle((int)myActor.Center.X - temp.X, (int)myActor.Center.Y - temp.Y, myActor.height, myActor.height);

                        if ((myActor.GetComponent<Rigidbody>().velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, tileRect, myActor.GetComponent<Rigidbody>().velocity)) ||
                            (myActor.GetComponent<Rigidbody>().velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, tileRect, myActor.GetComponent<Rigidbody>().velocity)))
                            myActor.GetComponent<Rigidbody>().velocity.X = 0;

                        if ((myActor.GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, myActor.GetComponent<Rigidbody>().velocity)) ||
                            (myActor.GetComponent<Rigidbody>().velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, myActor.GetComponent<Rigidbody>().velocity)))
                            myActor.GetComponent<Rigidbody>().velocity.Y = 0;
                    }
                }
            }

            base.Update();
        }
    }
}
