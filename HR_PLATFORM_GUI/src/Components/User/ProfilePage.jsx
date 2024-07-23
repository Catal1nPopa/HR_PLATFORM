import { Box, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";

const ProfilePage = () => {
  const { t } = useTranslation();

  return (
    <Box>
      <Typography variant='h4' component='h1' gutterBottom>
        {t("ProfilePage")}
      </Typography>
    </Box>
  );
};
export default ProfilePage;
