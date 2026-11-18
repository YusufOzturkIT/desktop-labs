# Desktop Platform Comparison: Avalonia UI vs. WPF

**Authors:** Yusuf Öztürk & Partner  
**Instructor:** Krzysztof Osada  
**Course:** Desktop Application Development  

## 1. Technology Overview
* **Avalonia UI:** An open-source (.NET Foundation / AvaloniaUI OU), cross-platform UI framework written in C#. License: MIT. Enables building applications for Windows, macOS, Linux, iOS, Android, and WebAssembly using XAML and C#.
* **WPF (Windows Presentation Foundation):** Maintained by Microsoft as an open-source component under the MIT license. Primarily designed for Windows desktop application development using XAML and C#.

## 2. Platform Comparison Matrix

| Feature / Metric | Avalonia UI | WPF (Windows Presentation Foundation) |
| :--- | :--- | :--- |
| **Supported Operating Systems** | Windows, macOS, Linux, iOS, Android, WASM | Windows only |
| **UI Description Language** | XAML (Avalonia dialect with CSS-like styling) | XAML (Standard Microsoft dialect) |
| **Architectural Pattern** | MVVM (via `CommunityToolkit.Mvvm` or ReactiveUI) | MVVM (Native binding support) |
| **Published App Size (Self-contained)** | ~60–80 MB (Cross-platform runtime bundled) | ~50–70 MB (Windows runtime bundled) |
| **Startup Time** | ~150ms – 300ms (Very snappy) | ~100ms – 250ms (Native Windows execution) |
| **Access to OS Features** | Cross-platform abstractions (File Picker, System Tray, Storage) | Direct Win32 APIs, Windows Registry, COM Interop |
| **Tooling & IDE Support** | VS Code, Rider, Visual Studio (Avalonia Extension) | Visual Studio (Full Visual Designer & Blend), Rider |
| **Community & Job Market** | Rapidly growing open-source community; strong cross-platform adoption | Established enterprise standard for Windows desktop tools |

## 3. "Hello World" Counter Implementations
Both applications implement a simple counter interface (a window containing a button and an interactive click counter).
* **AvaloniaCounter:** Built using Avalonia 11 and `CommunityToolkit.Mvvm`.
* **WpfCounter:** Built using .NET WPF.

## 4. Verdict & Recommendations
* **(a) Internal Company Tool (Windows-only):** **WPF** or **Avalonia**. WPF offers seamless Windows legacy system integration, whereas Avalonia provides a modern styling engine while keeping future cross-platform migration open.
* **(b) Cross-Platform Product:** **Avalonia UI**. It allows sharing 100% of the C# ViewModel/Model codebase across Windows, macOS, and Linux without UI rewrites.

## 5. References
1. Avalonia Documentation: https://docs.avaloniaui.net/
2. WPF Documentation: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/
