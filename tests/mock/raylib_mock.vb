' verbose-raylib — 100% Pure VerBose Headless Mock Backend for CI & Automated Testing
' Replaces csrc/headless_mock.c with zero C code.

Declare Function sqrtf(x As Single) As Single
Declare Function sinf(x As Single) As Single
Declare Function cosf(x As Single) As Single
Declare Function atan2f(y, x As Single) As Single
Declare Function fabsf(x As Single) As Single
Declare Function fmodf(x, y As Single) As Single
Declare Function strlen(s As Pointer) As Long

Private Static g_mockFrame As Integer = 0
Private Static g_mockMaxFrames As Integer = 5
Private Static g_mockDrawCalls As Integer = 0
Private Static g_mockWinW As Integer = 800
Private Static g_mockWinH As Integer = 450
Private Static g_mockRng As Integer = 1337

Private Function MockNextRng() As Integer
  g_mockRng = (g_mockRng * 1103515245 + 12345) And 0x7FFFFFFF
  Return g_mockRng
End Function

Public Sub InitWindow(width, height As Integer, title As String)
  g_mockWinW = width
  g_mockWinH = height
  g_mockFrame = 0
  g_mockDrawCalls = 0
  Printf("[headless] InitWindow(%d, %d, \"%s\")\n", width, height, title)
End Sub

Public Sub CloseWindow()
  Printf("[headless] CloseWindow (frames=%d, draw_calls=%d)\n", g_mockFrame, g_mockDrawCalls)
End Sub

Public Function WindowShouldClose() As Boolean
  If g_mockFrame >= g_mockMaxFrames Then
    Return True
  End If
  g_mockFrame = g_mockFrame + 1
  Return False
End Function

Public Function IsWindowReady() As Boolean
  Return True
End Function

Public Function IsWindowResized() As Boolean
  Return False
End Function

Public Function GetScreenWidth() As Integer
  Return g_mockWinW
End Function

Public Function GetScreenHeight() As Integer
  Return g_mockWinH
End Function

Public Function GetRenderWidth() As Integer
  Return g_mockWinW
End Function

Public Function GetRenderHeight() As Integer
  Return g_mockWinH
End Function

Public Sub SetWindowTitle(title As String)
End Sub

Public Sub SetWindowSize(width, height As Integer)
  g_mockWinW = width
  g_mockWinH = height
End Sub

Public Sub SetWindowMinSize(width, height As Integer)
End Sub

Public Sub SetWindowMaxSize(width, height As Integer)
End Sub

Public Sub SetConfigFlags(flags As Integer)
End Sub

Public Sub SetTraceLogLevel(logLevel As Integer)
End Sub

Public Sub ToggleFullscreen()
End Sub

Public Sub ToggleBorderlessWindowed()
End Sub

Public Sub SetTargetFPS(fps As Integer)
End Sub

Public Function GetFrameTime() As Single
  Return 1.0 / 60.0
End Function

Public Function GetTime() As Double
  Return Double(g_mockFrame) / 60.0
End Function

Public Function GetFPS() As Integer
  Return 60
End Function

Public Sub BeginDrawing()
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub EndDrawing()
End Sub

Public Sub ClearBackground(color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub BeginMode2D(camera As Camera2D)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub EndMode2D()
End Sub

Public Sub BeginMode3D(camera As Camera3D)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub EndMode3D()
End Sub

Public Sub BeginBlendMode(mode As Integer)
End Sub

Public Sub EndBlendMode()
End Sub

Public Sub BeginScissorMode(x, y, width, height As Integer)
End Sub

Public Sub EndScissorMode()
End Sub

Public Sub BeginTextureMode(target As RenderTexture2D)
End Sub

Public Sub EndTextureMode()
End Sub

Public Function IsKeyPressed(key As Integer) As Boolean
  Return (g_mockFrame = 1) And (key = 32)
End Function

Public Function IsKeyDown(key As Integer) As Boolean
  Return False
End Function

Public Function IsKeyReleased(key As Integer) As Boolean
  Return False
End Function

Public Function IsKeyUp(key As Integer) As Boolean
  Return True
End Function

Public Function GetKeyPressed() As Integer
  Return 0
End Function

Public Function GetCharPressed() As Integer
  Return 0
End Function

Public Sub SetExitKey(key As Integer)
End Sub

Public Function IsMouseButtonPressed(button As Integer) As Boolean
  Return False
End Function

Public Function IsMouseButtonDown(button As Integer) As Boolean
  Return False
End Function

Public Function IsMouseButtonReleased(button As Integer) As Boolean
  Return False
End Function

Public Function GetMouseX() As Integer
  Return g_mockWinW / 2 + g_mockFrame * 10
End Function

Public Function GetMouseY() As Integer
  Return g_mockWinH / 2
End Function

Public Function GetMousePosition() As Vector2
  Dim v As Vector2 With ( x = Single(g_mockWinW / 2 + g_mockFrame * 10), y = Single(g_mockWinH / 2) )
  Return v
End Function

Public Function GetMouseDelta() As Vector2
  Dim v As Vector2 With ( x = 2.0, y = 0.0 )
  Return v
End Function

Public Function GetMouseWheelMove() As Single
  Return 0.0
End Function

Public Sub HideCursor()
End Sub

Public Sub ShowCursor()
End Sub

Public Sub DisableCursor()
End Sub

Public Sub EnableCursor()
End Sub

Public Sub UpdateCamera(ByRef camera As Camera3D, mode As Integer)
End Sub

Public Sub UpdateCameraPro(ByRef camera As Camera3D, movement, rotation As Vector3, zoom As Single)
End Sub

Public Function GetScreenToWorldRay(position As Vector2, camera As Camera3D) As Ray
  Dim r As Ray With ( position = camera.position, direction = Vector3(0.0, 0.0, -1.0) )
  Return r
End Function

Public Function GetWorldToScreen(position As Vector3, camera As Camera3D) As Vector2
  Dim v As Vector2 With ( x = 400.0, y = 225.0 )
  Return v
End Function

Public Function GetScreenToWorld2D(position As Vector2, camera As Camera2D) As Vector2
  Return position
End Function

Public Function GetWorldToScreen2D(position As Vector2, camera As Camera2D) As Vector2
  Return position
End Function

Public Sub DrawPixel(posX, posY As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawPixelV(position As Vector2, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawLine(startPosX, startPosY, endPosX, endPosY As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawLineV(startPos, endPos As Vector2, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawLineEx(startPos, endPos As Vector2, thick As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawLineBezier(startPos, endPos As Vector2, thick As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCircle(centerX, centerY As Integer, radius As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCircleV(center As Vector2, radius As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCircleGradient(center As Vector2, radius As Single, inner, outer As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCircleLines(centerX, centerY As Integer, radius As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCircleLinesV(center As Vector2, radius As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRing(center As Vector2, innerRadius, outerRadius, startAngle, endAngle As Single, segments As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawEllipse(centerX, centerY As Integer, radiusH, radiusV As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangle(posX, posY, width, height As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleV(position, size As Vector2, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleRec(rec As Rectangle, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectanglePro(rec As Rectangle, origin As Vector2, rotation As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleLines(posX, posY, width, height As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleLinesEx(rec As Rectangle, thick As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleRounded(rec As Rectangle, roundness As Single, segments As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleRoundedLinesEx(rec As Rectangle, roundness As Single, segments As Integer, thick As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleGradientV(posX, posY, width, height As Integer, top, bottom As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRectangleGradientH(posX, posY, width, height As Integer, left, right As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTriangle(v1, v2, v3 As Vector2, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTriangleLines(v1, v2, v3 As Vector2, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawPoly(center As Vector2, sides As Integer, radius, rotation As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawPolyLines(center As Vector2, sides As Integer, radius, rotation As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawPolyLinesEx(center As Vector2, sides As Integer, radius, rotation, lineThick As Single, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Function CheckCollisionRecs(a, b As Rectangle) As Boolean
  Return (a.x < b.x + b.width) And (a.x + a.width > b.x) And (a.y < b.y + b.height) And (a.y + a.height > b.y)
End Function

Public Function CheckCollisionCircles(c1 As Vector2, r1 As Single, c2 As Vector2, r2 As Single) As Boolean
  Dim dx As Single = c2.x - c1.x
  Dim dy As Single = c2.y - c1.y
  Dim rr As Single = r1 + r2
  Return (dx * dx + dy * dy) <= (rr * rr)
End Function

Public Function CheckCollisionCircleRec(center As Vector2, radius As Single, rec As Rectangle) As Boolean
  Dim cx As Single = center.x
  If cx < rec.x Then
    cx = rec.x
  End If
  If cx > rec.x + rec.width Then
    cx = rec.x + rec.width
  End If
  Dim cy As Single = center.y
  If cy < rec.y Then
    cy = rec.y
  End If
  If cy > rec.y + rec.height Then
    cy = rec.y + rec.height
  End If
  Dim dx As Single = center.x - cx
  Dim dy As Single = center.y - cy
  Return (dx * dx + dy * dy) <= (radius * radius)
End Function

Public Function CheckCollisionPointRec(p As Vector2, r As Rectangle) As Boolean
  Return (p.x >= r.x) And (p.x <= r.x + r.width) And (p.y >= r.y) And (p.y <= r.y + r.height)
End Function

Public Function CheckCollisionPointCircle(p, c As Vector2, r As Single) As Boolean
  Dim dx As Single = p.x - c.x
  Dim dy As Single = p.y - c.y
  Return (dx * dx + dy * dy) <= (r * r)
End Function

Public Function GetCollisionRec(a, b As Rectangle) As Rectangle
  Dim res As Rectangle
  If Not CheckCollisionRecs(a, b) Then
    Return res
  End If
  Dim x1 As Single = a.x
  If b.x > x1 Then
    x1 = b.x
  End If
  Dim y1 As Single = a.y
  If b.y > y1 Then
    y1 = b.y
  End If
  Dim x2 As Single = a.x + a.width
  If b.x + b.width < x2 Then
    x2 = b.x + b.width
  End If
  Dim y2 As Single = a.y + a.height
  If b.y + b.height < y2 Then
    y2 = b.y + b.height
  End If
  res.x = x1
  res.y = y1
  res.width = x2 - x1
  res.height = y2 - y1
  Return res
End Function

Public Sub DrawFPS(posX, posY As Integer)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawText(text As String, posX, posY, fontSize As Integer, color As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTextEx(font As Font, text As String, position As Vector2, fontSize, spacing As Single, tint As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Function MeasureText(text As String, fontSize As Integer) As Integer
  If IsNull(text) Then
    Return 0
  End If
  Return Integer(strlen(text)) * (fontSize / 2)
End Function

Public Function MeasureTextEx(font As Font, text As String, fontSize, spacing As Single) As Vector2
  Dim v As Vector2 With ( x = Single(MeasureText(text, Integer(fontSize))), y = fontSize )
  Return v
End Function

Public Function GetFontDefault() As Font
  Dim f As Font
  f.baseSize = 10
  Return f
End Function

Public Function GenImageColor(w, h As Integer, c As Color) As Image
  Dim img As Image With ( data = Pointer(0), width = w, height = h, mipmaps = 1, format = 7 )
  Return img
End Function

Public Sub UnloadImage(img As Image)
End Sub

Public Function LoadTexture(fileName As String) As Texture2D
  Dim t As Texture2D With ( id = 1, width = 64, height = 64, mipmaps = 1, format = 7 )
  Return t
End Function

Public Function LoadTextureFromImage(img As Image) As Texture2D
  Dim t As Texture2D With ( id = 1, width = img.width, height = img.height, mipmaps = 1, format = img.format )
  Return t
End Function

Public Sub UnloadTexture(t As Texture2D)
End Sub

Public Sub UpdateTexture(t As Texture2D, pixels As Pointer)
End Sub

Public Sub SetTextureFilter(t As Texture2D, filter As Integer)
End Sub

Public Sub DrawTexture(t As Texture2D, posX, posY As Integer, tint As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTextureV(t As Texture2D, position As Vector2, tint As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTextureEx(t As Texture2D, position As Vector2, rotation, scale As Single, tint As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTextureRec(t As Texture2D, source As Rectangle, position As Vector2, tint As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawTexturePro(t As Texture2D, source, dest As Rectangle, origin As Vector2, rotation As Single, tint As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Function LoadRenderTexture(w, h As Integer) As RenderTexture2D
  Dim rt As RenderTexture2D
  rt.id = 1
  rt.texture = Texture2D With ( id = 1, width = w, height = h, mipmaps = 1, format = 7 )
  rt.depth = Texture2D With ( id = 2, width = w, height = h, mipmaps = 1, format = 0 )
  Return rt
End Function

Public Sub UnloadRenderTexture(rt As RenderTexture2D)
End Sub

Public Sub DrawLine3D(a, b As Vector3, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawPoint3D(p As Vector3, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCircle3D(p As Vector3, r As Single, ax As Vector3, ang As Single, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCube(p As Vector3, w, h, l As Single, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCubeV(p, s As Vector3, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCubeWires(p As Vector3, w, h, l As Single, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCubeWiresV(p, s As Vector3, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawSphere(p As Vector3, r As Single, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawSphereEx(p As Vector3, r As Single, rings, slices As Integer, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawSphereWires(p As Vector3, r As Single, rings, slices As Integer, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCylinder(p As Vector3, rt, rb, h As Single, s As Integer, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCylinderEx(a, b As Vector3, r1, r2 As Single, s As Integer, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCylinderWires(p As Vector3, rt, rb, h As Single, s As Integer, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawCapsule(a, b As Vector3, r As Single, rings, slices As Integer, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawPlane(p As Vector3, s As Vector2, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawRay(r As Ray, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawGrid(slices As Integer, spacing As Single)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Sub DrawBoundingBox(b As BoundingBox, c As Color)
  g_mockDrawCalls = g_mockDrawCalls + 1
End Sub

Public Function CheckCollisionSpheres(c1 As Vector3, r1 As Single, c2 As Vector3, r2 As Single) As Boolean
  Dim dx As Single = c2.x - c1.x
  Dim dy As Single = c2.y - c1.y
  Dim dz As Single = c2.z - c1.z
  Dim rr As Single = r1 + r2
  Return (dx * dx + dy * dy + dz * dz) <= (rr * rr)
End Function

Public Function CheckCollisionBoxes(a, b As BoundingBox) As Boolean
  Return (a.max.x >= b.min.x) And (a.min.x <= b.max.x) And (a.max.y >= b.min.y) And (a.min.y <= b.max.y) And (a.max.z >= b.min.z) And (a.min.z <= b.max.z)
End Function

Public Function CheckCollisionBoxSphere(b As BoundingBox, c As Vector3, r As Single) As Boolean
  Return False
End Function

Public Function GetRayCollisionSphere(ray As Ray, c As Vector3, r As Single) As RayCollision
  Dim rc As RayCollision
  rc.normal = Vector3(0.0, 1.0, 0.0)
  Return rc
End Function

Public Function GetRayCollisionBox(ray As Ray, b As BoundingBox) As RayCollision
  Dim rc As RayCollision
  rc.normal = Vector3(0.0, 1.0, 0.0)
  Return rc
End Function

Public Function Fade(c As Color, alpha As Single) As Color
  Dim a As Single = alpha
  If a < 0.0 Then
    a = 0.0
  End If
  If a > 1.0 Then
    a = 1.0
  End If
  c.a = Byte(255.0 * a)
  Return c
End Function

Public Function ColorToInt(c As Color) As Integer
  Dim r As Integer = Integer(c.r) And 255
  Dim g As Integer = Integer(c.g) And 255
  Dim b As Integer = Integer(c.b) And 255
  Dim a As Integer = Integer(c.a) And 255
  Return (r Shl 24) Or (g Shl 16) Or (b Shl 8) Or a
End Function

Private Function MockHsvChan(n, h, s, v As Single) As Byte
  Dim k As Single = fmodf(n + h / 60.0, 6.0)
  Dim m As Single = k
  If (4.0 - k) < m Then
    m = 4.0 - k
  End If
  If 1.0 < m Then
    m = 1.0
  End If
  If m < 0.0 Then
    m = 0.0
  End If
  Return Byte((v - v * s * m) * 255.0)
End Function

Public Function ColorFromHSV(h, s, v As Single) As Color
  Dim c As Color With (
    r = MockHsvChan(5.0, h, s, v),
    g = MockHsvChan(3.0, h, s, v),
    b = MockHsvChan(1.0, h, s, v),
    a = Byte(255)
  )
  Return c
End Function

Public Function ColorAlpha(c As Color, alpha As Single) As Color
  Return Fade(c, alpha)
End Function

Public Function ColorTint(c, t As Color) As Color
  Dim res As Color With (
    r = Byte(((Integer(c.r) And 255) * (Integer(t.r) And 255)) / 255),
    g = Byte(((Integer(c.g) And 255) * (Integer(t.g) And 255)) / 255),
    b = Byte(((Integer(c.b) And 255) * (Integer(t.b) And 255)) / 255),
    a = Byte(((Integer(c.a) And 255) * (Integer(t.a) And 255)) / 255)
  )
  Return res
End Function

Public Function ColorLerp(a, b As Color, t As Single) As Color
  Dim f As Single = t
  If f < 0.0 Then
    f = 0.0
  End If
  If f > 1.0 Then
    f = 1.0
  End If
  Dim ar As Single = Single(Integer(a.r) And 255)
  Dim ag As Single = Single(Integer(a.g) And 255)
  Dim ab As Single = Single(Integer(a.b) And 255)
  Dim aa As Single = Single(Integer(a.a) And 255)
  Dim br As Single = Single(Integer(b.r) And 255)
  Dim bg As Single = Single(Integer(b.g) And 255)
  Dim bb As Single = Single(Integer(b.b) And 255)
  Dim ba As Single = Single(Integer(b.a) And 255)
  Dim res As Color With (
    r = Byte(ar + (br - ar) * f),
    g = Byte(ag + (bg - ag) * f),
    b = Byte(ab + (bb - ab) * f),
    a = Byte(aa + (ba - aa) * f)
  )
  Return res
End Function

Public Function GetColor(hexVal As Integer) As Color
  Dim c As Color With (
    r = Byte((hexVal Shr 24) And 255),
    g = Byte((hexVal Shr 16) And 255),
    b = Byte((hexVal Shr 8) And 255),
    a = Byte(hexVal And 255)
  )
  Return c
End Function

Public Sub InitAudioDevice()
End Sub

Public Sub CloseAudioDevice()
End Sub

Public Function IsAudioDeviceReady() As Boolean
  Return True
End Function

Public Sub SetMasterVolume(v As Single)
End Sub

Public Function LoadSound(fileName As String) As Sound
  Dim s As Sound
  Return s
End Function

Public Sub UnloadSound(s As Sound)
End Sub

Public Sub PlaySound(s As Sound)
End Sub

Public Sub StopSound(s As Sound)
End Sub

Public Sub PauseSound(s As Sound)
End Sub

Public Sub ResumeSound(s As Sound)
End Sub

Public Function IsSoundPlaying(s As Sound) As Boolean
  Return False
End Function

Public Sub SetSoundVolume(s As Sound, v As Single)
End Sub

Public Sub SetSoundPitch(s As Sound, p As Single)
End Sub

Public Function GetRandomValue(minVal, maxVal As Integer) As Integer
  If minVal >= maxVal Then
    Return minVal
  End If
  Return minVal + (MockNextRng() Mod (maxVal - minVal + 1))
End Function

Public Sub SetRandomSeed(seed As Integer)
  g_mockRng = seed
End Sub

Public Sub TakeScreenshot(fileName As String)
End Sub

Public Function IsFileDropped() As Boolean
  Return False
End Function

Public Function LoadDroppedFiles() As FilePathList
  Dim f As FilePathList
  Return f
End Function

Public Sub UnloadDroppedFiles(files As FilePathList)
End Sub
