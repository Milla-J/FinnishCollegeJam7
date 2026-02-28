# **🎮 Description:**
### **Project made for the Finnish Game Jam #7** (game's itch.io page: [click here](https://theio.itch.io/robo-rux))
#### **Winner of a Finnish Game Jam Awards 2024, Duct Tape Fixes All Award**
The game is a time-constrained repair simulator. You play as a technician who is tasked with restoring malfunctioning robots using a limited set of tools. You must use your own dexterity and avoid touching the robot's walls. All of this takes place while the robot moves along the conveyor belt.
Each defect requires a specific tool. Using the wrong tool has no effect.

## Core Gameplay Loop:
1️⃣ Select a damaged robot.
2️⃣ Identify visible faults.
3️⃣ Select an appropriate tool.
4️⃣Position and rotate the tool correctly.
4️⃣ Apply repair.
Repeat under time pressure.

Failure conditions are spatial (touching walls) and temporal (running out of time).

## ⚡️ The project extensively applies OOP, including:
- **Encapsulation** – Data hiding and controlled access through private fields and public properties (e.g., `MusicObject` and `SFXObject` use [SerializeField] with private backing and public getters to control audio metadata access)
- **Composition** – Combining multiple objects to build complex functionality (e.g., `Labyrinth` contains lists of `ProblemPlace` and `Problem` objects; `GameplayLoopController` orchestrates `RobotsPool`, `LabyrinthPool`, `RobotsExit` and manages game flow; `PopUpController` coordinates `Robot` and `Labyrinth` interactions for UI state)
- **Inheritance through MonoBehaviour** – All game-specific behavior extends Unity's `MonoBehaviour` (e.g., `TutorialController`, `VisualNovelController`, `MenuManager` inherit from MonoBehaviour, enabling lifecycle management and scene integration)
- **Polymorphism via Method Overloading** – Multiple versions of methods handling different types (e.g., `AudioManager.PlayAudio()` is overloaded with both `MusicType` and `SFXType` parameters, allowing a single method interface for different audio categories)
- **Polymorphism via Generic Methods** – Reusable algorithms that work with any type (e.g., `UsefulStuff.ShuffleList<T>()` and `UsefulStuff.GetRandomIndex<T>()` shuffle and randomly select from any list regardless of element type, used across `LabyrinthPool` for `Labyrinth` lists and `RobotsPool` for `Robot` lists)
- **Abstraction** – Complex systems hidden behind simple interfaces (e.g., `Labyrinth` abstracts problem management - callers only need to call `UpdateProblems(count)` without knowing about internal `ProblemPlace` allocation and `Problem` prefab selection; `GameplayLoopController` abstracts difficulty progression logic behind simple `TryToSwitchMode()`)
- **Delegation** – Assigning responsibilities to specialized objects (e.g., `AudioManager` delegates volume control to `VolumeController`, keeping concerns separated and reusable)

## ⚡️ Design Patterns used:
- **Observer Pattern** – Event-driven communication between components (e.g., `VisualNovelController` publishes events like `OnSceneEnded` and `OnSentenceCompleted` that `TutorialController` subscribes to, decoupling narrative flow from scene management)
- **Object Pool Pattern** – Reusing pre-instantiated objects to minimize garbage collection (e.g., `RobotsPool` maintains a list of pre-created `Robot` prefabs and reuses them with `TryToGetRobot()` and recycling via `AddBackToPool()`; `LabyrinthPool` manages separate pools for Easy/Normal/Hard difficulty labyrinths, recycling them based on availability)
- **Strategy Pattern** – Encapsulating different difficulty behaviors (e.g., `LabyrinthPool.GetAvailableLabyrinth()` uses a switch statement on `DifficultyLevel` to select different problem amounts and pools; `GameplayLoopController` maintains separate speed and spawn rate parameters for Easy/Normal/Hard, switching strategies as difficulty progresses)
- **ScriptableObject Pattern** – Asset-based data management for flexible content (e.g., `StoryScene` extends `ScriptableObject` and contains a list of nested `Sentence` objects, allowing non-programmers to create narrative content through the Inspector)
- **Event Callback Pattern** – Listener registration for asynchronous responses (e.g., `VolumeController` uses `AddListener()` on UI sliders (`_musicSlider.onValueChanged.AddListener(ChangeMusicVolume)`) to respond to volume changes in real-time without tight coupling)

## ⚡️Other things about the project:
- **Difficulty Progression Through Composition** – `LabyrinthPool` maintains three separate object pools (Easy, Normal, Hard) with difficulty-specific problem counts, and `GameplayLoopController` dynamically switches pools and parameters based on robots spawned. This allows smooth difficulty scaling without modifying core logic.
- **Resource References and Sprite Swapping** – Game objects like robots use serialized sprite references to swap between visual states (regular vs. outline sprites), allowing designers to iterate on visuals without code changes and demonstrating good separation of concerns between logic and presentation.
- **Tutorial System with Positional Feedback** – `TutorialController` coordinates multiple systems (visual novel, UI outlines, scene management) and uses event-driven callbacks to show contextual help at specific narrative points, demonstrating how to compose multiple managers into a cohesive learning experience.
