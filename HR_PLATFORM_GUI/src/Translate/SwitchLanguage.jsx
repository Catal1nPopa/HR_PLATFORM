import i18next from "i18next";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import React, { useState } from "react";
import gbFlag from "../assets/flags/gb.svg";
import roFlag from "../assets/flags/md.svg";
import { useTranslation } from "react-i18next";

const SwitchLanguage = () => {
  const [language, setLanguage] = useState("en");
  const { t, i18n } = useTranslation();

  const handleChange = (event) => {
    const selectedLanguage = event.target.value;
    setLanguage(selectedLanguage);
    i18next.changeLanguage(selectedLanguage);
  };

  return (
    <FormControl variant='standard' sx={{ m: 1, minWidth: 120 }}>
      <InputLabel id='language-select-label'>{t("Language")}</InputLabel>
      <Select
        labelId='language-select-label'
        id='language-select'
        value={language}
        onChange={handleChange}
        label='Language'
      >
        <MenuItem value='en'>
          <img
            src={gbFlag}
            alt='GB Flag'
            width='20'
            height='15'
            style={{ marginRight: 8 }}
          />
          English
        </MenuItem>
        <MenuItem value='ro'>
          <img
            src={roFlag}
            alt='Romanian Flag'
            width='20'
            height='15'
            style={{ marginRight: 8 }}
          />
          Română
        </MenuItem>
      </Select>
    </FormControl>
  );
};

export default SwitchLanguage;
