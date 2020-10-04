# AsteroidsGame
Asteroids Game on Unity 2019.4.11f1

## Architecture overview
Asteroids uses the idea of space wrapping which when an object move outside of an edge of the screen it shows up on the opposite edge. The original game use vector graphics and its engine is built to support that. Rebuilding that in a modern engine would make every other aspect of the rendering and physics of the engine incompatible with your code. So, we need to find a way to emulate that behavior using standard Unity workflow.

The first thing that we must check is when an object is outside of screen, then we wrap it to the opposite side. For that, I used a bounding box that must contains the object rendering. So, when the top left corner moves out of screen, the object is wrapped.

If we stop here, there is a moment where the object is not visible in the screen, but in the original game, the object shows partially in one side and partially on the other. To achieve that we can copy the object and translate a copy to the other side. I am going with part of this idea, but if we have a complex object to render or an effect, copying this object can be demanding. To solve this, I am using auxiliary cameras that render what is on the side and overlap on the screen, giving the desired effect.

The copy approach is used in the colliders because the object can be renderer in the “right” spot, but the collider is not there. So, coping the collider and routing the collisions events to original object make it seems that the object is there. Three auxiliary colliders follow the original object only if the object is in a zone that it can be renderer by the other cameras.

Now we can use all Unity features like shaders, particles, 3D/2D objects and physics maintaining the desired mechanics of Asteroids.


## Used free assets
https://assetstore.unity.com/packages/3d/luminaris-starship-71439

https://assetstore.unity.com/packages/3d/environments/asteroids-pack-84988

https://assetstore.unity.com/packages/vfx/particles/effect-textures-and-prefabs-109031

https://www.fontspace.com/sf-atarian-system-font-f6230

https://opengameart.org/content/space-music-blind-shift

https://opengameart.org/content/4-projectile-launches

https://opengameart.org/content/2-high-quality-explosions

https://opengameart.org/content/fire-loop

https://opengameart.org/content/low-poly-3d-flying-saucer-model