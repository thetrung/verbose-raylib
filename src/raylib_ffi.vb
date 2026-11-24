' verbose-raylib — Native C FFI Declarations for Raylib 5.x / 6.x + Shim Helpers
Include "raylib_types.vb"

' --- Window & Context ---
Declare Sub InitWindow(width, height As Integer, title As String)
Declare Sub CloseWindow()
Declare Function WindowShouldClose() As Boolean
Declare Function IsWindowReady() As Boolean
Declare Function IsWindowResized() As Boolean
Declare Function GetScreenWidth() As Integer
Declare Function GetScreenHeight() As Integer
Declare Function GetRenderWidth() As Integer
Declare Function GetRenderHeight() As Integer
Declare Sub SetWindowTitle(title As String)
Declare Sub SetWindowSize(width, height As Integer)
Declare Sub SetConfigFlags(flags As Integer)
Declare Sub SetTraceLogLevel(logLevel As Integer)
Declare Sub ToggleFullscreen()

' --- Timing ---
Declare Sub SetTargetFPS(fps As Integer)
Declare Function GetFrameTime() As Single
Declare Function GetTime() As Double
Declare Function GetFPS() As Integer

' --- Drawing Modes ---
Declare Sub BeginDrawing()
Declare Sub EndDrawing()
Declare Sub ClearBackground(color As Color)
Declare Sub BeginMode2D(camera As Camera2D)
Declare Sub EndMode2D()
Declare Sub BeginMode3D(camera As Camera3D)
Declare Sub EndMode3D()
Declare Sub BeginBlendMode(mode As Integer)
Declare Sub EndBlendMode()
Declare Sub BeginScissorMode(x, y, width, height As Integer)
Declare Sub EndScissorMode()
Declare Sub BeginTextureMode(target As RenderTexture2D)
Declare Sub EndTextureMode()

' --- Input: Keyboard ---
Declare Function IsKeyPressed(key As Integer) As Boolean
Declare Function IsKeyDown(key As Integer) As Boolean
Declare Function IsKeyReleased(key As Integer) As Boolean
Declare Function IsKeyUp(key As Integer) As Boolean
Declare Function GetKeyPressed() As Integer
Declare Function GetCharPressed() As Integer
Declare Sub SetExitKey(key As Integer)

' --- Input: Mouse ---
Declare Function IsMouseButtonPressed(button As Integer) As Boolean
Declare Function IsMouseButtonDown(button As Integer) As Boolean
Declare Function IsMouseButtonReleased(button As Integer) As Boolean
Declare Function GetMouseX() As Integer
Declare Function GetMouseY() As Integer
Declare Function GetMousePosition() As Vector2
Declare Function GetMouseDelta() As Vector2
Declare Function GetMouseWheelMove() As Single
Declare Sub HideCursor()
Declare Sub ShowCursor()
Declare Sub DisableCursor()
Declare Sub EnableCursor()

' --- Camera & Screen-Space ---
Declare Sub UpdateCamera(ByRef camera As Camera3D, mode As Integer)
Declare Sub UpdateCameraPro(ByRef camera As Camera3D, movement, rotation As Vector3, zoom As Single)
Declare Function GetScreenToWorldRay(position As Vector2, camera As Camera3D) As Ray
Declare Function GetWorldToScreen(position As Vector3, camera As Camera3D) As Vector2
Declare Function GetScreenToWorld2D(position As Vector2, camera As Camera2D) As Vector2
Declare Function GetWorldToScreen2D(position As Vector2, camera As Camera2D) As Vector2

' --- 2D Shapes ---
Declare Sub DrawPixel(posX, posY As Integer, color As Color)
Declare Sub DrawPixelV(position As Vector2, color As Color)
Declare Sub DrawLine(startPosX, startPosY, endPosX, endPosY As Integer, color As Color)
Declare Sub DrawLineV(startPos, endPos As Vector2, color As Color)
Declare Sub DrawLineEx(startPos, endPos As Vector2, thick As Single, color As Color)
Declare Sub DrawLineBezier(startPos, endPos As Vector2, thick As Single, color As Color)
Declare Sub DrawCircle(centerX, centerY As Integer, radius As Single, color As Color)
Declare Sub DrawCircleV(center As Vector2, radius As Single, color As Color)
Declare Sub DrawCircleLines(centerX, centerY As Integer, radius As Single, color As Color)
Declare Sub DrawCircleLinesV(center As Vector2, radius As Single, color As Color)
Declare Sub DrawRing(center As Vector2, innerRadius, outerRadius, startAngle, endAngle As Single, segments As Integer, color As Color)
Declare Sub DrawEllipse(centerX, centerY As Integer, radiusH, radiusV As Single, color As Color)
Declare Sub DrawRectangle(posX, posY, width, height As Integer, color As Color)
Declare Sub DrawRectangleV(position, size As Vector2, color As Color)
Declare Sub DrawRectangleRec(rec As Rectangle, color As Color)
Declare Sub DrawRectanglePro(rec As Rectangle, origin As Vector2, rotation As Single, color As Color)
Declare Sub DrawRectangleLines(posX, posY, width, height As Integer, color As Color)
Declare Sub DrawRectangleLinesEx(rec As Rectangle, thick As Single, color As Color)
Declare Sub DrawRectangleRounded(rec As Rectangle, roundness As Single, segments As Integer, color As Color)
Declare Sub DrawRectangleRoundedLinesEx(rec As Rectangle, roundness As Single, segments As Integer, thick As Single, color As Color)
Declare Sub DrawRectangleGradientV(posX, posY, width, height As Integer, top, bottom As Color)
Declare Sub DrawRectangleGradientH(posX, posY, width, height As Integer, left, right As Color)
Declare Sub DrawTriangle(v1, v2, v3 As Vector2, color As Color)
Declare Sub DrawTriangleLines(v1, v2, v3 As Vector2, color As Color)
Declare Sub DrawPoly(center As Vector2, sides As Integer, radius, rotation As Single, color As Color)
Declare Sub DrawPolyLines(center As Vector2, sides As Integer, radius, rotation As Single, color As Color)
Declare Sub DrawPolyLinesEx(center As Vector2, sides As Integer, radius, rotation, lineThick As Single, color As Color)

' --- 2D Collision ---
Declare Function CheckCollisionRecs(rec1, rec2 As Rectangle) As Boolean
Declare Function CheckCollisionCircles(center1 As Vector2, radius1 As Single, center2 As Vector2, radius2 As Single) As Boolean
Declare Function CheckCollisionCircleRec(center As Vector2, radius As Single, rec As Rectangle) As Boolean
Declare Function CheckCollisionPointRec(point As Vector2, rec As Rectangle) As Boolean
Declare Function CheckCollisionPointCircle(point, center As Vector2, radius As Single) As Boolean
Declare Function GetCollisionRec(rec1, rec2 As Rectangle) As Rectangle

' --- Text & Fonts ---
Declare Sub DrawFPS(posX, posY As Integer)
Declare Sub DrawText(text As String, posX, posY, fontSize As Integer, color As Color)
Declare Sub DrawTextEx(font As Font, text As String, position As Vector2, fontSize, spacing As Single, tint As Color)
Declare Function MeasureText(text As String, fontSize As Integer) As Integer
Declare Function MeasureTextEx(font As Font, text As String, fontSize, spacing As Single) As Vector2
Declare Function GetFontDefault() As Font

' --- Textures & RenderTextures ---
Declare Function LoadTexture(fileName As String) As Texture2D
Declare Sub UnloadTexture(texture As Texture2D)
Declare Sub DrawTexture(texture As Texture2D, posX, posY As Integer, tint As Color)
Declare Sub DrawTextureV(texture As Texture2D, position As Vector2, tint As Color)
Declare Sub DrawTextureEx(texture As Texture2D, position As Vector2, rotation, scale As Single, tint As Color)
Declare Sub DrawTextureRec(texture As Texture2D, source As Rectangle, position As Vector2, tint As Color)
Declare Sub DrawTexturePro(texture As Texture2D, source, dest As Rectangle, origin As Vector2, rotation As Single, tint As Color)
Declare Function LoadRenderTexture(width, height As Integer) As RenderTexture2D
Declare Sub UnloadRenderTexture(target As RenderTexture2D)

' --- 3D Shapes & Collision ---
Declare Sub DrawLine3D(startPos, endPos As Vector3, color As Color)
Declare Sub DrawPoint3D(position As Vector3, color As Color)
Declare Sub DrawCircle3D(center As Vector3, radius As Single, rotationAxis As Vector3, rotationAngle As Single, color As Color)
Declare Sub DrawCube(position As Vector3, width, height, length As Single, color As Color)
Declare Sub DrawCubeV(position, size As Vector3, color As Color)
Declare Sub DrawCubeWires(position As Vector3, width, height, length As Single, color As Color)
Declare Sub DrawCubeWiresV(position, size As Vector3, color As Color)
Declare Sub DrawSphere(centerPos As Vector3, radius As Single, color As Color)
Declare Sub DrawSphereEx(centerPos As Vector3, radius As Single, rings, slices As Integer, color As Color)
Declare Sub DrawSphereWires(centerPos As Vector3, radius As Single, rings, slices As Integer, color As Color)
Declare Sub DrawCylinder(position As Vector3, radiusTop, radiusBottom, height As Single, sides As Integer, color As Color)
Declare Sub DrawCylinderEx(startPos, endPos As Vector3, startRadius, endRadius As Single, sides As Integer, color As Color)
Declare Sub DrawCylinderWires(position As Vector3, radiusTop, radiusBottom, height As Single, sides As Integer, color As Color)
Declare Sub DrawCapsule(startPos, endPos As Vector3, radius As Single, rings, slices As Integer, color As Color)
Declare Sub DrawPlane(centerPos As Vector3, size As Vector2, color As Color)
Declare Sub DrawRay(ray As Ray, color As Color)
Declare Sub DrawGrid(slices As Integer, spacing As Single)
Declare Sub DrawBoundingBox(box As BoundingBox, color As Color)
Declare Function CheckCollisionSpheres(center1 As Vector3, radius1 As Single, center2 As Vector3, radius2 As Single) As Boolean
Declare Function CheckCollisionBoxes(box1, box2 As BoundingBox) As Boolean
Declare Function CheckCollisionBoxSphere(box As BoundingBox, center As Vector3, radius As Single) As Boolean
Declare Function GetRayCollisionSphere(ray As Ray, center As Vector3, radius As Single) As RayCollision
Declare Function GetRayCollisionBox(ray As Ray, box As BoundingBox) As RayCollision

' --- Color Utilities ---
Declare Function Fade(color As Color, alpha As Single) As Color
Declare Function ColorToInt(color As Color) As Integer
Declare Function ColorFromHSV(hue, saturation, value As Single) As Color
Declare Function ColorAlpha(color As Color, alpha As Single) As Color
Declare Function ColorTint(color, tint As Color) As Color
Declare Function ColorLerp(color1, color2 As Color, factor As Single) As Color
Declare Function GetColor(hexValue As Integer) As Color

' --- Audio ---
Declare Sub InitAudioDevice()
Declare Sub CloseAudioDevice()
Declare Function IsAudioDeviceReady() As Boolean
Declare Sub SetMasterVolume(volume As Single)
Declare Function LoadSound(fileName As String) As Sound
Declare Sub UnloadSound(sound As Sound)
Declare Sub PlaySound(sound As Sound)
Declare Sub StopSound(sound As Sound)
Declare Sub PauseSound(sound As Sound)
Declare Sub ResumeSound(sound As Sound)
Declare Function IsSoundPlaying(sound As Sound) As Boolean
Declare Sub SetSoundVolume(sound As Sound, volume As Single)
Declare Sub SetSoundPitch(sound As Sound, pitch As Single)

' --- Random & Misc ---
Declare Function GetRandomValue(minVal, maxVal As Integer) As Integer
Declare Sub SetRandomSeed(seed As Integer)
Declare Sub TakeScreenshot(fileName As String)

' --- C Math (Single-precision, linked via -lm) ---
Declare Function sqrtf(x As Single) As Single
Declare Function sinf(x As Single) As Single
Declare Function cosf(x As Single) As Single
Declare Function atan2f(y, x As Single) As Single
Declare Function fabsf(x As Single) As Single
Declare Function fmodf(x, y As Single) As Single

' --- Optional C Shim Bridges (csrc/verbose_raylib_shim.c) ---
' Bridges Raylib 5.x / 6.x API differences & provides ABI/helper utilities
Declare Sub RlShimDrawCircleGradient(cx, cy, radius As Single, inner, outer As Color)
Declare Function RlShimRandomFloat(minVal, maxVal As Single) As Single
Declare Function RlShimIntToFloat(v As Integer) As Single
Declare Function RlShimFloatToInt(v As Single) As Integer
Declare Function RlShimClampByte(v As Integer) As Byte
Declare Function RlShimVersionString() As String
Declare Function RlShimSizeOfColor() As Integer
Declare Function RlShimSizeOfVector2() As Integer
Declare Function RlShimSizeOfVector3() As Integer
Declare Function RlShimSizeOfRectangle() As Integer
Declare Function RlShimSizeOfCamera2D() As Integer
Declare Function RlShimSizeOfCamera3D() As Integer
Declare Function RlShimSizeOfRay() As Integer
Declare Function RlShimSizeOfRayCollision() As Integer
Declare Function RlShimSizeOfTexture2D() As Integer
Declare Function RlShimSizeOfRenderTexture2D() As Integer
Declare Function RlShimSizeOfFont() As Integer
Declare Function RlShimSizeOfSound() As Integer
