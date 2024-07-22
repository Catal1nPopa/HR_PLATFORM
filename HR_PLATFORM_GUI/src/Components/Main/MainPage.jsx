import { useTranslation } from "react-i18next";
import http from "../../instance";
import { useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

const MainPage = () => {
  const [data, setData] = useState("");
  const [error, setError] = useState("");

  const check = () => {
    const Token = localStorage.getItem("token");
    const decode = jwtDecode(Token);
    console.log(decode);
  };

  useEffect(() => {
    http
      .get("/api/Auth/test")
      .then((response) => response.data) // Folosește response.json() pentru a obține datele JSON
      .then((data) => {
        setData(data); // Setează datele în starea ta
      })
      .catch((error) => {
        setError(error.message || "An error occurred"); // Setează mesajul de eroare
      });
    console.log("interceptor");
  }, []);

  if (error) return <div>Error: {error}</div>;
  if (!data) return <div>Loading...</div>;

  return (
    <div>
      {/* <h4>{t("HomePage")}</h4> */}
      <h1>Data</h1>
      <pre>{JSON.stringify(data, null, 2)}</pre>

      <button onClick={check}> check</button>
    </div>
  );
};

export default MainPage;
