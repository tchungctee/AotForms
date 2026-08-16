using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Globalization;

namespace x
{
    public class AuthResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string plan { get; set; }
        public string expires_at { get; set; }
        public bool lifetime { get; set; }
        public string note { get; set; }
        public string license_key { get; set; }
        public string device_id { get; set; }
        public string device_name { get; set; }
        public string activated_at { get; set; }
        public string app_id { get; set; }
        public int days_remaining { get; set; }
        public DateTime? expiration_date { get; set; }
    }

    public static class AuthHandler
    {
        public static DateTime? ExpirationDate { get; set; }
        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

        private static string logFilePath;
        private const string current_app = "ai hoi";
        private const string apiUrl = "http://example.com";

        public static void Initialize(string basePath = "")
        {
            if (string.IsNullOrEmpty(basePath))
            {
                logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "a.txt");
            }
            else
            {
                logFilePath = Path.Combine(basePath, "a.txt");
            }

            WriteLog("=== AUTH HANDLER INITIALIZED FOR IMGUI ===");
        }

        // ĐÃ SỬA: Hàm này bây giờ sẽ LUÔN LUÔN trả về success = true
        public static async Task<AuthResponse> ValidateKeyAsync(string userKey)
        {
            try
            {
                if (File.Exists(logFilePath)) File.Delete(logFilePath);
                WriteLog("=== BẮT ĐẦU XÁC THỰC (Bypass Mode) ===");

                // Vẫn ghi log để theo dõi nếu muốn
                string hwid = GetHardwareId();
                string deviceName = Environment.MachineName;
                WriteLog($"HWID: {hwid}, DeviceName: {deviceName}");

                // Tạo một phản hồi thành công "giả lập" hoàn hảo
                var result = new AuthResponse
                {
                    success = true,
                    message = "Xác thực thành công! (Bypassed)",
                    plan = "Premium/Lifetime",
                    expires_at = "Lifetime",
                    lifetime = true,
                    note = "Bypass active",
                    license_key = userKey,
                    device_id = hwid,
                    device_name = deviceName,
                    activated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    app_id = current_app,
                    days_remaining = -1,
                    expiration_date = DateTime.MaxValue
                };

                // Gán biến tĩnh toàn cục cho ngày hết hạn là vĩnh viễn
                ExpirationDate = DateTime.MaxValue;

                WriteLog("Kết quả: THÀNH CÔNG (Luôn luôn True)");

                // Sử dụng Task.FromResult để tương thích với async/await gốc mà không cần gọi API thật
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                WriteLog($"Lỗi: {ex.Message}");
                // Kể cả có lỗi xảy ra trong try, vẫn ép buộc trả về true ở catch
                ExpirationDate = DateTime.MaxValue;
                return new AuthResponse
                {
                    success = true,
                    message = "Xác thực thành công! (Bypass Fallback)",
                    lifetime = true,
                    days_remaining = -1,
                    expiration_date = DateTime.MaxValue
                };
            }
        }

        public static int CalculateDaysRemaining(DateTime expiryDate)
        {
            if (expiryDate == DateTime.MaxValue)
                return -1;

            TimeSpan timeLeft = expiryDate - DateTime.Now;
            int days = (int)Math.Ceiling(timeLeft.TotalDays);
            return days < 0 ? 0 : days;
        }

        public static string FormatTimeRemaining(DateTime expiryDate)
        {
            if (expiryDate == DateTime.MaxValue)
                return "Vĩnh viễn";

            if (expiryDate == DateTime.MinValue)
                return "Không có thời hạn";

            TimeSpan timeLeft = expiryDate - DateTime.Now;

            if (timeLeft.TotalDays >= 1)
            {
                int days = (int)timeLeft.TotalDays;
                return $"{days} ngày {(timeLeft.Hours > 0 ? $"{timeLeft.Hours} giờ" : "")}";
            }
            else if (timeLeft.TotalHours >= 1)
            {
                return $"{timeLeft.Hours} giờ {timeLeft.Minutes} phút";
            }
            else if (timeLeft.TotalMinutes >= 1)
            {
                return $"{timeLeft.Minutes} phút {timeLeft.Seconds} giây";
            }
            else if (timeLeft.TotalSeconds > 0)
            {
                return $"{timeLeft.Seconds} giây";
            }
            else
            {
                return "Đã hết hạn";
            }
        }

        public static (string keyInput, bool submitPressed) DrawLicenseInputUI(string currentKey = "", float width = 300, float height = 30)
        {
            string keyInput = currentKey;
            bool submitPressed = false;
            return (keyInput, submitPressed);
        }

        private static bool IsValidKeyFormat(string key)
        {
            return true; // Cho phép mọi định dạng key
        }

        private static void WriteLog(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"[{DateTime.Now:HH:mm:ss}] {message}\n");
            }
            catch { }
        }

        private static string GetHardwareId()
        {
            try
            {
                string hardwareInfo = "";
                using (var searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        hardwareInfo += obj["ProcessorId"]?.ToString() ?? "";
                        break;
                    }
                }

                if (string.IsNullOrEmpty(hardwareInfo) || hardwareInfo == "0" || hardwareInfo == "00000000")
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            hardwareInfo = obj["SerialNumber"]?.ToString() ?? "";
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(hardwareInfo) || hardwareInfo == "0" || hardwareInfo == "00000000")
                {
                    hardwareInfo = GetVolumeSerial("C");
                }

                if (string.IsNullOrEmpty(hardwareInfo) || hardwareInfo == "0" || hardwareInfo == "00000000")
                {
                    string combinedInfo = Environment.MachineName + Environment.UserName + Environment.OSVersion.VersionString;
                    using (var sha256 = SHA256.Create())
                    {
                        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedInfo));
                        return BitConverter.ToString(hash).Replace("-", "").ToLower();
                    }
                }

                using (var sha256 = SHA256.Create())
                {
                    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(hardwareInfo));
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
            catch
            {
                return "Bypass_HWID";
            }
        }

        private static string GetVolumeSerial(string driveLetter)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveLetter);
                if (drive.IsReady)
                {
                    return drive.RootDirectory.ToString().GetHashCode().ToString("X");
                }
            }
            catch { }
            return "";
        }
    }
}