' Demo 01: Arena-Backed Particle Gravity Well Simulation (400 particles + glow + trails)
' Controls:
'   Mouse Move       - Move gravity / repulsion attractor
'   Left Mouse Hold  - Repulse particles
'   SPACE            - Toggle vortex swirl mode
'   R                - Reset particles
Include "raylib.vb"

Structure Particle
  Dim pos, prev, vel As Vector2
  Dim hue, radius, mass As Single
End Structure

Private Sub InitParticles(ByRef ps() As Particle, n As Integer, w, h As Single)
  For i = 0 To n - 1 Do
    Dim angle As Single = RlShimRandomFloat(0.0, RL_TAU)
    Dim dist As Single = RlShimRandomFloat(40.0, 260.0)
    Dim cx As Single = w * 0.5 + cosf(angle) * dist
    Dim cy As Single = h * 0.5 + sinf(angle) * dist
    Dim speed As Single = RlShimRandomFloat(40.0, 140.0)
    With ps(i)
      .pos = Vec2(cx, cy)
      .prev = .pos
      .vel = Vec2(-sinf(angle) * speed, cosf(angle) * speed)
      .hue = fmodf(RlShimIntToFloat(i) * 1.4, 360.0)
      .radius = RlShimRandomFloat(2.0, 4.8)
      .mass = RlShimRandomFloat(0.8, 2.2)
    End With
  End For
End Sub

Public Function main() As Integer
  Dim winW As Integer = 1024
  Dim winH As Integer = 640
  RlOpenQuiet(winW, winH, 60, "VerBose-Raylib :: 01 Particle Gravity Arena")

  Dim count As Integer = 400
  Dim worldArena As Arena = ArenaCreate(65536)
  Dim particles() As Particle = ArenaAlloc(ByRef worldArena, Particle, count)
  InitParticles(ByRef particles, count, RlShimIntToFloat(winW), RlShimIntToFloat(winH))

  Dim vortex As Boolean = True
  Dim elapsed As Single = 0.0

  While RlRunning() Do
    Dim dt As Single = RlClampF(GetFrameTime(), 0.001, 0.033)
    elapsed = elapsed + dt

    If IsKeyPressed(KEY_SPACE) Then
      vortex = Not vortex
    End If
    If IsKeyPressed(KEY_R) Then
      InitParticles(ByRef particles, count, RlShimIntToFloat(winW), RlShimIntToFloat(winH))
    End If

    Dim target As Vector2 = GetMousePosition()
    Dim repulse As Boolean = IsMouseButtonDown(MOUSE_BUTTON_LEFT)
    Dim sign As Single = 1.0
    If repulse Then
      sign = -2.2
    End If

    Dim maxW As Single = RlShimIntToFloat(winW)
    Dim maxH As Single = RlShimIntToFloat(winH)

    For i = 0 To count - 1 Do
      With particles(i)
        .prev = .pos
        Dim delta As Vector2 = Vec2Sub(target, .pos)
        Dim dSq As Single = Vec2LenSq(delta) + 450.0
        Dim dist As Single = sqrtf(dSq)
        Dim dir As Vector2 = Vec2(delta.x / dist, delta.y / dist)

        Dim force As Single = (36000.0 * sign) / dSq
        .vel.x = .vel.x + dir.x * force * dt / .mass
        .vel.y = .vel.y + dir.y * force * dt / .mass

        If vortex Then
          .vel.x = .vel.x - dir.y * 140.0 * dt
          .vel.y = .vel.y + dir.x * 140.0 * dt
        End If

        ' Mild velocity damping
        .vel = Vec2Scale(.vel, 0.992)
        .pos = Vec2Add(.pos, Vec2Scale(.vel, dt))

        ' Soft boundary bounce
        If .pos.x < .radius Then
          .pos.x = .radius
          .vel.x = fabsf(.vel.x) * 0.8
        End If
        If .pos.x > maxW - .radius Then
          .pos.x = maxW - .radius
          .vel.x = -fabsf(.vel.x) * 0.8
        End If
        If .pos.y < .radius Then
          .pos.y = .radius
          .vel.y = fabsf(.vel.y) * 0.8
        End If
        If .pos.y > maxH - .radius Then
          .pos.y = maxH - .radius
          .vel.y = -fabsf(.vel.y) * 0.8
        End If
        .hue = fmodf(.hue + dt * 24.0, 360.0)
      End With
    End For

    RlBeginFrame(RlMidnight())
    BeginBlendMode(BLEND_ADDITIVE)
    For i = 0 To count - 1 Do
      Dim col As Color = RlHsv(particles(i).hue, 0.82, 0.95)
      DrawLineEx(particles(i).prev, particles(i).pos, particles(i).radius * 0.75, RlWithAlpha(col, 0.55))
      DrawCircleV(particles(i).pos, particles(i).radius, col)
    End For
    If repulse Then
      RlDrawGlowCircle(target, 12.0, RlCoral())
    Else
      RlDrawGlowCircle(target, 10.0, RlCyan())
    End If
    EndBlendMode()

    Dim modeStr As String = "ATTRACT"
    If repulse Then
      modeStr = "REPULSE"
    End If
    Dim swirlStr As String = "OFF"
    If vortex Then
      swirlStr = "ON"
    End If
    RlDrawHudBanner($"Particle Gravity Arena ({count} Arena-backed particles)", $"Mode: {modeStr} | Vortex [SPACE]: {swirlStr} | [R] Reset | FPS: {GetFPS()}", 480.0)
    RlEndFrame()
  End While

  ArenaDestroy(ByRef worldArena)
  RlClose()
  Return 0
End Function
