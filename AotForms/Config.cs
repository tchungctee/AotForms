using System.Drawing;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;

namespace AotForms
{
    internal static class Config
    {
        // Lớp Config để cấu hình FOV và các tùy chọn khác

        // Tắt FOV ban đầu
        public static bool AimbotAutoAim = true;       // Tự động ghim khi trong FOV
        public static bool silent360 = false;
        public static bool Slientv2 = true;
        public static float AimbotFOV5 = 100f;          // Bán kính FOV
        public static bool ShowFOV = true;                    // Bật/tắt vòng FOV
        public static bool showsta = true;
        public static Vector4 FOVColorFloat = new Vector4(1.0f, 1.0f, 1.0f, 0.4f); // TRẮNG + TRONG SUỐT
        public static bool EnemyPullEnabled = false;                                                                      // Khoảng cách tối đa
        public static float TeleOffsetX = 0.0f; // Trái/phải
        public static float TeleOffsetY = 0.0f; // Trên/dưới
        public static float TeleOffsetZ = 0.0f; // Trước/sau
        public static bool wall = false;
        public static bool teliport = false;
        internal static bool WaitingForKeybindwall = false;
        public static Keys walltKey = Keys.None;          // The selected key for toggling AimBot
        public static string wallPortKeyLabel = "None";
        internal static bool WaitingForKeybindunderplayer = false;
        public static string underplayerPortKeyLabel = "None";
        public static Keys underplayerKey = Keys.None;
        public static bool WaitingForKeybind = false;      // Waiting for user to set a keybind
        public static string AimBotKeyLabel = "None";      // Label for the selected key
        internal static float AimFov1 = 200f;
        public static Keys fixespKey = Keys.None;
        public static string fixespPortKeyLabel = "None";
        internal static bool WaitingForKeybinfixesp = false;
        public static bool FixEspKeyWasPressed = false;
        public enum HealthPosition
        {
            Left,
            Right,
            Top,
            Bottom
        }
        public static HealthPosition ESPHeadPosition { get; set; } = HealthPosition.Left;
        public static bool KeyAlreadyPressed = false;
        public static bool KeyAlreadyPressed3 = false;   // Prevent repeated toggling while holding a key
        public static bool KeyAlreadyPressed4 = false;   // Prevent repeated toggling while holding a key
        public static bool KeyAlreadyPressed5 = false;
        public static bool KeyAlreadyPressed6 = false;  // Prevent repeated toggling while holding a key
        public static bool KeyAlreadyPressed7 = false;
        public static bool KeyAlreadyPressed8 = false;
        public static bool KeyAlreadyPressed9 = false;
        public static bool KeyAlreadyPressed10 = false;
        public static bool KeyAlreadyPressed11 = false;
        public static bool KeyAlreadyPressed12 = false;
        public static bool KeyAlreadyPressed13 = false;
        public static Keys SpeedHackKey = Keys.None;
        public static Keys SpeedHackKey1 = Keys.None;  // The selected key for toggling SpeedHack
        public static string SpeedHackKeyLabel = "None"; // Label for the selected SpeedHack key
        internal static float FOVTapSmoothness = 3.0f;
        internal static float FOVHoldSmoothness = 12.0f;
        public static bool AimbotVisible = false;
        public static bool enableAimBot2 = false;

        public static bool WaitingForKeybind1 = false;      // Waiting for user to set a keybind
        public static string AimBotKeyLabel1 = "None";      // Label for the selected key

        public static bool AimByLong = false;
        private static bool speedDownEnabled = false;
        private static bool speedFireEnabled = false;
        internal static bool DrawFOV2 = false;
        internal static bool PullPlayerEnabled = false;

        public static bool AimbotSafe = false;
        public static bool brutalEnabled = false;
        public static bool fovaimmemory = false;
        public static bool Aiplayerauto = false;
        internal static float AimbotFOV1 = 90f;
        public static bool flyme = false;
        public static float AimSmoothFactor = 0.5f;
        //hotkey brutal
        internal static bool BrutalEnabled = false; // DÙNG CHO CẢ HOTKEY
        internal static Keys BrutalKey = Keys.None;
        internal static string BrutalKeyLabel = "None";
        internal static bool WaitingForKeybindBrutal = false;
        internal static bool KeyAlreadyPressedBrutal = false;
        internal static bool ShowWatermark = true;
        internal static string WatermarkText = "ZYRE STORE";
        internal static float WatermarkSpeed = 1.0f;
        internal static System.Drawing.Color WatermarkColor = System.Drawing.Color.White;
        internal static float WatermarkFontSize = 16f;

        internal static Keys ActivationKey = Keys.None;


        internal static bool VerifyMemoryWrites = false; // Thêm dòng này
        internal static bool AimBotRage2 = false;
        internal static bool SilentAim2 = false;

        internal static Keys AimbotKey2 = Keys.LButton;
       
        private static System.Timers.Timer brutalTimer;
        private static long[] savedAddresses = new long[2];

        internal static bool AimbotNewEnabled = false;
        public static int AimSpeedDelay = 12;          // Tốc độ lên đầu (ms) – CHỈNH ĐƯỢC
        public static float AimRandomSpread = 0.8f;    // Độ lệch random (0.0 - 2.0)
        public static float pushStrength = 0.12f;   
        public static float AimBotFov = 0.1f;
        public static int AimbotTickRate = 10;
        public static float sensitivity = 1.0f;
        public static float AimBotMaxDistance = 150f;
        public static bool IgnoreKnocked = false;
        // Delay giữa các lần ghi memory (ms)
        public static float AimBotMaxDistance1 = 150f;


        public static float AimSmooth = 0.15f; // càng nhỏ càng mượt
        public static float PredictValue = 0.12f; // giá trị prediction vị trí
        public static float AimRotationThreshold = 0.005f; // ngưỡng write memory
        public static int AimLogicInterval = 20; // delay logic


        internal static bool DelayMode = false; // Flag to enable/disable delay mode
        internal static int DelayMin = 0;
        internal static int DelayMax = 150;
        internal static bool showsilent = true;
        internal static bool showpull = true;
        internal static bool showteleport = true;
        internal static bool showai = true;
        internal static bool showwall = true;
        internal static bool showdive = false;
        internal static bool showsilent2 = false;
        internal static bool showaimbot = false;
        internal static bool showfovaim = false;
        internal static bool showkno = false;
        internal static bool showai2 = false;
        internal static bool showup = false;
        internal static bool showfly = false;
        public static float AimSmoothness = 0.5f; // càng nhỏ càng nhanh
        public static int AimInterval = 15; // delay khi viết góc aim (ms)
        internal static int AIRender = 10; // Distance threshold for AI rendering

        internal static bool enableAimBot = false;
      public static float AimbotDelayHold = 100f; // Delay for holding the aimbot key
        public static float AimFOV11 = 25f;              // FOV tính theo khoảng cách màn hình
        public static float AimTightness1 = 2.0f;       // Độ chặt (cao = chặt)
        public static float AimBotMaxDistance51 = 200f; // Khoảng cách tối đa đến mục tiêu
        public static int DelayAim = 100;
        internal static float FOVMouseRadius = 10f;
               internal static float FOVMouseRadius1 = 10f;
        internal static bool FOVMouseEnabled = false; // Whether FOV Mouse is enabled
        internal static bool FOVMouseEnabled1 = false; // Whether FOV Mouse is enabled
        internal static bool AimbotV1Enabled = false;
        internal static bool NoReload = false;
        internal static bool FOVMouseEnabled2 = false; // Whether FOV Mouse is enabled
        internal static bool AimBotV1 = false;
        public static bool DrawFOV = false;
        public static bool SmoothAim = false; // Bật để aim mượt như người
        public static bool VisibilityCheck = true; // Chỉ aim vào mục tiêu trong tầm nhìn
        internal static bool Aimfovc = false;
        internal static Color Aimfovcolor = Color.White;

        public static bool ShowName2 = true;
        public static string Name2Text = "Zyre Store";
        public static float Name2Speed = 180f; // pixels/second

        public static bool CrosshairEnabled = false;
        public static Color CrosshairColor = Color.White;
        public static float CrosshairSize = 15f;
        public static float CrosshairThickness = 2f;
        public static float CrosshairRotationSpeed = 2f;
        internal static float FOVMouseRadius2 = 10f;
        internal static bool AimBot = false;
       
        internal static bool Aimkill = false;
        internal static bool AimLegit = false;
        internal static Keys AimbotKey1 = Keys.None;
        internal static bool Slient2 = false;
        internal static bool AimBotLeft = false;
        internal static float aimlegit = 0.05f;
        public static bool StreamMode = false;

        internal static Keys Silent = Keys.LButton;

        internal static Keys Silent2 = Keys.LButton;

        internal static Keys Aim9 = Keys.LButton;
        internal static Keys AimFovKey = Keys.LButton;


        internal static bool WaitingForKeybindSp = false;
        public static Keys SpKey = Keys.None;          // The selected key for toggling AimBot
        public static string SpKeyLabel = "None";
        public static bool SilentAimEnabled = false;
        public static int SilentAimMode = 0; // 0 = 360, 1 = V1


        internal static bool WaitingForKeybindTelePort = false;
        public static Keys TelePortKey = Keys.None;          // The selected key for toggling AimBot
        public static string TelePortKeyLabel = "None";

        internal static bool WaitingForKeybindUpPlayer = false;
        public static Keys UpPlayerKey = Keys.None;          // The selected key for toggling AimBot
        public static string UpPlayerKeyLabel = "None";


        internal static bool WaitingForKeybindDownPlayer = false;
        public static Keys DownPlayerKey = Keys.None;          // The selected key for toggling AimBot
        public static string DownPlayerKeyLabel = "None";

        internal static bool WaitingForKeybindTeleKill = false;
        public static Keys TeleKillKey = Keys.None;          // The selected key for toggling AimBot
        public static string TeleKillKeyLabel = "None";
        internal static bool WaitingForKeybindfixesp = false;
        public static Keys fixespKey1 = Keys.None;          // The selected key for toggling AimBot
        public static string fixespKeyLabel = "None";
 

        public static bool AiplayerAUTO = false;
        public static bool fixesp = false;

        public static float aiplayersetup = 1.1f; // Nếu muốn chỉnh tay
        public static bool Aiplayer = false;
        internal static bool WaitingForKeybindAiplayer = false;
        public static Keys AiplayerKey = Keys.None;          // The selected key for toggling AimBot
        public static string AiplayerKeyLabel = "None";

        public static float UpPlayerHeight = 2.0f; // Độ cao muốn up player lên
                                                   // 👉 Thêm dòng này để điều chỉnh độ cao bay
        public static float flymeHeight = 1000.0f;
        public static float downSpeed = 0.8f; // mặc định mỗi vòng giảm 0.8 đơn vị


        internal static bool proxtelekill = false;
        internal static bool UpPlayer = false;
        public static float UpPlayerOffset = 0.4f;
        internal static bool AimLock = false;
        internal static bool Showname = false;
        public static int AimDelayMs = 80;
        internal static bool Slient = false;
        public static float HeadRate = 0.0f;   // 30% đầu
        public static float ChestRate = 0.0f;  // 70% ngực/cổ
        public static float ChestOffsetY = -0.25f;
        internal static bool AimFOV = false;
        public static float AimbotFOV = 90;
        public static float AimBodyToHeadDelay = 1.0f;     // 1 GIÂY TỪ BỤNG → ĐẦU
        public static bool Aimbot2 = true;
        public static bool IgnoreKnocked2 = true;
        public static float AimbotFOVvalue = 100f;
        public static float AimBotMaxDistance2 = 300f;
        public static bool EnableSmooth = true;
        
        public static float AimSmoothing = 0.2f;
        public static float AimBotMaxDistance3 = 300f;
        public static bool AutoShoot { get; set; }
        public static float AutoShootMaxDistance { get; set; }
        public static bool Debug { get; internal set; }
   
        internal static bool minimap = false;
        internal static bool espall = false;
        internal static bool SilentAim = false;
        internal static bool FixEsp = false;
       

        // DÙNG TRONG TELEPORT
  
        internal static bool telekill = false;
      
        public static bool RenderFov = true;
        internal static Keys AimbotKey = Keys.LButton; // Mặc định: Chuột trái



        public static float AimFOV1 = 25f;              // FOV tính theo khoảng cách màn hình
        public static float AimTightness = 2.0f;       // Độ chặt (cao = chặt)
        public static float AimBotMaxDistance5 = 200f; // Khoảng cách tối đa đến mục tiêu

        public static float SilentFov = 80f;   // bán kính vòng silent
        public static bool ShowSilentFov = false;

        internal static bool Speed = false;
        internal static bool NoRecoil = false;
        internal static bool MagicBullet = false;
        internal static bool NoCache = false;
        internal static bool RGB = false;

        internal static Color FovColor = Color.White;
        internal static bool ESPLine = false;
        internal static Color ESPLineColor = Color.White;
        internal static bool ESPLine2 = false;

        internal static bool ESPBox = false;
        internal static Color ESPBoxColor = Color.White;

        internal static bool ESPName = false;
        internal static Color ESPNameColor = Color.White;
        internal static bool ESPHealth1 = true;
        internal static bool ESPHealth = false;
        internal static bool ESPFillBox = false;
        internal static Color ESPFillBoxColor = Color.White;

        internal static bool ESPCornerLines = false;
        internal static Color ESPBonesColor = Color.FromArgb(254, 255, 159);
        internal static bool ESPBones = false;
       
        internal static bool ESPLineDuoi = false;
        internal static Color ESPLineDuoiColor = Color.Red;
       
        internal static bool ESPInfo = false;
        internal static bool ESPSkeleton = false;
        internal static Color ESPSkeletonColor = Color.FromArgb(255, 255, 0, 0);
        internal static bool ESPDistance = false;
        internal static bool sound = false;
        // aim ai
        

        // 4 cái head riêng biệt để bật/tắt từng cái
        public static bool ESPHeadLeft { get; set; } = true;
        public static bool ESPHeadRight { get; set; } = true;
        public static bool ESPHeadTop { get; set; } = true;
        public static bool ESPHeadBottom { get; set; } = true;
        public enum LineMode
        {
            Straight,      // Đường thẳng
            Curved,        // Đường cong Bézier
            ZigZag,        // Đường zích zắc
            Wave,          // Đường sóng
            Spiral,        // Đường xoắn ốc
            Dashed,        // Đường nét đứt
            Dotted,        // Đường chấm
            Arrow,         // Đường có mũi tên
            Lightning,     // Đường tia chớp
            Spring,        // Đường lò xo
            Pulse,         // Đường xung
            DNA,           // Đường xoắn kép
            Electric       // Đường điện
        }

        public static class Config1
        {
            public static bool ESPLine = false;
            public static LineMode ESPLineMode = LineMode.Curved;
            public static float ESPLineThickness = 1.5f;
            public static float ESPLineGlowRadius = 3.4f;
          
            
           
            public static Color ESPLineColor = Color.White;
            public static bool ESPRainbowMode = true; // Mặc định bật rainbow
        }

        internal static bool rgb = false;
        internal static float iconsize = 1.0f;
        public static Vector4 ICONCOLOR = new Vector4(1f, 1f, 1f, 1f);
        public static int MemoryAimRepeat = 2;        // Số lần lặp khi AimByMemory
        public static int MemoryAimDelay = 10;        // Delay giữa các lần ghi memory (ms)
        public static HealthColorMode ESPHealthColorMode { get; set; } = HealthColorMode.Static;
        internal static float AimBotSmooth = 16f;
        public static bool ESPRainbowMode = true;
        // Thêm enum
        public enum HealthColorMode
        {
            Static,     // Màu cố định (xanh, vàng, đỏ)
            Gradient,   // Chuyển màu dần
            Rainbow     // Cầu vồng
        }
        internal static bool teli = false;
        internal static bool AimBotRage = false;

        internal static bool speed = false;
        internal static bool speedx = false;

        internal static bool CameraHackEnabled = false;
        internal static bool down = false;
       

        internal static float test = 100;

   
        internal static bool UpdateEntities = false;
        internal static bool telekill2 = false;

     


        internal static float AimFov = 200f;
   
        internal static Color NameCheat = Color.Cyan;
       

        internal static bool ESPHealthText = false;
        internal static Color ESPHealthColor = Color.Green;
        public static List<string> ESPHeadPositions { get; set; } = new List<string>
    {
        "left",
        "right",
        "top",
        "bottom"
    };
        internal static bool FOVEnabled = false;
       

        internal static float DiveKillDepth = 2.5f; // THÊM CONFIG ĐỘ SÂU

        internal static float cameraVal = 1.0f;
        internal static float visionVal = 3.141592741f;

       public static float teleportDistance = 1.0f;
        public static int teleportDelay = 15;
        internal static bool RealTeleport = false;
        internal static float TeleportRange = 10f;

        internal static float test1 = 0.01f;
        internal static bool SidePullMagnet = false;
        public static float TeleportRange1 = 10f;


        public static float AimMouseMaxMove = 12f;  // Max pixel di chuyển mỗi frame
        public static float AimMouseSway = 3f;      // Rung nhẹ như người thật


        public static float AimSpeed = 5f; // Default medium speed
        internal static int AimbotDelayBeforeAim = 0; // 0ms = aim ngay, 50ms = delay 50ms

        internal static bool Slienttinhvi = false;

        public static bool ESPWeapon = false;
        internal static bool ESPWeaponIcon = false;




        internal static bool Aimbotbeta = false;
        internal static bool silentlegit = false;
        internal static bool Enabled = false;
        internal static bool AimbotVisible1 = false;
       
        internal static bool silentlegit1 = false;


        internal static float FOV = 90f;

        internal static float Smoothness2 = 1f;

        internal static float Smoothness = 5f;

        internal static float SoftnessFactor = 0.4f;
        internal static bool IgnoreKnockedPlayers = true;
        internal static bool VisibilityCheck1 = false;


        internal static float MaxDistance = 500.0f;


        public static float AimMouseSensi = 1.0f;    // tốc độ aim
        public static float AimMouseSmooth = 5.0f;   // độ mượt
        public static float aimfov = 150f;

        internal static float SoftnessFactor2 = 0.4f;



        // Hoặc nếu bạn muốn thêm cái mới hoàn toàn:
        internal static class SilentAimConfig
        {
            internal static bool VerifyMemoryWrites = false;
            internal static float MaxHumanization = 0.08f;
            internal static float MinHumanization = 0.02f;


        }
    public enum TargetingMode
    {
        ClosestToCrosshair,
        Target360,
        ClosestToPlayer,
        LowestHealth,
    }
    public enum AimBotType
    {
        Silent,
        AI,
        Mouse,
        XynQaw
    }

}
}
