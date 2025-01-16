// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
namespace OldBit.Joypad.Platforms.Linux.Interop;

internal static class KeyCode
{
	internal const int KEY_RESERVED = 0;
	internal const int KEY_ESC = 1;
	internal const int KEY_1 = 2;
	internal const int KEY_2 = 3;
	internal const int KEY_3 = 4;
	internal const int KEY_4 = 5;
	internal const int KEY_5 = 6;
	internal const int KEY_6 = 7;
	internal const int KEY_7 = 8;
	internal const int KEY_8 = 9;
	internal const int KEY_9 = 10;
	internal const int KEY_0 = 11;
	internal const int KEY_MINUS = 12;
	internal const int KEY_EQUAL = 13;
	internal const int KEY_BACKSPACE = 14;
	internal const int KEY_TAB = 15;
	internal const int KEY_Q = 16;
	internal const int KEY_W = 17;
	internal const int KEY_E = 18;
	internal const int KEY_R = 19;
	internal const int KEY_T = 20;
	internal const int KEY_Y = 21;
	internal const int KEY_U = 22;
	internal const int KEY_I = 23;
	internal const int KEY_O = 24;
	internal const int KEY_P = 25;
	internal const int KEY_LEFTBRACE = 26;
	internal const int KEY_RIGHTBRACE = 27;
	internal const int KEY_ENTER = 28;
	internal const int KEY_LEFTCTRL = 29;
	internal const int KEY_A = 30;
	internal const int KEY_S = 31;
	internal const int KEY_D = 32;
	internal const int KEY_F = 33;
	internal const int KEY_G = 34;
	internal const int KEY_H = 35;
	internal const int KEY_J = 36;
	internal const int KEY_K = 37;
	internal const int KEY_L = 38;
	internal const int KEY_SEMICOLON = 39;
	internal const int KEY_APOSTROPHE = 40;
	internal const int KEY_GRAVE = 41;
	internal const int KEY_LEFTSHIFT = 42;
	internal const int KEY_BACKSLASH = 43;
	internal const int KEY_Z = 44;
	internal const int KEY_X = 45;
	internal const int KEY_C = 46;
	internal const int KEY_V = 47;
	internal const int KEY_B = 48;
	internal const int KEY_N = 49;
	internal const int KEY_M = 50;
	internal const int KEY_COMMA = 51;
	internal const int KEY_DOT = 52;
	internal const int KEY_SLASH = 53;
	internal const int KEY_RIGHTSHIFT = 54;
	internal const int KEY_KPASTERISK = 55;
	internal const int KEY_LEFTALT = 56;
	internal const int KEY_SPACE = 57;
	internal const int KEY_CAPSLOCK = 58;
	internal const int KEY_F1 = 59;
	internal const int KEY_F2 = 60;
	internal const int KEY_F3 = 61;
	internal const int KEY_F4 = 62;
	internal const int KEY_F5 = 63;
	internal const int KEY_F6 = 64;
	internal const int KEY_F7 = 65;
	internal const int KEY_F8 = 66;
	internal const int KEY_F9 = 67;
	internal const int KEY_F10 = 68;
	internal const int KEY_NUMLOCK = 69;
	internal const int KEY_SCROLLLOCK = 70;
	internal const int KEY_KP7 = 71;
	internal const int KEY_KP8 = 72;
	internal const int KEY_KP9 = 73;
	internal const int KEY_KPMINUS = 74;
	internal const int KEY_KP4 = 75;
	internal const int KEY_KP5 = 76;
	internal const int KEY_KP6 = 77;
	internal const int KEY_KPPLUS = 78;
	internal const int KEY_KP1 = 79;
	internal const int KEY_KP2 = 80;
	internal const int KEY_KP3 = 81;
	internal const int KEY_KP0 = 82;
	internal const int KEY_KPDOT = 83;

	internal const int KEY_ZENKAKUHANKAKU = 85;
	internal const int KEY_102ND = 86;
	internal const int KEY_F11 = 87;
	internal const int KEY_F12 = 88;
	internal const int KEY_RO = 89;
	internal const int KEY_KATAKANA = 90;
	internal const int KEY_HIRAGANA = 91;
	internal const int KEY_HENKAN = 92;
	internal const int KEY_KATAKANAHIRAGANA = 93;
	internal const int KEY_MUHENKAN = 94;
	internal const int KEY_KPJPCOMMA = 95;
	internal const int KEY_KPENTER = 96;
	internal const int KEY_RIGHTCTRL = 97;
	internal const int KEY_KPSLASH = 98;
	internal const int KEY_SYSRQ = 99;
	internal const int KEY_RIGHTALT = 100;
	internal const int KEY_LINEFEED = 101;
	internal const int KEY_HOME = 102;
	internal const int KEY_UP = 103;
	internal const int KEY_PAGEUP = 104;
	internal const int KEY_LEFT = 105;
	internal const int KEY_RIGHT = 106;
	internal const int KEY_END = 107;
	internal const int KEY_DOWN = 108;
	internal const int KEY_PAGEDOWN = 109;
	internal const int KEY_INSERT = 110;
	internal const int KEY_DELETE = 111;
	internal const int KEY_MACRO = 112;
	internal const int KEY_MUTE = 113;
	internal const int KEY_VOLUMEDOWN = 114;
	internal const int KEY_VOLUMEUP = 115;
	internal const int KEY_POWER = 116; /** SC System Power Down */
	internal const int KEY_KPEQUAL = 117;
	internal const int KEY_KPPLUSMINUS = 118;
	internal const int KEY_PAUSE = 119;
	internal const int KEY_SCALE = 120; /** AL Compiz Scale (Expose) */

	internal const int KEY_KPCOMMA = 121;
	internal const int KEY_HANGEUL = 122;
	internal const int KEY_HANGUEL = KEY_HANGEUL;
	internal const int KEY_HANJA = 123;
	internal const int KEY_YEN = 124;
	internal const int KEY_LEFTMETA = 125;
	internal const int KEY_RIGHTMETA = 126;
	internal const int KEY_COMPOSE = 127;

	internal const int KEY_STOP = 128; /** AC Stop */
	internal const int KEY_AGAIN = 129;
	internal const int KEY_PROPS = 130; /** AC Properties */
	internal const int KEY_UNDO = 131; /** AC Undo */
	internal const int KEY_FRONT = 132;
	internal const int KEY_COPY = 133; /** AC Copy */
	internal const int KEY_OPEN = 134; /** AC Open */
	internal const int KEY_PASTE = 135; /** AC Paste */
	internal const int KEY_FIND = 136; /** AC Search */
	internal const int KEY_CUT = 137; /** AC Cut */
	internal const int KEY_HELP = 138; /** AL Integrated Help Center */
	internal const int KEY_MENU = 139; /** Menu (show menu) */
	internal const int KEY_CALC = 140; /** AL Calculator */
	internal const int KEY_SETUP = 141;
	internal const int KEY_SLEEP = 142; /** SC System Sleep */
	internal const int KEY_WAKEUP = 143; /** System Wake Up */
	internal const int KEY_FILE = 144; /** AL Local Machine Browser */
	internal const int KEY_SENDFILE = 145;
	internal const int KEY_DELETEFILE = 146;
	internal const int KEY_XFER = 147;
	internal const int KEY_PROG1 = 148;
	internal const int KEY_PROG2 = 149;
	internal const int KEY_WWW = 150; /** AL Internet Browser */
	internal const int KEY_MSDOS = 151;
	internal const int KEY_COFFEE = 152; /** AL Terminal Lock/Screensaver */
	internal const int KEY_SCREENLOCK = KEY_COFFEE;
	internal const int KEY_ROTATE_DISPLAY = 153; /** Display orientation for e.g. tablets */
	internal const int KEY_DIRECTION = KEY_ROTATE_DISPLAY;
	internal const int KEY_CYCLEWINDOWS = 154;
	internal const int KEY_MAIL = 155;
	internal const int KEY_BOOKMARKS = 156; /** AC Bookmarks */
	internal const int KEY_COMPUTER = 157;
	internal const int KEY_BACK = 158; /** AC Back */
	internal const int KEY_FORWARD = 159; /** AC Forward */
	internal const int KEY_CLOSECD = 160;
	internal const int KEY_EJECTCD = 161;
	internal const int KEY_EJECTCLOSECD = 162;
	internal const int KEY_NEXTSONG = 163;
	internal const int KEY_PLAYPAUSE = 164;
	internal const int KEY_PREVIOUSSONG = 165;
	internal const int KEY_STOPCD = 166;
	internal const int KEY_RECORD = 167;
	internal const int KEY_REWIND = 168;
	internal const int KEY_PHONE = 169; /** Media Select Telephone */
	internal const int KEY_ISO = 170;
	internal const int KEY_CONFIG = 171; /** AL Consumer Control Configuration */
	internal const int KEY_HOMEPAGE = 172; /** AC Home */
	internal const int KEY_REFRESH = 173; /** AC Refresh */
	internal const int KEY_EXIT = 174; /** AC Exit */
	internal const int KEY_MOVE = 175;
	internal const int KEY_EDIT = 176;
	internal const int KEY_SCROLLUP = 177;
	internal const int KEY_SCROLLDOWN = 178;
	internal const int KEY_KPLEFTPAREN = 179;
	internal const int KEY_KPRIGHTPAREN = 180;
	internal const int KEY_NEW = 181; /** AC New */
	internal const int KEY_REDO = 182; /** AC Redo/Repeat */

	internal const int KEY_F13 = 183;
	internal const int KEY_F14 = 184;
	internal const int KEY_F15 = 185;
	internal const int KEY_F16 = 186;
	internal const int KEY_F17 = 187;
	internal const int KEY_F18 = 188;
	internal const int KEY_F19 = 189;
	internal const int KEY_F20 = 190;
	internal const int KEY_F21 = 191;
	internal const int KEY_F22 = 192;
	internal const int KEY_F23 = 193;
	internal const int KEY_F24 = 194;

	internal const int KEY_PLAYCD = 200;
	internal const int KEY_PAUSECD = 201;
	internal const int KEY_PROG3 = 202;
	internal const int KEY_PROG4 = 203;
	internal const int KEY_ALL_APPLICATIONS = 204; /** AC Desktop Show All Applications */
	internal const int KEY_DASHBOARD = KEY_ALL_APPLICATIONS;
	internal const int KEY_SUSPEND = 205;
	internal const int KEY_CLOSE = 206; /** AC Close */
	internal const int KEY_PLAY = 207;
	internal const int KEY_FASTFORWARD = 208;
	internal const int KEY_BASSBOOST = 209;
	internal const int KEY_PRINT = 210; /** AC Print */
	internal const int KEY_HP = 211;
	internal const int KEY_CAMERA = 212;
	internal const int KEY_SOUND = 213;
	internal const int KEY_QUESTION = 214;
	internal const int KEY_EMAIL = 215;
	internal const int KEY_CHAT = 216;
	internal const int KEY_SEARCH = 217;
	internal const int KEY_CONNECT = 218;
	internal const int KEY_FINANCE = 219; /** AL Checkbook/Finance */
	internal const int KEY_SPORT = 220;
	internal const int KEY_SHOP = 221;
	internal const int KEY_ALTERASE = 222;
	internal const int KEY_CANCEL = 223; /** AC Cancel */
	internal const int KEY_BRIGHTNESSDOWN = 224;
	internal const int KEY_BRIGHTNESSUP = 225;
	internal const int KEY_MEDIA = 226;

	internal const int KEY_SWITCHVIDEOMODE = 227; /** Cycle between available video
						   outputs (Monitor/LCD/TV-out/etc) */
	internal const int KEY_KBDILLUMTOGGLE = 228;
	internal const int KEY_KBDILLUMDOWN = 229;
	internal const int KEY_KBDILLUMUP = 230;

	internal const int KEY_SEND = 231; /** AC Send */
	internal const int KEY_REPLY = 232; /** AC Reply */
	internal const int KEY_FORWARDMAIL = 233; /** AC Forward Msg */
	internal const int KEY_SAVE = 234; /** AC Save */
	internal const int KEY_DOCUMENTS = 235;

	internal const int KEY_BATTERY = 236;

	internal const int KEY_BLUETOOTH = 237;
	internal const int KEY_WLAN = 238;
	internal const int KEY_UWB = 239;

	internal const int KEY_UNKNOWN = 240;

	internal const int KEY_VIDEO_NEXT = 241; /** drive next video source */
	internal const int KEY_VIDEO_PREV = 242; /** drive previous video source */
	internal const int KEY_BRIGHTNESS_CYCLE = 243; /** brightness up; /*after max is min */
	internal const int KEY_BRIGHTNESS_AUTO = 244; /** Set Auto Brightness: manual
						  brightness control is off;
						  rely on ambient */
	internal const int KEY_BRIGHTNESS_ZERO = KEY_BRIGHTNESS_AUTO;
	internal const int KEY_DISPLAY_OFF = 245; /** display device to off state */

	internal const int KEY_WWAN = 246; /** Wireless WAN (LTE; /*UMTS; /*GSM; /*etc.) */
	internal const int KEY_WIMAX = KEY_WWAN;
	internal const int KEY_RFKILL = 247; /** Key that controls all radios */

	internal const int KEY_MICMUTE = 248; /** Mute / unmute the microphone */

	/* Code 255 ,is reserved for special needs of AT keyboard driver */

	internal const int BTN_MISC = 0x100;
	internal const int BTN_0 = 0x100;
	internal const int BTN_1 = 0x101;
	internal const int BTN_2 = 0x102;
	internal const int BTN_3 = 0x103;
	internal const int BTN_4 = 0x104;
	internal const int BTN_5 = 0x105;
	internal const int BTN_6 = 0x106;
	internal const int BTN_7 = 0x107;
	internal const int BTN_8 = 0x108;
	internal const int BTN_9 = 0x109;

	internal const int BTN_MOUSE = 0x110;
	internal const int BTN_LEFT = 0x110;
	internal const int BTN_RIGHT = 0x111;
	internal const int BTN_MIDDLE = 0x112;
	internal const int BTN_SIDE = 0x113;
	internal const int BTN_EXTRA = 0x114;
	internal const int BTN_FORWARD = 0x115;
	internal const int BTN_BACK = 0x116;
	internal const int BTN_TASK = 0x117;

	internal const int BTN_JOYSTICK = 0x120;
	internal const int BTN_TRIGGER = 0x120;
	internal const int BTN_THUMB = 0x121;
	internal const int BTN_THUMB2 = 0x122;
	internal const int BTN_TOP = 0x123;
	internal const int BTN_TOP2 = 0x124;
	internal const int BTN_PINKIE = 0x125;
	internal const int BTN_BASE = 0x126;
	internal const int BTN_BASE2 = 0x127;
	internal const int BTN_BASE3 = 0x128;
	internal const int BTN_BASE4 = 0x129;
	internal const int BTN_BASE5 = 0x12a;
	internal const int BTN_BASE6 = 0x12b;
	internal const int BTN_DEAD = 0x12f;

	internal const int BTN_GAMEPAD = 0x130;
	internal const int BTN_SOUTH = 0x130;
	internal const int BTN_A = BTN_SOUTH;
	internal const int BTN_EAST = 0x131;
	internal const int BTN_B = BTN_EAST;
	internal const int BTN_C = 0x132;
	internal const int BTN_NORTH = 0x133;
	internal const int BTN_X = BTN_NORTH;
	internal const int BTN_WEST = 0x134;
	internal const int BTN_Y = BTN_WEST;
	internal const int BTN_Z = 0x135;
	internal const int BTN_TL = 0x136;
	internal const int BTN_TR = 0x137;
	internal const int BTN_TL2 = 0x138;
	internal const int BTN_TR2 = 0x139;
	internal const int BTN_SELECT = 0x13a;
	internal const int BTN_START = 0x13b;
	internal const int BTN_MODE = 0x13c;
	internal const int BTN_THUMBL = 0x13d;
	internal const int BTN_THUMBR = 0x13e;

	internal const int BTN_DIGI = 0x140;
	internal const int BTN_TOOL_PEN = 0x140;
	internal const int BTN_TOOL_RUBBER = 0x141;
	internal const int BTN_TOOL_BRUSH = 0x142;
	internal const int BTN_TOOL_PENCIL = 0x143;
	internal const int BTN_TOOL_AIRBRUSH = 0x144;
	internal const int BTN_TOOL_FINGER = 0x145;
	internal const int BTN_TOOL_MOUSE = 0x146;
	internal const int BTN_TOOL_LENS = 0x147;
	internal const int BTN_TOOL_QUINTTAP = 0x148; /** Five fingers on trackpad */
	internal const int BTN_STYLUS3 = 0x149;
	internal const int BTN_TOUCH = 0x14a;
	internal const int BTN_STYLUS = 0x14b;
	internal const int BTN_STYLUS2 = 0x14c;
	internal const int BTN_TOOL_DOUBLETAP = 0x14d;
	internal const int BTN_TOOL_TRIPLETAP = 0x14e;
	internal const int BTN_TOOL_QUADTAP = 0x14f; /** Four fingers on trackpad */

	internal const int BTN_WHEEL = 0x150;
	internal const int BTN_GEAR_DOWN = 0x150;
	internal const int BTN_GEAR_UP = 0x151;

	internal const int KEY_OK = 0x160;
	internal const int KEY_SELECT = 0x161;
	internal const int KEY_GOTO = 0x162;
	internal const int KEY_CLEAR = 0x163;
	internal const int KEY_POWER2 = 0x164;
	internal const int KEY_OPTION = 0x165;
	internal const int KEY_INFO = 0x166; /** AL OEM Features/Tips/Tutorial */
	internal const int KEY_TIME = 0x167;
	internal const int KEY_VENDOR = 0x168;
	internal const int KEY_ARCHIVE = 0x169;
	internal const int KEY_PROGRAM = 0x16a; /** Media Select Program Guide */
	internal const int KEY_CHANNEL = 0x16b;
	internal const int KEY_FAVORITES = 0x16c;
	internal const int KEY_EPG = 0x16d;
	internal const int KEY_PVR = 0x16e; /** Media Select Home */
	internal const int KEY_MHP = 0x16f;
	internal const int KEY_LANGUAGE = 0x170;
	internal const int KEY_TITLE = 0x171;
	internal const int KEY_SUBTITLE = 0x172;
	internal const int KEY_ANGLE = 0x173;
	internal const int KEY_FULL_SCREEN = 0x174; /** AC View Toggle */
	internal const int KEY_ZOOM = KEY_FULL_SCREEN;
	internal const int KEY_MODE = 0x175;
	internal const int KEY_KEYBOARD = 0x176;
	internal const int KEY_ASPECT_RATIO = 0x177; /** HUTRR37: Aspect */
	internal const int KEY_SCREEN = KEY_ASPECT_RATIO;
	internal const int KEY_PC = 0x178; /** Media Select Computer */
	internal const int KEY_TV = 0x179; /** Media Select TV */
	internal const int KEY_TV2 = 0x17a; /** Media Select Cable */
	internal const int KEY_VCR = 0x17b; /** Media Select VCR */
	internal const int KEY_VCR2 = 0x17c; /** VCR Plus */
	internal const int KEY_SAT = 0x17d; /** Media Select Satellite */
	internal const int KEY_SAT2 = 0x17e;
	internal const int KEY_CD = 0x17f; /** Media Select CD */
	internal const int KEY_TAPE = 0x180; /** Media Select Tape */
	internal const int KEY_RADIO = 0x181;
	internal const int KEY_TUNER = 0x182; /** Media Select Tuner */
	internal const int KEY_PLAYER = 0x183;
	internal const int KEY_TEXT = 0x184;
	internal const int KEY_DVD = 0x185; /** Media Select DVD */
	internal const int KEY_AUX = 0x186;
	internal const int KEY_MP3 = 0x187;
	internal const int KEY_AUDIO = 0x188; /** AL Audio Browser */
	internal const int KEY_VIDEO = 0x189; /** AL Movie Browser */
	internal const int KEY_DIRECTORY = 0x18a;
	internal const int KEY_LIST = 0x18b;
	internal const int KEY_MEMO = 0x18c; /** Media Select Messages */
	internal const int KEY_CALENDAR = 0x18d;
	internal const int KEY_RED = 0x18e;
	internal const int KEY_GREEN = 0x18f;
	internal const int KEY_YELLOW = 0x190;
	internal const int KEY_BLUE = 0x191;
	internal const int KEY_CHANNELUP = 0x192; /** Channel Increment */
	internal const int KEY_CHANNELDOWN = 0x193; /** Channel Decrement */
	internal const int KEY_FIRST = 0x194;
	internal const int KEY_LAST = 0x195; /** Recall Last */
	internal const int KEY_AB = 0x196;
	internal const int KEY_NEXT = 0x197;
	internal const int KEY_RESTART = 0x198;
	internal const int KEY_SLOW = 0x199;
	internal const int KEY_SHUFFLE = 0x19a;
	internal const int KEY_BREAK = 0x19b;
	internal const int KEY_PREVIOUS = 0x19c;
	internal const int KEY_DIGITS = 0x19d;
	internal const int KEY_TEEN = 0x19e;
	internal const int KEY_TWEN = 0x19f;
	internal const int KEY_VIDEOPHONE = 0x1a0; /** Media Select Video Phone */
	internal const int KEY_GAMES = 0x1a1; /** Media Select Games */
	internal const int KEY_ZOOMIN = 0x1a2; /** AC Zoom In */
	internal const int KEY_ZOOMOUT = 0x1a3; /** AC Zoom Out */
	internal const int KEY_ZOOMRESET = 0x1a4; /** AC Zoom */
	internal const int KEY_WORDPROCESSOR = 0x1a5; /** AL Word Processor */
	internal const int KEY_EDITOR = 0x1a6; /** AL Text Editor */
	internal const int KEY_SPREADSHEET = 0x1a7; /** AL Spreadsheet */
	internal const int KEY_GRAPHICSEDITOR = 0x1a8; /** AL Graphics Editor */
	internal const int KEY_PRESENTATION = 0x1a9; /** AL Presentation App */
	internal const int KEY_DATABASE = 0x1aa; /** AL Database App */
	internal const int KEY_NEWS = 0x1ab; /** AL Newsreader */
	internal const int KEY_VOICEMAIL = 0x1ac; /** AL Voicemail */
	internal const int KEY_ADDRESSBOOK = 0x1ad; /** AL Contacts/Address Book */
	internal const int KEY_MESSENGER = 0x1ae; /** AL Instant Messaging */
	internal const int KEY_DISPLAYTOGGLE = 0x1af; /** Turn display (LCD) on and off */
	internal const int KEY_BRIGHTNESS_TOGGLE = KEY_DISPLAYTOGGLE;
	internal const int KEY_SPELLCHECK = 0x1b0; /** AL Spell Check */
	internal const int KEY_LOGOFF = 0x1b1; /** AL Logoff */

	internal const int KEY_DOLLAR = 0x1b2;
	internal const int KEY_EURO = 0x1b3;

	internal const int KEY_FRAMEBACK = 0x1b4; /** Consumer - transport controls */
	internal const int KEY_FRAMEFORWARD = 0x1b5;
	internal const int KEY_CONTEXT_MENU = 0x1b6; /** GenDesc - system context menu */
	internal const int KEY_MEDIA_REPEAT = 0x1b7; /** Consumer - transport control */
	internal const int KEY_10CHANNELSUP = 0x1b8; /** 10 ,channels up (10+) */
	internal const int KEY_10CHANNELSDOWN = 0x1b9; /** 10 ,channels down (10-) */
	internal const int KEY_IMAGES = 0x1ba; /** AL Image Browser */
	internal const int KEY_NOTIFICATION_CENTER = 0x1bc; /** Show/hide the notification center */
	internal const int KEY_PICKUP_PHONE = 0x1bd; /** Answer incoming call */
	internal const int KEY_HANGUP_PHONE = 0x1be; /** Decline incoming call */

	internal const int KEY_DEL_EOL = 0x1c0;
	internal const int KEY_DEL_EOS = 0x1c1;
	internal const int KEY_INS_LINE = 0x1c2;
	internal const int KEY_DEL_LINE = 0x1c3;

	internal const int KEY_FN = 0x1d0;
	internal const int KEY_FN_ESC = 0x1d1;
	internal const int KEY_FN_F1 = 0x1d2;
	internal const int KEY_FN_F2 = 0x1d3;
	internal const int KEY_FN_F3 = 0x1d4;
	internal const int KEY_FN_F4 = 0x1d5;
	internal const int KEY_FN_F5 = 0x1d6;
	internal const int KEY_FN_F6 = 0x1d7;
	internal const int KEY_FN_F7 = 0x1d8;
	internal const int KEY_FN_F8 = 0x1d9;
	internal const int KEY_FN_F9 = 0x1da;
	internal const int KEY_FN_F10 = 0x1db;
	internal const int KEY_FN_F11 = 0x1dc;
	internal const int KEY_FN_F12 = 0x1dd;
	internal const int KEY_FN_1 = 0x1de;
	internal const int KEY_FN_2 = 0x1df;
	internal const int KEY_FN_D = 0x1e0;
	internal const int KEY_FN_E = 0x1e1;
	internal const int KEY_FN_F = 0x1e2;
	internal const int KEY_FN_S = 0x1e3;
	internal const int KEY_FN_B = 0x1e4;
	internal const int KEY_FN_RIGHT_SHIFT = 0x1e5;

	internal const int KEY_BRL_DOT1 = 0x1f1;
	internal const int KEY_BRL_DOT2 = 0x1f2;
	internal const int KEY_BRL_DOT3 = 0x1f3;
	internal const int KEY_BRL_DOT4 = 0x1f4;
	internal const int KEY_BRL_DOT5 = 0x1f5;
	internal const int KEY_BRL_DOT6 = 0x1f6;
	internal const int KEY_BRL_DOT7 = 0x1f7;
	internal const int KEY_BRL_DOT8 = 0x1f8;
	internal const int KEY_BRL_DOT9 = 0x1f9;
	internal const int KEY_BRL_DOT10 = 0x1fa;

	internal const int KEY_NUMERIC_0 = 0x200; /** used by phones; /*remote controls; /**/
	internal const int KEY_NUMERIC_1 = 0x201; /** and other keypads */
	internal const int KEY_NUMERIC_2 = 0x202;
	internal const int KEY_NUMERIC_3 = 0x203;
	internal const int KEY_NUMERIC_4 = 0x204;
	internal const int KEY_NUMERIC_5 = 0x205;
	internal const int KEY_NUMERIC_6 = 0x206;
	internal const int KEY_NUMERIC_7 = 0x207;
	internal const int KEY_NUMERIC_8 = 0x208;
	internal const int KEY_NUMERIC_9 = 0x209;
	internal const int KEY_NUMERIC_STAR = 0x20a;
	internal const int KEY_NUMERIC_POUND = 0x20b;
	internal const int KEY_NUMERIC_A = 0x20c; /** Phone key A - HUT Telephony 0xb9 ,*/
	internal const int KEY_NUMERIC_B = 0x20d;
	internal const int KEY_NUMERIC_C = 0x20e;
	internal const int KEY_NUMERIC_D = 0x20f;

	internal const int KEY_CAMERA_FOCUS = 0x210;
	internal const int KEY_WPS_BUTTON = 0x211; /** WiFi Protected Setup key */

	internal const int KEY_TOUCHPAD_TOGGLE = 0x212; /** Request switch touchpad on or off */
	internal const int KEY_TOUCHPAD_ON = 0x213;
	internal const int KEY_TOUCHPAD_OFF = 0x214;

	internal const int KEY_CAMERA_ZOOMIN = 0x215;
	internal const int KEY_CAMERA_ZOOMOUT = 0x216;
	internal const int KEY_CAMERA_UP = 0x217;
	internal const int KEY_CAMERA_DOWN = 0x218;
	internal const int KEY_CAMERA_LEFT = 0x219;
	internal const int KEY_CAMERA_RIGHT = 0x21a;

	internal const int KEY_ATTENDANT_ON = 0x21b;
	internal const int KEY_ATTENDANT_OFF = 0x21c;
	internal const int KEY_ATTENDANT_TOGGLE = 0x21d; /** Attendant call on or off */
	internal const int KEY_LIGHTS_TOGGLE = 0x21e; /** Reading light on or off */

	internal const int TN_DPAD_UP = 0x220;
	internal const int TN_DPAD_DOWN = 0x221;
	internal const int TN_DPAD_LEFT = 0x222;
	internal const int TN_DPAD_RIGHT = 0x223;

	internal const int KEY_ALS_TOGGLE = 0x230; /** Ambient light sensor */
	internal const int KEY_ROTATE_LOCK_TOGGLE = 0x231; /** Display rotation lock */
	internal const int KEY_REFRESH_RATE_TOGGLE = 0x232; /** Display refresh rate toggle */

	internal const int KEY_BUTTONCONFIG = 0x240; /** AL Button Configuration */
	internal const int KEY_TASKMANAGER = 0x241; /** AL Task/Project Manager */
	internal const int KEY_JOURNAL = 0x242; /** AL Log/Journal/Timecard */
	internal const int KEY_CONTROLPANEL = 0x243; /** AL Control Panel */
	internal const int KEY_APPSELECT = 0x244; /** AL Select Task/Application */
	internal const int KEY_SCREENSAVER = 0x245; /** AL Screen Saver */
	internal const int KEY_VOICECOMMAND = 0x246; /** Listening Voice Command */
	internal const int KEY_ASSISTANT = 0x247; /** AL Context-aware desktop assistant */
	internal const int KEY_KBD_LAYOUT_NEXT = 0x248; /** AC Next Keyboard Layout Select */
	internal const int KEY_EMOJI_PICKER = 0x249; /** Show/hide emoji picker (HUTRR101) */
	internal const int KEY_DICTATE = 0x24a; /** Start or Stop Voice Dictation Session (HUTRR99) */
	internal const int KEY_CAMERA_ACCESS_ENABLE = 0x24b; /** Enables programmatic access to camera devices. (HUTRR72) */
	internal const int KEY_CAMERA_ACCESS_DISABLE = 0x24c; /** Disables programmatic access to camera devices. (HUTRR72) */
	internal const int KEY_CAMERA_ACCESS_TOGGLE = 0x24d; /** Toggles the current state of the camera access control. (HUTRR72) */
	internal const int KEY_ACCESSIBILITY = 0x24e; /** Toggles the system bound accessibility UI/command (HUTRR116) */
	internal const int KEY_DO_NOT_DISTURB = 0x24f; /** Toggles the system-wide "Do Not Disturb" control (HUTRR94)*/

	internal const int KEY_BRIGHTNESS_MIN = 0x250; /** Set Brightness to Minimum */
	internal const int KEY_BRIGHTNESS_MAX = 0x251; /** Set Brightness to Maximum */

	internal const int KEY_KBDINPUTASSIST_PREV = 0x260;
	internal const int KEY_KBDINPUTASSIST_NEXT = 0x261;
	internal const int KEY_KBDINPUTASSIST_PREVGROUP = 0x262;
	internal const int KEY_KBDINPUTASSIST_NEXTGROUP = 0x263;
	internal const int KEY_KBDINPUTASSIST_ACCEPT = 0x264;
	internal const int KEY_KBDINPUTASSIST_CANCEL = 0x265;

	/* Diagonal movement keys */
	internal const int KEY_RIGHT_UP = 0x266;
	internal const int KEY_RIGHT_DOWN = 0x267;
	internal const int KEY_LEFT_UP = 0x268;
	internal const int KEY_LEFT_DOWN = 0x269;

	internal const int KEY_ROOT_MENU = 0x26a; /** Show Device's Root Menu */

	/* Show Top Menu of the Media (e.g. DVD) */
	internal const int KEY_MEDIA_TOP_MENU = 0x26b;
	internal const int KEY_NUMERIC_11 = 0x26c;
	internal const int KEY_NUMERIC_12 = 0x26d;

    /*
     * Toggle Audio Description: refers to an audio service that helps blind and
     * visually impaired consumers understand the action in a program. Note: in
     * some countries this is referred to as "Video Description".
     */
	internal const int KEY_AUDIO_DESC = 0x26e;
	internal const int KEY_3D_MODE = 0x26f;
	internal const int KEY_NEXT_FAVORITE = 0x270;
	internal const int KEY_STOP_RECORD = 0x271;
	internal const int KEY_PAUSE_RECORD = 0x272;
	internal const int KEY_VOD = 0x273; /** Video on Demand */
	internal const int KEY_UNMUTE = 0x274;
	internal const int KEY_FASTREVERSE = 0x275;
	internal const int KEY_SLOWREVERSE = 0x276;

	/*
	 * Control a data application associated with the currently viewed channel;
	 * e.g. teletext or data broadcast application (MHEG; /*MHP; /*HbbTV; /*etc.)
	 */
	internal const int KEY_DATA = 0x277;
	internal const int KEY_ONSCREEN_KEYBOARD = 0x278;

	/* Electronic privacy screen control */
	internal const int KEY_PRIVACY_SCREEN_TOGGLE = 0x279;

	/* Select an area of screen to be copied */
	internal const int KEY_SELECTIVE_SCREENSHOT = 0x27a;

	/* Move the focus to the next or previous user controllable element within a UI container */
	internal const int KEY_NEXT_ELEMENT = 0x27b;
	internal const int KEY_PREVIOUS_ELEMENT = 0x27c;

	/* Toggle Autopilot engagement */
	internal const int KEY_AUTOPILOT_ENGAGE_TOGGLE = 0x27d;

	/* Shortcut Keys */
	internal const int KEY_MARK_WAYPOINT = 0x27e;
	internal const int KEY_SOS = 0x27f;
	internal const int KEY_NAV_CHART = 0x280;
	internal const int KEY_FISHING_CHART = 0x281;
	internal const int KEY_SINGLE_RANGE_RADAR = 0x282;
	internal const int KEY_DUAL_RANGE_RADAR = 0x283;
	internal const int KEY_RADAR_OVERLAY = 0x284;
	internal const int KEY_TRADITIONAL_SONAR = 0x285;
	internal const int KEY_CLEARVU_SONAR = 0x286;
	internal const int KEY_SIDEVU_SONAR = 0x287;
	internal const int KEY_NAV_INFO = 0x288;
	internal const int KEY_BRIGHTNESS_MENU = 0x289;

	/*
	 * Some keyboards have keys which do not have a defined meaning; /*these keys
	 * are intended to be programmed / bound to macros by the user. For most
	 * keyboards with these macro-keys the key-sequence to inject; /*or action to
	 * take; /*is all handled by software on the host side. So from the kernel's
	 * point of view these are just normal keys.
	 *
	 * The KEY_MACRO# codes below are intended for such keys; /*which may be labeled
	 * e.g. G1-G18; /*or S1 ,- S30. The KEY_MACRO# codes MUST NOT be used for keys
	 * where the marking on the key does indicate a defined meaning / purpose.
	 *
	 * The KEY_MACRO# codes MUST also NOT be used as fallback for when no existing
	 * KEY_FOO define matches the marking / purpose. In this case a new KEY_FOO
	 * define MUST be added.
	 */
	internal const int KEY_MACRO1 = 0x290;
	internal const int KEY_MACRO2 = 0x291;
	internal const int KEY_MACRO3 = 0x292;
	internal const int KEY_MACRO4 = 0x293;
	internal const int KEY_MACRO5 = 0x294;
	internal const int KEY_MACRO6 = 0x295;
	internal const int KEY_MACRO7 = 0x296;
	internal const int KEY_MACRO8 = 0x297;
	internal const int KEY_MACRO9 = 0x298;
	internal const int KEY_MACRO10 = 0x299;
	internal const int KEY_MACRO11 = 0x29a;
	internal const int KEY_MACRO12 = 0x29b;
	internal const int KEY_MACRO13 = 0x29c;
	internal const int KEY_MACRO14 = 0x29d;
	internal const int KEY_MACRO15 = 0x29e;
	internal const int KEY_MACRO16 = 0x29f;
	internal const int KEY_MACRO17 = 0x2a0;
	internal const int KEY_MACRO18 = 0x2a1;
	internal const int KEY_MACRO19 = 0x2a2;
	internal const int KEY_MACRO20 = 0x2a3;
	internal const int KEY_MACRO21 = 0x2a4;
	internal const int KEY_MACRO22 = 0x2a5;
	internal const int KEY_MACRO23 = 0x2a6;
	internal const int KEY_MACRO24 = 0x2a7;
	internal const int KEY_MACRO25 = 0x2a8;
	internal const int KEY_MACRO26 = 0x2a9;
	internal const int KEY_MACRO27 = 0x2aa;
	internal const int KEY_MACRO28 = 0x2ab;
	internal const int KEY_MACRO29 = 0x2ac;
	internal const int KEY_MACRO30 = 0x2ad;

	/*
	 * Some keyboards with the macro-keys described above have some extra keys
	 * for controlling the host-side software responsible for the macro handling:
	 * -A macro recording start/stop key. Note that not all keyboards which emit
	 *  KEY_MACRO_RECORD_START will also emit KEY_MACRO_RECORD_STOP if
	 *  KEY_MACRO_RECORD_STOP is not advertised; /*then KEY_MACRO_RECORD_START
	 *  should be interpreted as a recording start/stop toggle;
	 * -Keys for switching between different macro (pre)sets; /*either a key for
	 *  cycling through the configured presets or keys to directly select a preset.
	 */
	internal const int KEY_MACRO_RECORD_START = 0x2b0;
	internal const int KEY_MACRO_RECORD_STOP = 0x2b1;
	internal const int KEY_MACRO_PRESET_CYCLE = 0x2b2;
	internal const int KEY_MACRO_PRESET1 = 0x2b3;
	internal const int KEY_MACRO_PRESET2 = 0x2b4;
	internal const int KEY_MACRO_PRESET3 = 0x2b5;

	/*
	 * Some keyboards have a buildin LCD panel where the contents are controlled
	 * by the host. Often these have a number of keys directly below the LCD
	 * intended for controlling a menu shown on the LCD. These keys often don't
	 * have any labeling so we just name them KEY_KBD_LCD_MENU#
	 */
	internal const int KEY_KBD_LCD_MENU1 = 0x2b8;
	internal const int KEY_KBD_LCD_MENU2 = 0x2b9;
	internal const int KEY_KBD_LCD_MENU3 = 0x2ba;
	internal const int KEY_KBD_LCD_MENU4 = 0x2bb;
	internal const int KEY_KBD_LCD_MENU5 = 0x2bc;

	internal const int BTN_TRIGGER_HAPPY = 0x2c0;
	internal const int BTN_TRIGGER_HAPPY1 = 0x2c0;
	internal const int BTN_TRIGGER_HAPPY2 = 0x2c1;
	internal const int BTN_TRIGGER_HAPPY3 = 0x2c2;
	internal const int BTN_TRIGGER_HAPPY4 = 0x2c3;
	internal const int BTN_TRIGGER_HAPPY5 = 0x2c4;
	internal const int BTN_TRIGGER_HAPPY6 = 0x2c5;
	internal const int BTN_TRIGGER_HAPPY7 = 0x2c6;
	internal const int BTN_TRIGGER_HAPPY8 = 0x2c7;
	internal const int BTN_TRIGGER_HAPPY9 = 0x2c8;
	internal const int BTN_TRIGGER_HAPPY10 = 0x2c9;
	internal const int BTN_TRIGGER_HAPPY11 = 0x2ca;
	internal const int BTN_TRIGGER_HAPPY12 = 0x2cb;
	internal const int BTN_TRIGGER_HAPPY13 = 0x2cc;
	internal const int BTN_TRIGGER_HAPPY14 = 0x2cd;
	internal const int BTN_TRIGGER_HAPPY15 = 0x2ce;
	internal const int BTN_TRIGGER_HAPPY16 = 0x2cf;
	internal const int BTN_TRIGGER_HAPPY17 = 0x2d0;
	internal const int BTN_TRIGGER_HAPPY18 = 0x2d1;
	internal const int BTN_TRIGGER_HAPPY19 = 0x2d2;
	internal const int BTN_TRIGGER_HAPPY20 = 0x2d3;
	internal const int BTN_TRIGGER_HAPPY21 = 0x2d4;
	internal const int BTN_TRIGGER_HAPPY22 = 0x2d5;
	internal const int BTN_TRIGGER_HAPPY23 = 0x2d6;
	internal const int BTN_TRIGGER_HAPPY24 = 0x2d7;
	internal const int BTN_TRIGGER_HAPPY25 = 0x2d8;
	internal const int BTN_TRIGGER_HAPPY26 = 0x2d9;
	internal const int BTN_TRIGGER_HAPPY27 = 0x2da;
	internal const int BTN_TRIGGER_HAPPY28 = 0x2db;
	internal const int BTN_TRIGGER_HAPPY29 = 0x2dc;
	internal const int BTN_TRIGGER_HAPPY30 = 0x2dd;
	internal const int BTN_TRIGGER_HAPPY31 = 0x2de;
	internal const int BTN_TRIGGER_HAPPY32 = 0x2df;
	internal const int BTN_TRIGGER_HAPPY33 = 0x2e0;
	internal const int BTN_TRIGGER_HAPPY34 = 0x2e1;
	internal const int BTN_TRIGGER_HAPPY35 = 0x2e2;
	internal const int BTN_TRIGGER_HAPPY36 = 0x2e3;
	internal const int BTN_TRIGGER_HAPPY37 = 0x2e4;
	internal const int BTN_TRIGGER_HAPPY38 = 0x2e5;
	internal const int BTN_TRIGGER_HAPPY39 = 0x2e6;
	internal const int BTN_TRIGGER_HAPPY40 = 0x2e7;

	/* We avoid low common keys in module aliases so they don't get huge. */
	internal const int KEY_MIN_INTERESTING = KEY_MUTE;
	internal const int KEY_MAX = 0x2ff;
	internal const int KEY_CNT = (KEY_MAX + 1);
}