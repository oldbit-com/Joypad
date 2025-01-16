# Joypad

Joypad is a simple cross-platform game controller library for dotnet. It supports both USB and Bluetooth game controllers.

It was specifically created to be used by [Spectron](https://github.com/oldbit-com/Spectron), my ZX Spectrum emulator. 
I needed a simple way of handling game controllers and couldn't find anything that would suit my needs.
Therefore, I decided to create my own solution.

## Features
- written in C# and .NET 8
- no external dependencies other than native OS frameworks
- cross-platform, currently supports macOS, Windows and Linux
- supports USB and Bluetooth game controllers

## Platforms
- [MacOS](#MacOS)
- [Windows](#Windows)
- [Linux](#Linux)

### MacOS
Implementation is using IOHID, part of [IOKit](https://developer.apple.com/documentation/iokit) framework. Any controller that is compatible with MacOS should work.

### Windows
Implementation is using [XInput](https://docs.microsoft.com/en-us/windows/win32/xinput/xinput-technical-reference) API. This is very easy to use API, but only supports controllers that
are compatible with XInput. Therefore some older legacy controllers might not work. The alternative is to use DirectInput, 
but it is not supported by this library.

### Linux
Implementation is using [evdev](https://docs.kernel.org/input/input.html#event-interface).

## Usage
See [DemoApp](src/DemoApp) for a simple example of how to use the library.

## TODO
Normalize output values for analog controls.
