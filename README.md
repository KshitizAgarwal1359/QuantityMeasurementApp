# QuantityMeasurementApp
## Project Overview
The **QuantityMeasurementApp** is a C# application designed to perform quantity measurement operations such as comparing and converting units of measurement. The application focuses on ensuring accurate comparisons between numerical values measured in various units (feet, inches, yards, etc.) while handling edge cases like null values, type safety, and floating-point precision.
The project follows **SOLID principles**, clean architecture, and test-driven development (TDD) practices to ensure production-ready, maintainable code.
## Features
- **UC1 - Feet Measurement Equality**: Compare two numerical values measured in feet for equality with proper handling of:
  - Same value comparison
  - Different value comparison
  - Null comparison
  - Type safety (different class comparison)
  - Reference equality
  - Floating-point precision using tolerance-based comparison
## Tech Stack
- **Language**: C# (.NET 10)
- **Testing Framework**: xUnit
- **Build Tool**: dotnet CLI
- **Version Control**: Git & GitHub
## How to Run
### Prerequisites
- .NET SDK 10.0 or later installed
- Git installed

### Steps

1. **Clone the repository**:
   ```bash
   git clone https://github.com/kshitizagarwal1359/QuantityMeasurementApp.git
   cd QuantityMeasurementApp
   ```
2. **Build the solution**:
   ```bash
   dotnet build
   ```
3. **Run the application**:
   ```bash
   dotnet run --project QuantityMeasurementApp
   ```
4. **Run the tests**:
   ```bash
   dotnet test
   ```
## Branch Strategy
| Branch | Purpose |
|--------|---------|
| `main` | Production-ready code with documentation |
| `dev` | Integration branch for merging features |
| `feature/<feature-name>` | Individual feature branches created from `dev` |
### Workflow
1. Create feature branches from `dev` (e.g., `feature/UC1-FeetEquality`)
2. Implement and test the feature on the feature branch
3. Merge completed features into `dev`
4. Once stable, merge `dev` into `main`
## Commit Message Format
All commits follow the format:
```
[Name].Add - <descriptive message>
[Name].Refactor - <descriptive message>
```
**Examples**:
- `[Kshitiz Agarwal]Add - Implemented feet measurement equality comparison`
- `[Kshitiz Agarwal]Refactor - Improved Feet class equals method structure`
## Code Standards
- Proper indentation and formatting
- Clean architecture following SOLID principles
- Meaningful comments explaining business logic
- No unnecessary commented-out code
- Proper exception handling
- Meaningful class, method, and variable names