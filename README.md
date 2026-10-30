# IntroToCG_Makeup

**Reflection Shader:**

I made a simple ShaderGraph using the reflection probe node to create a slight reflection on the ground surface, which, although different from the original game, I thought would add a nice touch to the scene as I wasn't sure whether I was going to have enough time for textures. The only issue with this is, since there were no objects above the camera and/or reflection probe, the resulting cube map had a nearly identical effect to the scene as if it were using the skybox.


**Texture Tiling Shader:**

This was was most easily used for the lava, so I offset noise with time values to make a tiling animation. Paired with color interpolation and a bit of tiling (as the object was scaled non-uniformly, and I did not want to make world-space UVs), the resulting fragments .


**Multiple UVs Shader:**

Not being sure what to use this for, I made a second ground object and applied sample vignette texture as a detail mask to my custom groud texture, and, with an additional multiplier, made it look a bit more like lava cracks.


**Lighting Toggle Shader:**

This programmed shader includes the three main lighting models covered (Lambert, Ambient, and Specular) with toggle parameters that enable the logic for each, which are controlled at runtime based on input (controls shown in-game).


**General Purpose Shader:**

This shader was also tricky to find a use for as 'general purpose' is pretty vague, especially when the scene itself is not intended to be graphically complex. Thus, I just left it as the default HLSL/ShaderLab, which includes a base color and texture, as well as tiling and offset parameters. The material which used this shader was the background volcano, as it did not have any special lighting or effects - only a color, and, time permitting, a texture.


**Additional Shader:**

The final shader I made was for the falling fireball, as per in the demo video. I had yet to adjust any vertex positions yet, so I decided to do so for a cooler effect. Aside from that, this shader is fairly similar to the lava shader, but with different color and noise values, as well as modifying each vertex's local Y coordinate based on said noise, multiplied by a height parameter.
