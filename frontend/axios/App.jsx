import { BrowserRouter as Router, NavLink, Routes, Route } from "react-router-dom";
import { ItmpListPage } from "./ItmpListPage";
import { ItmpSinglePage } from "./ItmpSinglePage";
import { ItmpCreatePage } from "./ItmpCreatePage";
import { ItmpModPage } from "./ItmpModPage";
import { ItmpDelPage } from "./ItmpDelPage";
import './App.css';

export const App=()=> {
  return (
    <Router>
      <nav className="navbar navbar-expand-sm navbar-dark bg-dark">
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <li className="nav-item">
              <NavLink to={'/'} className={({isActive}) => "nav-link" + (isActive ? "active" : "")}>
                <span className="nav-link">ITMP lista</span>
              </NavLink>
              </li>
              <li className="nav-item">
              <NavLink to={'/new-itmp'} className={({isActive}) => "nav-link" + (isActive ? "active" : "")}>
                <span className="nav-link">Új ITMP bejegyzés</span>
              </NavLink>
              </li>
          </ul>
        </div>
      </nav>
      <Routes>
        <Route path="/" exact element={<ItmpListPage />} />
        <Route path="/itmp/:ItmpId" exact element={<ItmpSinglePage />} />
        <Route path="/new-itmp" exact element={<ItmpCreatePage />} />
        <Route path="/mod-itmp/:ItmpId" exact element={<ItmpModPage />} />
        <Route path="/del-itmp/:ItmpId" exact element={<ItmpDelPage />} />
      </Routes>
    </Router>
  );
}