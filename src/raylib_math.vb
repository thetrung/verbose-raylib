' verbose-raylib — Idiomatic VerBose 2D/3D Vector, Rectangle & Scalar Math Helpers
Include "raylib_ffi.vb"

Public Static RL_PI As Single = 3.14159265
Public Static RL_TAU As Single = 6.28318530
Public Static RL_DEG2RAD As Single = 0.0174532925
Public Static RL_RAD2DEG As Single = 57.2957795

' --- Scalar helpers ---
Public Function RlClampF(v, lo, hi As Single) As Single
  If v < lo Then
    Return lo
  End If
  If v > hi Then
    Return hi
  End If
  Return v
End Function

Public Function RlLerpF(a, b, t As Single) As Single
  Return a + (b - a) * t
End Function

' --- Vector2 ---
Public Function Vec2(x, y As Single) As Vector2
  Dim v As Vector2 With ( x = x, y = y )
  Return v
End Function

Public Function Vec2Zero() As Vector2
  Return Vec2(0.0, 0.0)
End Function

Public Function Vec2Add(a, b As Vector2) As Vector2
  Return Vec2(a.x + b.x, a.y + b.y)
End Function

Public Function Vec2Sub(a, b As Vector2) As Vector2
  Return Vec2(a.x - b.x, a.y - b.y)
End Function

Public Function Vec2Scale(v As Vector2, s As Single) As Vector2
  Return Vec2(v.x * s, v.y * s)
End Function

Public Function Vec2Dot(a, b As Vector2) As Single
  Return a.x * b.x + a.y * b.y
End Function

Public Function Vec2LenSq(v As Vector2) As Single
  Return v.x * v.x + v.y * v.y
End Function

Public Function Vec2Len(v As Vector2) As Single
  Return sqrtf(v.x * v.x + v.y * v.y)
End Function

Public Function Vec2Dist(a, b As Vector2) As Single
  Return Vec2Len(Vec2Sub(a, b))
End Function

Public Function Vec2Norm(v As Vector2) As Vector2
  Dim l As Single = Vec2Len(v)
  If l > 0.00001 Then
    Return Vec2(v.x / l, v.y / l)
  End If
  Return Vec2Zero()
End Function

Public Function Vec2Lerp(a, b As Vector2, t As Single) As Vector2
  Return Vec2(RlLerpF(a.x, b.x, t), RlLerpF(a.y, b.y, t))
End Function

Public Function Vec2Rotate(v As Vector2, radians As Single) As Vector2
  Dim c As Single = cosf(radians)
  Dim s As Single = sinf(radians)
  Return Vec2(v.x * c - v.y * s, v.x * s + v.y * c)
End Function

' --- Vector3 ---
Public Function Vec3(x, y, z As Single) As Vector3
  Dim v As Vector3 With ( x = x, y = y, z = z )
  Return v
End Function

Public Function Vec3Zero() As Vector3
  Return Vec3(0.0, 0.0, 0.0)
End Function

Public Function Vec3Add(a, b As Vector3) As Vector3
  Return Vec3(a.x + b.x, a.y + b.y, a.z + b.z)
End Function

Public Function Vec3Sub(a, b As Vector3) As Vector3
  Return Vec3(a.x - b.x, a.y - b.y, a.z - b.z)
End Function

Public Function Vec3Scale(v As Vector3, s As Single) As Vector3
  Return Vec3(v.x * s, v.y * s, v.z * s)
End Function

Public Function Vec3Dot(a, b As Vector3) As Single
  Return a.x * b.x + a.y * b.y + a.z * b.z
End Function

Public Function Vec3Cross(a, b As Vector3) As Vector3
  Return Vec3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x)
End Function

Public Function Vec3Len(v As Vector3) As Single
  Return sqrtf(v.x * v.x + v.y * v.y + v.z * v.z)
End Function

Public Function Vec3Dist(a, b As Vector3) As Single
  Return Vec3Len(Vec3Sub(a, b))
End Function

Public Function Vec3Norm(v As Vector3) As Vector3
  Dim l As Single = Vec3Len(v)
  If l > 0.00001 Then
    Return Vec3(v.x / l, v.y / l, v.z / l)
  End If
  Return Vec3Zero()
End Function

Public Function Vec3Lerp(a, b As Vector3, t As Single) As Vector3
  Return Vec3(RlLerpF(a.x, b.x, t), RlLerpF(a.y, b.y, t), RlLerpF(a.z, b.z, t))
End Function

' --- Rectangle ---
Public Function Rect(x, y, w, h As Single) As Rectangle
  Dim r As Rectangle With ( x = x, y = y, width = w, height = h )
  Return r
End Function

Public Function RectCenter(r As Rectangle) As Vector2
  Return Vec2(r.x + r.width * 0.5, r.y + r.height * 0.5)
End Function

Public Function RectContains(r As Rectangle, p As Vector2) As Boolean
  Return p.x >= r.x And p.x <= r.x + r.width And p.y >= r.y And p.y <= r.y + r.height
End Function

Public Function RectOverlaps(a, b As Rectangle) As Boolean
  Return a.x < b.x + b.width And a.x + a.width > b.x And a.y < b.y + b.height And a.y + a.height > b.y
End Function
