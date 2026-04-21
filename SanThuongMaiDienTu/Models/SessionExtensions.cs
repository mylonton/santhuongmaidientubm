using System.Text.Json;

namespace SanThuongMaiDienTu.Helpers
{
    public static class SessionExtensions
    {
        // Lưu đối tượng vào Session (ép sang JSON)
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // Đọc đối tượng từ Session (giải mã JSON)
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}