# Pong (Unity 2D)

A simple Pong clone made in Unity, where the player faces an AI-controlled paddle.

## Download

- [Play in browser](https://dinnerbone2065.itch.io/pong) (download not necessery but please play in full screen mode)
- [Windows or Mac](https://github.com/diamorim/pong/releases/tag/v1.0.0)

## Instructions

### Windows

1. Extract the file `Pong-Windows.zip`
2. Open the Folder `Pong-Windows.zip`
3. Launch the file `Pong.exe` 

### Windows
1. Download `Pong-Windows.zip`
2. Extract the ZIP file.
3. Open the extracted folder.
4. Double-click `Pong.exe` to start the game.

### macOS
1. Download `Pong-macOS.zip` from the latest release.
2. Extract the ZIP file.
3. Move `Pong.app` to your Applications folder (optional).
4. Double-click `Pong.app` to launch the game.

**Note for macOS users:** If macOS blocks the application because it comes from an unidentified developer:

1. Open **System Settings → Privacy & Security**.
2. Scroll down to the Security section.
3. Click **Open Anyway** next to the blocked application.
4. Confirm that you want to open it.

### Browser

- Open this [link](https://dinnerbone2065.itch.io/pong)
- Press in the blue button at the bottom right corner of the game to open Fullscreen

## How to play

1. Open the game.
2. Open the main scene and press **Play**.
3. Press **Space** on the start screen to begin the match.
4. Use `W` and `S` or `up arrow` and `down arrow` to move
4. Hit the ball back and score points against the AI.
5. press `Alt + f4` to quit the game, if playing online just press esc to close the fullscreen mode.

## Script overview

| Script | Purpose |
| --- | --- |
| `GameManager` | Handles game states (start, in game, point scored, game over), `Time.timeScale`, UI screens and round resets |
| `ScoreManager` | Handles the score, it changes the score boards and store the data of the points |
| `Ball` | Moves the ball through `Rigidbody2D`, handles collisions with walls and paddles, and speeds up on|every paddle hit |
| `Player` | Handles the main player you are controlling that allow you to move up and down.
| `BasicAI` | AI-controlled paddle with three states: waiting for the start, mirroring the ball (far) and following the ball (close) |
| `IdWall` | Identifies which player each scoring wall belongs to |

## How it works

- **Game states:** `start` → `inGame` → `madePoint` → `gameOver`.
- **Scoring:** when the ball hits a scoring wall (layer 9), `GameManager` pauses the game, shows the point screen, waits for a cooldown and resets the positions.
- **Ball speed:** increases slightly on every paddle hit and returns to its original value when the round resets.
- **AI:** reacts based on its distance to the ball (`distanceToReact`) and respects a vertical movement limit (`MovingLimitation`).

## Inspector setup

- **GameManager:** assign the paddles, their original positions, the ball and the UI objects; adjust the cooldowns.
- **Ball:** set `speed`, `speedIncreasement` and `platformHitWindow`.
- **BasicAI:** set `speed`, `distanceToReact`, `MovingLimitation` and `MovingLimitationSpeed`.
- **Layers used:** 6 and 7 (lower and upper paddle hitboxes), 8 (bounce walls) and 9 (scoring walls).

## Technical notes
 
- **Why states:** I modeled the game flow (start, in game, point scored, game over) as explicit states to keep the code organized, and because it was a pattern I had never used before and wanted to learn.
- **AI:** `BasicAI` has three behaviors. It waits for the first hit, mirrors the ball's vertical position while the ball is far away, and follows the ball once it gets close. `distanceToReact` and `MovingLimitation` control how the AI behaves, so they can be tuned to change the difficulty.
- **Physics:** the ball and the paddles move through `Rigidbody2D` velocity inside `FixedUpdate`.
- **Pausing:** `Time.timeScale = 0` freezes the game between points, and `WaitForSecondsRealtime` keeps the cooldowns running while it is paused.

## Author

Diego Amorim
