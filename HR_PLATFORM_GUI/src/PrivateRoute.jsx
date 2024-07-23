// // PrivateRoute.js
// import React from "react";
// import { Navigate, useLocation } from "react-router-dom";
// import useAuth from "./Components/Auth/UseAuth";

// const PrivateRoute = ({ children }) => {
//   const { isAuthenticated } = useAuth();
//   const location = useLocation();

//   if (!isAuthenticated) {
//     // Redirecționează către pagina de login și păstrează locația pentru a reveni după autentificare
//     return <Navigate to='/login' state={{ from: location }} replace />;
//   }

//   return children;
// };
// export default PrivateRoute;

// PrivateRoute.js
import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import useAuth from "./Components/Auth/UseAuth";

const PrivateRoute = ({ children }) => {
  const { isAuthenticated, isAuthChecked } = useAuth();
  const location = useLocation();

  if (!isAuthChecked) {
    return null; // Sau un loader de încărcare
  }

  if (!isAuthenticated) {
    // Redirecționează către pagina de login și păstrează locația pentru a reveni după autentificare
    return <Navigate to='/login' state={{ from: location }} replace />;
  }

  return children;
};

export default PrivateRoute;
