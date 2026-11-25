' verbose-raylib — 100% Pure VerBose Compatibility & Utility Helpers (zero C shim!)
' Uses VerBose v0.7.1 built-in Single()/Integer()/Byte() casts and native Raylib FFI.

Declare Function snprintf(buf As Pointer, sz As Long, fmt As Pointer, ...) As Integer

Public Sub RlShimDrawCircleGradient(cx, cy, radius As Single, inner, outer As Color)
  Dim center As Vector2 With ( x = cx, y = cy )
  DrawCircleGradient(center, radius, inner, outer)
End Sub

Public Function RlShimPollDroppedFile(outPath As Pointer, cap As Integer) As Integer
  If IsNull(outPath) Or (cap <= 1) Then
    Return 0
  End If
  If Not IsFileDropped() Then
    Return 0
  End If
  Dim files As FilePathList = LoadDroppedFiles()
  Dim found As Integer = 0
  If (files.count > 0) And (Not IsNull(files.paths)) Then
    Dim firstPath As Pointer = PtrRead(files.paths, Pointer)
    If Not IsNull(firstPath) Then
      snprintf(outPath, Long(cap), "%s", firstPath)
      found = 1
    End If
  End If
  UnloadDroppedFiles(files)
  Return found
End Function

Public Function RlShimRandomFloat(minVal, maxVal As Single) As Single
  Dim r As Integer = GetRandomValue(0, 10000)
  Dim t As Single = Single(r) / 10000.0
  Return minVal + (maxVal - minVal) * t
End Function

Public Function RlShimIntToFloat(v As Integer) As Single
  Return Single(v)
End Function

Public Function RlShimFloatToInt(v As Single) As Integer
  Return Integer(v)
End Function

Public Function RlShimClampByte(v As Integer) As Byte
  If v < 0 Then
    Return Byte(0)
  End If
  If v > 255 Then
    Return Byte(255)
  End If
  Return Byte(v)
End Function

Public Function RlShimVersionString() As String
  Return "6.x-pure-vb"
End Function

' Verified 64-bit SysV/C ABI sizes of Raylib structs
Public Function RlShimSizeOfColor() As Integer
  Return 4
End Function

Public Function RlShimSizeOfVector2() As Integer
  Return 8
End Function

Public Function RlShimSizeOfVector3() As Integer
  Return 12
End Function

Public Function RlShimSizeOfRectangle() As Integer
  Return 16
End Function

Public Function RlShimSizeOfCamera2D() As Integer
  Return 24
End Function

Public Function RlShimSizeOfCamera3D() As Integer
  Return 44
End Function

Public Function RlShimSizeOfRay() As Integer
  Return 24
End Function

Public Function RlShimSizeOfRayCollision() As Integer
  Return 32
End Function

Public Function RlShimSizeOfTexture2D() As Integer
  Return 20
End Function

Public Function RlShimSizeOfRenderTexture2D() As Integer
  Return 44
End Function

Public Function RlShimSizeOfFont() As Integer
  Return 48
End Function

Public Function RlShimSizeOfSound() As Integer
  Return 40
End Function
