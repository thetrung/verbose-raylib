// verbose-raylib — Lightweight C ABI & Compatibility Shim for Raylib 5.x / 6.x
#include <raylib.h>
#include <stdint.h>
#include <stdio.h>

int RlShimPollDroppedFile(char *outPath, int cap) {
    if (!outPath || cap <= 1) return 0;
    if (!IsFileDropped()) return 0;
    FilePathList files = LoadDroppedFiles();
    int found = 0;
    if (files.count > 0 && files.paths && files.paths[0]) {
        snprintf(outPath, (size_t)cap, "%s", files.paths[0]);
        found = 1;
    }
    UnloadDroppedFiles(files);
    return found;
}

void RlShimDrawCircleGradient(float cx, float cy, float radius, Color inner, Color outer) {
#if defined(RAYLIB_VERSION_MAJOR) && RAYLIB_VERSION_MAJOR >= 6
    Vector2 center = { cx, cy };
    DrawCircleGradient(center, radius, inner, outer);
#else
    DrawCircleGradient((int)cx, (int)cy, radius, inner, outer);
#endif
}

float RlShimRandomFloat(float minVal, float maxVal) {
    int r = GetRandomValue(0, 10000);
    float t = (float)r / 10000.0f;
    return minVal + (maxVal - minVal) * t;
}

float RlShimIntToFloat(int v) {
    return (float)v;
}

int RlShimFloatToInt(float v) {
    return (int)v;
}

uint8_t RlShimClampByte(int v) {
    if (v < 0) return 0;
    if (v > 255) return 255;
    return (uint8_t)v;
}

const char *RlShimVersionString(void) {
#ifdef RAYLIB_VERSION
    return RAYLIB_VERSION;
#else
    return "5.x";
#endif
}

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
