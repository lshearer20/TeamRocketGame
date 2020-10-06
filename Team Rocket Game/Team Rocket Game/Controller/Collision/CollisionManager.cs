using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Rocket_Game.Model.Entities;
using Team_Rocket_Game.Model.Entities.Bullets;

namespace Team_Rocket_Game.Controller.Collision
{
    //We really struggled with collision
    //Referenced an old project for collision help
    public class CollisionManager
    {
        private List<List<CollisionRegion>> collisionRegions;
        private int width, height, regionWidth;

        public CollisionManager(int windowHeight, int windowWidth, int regionWidth)
        {
            this.collisionRegions = new List<List<CollisionRegion>>();

            this.width = windowWidth / regionWidth + regionWidth;
            this.height = windowHeight / regionWidth + regionWidth;

            for (int i = 0; i < this.width; ++i)
            {
                this.collisionRegions.Add(new List<CollisionRegion>());
                for (int j = 0; j < this.height; ++j)
                {
                    this.collisionRegions[i].Add(new CollisionRegion());
                }
            }
            this.regionWidth = regionWidth;
        }

        public Vector2 GetCoordinates(Vector2 position)
        {
            Vector2 ret = new Vector2();
            ret.X = position.X / this.regionWidth;
            ret.Y = position.Y / this.regionWidth;
            return ret;
        }

        public HashSet<Component> GetObjectsCollided(Component component, Type type)
        {
            HashSet<Component> ret = new HashSet<Component>();
            double radius = component.HitBoxRadius;

            /* Directions */
            int[,] dirs = new int[,] {
                { 1, 1 }, { 0, 1 }, { -1, 1 },
                { 1, 0 } , { 0, 0 } , { -1, 0 },
                { 1, -1 } , { 0, -1 } , { -1, -1 }
            };

            // Check all neighbors
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 1; j <= 1; ++j)
                {
                    int xOffset = dirs[i, 0];
                    int yOffset = dirs[i, 1];

                    CollisionRegion collision = GetOffsetRegion(component, xOffset, yOffset);
                    //null indicates that the coordinates were invalid
                    if (collision != null)
                    {
                        //For all game objects in the bucket, if within the collision region add to return set
                        foreach (Component componentObj in collision.GetComponents())
                        {
                            if (!componentObj.GetType().IsSubclassOf(type) && !componentObj.GetType().Equals(type))
                                continue;
                            if (componentObj.BoundsContains(componentObj)
                                || componentObj.BoundsContains(componentObj))
                            {
                                ret.Add(componentObj);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private CollisionRegion GetRegion(Component gameObject)
        {
            Vector2 coordinates = GetCoordinates(gameObject.Position);
            if (!IsValidRegion((int)coordinates.X, (int)coordinates.Y))
                return null;

            CollisionRegion bucket = collisionRegions[(int)coordinates.X][(int)coordinates.Y];
            return bucket;
        }

        private bool IsValidRegion(int x, int y)
        {
            return !(x >= this.width || x < 0
              || y >= this.height || y < 0);
        }

        private CollisionRegion GetOffsetRegion(Component gameObject, int xOffset, int yOffset)
        {
            Vector2 coordinates = GetCoordinates(gameObject.Position);

            /* If the coordinates are out of bounds, we return null. */
            if (!IsValidRegion((int)coordinates.X + xOffset, (int)coordinates.Y + yOffset))
                return null;

            CollisionRegion bucket = collisionRegions[(int)coordinates.X + xOffset][(int)coordinates.Y + yOffset];
            return bucket;
        }

        public void Add(Component component)
        {
            CollisionRegion region = GetRegion(component);

            if (region == null)
            {
                return;
            }
            region.AddComponent(component);
        }

        public bool Remove(Component component)
        {
            CollisionRegion region = GetRegion(component);
            if (region == null)
                return false;

            return region.RemoveComponent(component);
        }

        public void UdateObjectPositionWithFunction(Component component, Func<bool> lambda)
        {
            Remove(component);
            lambda.Invoke();
            Add(component);
        }

        public void HandleEnemyBulletCollisions(EnemyBullet bullet)
        {
            HashSet<Component> collidedBullets = GetObjectsCollided(bullet, typeof(EnemyBullet));
            HashSet<Component> collidedEnemies = GetObjectsCollided(bullet, typeof(Enemy));

            if (Player.Instance.BoundsContains(bullet) || bullet.BoundsContains(Player.Instance))
            {
                Player.Instance.TakeDamage(bullet.Damage);
                bullet.IsDead = true;
            }

            // see if deflected bullet hits enemy bullet
            foreach (EnemyBullet go in collidedBullets)
            {
                if (bullet.IsDead) { break; }
                else if (go.IsDead || go == bullet) { continue; }
                if (bullet.Movement.Deflected)
                {
                    HandleBulletCollision(bullet, go);
                }

            }
            // see if deflected enemy bullet hits an enemy
            foreach (Enemy go in collidedEnemies)
            {
                if (bullet.IsDead) { break; }
                else if (go.IsDead) { continue; }
                if (bullet.Movement.Deflected)
                {
                    go.TakeDamage(bullet.Damage);
                    bullet.IsDead = true;
                }

            }

            if (bullet.IsDead)
            {
                Remove(bullet);
            }
        }

        public void HandlePlayerBulletCollisions(PlayerBullet bullet)
        {
            // player hits itself from defelcted bullet
            if ((Player.Instance.BoundsContains(bullet) || bullet.BoundsContains(Player.Instance)) && bullet.Movement.Deflected == true)
            {
                Player.Instance.TakeDamage(bullet.Damage);
                bullet.IsDead = true;
            }

            HashSet<Component> collidedEnemies = GetObjectsCollided(bullet, typeof(Enemy));
            HashSet<Component> collidedBullets = GetObjectsCollided(bullet, typeof(EnemyBullet));

            //collision with enemy
            foreach (Enemy enemyObject in collidedEnemies)
            {
                if (enemyObject.IsDead) { continue; }
                enemyObject.TakeDamage(bullet.Damage);
                bullet.IsDead = true;
            }
            //collision with enemy bullets
            foreach (EnemyBullet enemyBullet in collidedBullets)
            {
                if (bullet.IsDead) { break; }
                else if (enemyBullet.IsDead) { continue; }
                HandleBulletCollision(bullet, enemyBullet);
            }

            if (bullet.IsDead)
            {
                Remove(bullet);
            }
        }

        public void HandleBulletCollision(Bullet bullet, Bullet other)
        {
            float m1 = (float)(bullet.Sprite.Width * bullet.Sprite.Height);
            float m2 = (float)(other.Sprite.Width * other.Sprite.Height);

            Vector2 P = m1 * bullet.Movement.GetVelocityVector()
               + m2 * other.Movement.GetVelocityVector();

            Vector2 C = bullet.Movement.GetVelocityVector()
                - other.Movement.GetVelocityVector();

            Vector2 v2f = (P + m1 * C) / (m1 + m2);
            Vector2 v1f = v2f - C;

            float heatLoss = 1f;

            v2f.Normalize();
            v1f.Normalize();

            if (!bullet.Texture.Equals("playerbulletspecial"))
            {
                if (!(bullet.Movement.Deflected && other.Texture.Equals("playerbulletspecial")))
                {
                    bullet.Movement.SetVelocityVector(v1f * heatLoss);
                    bullet.Velocity = v1f.Length();
                }
            }
            if (!other.Texture.Equals("playerbulletspecial"))
            {
                if (!(other.Movement.Deflected && bullet.Texture.Equals("playerbulletspecial")))
                {
                    other.Movement.SetVelocityVector(v2f * heatLoss);
                    other.Velocity = v2f.Length();
                }
            }
        }
    }
}