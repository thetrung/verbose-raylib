// verbose-raylib — Headless Mock Backend for CI & Automated Testing (No GPU/Display Required)
#include <math.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct Vector2 { float x, y; } Vector2;
typedef struct Vector3 { float x, y, z; } Vector3;
typedef struct Vector4 { float x, y, z, w; } Vector4;
typedef struct Color { unsigned char r, g, b, a; } Color;
typedef struct Rectangle { float x, y, width, height; } Rectangle;
typedef struct Image { void *data; int width, height, mipmaps, format; } Image;
typedef struct Texture2D { unsigned int id; int width, height, mipmaps, format; } Texture2D;
typedef struct RenderTexture2D { unsigned int id; Texture2D texture; Texture2D depth; } RenderTexture2D;
typedef struct Font { int baseSize, glyphCount, glyphPadding; Texture2D texture; void *recs; void *glyphs; } Font;
typedef struct Camera2D { Vector2 offset, target; float rotation, zoom; } Camera2D;
typedef struct Camera3D { Vector3 position, target, up; float fovy; int projection; } Camera3D;
typedef struct Ray { Vector3 position, direction; } Ray;
typedef struct RayCollision { bool hit; float distance; Vector3 point, normal; } RayCollision;
typedef struct BoundingBox { Vector3 min, max; } BoundingBox;
typedef struct AudioStream { void *buffer, *processor; unsigned int sampleRate, sampleSize, channels; } AudioStream;
typedef struct Sound { AudioStream stream; unsigned int frameCount; } Sound;

static int g_frame = 0;
static int g_max_frames = 5;
static int g_draw_calls = 0;
static int g_win_w = 800;
static int g_win_h = 450;
static unsigned int g_rng = 1337;

static unsigned int next_rng(void) {
    g_rng = g_rng * 1664525u + 1013904223u;
    return g_rng;
}

void InitWindow(int w, int h, const char *title) {
    g_win_w = w;
    g_win_h = h;
    g_frame = 0;
    g_draw_calls = 0;
    printf("[headless] InitWindow(%d, %d, \"%s\")\n", w, h, title ? title : "");
}
void CloseWindow(void) {
    printf("[headless] CloseWindow (frames=%d, draw_calls=%d)\n", g_frame, g_draw_calls);
}
bool WindowShouldClose(void) {
    if (g_frame >= g_max_frames) return true;
    g_frame++;
    return false;
}
bool IsWindowReady(void) { return true; }
bool IsWindowResized(void) { return false; }
int GetScreenWidth(void) { return g_win_w; }
int GetScreenHeight(void) { return g_win_h; }
int GetRenderWidth(void) { return g_win_w; }
int GetRenderHeight(void) { return g_win_h; }
void SetWindowTitle(const char *title) { (void)title; }
void SetWindowSize(int w, int h) { g_win_w = w; g_win_h = h; }
void SetConfigFlags(unsigned int flags) { (void)flags; }
void SetTraceLogLevel(int lvl) { (void)lvl; }
void ToggleFullscreen(void) {}
void SetTargetFPS(int fps) { (void)fps; }
float GetFrameTime(void) { return 1.0f / 60.0f; }
double GetTime(void) { return (double)g_frame / 60.0; }
int GetFPS(void) { return 60; }

void BeginDrawing(void) { g_draw_calls++; }
void EndDrawing(void) {}
void ClearBackground(Color c) { (void)c; g_draw_calls++; }
void BeginMode2D(Camera2D cam) { (void)cam; g_draw_calls++; }
void EndMode2D(void) {}
void BeginMode3D(Camera3D cam) { (void)cam; g_draw_calls++; }
void EndMode3D(void) {}
void BeginBlendMode(int mode) { (void)mode; }
void EndBlendMode(void) {}
void BeginScissorMode(int x, int y, int w, int h) { (void)x; (void)y; (void)w; (void)h; }
void EndScissorMode(void) {}
void BeginTextureMode(RenderTexture2D t) { (void)t; }
void EndTextureMode(void) {}

bool IsKeyPressed(int key) { return (g_frame == 1 && key == 32); } // Space pressed on frame 1
bool IsKeyDown(int key) { (void)key; return false; }
bool IsKeyReleased(int key) { (void)key; return false; }
bool IsKeyUp(int key) { (void)key; return true; }
int GetKeyPressed(void) { return 0; }
int GetCharPressed(void) { return 0; }
void SetExitKey(int key) { (void)key; }

bool IsMouseButtonPressed(int b) { (void)b; return false; }
bool IsMouseButtonDown(int b) { (void)b; return false; }
bool IsMouseButtonReleased(int b) { (void)b; return false; }
int GetMouseX(void) { return g_win_w / 2 + g_frame * 10; }
int GetMouseY(void) { return g_win_h / 2; }
Vector2 GetMousePosition(void) { Vector2 v = { (float)(g_win_w / 2 + g_frame * 10), (float)(g_win_h / 2) }; return v; }
Vector2 GetMouseDelta(void) { Vector2 v = { 2.0f, 0.0f }; return v; }
float GetMouseWheelMove(void) { return 0.0f; }
void HideCursor(void) {}
void ShowCursor(void) {}
void DisableCursor(void) {}
void EnableCursor(void) {}

void UpdateCamera(Camera3D *cam, int mode) { (void)cam; (void)mode; }
void UpdateCameraPro(Camera3D *cam, Vector3 m, Vector3 r, float z) { (void)cam; (void)m; (void)r; (void)z; }
Ray GetScreenToWorldRay(Vector2 p, Camera3D cam) { (void)p; Ray r = { cam.position, {0,0,-1} }; return r; }
Vector2 GetWorldToScreen(Vector3 p, Camera3D cam) { (void)p; (void)cam; Vector2 v = { 400.0f, 225.0f }; return v; }
Vector2 GetScreenToWorld2D(Vector2 p, Camera2D cam) { (void)cam; return p; }
Vector2 GetWorldToScreen2D(Vector2 p, Camera2D cam) { (void)cam; return p; }

void DrawPixel(int x, int y, Color c) { (void)x; (void)y; (void)c; g_draw_calls++; }
void DrawPixelV(Vector2 p, Color c) { (void)p; (void)c; g_draw_calls++; }
void DrawLine(int x1, int y1, int x2, int y2, Color c) { (void)x1; (void)y1; (void)x2; (void)y2; (void)c; g_draw_calls++; }
void DrawLineV(Vector2 a, Vector2 b, Color c) { (void)a; (void)b; (void)c; g_draw_calls++; }
void DrawLineEx(Vector2 a, Vector2 b, float t, Color c) { (void)a; (void)b; (void)t; (void)c; g_draw_calls++; }
void DrawLineBezier(Vector2 a, Vector2 b, float t, Color c) { (void)a; (void)b; (void)t; (void)c; g_draw_calls++; }
void DrawCircle(int x, int y, float r, Color c) { (void)x; (void)y; (void)r; (void)c; g_draw_calls++; }
void DrawCircleV(Vector2 p, float r, Color c) { (void)p; (void)r; (void)c; g_draw_calls++; }
void DrawCircleLines(int x, int y, float r, Color c) { (void)x; (void)y; (void)r; (void)c; g_draw_calls++; }
void DrawCircleLinesV(Vector2 p, float r, Color c) { (void)p; (void)r; (void)c; g_draw_calls++; }
void DrawRing(Vector2 p, float ir, float orad, float sa, float ea, int seg, Color c) { (void)p; (void)ir; (void)orad; (void)sa; (void)ea; (void)seg; (void)c; g_draw_calls++; }
void DrawEllipse(int x, int y, float rh, float rv, Color c) { (void)x; (void)y; (void)rh; (void)rv; (void)c; g_draw_calls++; }
void DrawRectangle(int x, int y, int w, int h, Color c) { (void)x; (void)y; (void)w; (void)h; (void)c; g_draw_calls++; }
void DrawRectangleV(Vector2 p, Vector2 s, Color c) { (void)p; (void)s; (void)c; g_draw_calls++; }
void DrawRectangleRec(Rectangle r, Color c) { (void)r; (void)c; g_draw_calls++; }
void DrawRectanglePro(Rectangle r, Vector2 o, float rot, Color c) { (void)r; (void)o; (void)rot; (void)c; g_draw_calls++; }
void DrawRectangleLines(int x, int y, int w, int h, Color c) { (void)x; (void)y; (void)w; (void)h; (void)c; g_draw_calls++; }
void DrawRectangleLinesEx(Rectangle r, float t, Color c) { (void)r; (void)t; (void)c; g_draw_calls++; }
void DrawRectangleRounded(Rectangle r, float rn, int seg, Color c) { (void)r; (void)rn; (void)seg; (void)c; g_draw_calls++; }
void DrawRectangleRoundedLinesEx(Rectangle r, float rn, int seg, float t, Color c) { (void)r; (void)rn; (void)seg; (void)t; (void)c; g_draw_calls++; }
void DrawRectangleGradientV(int x, int y, int w, int h, Color t, Color b) { (void)x; (void)y; (void)w; (void)h; (void)t; (void)b; g_draw_calls++; }
void DrawRectangleGradientH(int x, int y, int w, int h, Color l, Color r) { (void)x; (void)y; (void)w; (void)h; (void)l; (void)r; g_draw_calls++; }
void DrawTriangle(Vector2 a, Vector2 b, Vector2 c, Color col) { (void)a; (void)b; (void)c; (void)col; g_draw_calls++; }
void DrawTriangleLines(Vector2 a, Vector2 b, Vector2 c, Color col) { (void)a; (void)b; (void)c; (void)col; g_draw_calls++; }
void DrawPoly(Vector2 p, int sides, float r, float rot, Color c) { (void)p; (void)sides; (void)r; (void)rot; (void)c; g_draw_calls++; }
void DrawPolyLines(Vector2 p, int sides, float r, float rot, Color c) { (void)p; (void)sides; (void)r; (void)rot; (void)c; g_draw_calls++; }
void DrawPolyLinesEx(Vector2 p, int sides, float r, float rot, float lt, Color c) { (void)p; (void)sides; (void)r; (void)rot; (void)lt; (void)c; g_draw_calls++; }

bool CheckCollisionRecs(Rectangle a, Rectangle b) {
    return (a.x < b.x + b.width) && (a.x + a.width > b.x) && (a.y < b.y + b.height) && (a.y + a.height > b.y);
}
bool CheckCollisionCircles(Vector2 c1, float r1, Vector2 c2, float r2) {
    float dx = c2.x - c1.x, dy = c2.y - c1.y;
    return (dx * dx + dy * dy) <= (r1 + r2) * (r1 + r2);
}
bool CheckCollisionCircleRec(Vector2 center, float radius, Rectangle rec) {
    float cx = center.x < rec.x ? rec.x : (center.x > rec.x + rec.width ? rec.x + rec.width : center.x);
    float cy = center.y < rec.y ? rec.y : (center.y > rec.y + rec.height ? rec.y + rec.height : center.y);
    float dx = center.x - cx, dy = center.y - cy;
    return (dx * dx + dy * dy) <= radius * radius;
}
bool CheckCollisionPointRec(Vector2 p, Rectangle r) {
    return p.x >= r.x && p.x <= r.x + r.width && p.y >= r.y && p.y <= r.y + r.height;
}
bool CheckCollisionPointCircle(Vector2 p, Vector2 c, float r) {
    float dx = p.x - c.x, dy = p.y - c.y;
    return (dx * dx + dy * dy) <= r * r;
}
Rectangle GetCollisionRec(Rectangle a, Rectangle b) {
    Rectangle r = { 0, 0, 0, 0 };
    if (!CheckCollisionRecs(a, b)) return r;
    float x1 = a.x > b.x ? a.x : b.x;
    float y1 = a.y > b.y ? a.y : b.y;
    float x2 = (a.x + a.width) < (b.x + b.width) ? (a.x + a.width) : (b.x + b.width);
    float y2 = (a.y + a.height) < (b.y + b.height) ? (a.y + a.height) : (b.y + b.height);
    r.x = x1; r.y = y1; r.width = x2 - x1; r.height = y2 - y1;
    return r;
}

void DrawFPS(int x, int y) { (void)x; (void)y; g_draw_calls++; }
void DrawText(const char *text, int x, int y, int fs, Color c) { (void)text; (void)x; (void)y; (void)fs; (void)c; g_draw_calls++; }
void DrawTextEx(Font f, const char *t, Vector2 p, float fs, float sp, Color c) { (void)f; (void)t; (void)p; (void)fs; (void)sp; (void)c; g_draw_calls++; }
int MeasureText(const char *text, int fs) { return text ? (int)strlen(text) * (fs / 2) : 0; }
Vector2 MeasureTextEx(Font f, const char *t, float fs, float sp) { (void)f; (void)sp; Vector2 v = { (float)MeasureText(t, (int)fs), fs }; return v; }
Font GetFontDefault(void) { Font f = { 0 }; f.baseSize = 10; return f; }

Image GenImageColor(int w, int h, Color c) { (void)c; Image img = { NULL, w, h, 1, 7 }; return img; }
void UnloadImage(Image img) { (void)img; }
Texture2D LoadTexture(const char *f) { (void)f; Texture2D t = { 1, 64, 64, 1, 7 }; return t; }
Texture2D LoadTextureFromImage(Image img) { Texture2D t = { 1, img.width, img.height, 1, img.format }; return t; }
void UnloadTexture(Texture2D t) { (void)t; }
void UpdateTexture(Texture2D t, const void *pixels) { (void)t; (void)pixels; }
void SetTextureFilter(Texture2D t, int filter) { (void)t; (void)filter; }
void DrawTexture(Texture2D t, int x, int y, Color c) { (void)t; (void)x; (void)y; (void)c; g_draw_calls++; }
void DrawTextureV(Texture2D t, Vector2 p, Color c) { (void)t; (void)p; (void)c; g_draw_calls++; }
void DrawTextureEx(Texture2D t, Vector2 p, float r, float s, Color c) { (void)t; (void)p; (void)r; (void)s; (void)c; g_draw_calls++; }
void DrawTextureRec(Texture2D t, Rectangle sr, Vector2 p, Color c) { (void)t; (void)sr; (void)p; (void)c; g_draw_calls++; }
void DrawTexturePro(Texture2D t, Rectangle s, Rectangle d, Vector2 o, float r, Color c) { (void)t; (void)s; (void)d; (void)o; (void)r; (void)c; g_draw_calls++; }
RenderTexture2D LoadRenderTexture(int w, int h) { RenderTexture2D rt = { 1, {1, w, h, 1, 7}, {2, w, h, 1, 0} }; return rt; }
void UnloadRenderTexture(RenderTexture2D rt) { (void)rt; }

void DrawLine3D(Vector3 a, Vector3 b, Color c) { (void)a; (void)b; (void)c; g_draw_calls++; }
void DrawPoint3D(Vector3 p, Color c) { (void)p; (void)c; g_draw_calls++; }
void DrawCircle3D(Vector3 p, float r, Vector3 ax, float ang, Color c) { (void)p; (void)r; (void)ax; (void)ang; (void)c; g_draw_calls++; }
void DrawCube(Vector3 p, float w, float h, float l, Color c) { (void)p; (void)w; (void)h; (void)l; (void)c; g_draw_calls++; }
void DrawCubeV(Vector3 p, Vector3 s, Color c) { (void)p; (void)s; (void)c; g_draw_calls++; }
void DrawCubeWires(Vector3 p, float w, float h, float l, Color c) { (void)p; (void)w; (void)h; (void)l; (void)c; g_draw_calls++; }
void DrawCubeWiresV(Vector3 p, Vector3 s, Color c) { (void)p; (void)s; (void)c; g_draw_calls++; }
void DrawSphere(Vector3 p, float r, Color c) { (void)p; (void)r; (void)c; g_draw_calls++; }
void DrawSphereEx(Vector3 p, float r, int ri, int sl, Color c) { (void)p; (void)r; (void)ri; (void)sl; (void)c; g_draw_calls++; }
void DrawSphereWires(Vector3 p, float r, int ri, int sl, Color c) { (void)p; (void)r; (void)ri; (void)sl; (void)c; g_draw_calls++; }
void DrawCylinder(Vector3 p, float rt, float rb, float h, int s, Color c) { (void)p; (void)rt; (void)rb; (void)h; (void)s; (void)c; g_draw_calls++; }
void DrawCylinderEx(Vector3 a, Vector3 b, float r1, float r2, int s, Color c) { (void)a; (void)b; (void)r1; (void)r2; (void)s; (void)c; g_draw_calls++; }
void DrawCylinderWires(Vector3 p, float rt, float rb, float h, int s, Color c) { (void)p; (void)rt; (void)rb; (void)h; (void)s; (void)c; g_draw_calls++; }
void DrawCapsule(Vector3 a, Vector3 b, float r, int ri, int sl, Color c) { (void)a; (void)b; (void)r; (void)ri; (void)sl; (void)c; g_draw_calls++; }
void DrawPlane(Vector3 p, Vector2 s, Color c) { (void)p; (void)s; (void)c; g_draw_calls++; }
void DrawRay(Ray r, Color c) { (void)r; (void)c; g_draw_calls++; }
void DrawGrid(int s, float sp) { (void)s; (void)sp; g_draw_calls++; }
void DrawBoundingBox(BoundingBox b, Color c) { (void)b; (void)c; g_draw_calls++; }
bool CheckCollisionSpheres(Vector3 c1, float r1, Vector3 c2, float r2) {
    float dx = c2.x - c1.x, dy = c2.y - c1.y, dz = c2.z - c1.z;
    return (dx*dx + dy*dy + dz*dz) <= (r1 + r2)*(r1 + r2);
}
bool CheckCollisionBoxes(BoundingBox a, BoundingBox b) {
    return (a.max.x >= b.min.x && a.min.x <= b.max.x) && (a.max.y >= b.min.y && a.min.y <= b.max.y) && (a.max.z >= b.min.z && a.min.z <= b.max.z);
}
bool CheckCollisionBoxSphere(BoundingBox b, Vector3 c, float r) { (void)b; (void)c; (void)r; return false; }
RayCollision GetRayCollisionSphere(Ray ray, Vector3 c, float r) { (void)ray; (void)c; (void)r; RayCollision rc = { false, 0, {0,0,0}, {0,1,0} }; return rc; }
RayCollision GetRayCollisionBox(Ray ray, BoundingBox b) { (void)ray; (void)b; RayCollision rc = { false, 0, {0,0,0}, {0,1,0} }; return rc; }

Color Fade(Color c, float a) { if (a < 0) a = 0; if (a > 1) a = 1; c.a = (unsigned char)(255.0f * a); return c; }
int ColorToInt(Color c) { return ((int)c.r << 24) | ((int)c.g << 16) | ((int)c.b << 8) | (int)c.a; }
Color ColorFromHSV(float h, float s, float v) {
    float k = fmodf(5.0f + h / 60.0f, 6.0f);
    float r = v - v * s * fmaxf(0.0f, fminf(fminf(k, 4.0f - k), 1.0f));
    k = fmodf(3.0f + h / 60.0f, 6.0f);
    float g = v - v * s * fmaxf(0.0f, fminf(fminf(k, 4.0f - k), 1.0f));
    k = fmodf(1.0f + h / 60.0f, 6.0f);
    float b = v - v * s * fmaxf(0.0f, fminf(fminf(k, 4.0f - k), 1.0f));
    Color c = { (unsigned char)(r * 255.0f), (unsigned char)(g * 255.0f), (unsigned char)(b * 255.0f), 255 };
    return c;
}
Color ColorAlpha(Color c, float a) { return Fade(c, a); }
Color ColorTint(Color c, Color t) {
    Color r = { (unsigned char)(c.r * t.r / 255), (unsigned char)(c.g * t.g / 255), (unsigned char)(c.b * t.b / 255), (unsigned char)(c.a * t.a / 255) };
    return r;
}
Color ColorLerp(Color a, Color b, float f) {
    if (f < 0) f = 0; if (f > 1) f = 1;
    Color r = {
        (unsigned char)(a.r + (b.r - a.r) * f),
        (unsigned char)(a.g + (b.g - a.g) * f),
        (unsigned char)(a.b + (b.b - a.b) * f),
        (unsigned char)(a.a + (b.a - a.a) * f)
    };
    return r;
}
Color GetColor(unsigned int hex) {
    Color c = { (unsigned char)((hex >> 24) & 0xFF), (unsigned char)((hex >> 16) & 0xFF), (unsigned char)((hex >> 8) & 0xFF), (unsigned char)(hex & 0xFF) };
    return c;
}

void InitAudioDevice(void) {}
void CloseAudioDevice(void) {}
bool IsAudioDeviceReady(void) { return true; }
void SetMasterVolume(float v) { (void)v; }
Sound LoadSound(const char *f) { (void)f; Sound s = { 0 }; return s; }
void UnloadSound(Sound s) { (void)s; }
void PlaySound(Sound s) { (void)s; }
void StopSound(Sound s) { (void)s; }
void PauseSound(Sound s) { (void)s; }
void ResumeSound(Sound s) { (void)s; }
bool IsSoundPlaying(Sound s) { (void)s; return false; }
void SetSoundVolume(Sound s, float v) { (void)s; (void)v; }
void SetSoundPitch(Sound s, float p) { (void)s; (void)p; }

int GetRandomValue(int minVal, int maxVal) {
    if (minVal >= maxVal) return minVal;
    return minVal + (int)(next_rng() % (unsigned int)(maxVal - minVal + 1));
}
void SetRandomSeed(unsigned int seed) { g_rng = seed; }
void TakeScreenshot(const char *f) { (void)f; }
bool IsFileDropped(void) { return false; }

// Shim functions for headless mode
int RlShimPollDroppedFile(char *outPath, int cap) { (void)outPath; (void)cap; return 0; }
void RlShimDrawCircleGradient(float cx, float cy, float r, Color in, Color out) { (void)cx; (void)cy; (void)r; (void)in; (void)out; g_draw_calls++; }
float RlShimRandomFloat(float lo, float hi) { return lo + (hi - lo) * ((float)(GetRandomValue(0, 10000)) / 10000.0f); }
float RlShimIntToFloat(int v) { return (float)v; }
int RlShimFloatToInt(float v) { return (int)v; }
uint8_t RlShimClampByte(int v) { return v < 0 ? 0 : (v > 255 ? 255 : (uint8_t)v); }
const char *RlShimVersionString(void) { return "headless-mock"; }
int RlShimSizeOfColor(void)           { return (int)sizeof(Color); }
int RlShimSizeOfVector2(void)         { return (int)sizeof(Vector2); }
int RlShimSizeOfVector3(void)         { return (int)sizeof(Vector3); }
int RlShimSizeOfRectangle(void)       { return (int)sizeof(Rectangle); }
int RlShimSizeOfCamera2D(void)        { return (int)sizeof(Camera2D); }
int RlShimSizeOfCamera3D(void)        { return (int)sizeof(Camera3D); }
int RlShimSizeOfRay(void)             { return (int)sizeof(Ray); }
int RlShimSizeOfRayCollision(void)    { return (int)sizeof(RayCollision); }
int RlShimSizeOfTexture2D(void)       { return (int)sizeof(Texture2D); }
int RlShimSizeOfRenderTexture2D(void) { return (int)sizeof(RenderTexture2D); }
int RlShimSizeOfFont(void)            { return (int)sizeof(Font); }
int RlShimSizeOfSound(void)           { return (int)sizeof(Sound); }
