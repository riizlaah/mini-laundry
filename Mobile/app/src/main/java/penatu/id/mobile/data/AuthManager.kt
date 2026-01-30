package penatu.id.mobile.data

import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.getValue
import androidx.compose.runtime.setValue
import kotlinx.coroutines.delay

data class User (
    val username: String,
    val name: String,
    val password: String,
)

class AuthManager {
    private val users = mutableMapOf(
        "admin" to User("admin", "Admin Hengker", "nullbyte")
    )
    var currUser by mutableStateOf<User?>(null)
        private set
    var isLoggedIn by mutableStateOf(false)
        private set
    suspend fun login(username: String,password: String): Result<User> {
        delay(500)
        val user = users[username] ?: return Result.failure(Exception("User tidak ditemukan!"))
        if(user.password != password) return Result.failure(Exception("Kredensial salah!"))
        currUser = user
        isLoggedIn = true
        return Result.success(user)
    }
    fun logout() {
        isLoggedIn = false
        currUser = null
    }
    fun validateInput(username: String, password: String): String? {
        return when {
            username.isBlank() -> "Username tidak boleh kosong!"
            password.isBlank() -> "Password tidak boleh kosong!"
            else -> null
        }
    }
}