#if UNITY_EDITOR || UNITY_ANDROID
using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Input.LowLevel;
using UnityEngine.Experimental.Input.Plugins.Android.LowLevel;
using UnityEngine.Experimental.Input.Utilities;

namespace UnityEngine.Experimental.Input.Plugins.Android.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AndroidGameControllerState : IInputStateTypeInfo
    {
        private const int kMaxAndroidAxes = 48;
        private const int kMaxAndroidButtons = 220;

        public static FourCC kFormat = new FourCC('A', 'G', 'C', ' ');

        ////REVIEW: is this mapping something that Android *guarantees* for us or do we need per-product templates
        ////        to deal with specific gamepads individually?
        [InputControl(name = "buttonSouth", bit = (uint)AndroidKeyCode.ButtonA)]
        [InputControl(name = "buttonWest", bit = (uint)AndroidKeyCode.ButtonX)]
        [InputControl(name = "buttonNorth", bit = (uint)AndroidKeyCode.ButtonY)]
        [InputControl(name = "buttonEast", bit = (uint)AndroidKeyCode.ButtonB)]
        [InputControl(name = "leftStickPress", bit = (uint)AndroidKeyCode.ButtonThumbl)]
        [InputControl(name = "rightStickPress", bit = (uint)AndroidKeyCode.ButtonThumbr)]
        [InputControl(name = "leftShoulder", bit = (uint)AndroidKeyCode.ButtonL1)]
        [InputControl(name = "rightShoulder", bit = (uint)AndroidKeyCode.ButtonR1)]
        [InputControl(name = "start", bit = (uint)AndroidKeyCode.ButtonStart)]
        [InputControl(name = "select", bit = (uint)AndroidKeyCode.ButtonSelect)]
        public fixed uint buttons[(kMaxAndroidButtons + 31) / 32];

        private const uint kAxisOffset = sizeof(uint) * (uint)((kMaxAndroidButtons + 31) / 32);

        [InputControl(name = "leftTrigger", offset = (uint)AndroidAxis.Ltrigger * sizeof(float) + kAxisOffset)]
        [InputControl(name = "rightTrigger", offset = (uint)AndroidAxis.Rtrigger * sizeof(float) + kAxisOffset)]
        [InputControl(name = "leftStick")]
        [InputControl(name = "rightStick", offset = (uint)AndroidAxis.Z * sizeof(float) + kAxisOffset)]
        public fixed float axis[kMaxAndroidAxes];

        public FourCC GetFormat()
        {
            return kFormat;
        }

        public AndroidGameControllerState WithButton(AndroidKeyCode code, bool value = true)
        {
            fixed(uint* buttonsPtr = buttons)
            {
                if (value)
                    buttonsPtr[(int)code / 32] |= (uint)1 << ((int)code % 32);
                else
                    buttonsPtr[(int)code / 32] &= ~((uint)1 << ((int)code % 32));
            }
            return this;
        }

        public AndroidGameControllerState WithAxis(AndroidAxis axis, float value)
        {
            fixed(float* axisPtr = this.axis)
            {
                axisPtr[(int)axis] = value;
            }
            return this;
        }
    }

    public enum AndroidKeyCode
    {
        Unknown = 0,
        SoftLeft = 1,
        SoftRight = 2,
        Home = 3,
        Back = 4,
        Call = 5,
        Endcall = 6,
        Alpha0 = 7,
        Alpha1 = 8,
        Alpha2 = 9,
        Alpha3 = 10,
        Alpha4 = 11,
        Alpha5 = 12,
        Alpha6 = 13,
        Alpha7 = 14,
        Alpha8 = 15,
        Alpha9 = 16,
        Star = 17,
        Pound = 18,
        DpadUp = 19,
        DpadDown = 20,
        DpadLeft = 21,
        DpadRight = 22,
        DpadCenter = 23,
        VolumeUp = 24,
        VolumeDown = 25,
        Power = 26,
        Camera = 27,
        Clear = 28,
        A = 29,
        B = 30,
        C = 31,
        D = 32,
        E = 33,
        F = 34,
        G = 35,
        H = 36,
        I = 37,
        J = 38,
        K = 39,
        L = 40,
        M = 41,
        N = 42,
        O = 43,
        P = 44,
        Q = 45,
        R = 46,
        S = 47,
        T = 48,
        U = 49,
        V = 50,
        W = 51,
        X = 52,
        Y = 53,
        Z = 54,
        Comma = 55,
        Period = 56,
        AltLeft = 57,
        AltRight = 58,
        ShiftLeft = 59,
        ShiftRight = 60,
        Tab = 61,
        Space = 62,
        Sym = 63,
        Explorer = 64,
        Envelope = 65,
        Enter = 66,
        Del = 67,
        Grave = 68,
        Minus = 69,
        Equals = 70,
        LeftBracket = 71,
        RightBracket = 72,
        Backslash = 73,
        Semicolon = 74,
        Apostrophe = 75,
        Slash = 76,
        At = 77,
        Num = 78,
        Headsethook = 79,
        Focus = 80, // *Camera* focus
        Plus = 81,
        Menu = 82,
        Notification = 83,
        Search = 84,
        MediaPlayPause = 85,
        MediaStop = 86,
        MediaNext = 87,
        MediaPrevious = 88,
        MediaRewind = 89,
        MediaFastForward = 90,
        Mute = 91,
        PageUp = 92,
        PageDown = 93,
        Pictsymbols = 94,
        SwitchCharset = 95,
        ButtonA = 96,
        ButtonB = 97,
        ButtonC = 98,
        ButtonX = 99,
        ButtonY = 100,
        ButtonZ = 101,
        ButtonL1 = 102,
        ButtonR1 = 103,
        ButtonL2 = 104,
        ButtonR2 = 105,
        ButtonThumbl = 106,
        ButtonThumbr = 107,
        ButtonStart = 108,
        ButtonSelect = 109,
        ButtonMode = 110,
        Escape = 111,
        ForwardDel = 112,
        CtrlLeft = 113,
        CtrlRight = 114,
        CapsLock = 115,
        ScrollLock = 116,
        MetaLeft = 117,
        MetaRight = 118,
        Function = 119,
        Sysrq = 120,
        Break = 121,
        MoveHome = 122,
        MoveEnd = 123,
        Insert = 124,
        Forward = 125,
        MediaPlay = 126,
        MediaPause = 127,
        MediaClose = 128,
        MediaEject = 129,
        MediaRecord = 130,
        F1 = 131,
        F2 = 132,
        F3 = 133,
        F4 = 134,
        F5 = 135,
        F6 = 136,
        F7 = 137,
        F8 = 138,
        F9 = 139,
        F10 = 140,
        F11 = 141,
        F12 = 142,
        NumLock = 143,
        Numpad0 = 144,
        Numpad1 = 145,
        Numpad2 = 146,
        Numpad3 = 147,
        Numpad4 = 148,
        Numpad5 = 149,
        Numpad6 = 150,
        Numpad7 = 151,
        Numpad8 = 152,
        Numpad9 = 153,
        NumpadDivide = 154,
        NumpadMultiply = 155,
        NumpadSubtract = 156,
        NumpadAdd = 157,
        NumpadDot = 158,
        NumpadComma = 159,
        NumpadEnter = 160,
        NumpadEquals = 161,
        NumpadLeftParen = 162,
        NumpadRightParen = 163,
        VolumeMute = 164,
        Info = 165,
        ChannelUp = 166,
        ChannelDown = 167,
        ZoomIn = 168,
        ZoomOut = 169,
        Tv = 170,
        Window = 171,
        Guide = 172,
        Dvr = 173,
        Bookmark = 174,
        Captions = 175,
        Settings = 176,
        TvPower = 177,
        TvInput = 178,
        StbPower = 179,
        StbInput = 180,
        AvrPower = 181,
        AvrInput = 182,
        ProgRed = 183,
        ProgGreen = 184,
        ProgYellow = 185,
        ProgBlue = 186,
        AppSwitch = 187,
        Button1 = 188,
        Button2 = 189,
        Button3 = 190,
        Button4 = 191,
        Button5 = 192,
        Button6 = 193,
        Button7 = 194,
        Button8 = 195,
        Button9 = 196,
        Button10 = 197,
        Button11 = 198,
        Button12 = 199,
        Button13 = 200,
        Button14 = 201,
        Button15 = 202,
        Button16 = 203,
        LanguageSwitch = 204,
        MannerMode = 205,
        Mode3D = 206,
        Contacts = 207,
        Calendar = 208,
        Music = 209,
        Calculator = 210,
        ZenkakuHankaku = 211,
        Eisu = 212,
        Muhenkan = 213,
        Henkan = 214,
        KatakanaHiragana = 215,
        Yen = 216,
        Ro = 217,
        Kana = 218,
        Assist = 219,
    };

    public enum AndroidAxis
    {
        X = 0,
        Y = 1,
        Pressure = 2,
        Size = 3,
        TouchMajor = 4,
        TouchMinor = 5,
        ToolMajor = 6,
        ToolMinor = 7,
        Orientation = 8,
        Vscroll = 9,
        Hscroll = 10,
        Z = 11,
        Rx = 12,
        Ry = 13,
        Rz = 14,
        HatX = 15,
        HatY = 16,
        Ltrigger = 17,
        Rtrigger = 18,
        Throttle = 19,
        Rudder = 20,
        Wheel = 21,
        Gas = 22,
        Brake = 23,
        Distance = 24,
        Tilt = 25,
        Generic1 = 32,
        Generic2 = 33,
        Generic3 = 34,
        Generic4 = 35,
        Generic5 = 36,
        Generic6 = 37,
        Generic7 = 38,
        Generic8 = 39,
        Generic9 = 40,
        Generic10 = 41,
        Generic11 = 42,
        Generic12 = 43,
        Generic13 = 44,
        Generic14 = 45,
        Generic15 = 46,
        Generic16 = 47,
    };

    [Serializable]
    public struct AndroidDeviceCapabilities
    {
        public string deviceDescriptor;
        public int productId;
        public int vendorId;
        public bool isVirtual;
        public AndroidAxis[] motionAxes;

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static AndroidDeviceCapabilities FromJson(string json)
        {
            if (json == null)
                throw new ArgumentNullException("json");
            return JsonUtility.FromJson<AndroidDeviceCapabilities>(json);
        }

        public override string ToString()
        {
            return string.Format(
                "deviceDescriptor = {0}, productId = {1}, vendorId = {2}, isVirtual = {3}, motionAxes = {4}",
                deviceDescriptor,
                productId,
                vendorId,
                isVirtual,
                string.Join(",", motionAxes.Select(i => i.ToString()).ToArray()));
        }
    }
}

namespace UnityEngine.Experimental.Input.Plugins.Android
{
    ////REVIEW: Is this guaranteed to be a Gamepad? If both joystick and gamepad come through here, we need to split this up.
    ////        Also, probably should be called AndroidGamepad
    [InputTemplate(stateType = typeof(AndroidGameControllerState))]
    public class AndroidGameController : Gamepad
    {
        internal static string OnFindTemplateForDevice(int deviceId, ref InputDeviceDescription description,
            string matchedTemplate, IInputRuntime runtime)
        {
            if (string.IsNullOrEmpty(matchedTemplate) || matchedTemplate != "AndroidGameController")
                return null;

            if (description.interfaceName != "Android" || description.deviceClass != "AndroidGameController")
                return null;

            if (!string.IsNullOrEmpty(description.capabilities))
            {
                var caps = AndroidDeviceCapabilities.FromJson(description.capabilities);
                if (caps.motionAxes != null)
                {
                    if (caps.motionAxes.Contains(AndroidAxis.HatX) && caps.motionAxes.Contains(AndroidAxis.HatY))
                        return "AndroidGamepadWithDpadAxes";
                }
            }

            return "AndroidGamepadWithDpadButtons";
        }
    }
}
#endif // UNITY_EDITOR || UNITY_ANDROID