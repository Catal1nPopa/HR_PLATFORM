import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.scss";
import "./Translate/i18n";
import { useTranslation } from "react-i18next";
import SwitchLanguage from "./Translate/SwitchLanguage";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import {
  Switch,
  Card,
  CardContent,
  CardMedia,
  Typography,
  Box,
  Button,
} from "@mui/material";
import { Route, Routes, useLocation, useNavigate } from "react-router-dom";
import HomePage from "./Components/Main/MainPage";
import LoginPage from "./Components/Auth/LoginPage";

import darkBackground from "./assets/black_mode.jpg";
import lightBackground from "./assets/white_mode.jpg";

function App() {
  const [count, setCount] = useState(0);
  const { t, i18n } = useTranslation();
  const [toggleDarkMode, setToggleDarkMode] = useState(true);

  const toggleDarkTheme = () => {
    setToggleDarkMode(!toggleDarkMode);
  };

  const darkTheme = createTheme({
    palette: {
      mode: toggleDarkMode ? "dark" : "light", //default theme
      primary: {
        main: "#90caf9",
        light: "#e3f2fd",
        dark: "#42a5f5",
        contrastText: "rgba(0,0,0,0.87)",
      },
      secondary: {
        main: "#ce93db",
        light: "#f3e5f5",
        dark: "#ab47bc",
        contrastText: "rgba(0,0,0,0.87)",
      },
      error: {
        main: "#f44336",
        light: "#e57373",
        dark: "#d32f2f",
        contrastText: "#fff",
      },
      warning: {
        main: "#ffa726",
        light: "#ffb47d",
        dark: "#f57c00",
        contrastText: "rgba(0,0,0,0.87)",
      },
      info: {
        main: "#29b6f6",
        light: "#4fc3f7",
        dark: "#0288d1",
        contrastText: "rgba(0,0,0,0.87)",
      },
      success: {
        main: "#66bb6a",
        light: "#81c784",
        dark: "#388e3c",
        contrastText: "rgba(0,0,0,0.87)",
      },
    },
  });

  return (
    <>
      <ThemeProvider theme={darkTheme}>
        {/* <CssBaseline /> */}
        {/* <Switch checked={toggleDarkMode} onChange={toggleDarkTheme} /> */}
        <Routes>
          <Route path='/' element={<HomePage />} />
          <Route path='/login' element={<LoginPage />} />
        </Routes>
        <SwitchLanguage />
      </ThemeProvider>
    </>
  );
}

export default App;
