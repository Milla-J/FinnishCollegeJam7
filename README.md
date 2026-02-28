## ⚡️ The project extensively applies OOP, including:

- **Encapsulation** – Data hiding and controlled access through private fields and public properties (e.g., `MusicObject` and `SFXObject` use [SerializeField] with private backing and public getters to control audio metadata access)
- **Composition** – Combining multiple objects to build complex functionality (e.g., `AudioManager` contains lists of `MusicObject` and `SFXObject`, delegating audio playback responsibility while maintaining a dictionary of audio clips for efficient lookup)
- **Inheritance through MonoBehaviour** – All game-specific behavior extends Unity's `MonoBehaviour` (e.g., `TutorialController`, `VisualNovelController`, `MenuManager` inherit from MonoBehaviour, enabling lifecycle management and scene integration)
- **Polymorphism via Method Overloading** – Multiple versions of methods handling different types (e.g., `AudioManager.PlayAudio()` is overloaded with both `MusicType` and `SFXType` parameters, allowing a single method interface for different audio categories)
- **Abstraction** – Complex systems hidden behind simple interfaces (e.g., `VolumeController` abstracts audio mixer controls and slider event management, exposing only initialization and cleanup to consumers)
- **Delegation** – Assigning responsibilities to specialized objects (e.g., `AudioManager` delegates volume control to `VolumeController`, keeping concerns separated and reusable)

## ⚡️ Design Patterns used:
- **Observer Pattern** – Event-driven communication between components (e.g., `VisualNovelController` publishes events like `OnSceneEnded` and `OnSentenceCompleted` that `TutorialController` subscribes to, decoupling narrative flow from scene management)
- **Strategy Pattern** – Encapsulating different audio behaviors (e.g., music vs. SFX playback uses different audio sources but the same `PlayAudio()` interface, allowing strategies to be swapped via method overloading)
- **ScriptableObject Pattern** – Asset-based data management for flexible content (e.g., `StoryScene` extends `ScriptableObject` and contains a list of nested `Sentence` objects, allowing non-programmers to create narrative content through the Inspector)
- **Event Callback Pattern** – Listener registration for asynchronous responses (e.g., `VolumeController` uses `AddListener()` on UI sliders (`_musicSlider.onValueChanged.AddListener(ChangeMusicVolume)`) to respond to volume changes in real-time without tight coupling)

## ⚡️Other things about the project:
- **Resource References and Sprite Swapping** – Game objects like robots use serialized sprite references to swap between visual states (regular vs. outline sprites), allowing designers to iterate on visuals without code changes and demonstrating good separation of concerns between logic and presentation.
- **Tutorial System with Positional Feedback** – `TutorialController` coordinates multiple systems (visual novel, UI outlines, scene management) and uses event-driven callbacks to show contextual help at specific narrative points, demonstrating how to compose multiple managers into a cohesive learning experience.
