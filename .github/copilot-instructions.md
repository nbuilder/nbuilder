# GitHub Copilot Instructions

## 1. Project Context
- .NET project to help generate fake data for testing.
- C# 14.0 targeting .NET 10

## 2. Coding and Solution Guidelines
- Use simple, best-practice implementations
- Follow SOLID principles and modern C# patterns
- Prioritize performance and maintainability

## 3. Code Example Guidance
- Show only a few lines above and below the relevant code section
- Maintain the existing code style and patterns but mention if the code is not following best practices
- Use clear, concise comments to explain complex logic

## 4. Testing Guidelines
- Test edge cases, including permission boundaries and edge cases.
- Use Theories over Facts when testing the same logic with different data.
- Use xUnit v3 with MTP v 2+ for testing framework.

## 6. Naming Conventions
- Use PascalCase for public members, method names, and component names.
- Use camelCase for private fields and local variables and _camelCase for private readonly fields.
- Prefix interface names with "I" (e.g., IUserService).

## 7. Formatting
- Prefer file-scoped namespaces and single-line using directives.
- Follow `.editorconfig` if present.