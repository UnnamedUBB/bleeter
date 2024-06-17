import './App.css'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import LoginPage from './pages/LoginPage';
import SignPage from './pages/SignPage';
import NoPage from './pages/NoPage';
import Followed from "@/pages/Followed.tsx";
import Profile from './pages/Profile';
import Comment from './pages/Comment';
import Messenger from './pages/Messenger';




function App() {

  return (
    <>

      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="comment" element={<Comment />} />
          <Route path="profile" element={<Profile />} />
          <Route path="followed" element={<Followed />} />
          <Route path="*" element={<NoPage />} />
          <Route path="signpage" element={<SignPage />} />
          <Route path="loginpage" index element={<LoginPage />} />
          <Route path="messenger" element={<Messenger />} />
        </Routes>
      </BrowserRouter>

     
      
    </>

  )
}

export default App
