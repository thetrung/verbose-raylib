# verbose-raylib

Idiomatic **VerBose-Lang** (`v0.6.0+`) + C ABI wrapper for **Raylib 5.x / 6.x** featuring modular bindings, pure-VerBose 2D/3D vector & rectangle math, color palettes, orbital 3D camera helpers, immediate-mode HUD widgets, a Raylib 5.x/6.x compatibility shim, a headless mock for CI, and 3 graphical demos.

## Layout

```text
verbose-raylib/
├── src/
│   ├── raylib.vb             # Single umbrella Include entry point
│   ├── raylib_types.vb       # ABI-verified Raylib structs & enums
│   ├── raylib_ffi.vb         # Native C FFI Declare bindings (2D, 3D, Input, Audio, Textures)
│   ├── raylib_colors.vb      # Palette constructors (RlRgb, RlRgba, RlHsv, RlCyan, ...)
│   ├── raylib_math.vb        # Pure-VerBose Vector2 / Vector3 / Rectangle math
│   └── raylib_gfx.vb         # High-level lifecycle, orbital camera, glow, HUD & UI helpers
├── csrc/
│   ├── verbose_raylib_shim.c # Lightweight Raylib 5.x / 6.x C shim & ABI sizeof helpers
│   └── headless_mock.c       # Complete headless mock backend for GPU-less testing & CI
├── demos/
│   ├── 01_particle_arena.vb  # 400 Arena-backed gravity/vortex particles with glow trails
│   ├── 02_solar_system_3d.vb # Interactive 3D solar system with orbital camera & asteroid belt
│   └── 03_arcade_breakout.vb # Complete Breakout game using Match ... With & collision FFI
├── tests/
│   └── test_raylib_wrapper.vb # ABI sizeof() & math/collision Test/Assert suite
├── verbose.toml              # Package manifest (for verbose-pkg / vpm)
└── makefile
```

## Quick Start

```vb
Include "raylib.vb"

Public Function main() As Integer
  RlOpenQuiet(800, 450, 60, "Hello VerBose + Raylib")
  While RlRunning() Do
    RlBeginFrame(RlMidnight())
    RlDrawGlowCircle(Vec2(400.0, 225.0), 42.0, RlCyan())
    RlDrawHudBanner("VerBose-Lang + Raylib", $"FPS: {GetFPS()}", 320.0)
    RlEndFrame()
  End While
  RlClose()
  Return 0
End Function
```

Compile and run with system `libraylib`:

```bash
verbose app.vb -I src csrc/verbose_raylib_shim.c -lraylib -lm
```

## Demos & Tests

```bash
# Build all 3 graphical demos against system libraylib
make demos

# Run individual graphical demos
make run-particles
make run-solar
make run-breakout

# Run headless test suite + all 3 demos headlessly (no display required)
make test

# Verify C ABI struct sizes directly against installed raylib.h / libraylib
make test-real
```
