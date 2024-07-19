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
} from "@mui/material";

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
      },
      secondary: {
        main: "#f48fb1",
      },
    },
  });

  return (
    <>
      <ThemeProvider theme={darkTheme}>
        <CssBaseline />
        <div
          style={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <h2>toggle dark mode</h2>
          <Switch checked={toggleDarkMode} onChange={toggleDarkTheme} />
          <Card sx={{ width: "30%", borderRadius: 3, padding: 1 }}>
            <CardContent>
              <CardMedia
                sx={{ height: 180, borderRadius: 3 }}
                image='https://images.pexels.com/photos/546819/pexels-photo-546819.jpeg'
                title='semaphore'
              />
              <Typography variant='h4' component='div' sx={{ marginTop: 3 }}>
                <button>{t("Logout")}</button> <button>{t("Login")}</button>
              </Typography>
              <Typography sx={{ mb: 1.5 }} color='text.secondary'>
                by Semaphore
              </Typography>
              <Typography variant='body1'>{t("Description")}</Typography>
              <SwitchLanguage />
            </CardContent>
          </Card>
        </div>
      </ThemeProvider>
    </>
    //    <SwitchLanguage />
    //  </>
  );
}

export default App;
