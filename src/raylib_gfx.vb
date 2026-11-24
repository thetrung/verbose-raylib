' verbose-raylib — High-Level VerBose-Style Graphics, Camera & UI Helpers
Include "raylib_colors.vb"
Include "raylib_math.vb"

' --- Window Lifecycle ---
Public Sub RlOpen(width, height, fps As Integer, title As String)
  SetConfigFlags(FLAG_MSAA_4X_HINT Or FLAG_VSYNC_HINT)
  InitWindow(width, height, title)
  SetTargetFPS(fps)
End Sub

Public Sub RlOpenQuiet(width, height, fps As Integer, title As String)
  SetTraceLogLevel(LOG_WARNING)
  RlOpen(width, height, fps, title)
End Sub

Public Function RlRunning() As Boolean
  Return Not WindowShouldClose()
End Function

Public Sub RlClose()
  CloseWindow()
End Sub

Public Sub RlBeginFrame(bg As Color)
  BeginDrawing()
  ClearBackground(bg)
End Sub

Public Sub RlEndFrame()
  EndDrawing()
End Sub

' --- Cameras ---
Public Function RlCamera2D(offset, target As Vector2, rotation, zoom As Single) As Camera2D
  Dim cam As Camera2D With (
    offset = offset,
    target = target,
    rotation = rotation,
    zoom = zoom
  )
  Return cam
End Function

Public Function RlCamera3D(pos, target As Vector3, fovy As Single) As Camera3D
  Dim cam As Camera3D With (
    position = pos,
    target = target,
    up = Vec3(0.0, 1.0, 0.0),
    fovy = fovy,
    projection = CAMERA_PERSPECTIVE
  )
  Return cam
End Function

' Smooth spherical orbital positioning around cam.target
Public Sub RlOrbitCamera(ByRef cam As Camera3D, radius, yawRad, pitchRad As Single)
  Dim clampedPitch As Single = RlClampF(pitchRad, -1.45, 1.45)
  Dim cp As Single = cosf(clampedPitch)
  Dim sp As Single = sinf(clampedPitch)
  Dim cy As Single = cosf(yawRad)
  Dim sy As Single = sinf(yawRad)
  With cam
    .position.x = .target.x + radius * cp * cy
    .position.y = .target.y + radius * sp
    .position.z = .target.z + radius * cp * sy
  End With
End Sub

' --- High-Level 2D Drawing & HUD Helpers ---
Public Sub RlDrawGlowCircle(center As Vector2, radius As Single, col As Color)
  RlShimDrawCircleGradient(center.x, center.y, radius * 2.2, RlWithAlpha(col, 0.45), RlWithAlpha(col, 0.0))
  DrawCircleV(center, radius, col)
End Sub

Public Sub RlDrawPanel(bounds As Rectangle, fill, border As Color)
  DrawRectangleRounded(bounds, 0.14, 8, fill)
  DrawRectangleRoundedLinesEx(bounds, 0.14, 8, 1.5, border)
End Sub

Public Sub RlDrawProgressBar(bounds As Rectangle, ratio As Single, bg, fg, border As Color)
  Dim clamped As Single = RlClampF(ratio, 0.0, 1.0)
  DrawRectangleRec(bounds, bg)
  If clamped > 0.0 Then
    Dim fillRec As Rectangle = Rect(bounds.x + 2.0, bounds.y + 2.0, (bounds.width - 4.0) * clamped, bounds.height - 4.0)
    DrawRectangleRec(fillRec, fg)
  End If
  DrawRectangleLinesEx(bounds, 1.5, border)
End Sub

Public Sub RlDrawCenteredText(text As String, centerX, posY, fontSize As Integer, col As Color)
  Dim tw As Integer = MeasureText(text, fontSize)
  DrawText(text, centerX - (tw / 2), posY, fontSize, col)
End Sub

Public Sub RlDrawHudBanner(title, subtitle As String, panelW As Single)
  Dim box As Rectangle = Rect(14.0, 14.0, panelW, 64.0)
  RlDrawPanel(box, RlRgba(12, 16, 28, 205), RlRgba(90, 130, 210, 180))
  DrawText(title, 26, 24, 20, RlRayWhite())
  DrawText(subtitle, 26, 49, 14, RlSkyBlue())
End Sub

' Interactive immediate-mode UI button; returns True when clicked
Public Function RlButton(bounds As Rectangle, label As String, fontSize As Integer) As Boolean
  Dim mouse As Vector2 = GetMousePosition()
  Dim hovered As Boolean = RectContains(bounds, mouse)
  Dim pressed As Boolean = hovered And IsMouseButtonDown(MOUSE_BUTTON_LEFT)
  Dim clicked As Boolean = hovered And IsMouseButtonReleased(MOUSE_BUTTON_LEFT)

  Dim bg As Color = RlRgba(36, 44, 64, 235)
  Dim border As Color = RlRgba(110, 145, 220, 200)
  If pressed Then
    bg = RlRgba(70, 115, 200, 245)
    border = RlRayWhite()
  Else
    If hovered Then
      bg = RlRgba(52, 68, 102, 240)
      border = RlCyan()
    End If
  End If

  RlDrawPanel(bounds, bg, border)
  Dim cx As Integer = RlShimFloatToInt(bounds.x + bounds.width * 0.5)
  Dim cy As Integer = RlShimFloatToInt(bounds.y + (bounds.height - RlShimIntToFloat(fontSize)) * 0.5)
  RlDrawCenteredText(label, cx, cy, fontSize, RlRayWhite())
  Return clicked
End Function

' --- 3D Drawing Helpers ---
Public Sub RlDrawOrbitRing(center As Vector3, radius As Single, segments As Integer, col As Color)
  If segments < 8 Then
    segments = 8
  End If
  Dim stepRad As Single = RL_TAU / RlShimIntToFloat(segments)
  For i = 0 To segments - 1 Do
    Dim a0 As Single = RlShimIntToFloat(i) * stepRad
    Dim a1 As Single = RlShimIntToFloat(i + 1) * stepRad
    Dim p0 As Vector3 = Vec3(center.x + cosf(a0) * radius, center.y, center.z + sinf(a0) * radius)
    Dim p1 As Vector3 = Vec3(center.x + cosf(a1) * radius, center.y, center.z + sinf(a1) * radius)
    DrawLine3D(p0, p1, col)
  End For
End Sub

Public Sub RlDrawAxes3D(origin As Vector3, length As Single)
  DrawLine3D(origin, Vec3Add(origin, Vec3(length, 0.0, 0.0)), RlRed())
  DrawLine3D(origin, Vec3Add(origin, Vec3(0.0, length, 0.0)), RlGreen())
  DrawLine3D(origin, Vec3Add(origin, Vec3(0.0, 0.0, length)), RlBlue())
End Sub
