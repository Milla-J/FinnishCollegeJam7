## The project extensively applies OOP, including:

- **Encapsulation** – Data hiding and controlled access through private fields and public properties (e.g., `MusicObject` and `SFXObject` use [SerializeField] with private backing and public getters to control audio metadata access)
- **Composition** – Combining multiple objects to build complex functionality (e.g., `AudioManager` contains lists of `MusicObject` and `SFXObject`, delegating audio playback responsibility while maintaining a dictionary of audio clips for efficient lookup)
- **Inheritance through MonoBehaviour** – All game-specific behavior extends Unity's `MonoBehaviour` (e.g., `TutorialController`, `VisualNovelController`, `MenuManager` inherit from MonoBehaviour, enabling lifecycle management and scene integration)
- **Polymorphism via Method Overloading** – Multiple versions of methods handling different types (e.g., `AudioManager.PlayAudio()` is overloaded with both `MusicType` and `SFXType` parameters, allowing a single method interface for different audio categories)
- **Abstraction** – Complex systems hidden behind simple interfaces (e.g., `VolumeController` abstracts audio mixer controls and slider event management, exposing only initialization and cleanup to consumers)
- **Delegation** – Assigning responsibilities to specialized objects (e.g., `AudioManager` delegates volume control to `VolumeController`, keeping concerns separated and reusable)

## Design Patterns used:

- **Singleton Pattern** – Global single instance for cross-scene persistence (e.g., `AudioManager` uses `public static AudioManager instance` with lazy initialization in Awake(), ensuring only one manager exists across all scenes with `DontDestroyOnLoad`)
- **Observer Pattern** – Event-driven communication between components (e.g., `VisualNovelController` publishes events like `OnSceneEnded` and `OnSentenceCompleted` that `TutorialController` subscribes to, decoupling narrative flow from scene management)
- **Strategy Pattern** – Encapsulating different audio behaviors (e.g., music vs. SFX playback uses different audio sources but the same `PlayAudio()` interface, allowing strategies to be swapped via method overloading)
- **Data Transfer Object (DTO) Pattern** – Serializable data containers for configuration (e.g., `MusicObject` and `SFXObject` serve as simple data containers with public properties, making them easy to configure in the Unity Inspector and reusable across scenes)
- **ScriptableObject Pattern** – Asset-based data management for flexible content (e.g., `StoryScene` extends `ScriptableObject` and contains a list of nested `Sentence` objects, allowing non-programmers to create narrative content through the Inspector)
- **Event Callback Pattern** – Listener registration for asynchronous responses (e.g., `VolumeController` uses `AddListener()` on UI sliders (`_musicSlider.onValueChanged.AddListener(ChangeMusicVolume)`) to respond to volume changes in real-time without tight coupling)

## Other things about the project:

- **Lazy Initialization with Null Checks** – `AudioManager` demonstrates safe initialization by checking if `instance == null` before setting it, preventing duplicate managers and ensuring thread-safe singleton creation. The separate `Initialize()` method keeps setup logic organized and testable.
- **Dictionary-Based Audio Lookup** – Instead of using switch statements or nested conditionals, `AudioManager` builds a `Dictionary<string, AudioClip>` mapping enum names to audio clips, providing O(1) lookup performance and making it easy to add new audio types without modifying core logic.
- **Enum-Driven Type Safety** – Using `MusicType` and `SFXType` enums instead of string identifiers prevents typos and provides compile-time safety. The code converts enums to strings for dictionary keys, combining type safety with dynamic lookup flexibility.
- **Coroutine-Based Sequential Gameplay** – `VisualNovelController` uses `IEnumerator` coroutines with `WaitForSeconds` to create natural pacing for text display, separating timing logic from state management and making it easy to tune animation speeds through serialized fields.
- **Composition Over Inheritance in Audio Management** – Rather than creating a complex inheritance hierarchy, `AudioManager` uses composition by holding instances of `VolumeController` and collections of data objects, making the system more flexible and easier to extend with new audio types.
- **State Machine Pattern for Text Display** – `VisualNovelController` uses a simple `State` enum (Playing/Completed) to manage the typing animation state, making state transitions explicit and preventing invalid state combinations.
- **Resource References and Sprite Swapping** – Game objects like robots use serialized sprite references to swap between visual states (regular vs. outline sprites), allowing designers to iterate on visuals without code changes and demonstrating good separation of concerns between logic and presentation.
- **Tutorial System with Positional Feedback** – `TutorialController` coordinates multiple systems (visual novel, UI outlines, scene management) and uses event-driven callbacks to show contextual help at specific narrative points, demonstrating how to compose multiple managers into a cohesive learning experience.
