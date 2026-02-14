package penatu.id.mobile

import android.app.AlertDialog
import android.content.Context
import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Surface
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import penatu.id.mobile.data.AuthManager
import penatu.id.mobile.ui.theme.LaundryMobileTheme
import penatu.id.mobile.view.DashboardScreen
import penatu.id.mobile.view.LoginScreen

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            Index()
        }
    }
}

@Composable
fun Index() {
    val authManager = remember { AuthManager() }
    LaundryMobileTheme {
        Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
            if(authManager.isLoggedIn) {
                DashboardScreen(Modifier.padding(innerPadding), authManager, onLogout = {authManager.logout()})
            } else {
                LoginScreen(Modifier.padding(innerPadding), authManager, onLoginSuccess = {})
            }
        }
    }
}

fun Context.buildAlert(msg: String, title: String = "Alert"): AlertDialog {
    val builder = AlertDialog.Builder(this)
    builder.setMessage(msg).setTitle(title)
    return builder.create()
}
