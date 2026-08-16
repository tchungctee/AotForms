
using ClickableTransparentOverlay;
using Client;
using Guna.UI2.WinForms;
using ImGuiNET;
using KhoaVuxMem;
using Loader;
using Memory;
using Microsoft.VisualBasic.Logging;
using SharpGen.Runtime;
using SmartMewwxSeww;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Windows.Forms;
using x;
using static AotForms.Config;
using static AotForms.WinAPI;
using System.IO;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace AotForms
{
    public static class FontManager
    {
        public static ImFontPtr VerdanaSmall;
        public static ImFontPtr VerdanaNormal;
        public static ImFontPtr VerdanaBig;

        public static ImFontPtr InterSmall;
        public static ImFontPtr InterNormal;
        public static ImFontPtr InterBig;

        // THÊM FONT TIẾNG VIỆT
        public static ImFontPtr VietnameseFont;
        public static ImFontPtr VietnameseFontBig;
    }

    internal partial class ESP : ClickableTransparentOverlay.Overlay
    {
        string fontpath = "C:\\Windows\\Fonts\\taileb.ttf";
        // QUAN TRỌNG: Thêm glyph ranges cho tiếng Việt
        public ESP() : base("Efficiency")
        {
            // Khởi tạo cancellation token source
            _cancellationTokenSource = new CancellationTokenSource();

            // Có thể khởi tạo thêm các biến cần thiết
        }
        // ============ CONFIG ============
        public static bool BrutalEnabled = false;
        public static Keys BrutalKey = Keys.F9;
        public static string BrutalKeyLabel = "F9";
        public static bool WaitingForKeybindBrutal = false;
        public static bool KeyAlreadyPressedBrutal = false;

        // ============ STATE ============

        private static Dictionary<long, string> savedSpeedDown = new();
        private static Dictionary<long, string> savedSpeedFire = new();
        private static string brutalStatus = "Ready";
        private static float statusTimer = 0f;
        private const float STATUS_DURATION = 3f;

        private static bool isExecuted = false;
        private static string statusMessage = "Ready";
        private static bool removeBugCam = false;
        private static string statusBugCam = "Not yet done.";

        // ============ CONFIG ============
        public static bool WallEnabled = false;
        public static Keys WallKey = Keys.F10;
        public static string WallKeyLabel = "F10";
        public static bool WaitingForKeybindWall = false;
        public static bool KeyAlreadyPressedWall = false;

        // ============ STATE ============

        private static Dictionary<long, string> savedWall = new();
        private static string wallStatus = "Ready";


        // ============ STATE ============

        private static Dictionary<long, string> savedWall1 = new();
        private static bool noReload = false;
        private static string statusNoReload = "Not yet done.";


        private static bool isInitialized = false; // CHO BRUTAL
        private static bool isWallInitialized = false; // CHO WALL
        private static readonly Random rnd = new Random();
        // Thêm các hằng số và enum này vào class

        const int WS_EX_APPWINDOW = 0x00040000;
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        private Vector2 name2Pos = new Vector2(100, 100);
        private Vector2 name2Dir = new Vector2(1, 1);
        private readonly Stopwatch zyreTimer = Stopwatch.StartNew();
        private long lastUpdate = 0;
        enum WDA : uint
        {
            WDA_NONE = 0x00000000,
            WDA_MONITOR = 0x00000001,
            WDA_EXCLUDEFROMCAPTURE = 0x00000011,
        }

        private const uint WDA_MONITOR = 0x00000001;
        private const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;
        private static AuthResponse currentAuth = null;
        private static bool isLicenseValid = false;
        private static string licenseStatus = "Chua dang nhap";
        private static DateTime? licenseExpiry = null;
        private static string currentLicenseKey = "";
        // Thêm các hàm WinAPI
        // Thêm các hàm WinAPI cho Stream Mode
        [DllImport("user32.dll")]
        public static extern bool GetWindowDisplayAffinity(IntPtr hWnd, ref uint dwAffinity);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize,
            IntPtr hdcSrc, ref Point pprSrc, uint crKey, ref BLENDFUNCTION pblend, uint dwFlags);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);


        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]




        private static extern short GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll")]
        private static extern bool Beep(int frequency, int duration);
        private CX memoryfast = new CX();
        private bool anticheat = false;
        private static KhoaVu m = new KhoaVu();
        Mem sew = new Mem();
        public static int AimAssistRadius = 100;
        private bool isProcessing = false;
        private float progress = 0.0f;
        private ConcurrentDictionary<int, EntityRenderData> processedEntities = new();
        private Task entityProcessingTask;
        private struct EntityRenderData
        {
            public Vector2 headScreenPos;
            public Vector2 bottomScreenPos;
            public float Distance;
            public bool IsValid;
        }
        static hailong Gay = new hailong();
        private static Dictionary<string, IntPtr> _weaponIcons = new();
        public static class FontManager
        {
            public static ImFontPtr SmallFont;
            public static ImFontPtr BigFont;
        }
        private CancellationTokenSource _cancellationTokenSource;
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);
        public static bool IsKeyDown(Keys key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }
        // Start a background task to listen for key presses



        private void StartHotkeyThread()
        {
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        // === SPEED ===
                        if (KeyHelper.IsKeyDown(Config.SpKey))
                        {
                            if (!Config.KeyAlreadyPressed)
                            {
                                Config.speed = !Config.speed;
                                Config.KeyAlreadyPressed = true;
                            }
                        }
                        else Config.KeyAlreadyPressed = false;

                        // === TELEKILL ===
                        if (KeyHelper.IsKeyDown(Config.TelePortKey))
                        {
                            if (!Config.KeyAlreadyPressed3)
                            {
                                Config.telekill = !Config.telekill;
                                Config.KeyAlreadyPressed3 = true;
                            }
                        }
                        else Config.KeyAlreadyPressed3 = false;

                        // === PROX TELEKILL ===
                        if (KeyHelper.IsKeyDown(Config.TeleKillKey))
                        {
                            if (!Config.KeyAlreadyPressed4)
                            {
                                Config.proxtelekill = !Config.proxtelekill;
                                Config.KeyAlreadyPressed4 = true;
                            }
                        }
                        else Config.KeyAlreadyPressed4 = false;

                        // === UP PLAYER ===
                        if (KeyHelper.IsKeyDown(Config.UpPlayerKey))
                        {
                            if (!Config.KeyAlreadyPressed6)
                            {
                                Config.UpPlayer = !Config.UpPlayer;
                                Config.KeyAlreadyPressed6 = true;
                            }
                        }
                        else Config.KeyAlreadyPressed6 = false;

                        // === AI PLAYER ===
                        if (KeyHelper.IsKeyDown(Config.AiplayerKey))
                        {
                            if (!Config.KeyAlreadyPressed7)
                            {
                                Config.Aiplayer = !Config.Aiplayer;
                                Config.KeyAlreadyPressed7 = true;
                                Console.WriteLine("Toggled Aiplayer: " + Config.Aiplayer);
                            }
                        }
                        else Config.KeyAlreadyPressed7 = false;



                        // === FAKE WALL ===
                        if (KeyHelper.IsKeyDown(Config.walltKey))
                        {
                            if (!Config.KeyAlreadyPressed9)
                            {
                                Config.wall = !Config.wall;
                                Config.KeyAlreadyPressed9 = true;
                                Console.WriteLine("Toggled Fake Wall: " + Config.wall);
                            }
                        }
                        else Config.KeyAlreadyPressed9 = false;

                        // === UNDER PLAYER ===
                        if (KeyHelper.IsKeyDown(Config.underplayerKey))
                        {
                            if (!Config.KeyAlreadyPressed10)
                            {
                                Config.teliport = !Config.teliport;
                                Config.KeyAlreadyPressed10 = true;
                                Console.WriteLine("Toggled Under Player: " + Config.teliport);
                            }
                        }
                        else Config.KeyAlreadyPressed10 = false;
                        if (KeyHelper.IsKeyDown(BrutalKey))
                        {
                            if (!KeyAlreadyPressedBrutal)
                            {
                                ToggleBrutal();
                                KeyAlreadyPressedBrutal = true;
                                Console.WriteLine($"Brutal toggled: {BrutalEnabled}");
                            }
                        }
                        else
                        {
                            KeyAlreadyPressedBrutal = false;
                        }

                        if (KeyHelper.IsKeyDown(Config.fixespKey1))
                        {
                            if (!Config.KeyAlreadyPressed13)
                            {
                                Config.fixesp = !Config.fixesp;
                                Config.KeyAlreadyPressed13 = true;
                            }
                        }
                        if (KeyHelper.IsKeyDown(WallKey))
                        {
                            if (!KeyAlreadyPressedWall)
                            {
                                // GỌI HÀM TOGGLE TỪ WALLV1 CLASS
                                ToggleWall();
                                KeyAlreadyPressedWall = true;
                                Console.WriteLine($"Wall toggled: {WallEnabled}");
                            }
                        }
                        else
                        {
                            KeyAlreadyPressedWall = false;
                        }



                        Thread.Sleep(1);
                    }
                    catch { }
                }
            })
            { IsBackground = true }.Start();
        }
















        private void StartHotkeyListener()
        {
            new Thread(() =>
            {
                while (true)
                {
                    // SPEED
                    if (KeyHelper.IsKeyDown(Config.SpKey))
                    {
                        if (!Config.KeyAlreadyPressed)
                        {
                            Config.speed = !Config.speed;
                            Config.KeyAlreadyPressed = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed = false;
                    }

                    // TELEPORT
                    if (KeyHelper.IsKeyDown(Config.TelePortKey))
                    {
                        if (!Config.KeyAlreadyPressed3)
                        {
                            Config.telekill = !Config.telekill;
                            Config.KeyAlreadyPressed3 = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed3 = false;
                    }

                    // TELEKILL
                    if (KeyHelper.IsKeyDown(Config.TeleKillKey))
                    {
                        if (!Config.KeyAlreadyPressed4)
                        {
                            Config.proxtelekill = !Config.proxtelekill;
                            Config.KeyAlreadyPressed4 = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed4 = false;
                    }

                    // UP PLAYER
                    if (KeyHelper.IsKeyDown(Config.UpPlayerKey))
                    {
                        if (!Config.KeyAlreadyPressed6)
                        {
                            Config.UpPlayer = !Config.UpPlayer;
                            Config.KeyAlreadyPressed6 = true;
                        }
                    }
                    else
                    {
                        Config.KeyAlreadyPressed6 = false;
                    }

                    Thread.Sleep(10); // tránh ăn 100% CPU
                }
            })
            { IsBackground = true }.Start();
        }



        int EnemyCount = 0;
        IntPtr hWnd;
        IntPtr HDPlayer;
        private Vector4 lineColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 fovColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 boxColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 fillboxColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 crossColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 ESPLineDuoiColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector4 skeletonColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private bool isAutoRefreshChecked = false;
        private int selectedBoxIndex = 0;



        private readonly string[] _comboItems2 = { "Closest To Crosshair", "Target 360", "Closest player", "Lowest health" };
        private readonly string[] _comboItems1 = { "Silent Aim", "Aimbot Rage(Risky)" };
        private readonly string[] _comboItems = { "AimBot", "Aim Mouse", "Aim By XynQaw", "Silent Aim" };
        private readonly string[] _headerItems = { "Aim", "Esp", "Colors", "Extras", "Misc" };
        private int _selectedHeader, _comboBox, _comboBox1, _comboBox2;

        private bool isAutoRefreshActive = false; private async void autorefresh_Tick(object sender, EventArgs e)
        {
            while (isAutoRefreshActive) // Stop the loop if the flag is false
            {
                InternalMemory.Cache = new();
                Core.Entities = new();

                await Task.Delay(1000); // Wait for 1 second before looping again
            }
        }
        private async void AntiCheat()
        {
            var patterns = new (string search, string replace)[]
            {
        ("00 48 2D E9 0D B0 A0 E1 60 D0 4D E2 E8 20 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 24 00 8D E5 20 10 8D E5 24 00 9D E5 50 00 C0 F2 28 10 4B E2 01 20 A0 E1 CD 0A 42 F4 CF 0A 42 F4 00 20 A0 E3 2A 20 4B E5 3D 28 06 E3 BC 22 4B E1 2C 20 4B E2",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 28 D0 4D E2 84 20 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 14 00 8D E5 10 10 8D E5 14 00 9D E5 10 10 9D E5 10 20 4B E2 04 00 8D E5 02 00 A0 E1 00 20 8D E5",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 10 D0 4D E2 04 00 0B E5 08 10 8D E5 04 00 1B E5 08 10 9D E5 00 10 80 E5 00 10 90 E5 04 00 8D E5 01 00 A0 E1 CF FF FF EB 04 10 9D E5 00 00 8D E5 01 00 A0 E1 0B D0 A0 E1 00 88 BD E8",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 10 D0 4D E2 04 00 0B E5 04 00 1B E5 00 10 90 E5 08 00 8D E5 01 00 A0 E1 BC FF FF EB FF FF FF EA 08 00 9D E5 0B D0 A0 E1 00 88 BD E8",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 68 D0 4D E2 00 10 E0 E3",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B8 12 9F E5",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 C0 D0 4D E2 40 15 9F E5",
 "00 00 A0 E3 1E FF 2F E1"),

("00 48 2D E9 0D B0 A0 E1 70 D0 4D E2 B8 12 9F E5",
 "00 00 A0 E3 1E FF 2F E1"),

("30 48 2D E9 08 B0 8D E2 78 D0 4D E2 01 DA 4D E2 C0 13 9F E5 01 10 8F E0 BC 23 9F E5 02 20 9F E7 00 20 92 E5",
 "00 00 A0 E3 1E FF 2F E1"),

("30 48 2D E9 08 B0 8D E2 8E DF 4D E2 F8 13 9F E5",
 "00 00 A0 E3 1E FF 2F E1"),

("30 48 2D E9 08 B0 8D E2 42 DF 4D E2 DC 02 9F E5",
 "00 00 A0 E3 1E FF 2F E1"),

("30 48 2D E9 08 B0 8D E2 18 D0 4D E2 0C 00 0B E5 10 10 8D E5 10 00 9D E5 EC 00 90 E5 01 00 D0 E5 0F 00 CD E5",
 "00 00 A0 E3 1E FF 2F E1"),

("30 48 2D E9 08 B0 8D E2 20 D0 4D E2 10 C0 9B E5 0C E0 9B E5 08 40 9B E5 D0 50 9F E5 05 50 9F E7 00 50 95 E5 0C 50 0B E5",
 "00 00 A0 E3 1E FF 2F E1"),

("30 48 2D E9 08 B0 8D E2 2E DE 4D E2 22 00 4B E2",
 "00 20 70 47"),

("0A 00 A0 E3 5C 10 93 E5 B0 49 C1 E5 5C 10 93 E5",
 "00 F0 20 E3")


            };

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            bool success = false;

            foreach (var (search, replace) in patterns)
            {
                IEnumerable<long> addresses = await m.AoBScan2(search, writable: true);

                if (!addresses.Any())
                {

                    continue;
                }

                foreach (var addr in addresses)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", replace);
                }

                success = true;
            }

            if (success)
            {

                Console.Beep(1000, 500);
                MessageBox.Show("Done Anticheat VIP");
            }
        }
        public static async void ExecuteFastReload()
        {
            if (isExecuted)
            {
                SetStatus("Fast Reload already executed!");
                return;
            }

            try
            {
                SetStatus("Starting Fast Reload...");

                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    SetStatus("HD-Player not found!");
                    return;
                }

                int proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);

                // SCAN & REP FAST RELOAD
                SetStatus("Scanning Fast Reload pattern...");
                var addresses = await m.AoBScan2("10 0a 18 ee 02 8b bd ec 30 88 bd e8 30 48 2d e9 08 b0 8d e2", writable: true);

                if (!addresses.Any())
                {
                    SetStatus("Fast Reload pattern not found!");
                    return;
                }

                // APPLY PATCH
                SetStatus("Patching Fast Reload...");
                foreach (var addr in addresses)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", "10 0a 18 ee 02 8b bd ec 30 88 bd e8 ff 00 45 e3 1e ff 2f e1");
                }

                isExecuted = true;
                SetStatus("FAST RELOAD ACTIVATED!");
                Console.Beep(1200, 500);
            }
            catch (Exception ex)
            {
                SetStatus($"Error: {ex.Message}");
            }
        }

        private static void SetStatus(string message)
        {

        }
        private async void AntiCheat2()
        {
            var patterns = new (string search, string replace)[]
            {
       ("00 48 2D E9 0D B0 A0 E1 C0 D0 4D E2 40 15 9F E5", "00 00 A0 E3 1E FF 2F E1"),
("00 48 2D E9 0D B0 A0 E1 60 D0 4D E2 E8 20 9F E5 02 20 9F E7 00 20 92 E5 04 20 0B E5 24 00 8D E5 20 10 8D E5 24 00 9D E5 50 00 C0 F2 28 10 4B E2 01 20 A0 E1 CD 0A 42 F4 CF 0A 42 F4 00 20 A0 E3 2A 20 4B E5 3D 28 06 E3 BC 22 4B E1 2C 20 4B E2", "00 00 A0 E3 1E FF 2F E1"),
("00 48 2D E9 0D B0 A0 E1 28 D0 4D E2 84 20 9F E5", "00 00 A0 E3 1E FF 2F E1"),
("00 48 2D E9 0D B0 A0 E1 10 D0 4D E2 04 00 0B E5 08 10 8D E5 04 00 1B E5 08 10 9D E5 00 10 80 E5 00 10 90 E5 04 00 8D E5 01 00 A0 E1 CF FF FF EB 04 10 9D E5 00 00 8D E5 01 00 A0 E1 0B D0 A0 E1 00 88 BD E8", "00 00 A0 E3 1E FF 2F E1"),
("00 48 2D E9 0D B0 A0 E1 10 D0 4D E2 04 00 0B E5 04 00 1B E5 00 10 90 E5 08 00 8D E5 01 00 A0 E1 BC FF FF EB FF FF FF EA 08 00 9D E5 0B D0 A0 E1 00 88 BD E8", "00 00 A0 E3 1E FF 2F E1")
            };

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            bool success = false;

            foreach (var (search, replace) in patterns)
            {
                IEnumerable<long> addresses = await m.AoBScan2(search, writable: true);

                if (!addresses.Any())
                {
                    MessageBox.Show($"Không tìm thấy chuỗi: {search}");
                    continue;
                }

                foreach (var addr in addresses)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", replace);
                }

                success = true;
            }

            if (success)
            {

                Console.Beep(1000, 500);
                MessageBox.Show("Done Anticheat Step 2");
            }
        }
        private async void AntiCheat3()
        {
            var patterns = new (string search, string replace)[]
            {
       ("30 48 2D E9 08 B0 8D E2 4A DF 4D E2 AC C4 9F E5 0C C0 9F E7 00 C0 9C E5 0C C0 0B E5 8C 00 0B E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 2F DE 4D E2 0C 1E 9F E5 01 10 9F E7 00 10 91 E5 0C 10 0B E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 C6 DF 4D E2 20 20 4B E2 08 38 9F E5 03 30 8F E0 04 C8 9F E5 0C C0 9F E7 00 C0 9C E5 0C C0 0B E5 A4 00 8D E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 42 DF 4D E2 01 DB 4D E2 08 C0 9B E5 7C E5 9F E5 0E E0 9F E7 00 E0 9E E5", "00 00 A0 E3 1E FF 2F E1"),
("F0 4B 2D E9 18 B0 8D E2 26 DE 4D E2 0C 10 9B E5 08 C0 9B E5 A0 EB 9F E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 4E DF 4D E2 04 C8 9F E5", "00 00 A0 E3 1E FF 2F E1"),
("00 48 2D E9 0D B0 A0 E1 98 D0 4D E2 CC C2 9F E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 39 DE 4D E2 6C CD 9F E5 0C C0 9F E7 00 C0 9C E5 0C C0 0B E5 44 01 8D E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 48 D0 4D E2 01 DB 4D E2 84 02 9F E5 00 00 8F E0 90 12 9F E5", "00 00 A0 E3 1E FF 2F E1"),
("10 4C 2D E9 08 B0 8D E2 78 D0 4D E2 0C C0 9B E5 08 E0 9B E5 58 44 9F E5 04 40 9F E7 00 40 94 E5 0C 40 0B E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 56 DF 4D E2 01 DB 4D E2 68 10 9F E5 01 10 9F E7 00 10 91 E5 0C 10 0B E5 A8 02 0B E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 4E DF 4D E2 04 C8 9F E5 0C C0 9F E7 00 C0 9C E5 0C C0 0B E5 98 00 8D E5 A0 10 8D E5 94 20 8D E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 2D DE 4D E2 08 C0 9B E5 20 EB 9F E5 0E E0 9F E7 00 E0 9E E5 0C E0 0B E5 84 00 8D E5 C8 10 8D E5 C4 20 8D E5", "00 00 A0 E3 1E FF 2F E1"),
("10 4C 2D E9 08 B0 8D E2 C0 D0 4D E2 0C C0 9B E5 08 E0 9B E5 48 48 9F E5 04 40 9F E7 00 40 94 E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 2F DE 4D E2 94 1D 9F E5 01 10 9F E7 00 10 91 E5 0C 10 0B E5 94 00 8D E5 94 00 9D E5 00 10 00 E3 93 10 CD E5", "00 00 A0 E3 1E FF 2F E1"),
("10 4C 2D E9 08 B0 8D E2 A6 DF 4D E2 0C C0 9B E5 08 E0 9B E5 EC 44 9F E5 04 40 9F E7 00 40 94 E5 0C 40 0B E5", "00 00 A0 E3 1E FF 2F E1"),
("30 48 2D E9 08 B0 8D E2 92 DF 4D E2 01 DB 4D E2", "00 00 A0 E3 1E FF 2F E1"),
("10 4C 2D E9 08 B0 8D E2 90 D0 4D E2 D0 14 9F E5 01 10 9F E7 00 10 91 E5 0C 10 0B E5 34 00 0B E5", "00 00 A0 E3 1E FF 2F E1")
            };

            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                MessageBox.Show("Process not detected!");
                return;
            }

            int proc = Process.GetProcessesByName("HD-Player")[0].Id;
            m.OpenProcess(proc);

            bool success = false;

            foreach (var (search, replace) in patterns)
            {
                IEnumerable<long> addresses = await m.AoBScan2(search, writable: true);

                if (!addresses.Any())
                {
                    MessageBox.Show($"Không tìm thấy chuỗi: {search}");
                    continue;
                }

                foreach (var addr in addresses)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", replace);
                }

                success = true;
            }

            if (success)
            {

                Console.Beep(1000, 500);
                MessageBox.Show("Done Anticheat Step 3");
            }
        }

        // ============ BRUTAL FUNCTIONS ============
        public static void ToggleBrutal()
        {
            if (BrutalEnabled)
            {
                DisableBrutal();
            }
            else
            {
                EnableBrutal();
            }
        }

        private static void EnableBrutal()
        {
            if (!isInitialized)
            {
                SetStatus("Chưa scan patterns lần đầu!", true);
                BrutalEnabled = false;
                return;
            }

            try
            {
                if (!OpenProcess()) return;

                // BẬT SPEED DOWN
                foreach (var addr in savedSpeedDown.Keys)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", "00 00 80 40 00 00 80 40 CB D2 4D 3E");
                }

                // BẬT SPEED FIRE
                foreach (var addr in savedSpeedFire.Keys)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", "08 39 60 3B 08 39 60 3B 08 39 60 3B");
                }

                SetStatus("BRUTAL MODE ON!", false);
                BrutalEnabled = true;
                Console.Beep(1000, 300);
            }
            catch (Exception ex)
            {
                SetStatus($"Enable Error: {ex.Message}", true);
                BrutalEnabled = false;
            }
        }

        private static void DisableBrutal()
        {
            try
            {
                if (!OpenProcess()) return;

                // TẮT SPEED DOWN
                foreach (var patch in savedSpeedDown)
                {
                    m.WriteMemory(patch.Key.ToString("X"), "bytes", patch.Value);
                }

                // TẮT SPEED FIRE
                foreach (var patch in savedSpeedFire)
                {
                    m.WriteMemory(patch.Key.ToString("X"), "bytes", patch.Value);
                }

                SetStatus("BRUTAL MODE OFF!", false);
                BrutalEnabled = false;
                Console.Beep(800, 300);
            }
            catch (Exception ex)
            {
                SetStatus($"Disable Error: {ex.Message}", true);
                BrutalEnabled = false;
            }
        }

        // ============ SCAN PATTERNS ============
        public static async void ScanPatternsFirstTime()
        {
            try
            {
                if (!OpenProcess()) return;

                // SCAN SPEED DOWN
                var addresses1 = await m.AoBScan2("00 00 80 40 33 33 93 40 3D 0A F7 3F", writable: true);
                foreach (var addr in addresses1)
                {
                    savedSpeedDown[addr] = "00 00 80 40 33 33 93 40 3D 0A F7 3F";
                }

                // SCAN SPEED FIRE
                var addresses2 = await m.AoBScan2("02 2B 07 3D 02 2B 07 3D 02 2B 07 3D", writable: true);
                foreach (var addr in addresses2)
                {
                    savedSpeedFire[addr] = "02 2B 07 3D 02 2B 07 3D 02 2B 07 3D";
                }

                isInitialized = true;
                SetStatus("Scan completed! Ready to toggle Brutal.", false);
            }
            catch (Exception ex)
            {
                SetStatus($"Scan Error: {ex.Message}", true);
            }
        }

        // ============ HELPER FUNCTIONS ============
        private static bool OpenProcess()
        {
            try
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    SetStatus("HD-Player not found!", true);
                    return false;
                }

                int proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);
                return true;
            }
            catch
            {
                SetStatus("Cannot open process!", true);
                return false;
            }
        }

        private static void SetStatus(string message, bool isError)
        {
            brutalStatus = message;
            statusTimer = STATUS_DURATION;
            Console.WriteLine($"Brutal: {message}");
        }




        private void RestoreSpeedDown()
        {
            foreach (var patch in savedSpeedDown)
            {
                try
                {
                    m.WriteMemory(patch.Key.ToString("X"), "bytes", patch.Value);
                }
                catch { }
            }
            savedSpeedDown.Clear();
            Console.Beep(800, 200);
        }

        private void RestoreSpeedFire()
        {
            foreach (var patch in savedSpeedFire)
            {
                try
                {
                    m.WriteMemory(patch.Key.ToString("X"), "bytes", patch.Value);
                }
                catch { }
            }
            savedSpeedFire.Clear();
            Console.Beep(800, 200);
        }

        // ============ WALL FUNCTIONS ============
        public static void ToggleWall()
        {
            if (WallEnabled)
            {
                DisableWallv1();
            }
            else
            {
                EnableWallv1();
            }
        }

        private static void EnableWallv1()
        {
            if (!isInitialized)
            {
                SetStatus("Chưa scan patterns lần đầu!", true);
                WallEnabled = false;
                return;
            }

            try
            {
                if (!OpenProcess()) return;

                // BẬT WALL
                foreach (var addr in savedWall.Keys)
                {
                    m.WriteMemory(addr.ToString("X"), "bytes", "00 00 ef c1 ae 47 81 3f ae 47 81 3f ae 47 81 3f 00 1a b7 ee dc 3a 9f ed 30");
                }

                SetStatus("WALL V1 ON!", false);
                WallEnabled = true;
                Console.Beep(1000, 300);
            }
            catch (Exception ex)
            {
                SetStatus($"Enable Error: {ex.Message}", true);
                WallEnabled = false;
            }
        }

        private static void DisableWallv1()
        {
            try
            {
                if (!OpenProcess()) return;

                // TẮT WALL
                foreach (var patch in savedWall)
                {
                    m.WriteMemory(patch.Key.ToString("X"), "bytes", patch.Value);
                }

                SetStatus("WALL V1 OFF!", false);
                WallEnabled = false;
                Console.Beep(800, 300);
            }
            catch (Exception ex)
            {
                SetStatus($"Disable Error: {ex.Message}", true);
                WallEnabled = false;
            }
        }

        // ============ SCAN PATTERNS ============
        public static async void ScanPatternsFirstTimewall()
        {
            try
            {
                if (!OpenProcess()) return;

                // SCAN WALL
                var addresses = await m.AoBScan2("ae 47 81 3f ae 47 81 3f ae 47 81 3f ae 47 81 3f 00 1a b7 ee dc 3a 9f ed 30", writable: true);
                foreach (var addr in addresses)
                {
                    savedWall[addr] = "ae 47 81 3f ae 47 81 3f ae 47 81 3f ae 47 81 3f 00 1a b7 ee dc 3a 9f ed 30";
                }

                isWallInitialized = true; // DÙNG BIẾN RIÊNG CHO WALL
                SetStatus("Scan completed! Ready to toggle Wall.", false);
            }
            catch (Exception ex)
            {
                SetStatus($"Scan Error: {ex.Message}", true);
            }
        }

        // ============ HELPER FUNCTIONS ============
        private static bool OpenProcessv1()
        {
            try
            {
                if (Process.GetProcessesByName("HD-Player").Length == 0)
                {
                    SetStatus("HD-Player not found!", true);
                    return false;
                }

                int proc = Process.GetProcessesByName("HD-Player")[0].Id;
                m.OpenProcess(proc);
                return true;
            }
            catch
            {
                SetStatus("Cannot open process!", true);
                return false;
            }
        }

        private static void SetStatusv1(string message, bool isError)
        {
            wallStatus = message;
            statusTimer = STATUS_DURATION;
            Console.WriteLine($"Wall: {message}");
        }





        private void DrawRainbowLine(Vector2 startPoint, Vector2 headPoint, uint baseColor, float thickness,
       float glowRadius, float feather, float glowOpacityMultiplier, LineMode mode)
        {
            float distance = Vector2.Distance(startPoint, headPoint);
            if (distance < 15f) return; // TĂNG NGƯỠNG LÊN 15f

            var drawList = ImGui.GetBackgroundDrawList();
            const int NUM_STEPS = 50;
            float timeFactor = Environment.TickCount * 0.001f;

            Vector2[] points = GenerateLinePoints(startPoint, headPoint, mode, NUM_STEPS, timeFactor, distance);

            // LỌC ĐIỂM TRƯỚC KHI VẼ
            List<Vector2> validPoints = new List<Vector2>();
            for (int i = 0; i < points.Length; i++)
            {
                if (!IsInvalidPoint(points[i]))
                {
                    validPoints.Add(points[i]);
                }
            }

            // VẼ CÁC ĐOẠN HỢP LỆ
            for (int i = 1; i < validPoints.Count; i++)
            {
                Vector2 prevPoint = validPoints[i - 1];
                Vector2 currentPoint = validPoints[i];

                // KIỂM TRA NGHIÊM NGẶT HƠN
                float segmentDistance = Vector2.Distance(prevPoint, currentPoint);
                if (segmentDistance < 3f) continue; // TĂNG NGƯỠNG LÊN 3f
                if (segmentDistance > distance * 1.3f) continue; // TĂNG NGƯỠNG LÊN 1.3f

                uint segmentColor = Config.ESPRainbowMode ? GetRainbowColor(i, validPoints.Count, timeFactor) : baseColor;
                DrawGlowSegmentOptimized(drawList, prevPoint, currentPoint, segmentColor, thickness, glowRadius, feather, glowOpacityMultiplier);
            }
        }

        private bool IsInvalidPoint(Vector2 point)
        {
            return float.IsNaN(point.X) || float.IsNaN(point.Y) ||
                   float.IsInfinity(point.X) || float.IsInfinity(point.Y) ||
                   point.X < -1000 || point.X > Core.Width + 1000 ||
                   point.Y < -1000 || point.Y > Core.Height + 1000;
        }

        private uint GetRainbowColor(int index, int totalPoints, float timeFactor)
        {
            float phase = (index / (float)totalPoints) * MathF.PI * 2 + timeFactor;
            float red = 0.5f + 0.5f * MathF.Sin(phase);
            float green = 0.5f + 0.5f * MathF.Sin(phase + 2.094f);
            float blue = 0.5f + 0.5f * MathF.Sin(phase + 4.189f);

            return ImGui.ColorConvertFloat4ToU32(new Vector4(
                Math.Clamp(red, 0.1f, 1.0f),
                Math.Clamp(green, 0.1f, 1.0f),
                Math.Clamp(blue, 0.1f, 1.0f),
                1.0f
            ));
        }

        private Vector2[] GenerateLinePoints(Vector2 start, Vector2 end, LineMode mode, int numSteps, float timeFactor, float distance)
        {
            // Tạo một baseColor mặc định
            uint defaultBaseColor = ImGui.ColorConvertFloat4ToU32(new Vector4(1.0f, 1.0f, 1.0f, 1.0f));

            Vector2[] points = mode switch
            {
                LineMode.Straight => GenerateStraightLine(start, end, numSteps),
                LineMode.Curved => GenerateCurvedLine(start, end, numSteps),
                LineMode.ZigZag => GenerateZigZagLine(start, end, numSteps, timeFactor, distance),
                LineMode.Wave => GenerateWaveLine(start, end, numSteps, timeFactor, distance),
                LineMode.Spiral => GenerateSpiralLine(start, end, numSteps, timeFactor, distance),
                LineMode.Dashed => GenerateDashedLine(start, end, numSteps, timeFactor, distance),
                LineMode.Dotted => GenerateDottedLine(start, end, numSteps, timeFactor, distance),
                LineMode.Arrow => GenerateArrowLine(start, end, numSteps, timeFactor, distance, defaultBaseColor),
                LineMode.Lightning => GenerateLightningLine(start, end, numSteps, timeFactor, distance),
                LineMode.Spring => GenerateSpringLine(start, end, numSteps, timeFactor, distance),
                LineMode.Pulse => GeneratePulseLine(start, end, numSteps, timeFactor, distance),
                LineMode.DNA => GenerateDnaLine(start, end, numSteps, timeFactor, distance),
                LineMode.Electric => GenerateElectricLine(start, end, numSteps, timeFactor, distance),
                _ => GenerateStraightLine(start, end, numSteps)
            };

            // ĐẢM BẢO ĐIỂM ĐẦU VÀ CUỐI LUÔN ĐÚNG
            if (points.Length > 0)
            {
                points[0] = start;
                points[points.Length - 1] = end;
            }

            return points;
        }

        // SỬA LẠI TẤT CẢ CÁC HÀM - ĐƠN GIẢN VÀ AN TOÀN
        private Vector2[] GenerateDashedLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                bool isDash = (i % 8) < 4; // GIẢM MẬT ĐỘ DASH
                points[i] = isDash ? start + direction * (t * distance) : new Vector2(float.NaN, float.NaN);
            }
            return points;
        }

        private Vector2[] GenerateDottedLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);

            // GIẢM MẬT ĐỘ CHẤM
            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;

                if (i % 5 == 0 || i == 0 || i == numSteps) // GIẢM MẬT ĐỘ
                {
                    points[i] = start + direction * (t * distance);
                }
                else
                {
                    points[i] = new Vector2(float.NaN, float.NaN);
                }
            }
            return points;
        }

        private Vector2[] GenerateArrowLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance, uint baseColor)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);

            // TẠO ĐƯỜNG THẲNG
            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                points[i] = start + direction * (t * distance);
            }

            // VẼ MŨI TÊN TRỰC TIẾP
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);
            float arrowSize = Math.Min(distance * 0.08f, 12f); // GIẢM KÍCH THƯỚC MŨI TÊN
            Vector2 arrowTip = end;
            Vector2 arrowBase = end - direction * arrowSize;
            Vector2 arrowLeft = arrowBase + perpendicular * (arrowSize * 0.5f);
            Vector2 arrowRight = arrowBase - perpendicular * (arrowSize * 0.5f);

            var drawList = ImGui.GetBackgroundDrawList();
            uint arrowColor = Config.ESPRainbowMode ? GetRainbowColor(numSteps, numSteps, timeFactor) : baseColor;
            drawList.AddLine(arrowTip, arrowLeft, arrowColor, 1.5f); // GIẢM ĐỘ DÀY
            drawList.AddLine(arrowTip, arrowRight, arrowColor, 1.5f);

            return points;
        }

        private Vector2[] GenerateLightningLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                Vector2 basePoint = start + direction * (t * distance);

                if (i % 8 == 0) // GIẢM MẬT ĐỘ JITTER
                {
                    float jitter = (float)(rnd.NextDouble() - 0.5) * 4f; // GIẢM BIÊN ĐỘ
                    points[i] = basePoint + perpendicular * jitter;
                }
                else
                {
                    points[i] = basePoint;
                }
            }
            return points;
        }

        private Vector2[] GenerateSpringLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                float wave = MathF.Sin(t * 12f + timeFactor * 3f) * 3f; // GIẢM BIÊN ĐỘ
                points[i] = start + direction * (t * distance) + perpendicular * wave;
            }
            return points;
        }

        private Vector2[] GeneratePulseLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);

            // HIỆU ỨNG PULSE - GIẢM TẦN SUẤT
            bool isVisible = (int)(timeFactor * 4f) % 2 == 0; // GIẢM TỐC ĐỘ

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;

                if (isVisible && (i % 10 < 8)) // GIẢM MẬT ĐỘ
                {
                    points[i] = start + direction * (t * distance);
                }
                else
                {
                    points[i] = new Vector2(float.NaN, float.NaN);
                }
            }

            return points;
        }

        private Vector2[] GenerateDnaLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                float wave1 = MathF.Sin(t * 8f + timeFactor * 2f) * 4f; // GIẢM BIÊN ĐỘ
                float wave2 = MathF.Cos(t * 8f + timeFactor * 2f) * 3f;
                points[i] = start + direction * (t * distance) + perpendicular * (wave1 + wave2);
            }
            return points;
        }

        private Vector2[] GenerateElectricLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                float jitter = (float)((rnd.NextDouble() - 0.5) * 6f * MathF.Sin(timeFactor * 4f + i * 0.2f)); // GIẢM BIÊN ĐỘ
                Vector2 basePoint = start + direction * (t * distance);
                points[i] = basePoint + perpendicular * Math.Clamp(jitter, -8f, 8f);
            }
            return points;
        }

        // CÁC HÀM CŨ - GIẢM BIÊN ĐỘ
        private Vector2[] GenerateStraightLine(Vector2 start, Vector2 end, int numSteps)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                points[i] = new Vector2(start.X + (end.X - start.X) * t, start.Y + (end.Y - start.Y) * t);
            }
            return points;
        }

        private Vector2[] GenerateCurvedLine(Vector2 start, Vector2 end, int numSteps)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 midPoint = (start + end) * 0.5f;
            Vector2 controlPoint = new Vector2(midPoint.X + 15f, midPoint.Y - 30f); // GIẢM ĐỘ CONG

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                float oneMinusT = 1 - t;
                points[i] = new Vector2(
                    oneMinusT * oneMinusT * start.X + 2 * oneMinusT * t * controlPoint.X + t * t * end.X,
                    oneMinusT * oneMinusT * start.Y + 2 * oneMinusT * t * controlPoint.Y + t * t * end.Y
                );
            }
            return points;
        }

        private Vector2[] GenerateZigZagLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            // ZIGZAG - GIẢM BIÊN ĐỘ
            int segments = 3; // GIẢM SỐ ĐOẠN
            float segmentLength = distance / segments;
            float zigzagAmplitude = Math.Min(distance * 0.1f, 15f); // GIẢM BIÊN ĐỘ

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                Vector2 basePoint = start + direction * (t * distance);

                int segmentIndex = (int)(t * segments);
                float segmentT = (t * segments) - segmentIndex;

                float zigzag = 0f;

                if (segmentIndex == 0)
                {
                    zigzag = zigzagAmplitude;
                }
                else if (segmentIndex == 1)
                {
                    zigzag = zigzagAmplitude - (segmentT * 2 * zigzagAmplitude);
                }
                else if (segmentIndex == 2)
                {
                    zigzag = -zigzagAmplitude;
                }

                points[i] = basePoint + perpendicular * zigzag;
            }

            return points;
        }

        private Vector2[] GenerateWaveLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = Vector2.Normalize(end - start);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                float wave = MathF.Sin(t * 10f + timeFactor * 2f) * 6f; // GIẢM BIÊN ĐỘ
                points[i] = start + direction * (t * distance) + perpendicular * wave;
            }
            return points;
        }

        private Vector2[] GenerateSpiralLine(Vector2 start, Vector2 end, int numSteps, float timeFactor, float distance)
        {
            Vector2[] points = new Vector2[numSteps + 1];
            Vector2 direction = end - start;
            Vector2 normalizedDirection = Vector2.Normalize(direction);
            Vector2 perpendicular = new Vector2(-normalizedDirection.Y, normalizedDirection.X);
            float maxRadius = Math.Min(distance * 0.02f, 20f); // GIẢM BÁN KÍNH
            float spiralTurns = 1.5f; // GIẢM SỐ VÒNG

            for (int i = 0; i <= numSteps; i++)
            {
                float t = i / (float)numSteps;
                float angle = t * MathF.PI * 2 * spiralTurns + timeFactor * 1.5f;
                float radius = (1 - t * 0.8f) * maxRadius;
                Vector2 basePoint = start + normalizedDirection * (t * distance);
                Vector2 spiralOffset = perpendicular * (MathF.Cos(angle) * radius);
                points[i] = basePoint + spiralOffset;
            }
            points[numSteps] = end;
            return points;
        }

        private void DrawGlowSegmentOptimized(ImDrawListPtr drawList, Vector2 start, Vector2 end, uint color,
                                              float thickness, float glowRadius, float feather, float opacity)
        {
            float segmentLength = Vector2.Distance(start, end);
            if (segmentLength < 3f) return; // TĂNG NGƯỠNG LÊN 3f

            Vector4 colorVec = ColorU32ToFloat4(color);
            int glowSteps = Math.Clamp((int)(glowRadius / feather), 2, 4); // GIẢM SỐ BƯỚC GLOW

            for (int i = glowSteps; i > 0; i--)
            {
                float r = glowRadius * (i / (float)glowSteps);
                float alpha = colorVec.W * (r / glowRadius) * opacity * 0.7f; // GIẢM OPACITY
                uint glowColor = ImGui.ColorConvertFloat4ToU32(new Vector4(colorVec.X, colorVec.Y, colorVec.Z, alpha));
                drawList.AddLine(start, end, glowColor, thickness + r * 0.1f); // GIẢM HỆ SỐ GLOW
            }

            drawList.AddLine(start, end, color, thickness);
        }

        private Vector4 ColorU32ToFloat4(uint color)
        {
            const float inv255 = 1.0f / 255.0f;
            return new Vector4(
                (color & 0xFF) * inv255,
                ((color >> 8) & 0xFF) * inv255,
                ((color >> 16) & 0xFF) * inv255,
                ((color >> 24) & 0xFF) * inv255
            );
        }
        private void DrawMinimap()
        {
            const float detectionRange = 250f;
            const float minimapRadius = 110f;
            const float margin = 20f;

            Vector2 minimapCenter = new Vector2(
                margin + minimapRadius,
                Core.Height - minimapRadius - margin
            );

            ImDrawListPtr draw = ImGui.GetBackgroundDrawList();

            // === BACKGROUND ===
            draw.AddCircleFilled(minimapCenter, minimapRadius, ColorToUint32(Color.FromArgb(150, 15, 15, 15)));
            draw.AddCircle(minimapCenter, minimapRadius, ColorToUint32(Color.White), 64, 1f);

            // === COMPASS ===
            string[] compass = { "N", "E", "S", "W" };

            float yaw = GetCameraYaw();
            float cosYaw = MathF.Cos(yaw);
            float sinYaw = MathF.Sin(yaw);

            ImGui.PushFont(FontManager.SmallFont);

            for (int i = 0; i < 4; i++)
            {
                float angle = (i * MathF.PI / 2f) - MathF.PI / 2 + yaw;
                Vector2 pos = minimapCenter + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (minimapRadius - 12f);

                uint shadow = ColorToUint32(Color.FromArgb(170, 0, 0, 0));
                draw.AddText(pos + new Vector2(1, 1), shadow, compass[i]);

                uint col = (i == 0) ? ColorToUint32(Color.Red) : ColorToUint32(Color.White);
                draw.AddText(pos, col, compass[i]);
            }

            ImGui.PopFont();

            // === PLAYER MARKER ===
            draw.AddCircleFilled(minimapCenter, 6f, ColorToUint32(Color.Cyan));

            // hướng camera
            Vector2 arrow = new Vector2(MathF.Sin(yaw), -MathF.Cos(yaw));
            draw.AddLine(minimapCenter, minimapCenter + arrow * 13f, ColorToUint32(Color.Cyan), 1.5f);

            // === ENEMY MARKERS ===
            foreach (var e in Core.Entities.Values)
            {
                if (e.IsDead) continue;

                float dist = Vector3.Distance(Core.LocalMainCamera, e.Head);
                if (dist > detectionRange) continue;

                Vector3 rel = e.Head - Core.LocalMainCamera;

                // XOAY THEO CAMERA YAW (công thức chuẩn Free Fire)
                float x = rel.Z * sinYaw + rel.X * cosYaw;
                float y = rel.Z * cosYaw - rel.X * sinYaw;

                float scale = minimapRadius / detectionRange;
                Vector2 pos = new Vector2(
                    minimapCenter.X + x * scale,
                    minimapCenter.Y - y * scale
                );

                if (Vector2.Distance(minimapCenter, pos) <= minimapRadius)
                {
                    uint col =
                        e.IsKnocked ? ColorToUint32(Color.Yellow) :
                        e.IsKnown ? ColorToUint32(Color.Red) :
                                      ColorToUint32(Color.Blue);

                    draw.AddCircleFilled(pos, 4f, col);
                }
            }
        }


        private static bool IsKeyPressed(Keys key)
        {
            return (GetAsyncKeyState(key) & 1) != 0;
        }
        protected override unsafe void Render()
        {
            RenderImgui();
            if (Config.ShowSilentFov)
            {
                var center = new Vector2(ImGui.GetIO().DisplaySize.X / 2f, ImGui.GetIO().DisplaySize.Y / 2f);
                ImGui.GetForegroundDrawList().AddCircle(
                    center,
                    Config.SilentFov,
                    ImGui.GetColorU32(new Vector4(0f, 0.8f, 1f, 0.8f)), // màu xanh cyan
                    64,
                    1.5f
                );
            }

            if (!Core.HaveMatrix || Core.CameraMatrix == Matrix4x4.Identity)
                return;
            var tmp = Core.Entities;
            if (tmp == null || tmp.Count == 0)
                return;

            EnemyCount = tmp.Values.Count(entity => !entity.IsDead);

            CreateHandle();

            var windowWidth = Core.Width;
            var windowHeight = Core.Height;
            string text = $"LH VN - {EnemyCount}";
            var textSize = ImGui.CalcTextSize(text);
            float textPosX = (windowWidth - textSize.X) / 2;
            float textPosY = 80;

            var drawList = ImGui.GetForegroundDrawList();

            // Màu chữ và nền khung tiêu đề
            uint textColor = ImGui.GetColorU32(new Vector4(0f, 1f, 1f, 1f));
            uint bgColor = ImGui.GetColorU32(new Vector4(0f, 0.1f, 0.1f, 0.6f));
            uint borderCol = ImGui.GetColorU32(new Vector4(0f, 1f, 1f, 0.8f));

            float paddingX = 15f, paddingY = 10f;
            Vector2 boxMin = new Vector2(textPosX - paddingX, textPosY - paddingY);
            Vector2 boxMax = new Vector2(textPosX + textSize.X + paddingX, textPosY + textSize.Y + paddingY);
            float rounding = 5f;

            drawList.AddRectFilled(boxMin, boxMax, bgColor, rounding);
            drawList.AddRect(boxMin, boxMax, borderCol, rounding, ImDrawFlags.None, 2f);
            drawList.AddText(new Vector2(textPosX, textPosY), textColor, text);



            if (Config.showsta)
            {
                try
                {
                    // Danh sách các tính năng và biến hiển thị tương ứng
                    var features = new List<(string name, Func<bool> getState, bool showSetting)>
        {
            ("Up Player", () => Config.UpPlayer, Config.showup),
            ("Pull Enemy", () => Config.proxtelekill, Config.showpull),
            ("Tele Enemy", () => Config.telekill, Config.showteleport),
            ("Ai Player", () => Config.Aiplayer, Config.showai),
            
         //   ("Dive Kill", () => Config.teliport, Config.showdive),
            ("Fly", () => Config.teliport, Config.showfly),
            ("Silent Legit", () => Config.Slient2, Config.showsilent),
            ("Silent 360", () => Config.silent360, Config.showsilent2),
            ("AimBot Legit", () => Config.AimLegit, Config.showaimbot),
            ("Show FOV", () => Config.ShowFOV, Config.showfovaim),
            ("Ignore Knocked", () => Config.IgnoreKnocked, Config.showkno),
            
        };

                    // Lọc ra các tính năng được bật hiển thị
                    var enabledFeatures = features.Where(f => f.showSetting).ToList();

                    // ============ KHUNG HIỆN ĐẠI ================
                    Vector2 windowPos = new Vector2(12, 12);

                    // Tính toán kích thước cửa sổ
                    int itemCount = enabledFeatures.Count;
                    float lineHeight = 18f;
                    float headerHeight = 40f;
                    float minHeight = 60f;
                    float contentHeight = itemCount > 0 ? (itemCount * lineHeight) : 0;
                    Vector2 windowSize = new Vector2(165, Math.Max(minHeight, headerHeight + contentHeight));

                    // 🎨 Màu sắc - SỬA LẠI CHO CHÍNH XÁC
                    uint borderColor = ImGui.GetColorU32(new Vector4(0.9f, 0.0f, 0.0f, 1.0f));
                    uint bgBlue = ImGui.GetColorU32(new Vector4(0.08f, 0.0f, 0.0f, 0.85f));

                    // 🟦 Vẽ nền bo góc và viền
                    drawList.AddRectFilled(windowPos, windowPos + windowSize, bgBlue, 8f);
                    drawList.AddRect(windowPos, windowPos + windowSize, borderColor, 8f, ImDrawFlags.None, 2.5f);

                    // ⚙️ Căn giữa tiêu đề
                    string title = "LH VN Status";
                    Vector2 titleSize = ImGui.CalcTextSize(title);
                    Vector2 titlePos = new Vector2(
                        windowPos.X + (windowSize.X / 2) - (titleSize.X / 2),
                        windowPos.Y + 7
                    );

                    // 🌊 Tiêu đề
                    drawList.AddText(titlePos, borderColor, title);

                    // ➖ Thanh ngang chia cách - ĐỔI TÊN BIẾN
                    float lineY = titlePos.Y + titleSize.Y + 6;
                    Vector2 statusDividerStart = new Vector2(windowPos.X + 10, lineY);
                    Vector2 statusDividerEnd = new Vector2(windowPos.X + windowSize.X - 10, lineY);
                    drawList.AddLine(statusDividerStart, statusDividerEnd, borderColor, 1.5f);

                    // Chỉ hiển thị các dòng status nếu có tính năng được bật
                    if (itemCount > 0)
                    {
                        // ⚙️ Các dòng trạng thái
                        Vector2 basePos = new Vector2(windowPos.X + 15, lineY + 10);

                        uint onColor = ImGui.GetColorU32(new Vector4(0.2f, 1f, 0.6f, 1f));
                        uint offColor = ImGui.GetColorU32(new Vector4(1f, 0.3f, 0.3f, 1f));

                        // Hiển thị từng tính năng được bật
                        for (int i = 0; i < itemCount; i++)
                        {
                            var feature = enabledFeatures[i];
                            bool isActive = feature.getState();

                            drawList.AddText(
                                basePos + new Vector2(0, lineHeight * i),
                                isActive ? onColor : offColor,
                                $"{feature.name} : {(isActive ? "ON" : "OFF")}"
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Bỏ qua lỗi để ESP vẫn chạy  
                    Console.WriteLine($"Status overlay error: {ex.Message}"


                );

                }
                if (Config.fixesp)
                {
                    UpdateEntities();
                    Core.Entities = new();
                    InternalMemory.Cache = new();
                }

                if (Config.ShowName2)
                {
                    var draw = ImGui.GetForegroundDrawList();

                    string Text = "Bot";
                    float scale = 1.2f;
                    var size = ImGui.CalcTextSize(text) * scale;

                    long now = zyreTimer.ElapsedMilliseconds;
                    long elapsed = now - lastUpdate;

                    if (elapsed >= 10)
                    {
                        float speed = (Config.Name2Speed * 0.1f) / 3.6f;

                        name2Pos.X += name2Dir.X * speed;
                        name2Pos.Y += name2Dir.Y * speed;

                        if (name2Pos.X <= 0 || name2Pos.X + size.X >= Core.Width)
                            name2Dir.X = -name2Dir.X;

                        if (name2Pos.Y <= 0 || name2Pos.Y + size.Y >= Core.Height)
                            name2Dir.Y = -name2Dir.Y;

                        lastUpdate = now;
                    }

                    uint color = ImGui.GetColorU32(new Vector4(1f, 0.3f, 0.9f, 1f));


                    draw.AddText(ImGui.GetFont(), ImGui.GetFontSize() * scale, name2Pos, color, text);
                }

            }


            // ============ ESP BOX ENEMY ============
            EnemyCount = tmp.Values.Count(entity => !entity.IsDead && entity.IsKnown);
            foreach (var entity in tmp.Values)
            {
                if (entity.IsDead || !entity.IsKnown) continue;
                var dist = Vector3.Distance(Core.LocalMainCamera, entity.Head);
                if (dist > 200) continue;

                var headScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Head, Core.Width, Core.Height);
                var bottomScreenPos = W2S.WorldToScreen(Core.CameraMatrix, entity.Root, Core.Width, Core.Height);
                if (headScreenPos.X < 1 || headScreenPos.Y < 1) continue;
                if (bottomScreenPos.X < 1 || bottomScreenPos.Y < 1) continue;

                float CornerHeight = Math.Abs(headScreenPos.Y - bottomScreenPos.Y);
                float CornerWidth = CornerHeight * 0.65f;
                // =====================
                // Vẽ ESP Box Enemy
                // =====================


                // if (!Core.HaveMatrix) return;

                CreateHandle();
























                if (Config.ESPLine2)
                {
                    // Check if the entity is "Knocked"
                    uint lineColor;

                    if (entity.IsKnocked)
                    {
                        lineColor = ColorToUint32(Color.Red); // Red for "Knocked" state
                    }
                    else
                    {
                        lineColor = ColorToUint32(Config.ESPLineColor); // Normal color
                    }

                    // Draw the line with the appropriate color
                    ImGui.GetBackgroundDrawList().AddLine(
                        new Vector2(Core.Width / 2f, 0f),
                        headScreenPos,
                        lineColor,
                        1f
                    );

                }


                if (Config.ESPLine)
                {
                    Vector2 lineStart = new Vector2(Core.Width / 2f, 25f);

                    // CODE NÀY VẪN CHẠY TỐT - KHÔNG CẦN SỬA
                    DrawRainbowLine(lineStart, headScreenPos,
                                    ColorToUint32(Config.ESPLineColor),
                                    Config1.ESPLineThickness,    // Thickness - có thể chỉnh được
                                    Config1.ESPLineGlowRadius,   // Glow radius - có thể chỉnh được  
                                    0.5f,    // Feather
                                    0.4f,    // Opacity
                                    Config1.ESPLineMode);        // Chế độ line - có thể chọn được
                }
                if (Config.ESPWeapon && !string.IsNullOrEmpty(entity.WeaponName))
                {
                    Vector2 fixedNameSize1 = new Vector2(95, 16);
                    if (headScreenPos.X >= 0 && headScreenPos.Y >= 0 &&
                        headScreenPos.X <= Core.Width && headScreenPos.Y <= Core.Height)
                    {
                        Vector2 namePos = new Vector2(
                            headScreenPos.X - fixedNameSize1.X / 2,
                            headScreenPos.Y - fixedNameSize1.Y - 13);

                        try
                        {
                            var weaponFileName = entity.WeaponName.ToLower();
                            var imagePath = $"C:\\weaponff\\Icons\\{weaponFileName}.png";
                            if (File.Exists(imagePath))
                            {
                                IntPtr imageHandle;
                                AddOrGetImagePointer(imagePath, true, out imageHandle, out var width, out var height);

                                if (imageHandle != IntPtr.Zero)
                                {
                                    Vector2 iconSize = new Vector2(60, 20);
                                    Vector2 iconPos = new Vector2(namePos.X + (fixedNameSize1.X - iconSize.X) / 2, namePos.Y - iconSize.Y - 2);
                                    ImGui.GetForegroundDrawList().AddImage(imageHandle, iconPos, iconPos + iconSize);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ESPWeapon] Image load error: {ex.Message}");
                        }
                    }
                }

                if (Config.ESPLineDuoi)
                {
                    // Check if the entity is "Knocked"
                    uint lineColor;


                    lineColor = ColorToUint32(Config.ESPLineDuoiColor); // Normal color


                    // Draw the line with the appropriate color
                    ImGui.GetBackgroundDrawList().AddLine(
                      new Vector2(Core.Width / 2f, Core.Height / 2f),  // Từ giữa màn hình (Center ESP)
                        headScreenPos,
                        lineColor,
                        1f
                    );

                }
                // Đọc thông tin vũ khí của entity từ bộ nhớ

                //if (Config.ShowFOV)
                //{
                //    // Lấy size màn hình REAL TIME
                //    Core.Width = (int)ImGui.GetIO().DisplaySize.X;
                //    Core.Height = (int)ImGui.GetIO().DisplaySize.Y;

                //    var draw = ImGui.GetForegroundDrawList();

                //    // Tâm luôn chính xác kể cả khi đổi chế độ window/fullscreen
                //    Vector2 center = new Vector2(Core.Width * 0.5f, Core.Height * 0.5f);

                //    uint color = ImGui.ColorConvertFloat4ToU32(Config.FOVColorFloat);

                //    float thickness = 1.2f;
                //    int segments = 72;

                //    draw.Flags |= ImDrawListFlags.AntiAliasedLines;

                //    // VẼ FOV đúng kích thước KHÔNG bị scale theo màn
                //    draw.AddCircle(center, Config.AimbotFOV, color, segments, thickness);
                //}




                if (Config.AimBot)
                {
                    //   var drawList = ImGui.GetBackgroundDrawList();
                    int numSegments = 1000; // Số lượng đoạn, càng cao thì hình tròn càng mịn
                    drawList.AddCircle(new Vector2(Core.Width / 2, Core.Height / 2), Config.AimbotFOV, ColorToUint32(Config.FovColor), numSegments, 1.4f);
                }

                if (Config.minimap)
                {
                    DrawMinimap();
                }


                if (Config.ESPInfo)
                {
                    ImGui.PushFont(FontManager.SmallFont);
                    if (headScreenPos.X >= 0 && headScreenPos.Y >= 0 && headScreenPos.X <= Core.Width && headScreenPos.Y <= Core.Height)
                    {
                        var vList = ImGui.GetForegroundDrawList();
                        var nameText = string.IsNullOrWhiteSpace(entity.Name) ? "bot" : entity.Name;

                        float totalWidth = 100f;
                        float healthBoxWidth = 26f;
                        float nameBoxWidth = totalWidth - healthBoxWidth;
                        Vector2 infoBoxSize = new Vector2(totalWidth, 14.5f);
                        float boxRounding = 2f;


                        Vector2 topLeftPos = new Vector2(headScreenPos.X - totalWidth / 2f, headScreenPos.Y - 35);

                        Vector2 healthBoxPos = topLeftPos;
                        Vector2 nameBoxPos = new Vector2(healthBoxPos.X + healthBoxWidth, healthBoxPos.Y);

                        vList.AddRectFilled(
                            healthBoxPos,
                            healthBoxPos + new Vector2(healthBoxWidth, infoBoxSize.Y),
                            ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 1)),
                            rounding,
                            ImDrawFlags.RoundCornersTopLeft
                        );

                        vList.AddRectFilled(
                            nameBoxPos,
                            nameBoxPos + new Vector2(nameBoxWidth, infoBoxSize.Y),
                            ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.75f)),
                            rounding,
                            ImDrawFlags.RoundCornersTopRight
                        );

                        int displayHealth = Math.Min((int)entity.Health, 200);
                        string healthStr = displayHealth.ToString();
                        Vector2 healthTextSize = ImGui.CalcTextSize(healthStr);
                        Vector2 healthTextPos = new Vector2(
                            healthBoxPos.X + (healthBoxWidth - healthTextSize.X) / 2,
                            healthBoxPos.Y + (infoBoxSize.Y - healthTextSize.Y) / 2
                        );
                        vList.AddText(healthTextPos, ImGui.ColorConvertFloat4ToU32(new Vector4(0f, 0f, 0f, 1f)), healthStr);


                        Vector2 nameTextSize = ImGui.CalcTextSize(nameText);
                        Vector2 nameTextPos = new Vector2(
                            nameBoxPos.X + 3,
                            nameBoxPos.Y + (infoBoxSize.Y - nameTextSize.Y) / 2
                        );

                        vList.AddText(nameTextPos, ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 0f, 1f)), nameText);


                        float clampedDist = Math.Clamp(dist, 0f, 9999f);
                        string distanceText = $"{MathF.Round(clampedDist)}m";
                        Vector2 distanceTextSize = ImGui.CalcTextSize(distanceText);
                        Vector2 distanceTextPos = new Vector2(
                            nameBoxPos.X + nameBoxWidth - distanceTextSize.X - 4,
                            nameBoxPos.Y + (infoBoxSize.Y - distanceTextSize.Y) / 2
                        );


                        vList.AddText(distanceTextPos, ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 1f)), distanceText);


                        float maxHealth = 200f;
                        float healthPercent = (float)displayHealth / maxHealth;
                        Vector2 barPos = new Vector2(topLeftPos.X, topLeftPos.Y + infoBoxSize.Y);
                        Vector2 barSize = new Vector2(totalWidth, 2);

                        Vector4 healthBarColor;
                        var colorGreen = new Vector4(0, 1, 0, 1);
                        var colorYellow = new Vector4(1, 1, 0, 1);
                        var colorRed = new Vector4(1, 0, 0, 1);

                        if (healthPercent > 0.7f)
                        {
                            float t = (healthPercent - 0.7f) / 0.3f;
                            healthBarColor = Vector4.Lerp(colorYellow, colorGreen, t);
                        }
                        else if (healthPercent > 0.4f)
                        {
                            float t = (healthPercent - 0.4f) / 0.3f;
                            healthBarColor = Vector4.Lerp(colorRed, colorYellow, t);
                        }
                        else
                        {
                            healthBarColor = colorRed;
                        }
                        vList.AddRectFilled(barPos, barPos + barSize, ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.8f)), 2f);
                        if (healthPercent > 0)
                        {
                            vList.AddRectFilled(
                                barPos,
                                new Vector2(barPos.X + barSize.X * healthPercent, barPos.Y + barSize.Y),
                                ImGui.ColorConvertFloat4ToU32(healthBarColor),
                                2f
                            );
                        }
                        float triangleWidth = 6f;
                        float triangleHeight = 4f;

                        Vector2 triangleCenter = new Vector2(topLeftPos.X + totalWidth / 2f, topLeftPos.Y + infoBoxSize.Y + barSize.Y);

                        Vector2 p1 = new Vector2(triangleCenter.X, triangleCenter.Y + triangleHeight);
                        Vector2 p2 = new Vector2(triangleCenter.X - triangleWidth / 2f, triangleCenter.Y);
                        Vector2 p3 = new Vector2(triangleCenter.X + triangleWidth / 2f, triangleCenter.Y);

                        vList.AddTriangleFilled(p1, p2, p3, ImGui.ColorConvertFloat4ToU32(new Vector4(1, 1, 1, 1)));
                    }
                    ImGui.PopFont();
                }






            if (Config.fixespKey1 != Keys.None)
                {
                    if (KeyHelper.IsKeyDown(Config.fixespKey1))
                    {
                        if (!Config.FixEspKeyWasPressed)
                        {
                            // Chạy Fix ESP 1 lần duy nhất
                            UpdateEntities();
                            Core.Entities = new();
                            InternalMemory.Cache = new();

                            Config.FixEspKeyWasPressed = true;   // Đánh dấu đã chạy
                        }
                    }
                    else
                    {
                        // Khi thả phím → cho phép nhấn lại
                        Config.FixEspKeyWasPressed = false;
                    }
                }
                if (Config.ESPDistance)
                {
                    string distanceText = $"{MathF.Round(dist)}m";

                    // Lấy kích thước chữ thật từ ImGui
                    Vector2 TextSize = ImGui.CalcTextSize(distanceText);

                    // Padding cho đẹp
                    float PaddingX = 4f;
                    float PaddingY = 2f;

                    // Vị trí khi căn giữa
                    Vector2 distancePosition = new Vector2(
                        bottomScreenPos.X - (textSize.X / 2),
                        bottomScreenPos.Y + 15f
                    );

                    // Nền mờ bo góc
                    ImGui.GetForegroundDrawList().AddRectFilled(
                        new Vector2(distancePosition.X - paddingX, distancePosition.Y - paddingY),
                        new Vector2(distancePosition.X + textSize.X + paddingX, distancePosition.Y + textSize.Y + paddingY),
                        ImGui.ColorConvertFloat4ToU32(new Vector4(0f, 0f, 0f, 0.6f)), // Màu nền
                        3f // Bo góc
                    );

                    // Vẽ chữ
                    ImGui.GetForegroundDrawList().AddText(
                        distancePosition,
                        ColorToUint32(Config.ESPLineColor),
                        distanceText
                    );
                }
                Vector2 fixedNameSize = new Vector2(95, 16);
                float healthBarHeight = 4;

                if (entity.Name == "")
                    entity.Name = "Bot";
                if (headScreenPos.X >= 0 && headScreenPos.Y >= 0 && headScreenPos.X <= Core.Width && headScreenPos.Y <= Core.Height)
                {
                    Vector2 namePos = new Vector2(headScreenPos.X - fixedNameSize.X / 2, headScreenPos.Y - fixedNameSize.Y - 15);



                    Vector2 textSizeName = ImGui.CalcTextSize(entity.Name);

                    Vector2 textSizeDistance = ImGui.CalcTextSize($" ({MathF.Round(Vector3.Distance(Core.LocalMainCamera, entity.Head))}m)");

                    Vector2 textPosName = new Vector2(namePos.X + 5, namePos.Y + (fixedNameSize.Y - textSizeName.Y) / 2);
                    Vector2 textPosDistance = new Vector2(namePos.X + fixedNameSize.X - textSizeDistance.X + 5, namePos.Y + (fixedNameSize.Y - textSizeDistance.Y) / 2);

                    Vector2 Size = new Vector2(80f, 18f);

                    // ----- TÍNH VỊ TRÍ TEXT -----
                    Vector2 TextPosName = new Vector2(
                        namePos.X + 5,
                        namePos.Y + (Size.Y - textSizeName.Y) / 2
                    );

                    Vector2 TextPosDistance = new Vector2(
                        namePos.X + Size.X - textSizeDistance.X + 5,
                        namePos.Y + (Size.Y - textSizeDistance.Y) / 2
                    );
                    // Tính distance + scale (xa nhỏ, gần to)
                    float Dist = Vector3.Distance(Core.LocalMainCamera, entity.Head);
                    float scale = Math.Clamp(250f / dist, 0.7f, 1.4f);

                    // Kích thước box name
                    Vector2 size = new Vector2(80f * scale, 18f * scale);

                    // Tính vị trí text
                    Vector2 PosName = new Vector2(
                        namePos.X + 5f * scale,
                        namePos.Y + (Size.Y - textSizeName.Y) / 2
                    );

                    Vector2 PosDistance = new Vector2(
                        namePos.X + Size.X - textSizeDistance.X + 5f * scale,
                        namePos.Y + (Size.Y - textSizeDistance.Y) / 2
                    );


                    // ===================== ESP NAME + DISTANCE =====================
                    if (Config.ESPName)
                    {
                        var draw = ImGui.GetForegroundDrawList();

                        // Box nền
                        draw.AddRectFilled(
                            namePos,
                            namePos + Size,
                            ImGui.ColorConvertFloat4ToU32(new Vector4(0, 0, 0, 0.7f)),
                            3f * scale
                        );

                        // Tên
                        draw.AddText(
                            textPosName,
                            ColorToUint32(Config.ESPNameColor),
                            entity.Name
                        );

                        // Distance
                        draw.AddText(
                            textPosDistance,
                            ColorToUint32(Config.ESPNameColor),
                            $"{MathF.Round(dist)}m"
                        );
                    }


                    // ===================== ESP HEALTH BAR =====================
                    if (Config.ESPHealth1 && Config.ESPName)
                    {
                        var draw = ImGui.GetForegroundDrawList();

                        float healthPercentage =
                            entity.Health > 1000 ? 1f :
                            entity.Health < 0 ? 1f :
                            (float)entity.Health / (entity.Health > 230 ? 500 : 200);

                        float barWidth = Size.X * healthPercentage;
                        Vector2 barPos = new Vector2(namePos.X, namePos.Y + Size.Y);

                        uint barColor =
                            healthPercentage > 0.8f ? ColorToUint32(Color.GreenYellow) :
                            healthPercentage > 0.4f ? ColorToUint32(Color.Orange) :
                            ColorToUint32(Color.Red);

                        if (entity.IsKnocked)
                            barColor = ColorToUint32(Color.Red);

                        // Nền bar
                        draw.AddRectFilled(
                            new Vector2(barPos.X, barPos.Y),
                            new Vector2(barPos.X + Size.X, barPos.Y + 2f * scale),
                            0x90000000
                        );

                        // Thanh máu
                        draw.AddRectFilled(
                            new Vector2(barPos.X, barPos.Y),
                            new Vector2(barPos.X + barWidth, barPos.Y + 2f * scale),
                            barColor
                        );
                    }


                    if (Config.FixEsp)
                    {
                        UpdateEntities();
                        Core.Entities = new();
                        InternalMemory.Cache = new();
                    }

                    if (Config.ESPFillBox)
                    {
                        float alpha = 1.2f;
                        uint fillboxColor = ColorToUint32(Color.White);
                        DrawFullBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, fillboxColor, alpha);
                        DrawCorneredBox2(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, fillboxColor, 1f);
                    }

                    if (Config.ESPBox)
                    {
                        uint boxColor = ColorToUint32(Config.ESPBoxColor);
                        DrawCorneredBox(headScreenPos.X - (CornerWidth / 2), headScreenPos.Y, CornerWidth, CornerHeight, boxColor, 1f);
                    }
                    if (Config.ESPSkeleton)
                    {
                        skeleton(entity);
                    }

                    if (Config.ESPHealth)
                    {
                        // ======= Lấy máu hiện tại =======
                        float rawHealth = Math.Max(0f, (float)entity.Health);

                        // ======= Xác định máu tối đa tự động =======
                        float maxHealth = (rawHealth > 400f) ? 500f : 200f;

                        // ======= Tính phần trăm máu =======
                        float healthPercentage = Math.Clamp(rawHealth / maxHealth, 0f, 1f);

                        // ======= Màu thanh máu =======
                        uint barColor;
                        if (healthPercentage > 0.8f)
                            barColor = ColorToUint32(Color.Lime);
                        else if (healthPercentage > 0.4f)
                            barColor = ColorToUint32(Color.Yellow);
                        else
                            barColor = ColorToUint32(Color.Red);

                        // ======= Kích thước box =======
                        float boxHeight = Math.Abs(bottomScreenPos.Y - headScreenPos.Y);
                        float boxWidth = boxHeight * 0.5f;
                        float boxX = headScreenPos.X - (boxWidth / 2);
                        float boxY = headScreenPos.Y;

                        // ======= Lấy vị trí từ config =======
                        var headPosition = Config.ESPHeadPosition;

                        // ======= Tính toán vị trí và kích thước =======
                        float barWidth = 4f;
                        float barHeight = boxHeight;
                        float posX = boxX - 6f;
                        float posY = boxY;
                        bool isHorizontal = false;

                        switch (headPosition)
                        {
                            case HealthPosition.Left:
                                barWidth = 4f;
                                barHeight = boxHeight;
                                posX = boxX - 6f;
                                posY = boxY;
                                isHorizontal = false;
                                break;

                            case HealthPosition.Right:
                                barWidth = 4f;
                                barHeight = boxHeight;
                                posX = boxX + boxWidth + 2f;
                                posY = boxY;
                                isHorizontal = false;
                                break;

                            case HealthPosition.Top:
                                barWidth = boxWidth * 0.8f;
                                barHeight = 4f;
                                posX = boxX + boxWidth * 0.1f;
                                posY = boxY - 6f;
                                isHorizontal = true;
                                break;

                            case HealthPosition.Bottom:
                                barWidth = boxWidth * 0.8f;
                                barHeight = 4f;
                                posX = boxX + boxWidth * 0.1f;
                                posY = boxY + boxHeight + 2f;
                                isHorizontal = true;
                                break;
                        }

                        // ======= Tính phần máu =======
                        float filledSize = isHorizontal ?
                            (barWidth * healthPercentage) :
                            (barHeight * healthPercentage);

                        float radius = isHorizontal ? (barHeight / 2f) : (barWidth / 2f);
                        radius = Math.Clamp(radius, 1f, 3f); // radius đẹp

                        // ======= VẼ NỀN (bo góc mượt) =======
                        drawList.AddRectFilled(
                            new Vector2(posX, posY),
                            new Vector2(posX + barWidth, posY + barHeight),
                            0x90000000,
                            radius
                        );

                        // ======= VẼ THANH MÁU BO GÓC =======
                        if (filledSize > 0.1f)
                        {
                            if (isHorizontal)
                            {
                                // Ngang (Top/Bottom)
                                drawList.AddRectFilled(
                                    new Vector2(posX, posY),
                                    new Vector2(posX + filledSize, posY + barHeight),
                                    barColor,
                                    radius,
                                    ImDrawFlags.RoundCornersLeft
                                );
                            }
                            else
                            {
                                // Dọc (Left/Right) – vẽ từ dưới lên
                                float startY = posY + barHeight - filledSize;

                                drawList.AddRectFilled(
                                    new Vector2(posX, startY),
                                    new Vector2(posX + barWidth, posY + barHeight),
                                    barColor,
                                    radius,
                                    ImDrawFlags.RoundCornersBottom
                                );
                            }
                        }
                    }


                    if (Config.AimbotVisible)
                    {
                        //   var drawList = ImGui.GetBackgroundDrawList();
                        int numSegments = 1000; // Số lượng đoạn, càng cao thì hình tròn càng mịn
                        drawList.AddCircle(new Vector2(Core.Width / 2, Core.Height / 2), Config.AimbotFOV, ColorToUint32(Config.FovColor), numSegments, 1.4f);
                    }

                    DisplayEnemyCount(Core.Width, Core.Height);
                    DrawShurikenCrosshair();
                }






                const uint WDA_MONITOR = 0x00000001;
                const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

                bool IsValidTarget(Entity entity)
                {
                    // Kiểm tra điều kiện hợp lệ của target, ví dụ:
                    return entity != null && !entity.IsDead && entity.IsEnemy;
                }


                void DrawSmallTextWithOutline(Vector2 pos, string text, uint textColor, uint outlineColor)
                {
                    var vList = ImGui.GetForegroundDrawList();
                    float outlineThickness = 1.2f;  // Smaller outline for smoothness
                    float boldOffset = 0.4f;        // Adjusted for smaller text
                    float spacing = 2.0f;           // Adds space between characters

                    Vector2 adjustedPos = pos;

                    foreach (char c in text)
                    {
                        string character = c.ToString(); // Convert char to string

                        // Smooth outline
                        for (float x = -outlineThickness; x <= outlineThickness; x += 1.0f)
                        {
                            for (float y = -outlineThickness; y <= outlineThickness; y += 1.0f)
                            {
                                if (x == 0 && y == 0) continue;
                                vList.AddText(new Vector2(adjustedPos.X + x, adjustedPos.Y + y), outlineColor, character);
                            }
                        }

                        // Bold effect
                        vList.AddText(new Vector2(adjustedPos.X - boldOffset, adjustedPos.Y), textColor, character);
                        vList.AddText(new Vector2(adjustedPos.X + boldOffset, adjustedPos.Y), textColor, character);
                        vList.AddText(new Vector2(adjustedPos.X, adjustedPos.Y - boldOffset), textColor, character);
                        vList.AddText(new Vector2(adjustedPos.X, adjustedPos.Y + boldOffset), textColor, character);

                        // Main text layer
                        vList.AddText(adjustedPos, textColor, character);

                        // Move position for next character with spacing
                        adjustedPos.X += ImGui.CalcTextSize(character).X + spacing;
                    }
                }
            }
        }


        // Event khi thay đổi giá trị trackbar, cập nhật label hiển thị

        // Hàm fixesp - làm mới dữ liệu ESP (ví dụ)
        // Biến CancellationTokenSource toàn cục
        private CancellationTokenSource espResetToken = new CancellationTokenSource();
        private Task espResetTask = null;
        private int espResetDelay = 1000; // mặc định 5s

        private void UpdateEntities()
        {

            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsTeam != Bool3.False) continue;

                TreeNode entityNode = new TreeNode(entity.Name);

                entityNode.Nodes.Add(new TreeNode($"IsKnown: {entity.IsKnown}"));
                entityNode.Nodes.Add(new TreeNode($"IsTeam: {entity.IsTeam}"));
                entityNode.Nodes.Add(new TreeNode($"Head: {entity.Head}"));
                entityNode.Nodes.Add(new TreeNode($"Root: {entity.Root}"));
                entityNode.Nodes.Add(new TreeNode($"Health: {entity.Health}"));
                entityNode.Nodes.Add(new TreeNode($"IsDead: {entity.IsDead}"));
                entityNode.Nodes.Add(new TreeNode($"IsKnocked: {entity.IsKnocked}"));


            }
            Thread.Sleep(1000);
        }
        private void NoCache()
        {

            InternalMemory.Cache = new();
            Core.Entities = new();
            Thread.Sleep(1000);
        }
        // Hàm start auto reset ESP, có delay truyền vào
        private void StartEspAutoReset()
        {
            // Hủy task cũ nếu có
            espResetToken.Cancel();
            espResetToken = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!espResetToken.Token.IsCancellationRequested)
                {
                    try
                    {
                        fixesp();       // Làm mới dữ liệu ESP
                        updateEsp(); // Xử lý, log hoặc render ESP
                        NoCache();
                        UpdateEntities();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ESP error: " + ex.Message);
                    }

                    await Task.Delay(1000); // Delay động từ tham số
                }
            }, espResetToken.Token);
        }


        private void fixesp()
        {
            Core.LocalPlayer = new();
            Core.Entities = new();
            InternalMemory.Cache = new();
            Core.Entities = new();
        }
        private void updateEsp() //
        {
            foreach (var entity in Core.Entities.Values)
            {
                if (entity.IsTeam != Bool3.False) continue;
                TreeNode entityNode = new TreeNode(entity.Name);
                entityNode.Nodes.Add(new TreeNode($"IsKnown: {entity.IsKnown}"));
                entityNode.Nodes.Add(new TreeNode($"IsTeam: {entity.IsTeam}"));
                entityNode.Nodes.Add(new TreeNode($"Head: {entity.Head}"));
                entityNode.Nodes.Add(new TreeNode($"Root: {entity.Root}"));
                entityNode.Nodes.Add(new TreeNode($"Health: {entity.Health}"));
                entityNode.Nodes.Add(new TreeNode($"IsDead: {entity.IsDead}"));
                entityNode.Nodes.Add(new TreeNode($"IsKnocked: {entity.IsKnocked}"));
            }
        }
        public void DrawGradientBox(float X, float Y, float W, float H, Color topColor, Color bottomColor)
        {
            var vList = ImGui.GetForegroundDrawList();

            int slices = 50; // Number of slices for gradient
            float sliceHeight = H / slices;

            for (int i = 0; i < slices; i++)
            {
                float t = (float)i / slices; // Interpolation factor
                Color sliceColor = Color.FromArgb(
                    (int)(topColor.A * (1 - t) + bottomColor.A * t), // Interpolating opacity
                    (int)(topColor.R * (1 - t) + bottomColor.R * t), // Interpolating Red
                    (int)(topColor.G * (1 - t) + bottomColor.G * t), // Interpolating Green
                    (int)(topColor.B * (1 - t) + bottomColor.B * t)  // Interpolating Blue
                );

                uint sliceColorUint = ColorToUint32(sliceColor);

                // Draw each slice
                vList.AddRectFilled(
                    new Vector2(X, Y + i * sliceHeight),
                    new Vector2(X + W, Y + (i + 1) * sliceHeight),
                    sliceColorUint
                );
            }
        }
        private bool showMenu = true;

        string user = "";
        string pass = "";
        bool ShowPanelZ = true;
        private static int loginCount = 0;
        //private static string username = "";
        //private static string password = "";
        //private static bool rememberMe = false;
        //private static bool Logins = false;


        bool ShowPanelz()
        {
            return ShowPanelZ == true;
        }

        bool Logins = false;
        private int selectedTab = 0;



        private bool wasInsertPressed = false; // Theo dõi trạng thái Insert


        bool scopeEnabled = false;
        bool scopeBusy = false;
        bool scopeReady = false;
        List<long> scopeAddresses = new List<long>();
        string statusText = "";
        // Spd
        string[] processName = { "HD-Player" };
        string hex = "00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";           // AoB cần quét
        string replace = "00 00 00 00 00 80 40 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";       // thay bằng bytes
        string restore = "00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 80 BF 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 00 00 00 00 00 00 00 00 80 BF 00 00 80 7F 00 00 80 7F 00 00 80 7F 00 00 80 FF";       // khôi phục lại
        bool showWindow = true;

        private string licenseKey = "";
        private bool isLoggedIn = false;
        private string loginMessage = "";
        private bool autoResetEsp = false;




        // Thêm biến toàn cục để chống spam nút
        private DateTime lastToggleTime = DateTime.MinValue;

        private async void RenderImgui()
        {
            // Xử lý phím Insert không bị chớp
            if (IsKeyPressed(Keys.Insert) && DateTime.Now - lastToggleTime > TimeSpan.FromMilliseconds(200))
            {
                showMenu = !showMenu;
                lastToggleTime = DateTime.Now;
            }




            // KHÔNG cần gọi RenderImgui() ở đây nữa



            // ❌ Không return nếu showMenu = false (ESP vẫn cần vẽ!)
            if (!showMenu) return;








            ApplyStyle(); // Gọi style dùng chung cho cả login và main
            ReplaceFont(fontpath, 15, FontGlyphRangeType.Vietnamese); // To hơn một chút

            ImGui.PushFont(FontManager.BigFont);

            if (!isLoggedIn)
            {
                ApplyStyle();
                ReplaceFont(fontpath, 15, FontGlyphRangeType.Vietnamese);
                ImGui.PushFont(FontManager.BigFont);

                Vector2 loginSize = new Vector2(360, 220); // Tăng chiều cao lên chút
                ImGui.SetNextWindowSize(loginSize, ImGuiCond.Always);
                ImGui.Begin("LH VN | Login", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);

                // --- Tiêu đề ---
                ImGui.SetCursorPosX((loginSize.X - ImGui.CalcTextSize("LH VN Menu").X) / 2);
                ImGui.TextColored(new Vector4(0.2f, 0.8f, 1.0f, 1.0f), "LH VN Menu");
                ImGui.Separator();

                // --- Thông tin license hiện tại ---
                if (currentAuth != null && !string.IsNullOrEmpty(currentLicenseKey))
                {
                    ImGui.TextColored(new Vector4(1.0f, 0.5f, 0.0f, 1.0f), $"Key: {currentLicenseKey.Substring(0, Math.Min(12, currentLicenseKey.Length))}...");

                    if (currentAuth.success)
                    {
                        ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Trạng thái: Đã kích hoạt");
                        ImGui.TextColored(new Vector4(0.7f, 0.7f, 1.0f, 1.0f), $"Gói: {currentAuth.plan}");

                        if (currentAuth.lifetime)
                        {
                            ImGui.TextColored(new Vector4(0.0f, 1.0f, 1.0f, 1.0f), "Loại: Vĩnh viễn");
                        }
                        else if (currentAuth.expiration_date.HasValue)
                        {
                            TimeSpan timeLeft = currentAuth.expiration_date.Value - DateTime.Now;
                            int daysLeft = (int)Math.Ceiling(timeLeft.TotalDays);

                            if (daysLeft > 0)
                            {
                                ImGui.TextColored(new Vector4(0.0f, 1.0f, 0.0f, 1.0f), $"Còn lại: {daysLeft} ngày");
                            }
                            else
                            {
                                ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), "ĐÃ HẾT HẠN!");
                            }
                        }
                    }
                    else
                    {
                        ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), $"Lỗi: {currentAuth.message}");
                    }
                    ImGui.Separator();
                }

                ImGui.Text("Nhap Key:");
                float inputWidth = ImGui.GetWindowSize().X - 40;
                ImGui.SetCursorPosX(20);
                ImGui.PushItemWidth(inputWidth);

                // Style cho textbox
                ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 6f);
                ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12, 8));

                // Textbox nhập key
                if (ImGui.InputText("##license", ref licenseKey, 100,
                    ImGuiInputTextFlags.AutoSelectAll | ImGuiInputTextFlags.CharsNoBlank))
                {
                    // Tự động chuyển sang uppercase và loại bỏ khoảng trắng
                    licenseKey = licenseKey.ToUpper().Replace(" ", "").Replace("-", "");
                }

                ImGui.PopStyleVar(2);
                ImGui.PopItemWidth();

                ImGui.Spacing();

                // --- Nút Login ---
                ImGui.SetCursorPosX(20);
                bool canLogin = !string.IsNullOrWhiteSpace(licenseKey) && licenseKey.Length >= 8;

                if (!canLogin)
                {
                    ImGui.BeginDisabled();
                }

                if (ImGui.Button("LOGIN", new Vector2(inputWidth, 40)))
                {
                    if (!string.IsNullOrWhiteSpace(licenseKey))
                    {
                        // Xóa key cũ trong bộ nhớ
                        currentLicenseKey = "";
                        currentAuth = null;

                        // Hiển thị loading
                        licenseStatus = "Dang xac thuc...";

                        // Chạy async để không block UI
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                // Gọi API auth mới
                                var result = await AuthHandler.ValidateKeyAsync(licenseKey);

                                // Lưu kết quả
                                currentAuth = result;
                                currentLicenseKey = licenseKey;

                                if (result.success)
                                {
                                    isLoggedIn = true;
                                    licenseStatus = "Xac thuc thanh cong!";

                                    // 👉 KHỞI ĐỘNG HOTKEY THREAD KHI LOGIN THÀNH CÔNG
                                    StartHotkeyThread();

                                    // 👉 KHỞI TẠO CÁC THỨ KHÁC NẾU CẦN
                                    Console.Beep(800, 200);
                                    Console.Beep(1000, 200);
                                }
                                else
                                {
                                    licenseStatus = $"Loi: {result.message}";
                                    isLoggedIn = false;

                                    Console.Beep(400, 500);
                                }
                            }
                            catch (Exception ex)
                            {
                                licenseStatus = $"Loi ket noi: {ex.Message}";
                                isLoggedIn = false;

                                Console.Beep(300, 500);
                            }
                        });
                    }
                }

                if (!canLogin)
                {
                    ImGui.EndDisabled();

                    // Tooltip khi disabled
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.SetTooltip("Key phai co it nhat 8 ky tu");
                    }
                }

                ImGui.Spacing();

                // --- Hiển thị trạng thái ---
                if (!string.IsNullOrEmpty(licenseStatus))
                {
                    ImGui.SetCursorPosX(20);

                    Vector4 statusColor = licenseStatus.Contains("thanh cong") ?
                        new Vector4(0.0f, 1.0f, 0.0f, 1.0f) :
                        licenseStatus.Contains("Lỗi") ?
                        new Vector4(1.0f, 0.3f, 0.3f, 1.0f) :
                        new Vector4(1.0f, 1.0f, 0.0f, 1.0f);

                    ImGui.TextColored(statusColor, licenseStatus);
                }

                // --- Link mua key ---
                ImGui.Spacing();
                ImGui.Separator();

                string buyText = "hi chat";
                float buyWidth = ImGui.CalcTextSize(buyText).X;
                ImGui.SetCursorPosX((ImGui.GetWindowSize().X - buyWidth) / 2f);

                if (ImGui.Selectable(buyText, false, ImGuiSelectableFlags.None, new Vector2(buyWidth, 0)))
                {
                    // Mở link Discord khi click
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://dsc.gg/legithax", // Thay link Discord thật
                            UseShellExecute = true
                        });
                    }
                    catch { }
                }

                ImGui.End();
                ImGui.PopFont();
                return;
            }






            ImGuiStylePtr style = ImGui.GetStyle();
            style.WindowBorderSize = 0f;
            style.WindowRounding = 4f;
            style.FrameBorderSize = 0f;
            style.FrameRounding = 6f;
            style.GrabRounding = 4f;
            style.PopupRounding = 6f;
            style.TabRounding = 6f;
            style.ItemSpacing = new Vector2(10, 8);
            style.WindowPadding = new Vector2(12, 10);
            style.FramePadding = new Vector2(8, 6);
            style.ScrollbarSize = 13f;
            style.ScrollbarRounding = 6f;
            Vector4 colorText = new Vector4(0.95f, 0.96f, 0.98f, 1.0f);
            Vector4 colorBgDark = new Vector4(0.10f, 0.10f, 0.11f, 1.0f);
            Vector4 colorFrame = new Vector4(0.16f, 0.16f, 0.18f, 1.0f);
            Vector4 colorHover = new Vector4(0.20f, 0.20f, 0.22f, 1.0f);
            Vector4 colorActiveBg = new Vector4(0.24f, 0.24f, 0.26f, 1.0f);
            Vector4 colorHighlight = new Vector4(0.95f, 0.95f, 0.95f, 1.0f);
            Vector4 tabBg = new Vector4(0.13f, 0.13f, 0.13f, 1.0f);
            Vector4 tabActive = new Vector4(0.10f, 0.10f, 0.10f, 1.0f);
            Vector4 tabInactive = new Vector4(0.16f, 0.16f, 0.16f, 1.0f);
            Vector4 tabHover = new Vector4(0.20f, 0.20f, 0.20f, 1.0f);
            Vector4 colorTabActiveTarget = colorHighlight;
            Vector4 colorTabInactiveTarget = new Vector4(0.93f, 0.93f, 0.93f, 1.0f);
            Vector4 colorTabHover = new Vector4(0.85f, 0.85f, 0.85f, 1.0f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.62f, 1.0f);
            style.Colors[(int)ImGuiCol.WindowBg] = colorBgDark;
            style.Colors[(int)ImGuiCol.ChildBg] = colorBgDark;
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.12f, 0.12f, 0.13f, 1.0f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.25f, 0.25f, 0.27f, 0.50f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.13f, 0.13f, 0.13f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.13f, 0.13f, 0.13f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = colorBgDark;
            style.Colors[(int)ImGuiCol.FrameBg] = colorFrame;
            style.Colors[(int)ImGuiCol.FrameBgHovered] = colorHover;
            style.Colors[(int)ImGuiCol.FrameBgActive] = colorActiveBg;
            style.Colors[(int)ImGuiCol.Button] = colorFrame;
            style.Colors[(int)ImGuiCol.ButtonHovered] = colorHover;
            style.Colors[(int)ImGuiCol.ButtonActive] = colorActiveBg;
            style.Colors[(int)ImGuiCol.CheckMark] = colorHighlight; // TRẮNG SÁNG (0.95f)
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.65f, 0.65f, 0.68f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = colorHighlight; // TRẮNG SÁNG (0.95f)
            style.Colors[(int)ImGuiCol.TabActive] = colorTabActiveTarget;
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = colorTabActiveTarget;
            style.Colors[(int)ImGuiCol.Tab] = colorTabInactiveTarget;
            style.Colors[(int)ImGuiCol.TabUnfocused] = colorTabInactiveTarget;
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.13f, 0.13f, 0.13f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.30f, 0.30f, 0.32f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.40f, 0.40f, 0.42f, 1.0f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.20f, 0.20f, 0.22f, 1.0f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = colorHover;
            style.Colors[(int)ImGuiCol.SeparatorActive] = colorActiveBg;
            style.Colors[(int)ImGuiCol.Header] = colorFrame;
            style.Colors[(int)ImGuiCol.HeaderHovered] = colorHover;
            style.Colors[(int)ImGuiCol.HeaderActive] = colorActiveBg;
            style.Colors[(int)ImGuiCol.ResizeGrip] = colorFrame;
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = colorHover;
            style.Colors[(int)ImGuiCol.ResizeGripActive] = colorActiveBg;
            style.Colors[(int)ImGuiCol.Tab] = tabInactive;
            style.Colors[(int)ImGuiCol.TabUnfocused] = tabInactive;
            style.Colors[(int)ImGuiCol.TabActive] = tabActive;
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = tabActive;
            style.Colors[(int)ImGuiCol.TabHovered] = tabHover;
            style.Colors[(int)ImGuiCol.PlotLines] = colorHighlight;
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = colorHighlight;
            style.Colors[(int)ImGuiCol.PlotHistogram] = colorHighlight;
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = colorHighlight;
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);

            ImGui.PushFont(FontManager.BigFont);
            ImGui.SetNextWindowSize(new Vector2(400, 350)); // ✅ Nhỏ gọn lại
            ImGui.Begin("LH VN", ImGuiWindowFlags.NoResize);

            string[] tabs = { "Aim", "Esp", "Misc", "Setting", };
            float buttonWidth = 80f;     // 👈 Giảm cho vừa
            float buttonHeight = 30f;
            float buttonSpacing = 8f;


            // Tính vị trí để canh giữa tab buttons
            float totalWidth = (buttonWidth * tabs.Length) + (buttonSpacing * (tabs.Length - 1));
            float windowWidth = ImGui.GetWindowSize().X;
            float offsetX = (windowWidth - totalWidth) / 2.0f;
            ImGui.SetCursorPosX(offsetX);

            for (int i = 0; i < tabs.Length; i++)
            {
                bool isActive = (selectedTab == i);

                // ✅ Màu nút đen đậm, hover nhẹ
                ImGui.PushStyleColor(ImGuiCol.Button, isActive ? new Vector4(0.10f, 0.10f, 0.10f, 1f) : new Vector4(0.06f, 0.06f, 0.06f, 1f));
                ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.15f, 0.15f, 0.15f, 1f));
                ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.12f, 0.12f, 0.12f, 1f));
                ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 1f, 1f, 1f)); // Text trắng rõ

                ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 8f);
                ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12, 8));
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(buttonSpacing, 6));

                if (i > 0)
                    ImGui.SameLine();

                if (ImGui.Button(tabs[i], new Vector2(buttonWidth, buttonHeight)))
                    selectedTab = i;

                ImGui.PopStyleVar(3);
                ImGui.PopStyleColor(4);
            }

            ImGui.Separator();
            ImGui.BeginChild("Content", new Vector2(0, 0), ImGuiChildFlags.None); // ✅ BeginChild bọc tất cả tabs


            if (selectedTab == 0)
            {
                ImGui.Separator();



                ImGui.Text("Legit Function");
                ImGui.Spacing();
                ImGui.Separator();
                ImGui.Spacing();

                // Silent & AimBot (chỉ chọn một)
                if (ImGui.Checkbox("Silent Legit", ref Config.Slient2) && Config.Slient2)
                {
                    Config.AimLegit = false;
                    Config.silent360 = false;
                }
                if (ImGui.Checkbox("Silent 360", ref Config.silent360) && Config.silent360)
                {
                    Config.AimLegit = false;
                    Config.Slient2 = false;
                }
               

                ImGui.Text("Silent Settings");




                // Head & Chest Rate (liên kết ngược)
                ImGui.Text("Aim Ratio");
                ImGui.Separator();

                // HEAD RATE
                float headBefore = Config.HeadRate;
                if (ImGui.SliderFloat("Head Rate", ref Config.HeadRate, 0.0f, 1.0f, "%.2f"))
                {
                    // Giảm Chest khi tăng Head
                    float change = Config.HeadRate - headBefore;
                    Config.ChestRate = Math.Max(0.0f, Config.ChestRate - change);
                }

                // CHEST RATE  
                float chestBefore = Config.ChestRate;
                if (ImGui.SliderFloat("Chest Rate", ref Config.ChestRate, 0.0f, 1.0f, "%.2f"))
                {
                    // Giảm Head khi tăng Chest
                    float change = Config.ChestRate - chestBefore;
                    Config.HeadRate = Math.Max(0.0f, Config.HeadRate - change);
                }

                // Hiển thị cảnh báo nếu vượt quá 1.0
                if (Config.HeadRate + Config.ChestRate > 1.0f)
                {
                    ImGui.TextColored(new Vector4(1f, 0.3f, 0.3f, 1f), "gay gay gay");
                }


                // ImGui.Checkbox("Show fov", ref Config.ShowSilentFov);
                ImGui.Separator();

               


                
                










                {


                    ImGui.Checkbox("Ignore Knocked", ref Config.IgnoreKnocked);














































                    //ImGui.Text("Telikill Range Power:");

                    //ImGui.SliderFloat("##Range", ref Config.TeleportRange, 10, 100, "%.2f");



















                    //     ImGui.Checkbox("Down Player", ref Config.down);
                    //     ImGui.Checkbox("Fly Me", ref Config.flyme);









                    //  ImGui.SliderFloat("##downspeed", ref Config.downSpeed, 0.1f, 5.0f, "%.2f");




                    //    ImGui.Checkbox("Down Player", ref Config.down);





                    ImGui.Spacing();





                    ImGui.Separator();
                    ImGui.Spacing();










                    ImGui.PopFont();




                }
            }

            else if (selectedTab == 1)
            {
                ImGui.Separator();
                ImGui.Text("Visuals Panel");
                ImGui.Checkbox("ESP Line (White)", ref Config.ESPLine2);
                ImGui.Checkbox("ESP Line (Rainbow)", ref Config.ESPLine);

                if (Config.ESPLine)
                {
                    // Dòng 1: Line Style
                    ImGui.Text("Style");
                    ImGui.SameLine(60);
                    ImGui.SetNextItemWidth(-1);
                    string[] lineModes = {
        "Straight", "Curved", "ZigZag", "Wave", "Spiral",
        "Dashed", "Dotted", "Arrow", "Lightning", "Spring",
        "Pulse", "DNA", "Electric"
    };
                    int currentMode = (int)Config1.ESPLineMode;
                    if (ImGui.Combo("##LineStyle", ref currentMode, lineModes, lineModes.Length))
                    {
                        Config1.ESPLineMode = (LineMode)currentMode;
                    }

                    // Dòng 2: Thickness + Glow
                    ImGui.Text("Thickness");
                    ImGui.SameLine(60);
                    ImGui.SetNextItemWidth(80);
                    ImGui.SliderFloat("##Thickness", ref Config1.ESPLineThickness, 1.0f, 15.0f, "%.1f");

                    ImGui.SameLine();
                    ImGui.Text("Glow");
                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(-1);
                    ImGui.SliderFloat("##Glow", ref Config1.ESPLineGlowRadius, 0.0f, 20.0f, "%.1f");

                    // Dòng 3: Rainbow Color Toggle
                    ImGui.Text("Rainbow");
                    ImGui.SameLine(60);
                    ImGui.Checkbox("##RainbowMode", ref Config1.ESPRainbowMode);

                    // Dòng 4: Color picker nếu không dùng rainbow
                    if (!Config1.ESPRainbowMode)
                    {
                        ImGui.Text("Color");
                        ImGui.SameLine(60);
                        Vector4 lineColor = new Vector4(
                            Config.ESPLineColor.R / 255f,
                            Config.ESPLineColor.G / 255f,
                            Config.ESPLineColor.B / 255f,
                            Config.ESPLineColor.A / 255f
                        );

                        if (ImGui.ColorEdit4("##LineColor", ref lineColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel))
                        {
                            Config.ESPLineColor = Color.FromArgb(
                                (int)(lineColor.W * 255),
                                (int)(lineColor.X * 255),
                                (int)(lineColor.Y * 255),
                                (int)(lineColor.Z * 255)
                            );
                        }
                    }


                }







                ImGui.Checkbox("ESP Box", ref Config.ESPBox);

                ImGui.SameLine();
                // Color picker when active

                ImGui.ColorEdit4("ESP Box Color", ref boxColor, ImGuiColorEditFlags.PickerMask | ImGuiColorEditFlags.NoInputs);
                Config.ESPBoxColor = Color.FromArgb(
                    (int)(boxColor.W * 255),
                    (int)(boxColor.X * 255),
                    (int)(boxColor.Y * 255),
                    (int)(boxColor.Z * 255)
                );











                ImGui.Checkbox("ESP Health", ref Config.ESPHealth);

                if (Config.ESPHealth)
                {
                    // Dòng 1: Health Position
                    ImGui.Text("Position");
                    ImGui.SameLine(60);
                    ImGui.SetNextItemWidth(-1);
                    string[] healthPositions = { "Left", "Right", "Top", "Bottom" };
                    int currentHealthPos = (int)Config.ESPHeadPosition;
                    if (ImGui.Combo("##HealthPosition", ref currentHealthPos, healthPositions, healthPositions.Length))
                    {
                        Config.ESPHeadPosition = (HealthPosition)currentHealthPos;
                    }

                    // Dòng 2: Health Bar Width (nếu muốn thêm)
                    // ImGui.Text("Width");
                    // ImGui.SameLine(60);
                    // ImGui.SetNextItemWidth(-1);
                    // ImGui.SliderFloat("##HealthWidth", ref Config.ESPHealthWidth, 2f, 10f, "%.1f");

                    // Dòng 3: Health Bar Colors (nếu muốn thêm)
                    // ImGui.Text("Colors");
                    // ImGui.SameLine(60);
                    // ImGui.SetNextItemWidth(-1);
                    // string[] colorModes = { "Gradient", "Static", "Rainbow" };
                    // int currentColorMode = (int)Config.ESPHealthColorMode;
                    // if (ImGui.Combo("##HealthColors", ref currentColorMode, colorModes, colorModes.Length))
                    // {
                    //     Config.ESPHealthColorMode = (HealthColorMode)currentColorMode;
                    // }
                }


                ImGui.Checkbox("ESP info", ref Config.ESPInfo );


                ImGui.Checkbox("ESP Name", ref Config.ESPName);

                ImGui.Checkbox("ESP FillBox", ref Config.ESPFillBox);
                
                //   ImGui.Checkbox("ESP Weapon", ref Config.ESPWeapon);
                //    ImGui.Checkbox("ESP Minimap", ref Config.minimap);








                ImGui.Checkbox("ESP Cross Shair", ref Config.CrosshairEnabled);

                {



                }

                ImGui.SliderFloat("Cross Shair Size", ref Config.CrosshairSize, 0, 100);

                ImGui.SliderFloat("Cross Shair Speed", ref Config.CrosshairRotationSpeed, 0, 100);

                // Color picker when active








                if (ImGui.Button("FIX ESP"))
                {
                    UpdateEntities();
                    Core.Entities = new();
                    InternalMemory.Cache = new();
                }
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindfixesp ? "Press a Key..." : Config.fixespKeyLabel))
                {
                    Config.WaitingForKeybindfixesp = true;
                    Config.fixespKeyLabel = "Press a Key...";
                }

                if (Config.WaitingForKeybindfixesp)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            // Không cho trùng key
                            if (key == Config.SpKey || key == Config.TelePortKey || key == Config.TeleKillKey)
                            {
                                Config.fixespKeyLabel = "Key in use!";
                            }
                            else
                            {
                                Config.fixespKey1 = key;
                                Config.fixespKeyLabel = key.ToString();
                            }

                            Config.WaitingForKeybindfixesp = false;
                            Config.KeyAlreadyPressed13 = false;
                            break;
                        }
                    }
                }
                ImGui.SameLine();

               
                {
                    if (autoResetEsp)
                    {
                        StartEspAutoReset(); // Hàm của bạn
                    }
                    else
                    {
                        espResetToken.Cancel(); // Hủy reset tự động
                    }
                }


            }


            else if (selectedTab == 3)
            {
                ImGui.Text("Settings Panel");
                // Thay đổi từ "Overlay" thành "Efficiency"
                IntPtr overlayHandle = FindWindow(null, "Efficiency");

                if (overlayHandle == IntPtr.Zero)
                {
                    ImGui.TextColored(new Vector4(1f, 0f, 0f, 1f), "Không tìm thấy cửa sổ 'Efficiency'!");
                }
                else
                {
                    // Hiển thị thông tin debug
                    if (ImGui.Button("Test##StreamMode"))
                    {
                        Console.WriteLine($"Efficiency Handle: 0x{overlayHandle:X}");

                        uint currentAffinity = 0;
                        GetWindowDisplayAffinity(overlayHandle, ref currentAffinity);
                        Console.WriteLine($"Current Affinity: {currentAffinity}");
                    }

                    // Checkbox Stream Mode
                    if (ImGui.Checkbox("Stream Mode", ref Config.StreamMode))
                    {
                        if (Config.StreamMode)
                        {
                            // Ẩn khỏi OBS/Stream
                            uint result = SetWindowDisplayAffinity(overlayHandle, WDA_EXCLUDEFROMCAPTURE);
                            if (result != 0)
                            {
                                Console.WriteLine("✅ Stream Mode: ON - Efficiency hidden from capture");
                                // Beep báo thành công
                                Beep(1000, 100);
                            }
                            else
                            {
                                Console.WriteLine("❌ Không thể bật Stream Mode");
                                Config.StreamMode = false; // Reset lại
                                Beep(500, 300); // Beep báo lỗi
                            }
                        }
                        else
                        {
                            // Hiện lại bình thường
                            uint result = SetWindowDisplayAffinity(overlayHandle, WDA_NONE);
                            if (result != 0)
                            {
                                Console.WriteLine("✅ Stream Mode: OFF - Efficiency visible");
                                Beep(800, 100);
                            }
                        }
                    }

                    // Hiển thị trạng thái
                    ImGui.SameLine();
                    uint affinity = 0;
                    GetWindowDisplayAffinity(overlayHandle, ref affinity);

                    if (affinity == WDA_EXCLUDEFROMCAPTURE)
                    {
                        ImGui.TextColored(new Vector4(0f, 1f, 0f, 1f), " (đang Ẩn)");
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip("Cửa sổ 'Efficiency' đang bị ẩn khỏi OBS/Recording");
                    }
                    else
                    {
                        ImGui.TextColored(new Vector4(1f, 1f, 0f, 1f), " (đang Hiện)");
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip("Cửa sổ 'Efficiency' đang hiển thị bình thường");
                    }
                }


                bool close = false;
                ImGui.Checkbox("Close Panel", ref close);
                {
                    if (close)
                    {
                        KillProcess("HD-Adb");
                        Task.Delay(2000);
                        KillProcess("HD-Player");
                        Task.Delay(1000);
                        Environment.Exit(0);
                    }
                }
                ImGui.Separator();
                ImGui.Text("Hot Key");
                ImGui.Checkbox("Show Status ", ref Config.showsta);
                ImGui.Checkbox("Show Up Player Status", ref Config.showup);
                ImGui.Checkbox("Show Pull Enemy Status", ref Config.showpull);
                ImGui.Checkbox("Show Tele Enemy Status", ref Config.showteleport);
                ImGui.Checkbox("Show Ai Player Status", ref Config.showai);
               
                //  ImGui.Checkbox("Show Dive Kill Status", ref Config.showdive);
                ImGui.Checkbox("Show Silent Legit Status", ref Config.showsilent);
                ImGui.Checkbox("Show Silent 360 Status", ref Config.showsilent2);
                ImGui.Checkbox("Show AimBot Legit Status", ref Config.showaimbot);
                ImGui.Checkbox("Show Fly Status", ref Config.showfly);
                ImGui.Checkbox("Show Ignore Knocked Status", ref Config.showkno);
                





                // 👇 Cấu hình chung
                float buttonWidth1 = 280f;
                float buttonHeight1 = 40f;
                float windowWidth1 = ImGui.GetWindowSize().X;
                float centerX = (windowWidth1 - buttonWidth1) / 2f;

                // 👇 Style nút: xanh dương


                // 👇 Button 1


                // 👇 Khoảng cách giữa 2 nút
                ImGui.Dummy(new Vector2(0, 8));


                // 👇 Pop style
                ImGui.PopStyleVar(2);
                ImGui.PopStyleColor(3);
































































            }




            else if (selectedTab == 2)
            {
                ImGui.Separator();
               



                //     ImGui.Checkbox("No Reload", ref Config.NoReload);
                // ImGui.Checkbox("No Recoil", ref Config.NoRecoil);



                // .. ImGui.Checkbox("Fly", ref Config.flyme);




                ImGui.Checkbox("", ref Config.Aiplayer);
                ImGui.SameLine();
                // ---------------- LONG BÉO HOTKEY ----------------
                ImGui.Text("AI Player :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindAiplayer ? "Press a Key..." : Config.AiplayerKeyLabel))
                {
                    Config.WaitingForKeybindAiplayer = true;
                    Config.AiplayerKeyLabel = "Press a Key...";
                }

                if (Config.WaitingForKeybindAiplayer)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            // Nếu key đang được sử dụng rồi
                            if (key == Config.AiplayerKey)
                            {
                                Config.AiplayerKeyLabel = "Key in use!";
                            }
                            else
                            {
                                // Gán key mới
                                Config.AiplayerKey = key;
                                Config.AiplayerKeyLabel = key.ToString();
                            }

                            // Reset trạng thái chờ key
                            Config.WaitingForKeybindAiplayer = false;
                            Config.KeyAlreadyPressed7 = false;
                            break; // thoát vòng foreach
                        }
                    }
                }
         
                ImGui.Checkbox("", ref Config.proxtelekill);
                ImGui.SameLine();
                // ---------------- TELEKILL HOTKEY ----------------
                ImGui.Text("Pull Enemies :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindTeleKill ? "Press a Key..." : Config.TeleKillKeyLabel))
                {
                    Config.WaitingForKeybindTeleKill = true;
                    Config.TeleKillKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindTeleKill)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.TelePortKey)
                                Config.TeleKillKeyLabel = "Key in use!";
                            else
                            {
                                Config.TeleKillKey = key;
                                Config.TeleKillKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindTeleKill = false;
                            Config.KeyAlreadyPressed4 = false;
                            break;
                        }
                    }
                }




                ImGui.Checkbox("", ref Config.telekill);
                ImGui.SameLine();
                // ---------------- TELEPORT HOTKEY ----------------
                ImGui.Text("Teleport :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindTelePort ? "Press a Key..." : Config.TelePortKeyLabel))
                {
                    Config.WaitingForKeybindTelePort = true;
                    Config.TelePortKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindTelePort)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.TeleKillKey)
                                Config.TelePortKeyLabel = "Key in use!";
                            else
                            {
                                Config.TelePortKey = key;
                                Config.TelePortKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindTelePort = false;
                            Config.KeyAlreadyPressed3 = false;
                            break;
                        }
                    }
                }

                ImGui.Checkbox("", ref Config.UpPlayer);
                ImGui.SameLine();
                // ---------------- UP PLAYER HOTKEY ----------------
                ImGui.Text("Up Player :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindUpPlayer ? "Press a Key..." : Config.UpPlayerKeyLabel))
                {
                    Config.WaitingForKeybindUpPlayer = true;
                    Config.UpPlayerKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindUpPlayer)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.TelePortKey || key == Config.TeleKillKey)
                                Config.UpPlayerKeyLabel = "Key in use!";
                            else
                            {
                                Config.UpPlayerKey = key;
                                Config.UpPlayerKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindUpPlayer = false;
                            Config.KeyAlreadyPressed6 = false;
                            break;
                        }
                    }
                }



                ImGui.Checkbox("", ref Config.teliport);
                ImGui.SameLine();
                // ---------------- UP PLAYER HOTKEY ----------------
                ImGui.Text("Fly Lop Xe :");
                ImGui.SameLine();
                if (ImGui.Button(Config.WaitingForKeybindunderplayer ? "Press a Key..." : Config.underplayerPortKeyLabel))
                {
                    Config.WaitingForKeybindunderplayer = true;
                    Config.underplayerPortKeyLabel = "Press a Key...";
                }
                if (Config.WaitingForKeybindunderplayer)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (KeyHelper.IsKeyDown(key))
                        {
                            if (key == Config.SpKey || key == Config.underplayerKey || key == Config.underplayerKey)
                                Config.underplayerPortKeyLabel = "Key in use!";
                            else
                            {
                                Config.underplayerKey = key;
                                Config.underplayerPortKeyLabel = key.ToString();
                            }
                            Config.WaitingForKeybindunderplayer = false;
                            Config.KeyAlreadyPressed10 = false;
                            break;
                        }
                    }
                }
                ImGui.Separator();
                // Slider 1: Điều chỉnh khoảng cách teleport
                ImGui.SliderFloat("Teleport Distance (wall)", ref teleportDistance, 0.1f, 10.0f, "%.1f m");
                ImGui.SliderFloat("Height Fly", ref Config.flymeHeight, -10.0f, 1000.0f, "%.1f m");
                // Slider 2: Điều chỉnh độ trễ teleport
                ImGui.SliderInt("Teleport Delay (wall)", ref teleportDelay, 1, 100, "%d ms");
                ImGui.SliderFloat("Up Player Height", ref Config.test, 1.0f, 100.0f, "%.1f m");
                //     ImGui.SliderFloat("Divekill seting", ref Config.DiveKillDepth, 1f, 5f, "%.1f s");
                if (ImGui.CollapsingHeader("Pull Settings"))
                {
                    ImGui.SliderFloat("Offset X", ref Config.TeleOffsetX, -5f, 5f, "%.2f");
                    ImGui.SliderFloat("Offset Y", ref Config.TeleOffsetY, -5f, 5f, "%.2f");
                    ImGui.SliderFloat("Offset Z", ref Config.TeleOffsetZ, -5f, 5f, "%.2f");
                }





                ImGui.EndChild();
                ImGui.End();

            }
        }









        private float statusDisplayTime = 0f;
        private const float STATUS_DISPLAY_DURATION = 3f;

        private int currentHotkey = -1;
        private bool waitingForHotkey = false;
        private bool hotkeyPressedLastFrame = false;

        private int secondHotkey = -1;
        private bool waitingForSecondHotkey = false;
        private bool secondHotkeyPressedLastFrame = false;

        private int thirdHotkey = -1;
        private bool waitingForThirdHotkey = false;
        private bool thirdHotkeyPressedLastFrame = false;

        private bool isStreamMode = false;
        private bool streamModeKeyPressedLastFrame = false;

        private bool beepEnabled = true;

        // Khai báo ở ngoài hàm Render, ví dụ: static hoặc global trong class
        static bool enablePatch = false;
        static string patchStatus = "Not yet done."; // Trạng thái hiển thị

        // Biến trạng thái - khai báo ngoài hàm render
        static bool aobPatch = false;
        static bool noRecoil = false;
        static bool godMode = false;
        static bool invisibility = false;

        static string statusAob = "Not yet done.";
        static string statusRecoil = "Not yet done.";
        static string statusGod = "Not yet done.";
        static string statusInvis = "Not yet done.";
        void HandleHotkeys()
        {
            // Speed Toggle
            if (KeyHelper.IsKeyDown(Config.SpKey))
            {
                if (!Config.KeyAlreadyPressed)
                {
                    Config.speed = !Config.speed;
                    Config.KeyAlreadyPressed = true;
                }
            }
            else Config.KeyAlreadyPressed = false;

            // Teleport Toggle
            if (KeyHelper.IsKeyDown(Config.TelePortKey))
            {
                if (!Config.KeyAlreadyPressed3)
                {
                    Config.telekill = !Config.telekill;
                    Config.KeyAlreadyPressed3 = true;
                }
            }
            else Config.KeyAlreadyPressed3 = false;

            // TeleKill Toggle
            if (KeyHelper.IsKeyDown(Config.TeleKillKey))
            {
                if (!Config.KeyAlreadyPressed4)
                {
                    Config.proxtelekill = !Config.proxtelekill;
                    Config.KeyAlreadyPressed4 = true;
                }
            }
            else Config.KeyAlreadyPressed4 = false;

            // Up Toggle
            if (KeyHelper.IsKeyDown(Config.UpPlayerKey))
            {
                if (!Config.KeyAlreadyPressed6)
                {
                    Config.UpPlayer = !Config.UpPlayer;
                    Config.KeyAlreadyPressed6 = true;
                }
            }
            else Config.KeyAlreadyPressed6 = false;
        }


        private bool waitingForFreezeKey = false;
        private bool waitingForGhostKey = false;
        private bool waitingForTelekillKey = false;

        private void ApplyStyle()
        {
            ImGuiStylePtr style = ImGui.GetStyle();

            style.WindowBorderSize = 0f;
            style.WindowRounding = 6f;
            style.FrameBorderSize = 0f;
            style.FrameRounding = 5f;
            style.GrabRounding = 4f;
            style.ScrollbarRounding = 6f;
            style.PopupRounding = 4f;
            style.ItemSpacing = new Vector2(10, 6);
            style.WindowPadding = new Vector2(10, 8);
            style.FramePadding = new Vector2(10, 6);

            // 1. NỀN CHÍNH - Đen than (Dark Charcoal)
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.09f, 0.09f, 0.09f, 0.98f); // #171717
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.08f, 0.08f, 0.08f, 1.00f); // #141414
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.11f, 0.11f, 0.11f, 0.98f); // #1C1C1C

            // 2. VĂN BẢN - Trắng tinh khiết
            style.Colors[(int)ImGuiCol.Text] = new Vector4(0.95f, 0.95f, 0.95f, 1.00f); // Trắng sáng
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.50f, 0.50f, 0.50f, 1.00f); // Xám nhạt

            // 3. NÚT BẤM - Xám đậm gradient
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.18f, 0.18f, 0.18f, 1.00f); // #2E2E2E
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f); // #404040
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.22f, 0.22f, 0.22f, 1.00f); // #383838

            // 4. FRAME (Checkbox, Input, Slider background) - Xám than
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.14f, 0.14f, 0.14f, 1.00f); // #242424
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f); // #333333
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.16f, 0.16f, 0.16f, 1.00f); // #292929

            // 5. TAB - Xám đậm với highlight trắng
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.12f, 0.12f, 0.12f, 1.00f); // #1F1F1F
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f); // #333333
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f); // #404040
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.10f, 0.10f, 0.10f, 1.00f); // #1A1A1A
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.18f, 0.18f, 0.18f, 1.00f); // #2E2E2E

            // 6. TITLE BAR - Đen tuyền với viền xám
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.06f, 0.06f, 0.06f, 1.00f); // #0F0F0F
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.08f, 0.08f, 0.08f, 1.00f); // #141414
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.06f, 0.06f, 0.06f, 0.75f); // #0F0F0F mờ

            // 7. SCROLLBAR - Xám đen
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.10f, 0.10f, 0.10f, 1.00f); // #1A1A1A
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.30f, 0.30f, 0.30f, 1.00f); // #4D4D4D
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.40f, 0.40f, 0.40f, 1.00f); // #666666
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.35f, 0.35f, 0.35f, 1.00f); // #595959

            // 8. CHECKMARK & SLIDER - Trắng sáng
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.95f, 0.95f, 0.95f, 1.00f); // Trắng
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.70f, 0.70f, 0.70f, 1.00f); // Xám sáng
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.85f, 0.85f, 0.85f, 1.00f); // Trắng xám

            // 9. HEADER (collapsing headers) - Xám đậm
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f); // #333333
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f); // #404040
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.30f, 0.30f, 0.30f, 1.00f); // #4D4D4D

            // 10. SEPARATOR & BORDER - Xám
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.25f, 0.25f, 0.25f, 1.00f); // #404040
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.35f, 0.35f, 0.35f, 1.00f); // #595959
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.45f, 0.45f, 0.45f, 1.00f); // #737373
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.20f, 0.20f, 0.20f, 1.00f); // #333333

            // 11. RESIZE GRIP - Xám nhẹ
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.30f, 0.30f, 0.30f, 0.30f); // #4D4D4D mờ
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.40f, 0.40f, 0.40f, 0.60f); // #666666
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.50f, 0.50f, 0.50f, 0.90f); // #808080

            // 12. PLOT LINES/HISTOGRAM - Xám trung tính
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f); // #999999
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.80f, 0.80f, 0.80f, 1.00f); // #CCCCCC
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.70f, 0.70f, 0.70f, 1.00f); // #B3B3B3
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(0.90f, 0.90f, 0.90f, 1.00f); // #E6E6E6

            // 13. MODAL DIM BACKGROUND - Đen trong suốt
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.60f); // Đen 60%
        }









        public void KillProcess(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }

        private float rotationAngle = 0f; // Rotation angle variable
        private void DrawShurikenCrosshair()
        {
            if (!Config.CrosshairEnabled) return;

            var drawList = ImGui.GetBackgroundDrawList();
            Vector2 center = new Vector2(Core.Width / 2f, Core.Height / 2f);
            float radius = Config.CrosshairSize;
            uint color = ColorToUint32(Config.CrosshairColor);

            // Number of blades
            int bladeCount = 4;
            float angleStep = 360f / bladeCount;

            // Draw each blade
            for (int i = 0; i < bladeCount; i++)
            {
                float angle = rotationAngle + i * angleStep;
                float angleInRadians = MathF.PI / 180f * angle;

                // Calculate the blade's points
                Vector2 point1 = new Vector2(
                    center.X + MathF.Cos(angleInRadians) * radius,
                    center.Y + MathF.Sin(angleInRadians) * radius
                );

                Vector2 point2 = new Vector2(
                    center.X + MathF.Cos(angleInRadians + MathF.PI / 6) * (radius / 2),
                    center.Y + MathF.Sin(angleInRadians + MathF.PI / 6) * (radius / 2)
                );

                Vector2 point3 = new Vector2(
                    center.X + MathF.Cos(angleInRadians - MathF.PI / 6) * (radius / 2),
                    center.Y + MathF.Sin(angleInRadians - MathF.PI / 6) * (radius / 2)
                );

                // Draw the blade
                drawList.AddTriangleFilled(point1, point2, point3, color);
            }

            // Increment rotation angle for animation
            rotationAngle += Config.CrosshairRotationSpeed;
            if (rotationAngle >= 360f) rotationAngle -= 360f;
        }
        private void DrawLine(ImDrawListPtr drawList, Vector2 startPos, Vector2 endPos, uint color)
        {
            if (startPos.X > 0 && startPos.Y > 0 &&
                endPos.X > 0 && endPos.Y > 0 &&
                startPos.X < Core.Width && startPos.Y < Core.Height &&
                endPos.X < Core.Width && endPos.Y < Core.Height)
            {
                drawList.AddLine(startPos, endPos, color, 1.5f);
            }
        }
        private uint GetESPColor(Color c)
        {
            return ImGui.ColorConvertFloat4ToU32(
                new Vector4(
                    c.R / 255f,
                    c.G / 255f,
                    c.B / 255f,
                    c.A / 255f
                )
            );
        }
        private void skeleton(Entity entity)
        {
            var drawList = ImGui.GetForegroundDrawList();
            uint lineColor = ColorToUint32(Config.ESPSkeletonColor);

            float dist = Math.Max(entity.Distance, 1f);

            // ----- DYNAMIC THICKNESS -----
            float thickness = 1.3f / (1f + (dist * 0.03f));
            thickness = Math.Clamp(thickness, 0.35f, 1.3f); // mỏng khi xa, dày như cũ khi gần

            // ==== WORLD → SCREEN ====
            Vector2 spine = W2S.WorldToScreen(Core.CameraMatrix, entity.Spine, Core.Width, Core.Height);
            Vector2 hip = W2S.WorldToScreen(Core.CameraMatrix, entity.Hip, Core.Width, Core.Height);

            Vector2 rShoulder = W2S.WorldToScreen(Core.CameraMatrix, entity.RightShoulder, Core.Width, Core.Height);
            Vector2 rElbow = W2S.WorldToScreen(Core.CameraMatrix, entity.RightElbow, Core.Width, Core.Height);
            Vector2 rWrist = W2S.WorldToScreen(Core.CameraMatrix, entity.RightWristJoint, Core.Width, Core.Height);

            Vector2 lShoulder = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftShoulder, Core.Width, Core.Height);
            Vector2 lElbow = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftElbow, Core.Width, Core.Height);
            Vector2 lWrist = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftWristJoint, Core.Width, Core.Height);

            Vector2 rFoot = W2S.WorldToScreen(Core.CameraMatrix, entity.RightFoot, Core.Width, Core.Height);
            Vector2 lCalf = W2S.WorldToScreen(Core.CameraMatrix, entity.LeftCalf, Core.Width, Core.Height);

            // FUNCTION DRAW
            void Bone(Vector2 a, Vector2 b)
            {
                if (a.X > 0 && b.X > 0)
                    drawList.AddLine(a, b, lineColor, thickness);
            }

            // ==== BODY ====
            Bone(spine, rShoulder);
            Bone(spine, hip);
            Bone(spine, lShoulder);

            // ==== ARMS ====
            Bone(lShoulder, rElbow);
            Bone(rShoulder, lElbow);

            Bone(lElbow, rWrist);
            Bone(rElbow, lWrist);

            // ==== LEGS ====
            Bone(hip, rFoot);
            Bone(hip, lCalf);
        }

        private float GetCameraYaw()
        {
            return MathF.Atan2(Core.CameraMatrix.M31, Core.CameraMatrix.M33);
        }



        void DrawFillBox(Vector2 position, float width, float height, float alpha)
        {
            uint fillColor = ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, alpha));
            ImGui.GetForegroundDrawList().AddRectFilled(
                new Vector2(position.X, position.Y),
                new Vector2(position.X + width, position.Y + height),
                fillColor
            );
        }

        //private void DrawFOVCircle(float centerX, float centerY, float radius, Color color)
        //{
        //    var drawList = ImGui.GetForegroundDrawList();
        //    drawList.AddCircle(new Vector2(centerX, centerY), radius, ColorToUint32(color), 30, 2.0f);
        //}

        public void DrawCorneredBox(float X, float Y, float W, float H, uint color, float thickness)
        {
            var vList = ImGui.GetForegroundDrawList();

            float lineW = W / 3;
            float lineH = H / 3;

            vList.AddLine(new Vector2(X, Y), new Vector2(X + W, Y), color, thickness); // Đỉnh
            vList.AddLine(new Vector2(X, Y), new Vector2(X, Y + H), color, thickness); // Trái
            vList.AddLine(new Vector2(X + W, Y), new Vector2(X + W, Y + H), color, thickness); // Phải
            vList.AddLine(new Vector2(X, Y + H), new Vector2(X + W, Y + H), color, thickness); // Dưới
        }








        static uint ColorToUint32(Color color)
        {
            return ImGui.ColorConvertFloat4ToU32(new Vector4(
                color.R / 255.0f,
                color.G / 255.0f,
                color.B / 255.0f,
                color.A / 255.0f));
        }

        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public const byte AC_SRC_OVER = 0x00;
            public const byte AC_SRC_ALPHA = 0x01;
        }

        private void HideFromTaskManager()
        {
            IntPtr hwnd = FindWindow(null, "efficiency");
            if (hwnd != IntPtr.Zero)
            {
                int currentStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, currentStyle | WS_EX_TOOLWINDOW);
                Console.WriteLine("✅ Đã ẩn khỏi Taskbar (nhưng vẫn trong Processes)");
            }
        }

        // Các cờ cho SetWindowLong
        const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int LWA_COLORKEY = 0x00000001;
        private const int LWA_ALPHA = 0x00000002;
        private IntPtr FindGameWindow()
        {
            var processes = Process.GetProcessesByName("HD-Player");
            if (processes.Length > 0)
            {
                return processes[0].MainWindowHandle;
            }

            // Hoặc tìm theo tên khác nếu cần
            processes = Process.GetProcessesByName("aow_exe");
            if (processes.Length > 0)
            {
                return processes[0].MainWindowHandle;
            }

            return IntPtr.Zero;
        }
        public IntPtr Handle { get; private set; }
        void CreateHandle()
        {
            RECT rect;
            GetWindowRect(Core.Handle, out rect);
            int x = rect.Left;
            int y = rect.Top;
            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;
            ImGui.SetWindowSize(new Vector2((float)width, (float)height));
            ImGui.SetWindowPos(new Vector2((float)x, (float)y));
            Size = new Size(width, height);
            Position = new Point(x, y);

            Core.Width = width;
            Core.Height = height;

            string overlay = "Overlay";
            IntPtr OverlayHwnd = FindWindow(null, overlay);



        }


        public const uint WDA_NONE = 0;
        //  public const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011; // = 17
        Vector4 HsvToRgb(float h, float s, float v)
        {
            int i = (int)(h * 6);
            float f = h * 6 - i;
            float p = v * (1 - s);
            float q = v * (1 - f * s);
            float t = v * (1 - (1 - f) * s);

            float r = 0, g = 0, b = 0;
            switch (i % 6)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
            }

            return new Vector4(r, g, b, 1f);
        }


        public void DrawHealthBarHorizontal(short health, short maxHealth, float X, float Y, float width)
        {
            var vList = ImGui.GetForegroundDrawList();
            float healthPercentage = (float)health / maxHealth;
            float barWidth = width * healthPercentage;
            uint barColor;

            if (healthPercentage > 0.8f)
            {
                barColor = ColorToUint32(Color.GreenYellow);
            }
            else if (healthPercentage > 0.4f)
            {
                barColor = ColorToUint32(Color.Orange);
            }
            else
            {
                barColor = ColorToUint32(Color.Red);
            }

            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + width, Y + 4), 0x90000000);

            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + barWidth, Y + 4), barColor);
        }

        public void DrawCorneredBox2(float X, float Y, float W, float H, uint color, float thickness)
        {
            var vList = ImGui.GetForegroundDrawList();

            float lineW = W / 3;
            float lineH = H / 3;

            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + W, Y + H), color & 0x00FFFFFF); // Đặt alpha thành 0 để có độ trong suốt




            vList.AddLine(new Vector2(X, Y - thickness / 2), new Vector2(X, Y + lineH), color, thickness);
            vList.AddLine(new Vector2(X - thickness / 2, Y), new Vector2(X + lineW, Y), color, thickness);
            vList.AddLine(new Vector2(X + W - lineW, Y), new Vector2(X + W + thickness / 2, Y), color, thickness);
            vList.AddLine(new Vector2(X + W, Y - thickness / 2), new Vector2(X + W, Y + lineH), color, thickness);
            vList.AddLine(new Vector2(X, Y + H - lineH), new Vector2(X, Y + H + thickness / 2), color, thickness);
            vList.AddLine(new Vector2(X - thickness / 2, Y + H), new Vector2(X + lineW, Y + H), color, thickness);
            vList.AddLine(new Vector2(X + W - lineW, Y + H), new Vector2(X + W + thickness / 2, Y + H), color, thickness);
            vList.AddLine(new Vector2(X + W, Y + H - lineH), new Vector2(X + W, Y + H + thickness / 2), color, thickness);
        }

        void DrawHealthBar(short health, short maxHealth, float X, float Y, float height, float width)
        {
            var drawList = ImGui.GetForegroundDrawList();
            float hpPercent = Math.Clamp((float)health / maxHealth, 0f, 1f);
            float barWidth = width * hpPercent;

            // Màu nền (đen, mờ)
            var bgColor = new Vector4(0f, 0f, 0f, 0.5f);
            drawList.AddRectFilled(new Vector2(X, Y), new Vector2(X + width, Y + height),
                ImGui.ColorConvertFloat4ToU32(bgColor), 3f);

            // Màu thanh máu (chuyển màu đẹp hơn)
            Vector4 barColor;
            if (hpPercent > 0.75f)
                barColor = new Vector4(0.0f, 1.0f, 0.0f, 1f); // Xanh lá
            else if (hpPercent > 0.5f)
                barColor = Vector4.Lerp(new Vector4(1f, 1f, 0f, 1f), new Vector4(0f, 1f, 0f, 1f), (hpPercent - 0.5f) * 4);
            else if (hpPercent > 0.25f)
                barColor = Vector4.Lerp(new Vector4(1f, 0.5f, 0f, 1f), new Vector4(1f, 1f, 0f, 1f), (hpPercent - 0.25f) * 4);
            else
                barColor = new Vector4(1f, 0f, 0f, 1f); // Đỏ

            drawList.AddRectFilled(new Vector2(X, Y), new Vector2(X + barWidth, Y + height),
                ImGui.ColorConvertFloat4ToU32(barColor), 3f);

            // Viền đen mờ
            drawList.AddRect(new Vector2(X, Y), new Vector2(X + width, Y + height),
                ImGui.ColorConvertFloat4ToU32(new Vector4(0f, 0f, 0f, 1f)), 3f, ImDrawFlags.None, 1.0f);

            // Vẽ chữ nhỏ phía bên phải
            string healthText = $"{health}/{maxHealth}";
            Vector2 textSize = ImGui.CalcTextSize(healthText);
            float textX = X + width + 6f;
            float textY = Y + (height - textSize.Y) / 2;

            drawList.AddText(new Vector2(textX, textY),
                ImGui.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.9f)), healthText);
        }

        public void DrawFullBox(float X, float Y, float W, float H, uint color, float alpha)
        {
            var vList = ImGui.GetForegroundDrawList();

            // Vẽ hộp filled box với màu sắc và độ trong suốt
            vList.AddRectFilled(new Vector2(X, Y), new Vector2(X + W, Y + H), color & 0x00FFFFFF | ((uint)(alpha * 255) << 24));
        }
        private void DisplayEnemyCount(float windowWidth, float windowHeight)
        {
            var drawList = ImGui.GetForegroundDrawList();

            //  string enemyCountText = $"{EnemyCount}";


            // Đặt kích thước font lớn hơn
            float fontSize = 220.0f; // Kích thước chữ lớn hơn
            ImGui.GetIO().FontGlobalScale = fontSize / ImGui.GetFontSize();
            // Tính toán kích thước chữ
            //   Vector2 textSize = ImGui.CalcTextSize(enemyCountText);

            // Tính toán vị trí để căn giữa
            //    float x = (windowWidth - textSize.X) / 2;
            // Nâng chữ lên trên giữa một chút
            float y = 40; // Điều chỉnh giá trị 100 tùy theo yêu cầu của bạn

            // Chỉnh sửa màu sắc chữ
            uint textColor = ColorToUint32(Color.White);

            // Hiển thị chữ trên màn hình
            //   drawList.AddText(new Vector2(x, y), textColor, enemyCountText);

            // Khôi phục kích thước font gốc
            ImGui.GetIO().FontGlobalScale = 1.0f;
        }
    }
}