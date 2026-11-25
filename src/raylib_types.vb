' verbose-raylib — Core ABI-Accurate Raylib 5.x / 6.x Structs & Enums
' Layout verified against C raylib.h via sizeof() assertions in tests/test_raylib_wrapper.vb

Public Structure Vector2
  Dim x, y As Single
End Structure

Public Structure Vector3
  Dim x, y, z As Single
End Structure

Public Structure Vector4
  Dim x, y, z, w As Single
End Structure

Public Structure Matrix
  Dim m0, m4, m8, m12 As Single
  Dim m1, m5, m9, m13 As Single
  Dim m2, m6, m10, m14 As Single
  Dim m3, m7, m11, m15 As Single
End Structure

Public Structure Color
  Dim r, g, b, a As Byte
End Structure

Public Structure Rectangle
  Dim x, y, width, height As Single
End Structure

Public Structure Image
  Dim data As Pointer
  Dim width, height, mipmaps, format As Integer
End Structure

Public Structure Texture2D
  Dim id, width, height, mipmaps, format As Integer
End Structure

Public Structure RenderTexture2D
  Dim id As Integer
  Dim texture As Texture2D
  Dim depth As Texture2D
End Structure

Public Structure NPatchInfo
  Dim source As Rectangle
  Dim left, top, right, bottom, layout As Integer
End Structure

Public Structure GlyphInfo
  Dim value, offsetX, offsetY, advanceX As Integer
  Dim image As Image
End Structure

Public Structure Font
  Dim baseSize, glyphCount, glyphPadding As Integer
  Dim texture As Texture2D
  Dim recs As Pointer
  Dim glyphs As Pointer
End Structure

Public Structure Camera2D
  Dim offset, target As Vector2
  Dim rotation, zoom As Single
End Structure

Public Structure Camera3D
  Dim position, target, up As Vector3
  Dim fovy As Single
  Dim projection As Integer
End Structure

Public Structure Ray
  Dim position, direction As Vector3
End Structure

Public Structure RayCollision
  Dim hit As Boolean
  Dim distance As Single
  Dim point, normal As Vector3
End Structure

Public Structure BoundingBox
  Dim min, max As Vector3
End Structure

Public Structure FilePathList
  Dim count As Integer
  Dim paths As Pointer
End Structure

Public Structure Shader
  Dim id As Integer
  Dim locs As Pointer
End Structure

Public Structure Wave
  Dim frameCount, sampleRate, sampleSize, channels As Integer
  Dim data As Pointer
End Structure

Public Structure AudioStream
  Dim buffer, processor As Pointer
  Dim sampleRate, sampleSize, channels As Integer
End Structure

Public Structure Sound
  Dim stream As AudioStream
  Dim frameCount As Integer
End Structure

Public Structure Music
  Dim stream As AudioStream
  Dim frameCount As Integer
  Dim looping As Boolean
  Dim ctxType As Integer
  Dim ctxData As Pointer
End Structure

' --- Enums ---

Public Enum ConfigFlags
  FLAG_VSYNC_HINT = 0x00000040
  FLAG_FULLSCREEN_MODE = 0x00000002
  FLAG_WINDOW_RESIZABLE = 0x00000004
  FLAG_WINDOW_UNDECORATED = 0x00000008
  FLAG_WINDOW_HIDDEN = 0x00000080
  FLAG_WINDOW_MINIMIZED = 0x00000200
  FLAG_WINDOW_MAXIMIZED = 0x00000400
  FLAG_WINDOW_UNFOCUSED = 0x00000800
  FLAG_WINDOW_TOPMOST = 0x00001000
  FLAG_WINDOW_ALWAYS_RUN = 0x00000100
  FLAG_WINDOW_TRANSPARENT = 0x00000010
  FLAG_WINDOW_HIGHDPI = 0x00002000
  FLAG_BORDERLESS_WINDOWED_MODE = 0x00008000
  FLAG_MSAA_4X_HINT = 0x00000020
  FLAG_INTERLACED_HINT = 0x00010000
End Enum

Public Enum TraceLogLevel
  LOG_ALL = 0
  LOG_TRACE = 1
  LOG_DEBUG = 2
  LOG_INFO = 3
  LOG_WARNING = 4
  LOG_ERROR = 5
  LOG_FATAL = 6
  LOG_NONE = 7
End Enum

Public Enum KeyboardKey
  KEY_NULL = 0
  KEY_APOSTROPHE = 39
  KEY_COMMA = 44
  KEY_MINUS = 45
  KEY_PERIOD = 46
  KEY_SLASH = 47
  KEY_ZERO = 48
  KEY_ONE = 49
  KEY_TWO = 50
  KEY_THREE = 51
  KEY_FOUR = 52
  KEY_FIVE = 53
  KEY_SIX = 54
  KEY_SEVEN = 55
  KEY_EIGHT = 56
  KEY_NINE = 57
  KEY_SEMICOLON = 59
  KEY_EQUAL = 61
  KEY_A = 65
  KEY_B = 66
  KEY_C = 67
  KEY_D = 68
  KEY_E = 69
  KEY_F = 70
  KEY_G = 71
  KEY_H = 72
  KEY_I = 73
  KEY_J = 74
  KEY_K = 75
  KEY_L = 76
  KEY_M = 77
  KEY_N = 78
  KEY_O = 79
  KEY_P = 80
  KEY_Q = 81
  KEY_R = 82
  KEY_S = 83
  KEY_T = 84
  KEY_U = 85
  KEY_V = 86
  KEY_W = 87
  KEY_X = 88
  KEY_Y = 89
  KEY_Z = 90
  KEY_LEFT_BRACKET = 91
  KEY_BACKSLASH = 92
  KEY_RIGHT_BRACKET = 93
  KEY_GRAVE = 96
  KEY_SPACE = 32
  KEY_ESCAPE = 256
  KEY_ENTER = 257
  KEY_TAB = 258
  KEY_BACKSPACE = 259
  KEY_INSERT = 260
  KEY_DELETE = 261
  KEY_RIGHT = 262
  KEY_LEFT = 263
  KEY_DOWN = 264
  KEY_UP = 265
  KEY_PAGE_UP = 266
  KEY_PAGE_DOWN = 267
  KEY_HOME = 268
  KEY_END = 269
  KEY_PAUSE = 284
  KEY_F1 = 290
  KEY_F2 = 291
  KEY_F3 = 292
  KEY_F4 = 293
  KEY_F5 = 294
  KEY_F6 = 295
  KEY_F7 = 296
  KEY_F8 = 297
  KEY_F9 = 298
  KEY_F10 = 299
  KEY_F11 = 300
  KEY_F12 = 301
  KEY_LEFT_SHIFT = 340
  KEY_LEFT_CONTROL = 341
  KEY_LEFT_ALT = 342
  KEY_LEFT_SUPER = 343
  KEY_RIGHT_SHIFT = 344
  KEY_RIGHT_CONTROL = 345
  KEY_RIGHT_ALT = 346
  KEY_RIGHT_SUPER = 347
End Enum

Public Enum MouseButton
  MOUSE_BUTTON_LEFT = 0
  MOUSE_BUTTON_RIGHT = 1
  MOUSE_BUTTON_MIDDLE = 2
  MOUSE_BUTTON_SIDE = 3
  MOUSE_BUTTON_EXTRA = 4
  MOUSE_BUTTON_FORWARD = 5
  MOUSE_BUTTON_BACK = 6
End Enum

Public Enum BlendMode
  BLEND_ALPHA = 0
  BLEND_ADDITIVE = 1
  BLEND_MULTIPLIED = 2
  BLEND_ADD_COLORS = 3
  BLEND_SUBTRACT_COLORS = 4
  BLEND_ALPHA_PREMULTIPLY = 5
End Enum

Public Enum CameraMode
  CAMERA_CUSTOM = 0
  CAMERA_FREE = 1
  CAMERA_ORBITAL = 2
  CAMERA_FIRST_PERSON = 3
  CAMERA_THIRD_PERSON = 4
End Enum

Public Enum CameraProjection
  CAMERA_PERSPECTIVE = 0
  CAMERA_ORTHOGRAPHIC = 1
End Enum

Public Enum TextureFilter
  TEXTURE_FILTER_POINT = 0
  TEXTURE_FILTER_BILINEAR = 1
  TEXTURE_FILTER_TRILINEAR = 2
  TEXTURE_FILTER_ANISOTROPIC_4X = 3
  TEXTURE_FILTER_ANISOTROPIC_8X = 4
  TEXTURE_FILTER_ANISOTROPIC_16X = 5
End Enum
