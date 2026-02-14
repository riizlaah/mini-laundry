package penatu.id.mobile.view

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.em
import kotlinx.coroutines.launch
import penatu.id.mobile.data.AuthManager
import penatu.id.mobile.ui.theme.DodgerBlue

@Composable
fun LoginScreen(modifier: Modifier, authManager: AuthManager, onLoginSuccess: () -> Unit) {
    Column(modifier = modifier, verticalArrangement = Arrangement.spacedBy(8.dp)) {
        var username by remember { mutableStateOf("") }
        var password by remember { mutableStateOf("") }
        var errMessage by remember {mutableStateOf("")}
        val scope = rememberCoroutineScope()
        Text(text = "Esemka Laundry", fontWeight = FontWeight.Bold, modifier = Modifier
            .fillMaxWidth().background(DodgerBlue).padding(24.dp)
            , textAlign = TextAlign.Center, color = Color.White, fontSize = 6.em)
        Column(Modifier.padding(32.dp), verticalArrangement = Arrangement.spacedBy(10.dp)) {
            OutlinedTextField(
                username, onValueChange = { newText: String -> username = newText },
                label = { Text("Username") }, modifier = Modifier.fillMaxWidth(),
                singleLine = true
            )
            OutlinedTextField(
                password, onValueChange = { newText: String -> password = newText },
                label = { Text("Password") }, modifier = Modifier.fillMaxWidth(),
                singleLine = true
            )
            if(errMessage.isNotEmpty()) {
                Text(errMessage, color = MaterialTheme.colorScheme.error, modifier = Modifier.padding(8.dp).fillMaxWidth())
            }
            Button(
                onClick = {
                    val validationErr = authManager.validateInput(username, password)
                    if(validationErr != null) {
                        errMessage = validationErr
                        return@Button
                    }
                    scope.launch {
                        val res = authManager.login(username, password)
                        res.onSuccess {
                            onLoginSuccess()
                        }.onFailure { exception ->
                            errMessage = exception.message ?: "Login gagal"
                        }
                    }
                }, shape = RoundedCornerShape(8.dp),
                modifier = Modifier.padding(4.dp).fillMaxWidth()
            ) {
                Text("Login")
            }
        }
    }
}