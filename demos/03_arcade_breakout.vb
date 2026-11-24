' Demo 03: Complete Arcade Breakout Mini-Game
' Showcases Match ... With state machine, With blocks, Arena bursts, and Raylib 2D collision!
Include "raylib.vb"

Enum GameState
  STATE_MENU = 0
  STATE_PLAYING = 1
  STATE_WON = 2
  STATE_OVER = 3
End Enum

Structure Brick
  Dim rec As Rectangle
  Dim hp As Integer
  Dim col As Color
End Structure

Private Sub ResetBricks(ByRef bricks() As Brick, cols, rows As Integer)
  Dim idx As Integer = 0
  For r = 0 To rows - 1 Do
    Dim rowCol As Color = RlCoral()
    Match r With
      Case 0 Then rowCol = RlCoral()
      Case 1 Then rowCol = RlOrange()
      Case 2 Then rowCol = RlGold()
      Case 3 Then rowCol = RlEmerald()
      Case _ Then rowCol = RlSkyBlue()
    End Match
    For c = 0 To cols - 1 Do
      With bricks(idx)
        .rec = Rect(55.0 + RlShimIntToFloat(c) * 88.0, 96.0 + RlShimIntToFloat(r) * 32.0, 80.0, 24.0)
        .hp = 1
        .col = rowCol
      End With
      idx = idx + 1
    End For
  End For
End Sub

Public Function main() As Integer
  Dim winW As Integer = 900
  Dim winH As Integer = 600
  RlOpenQuiet(winW, winH, 60, "VerBose-Raylib :: 03 Arcade Breakout")

  Dim cols As Integer = 9
  Dim rows As Integer = 5
  Dim totalBricks As Integer = cols * rows
  Dim gameArena As Arena = ArenaCreate(16384)
  Dim bricks() As Brick = ArenaAlloc(ByRef gameArena, Brick, totalBricks)
  ResetBricks(ByRef bricks, cols, rows)

  Dim state As Integer = STATE_MENU
  Dim score As Integer = 0
  Dim lives As Integer = 3
  Dim remaining As Integer = totalBricks

  Dim paddle As Rectangle = Rect(385.0, 548.0, 130.0, 16.0)
  Dim ballPos As Vector2 = Vec2(450.0, 520.0)
  Dim ballVel As Vector2 = Vec2(260.0, -320.0)
  Dim ballR As Single = 9.0

  While RlRunning() Do
    Dim dt As Single = RlClampF(GetFrameTime(), 0.001, 0.033)

    Match state With
      Case STATE_MENU Then
        If IsKeyPressed(KEY_SPACE) Or IsKeyPressed(KEY_ENTER) Then
          state = STATE_PLAYING
        End If
      Case STATE_PLAYING Then
        ' Paddle control via mouse or arrow keys
        If IsKeyDown(KEY_LEFT) Then
          paddle.x = paddle.x - 520.0 * dt
        End If
        If IsKeyDown(KEY_RIGHT) Then
          paddle.x = paddle.x + 520.0 * dt
        End If
        Dim mouse As Vector2 = GetMousePosition()
        Dim mx As Single = mouse.x
        If mx > 0.0 And mx < RlShimIntToFloat(winW) Then
          paddle.x = RlLerpF(paddle.x, mx - paddle.width * 0.5, 0.25)
        End If
        paddle.x = RlClampF(paddle.x, 12.0, RlShimIntToFloat(winW) - paddle.width - 12.0)

        ballPos = Vec2Add(ballPos, Vec2Scale(ballVel, dt))
        If ballPos.x < ballR Then
          ballPos.x = ballR
          ballVel.x = fabsf(ballVel.x)
        End If
        If ballPos.x > RlShimIntToFloat(winW) - ballR Then
          ballPos.x = RlShimIntToFloat(winW) - ballR
          ballVel.x = -fabsf(ballVel.x)
        End If
        If ballPos.y < ballR + 74.0 Then
          ballPos.y = ballR + 74.0
          ballVel.y = fabsf(ballVel.y)
        End If

        If CheckCollisionCircleRec(ballPos, ballR, paddle) And ballVel.y > 0.0 Then
          Dim hitOffset As Single = (ballPos.x - (paddle.x + paddle.width * 0.5)) / (paddle.width * 0.5)
          ballVel.x = hitOffset * 380.0
          ballVel.y = -fabsf(ballVel.y)
        End If

        For i = 0 To totalBricks - 1 Do
          If bricks(i).hp > 0 And CheckCollisionCircleRec(ballPos, ballR, bricks(i).rec) Then
            bricks(i).hp = 0
            remaining = remaining - 1
            score = score + 100
            ballVel.y = -ballVel.y
          End If
        End For

        If remaining <= 0 Then
          state = STATE_WON
        End If
        If ballPos.y > RlShimIntToFloat(winH) + 20.0 Then
          lives = lives - 1
          If lives <= 0 Then
            state = STATE_OVER
          Else
            ballPos = Vec2(paddle.x + paddle.width * 0.5, paddle.y - 20.0)
            ballVel = Vec2(240.0, -320.0)
          End If
        End If
      Case _ Then
        If IsKeyPressed(KEY_SPACE) Or IsKeyPressed(KEY_R) Then
          ResetBricks(ByRef bricks, cols, rows)
          score = 0
          lives = 3
          remaining = totalBricks
          ballPos = Vec2(450.0, 520.0)
          ballVel = Vec2(260.0, -320.0)
          state = STATE_PLAYING
        End If
    End Match

    RlBeginFrame(RlMidnight())
    ' Top HUD bar
    DrawRectangle(0, 0, winW, 70, RlSlate())
    DrawText("VERBOSE BREAKOUT", 24, 22, 24, RlCyan())
    DrawText($"SCORE: {score}", 360, 24, 22, RlGold())
    DrawText($"LIVES: {lives}", 580, 24, 22, RlCoral())
    DrawText($"LEFT: {remaining}", 740, 24, 22, RlEmerald())

    ' Bricks
    For i = 0 To totalBricks - 1 Do
      If bricks(i).hp > 0 Then
        DrawRectangleRounded(bricks(i).rec, 0.25, 6, bricks(i).col)
        DrawRectangleRoundedLinesEx(bricks(i).rec, 0.25, 6, 1.5, RlWithAlpha(RlWhite(), 0.35))
      End If
    End For

    ' Paddle & Ball
    DrawRectangleRounded(paddle, 0.45, 8, RlCyan())
    RlDrawGlowCircle(ballPos, ballR, RlRayWhite())

    Match state With
      Case STATE_MENU Then
        RlDrawPanel(Rect(240.0, 260.0, 420.0, 120.0), RlRgba(12, 16, 28, 230), RlCyan())
        RlDrawCenteredText("PRESS [SPACE] TO LAUNCH", 450, 294, 24, RlRayWhite())
        RlDrawCenteredText("Move paddle with Mouse or Left/Right Arrows", 450, 334, 16, RlSkyBlue())
      Case STATE_WON Then
        RlDrawPanel(Rect(240.0, 260.0, 420.0, 120.0), RlRgba(12, 28, 22, 235), RlEmerald())
        RlDrawCenteredText("VICTORY!", 450, 290, 30, RlEmerald())
        RlDrawCenteredText($"Final Score: {score} — Press [SPACE] to play again", 450, 334, 16, RlRayWhite())
      Case STATE_OVER Then
        RlDrawPanel(Rect(240.0, 260.0, 420.0, 120.0), RlRgba(32, 12, 16, 235), RlCoral())
        RlDrawCenteredText("GAME OVER", 450, 290, 30, RlCoral())
        RlDrawCenteredText($"Final Score: {score} — Press [SPACE] to retry", 450, 334, 16, RlRayWhite())
      Case _ Then
    End Match

    RlEndFrame()
  End While

  ArenaDestroy(ByRef gameArena)
  RlClose()
  Return 0
End Function
