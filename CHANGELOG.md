# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.0.1] - 2023-05-31

### Added
- D&D features:
  - Class structure for Abilities and Skills
  - Class structure for Character
  - Class structure for Background
  - Class structure for Class, SubClass, Level, SubLevel
  - Class structure for Species
  - Class structure for Weapons
  - Class structure for Armours
  - Class structure for Equipment
  - Class structure for Feats
  - Class structure for Languages
  - Class structure for Tools
  - Barbarian class with comprehensive class features
  - Character attributes and stat management system
  - Localization and Languages support
- Camera systems:
  - Paraboloid camera controller with smooth height and angle transitions
  - Optimized camera update system for performance improvements
  - Camera input system with keyboard and mouse support
- Documentation and project structure:
  - Comprehensive documentation for game systems
  - Code organization by feature domains
- New Message Logger system with Category Generator
- Code coverage implementation for testing
- Character creation flow system
- Test Character level creation
- New Player object in Test scene
- Save/Load system for characters
- Unit tests for Save/Load functionality
- Logging system with categorized logging
- Unit tests for Barbarian class features
- Deserialization system for savegames
- Deserialization support for CharacterStat from savegames
- Comprehensive test coverage for save/load operations
- Added crosshair pointer to setup new location for navigation
- Added crosshair that keeps the location to reach

### Changed
- Refactored Message Broker with Roslyn
- Improved localization of strings in Character UI
- Refactored Save/Load classes for better maintainability
- Standardized Barbarian class features naming conventions
- Updated display name and description formatting for class features
- Optimized ParaboloidCameraController for better performance by:
- Adding early return logic to avoid unnecessary updates when no user input or transitions are detected
- Centralizing camera null checks to prevent redundant validation in multiple methods
- Separating movement calculation from interpolation for cleaner code structure
- Improving overall processing efficiency during idle periods

### Removed
- Obsolete MessageBroker generators
- Redundant null checks in ParaboloidCameraController methods

### Technical Improvements
- Enhanced code coverage implementation
- Restructured logging system with category-based approach
- Performance optimizations:
  - Improved camera controller update logic with early-exit patterns
  - Optimized rendering for game objects and UI elements
  - Reduced unnecessary computations in transform updates
- Input system improvements:
  - Implemented clean input action mapping
  - Added responsive input handling for mouse and keyboard
  - Created robust event-based input notification system
- Code architecture enhancements:
  - Implemented class inheritance hierarchies for game features
  - Created modular component structure for character features
  - Used interface-based programming for flexible feature implementation
  - Applied naming conventions consistently across codebase
  - Improved code organization with proper namespacing
- Testing infrastructure:
  - Comprehensive unit test coverage for core systems
  - Automated test runners for continuous integration

## [0.0.0] - Previous versions not documented