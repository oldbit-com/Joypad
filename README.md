# Joypad

Joypad is a simple cross-platform game controller library for dotnet. It supports both USB and Bluetooth game controllers.

It was created to be used by [Spectron](https://github.com/oldbit-com/Spectron), my ZX Spectrum emulator. 
I needed a simple way of handling game controllers and couldn't find anything that would suit my needs.
Therefore, I decided to create my own library.

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
Implementation uses IOHID, part of [IOKit](https://developer.apple.com/documentation/iokit) framework.

### Windows
TODO: Implementation uses [XInput](https://docs.microsoft.com/en-us/windows/win32/xinput/xinput-technical-reference) API.

### Linux
TODO: Implementation uses
