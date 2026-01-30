package penatu.id.mobile.data

import org.json.JSONArray

data class Notification(
    val id: Int,
    val message: String,
    val datetime: String,
    val status: String
)

class NotificationManager {
    var notifs = mapOf<Int, User>()
    val jsonString = """[{"id":1,"message":"Your package is burning!","status":"error","datetime":"2026-01-29 10:26:37"},{"id":2,"message":"You must pay compensation to us for Rp100.000 because your package is burning","status":"warning","datetime":"2026-01-29 10:36:12"},{"id":3,"message":"Your package somehow repair itself, so we will sent it back to you.","status":"success","datetime":"2026-01-30 00:03:09"},{"id":5,"message":"Your package is being sent","status":"info","datetime":"2026-01-30 00:06:49"},{"id":6,"message":"Your package have came to your house. Btw, Where is our compensation?","status":"warning","datetime":"2026-01-30 00:07:58"}]"""
    fun readNotifs() {
        var array = JSONArray(jsonString)
        for(i in 0 until array.length()) {
            var obj = array.getJSONObject(i)
            var notif = Notification(
                id = obj.getInt("id"),
                message = obj.getString("message"),
                datetime = obj.getString("datetime"),
                status = obj.getString("status"))
        }
    }
}