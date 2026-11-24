' verbose-raylib — Umbrella Entry Point
' Usage:
'   Include "raylib.vb"
' Compile & link against real Raylib:
'   verbose app.vb -I src csrc/verbose_raylib_shim.c -lraylib -lm
' Or run headlessly for CI / tests:
'   verbose app.vb -I src csrc/headless_mock.c -lm

Include "raylib_types.vb"
Include "raylib_ffi.vb"
Include "raylib_colors.vb"
Include "raylib_math.vb"
Include "raylib_gfx.vb"
