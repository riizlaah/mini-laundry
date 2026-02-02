package penatu.id.mobile.view

import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.horizontalScroll
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowDown
import androidx.compose.material3.Button
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ElevatedButton
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.Icon
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.em
import kotlinx.coroutines.launch
import penatu.id.mobile.buildAlert
import penatu.id.mobile.data.AuthManager
import penatu.id.mobile.data.Notification
import penatu.id.mobile.data.NotificationManager
import penatu.id.mobile.ui.theme.DeepSkyBlue
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

enum class CurrContent {
    Home,
    Notifications,
    RequestForm
}

data class Pkg(
    var id: Int,
    var name: String
)

@Composable
fun DashboardScreen(authManager: AuthManager, onLogout: () -> Unit) {
    val user = authManager.currUser
    var currContent by remember { mutableStateOf(CurrContent.Home) }
    val notifManager = remember { NotificationManager() }
    notifManager.readNotifs()
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
                    fontSize = 5.em,
                    lineHeight = 1.2.em
                )
                when(currContent) {
                    CurrContent.Home -> {
                        Home(
                            {currContent = CurrContent.RequestForm},
                            {currContent = CurrContent.Notifications})
                    }
                    CurrContent.Notifications -> {
                        Notifs(
                            {currContent = CurrContent.Home},
                            notifManager.notifs.toList())
                    }
                    CurrContent.RequestForm -> {
                        RequestForm(
                            {currContent = CurrContent.Home},
                            authManager)
                    }

                }
            }
        }
    }
}

@Composable
fun Home(requestOnClick: () -> Unit, notifsOnClick: () -> Unit) {
    Text("Welcome to the Esemka Laundry mobile app.")
    Row(Modifier.fillMaxWidth(), horizontalArrangement = Arrangement.SpaceBetween) {
        ElevatedButton(onClick = requestOnClick, modifier = Modifier.padding(12.dp)) {
            Text("Request for Pickup")
        }
        ElevatedButton(onClick = notifsOnClick, modifier = Modifier.padding(12.dp)) {
            Text("Notifications")
        }
    }
}

@Composable
fun Notifs(onBack: () -> Unit, notifs: List<Notification>) {
    Column {
        TextButton(onClick = onBack) {
            Text("< Notifications")
        }
        HorizontalDivider()
        LazyColumn(verticalArrangement = Arrangement.spacedBy(8.dp)) {
            items(items = notifs, key = { item -> item.id }) { notif ->
                //             val color = when(notif.status) {
                //                 "error" -> Color.Red
                //                 "warning" -> Color.Yellow
                //                 "success" -> Color.Green
                //                 "info" -> DodgerBlue
                //                 else -> Color.Black
                //             }
                Column(
                    modifier = Modifier
                        .fillMaxWidth()
                        .border(2.dp, Color.Black, RoundedCornerShape(8.dp))
                        .padding(16.dp)
                ) {
                    Text(
                        notif.message,
                        modifier = Modifier.fillMaxWidth(),
                        textAlign = TextAlign.Center
                    )
                    Text(
                        LocalDateTime.parse(notif.datetime)
                            .format(
                                DateTimeFormatter
                                    .ofPattern("dd MMM yyyy HH:mm")
                            ),
                        modifier = Modifier.fillMaxWidth(), textAlign = TextAlign.Center
                    )
                }
            }
        }
    }
}

@Composable
fun RequestForm(onBack: () -> Unit, authManager: AuthManager) {
    var name by remember { mutableStateOf(authManager.currUser?.name ?: "") }
    var address by remember { mutableStateOf(authManager.currUser?.address ?: "") }
    var dropdownOpened by remember { mutableStateOf(false) }
    val availablePkgs = listOf(
        Pkg(1, "Paket Hari Raya"),
        Pkg(2, "Paket Kilat"),
        Pkg(3, "Paket Rombongan"),
        )
    var selectedPackage by remember { mutableStateOf(availablePkgs[0]) }
    val scope = rememberCoroutineScope()
    Column(Modifier.fillMaxWidth(), verticalArrangement = Arrangement.spacedBy(8.dp)) {
        TextButton(onClick = onBack) {
            Text("< Request Pickup")
        }
        HorizontalDivider()
        OutlinedTextField(value = name, readOnly = true, onValueChange = {it: String -> name = it},
            label = { Text("Name") }, modifier = Modifier.fillMaxWidth())
        OutlinedTextField(value = address, readOnly = true, onValueChange = {it: String -> address = it},
            label = { Text("Address") }, modifier = Modifier.fillMaxWidth())
        OutlinedButton({dropdownOpened = !dropdownOpened}, shape = RoundedCornerShape(8.dp),
            modifier = Modifier.padding(6.dp).fillMaxWidth()) {
            Row(fillWidth(), horizontalArrangement = Arrangement.SpaceBetween, verticalAlignment = Alignment.CenterVertically) {
                Text(selectedPackage.name)
                Icon(Icons.Default.KeyboardArrowDown, contentDescription = "More Option")
            }
            DropdownMenu(dropdownOpened, {dropdownOpened = false}) {
                for(pkg in availablePkgs) {
                    DropdownMenuItem(
                        text = {Text(pkg.name)},
                        onClick = {
                            selectedPackage = pkg
                            dropdownOpened = false
                        }
                    )
                }
            }
        }
        Row(fillWidth()) {
            val ctx = LocalContext.current
            Button(modifier = Modifier.weight(1f), shape = RoundedCornerShape(8.dp),
                onClick = onBack
            ) {Text("Cancel")}
            Spacer(Modifier.width(8.dp))
            Button(modifier = Modifier.weight(1f), shape = RoundedCornerShape(8.dp),
                onClick = {
                    scope.launch {
                        val res = authManager.sendReq(selectedPackage.id)
                        res.onSuccess {
                            val dialog = ctx.buildAlert("Success!", "Info")
                            dialog.show()
                        }.onFailure {
                            val dialog = ctx.buildAlert("Failde!", "Error")
                            dialog.show()
                        }
                    }
                }
            ) {
                Text("Send")
            }
        }
    }
}

fun fillWidth(): Modifier {
    return Modifier.fillMaxWidth()
}

