# Sainim

A WPF desktop application that converts layered PSD artwork into animated GIFs. Sainim automatically combines multiple layers into single animation frames based on a simple naming convention.

## What It Does

Sainim takes your layered PSD files and turns them into animated GIFs. The key feature: **multiple layers can be merged into a single animation frame** using a numbered naming system.

### Example
If you have layers named:
- `1_lineart` (the outline)
- `1_color` (the colors)
- `2_lineart` (next frame's outline)
- `2_color` (next frame's colors)

Sainim will create a 2-frame animation where frame 1 combines `1_lineart` + `1_color`, and frame 2 combines `2_lineart` + `2_color`.

## Features

- **PSD Import**: Automatically parses layered PSD files into animation frames
- **Smart Layer Organization**:
  - **Frame Sublayers**: Layers named `{number}_{label}` (like `1_lineart`, `1_color`) are merged into single frames based on their number
  - **Static Backgrounds**: Layers without underscores positioned below animation frames act as constant backgrounds
  - **Static Foregrounds**: Layers without underscores positioned above animation frames act as overlays
- **Playback Controls**: Preview your animation with adjustable speed and loop settings
- **GIF Export**: 
  - Configure frame rate and loop behavior
  - Selectively enable/disable layer types (e.g., export only lineart without color)
- **Localization**: Switch between English and Polish interface

## Tech Stack

- .NET 8.0 with WPF (Windows Presentation Foundation)
- C# with MVVM architecture
- Magick.NET for image processing
- Microsoft.Extensions.Hosting for dependency injection