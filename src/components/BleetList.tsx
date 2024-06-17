import React, { useState, useEffect } from 'react';
import axios from 'axios';
import BleetComponent from './BleetComponent';


interface Bleet {
  id: string;
  content: string;
  dateCreatedUtc: string;
  authorId: string;
  author?: string | null;
  comments?: string[] | null;
  likes?: number | null;
  createdBy?: string | null;
  dataDeletedUtc?: string | null;
  dateModifiedUtc?: string | null;
  deletedBy?: string | null;
  modifiedBy?: string | null;
}

function BleetList() {
  const [bleets, setBleets] = useState<Bleet[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchBleets = async () => {
      try {
        const token = localStorage.getItem('authToken');
        const response = await axios.get('https://localhost:5002/Bleet?PageSize=10&Page=1', {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        console.log('API Response:', response.data);

        if (response.status === 200) {
          // If the response is wrapped, adjust this accordingly
          const bleetsArray = response.data.data.bleets || response.data.data;
          if (Array.isArray(bleetsArray)) {
            setBleets(bleetsArray);
            console.log('Bleets state set to:', bleetsArray);
          } else {
            setError('Unexpected response format');
          }
        }
         else {
          setError('Failed to fetch bleets');
        }
      } catch (error: any) {
        console.error('Failed to fetch bleets:', error);
        setError('Failed to fetch bleets: ' + (error.response?.data?.message || error.message));
      } finally {
        setLoading(false);
      }
    };

    fetchBleets();
  }, []);

  // Log the state to check if it updates correctly
  console.log('Current bleets state:', bleets);

  if (loading) {
    return <p>Ładowanie...</p>;
  }
  console.log('Ilość bleetów:', bleets.length);
  return (
    <div className="content-center">
      <h1 className="text-5xl mt-5">Lista Bleetów</h1>

      {error && <p className="text-red-500 mt-4">{error}</p>}

      {bleets.length > 0 ? (
        <div className="mt-5 space-y-6">
          {bleets.map((bleet) => (
            <BleetComponent key={bleet.id} bleet={bleet} />
          ))}
        </div>
      ) : (
        <p>Brak Bleetów do wyświetlenia</p>
      )}
     
    </div>
  );
}

export default BleetList;
