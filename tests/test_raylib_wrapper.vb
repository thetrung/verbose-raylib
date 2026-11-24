' verbose-raylib — Automated Wrapper & ABI Verification Suite
' Run with headless mock:
'   verbose test tests/test_raylib_wrapper.vb -I src csrc/headless_mock.c -lm
' Or verify ABI sizes against real system libraylib:
'   verbose test tests/test_raylib_wrapper.vb -I src csrc/verbose_raylib_shim.c -lraylib -lm
Include "raylib.vb"

Test "ABI struct sizes match C raylib.h sizeof()"
  Assert SizeOf(Color) = RlShimSizeOfColor(), "Color size mismatch"
  Assert SizeOf(Vector2) = RlShimSizeOfVector2(), "Vector2 size mismatch"
  Assert SizeOf(Vector3) = RlShimSizeOfVector3(), "Vector3 size mismatch"
  Assert SizeOf(Rectangle) = RlShimSizeOfRectangle(), "Rectangle size mismatch"
  Assert SizeOf(Camera2D) = RlShimSizeOfCamera2D(), "Camera2D size mismatch"
  Assert SizeOf(Camera3D) = RlShimSizeOfCamera3D(), "Camera3D size mismatch"
  Assert SizeOf(Ray) = RlShimSizeOfRay(), "Ray size mismatch"
  Assert SizeOf(RayCollision) = RlShimSizeOfRayCollision(), "RayCollision size mismatch"
  Assert SizeOf(Texture2D) = RlShimSizeOfTexture2D(), "Texture2D size mismatch"
  Assert SizeOf(RenderTexture2D) = RlShimSizeOfRenderTexture2D(), "RenderTexture2D size mismatch"
  Assert SizeOf(Font) = RlShimSizeOfFont(), "Font size mismatch"
  Assert SizeOf(Sound) = RlShimSizeOfSound(), "Sound size mismatch"
End Test

Test "Vector2, Vector3 and Rectangle pure-VerBose math"
  Dim a As Vector2 = Vec2(3.0, 4.0)
  Dim b As Vector2 = Vec2(1.0, 2.0)
  Dim sum2 As Vector2 = Vec2Add(a, b)
  Assert sum2.x = 4.0 And sum2.y = 6.0, "Vec2Add failed"
  Assert Vec2Len(a) > 4.99 And Vec2Len(a) < 5.01, "Vec2Len(3,4) should be 5"
  Assert Vec2Dot(a, b) = 11.0, "Vec2Dot failed"

  Dim u As Vector3 = Vec3(1.0, 0.0, 0.0)
  Dim v As Vector3 = Vec3(0.0, 1.0, 0.0)
  Dim w As Vector3 = Vec3Cross(u, v)
  Assert w.x = 0.0 And w.y = 0.0 And w.z = 1.0, "Vec3Cross failed"
  Assert Vec3Dist(u, Vec3Zero()) > 0.99 And Vec3Dist(u, Vec3Zero()) < 1.01, "Vec3Dist failed"

  Dim r1 As Rectangle = Rect(10.0, 20.0, 100.0, 50.0)
  Dim r2 As Rectangle = Rect(80.0, 40.0, 60.0, 60.0)
  Assert RectContains(r1, Vec2(50.0, 40.0)), "RectContains should be true"
  Assert Not RectContains(r1, Vec2(200.0, 40.0)), "RectContains should be false"
  Assert RectOverlaps(r1, r2), "RectOverlaps should be true"
End Test

Test "Color palette, HSV, clamping and orbital camera"
  Dim c As Color = RlRgba(300, -10, 128, 255)
  Assert c.r = 255 And c.g = 0 And c.b = 128 And c.a = 255, "RlRgba clamping failed"

  Dim red As Color = RlHsv(0.0, 1.0, 1.0)
  Assert red.r >= 250 And red.g <= 5 And red.b <= 5, "RlHsv(0,1,1) should be red"

  Dim cam As Camera3D = RlCamera3D(Vec3(0.0, 5.0, 10.0), Vec3Zero(), 45.0)
  Assert cam.projection = CAMERA_PERSPECTIVE, "Default camera projection"
  RlOrbitCamera(ByRef cam, 10.0, 0.0, 0.0)
  Assert cam.position.x > 9.99 And cam.position.x < 10.01, "Orbit cam x"
  Assert fabsf(cam.position.y) < 0.01 And fabsf(cam.position.z) < 0.01, "Orbit cam y/z"
End Test

Test "C collision FFI round-trip"
  Dim r1 As Rectangle = Rect(0.0, 0.0, 50.0, 50.0)
  Dim r2 As Rectangle = Rect(25.0, 25.0, 50.0, 50.0)
  Assert CheckCollisionRecs(r1, r2), "CheckCollisionRecs true"
  Assert CheckCollisionCircleRec(Vec2(10.0, 10.0), 5.0, r1), "CheckCollisionCircleRec true"
  Dim inter As Rectangle = GetCollisionRec(r1, r2)
  Assert inter.width = 25.0 And inter.height = 25.0, "GetCollisionRec dimensions"
End Test
