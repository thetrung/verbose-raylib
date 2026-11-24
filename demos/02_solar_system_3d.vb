' Demo 02: 3D Interactive Solar System & Orbital Camera
' Controls:
'   Left/Right/Up/Down  - Orbit 3D camera
'   Mouse Wheel / +/-   - Zoom in/out
'   SPACE               - Pause / Resume orbital motion
'   1 / 2 / 3           - Time speed (0.5x / 1.0x / 3.0x)
Include "raylib.vb"

Structure Planet
  Dim orbitRadius, radius, angularSpeed, angle, tiltY As Single
  Dim hasRing, hasMoon As Boolean
  Dim col As Color
End Structure

Private Function MakePlanet(orb, rad, spd, phase, tilt As Single, ring, moon As Boolean, col As Color) As Planet
  Dim p As Planet With (
    orbitRadius = orb,
    radius = rad,
    angularSpeed = spd,
    angle = phase,
    tiltY = tilt,
    hasRing = ring,
    hasMoon = moon,
    col = col
  )
  Return p
End Function

Public Function main() As Integer
  RlOpenQuiet(1120, 680, 60, "VerBose-Raylib :: 02 3D Solar System")

  Dim cam As Camera3D = RlCamera3D(Vec3(0.0, 14.0, 26.0), Vec3Zero(), 45.0)
  Dim camRadius As Single = 26.0
  Dim camYaw As Single = 0.65
  Dim camPitch As Single = 0.45

  Dim planets(6) As Planet
  planets(0) = MakePlanet(4.2, 0.42, 1.85, 0.4, 0.12, False, False, RlLightGray())   ' Mercury
  planets(1) = MakePlanet(6.5, 0.65, 1.25, 1.8, -0.08, False, False, RlOrange())     ' Venus
  planets(2) = MakePlanet(9.4, 0.78, 0.92, 3.1, 0.05, False, True, RlSkyBlue())      ' Earth + Moon
  planets(3) = MakePlanet(12.2, 0.56, 0.72, 4.7, 0.14, False, False, RlCoral())      ' Mars
  planets(4) = MakePlanet(16.5, 1.45, 0.40, 0.9, -0.10, False, True, RlGold())       ' Jupiter
  planets(5) = MakePlanet(21.5, 1.15, 0.28, 2.5, 0.16, True, False, RlEmerald())     ' Ringed Giant

  ' Generate 120 asteroid belt angles in an Arena
  Dim beltCount As Integer = 120
  Dim arena As Arena = ArenaCreate(8192)
  Dim beltAngles() As Single = ArenaAlloc(ByRef arena, Single, beltCount)
  Dim beltRadii() As Single = ArenaAlloc(ByRef arena, Single, beltCount)
  For i = 0 To beltCount - 1 Do
    beltAngles(i) = RlShimRandomFloat(0.0, RL_TAU)
    beltRadii(i) = RlShimRandomFloat(13.6, 14.8)
  End For

  Dim paused As Boolean = False
  Dim timeScale As Single = 1.0
  Dim moonAngle As Single = 0.0

  While RlRunning() Do
    Dim dt As Single = RlClampF(GetFrameTime(), 0.001, 0.05)

    ' Interactive camera controls
    If IsKeyDown(KEY_LEFT) Then
      camYaw = camYaw - 1.4 * dt
    End If
    If IsKeyDown(KEY_RIGHT) Then
      camYaw = camYaw + 1.4 * dt
    End If
    If IsKeyDown(KEY_UP) Then
      camPitch = camPitch + 1.0 * dt
    End If
    If IsKeyDown(KEY_DOWN) Then
      camPitch = camPitch - 1.0 * dt
    End If
    camRadius = RlClampF(camRadius - GetMouseWheelMove() * 1.8, 9.0, 48.0)

    If IsKeyPressed(KEY_SPACE) Then
      paused = Not paused
    End If
    If IsKeyPressed(KEY_ONE) Then
      timeScale = 0.5
    End If
    If IsKeyPressed(KEY_TWO) Then
      timeScale = 1.0
    End If
    If IsKeyPressed(KEY_THREE) Then
      timeScale = 3.0
    End If

    If Not paused Then
      camYaw = camYaw + 0.14 * dt
      moonAngle = moonAngle + 3.2 * dt * timeScale
      For i = 0 To 5 Do
        planets(i).angle = planets(i).angle + planets(i).angularSpeed * dt * timeScale
      End For
      For i = 0 To beltCount - 1 Do
        beltAngles(i) = beltAngles(i) + 0.18 * dt * timeScale
      End For
    End If

    RlOrbitCamera(ByRef cam, camRadius, camYaw, camPitch)

    RlBeginFrame(RlRgb(7, 9, 18))
    BeginMode3D(cam)
    DrawGrid(28, 1.5)

    ' Sun + wireframe corona
    DrawSphere(Vec3Zero(), 2.2, RlGold())
    DrawSphereWires(Vec3Zero(), 2.45, 10, 14, RlWithAlpha(RlOrange(), 0.45))

    ' Planets, orbits, rings & moons
    For i = 0 To 5 Do
      With planets(i)
        RlDrawOrbitRing(Vec3Zero(), .orbitRadius, 64, RlRgba(70, 95, 145, 95))
        Dim px As Single = cosf(.angle) * .orbitRadius
        Dim pz As Single = sinf(.angle) * .orbitRadius
        Dim py As Single = sinf(.angle * 2.0) * .tiltY * .orbitRadius * 0.2
        Dim pos As Vector3 = Vec3(px, py, pz)
        DrawSphere(pos, .radius, .col)
        DrawSphereWires(pos, .radius * 1.02, 8, 10, RlWithAlpha(RlWhite(), 0.2))

        If .hasRing Then
          RlDrawOrbitRing(pos, .radius * 1.65, 32, RlGold())
          RlDrawOrbitRing(pos, .radius * 1.95, 32, RlWithAlpha(RlOrange(), 0.7))
        End If
        If .hasMoon Then
          Dim mDist As Single = .radius * 2.1
          Dim mPos As Vector3 = Vec3(pos.x + cosf(moonAngle) * mDist, pos.y + 0.2, pos.z + sinf(moonAngle) * mDist)
          DrawSphere(mPos, 0.22, RlRayWhite())
        End If
      End With
    End For

    ' Asteroid belt
    For i = 0 To beltCount - 1 Do
      Dim ax As Single = cosf(beltAngles(i)) * beltRadii(i)
      Dim az As Single = sinf(beltAngles(i)) * beltRadii(i)
      DrawCube(Vec3(ax, 0.0, az), 0.18, 0.14, 0.18, RlDarkGray())
    End For
    EndMode3D()

    Dim stateLabel As String = "RUNNING"
    If paused Then
      stateLabel = "PAUSED"
    End If
    RlDrawHudBanner($"3D Solar System (6 Planets + {beltCount} Asteroids)", $"State: {stateLabel} | Arrows: Orbit | Wheel: Zoom | [1/2/3] Speed | [SPACE] Pause | FPS: {GetFPS()}", 620.0)
    RlEndFrame()
  End While

  ArenaDestroy(ByRef arena)
  RlClose()
  Return 0
End Function
