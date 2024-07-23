import {
  styled,
  IconButton,
  Container,
  CssBaseline,
  Box,
  Avatar,
  Typography,
  TextField,
  Button,
} from "@mui/material";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import SwitchLanguage from "../../Translate/SwitchLanguage";
import { LockOutlined } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { Brightness4, Brightness7 } from "@mui/icons-material";
import black_mode from "../../assets/Login/black_mode.jpg";
import white_mode from "../../assets/Login/white_mode.jpg";
import useAuth from "./UseAuth";

const StyledContainer = styled(Container, {
  shouldForwardProp: (prop) => prop !== "darkMode",
})(({ darkMode }) => ({
  backgroundImage: `url(${darkMode ? black_mode : white_mode})`,
  backgroundSize: "cover",
  backgroundPosition: "center",
  height: "100vh",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
}));

const LoginPage = ({ darkMode, toggleDarkTheme }) => {
  const { t } = useTranslation();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { setIsAuthenticated } = useAuth();
  const navigate = useNavigate();

  const loginMethod = async () => {
    navigate("/");
    console.log("logat");
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const response = await fetch("/api/Auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
      });

      if (response.ok) {
        const data = await response.text();
        localStorage.setItem("token", data);
        setIsAuthenticated(true);
        navigate("/home");
        console.log("Login successful:", data);
      } else {
        console.error("Login failes: ", response.statusText);
      }
    } catch (error) {
      console.error("Error", error);
    }
  };

  return (
    <StyledContainer darkMode={darkMode} maxWidth={false}>
      <CssBaseline />
      <Box
        sx={{
          position: "absolute",
          top: 0,
          right: 0,
          display: "flex",
          justifyContent: "space-between",
          alignItems: "flex-start",
          width: "100%",
        }}
      >
        <Box sx={{ flexGrow: 1 }}></Box>
        <SwitchLanguage sx={{ marginRight: 2 }} />
        <IconButton onClick={toggleDarkTheme} sx={{ marginRight: 2 }}>
          {darkMode ? <Brightness7 /> : <Brightness4 />}
        </IconButton>
      </Box>
      <Container maxWidth='xs'>
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Box
            sx={{
              mt: 1,
              borderRadius: 5,
            }}
          >
            <Box>
              <Typography variant='h5'>{t("LoginPage")}</Typography>
              <IconButton onClick={toggleDarkTheme}>
                {darkMode ? <Brightness7 /> : <Brightness4 />}
              </IconButton>
            </Box>
            <form onSubmit={handleSubmit}>
              <TextField
                margin='normal'
                required
                fullWidth
                id='username'
                label='Username'
                name='username'
                autoFocus
                value={username}
                autoComplete='username'
                onChange={(e) => setUsername(e.target.value)}
              />

              <TextField
                margin='normal'
                required
                fullWidth
                id='password'
                name='password'
                label={t("Password")}
                type='password'
                autoComplete='current-password'
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />

              <Button
                fullWidth
                variant='contained'
                sx={{ mt: 3, mb: 2 }}
                type='submit'
              >
                {t("Login")}
              </Button>
            </form>
          </Box>
        </Box>
      </Container>
    </StyledContainer>
  );
};

export default LoginPage;
