' verbose-raylib — Idiomatic VerBose Color Palette & Helpers
Include "raylib_ffi.vb"

Public Function RlRgba(r, g, b, a As Integer) As Color
  Dim c As Color With (
    r = RlShimClampByte(r),
    g = RlShimClampByte(g),
    b = RlShimClampByte(b),
    a = RlShimClampByte(a)
  )
  Return c
End Function

Public Function RlRgb(r, g, b As Integer) As Color
  Return RlRgba(r, g, b, 255)
End Function

Public Function RlWithAlpha(c As Color, alpha As Single) As Color
  Return ColorAlpha(c, alpha)
End Function

Public Function RlHsv(hue, sat, val As Single) As Color
  Return ColorFromHSV(hue, sat, val)
End Function

Public Function RlLerpColor(a, b As Color, t As Single) As Color
  Return ColorLerp(a, b, t)
End Function

' --- Standard Palette Constructors ---
Public Function RlBlack() As Color
  Return RlRgb(0, 0, 0)
End Function

Public Function RlWhite() As Color
  Return RlRgb(255, 255, 255)
End Function

Public Function RlRayWhite() As Color
  Return RlRgb(245, 245, 245)
End Function

Public Function RlMidnight() As Color
  Return RlRgb(14, 17, 26)
End Function

Public Function RlSlate() As Color
  Return RlRgb(30, 36, 52)
End Function

Public Function RlDarkGray() As Color
  Return RlRgb(80, 80, 80)
End Function

Public Function RlLightGray() As Color
  Return RlRgb(200, 200, 200)
End Function

Public Function RlRed() As Color
  Return RlRgb(230, 41, 55)
End Function

Public Function RlCoral() As Color
  Return RlRgb(255, 107, 107)
End Function

Public Function RlOrange() As Color
  Return RlRgb(255, 161, 0)
End Function

Public Function RlGold() As Color
  Return RlRgb(255, 203, 0)
End Function

Public Function RlYellow() As Color
  Return RlRgb(253, 249, 0)
End Function

Public Function RlGreen() As Color
  Return RlRgb(0, 228, 48)
End Function

Public Function RlEmerald() As Color
  Return RlRgb(64, 210, 140)
End Function

Public Function RlCyan() As Color
  Return RlRgb(56, 214, 242)
End Function

Public Function RlSkyBlue() As Color
  Return RlRgb(102, 191, 255)
End Function

Public Function RlBlue() As Color
  Return RlRgb(0, 121, 241)
End Function

Public Function RlPurple() As Color
  Return RlRgb(168, 92, 249)
End Function

Public Function RlMagenta() As Color
  Return RlRgb(255, 0, 255)
End Function
