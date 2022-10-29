# Emitting particles while in OnParticleCollision method sometimes cause a termination of the method

# What happened

Emitting particles while in OnParticleCollision sometimes cause a termination of the method without any exceptions nor errors.

# How to reproduce

1. Open the attached scene on the Editor.
2. Open Console window. Play the scene.
3. Wait about 10 to 30 seconds (depending on randomness of the ParticleSystem setting).
4. You'll see Debug.LogError message on the Console.

# What the test program does

- My program uses two groups of a ParticleSystem and a Collider. (I call each group X and Y.)
- Each emitter Shape aims at the Collider each other.
- Two identical OnParticleCollision script are attached to where ParticleSystem is.
- OnParticleCollision on X calls a method of identical script on Y, and vice versa. That method calls ParticleSystem.Emit
- The system forms a kind of feedback loop like: X emits a a particle => When it hit on Y => Y emits => When it hit on X => X emits ...
- The script on Y emits two particles per called once. and I used cubes as colliders with rotating 45 degrees to make particles collide at different timings. So the number of particles gradually increases to MaxParticles.

Basically the program works as expected. But sometimes OnParticleCollision doesn't end normally.
It seems to be terminated (or be aborted) without any exceptions or errors.

To demonstrate it, the script records entering OnParticleCollision to a field and cleans up the record at the end by try-finally syntax. And at the beginning of OnParticleCollision it checks if the record is clean or it logs error if not.

# Additional notes
-  The issue doesn't happen if calling ParticleSystem.Emit directly. (See inactivated NotHappenIfCallEmitDirectly GameObject in the scene)
-  The issue doesn't happen if OnParticleCollision and Emit caller are separated into two scripts. (See NotHappenIfScriptIsSeparated)
