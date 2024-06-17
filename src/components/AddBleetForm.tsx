import React, { useState } from 'react';
import axios from './axiosConfig';
import { useNavigate } from 'react-router-dom';

function AddBleetForm() {
  const [content, setContent] = useState('');
  
  const [image, setImage] = useState<File | null>(null);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
 
  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (files && files.length > 0) {
      setImage(files[0]);
    }
  };

  const handleAddBleet = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    

    // Sprawdzenie czy wszystkie wymagane pola są wypełnione
    if (!content) {
      setError('Content is required');
      return;
    }

    // Przygotowanie danych do wysłania
   
    

    try {
      const response = await axios.post('https://localhost:5002/bleet', {content})
      

      if (response.status === 200) {
        // Przekierowanie po dodaniu bleeta
        navigate('/');
        console.log('Bleet added successfully:', response.data);
        setContent(''); // Clear the input after successful post
      } else {
        setError('Failed to add bleet');
      }
    } catch (error: any) {
      console.error('Failed to add bleet:', error);
      setError('Failed to add bleet: ' + (error.response?.data?.message || error.message));
    }
  };

  return (
    <div className="content-center">
      <h1 className="text-5xl mt-5">Dodaj nowy Bleet</h1>
      <form className="mt-10" onSubmit={handleAddBleet}>
        <textarea
          placeholder="Co nowego?"
          className="input bg-black rounded-xl text-center h-32 w-80"
          value={content}
          onChange={(e) => setContent(e.target.value)}
        />
        <p className="text-slate-600">Treść Bleetu</p>

        <input
          type="file"
          accept=".jpg,.jpeg,.png"
          className="mt-4"
          onChange={handleFileChange}
        />
        <p className="text-slate-600">Dodaj zdjęcie (opcjonalnie)</p>

        {error && <p className="text-red-500 mt-4">{error}</p>}

        <button type="submit" className="btn mt-5 bg-green-600 rounded-xl w-80 h-10 text-black">
          Dodaj Bleet
        </button>
      </form>
    </div>
  );
}

export default AddBleetForm;
