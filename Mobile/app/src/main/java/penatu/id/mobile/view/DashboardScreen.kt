package penatu.id.mobile.view

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Button
import androidx.compose.material3.ElevatedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.em
import penatu.id.mobile.data.AuthManager
import penatu.id.mobile.ui.theme.DeepSkyBlue

enum class CurrContent {
    Home,
    Notifications,
    RequestForm
}

@Composable
fun DashboardScreen(authManager: AuthManager, onLogout: () -> Unit) {
    val user = authManager.currUser
    var currContent = remember { CurrContent.Home }
    Scaffold {innerPadding ->
        Column(Modifier.padding(innerPadding)) {
            Row(modifier = Modifier
                .background(DeepSkyBlue)
                .padding(8.dp)
                .fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically) {
                Text(text = "Dashboard", fontWeight = FontWeight.Bold, color = Color.White)
                Button(onClick = onLogout, shape = RoundedCornerShape(4.dp)) {
                    Text("Log out")
                }
            }
            Column(Modifier.padding(8.dp)) {
                Text(
                    "Hello ${user?.name}!",
                    fontWeight = FontWeight.ExtraBold,
                    fontSize = 8.em,
                    lineHeight = 1.2.em
                )
                when(currContent) {
                    CurrContent.Home -> Home({}, {})
//                    CurrContent.Notifications ->
                    else -> Text("Empty")
                }
            }
        }
    }
}

@Composable
fun Home(requestOnClick: () -> Unit, notifsOnClick: () -> Unit) {
    Text("Welcome to the Esemka Laundry mobile app.")
    ElevatedButton(onClick = requestOnClick, modifier = Modifier.padding(12.dp)) {
        Text("Request for Pickup")
    }
    ElevatedButton(onClick = notifsOnClick, modifier = Modifier.padding(12.dp)) {
        Text("Notifications")
    }
}

@Composable
fun Notifs(onBack: () -> Unit) {
    TextButton(onClick = onBack) {
        Text("< Notifications")
    }
    LazyColumn(verticalArrangement = Arrangement.spacedBy(8.dp)) {

    }
}