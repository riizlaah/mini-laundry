package penatu.id.mobile.view

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Button
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.em
import penatu.id.mobile.data.AuthManager
import penatu.id.mobile.ui.theme.DeepSkyBlue

@Composable
fun DashboardScreen(authManager: AuthManager, onLogout: () -> Unit) {
    val user = authManager.currUser
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
                Text("Welcome to the Esemka Laundry mobile app.")
//                Button()
            }
        }
    }
}