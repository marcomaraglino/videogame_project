# Indie Survival Open World Game

## Overview

This project is a university-level software engineering exercise focused on the development of an indie survival open-world game. It demonstrates core principles in software design, documentation, and code quality, with an emphasis on maintainability and scalability.

## Table of Contents

1. [Project Description](#project-description)
2. [Installation and Setup](#installation-and-setup)
3. [Gameplay Features](#gameplay-features)
4. [Game Architecture](#game-architecture)
5. [Core Scripts and Code Review](#core-scripts-and-code-review)
6. [Contributing Guidelines](#contributing-guidelines)
7. [License](#license)

---

### Project Description

**Project Name**: Indie Survival Open World Game  
**Genre**: Survival, Open World  
**Platform**: Windows, macOS  
**Development Tools**: Unity, C#  
**Primary Goal**: Develop a minimalistic yet engaging survival experience that challenges players with resource management, exploration, and strategic combat in a dynamic environment.

---

### Installation and Setup

1. **Unity Version**: Ensure that [Unity Version 2021.3](https://unity.com/releases/editor/whats-new/2021.3) or later is installed.
2. **Clone the Repository**:
   ```bash
   git clone https://github.com/username/IndieSurvivalGame.git

### Installation and Setup

3. **Open in Unity**:
   - Launch Unity.
   - Select "Open Project" and navigate to the cloned repository.
4. **Dependencies**: Ensure that necessary Unity packages are installed (such as `Cinemachine` for camera control and `Input System` for input management).

---

### Gameplay Features

- **Resource Gathering**: Collect essential resources to survive (e.g., food, water, materials).
- **Crafting System**: Craft tools, weapons, and shelter to adapt to the environment.
- **Dynamic Day-Night Cycle**: Experience a realistic cycle with varying visibility and enemy behaviors.
- **Combat System**: Defend yourself against enemy threats using crafted weapons and tactics.
- **Exploration**: Traverse a large, procedurally generated world with diverse biomes and weather patterns.

---

### Game Architecture

The game is structured following a modular approach to ensure maintainability and scalability. Major components include:

- **Game Manager**: Oversees game state, scene transitions, and essential gameplay logic.
- **Player Controller**: Manages player input, movement, health, and interaction with the environment.
- **AI Controller**: Controls enemy behaviors, pathfinding, and interaction logic.
- **Inventory System**: Handles the storage, use, and management of resources and items.
- **Environment System**: Governs day-night cycles, weather, and resource spawning.

---

### Core Scripts and Code Review

This section includes detailed information on the core scripts involved in the project. Each script is documented with class descriptions, method summaries, and specific comments on any intricate logic. The code is designed to follow SOLID principles, with special attention to:

#### 1. InteractableObject.cs

- **Purpose**: Defines objects that the player can interact with, allowing for item names to be retrieved dynamically.
- **Key Methods**:
  - `GetItemName()`: Returns the name of the item, which can be displayed in the UI for interaction purposes.

#### 2. PlayerMovement.cs

- **Purpose**: Manages player movement, including walking, jumping, and swimming. It adapts movement based on whether the player is on the ground or in water.
- **Key Attributes**:
  - `speed`, `gravity`, `jumpHeight`: Control standard movement mechanics.
  - `swimSpeed`, `waterGravity`, `diveSpeed`: Handle movement in water, enabling a more realistic swimming experience.
- **Key Logic**:
  - **Ground Check**: Ensures the player can only jump if grounded.
  - **Water Detection**: Adjusts movement and gravity while the player is underwater, allowing for realistic swimming behavior.

#### 3. SelectionManager.cs

- **Purpose**: Handles player selection of interactable objects within a specified range.
- **Key Methods**:
  - `Update()`: Casts a ray from the camera to detect objects under the player’s mouse. If an object is interactable, it displays the item name in the UI.
- **Code Notes**:
  - Utilizes `TextMeshProUGUI` to dynamically show the object name when it’s interactable, enhancing the user experience by giving feedback on selectable items.

#### 4. WaterPhysics.cs

- **Purpose**: Simulates water physics, including drag and buoyancy, to enhance realism when the player enters or exits water.
- **Key Attributes**:
  - `waterDrag`, `waterAngularDrag`: Set the drag forces applied when the player is in water.
  - `buoyancyForce`: Determines the strength of the buoyancy effect.
- **Key Methods**:
  - `OnTriggerEnter(Collider)`: Activates water physics when the player enters water.
  - `OnTriggerExit(Collider)`: Restores original physics settings when the player leaves water.
- **Code Notes**:
  - Ensures that the buoyancy force varies based on the player’s position relative to water level, allowing for a gradual floating effect.

---

### Contributing Guidelines

For anyone interested in contributing:
- **Code Style**: Follow the [C# Style Guide](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/).
- **Documentation**: Ensure all new functions and classes are documented.
- **Pull Requests**: Submit PRs with detailed descriptions and link related issues.

---

### License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for more details.
