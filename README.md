# Sainim

A WPF desktop application for creating animations from layered PSD files. Transform static layered artwork into animated GIFs with precise control over frame composition.

## Features

- **PSD Layer Import**: Load PSD files and automatically parse layers into animation frames
- **Frame Management**: 
  - Combine multiple layers into single frames using numbered naming scheme
  - Preview individual frames
- **Layer Organization**:
  - **Frame Sublayers**: Numbered layers (e.g., `1_lineart`, `1_color`) that combine into single frames
  - **Static Background Layers**: Elements that remain constant throughout the animation, shown behind it
  - **Static Foreground Layers**: Static overlays applied on top of animated frames
- **Layer Type Control**: Toggle visibility of different layer types (background, foreground, specific sublayer categories)
- **Animation Playback**: Preview animations with adjustable speed and repeat settings
- **GIF Export**: 
  - Export animations as GIF files
  - Configurable frame rate and loop settings
  - Selective frame export with enabled layer types

## Layer Naming Scheme

Sainim uses layer names to determine their role in the animation:

- **Static Layers**: Simple names without separators (e.g., `background`, `overlay`)
  - Positioned before frame layers become background elements
  - Positioned after frame layers become foreground elements
- **Frame Sublayers**: Numbered format `{frameNumber}_{specialLabel}` (e.g., `1_lineart`, `1_color`, `2_lineart`)
  - Layers with the same frame number are merged into a single frame
  - Special labels allow selective layer type toggling during export

## Tech Stack

- .NET 8.0 with WPF (Windows Presentation Foundation)
- C# with MVVM architecture
- Magick.NET for image processing
- Microsoft.Extensions.Hosting for dependency injection