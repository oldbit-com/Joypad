// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
namespace OldBit.Joypad.Platforms.Linux.Interop;

internal static class AbsCode
{
   /*
    * Absolute axes
    */
   internal const int ABS_X = 0x00;
   internal const int ABS_Y = 0x01;
   internal const int ABS_Z = 0x02;
   internal const int ABS_RX = 0x03;
   internal const int ABS_RY = 0x04;
   internal const int ABS_RZ = 0x05;
   internal const int ABS_THROTTLE = 0x06;
   internal const int ABS_RUDDER = 0x07;
   internal const int ABS_WHEEL = 0x08;
   internal const int ABS_GAS = 0x09;
   internal const int ABS_BRAKE = 0x0a;
   internal const int ABS_HAT0X = 0x10;
   internal const int ABS_HAT0Y = 0x11;
   internal const int ABS_HAT1X = 0x12;
   internal const int ABS_HAT1Y = 0x13;
   internal const int ABS_HAT2X = 0x14;
   internal const int ABS_HAT2Y = 0x15;
   internal const int ABS_HAT3X = 0x16;
   internal const int ABS_HAT3Y = 0x17;
   internal const int ABS_PRESSURE = 0x18;
   internal const int ABS_DISTANCE = 0x19;
   internal const int ABS_TILT_X = 0x1a;
   internal const int ABS_TILT_Y = 0x1b;
   internal const int ABS_TOOL_WIDTH = 0x1c;

   internal const int ABS_VOLUME = 0x20;
   internal const int ABS_PROFILE = 0x21;

   internal const int ABS_MISC = 0x28;

   /*
    * 0x2e is reserved and should not be used in input drivers.
    * It was used by HID as ABS_MISC+6 and userspace needs to detect if
    * the next ABS_* event is correct or is just ABS_MISC + n.
    * We define here ABS_RESERVED so userspace can rely on it and detect
    * the situation described above.
    */
   internal const int ABS_RESERVED = 0x2e;

   internal const int ABS_MT_SLOT = 0x2f; /* MT slot being modified */
   internal const int ABS_MT_TOUCH_MAJOR = 0x30; /* Major axis of touching ellipse */
   internal const int ABS_MT_TOUCH_MINOR = 0x31; /* Minor axis (omit if circular) */
   internal const int ABS_MT_WIDTH_MAJOR = 0x32; /* Major axis of approaching ellipse */
   internal const int ABS_MT_WIDTH_MINOR = 0x33; /* Minor axis (omit if circular) */
   internal const int ABS_MT_ORIENTATION = 0x34; /* Ellipse orientation */
   internal const int ABS_MT_POSITION_X = 0x35; /* Center X touch position */
   internal const int ABS_MT_POSITION_Y = 0x36; /* Center Y touch position */
   internal const int ABS_MT_TOOL_TYPE = 0x37; /* Type of touching device */
   internal const int ABS_MT_BLOB_ID = 0x38; /* Group a set of packets as a blob */
   internal const int ABS_MT_TRACKING_ID = 0x39; /* Unique ID of initiated contact */
   internal const int ABS_MT_PRESSURE = 0x3a; /* Pressure on contact area */
   internal const int ABS_MT_DISTANCE = 0x3b; /* Contact hover distance */
   internal const int ABS_MT_TOOL_X = 0x3c; /* Center X tool position */
   internal const int ABS_MT_TOOL_Y = 0x3d; /* Center Y tool position */

   internal const int ABS_MAX = 0x3f;
   internal const int ABS_CNT = (ABS_MAX + 1);
}