using System.Runtime.InteropServices;

namespace TestApp;

public static partial class TestClass
{
    private const string GameControllerLibrary = "/System/Library/Frameworks/GameController.framework/GameController";

    [LibraryImport(GameControllerLibrary)]
    internal static partial IntPtr Controllers();
}