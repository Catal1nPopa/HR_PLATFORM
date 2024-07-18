import i18next from "i18next";

const SwitchLanguage = () => {
  return (
    <>
      <button onClick={() => i18next.changeLanguage("en")}>English</button>
      <button onClick={() => i18next.changeLanguage("ro")}>Romana</button>
    </>
  );
};

export default SwitchLanguage;
