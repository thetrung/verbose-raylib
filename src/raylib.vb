' verbose-raylib — Umbrella Entry Point (100% Pure VerBose, zero custom C files!)
' Usage:
'   Include "raylib.vb"
' Compile & link against real Raylib:
'   verbose app.vb -I src -lraylib -lm
' Or run headlessly for CI / tests:
'   verbose app.vb -I tests/mock -I src -lm

Include "raylib_types.vb"
Include "raylib_ffi.vb"
Include "raylib_shim.vb"
Include "raylib_colors.vb"
Include "raylib_math.vb"
Include "raylib_gfx.vb"
