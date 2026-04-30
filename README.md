# Vehicle Simulation

Unity project simulating a 4-wheel vehicle with rigidbody-based physics, custom suspension, and slope/obstacle handling.

## Requirements

- Unity 6.0.40f1
- Standalone target supported (Windows, macOS, Linux), Android and iOS

## Getting started

1. Open the project in Unity 6.0.40f1.
2. Open `Assets/_Project/Scenes/Boot.unity`.
3. Press Play. The bootstrap flow loads addressables, then transitions to the Simulation scene.
Optional: Maybe need to build Addressable before start to work

## Controls

| Action | Keyboard | Gamepad | Touch |
|---|---|---|---|
| Drive forward / back | W / S | Left stick (Y) | On-screen joystick |
| Steer / pivot | A / D | Left stick (X) | On-screen joystick |

A single stick controls both throttle and steering — pure throttle drives straight, pure steering pivots in place, mixed input curves.

## Configurable parameters

Movement parameters can be tuned via `VehicleMovementConfig` ScriptableObjects.

- Pre-made configs: `Assets/_Project/Configs/`
- Create new: right-click in Project window → `Create / Configs / Vehicle Movement Config`

Tunable fields include mass, center of mass, wheel radius, suspension travel, spring stiffness, damping, motor force, steering force, grip coefficient, and ground layer mask.

## Maps

Switch between test environments at runtime using the in-game UI buttons:

- **Prototype** — flat surface for baseline driving
- **Obstacles** — small bumps and stairs at varying heights
- **Terrain** — sloped terrain with hills and dips

## Architecture

The project is split into two scenes:

- **Boot** — loads addressables data, initializes services, transitions to Simulation.
- **Simulation** — instantiates vehicle and camera, manages map switching and gameplay loop.

State flow is driven by a state machine (`GameStateMachine`):

Vehicle movement is implemented as a strategy pattern. The `Vehicle` MonoBehaviour delegates to an `IVehicleMovementStrategy`, which can be swapped without touching the vehicle component. Current implementation: `RigidbodyUGVMovementStrategy`. Adding a new strategy (e.g., kinematic, wheel-collider-based) requires implementing the interface and registering it in the factory.

### Tech stack

- **Zenject** — dependency injection
- **UniTask** — async/await for asset loading and state transitions
- **ZLinq** — allocation-free LINQ for hot paths
- **Addressables** — runtime asset loading for vehicle prefabs, maps, and configs
- **Cinemachine** — third-person follow camera
- **Input System** — unified keyboard/gamepad/touch input

## Mobile

On Android and iOS builds, an on-screen HUD with a virtual joystick is enabled automatically in the Simulation scene. Desktop builds use keyboard and gamepad.

## Physics approach

Movement uses a custom rigidbody simulation rather than Unity's `WheelCollider`. Rationale:
Each wheel performs a `SphereCast` downward to find ground contact. Per-wheel forces are applied at the contact points: a spring-damper for suspension, a forward drive force projected onto the contact plane, and a lateral friction force opposing sideways slip. Yaw rotation emerges naturally from the imbalance between left-side and right-side drive forces.