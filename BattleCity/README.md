# Battle City Remake - OOP Project

## Project Overview

This project is a recreation of the arcade game Battle City. The goal was to rebuild the core mechanics of the game using Object-Oriented Programming principles to create a modular, scalable, and maintainable codebase.


https://github.com/user-attachments/assets/0fc99290-7ffb-4f28-b473-edc37da8164f



https://github.com/user-attachments/assets/7eb531cc-a99b-4aa8-b710-94469cd9c5f0




# Game Features

- **Player & Enemy Mechanics:** Player control, enemy spawning, and AI movement.

- **Map System:** Grid-based map loading (Brick, Steel, Bomb, Prize).

- **Combat System:** Bullet collision detection, health management, and destruction logic.
  
- **Dual User Interface.** **Console Mode:** ASCII-based rendering of the game using string buffers and console coloring. **WinForms Mode:** A graphical interface using sprites.

- **Custom Map Importing:** The game loads levels from external text files with .lvl extension.

- **Random Map Generator:** Generates random maps and validates it with a pathfinding check to ensure every generated map is solvable and the base is reachable.

- **Scoreboard System:** Tracks player performance. Saves and loads results to a local file (serialization) to maintain data between sessions.

## Installation & Setup

Follow these steps to build and run the game on your local machine.

1.  **Clone the Repository**
    Navigate to the directory where you want to install the game and run:
    ```bash
    git clone https://github.com/rintaro129/OOPro.git
    ```

2.  **Navigate to Project Folder**
    ```bash
    cd BattleCity
    ```

3.  **Build and Run**
    Compile the project and execute the binary using the following commands:
    ```bash
    dotnet build BattleCity.csproj
    ./bin/Debug/net8.0/BattleCity
    ```

> **IMPORTANT: DO NOT MOVE THE BINARY OR RES DIRECTORY**
>
> The compiled binary relies on relative paths to locate assets (images, maps, etc.) in the `res` directory.
> * **Do not** move the executable file out of the `bin/Debug/net8.0/` folder.
> * **Do not** rename or move the `res` folder.
>
> Moving these files will break the file paths and cause the game to crash or function improperly.

# Project Structure / Architecture

### UML Class Diagram BattleCity Engine

<img width="992" height="795" alt="image" src="https://github.com/user-attachments/assets/11377036-9c9a-4faf-b251-d1f6df154cf2" />

### UML Class Diagram Entities

<img width="938" height="831" alt="image" src="https://github.com/user-attachments/assets/b4dd4175-0d8d-4d63-9222-11f6dd4f10da" />

### 1. The Core: `BaseEntity` (Abstract)
The root abstract class for all objects in the game grid.

* **State Management:** Encapsulates core properties like Coordinates (`X`, `Y`), `Direction`, `Health`, and `SpeedTicks` (movement timing).
* **Event-Driven Design:** Exposes C# Events (`Created`, `Moved`, `Died`) to notify the rendering engine without tight coupling.
* **Physics:** Handles collision checks via methods like `CheckPositionIsSolid` and `CheckMovePosition`.
* **Polymorphism:** Defines virtual/abstract methods (`ProcessTurn`, `OnDied`) that must be implemented by children.

### 2. The Tank Hierarchy
The `Tank` class serves as an abstract base for all moving combatants, handling shared mechanics like `Shoot()` and `TakeDamage()`.

* **Player Tank:**
    * Inherits from `Tank`.
    * Adds logic for **Scoring** (`Score`, `ScoreAdd`) and UI updates via the `StatsUpdated` event.
* **Enemy AI (Inheritance Chain):**
    * **`EnemyLvl1`:** Random behavior.
    * **`EnemyLvl2`:** Random behavior with more health.
    * **`EnemyLvl3`:** Advanced AI. It implements **Dijkstra's Algorithm** (`GetClosestDestinationDijkstra`) to pathfind toward the player.

### 3. Combat & Projectiles
* **`Bullet`:** Handles projectile movement and collision. It maintains a reference to its owner (`Tank`) to assign kills to the player and to avoid spamming with bullets.
* **`Explosion`:** A temporary entity managed by `ProcessTurn` to render visual feedback before destroying itself.
* **`Spawn`:** Handles the logic for introducing new entities or prizes into the grid.

### 4. Obstacles & Environment
The `Obstacle` class groups static and interactive map elements.

* **Walls:** `BrickWall` (destructible) and `SteelWall` (indestructible).
* **Interactive Items:**
    * **`Bomb`:** An obstacle that triggers an `OnDied` event damaging nearby entities.
    * **`Prize` (Abstract):** A collectible item pattern.
        * Uses **Polymorphism** for the `GrantPrize()` method.
        * **`PrizeHealth`**: Restores HP.
        * **`PrizeSpeed`**: Increases update tick rate.
        * **`PrizeFreeze`**: Stops enemy movement.

<img width="769" height="282" alt="image" src="https://github.com/user-attachments/assets/ce369226-91f6-4100-befa-4e997afff7a9" />


# Tech Stack

- Language: C#

- Framework: .NET Core / .NET Framework

- GUI: Windows Forms (WinForms)

# Controls
| Action | Key Binding |
| :--- | :---: |
| **Move Up** | `↑` Up Arrow |
| **Move Down** | `↓` Down Arrow |
| **Move Left** | `←` Left Arrow |
| **Move Right** | `→` Right Arrow |
| **Shoot** | `S` |
| **Exit Game** | `Esc` |

# Screenshots
### Console UI
<img width="1021" height="1154" alt="image" src="https://github.com/user-attachments/assets/ab4a600d-9a6c-4599-9c9a-80a206f96d03" />

### GUI
<img width="1184" height="793" alt="image" src="https://github.com/user-attachments/assets/fb55bd6a-5064-4a1f-a892-406d72f11ad3" />

### Level1.lvl
<img width="369" height="312" alt="image" src="https://github.com/user-attachments/assets/aeaa094c-ea93-4588-9238-8e690115e9fe" />


