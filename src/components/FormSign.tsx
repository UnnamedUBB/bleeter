import { useState } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';

function FormSign() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('' );
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Sprawdzenie czy wszystkie wymagane pola są wypełnione
    if (!email || !password) {
      setError('Email and password are required');
      return;
    }

    const userData = {
      userName,
      email,
      password,
    };

    console.log('Sending data:', JSON.stringify(userData));

    try {
      const response = await axios.post('https://localhost:5001/account/create', userData);

      console.log('Response:', response);

      if (response.status === 200) {
        // Przekierowanie do strony logowania po udanej rejestracji
        navigate('/LoginPage');
        
      } else {
        // Obsługa innych statusów
        setError('Registration failed');
      }
    } catch (error: any) {
      console.error('Registration error:', error);
      setError('Registration failed: ' + (error.response?.data?.message || error.message));
    }
  };

  return (
    <div className="content-center">
      <img className="object-center inline w-40 h-40" src="Logo.svg" alt="Witaj" />

      <h1 className="text-5xl mt-5">Utwórz konto</h1>
      <p className="mt-4">Podaj email oraz hasło aby utworzyć konto</p>

      <form className="mt-10" onSubmit={handleRegister}>
        <input
          type="email"
          placeholder="Imie@example.com"
          className="input bg-black rounded-xl text-center h-10 w-80 mt-4"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <p className="text-slate-600">Podaj swój email</p>
        <div className="mt-4"></div>
        <input
          type="password"
          placeholder="ExamplePassword"
          className="input bg-black rounded-xl text-center h-10 w-80"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <p className="text-slate-600">Podaj swoje hasło</p>

        <input
          type="userName"
          placeholder="ExampleUserName"
          className="input bg-black rounded-xl text-center h-10 w-80"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
        <p className="text-slate-600">Podaj swoją nazwe</p>

        <button type="submit" className="btn mt-5 bg-green-600 rounded-xl w-80 h-10 text-black">
          Utwórz konto
        </button>
      </form>
      
      {error && <p className="text-red-500 mt-4">{error}</p>}
      
      <p className="mt-4">Masz już konto?</p>
      
      <Link to="/LoginPage" className="text-blue-500">
        <button className="btn mt-5 bg-green-600 rounded-xl w-40 h-10 text-black">Zaloguj się</button>
      </Link>
    </div>
  );
}

export default FormSign;
