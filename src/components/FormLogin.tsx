import React, { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';

function FormLogin() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    const userData = {
      email,
      password,
    };
    console.log('Sending data:', JSON.stringify(userData));
    try {
      const response = await axios.post('https://localhost:5001/account', userData);

      console.log('Login successful:', response.data);

      if (response.status === 200) {
        // Zapisz token w localStorage
        const token = response.data;
        localStorage.setItem('authToken', token);
        
        // Przekierowanie do głównej strony po zalogowaniu
        navigate('/');
      } else {
        setError('Login failed');
      }
    } catch (error: any) {
      console.error('Login failed:', error);
      if (error.response) {
        setError('Login failed: ' + (error.response.data?.message || error.response.statusText));
      } else if (error.request) {
        setError('Login failed: No response from server. Please check your network or server status.');
      } else {
        setError('Login failed: ' + error.message);
      }
    }
  };

  return (
    <div className="content-center">
      <img className="object-center inline w-40 h-40" src="Logo.svg" alt="Witaj" />

      <h1 className="text-5xl mt-5">Zaloguj się</h1>
      <p className="mt-4">Podaj email oraz hasło aby się zalogować</p>

      <form className="mt-10" onSubmit={handleLogin}>
        <input
          type="email"
          placeholder="Imie@example.com"
          className="input bg-black rounded-xl text-center h-10 w-80"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <p className="text-slate-600">Podaj swój email</p>

        <input
          type="password"
          placeholder="ExamplePassword"
          className="input bg-black rounded-xl text-center h-10 w-80 mt-4"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <p className="text-slate-600">Podaj swoje hasło</p>

        <button type="submit" className="btn mt-5 bg-green-600 rounded-xl w-80 h-10 text-black">
          Zaloguj się
        </button>
      </form>

      {error && <p className="text-red-500 mt-4">{error}</p>}

      <p className="mt-4">Nie masz konta?</p>
      <Link to="/SignPage" className="text-blue-500">
        <button className="btn mt-5 bg-green-600 rounded-xl w-40 h-10 text-black">Utwórz konto</button>
      </Link>
    </div>
  );
}

export default FormLogin;
