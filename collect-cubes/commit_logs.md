# Commit Logs

***
### Initial
Mon Jun 19 20:13:35 2023 +0300


feat: Add Daily_1.md file and document project planning and structures in the journal
Mon Jun 19 21:44:08 2023 +0300

refactor: Organize project file system

feat: Include HypeFire framework in the project

***
### "[add]: Create LevelData class for use in Level generator"
Tue Jun 20 02:45:34 2023 +0300

This commit introduces the creation of the LevelData class, specifically designed to be used in the Level generator. The LevelData class encapsulates information and constraints related to how levels should be generated.

Changes Made:

Added LevelData.cs class
The LevelData class provides a structured representation of level information, including details such as generation rules, constraints, and any other relevant data required for level generation.

This commit establishes the foundation for handling level-specific data within the Level generator, enabling the generation process to take into account specific information and constraints when creating levels.
***
### "[add]: MapLevelData.cs class for use in LevelGenerator"
Tue Jun 20 02:53:39 2023 +0300

This commit introduces the addition of the MapLevelData class, specifically designed for use in the LevelGenerator. The MapLevelData class is derived from the LevelData class and provides the necessary information for generating levels with object positions based on both a top-down view (topLevelMap) and a side view (sideLevelMap).

Changes Made:

Added MapLevelData.cs class
The MapLevelData class inherits from the LevelData class, allowing it to inherit and extend the functionality and constraints defined in the LevelData class. It includes the topLevelMap and sideLevelMap maps, which provide the necessary data for positioning objects within the generated levels from different perspectives.

This commit enhances the capabilities of the LevelGenerator by introducing the MapLevelData class, which allows for more flexibility in generating levels and placing objects based on both top-down and side views.
***
### "[add]: Add  ObjectData class to store generated data during level generation"
Wed Jun 21 04:32:13 2023 +0300

This commit introduces the addition of the ObjectData class, specifically designed to be used in level generators. The ObjectData class serves as a container for storing data related to objects generated during the level generation process.

Changes Made:

Added ObjectData.cs class
The ObjectData class provides properties and methods to store and access information about objects generated during the level generation process. This includes data such as object positions, properties, or any other relevant information specific to the generated objects.

By introducing the ObjectData class, this commit enhances the level generator's ability to store and manage generated data during the generation process, facilitating further manipulation or processing of the generated objects as needed.
***
### "[add]: Add BuilderBase abstract class to support polymorphism for level builders during level generation"
Wed Jun 21 04:35:23 2023 +0300

This commit introduces the addition of the BuilderBase abstract class, specifically designed to provide a level of abstraction and support polymorphism for level builders during the level generation process.

Changes Made:

Added BuilderBase.cs abstract class
The BuilderBase class serves as a foundation for level builders, allowing for different implementations and variations of level generation logic. It defines a set of common methods and properties that any derived class (specific level builders) should implement.

By introducing the BuilderBase abstract class, this commit promotes code reusability and modularity, enabling different level builders to adhere to a common interface and facilitating flexibility in selecting and using specific builders during level generation.
***
### "[add]: Add LevelData class to provide necessary information for level builders during level generation"
Wed Jun 21 04:43:15 2023 +0300

This commit introduces the addition of the LevelData class, specifically designed to provide the required information for level builders during the level generation process. The LevelData class is designed as a Scriptable object, allowing for multiple instances to be created, enabling the creation of unique data sets for each level.

Changes Made:

Added LevelData.cs class (Scriptable object)
The LevelData class serves as a central source of information for level builders, providing the necessary data and parameters required for generating levels.

By utilizing the LevelData class as a Scriptable object, this module contributes to the extensibility, flexibility, and polymorphism of level builders. It allows level builders to access and utilize different instances of LevelData, providing the ability to create unique and customized data sets for each level.
***
### "[add]: Add MapLevelData class derived from LevelData for MapLevelBuilder"
Wed Jun 21 04:51:38 2023 +0300

This commit introduces the addition of the MapLevelData class, which is derived from the LevelData class and provides the necessary data for the MapLevelBuilder. This class serves as the main data repository for generating levels based on graphical data.

Changes Made:

Added MapLevelData.cs class (derived from LevelData)
The MapLevelData class is an enhanced and tailored version of LevelData specifically designed for the MapLevelBuilder.

By introducing the MapLevelData class, this commit improves the functionality and usability of the MapLevelBuilder by providing a specialized data structure that is optimized for generating levels based on graphical data.
***
### "[add]: Add MapBuilder class derived from BuilderBase for map-based level generation"
Wed Jun 21 04:57:46 2023 +0300

This commit introduces the addition of the MapBuilder class, which is derived from the BuilderBase class, serving as a specialized level builder module for map-based level generation. Being derived from BuilderBase, it can be utilized by LevelGenerators.

Changes Made:

Added MapBuilder.cs class (derived from BuilderBase)
The MapBuilder class extends the functionalities provided by the BuilderBase class to support the generation of levels based on map or graphical data. It reads the visual data provided by MapLevelData, maps the pixels to the World Space, and provides ObjectData for the objects to be created by the LevelGenerator based on the colored data in the image, excluding white.

By introducing the MapBuilder class, this commit enhances the capabilities of the level generation process, allowing for the generation of levels based on visual representations and providing a structured ObjectData for the creation of objects based on the color information present in the image.
***
### "[add]: Add LevelGenerator abstract class to define a base specification for level generation and support polymorphism"
Wed Jun 21 05:06:06 2023 +0300

This commit introduces the addition of the LevelGenerator abstract class, which serves as a foundational definition for classes responsible for level generation. It provides a common interface for derived classes and supports polymorphism.

Changes Made:

Added LevelGenerator.cs abstract class
The LevelGenerator abstract class defines the basic structure and requirements for level generation. It includes the declaration of the Generate() and Clean() methods, which derived classes must implement. The Generate() method is responsible for generating the level, while the Clean() method is used to clean up any resources or temporary data associated with the generation process.

By introducing the LevelGenerator abstract class, this commit promotes code reusability, modularity, and flexibility in level generation. It allows for different implementations of level generation logic while adhering to a common interface.
***
### "[add]: Add IObjectController interface for enhanced data management and functionality of generated objects during level generation"
Wed Jun 21 05:11:04 2023 +0300

This commit introduces the addition of the IObjectController interface, specifically designed to simplify data management and enhance the functionality of generated objects during level generation.

Changes Made:

Added IObjectController.cs interface

This interface allow for setting the color, local position, and size of the objects generated during level generation.

By introducing the IObjectController interface, this commit improves the overall control and customization options for managing the generated objects during level generation, enhancing their visual appearance and behavior.
***
### "[add]: Add ObjectController class implementing the IObjectController interface to provide functionality"
Wed Jun 21 05:28:52 2023 +0300

This commit introduces the addition of the ObjectController class, which implements the IObjectController interface to provide the necessary functionality for managing and manipulating generated objects during level generation.

Changes Made:

Added ObjectController.cs class (implementing IObjectController)
The ObjectController class implements the methods defined in the IObjectController interface, allowing for setting the color, local position, and size of the generated objects. It provides the necessary logic and functionality to handle these operations effectively.

By adding the ObjectController class, this commit enhances the data management and functionality of the generated objects during level generation. It enables the customization and control of object properties, such as color, position, and size, providing greater flexibility and customization options.
***
### "[add]: Add ObjectControllerWithPool class derived from ObjectController, implementing IPoolObject<T> interface for object pooling support"
Wed Jun 21 05:32:51 2023 +0300

This commit introduces the addition of the ObjectControllerWithPool class, which is derived from the ObjectController class and implements the IPoolObject<T> interface to support the object pool pattern. The T parameter is passed as the class itself.

Changes Made:

Added ObjectControllerWithPool.cs class (derived from ObjectController, implementing IPoolObject<T>)
The ObjectControllerWithPool class extends the functionality provided by the ObjectController class and adds support for the object pool pattern. By implementing the IPoolObject<T> interface, it contributes to performance optimization by efficiently managing and reusing objects during level generation.

The IPoolObject<T> interface ensures that the ObjectControllerWithPool class can be added to and managed by an object pool, allowing for the efficient creation, retrieval, and recycling of objects. The T parameter refers to the class itself, enabling the object pool to work with instances of the ObjectControllerWithPool class.

By adding the ObjectControllerWithPool class, this commit enhances performance and resource management during level generation, making use of the object pool pattern to optimize object creation and utilization.
***
### "[add]: Add MapGenerator class derived from LevelGeneratorBase, utilizing MapBuilder as the builder"
Wed Jun 21 05:39:00 2023 +0300

This commit introduces the addition of the MapGenerator class, which is derived from the LevelGeneratorBase abstract class. The MapGenerator class serves as a specialized level generator and utilizes the MapBuilder as its builder component. Consequently, it requires the MapLevelData Scriptable Object to function properly.

Changes Made:

Added MapGenerator.cs class (derived from LevelGeneratorBase)
The MapGenerator class inherits the functionality and methods provided by the LevelGeneratorBase abstract class. It implements the necessary logic for generating levels and orchestrating the process.

The MapGenerator class specifically uses the MapBuilder as the builder component, which allows for the generation of levels based on the graphical data provided by the MapLevelData Scriptable Object. This enables the MapGenerator to create levels according to the visual representation and rules defined in the MapLevelData.

By introducing the MapGenerator class, this commit enhances the level generation capabilities by providing a specialized generator that utilizes the MapBuilder and requires the MapLevelData Scriptable Object for its operations.
***
### "[add]: Add MapGeneratorEditor class for managing the inspector window and providing editor code for MapGenerator"
Wed Jun 21 05:41:51 2023 +0300

This commit introduces the addition of the MapGeneratorEditor class, which is responsible for managing the inspector window and providing editor code specifically designed for the MapGenerator class.

Changes Made:

Added MapGeneratorEditor.cs class
***
### "[add]: Add map-generator-test scene for testing MapGenerator functionality"
Wed Jun 21 05:44:34 2023 +0300

This commit introduces the addition of the "map-generator-test" scene, which is specifically created for testing the functionality of the MapGenerator class.

Changes Made:

Added "map-generator-test" scene
The "map-generator-test" scene provides an environment dedicated to testing and validating the MapGenerator's capabilities. It allows for the generation and inspection of levels using the MapGenerator class, ensuring that the generated levels align with the provided visual map data.

Additionally, the necessary visual map data is included within the scene. This data serves as the input for the MapGenerator, enabling it to generate levels based on the specified graphical information.
***
### "[docs]: Add documentation file for ObjectData class (doc_1_ObjectData)"
Wed Jun 21 05:47:34 2023 +0300

This commit introduces the addition of the documentation file for the ObjectData class. The documentation file, named "doc_1_ObjectData," provides detailed information and explanations about the usage, functionality, and properties of the ObjectData class.

Changes Made:

Added "doc_1_ObjectData" documentation file

### "docs: Update documentation for ObjectData class (doc_1_ObjectData)"
Wed Jun 21 05:52:01 2023 +0300

This commit introduces updates to the documentation file for the ObjectData class, named "doc_1_ObjectData." The documentation has been revised and improved to provide more accurate and comprehensive information about the ObjectData class.

Changes Made:

Updated "doc_1_ObjectData" documentation file
***
### "[docs]: Add documentation file for MapBuilder class "doc_5_MapBuilder)"
Wed Jun 21 06:06:29 2023 +0300

This commit introduces the addition of the documentation file for the MapBuilder class. The documentation file, named "doc_5_MapBuilder," provides detailed information and explanations about the usage, functionality, and properties of the MapBuilder class.

Changes Made:

Added "doc_5_MapBuilder" documentation file
***
### "[docs]: Add documentation file for ObjectController class (doc_6_ObjectController)"
Wed Jun 21 06:14:38 2023 +0300

This commit introduces the addition of the documentation file for the ObjectController class. The documentation file, named "doc_6_ObjectController," provides detailed information and explanations about the usage, functionality, and properties of the ObjectController class.

Changes Made:

Added "doc_6_ObjectController" documentation file
***
### "[add]: Added development transcription for the second day in the Development Transcription folder"
Wed Jun 21 06:32:44 2023 +0300

This commit introduces the addition of the development transcription for the second day in the "Development Transcription" folder. The transcription contains a detailed record of the activities and progress made during the second day of development.

Changes Made:

Added development transcription for the second day
Placed the transcription in the "Development Transcription" folder
***
### "[add]: Added DoTween library to the project"
Fri Jun 23 04:07:00 2023 +0300

***
### "[add]]: Added assets provided by Alictus to the project"
Fri Jun 23 04:08:03 2023 +0300

***
### "[add]]: Added VFX (Visual Effects) to the project"
Fri Jun 23 04:08:44 2023 +0300

***
### "[refactor]: Updated PositionalConstraint.cs for positional constraint functionality"
Fri Jun 23 04:10:46 2023 +0300

***
### "[add]]: Added PlayerData class for storing player speed information"
Fri Jun 23 04:14:31 2023 +0300

This commit introduces the addition of the PlayerData class, designed as a scriptable object, to store the player's speed information based on Alictus's request.

Changes Made:

Added PlayerData.cs scriptable object
Organized PlayerData class to store player speed information
***
### "[refactor]: Updated PlayerController.cs to manage player movement and interaction with in-game objects"
Fri Jun 23 04:24:35 2023 +0300

This commit introduces updates to the PlayerController.cs script, which serves as a class responsible for handling player movement and interaction with in-game objects. Additionally, it includes an Observer container to track the state of the level. The level state can be either active or paused.

Changes Made:

Updated PlayerController.cs script
The updates made to the PlayerController.cs script enhance the player's movement capabilities and provide robust functionality for interacting with various objects within the game. The script now efficiently manages player input, enabling smooth and responsive movement controls.

Furthermore, the inclusion of the Observer container allows the PlayerController to track the state of the level. It can detect whether the level is currently active or paused, allowing the player's actions and interactions to be synchronized accordingly.
***
### "[feat]: Added CollectorHandle class for handling object collection"
Fri Jun 23 04:31:12 2023 +0300

This commit introduces the addition of the CollectorHandle class, which is responsible for handling object collection within the game.

Changes Made:

Added CollectorHandle.cs class
The CollectorHandle class provides functionality to manage the collection of game objects. It includes methods and logic to detect and handle the collection process when the player interacts with collectible objects.
***
### "[feat]: Added Collector class for storing and tracking collected objects"
Fri Jun 23 04:35:30 2023 +0300

This commit introduces the addition of the Collector class, which is responsible for storing and tracking collected objects by players.

Changes Made:

Added Collector.cs class
The Collector class provides functionality to store collected objects and keeps track of the number of collected objects. It includes methods to add objects to the collection and perform checks to determine if all objects have been collected within the game.

By adding the Collector class, this commit enables the game to implement a system for players to collect objects and provides a mechanism for tracking the progress of object collection. This can be used for various purposes such as completing objectives, unlocking rewards, or progressing through the game's narrative.
***
### "[feat]: Added AIPlayer class for managing AI logic in Bot Players"
Fri Jun 23 04:47:31 2023 +0300

This commit introduces the addition of the AIPlayer class, designed to handle AI logic for Bot Players in the game. The AIPlayer class allows for the implementation of a scalable AI system, which can evaluate and make decisions based on a rating system.

Changes Made:

Added AIPlayer.cs class
By adding the AIPlayer class, this commit enables the game to incorporate intelligent and dynamically responsive Bot Players that can navigate the game environment, make decisions based on their assigned ratings, and engage in object collection activities.
***
### "[add]]: Added AICollectorHandle class for handling object collection in AI Players"
Fri Jun 23 04:50:36 2023 +0300

This commit introduces the addition of the AICollectorHandle class, which is specifically designed to handle object collection for AI Players.

Changes Made:

Added AICollectorHandle.cs class
The AICollectorHandle class provides functionality to manage the object collection process for AI Players. It includes methods and logic tailored to AI-controlled players, allowing them to collect objects efficiently and effectively during gameplay.

By adding the AICollectorHandle class, this commit enhances the AI Player's ability to interact with the game environment and collect objects based on their AI-controlled decision-making process. It ensures that AI Players can make informed choices when it comes to collecting objects, contributing to more realistic and engaging gameplay experiences.
***
### "[add]: Added UIController class for managing in-game UI"
Fri Jun 23 04:52:47 2023 +0300

This commit introduces the addition of the UIController class, which is responsible for managing the user interface elements within the game.
***
### "[add]: Added TextMeshPro package"
Fri Jun 23 04:53:46 2023 +0300

This commit includes the addition of the TextMeshPro package to the project.
***
### "[add]: Added CameraPositionSetter class for dynamic camera positioning"
Fri Jun 23 04:55:28 2023 +0300

This commit introduces the addition of the CameraPositionSetter class, which is responsible for dynamically managing the camera position in the game.
***
### "[add]: Added TimedGenerator class for timed object spawning"
Fri Jun 23 05:00:09 2023 +0300

This commit introduces the addition of the TimedGenerator class, which is responsible for spawning objects based on timed intervals in the game.

Changes Made:

Added TimedGenerator.cs class
The TimedGenerator class is derived from the LevelGeneratorBase class and provides functionality to spawn objects at regular timed intervals. It allows developers to configure the timing and frequency of object spawns, providing a dynamic and interactive gameplay experience.

By adding the TimedGenerator class, this commit enhances the level generation capabilities of the game by introducing a time-based approach to object spawning. This feature adds diversity and challenges to the gameplay, creating a more engaging and entertaining experience for players.
***
### "[refactor]: Restructured MapGenerator class for improved handling of MapLevelData"
Fri Jun 23 05:04:26 2023 +0300

This commit involves a refactoring of the MapGenerator class to better handle the MapLevelData within the game.
***
### "[add]: Added MapGeneratorEditor class to manage the editor structure for MapGenerator"
Fri Jun 23 05:06:03 2023 +0300

This commit introduces the addition of the MapGeneratorEditor class, which is responsible for managing the editor structure of the MapGenerator.
***
### "[refactor]: Restructured LevelData class for improved structure"
Fri Jun 23 05:07:56 2023 +0300

***
### "[refactor]: Formatted MapLevelData class for improved readability"
Fri Jun 23 05:10:20 2023 +0300

This commit focuses on the formatting of the MapLevelData class to enhance its readability and maintainability.

Changes Made:

Formatted MapLevelData.cs class
***
### "[add]: Add TimedLevelData class for TimedGenerator"
Fri Jun 23 05:25:57 2023 +0300

This commit introduces the TimedLevelData class, which serves as the data source for the TimedGenerator. The TimedLevelData class provides the necessary information for generating timed levels.

Changes Made:

Added TimedLevelData.cs class
***
### "[refactor]: Update MapBuilder class formatting"
Fri Jun 23 05:29:13 2023 +0300

This commit refactors the MapBuilder class, applying formatting improvements to enhance readability and maintainability.
***
### "[add]: Add TimedBuilder class for TimedGenerator"
Fri Jun 23 05:31:46 2023 +0300

This commit introduces the TimedBuilder class, which is specifically designed for use with the TimedGenerator. The TimedBuilder handles data processing and provides the necessary data for generating timed levels.

The TimedBuilder class inherits from the BuilderBase class and implements the Build method, which constructs the object data based on the provided TimedLevelData. The Build method calculates the spawn points, object counts, and colors, and generates ObjectData instances accordingly.

The TimedBuilder class ensures that the desired number of objects is generated within the specified spawn range. It distributes the objects evenly among the spawn points and assigns colors based on the provided color list.

In cases where the number of generated objects is less than the target object count, the TimedBuilder class adds additional objects using the same logic.

This commit marks the TimedBuilder as built and returns the generated objects as an array of ObjectData.

The TimedBuilder class provides an additional constructor that takes a TimedLevelData parameter, allowing for convenient initialization with the required data.

This new class enhances the functionality of the TimedGenerator by handling the data processing and providing a structured approach to generating timed levels.
***
### "[add]: Add LevelManager class with Observer Pattern support"
Fri Jun 23 05:35:39 2023 +0300

The LevelManager class is responsible for managing the game levels and facilitating communication between different components. It implements the PublisherContainer class to support the Observer Pattern.

The LevelManager class provides a centralized way to manage level states, notify observers about level events, and control level progression.
***
### "[add]: Update ObjectController class for managing collectible objects and physics"
Fri Jun 23 05:39:24 2023 +0300

The ObjectController class is responsible for managing collectible objects and their physics interactions in the game. It implements the IObjectController interface.

The updated ObjectController class provides the necessary functionality to manage collectible objects, handle their physics interactions, and control their appearance and behavior in the game.
***
### "[add]: Add ObjectControllerWithPool class for supporting Object Pool pattern"
Fri Jun 23 05:41:36 2023 +0300

The ObjectControllerWithPool class is a derived class of the ObjectController class. It extends the functionality of the ObjectController class by supporting the Object Pool pattern. This class is primarily used by TimedGenerators.
***
### "[add]: Added ObjectDetector class"
Fri Jun 23 05:46:20 2023 +0300

The ObjectDetector class is responsible for handling the collectability of objects by PlayerControllers and ObjectControllers.
***
### ""[update]: Update project settings for improved performance"
Fri Jun 23 05:49:13 2023 +0300

The project settings have been updated to optimize performance and enhance overall efficiency. This includes configurations such as build settings, asset import settings, script compilation options, and other relevant project settings. These updates aim to ensure a smoother and more optimized workflow for the project.
***
### "[fix]: Fix errors and update MapBuilder.cs class"
Fri Jun 23 05:51:07 2023 +0300

The MapBuilder.cs class has been updated to fix errors and address any existing issues. The necessary fixes have been implemented to ensure proper functionality and eliminate any potential bugs. The class now functions as intended, providing the expected behavior and functionality for building maps in the project.
***
### "[add]: Add map-genrator-tes scene for testing MapGenerator with Pixel to level"
Fri Jun 23 06:09:43 2023 +0300

***
### "[add]: Add level channels and enhance AI logic with configurable parameters"
Fri Jun 23 06:11:21 2023 +0300

***
### "[chore]: Update prefabs and complete project"
Fri Jun 23 06:12:50 2023 +0300

The prefabs in the project have been updated to ensure consistency and improve the overall quality of the game. Various improvements, such as optimizing the models, adjusting materials, and refining the gameplay elements, have been made to enhance the visual and interactive aspects of the game.

***
### "docs: Add code documentation for PlayerController and AIPlayer classes"
Fri Jun 23 06:36:39 2023 +0300

The PlayerController and AIPlayer classes have been updated to include comprehensive code documentation. The code documentation provides detailed explanations and descriptions of the classes, their properties, methods, and functionality. This documentation will enhance code readability, maintainability, and ease of understanding for developers working with these classes.

***
### "docs: Add development daily for Day 3"
Fri Jun 23 06:49:05 2023 +0300

